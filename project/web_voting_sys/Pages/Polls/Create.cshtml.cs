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
            PollQuestions = new List<PollQuestion>
            {
                new PollQuestion
                {
                    Question = "What is your name?",
                    Answers = new List<PollChoice>
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
                    }
                },
                new PollQuestion
                {
                    Question = "What is your favorite color?",
                    Answers = new List<PollChoice>
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
                Questions = PollQuestions
            };
            return Page();
        }

        [BindProperty]
        public Poll Poll { get; set; }

        [BindProperty]
        public List<PollQuestion> PollQuestions { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Poll.Questions = PollQuestions;
            _context.Polls.Add(Poll);
            await _context.SaveChangesAsync();
            
            for (int i = 0; i < PollQuestions.Count; ++i)
            {
                PollQuestions[i].PollID = Poll.ID;
            }
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}