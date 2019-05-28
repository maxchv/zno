using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Zno.DAL.Entities;

namespace Zno.DAL.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TestSettingsQuestionType>()
                .HasKey(ta => new { ta.TestSettingsId, ta.QuestionTypeId });

            builder.Entity<TestSettingsQuestionType>()
                .HasOne(ta => ta.TestSettings)
                .WithMany(ta => ta.QuestionTypes)
                .HasForeignKey(ta => ta.TestSettingsId);

            builder.Entity<TestSettingsQuestionType>()
                .HasOne(ta => ta.QuestionType)
                .WithMany(ta => ta.TestSettings)
                .HasForeignKey(ta => ta.QuestionTypeId);
        }

        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestType> TestTypes { get; set; }
        public DbSet<TestSettings> TestSettings { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<QuestionType> QuestionTypes { get; set; }
        public DbSet<TestSettingsQuestionType> TestSettingsAnswerTypes { get; set; }
        public DbSet<ContentType> ContentTypes { get; set; }
    }
}