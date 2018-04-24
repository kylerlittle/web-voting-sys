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
    public class VoteConfirmationModel : PageModel
    {
        private readonly web_voting_sys.Data.PollContext _context;

        public VoteConfirmationModel(web_voting_sys.Data.PollContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Poll Poll { get; set; }
        public List<PollQuestion> PollQuestions { get; set; }
        public List<List<PollChoice>> PollChoices { get; set; }
        
        public async Task<IActionResult> OnGetAsync(int? id, int? question, int? choice)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (question == null)
            {
                return NotFound();
            }
            if (choice == null)
            {
                return NotFound();
            }
            // Grab the poll that matches the ID passed as route value!
            Poll = await _context.Polls.SingleOrDefaultAsync(m => m.ID == id);

            // Use LINQ query to filter only poll questions that have IDs that match the poll ID (route value)
            PollQuestions = await _context.PollQuestions
                .AsNoTracking()
                .Where(q => q.PollID == id)
                .ToListAsync();

            // TODO: PollChoices.Append() isn't actually working? Not sure why...
            PollChoices = new List<List<PollChoice>>(Poll.MaximimumQuestions);
            foreach (PollQuestion pollQuestion in PollQuestions)
            {
                List<PollChoice> choicesForQuestion = await _context.PollChoices
                    .AsNoTracking()
                    .Where(pq => pq.PollQuestionID == pollQuestion.ID)
                    .ToListAsync();
                PollChoices.Add(new List<PollChoice>(choicesForQuestion));
            }

            //Vote Tally incremented of the particular choice
            PollChoices[(int) question][(int)choice].VoteTally++;

            if (Poll == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Poll).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PollExists(Poll.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PollExists(int id)
        {
            return _context.Polls.Any(e => e.ID == id);
        }
    }
}