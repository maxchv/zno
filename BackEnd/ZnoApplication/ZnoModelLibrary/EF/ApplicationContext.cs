using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
        }

        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Test> Test { get; set; }
        public DbSet<TestType> TestType { get; set; }
    }
}