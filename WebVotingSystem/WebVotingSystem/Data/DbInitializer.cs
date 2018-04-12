using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebVotingSystem.Model;

namespace WebVotingSystem.Data
{
    public static class DbInitializer
    {
        public static void Initialize(PollContext context)
        {
            context.Database.EnsureCreated();

            // Look for any polls
            if (context.Polls.Any())
            {
                return;         // database already seeded; no need to initialize
            }

            //Dictionary<string, List<string>> pollQuestions1 = new Dictionary<string, List<string>>
            //{
            //    ["What is your favorite color?"] = { "red", "blue", "green" },
            //    ["What is the capital of Idaho?"] = { "Boise", "Nampa", "Coeur d'Alene" }
            //};

            //Dictionary<string, Dictionary<string, int>> results1 = new Dictionary<string, Dictionary<string, int>>
            //{
            //    ["What is your favorite color?"] = { ["red"] = 1, ["blue"] = 5, ["green"] = 10 },
            //    ["What is the capital of Idaho?"] = { ["Boise"] = 5, ["Nampa"] = 6, ["Coeur d'Alene"] = 5 }
            //};

            var pollQs1 = new PollQuestion[]
            {
                new PollQuestion{PollQuestionID=1, Question="What is your favorite color?",
                    Answers = new PollChoice[] { new PollChoice{ID=1, Choice="red" },
                    new PollChoice{ID=2, Choice="blue" }, new PollChoice{ID=3, Choice="green"} } }
            };

            // CONTINUE HERE TOMORROW
            // https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/intro?view=aspnetcore-2.1

            var polls = new Poll[]
            {
                new Poll{PollID=1,StartTime=DateTime.Parse("2018-03-14"), EndTime=DateTime.Parse("2018-05-05"), Name="TestPoll1", PollCreator="Kyler",
                Type="Registered Public", Questions=pollQs1 }
            };

            foreach (Poll poll in polls)
            {
                context.Polls.Add(poll);
            }
            context.SaveChanges();
        }
    }
}
