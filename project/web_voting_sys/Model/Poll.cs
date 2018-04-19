using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace web_voting_sys.Model
{
    public enum PollType {
        Public,
        [DisplayFormat(DataFormatString ="Invite Only")]
        Invite_Only }
    public class Poll
    {
        public int ID { get; set; }        // primary key
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Name { get; set; }
        public string PollCreator { get; set; }
        public PollType Type { get; set; }
        public readonly int MaximimumQuestions = 5;          // make the default max 5 for now
        public int NumberOfQuestions { get; set; }
        public readonly int MaximumAnswersPerQuestion = 5;           // make default max 5 for now
        public int AnswersPerQuestion { get; set; }         // for now, have same amount of answers for each question (in the future, we could change this)
        public List<PollQuestion> Questions { get; set; }
        public List<Data.ApplicationUser> Voters { get; set; }
    }
}