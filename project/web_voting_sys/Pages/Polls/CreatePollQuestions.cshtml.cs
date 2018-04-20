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
    // Continuation of Poll Creation (add Qs and answers in this part)
    // Based on Edit Template of CRUD
    public class CreatePollQuestionsModel : PageModel
    {
        private readonly web_voting_sys.Data.PollContext _context;

        public CreatePollQuestionsModel(web_voting_sys.Data.PollContext context)
        {
            _context = context;
        }

        // Only way to access this page is through Create for Polls, so id is an int rather than int?
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Poll = await _context.Polls.SingleOrDefaultAsync(m => m.ID == id);

            if (Poll == null)
            {
                return NotFound();
            }

            // Next, seed List<PollQuestion> and List<List<PollChoice>> with something since we know
            // their sizes now.
            PollQuestions = new List<PollQuestion>(Poll.NumberOfQuestions);       // initial capacity: number of questions
            PollChoices = new List<List<PollChoice>>(Poll.NumberOfQuestions);          // initial capacity: number of questions
            for (int i = 0; i < Poll.NumberOfQuestions; ++i)          // counting loop
            {
                List<PollChoice> addNext = new List<PollChoice>(Poll.AnswersPerQuestion);
                for (int j = 0; j < Poll.AnswersPerQuestion; ++j)         // counting loop
                {
                    //addNext.Append(new PollChoice { Choice = "", VoteTally = 0 });          // for each List<PollChoice>, append a new PollChoice with default values
                    addNext.Add(new PollChoice { Choice = "", VoteTally = 0 });
                }
                PollChoices.Add(addNext);
                PollQuestions.Add(new PollQuestion               // for each PollQuestion in PollQuestions, append a new PollQuestion with default values
                {
                    Question = "",
                    Answers = addNext
                });
            }

            return Page();
        }

        [BindProperty]
        public Poll Poll { get; set; }

        [BindProperty]
        public List<PollQuestion> PollQuestions { get; set; }

        [BindProperty]
        public List<List<PollChoice>> PollChoices { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Add relationships for each 'PollQuestion'
            for (int i = 0; i < PollQuestions.Count; ++i)
            {
                PollQuestions[i].Answers = PollChoices[i];
                PollQuestions[i].PollID = Poll.ID;
                _context.PollQuestions.Add(PollQuestions[i]);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}