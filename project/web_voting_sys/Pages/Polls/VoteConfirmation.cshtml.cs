using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web_voting_sys.Data;
using web_voting_sys.Model;

namespace web_voting_sys.Pages.Polls
{
    public class VoteConfirmationModel : PageModel
    {
        private readonly web_voting_sys.Data.PollContext _context;

        public VoteConfirmationModel(web_voting_sys.Data.PollContext context)
        {
            _context = context;
        }        
        public void OnGet() { }
    }
}