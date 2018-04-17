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
    public class IndexModel : PageModel
    {
        private readonly web_voting_sys.Data.PollContext _context;

        public IndexModel(web_voting_sys.Data.PollContext context)
        {
            _context = context;
        }

        public IList<Poll> Poll { get;set; }

        public async Task OnGetAsync()
        {
            Poll = await _context.Polls.ToListAsync();
        }
    }
}
