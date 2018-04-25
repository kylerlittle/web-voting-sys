using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web_voting_sys.Data;
using web_voting_sys.Model;

namespace web_voting_sys.Pages.Polls
{
    public class VoteModel : PageModel
    {
        private readonly web_voting_sys.Data.PollContext _context;

        public VoteModel(web_voting_sys.Data.PollContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Poll Poll { get; set; }
        [BindProperty]
        public List<PollQuestion> PollQuestions { get; set; }
        [BindProperty]
        public List<List<PollChoice>> PollChoices { get; set; }
        [BindProperty]
        public List<int> SelectedAnswers { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Grab the poll that matches the ID passed as route value!
            Poll = await _context.Polls.SingleOrDefaultAsync(m => m.ID == id);

            // If Poll is null, then for some reason it doesn't exist...
            if (Poll == null)
            {
                return NotFound();
            }

            // Use LINQ query to filter only poll questions that have IDs that match the poll ID (route value)
            PollQuestions = await _context.PollQuestions
                .AsNoTracking()
                .Where(q => q.PollID == id)
                .ToListAsync();

            // Use LINQ query again to filter only poll choices that have IDs that match the corresponding question IDs
            PollChoices = new List<List<PollChoice>>(Poll.MaximimumQuestions);
            foreach (PollQuestion pollQuestion in PollQuestions)
            {
                List<PollChoice> choicesForQuestion = await _context.PollChoices
                    .AsNoTracking()
                    .Where(pq => pq.PollQuestionID == pollQuestion.ID)
                    .ToListAsync();
                PollChoices.Add(new List<PollChoice>(choicesForQuestion));
            }

            // Lastly, initialize SelectedAnswers with default values so that the page can accept the form.
            SelectedAnswers = new List<int>(Poll.NumberOfQuestions);
            for (int i = 0; i < PollQuestions.Count; ++i)
            {
                SelectedAnswers.Add(Int32.MinValue);         // by default, set to empty
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            for (int i = 0; i < SelectedAnswers.Count; ++i)
            {
                PollChoice pc = await _context.PollChoices.SingleOrDefaultAsync(c => c.ID == SelectedAnswers[i]);    // grab poll choice from db that matches selected answer
                pc.VoteTally += 1;      // increment the vote tally since the user selected it
                _context.Attach(pc).State = EntityState.Modified;         // declare that the poll choice has been modified
                await _context.SaveChangesAsync();            // save changes to database
            }

            return RedirectToPage("./VoteConfirmation");
        }

        private bool PollExists(int id)
        {
            return _context.Polls.Any(e => e.ID == id);
        }
    }
}
