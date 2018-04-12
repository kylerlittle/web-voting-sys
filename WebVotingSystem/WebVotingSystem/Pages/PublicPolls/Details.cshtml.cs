using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebVotingSystem.Data;
using WebVotingSystem.Model;

namespace WebVotingSystem.Pages.PublicPolls
{
    public class DetailsModel : PageModel
    {
        private readonly WebVotingSystem.Data.PollContext _context;

        public DetailsModel(WebVotingSystem.Data.PollContext context)
        {
            _context = context;
        }

        public Poll Poll { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Poll = await _context.Polls.SingleOrDefaultAsync(m => m.PollID == id);

            if (Poll == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
