using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Syzoj.Api.Models;

namespace Syzoj.Api.Data
{
    public class ApplicationDbContext : IdentityUserContext<ApplicationUser>
    {
        public DbSet<Problem> Problems { get; set; }
        public DbSet<Problemset> Problemsets { get; set; }
        public DbSet<ProblemsetProblem> ProblemsetProblems { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProblemsetProblem>()
                .HasKey(psp => new { psp.ProblemsetId, psp.ProblemId });
            modelBuilder.Entity<ProblemsetProblem>()
                .HasIndex(psp => new { psp.ProblemsetId, psp.ProblemsetProblemId })
                .IsUnique();
            modelBuilder.Entity<ProblemsetProblem>()
                .HasOne(psp => psp.Problem)
                .WithMany(p => p.ProblemsetProblems)
                .HasForeignKey(psp => psp.ProblemId)
                .HasPrincipalKey(p => p.Id);
            modelBuilder.Entity<ProblemsetProblem>()
                .HasOne(psp => psp.Problemset)
                .WithMany(ps => ps.ProblemsetProblems)
                .HasForeignKey(psp => psp.ProblemsetId)
                .HasPrincipalKey(ps => ps.Id);
            
            var defaultProblemset = new Problemset() { Id = 1, Type = "debug" };
            modelBuilder.Entity<Problemset>()
                .HasData(defaultProblemset);
            var defaultProblem = new Problem() { Id = 1, ProblemType = null, Path = "/data/problem/1", Title = "Test problem", Content = null };
            modelBuilder.Entity<Problem>()
                .HasData(defaultProblem);
            var defaultProblemsetProblem = new ProblemsetProblem() { ProblemsetId = defaultProblemset.Id, ProblemId = defaultProblem.Id, ProblemsetProblemId = "debug" };
            modelBuilder.Entity<ProblemsetProblem>()
                .HasData(defaultProblemsetProblem);
        }
    }
}