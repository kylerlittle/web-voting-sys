using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using web_voting_sys.Data;
using web_voting_sys.Model;

namespace web_voting_sys.Pages.Polls
{
    public class CreateModel : PageModel
    {
        private readonly web_voting_sys.Data.PollContext _context;

        public CreateModel(web_voting_sys.Data.PollContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            // Testing: here I put in some default values for the form
            PollChoices = new List<List<PollChoice>>
            {
                new List<PollChoice>
                {
                    new PollChoice
                    {
                        Choice = "Kyler"
                    },
                    new PollChoice
                    {
                        Choice = "Emily"
                    },
                    new PollChoice
                    {
                        Choice = "Lance"
                    }
                },
                new List<PollChoice>
                {
                    new PollChoice
                    {
                        Choice = "Red"
                    },
                    new PollChoice
                    {
                        Choice = "Green"
                    },
                    new PollChoice
                    {
                        Choice = "Blue"
                    }
                }
            };
            PollQuestions = new List<PollQuestion>
            {
                new PollQuestion
                {
                    Question = "What is your name?",
                    Answers = PollChoices[0]
                },
                new PollQuestion
                {
                    Question = "What is your favorite color?",
                    Answers = PollChoices[1]
                }
            };
            
            Poll = new Poll
            {
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddSeconds(300.0),           // 5 min poll
                Name = "Default",
                PollCreator = "Default",
                Type = PollType.Public,
                NumberOfQuestions = 2,
                AnswersPerQuestion = 3,
                Questions = PollQuestions
            };
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

            // Add poll
            _context.Polls.Add(Poll);
            await _context.SaveChangesAsync();

            // Continue creation of poll by creating the questions, pass Poll's ID to ensure questions are associated with correct poll.
            return RedirectToPagePermanent("./CreatePollQuestions", new { id = Poll.ID });
        }
    }
}