using Microsoft.EntityFrameworkCore;

namespace products.Models
{
    public class Context : DbContext
    {
        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }

        public DbSet<Joins> joins { get; set; }

        public Context(DbContextOptions<Context> options) : base(options) { }
    }
} 