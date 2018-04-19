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
            // Initialize  "Num Question Options List." By default, let's have something like 5... would need to change in "Edit" Page as well.
            // which means there is a better design choice out there. Ah well.
            int maxNumQs = 5;
            NumQuestionOptions = new List<int>(maxNumQs);
            for (int i = 0; i < NumQuestionOptions.Capacity; ++i)
            {
                NumQuestionOptions.Append(i);
            }

            // Testing:
            List<PollQuestion> pollQuestions = new List<PollQuestion>
            {
                new PollQuestion
                {
                    Question = "What is your name?",
                    Answers = new PollChoice[]
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
                    Question = "What is your favorite color",
                    Answers = new PollChoice[]
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
                PollCreator = "Default Creator",
                Type = PollType.Public,
                NumberOfQuestions = 2,
                Questions = pollQuestions
            };
            return Page();
        }

        //public int NumberOfQuestions { get; set; }

        [BindProperty]
        public PollQuestion SingleQ { get; set; }
        //public List<PollQuestion> PollQuestions { get; set; }
        public List<int> NumQuestionOptions { get; set; }

        [BindProperty]
        public Poll Poll { get; set; }

        //[BindProperty]
        //public PollQuestion PollQuestion { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //Poll.Questions.Append(SingleQ);
            _context.Polls.Add(Poll);
            await _context.SaveChangesAsync();

            _context.PollQuestions.Add(SingleQ);
            await _context.SaveChangesAsync();

            Console.WriteLine(String.Format("Q: {0}", SingleQ.Question));

            return RedirectToPage("./Index");
        }
    }
}