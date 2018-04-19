using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_voting_sys.Model
{
    public enum PollType {
        Public,
        Invite_Only }
    public class Poll
    {
        public int ID { get; set; }        // primary key
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Name { get; set; }
        public string PollCreator { get; set; }
        public PollType Type { get; set; }
        public int MaximimumQuestions = 5;
        public int NumberOfQuestions { get; set; }
        public List<PollQuestion> Questions { get; set; }
        public IEnumerable<Data.ApplicationUser> Voters { get; set; }
    }
}