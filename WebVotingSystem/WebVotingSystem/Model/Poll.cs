using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebVotingSystem.Model
{
    //public enum PollType { ACCOUNT_FREE, REGISTERED_PUBLIC, INVITE_ONLY }
    public class Poll
    {        
        public int PollID { get; set; }        // primary key
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Name { get; set; }
        public string PollCreator { get; set; }

        
        //public Dictionary<string, List<string>> Questions { get; set; }                      // < Question, List < Choice > >
        //public Dictionary<string, Dictionary<string, int>> Results { get; set; }                    // < Question, < Choice, NumberOfVotes > >
        public string Type { get; set; }


        public IEnumerable<PollQuestion> Questions { get; set; }
    }
}
