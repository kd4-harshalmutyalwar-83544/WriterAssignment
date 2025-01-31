using Microsoft.EntityFrameworkCore;

namespace WriterAssign.Models
{
    public class PostDBContext : DbContext
    {

        public PostDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Post> Posts { get; set; }
    }
}

