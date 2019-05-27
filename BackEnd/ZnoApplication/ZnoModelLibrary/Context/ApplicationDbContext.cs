using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZnoModelLibrary.Entities;

namespace ZnoModelLibrary.Context
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

            builder.Entity<QuestionContent>()
                .HasKey(qc => new { qc.QuestionId, qc.ContentId });

            builder.Entity<QuestionContent>()
                .HasOne(qc => qc.Question)
                .WithMany(qc => qc.Contents)
                .HasForeignKey(qc => qc.QuestionId);

            builder.Entity<QuestionContent>()
                .HasOne(qc => qc.Content)
                .WithMany(qc => qc.Questions)
                .HasForeignKey(qc => qc.ContentId);

            builder.Entity<TestSettingsAnswerType>()
                .HasKey(ta => new { ta.TestSettingsId, ta.AnswerTypeId });

            builder.Entity<TestSettingsAnswerType>()
                .HasOne(ta => ta.TestSettings)
                .WithMany(ta => ta.AnswerTypes)
                .HasForeignKey(ta => ta.TestSettingsId);

            builder.Entity<TestSettingsAnswerType>()
                .HasOne(ta => ta.AnswerType)
                .WithMany(ta => ta.TestSettings)
                .HasForeignKey(ta => ta.AnswerTypeId);
        }

        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestType> TestTypes { get; set; }
        public DbSet<TestSettings> TestSettings { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<QuestionContent> QuestionContents { get; set; }
        public DbSet<AnswerType> AnswerTypes { get; set; }
        public DbSet<TestSettingsAnswerType> TestSettingsAnswerTypes { get; set; }
    }
}