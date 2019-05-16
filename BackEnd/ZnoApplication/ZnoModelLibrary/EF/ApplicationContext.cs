using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using ZnoModelLibrary.Entities;

namespace ZnoModelLibrary.EF
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder
            //    .Entity<ApplicationUser>()
            //    .Property(e => e.Status)
            //    .HasConversion(
            //        v => v.ToString(),
            //        v => (Status)Enum.Parse(typeof(Status), v));
        }

        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestType> TestTypes { get; set; }
        public DbSet<TestSettings> TestSettings { get; set; }
    }
}