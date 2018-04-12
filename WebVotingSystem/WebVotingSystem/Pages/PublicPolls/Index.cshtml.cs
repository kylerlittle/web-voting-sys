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
    public class IndexModel : PageModel
    {
        private readonly WebVotingSystem.Data.PollContext _context;

        public IndexModel(WebVotingSystem.Data.PollContext context)
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
