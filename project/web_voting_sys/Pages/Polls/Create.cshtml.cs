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
            return Page();
        }

        [BindProperty]
        public Poll Poll { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Polls.Add(Poll);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}