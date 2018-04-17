using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_voting_sys.Model
{
    public class PollQuestion
    {
        public int ID { get; set; }      // primary key
        public string Question { get; set; }
        public IEnumerable<PollChoice> Answers { get; set; }
    }
}
