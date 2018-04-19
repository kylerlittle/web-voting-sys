using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_voting_sys.Model
{
    public class PollQuestion
    {
        public int ID { get; set; }      // primary key
        public int PollID { get; set; }      // poll that this question belongs to
        public string Question { get; set; }
        public List<PollChoice> Answers { get; set; }
    }
}
