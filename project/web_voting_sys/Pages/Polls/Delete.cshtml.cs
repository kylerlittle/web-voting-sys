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
    public class DeleteModel : PageModel
    {
        private readonly web_voting_sys.Data.PollContext _context;

        public DeleteModel(web_voting_sys.Data.PollContext context)
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

            Poll = await _context.Polls.SingleOrDefaultAsync(m => m.ID == id);

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

            return RedirectToPage("./Pollindex");
        }
    }
}
