using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_voting_sys.Model;

namespace web_voting_sys.Data
{
    public class PollContextInitializer
    {
        public static void Initialize(PollContext context)
        {
            context.Database.EnsureCreated();

            // Look for any polls
            if (context.Polls.Any())
            {
                return;         // database already seeded; no need to initialize
            }

            var pollQs = new List<PollQuestion>
            {
                new PollQuestion{
                    ID =1,
                    Question ="What is your favorite color?",
                    Answers = new PollChoice[] { new PollChoice{ID=1, Choice="red" },
                    new PollChoice{ID=2, Choice="blue" }, new PollChoice{ID=3, Choice="green"} } }
            };

            foreach (PollQuestion pollQ in pollQs)
            {
                context.PollQuestions.Add(pollQ);
            }

            // CONTINUE HERE TOMORROW
            // https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/intro?view=aspnetcore-2.1

            var polls = new Poll[]
            {
                new Poll{ID=1,StartTime=DateTime.Parse("2018-03-14"), EndTime=DateTime.Parse("2018-05-05"), Name="TestPoll1", PollCreator="Kyler",
                Type=PollType.Public, Questions=pollQs }
            };

            foreach (Poll poll in polls)
            {
                
                context.Polls.Add(poll);
            }
            context.SaveChanges();


        }
    }

}
