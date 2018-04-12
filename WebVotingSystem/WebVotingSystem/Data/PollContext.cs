using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebVotingSystem.Model;
using Microsoft.EntityFrameworkCore;

namespace WebVotingSystem.Data
{
    public class PollContext : DbContext
    {
        public PollContext(DbContextOptions<PollContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Poll> Polls { get; set; }

        /// <summary>
        /// Because developers disagree on whether or not the table name should be plural, we override
        /// the behavior so it is singular.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Poll>().ToTable("Poll");
        }
    }
}
