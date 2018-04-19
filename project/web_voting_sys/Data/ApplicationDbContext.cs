using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using web_voting_sys.Model;

namespace web_voting_sys.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }

    public class PollContext : DbContext
    {
        public PollContext(DbContextOptions<PollContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Poll> Polls { get; set; }
        public DbSet<PollQuestion> PollQuestions { get; set; }
        public DbSet<PollChoice> PollChoices { get; set; }

        /// <summary>
        /// Because developers disagree on whether or not the table name should be plural, we override
        /// the behavior so it is singular.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Poll>().ToTable("Poll");
            modelBuilder.Entity<PollQuestion>().ToTable("PollQuestion");
            modelBuilder.Entity<PollChoice>().ToTable("PollChoice");
        }
    }
}
