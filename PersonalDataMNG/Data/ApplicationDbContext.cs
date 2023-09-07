using Microsoft.EntityFrameworkCore;
using PersonalDataMNG.Models;

namespace PersonalDataMNG.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {  
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Category> Category { get; set; }
    }
}
