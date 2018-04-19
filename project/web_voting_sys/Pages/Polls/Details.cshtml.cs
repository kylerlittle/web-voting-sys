using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using web_voting_sys.Data;
using web_voting_sys.Model;

namespace web_voting_sys.Pages.Polls
{
    public class DetailsModel : PageModel
    {
        private readonly web_voting_sys.Data.PollContext _context;

        public DetailsModel(web_voting_sys.Data.PollContext context)
        {
            _context = context;
        }

        public Poll Poll { get; set; }
        public List<PollQuestion> PollQuestions { get; set; }
        public List<List<PollChoice>> PollChoices { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
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
                PollChoices.Append(new List<PollChoice>(choicesForQuestion));
            }

            if (Poll == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
