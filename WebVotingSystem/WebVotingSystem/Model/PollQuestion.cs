using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebVotingSystem.Model
{
    public class PollQuestion
    {
        public int PollQuestionID { get; set; }
        public string Question { get; set; }

        // By Default, ICollection creates a HashSet
        public ICollection<PollChoice> Answers { get; set; }
    }
}
