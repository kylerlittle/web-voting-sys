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
    public class DeleteModel : PageModel
    {
        private readonly WebVotingSystem.Data.PollContext _context;

        public DeleteModel(WebVotingSystem.Data.PollContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Poll = await _context.Polls.FindAsync(id);

            if (Poll != null)
            {
                _context.Polls.Remove(Poll);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
