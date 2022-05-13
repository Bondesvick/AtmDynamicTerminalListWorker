using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AtmDynamicTerminalListWorker.Entities.Finacle
{
    public class FinacleDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<FinacleAtmItem> FinacleAtmItems { get; set; }
        public DbSet<FinacleBranchDetail> FincleBranchDetails { get; set; }

        public FinacleDbContext(DbContextOptions<FinacleDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseOracle(_configuration.GetConnectionString("FinacleDBConnection"));
    }
}