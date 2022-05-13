using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AtmDynamicTerminalListWorker.Entities.Post
{
    public class PostDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<PostAtmItem> PostAtmItems { get; set; }

        public PostDbContext(DbContextOptions<PostDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(_configuration.GetConnectionString("PostDBConnection"));
    }
}