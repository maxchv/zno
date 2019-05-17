using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZnoModelLibrary.Entities;

namespace ZnoModelLibrary.EF
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<GeneratedTest> GeneratedTests { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<TestSettings> TestSettings { get; set; }
        public virtual DbSet<TestType> TestTypes { get; set; }
        public virtual DbSet<UserAnswer> UserAnswers { get; set; }


        public ApplicationContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}