using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_voting_sys.Model
{
    public class PollChoice
    {
        public int ID { get; set; }           // primary key
        public string Choice { get; set; }
        public int VoteTally { get; set; }
    }
}
