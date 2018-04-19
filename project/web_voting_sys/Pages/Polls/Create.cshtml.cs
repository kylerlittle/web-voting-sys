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

            // Add relationships for 'Poll'
            Poll.Questions = PollQuestions;
            for (int i = 0; i < Poll.Questions.Count; ++i)
            {
                Poll.Questions[i].Answers = PollChoices[i];
            }

            // Add poll
            _context.Polls.Add(Poll);
            await _context.SaveChangesAsync();
            
            //// Add relationships for 'PollQuestions'
            //for (int i = 0; i < PollQuestions.Count; ++i)
            //{
            //    PollQuestions[i].PollID = Poll.ID;
            //    PollQuestions[i].Answers = PollChoices[i];

            //    // Add each PollQuestion in list
            //    _context.PollQuestions.Add(PollQuestions[i]);
            //    await _context.SaveChangesAsync();
            //}

            //// Add relationships for 'PollChoices'
            //for (int i = 0; i < PollChoices.Count; ++i)
            //{
            //    for (int j = 0; j < PollChoices[i].Count; ++j)
            //    {
            //        PollChoices[i][j].PollQuestionID = PollQuestions[i].ID;
            //        PollChoices[i][j].VoteTally = 0;         // on creation, nobody has voted for the choice yet

            //        // Add each PollChoice to database
            //        _context.PollChoices.Add(PollChoices[i][j]);
            //        await _context.SaveChangesAsync();
            //    }
            //}

            return RedirectToPage("./Index");
        }
    }
}