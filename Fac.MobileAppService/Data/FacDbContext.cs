using Fac.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Fac.Api.Data
{
    public class FacDbContext : DbContext
    {
        public FacDbContext(DbContextOptions<FacDbContext> options)
            : base(options)
        {
        }

        public DbSet<Case> Case { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<SubCategory> SubCategory { get; set; }
    }
}
