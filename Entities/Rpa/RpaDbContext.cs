using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AtmDynamicTerminalListWorker.Entities.Rpa
{
    public class RpaDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<AtmDynamicTerminal> AtmDynamicTerminals { get; set; }
        public DbSet<AtmGL> AtmGls { get; set; }
        public DbSet<BranchDetail> BranchDetails { get; set; }

        public RpaDbContext(DbContextOptions<RpaDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
            Database.SetCommandTimeout(600);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(_configuration.GetConnectionString("RpaDBConnection"));
    }
}