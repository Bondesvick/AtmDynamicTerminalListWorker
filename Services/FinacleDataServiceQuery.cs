using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AtmDynamicTerminalListWorker.Entities.Finacle;
using AtmDynamicTerminalListWorker.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AtmDynamicTerminalListWorker.Services
{
    public class FinacleDataServiceQuery : IFinacleDataService
    {
        private readonly FinacleDbContext _finacleDbContext;
        private readonly IConfiguration _configuration;

        public FinacleDataServiceQuery(FinacleDbContext finacleDbContext, IConfiguration configuration)
        {
            _finacleDbContext = finacleDbContext;
            _configuration = configuration;
        }

        public IEnumerable<FinacleAtmItem> GetAtmItems()
        {
            var query = _configuration["AppSettings:FinacleAtmItemQuery"];

            return _finacleDbContext.FinacleAtmItems
                .FromSqlRaw(query).AsNoTracking()
                .ToList();
        }

        public IEnumerable<FinacleBranchDetail> GetBranchDetails()
        {
            var query = _configuration["AppSettings:FinacleBranchDetail"];

            return _finacleDbContext.FincleBranchDetails
                .FromSqlRaw(query).AsNoTracking()
                .ToList();
        }

        //public IEnumerable<FinacleAtmItem> GetAtmItems()
        //{
        //    var query = _configuration["AppSettings:FinacleAtmItemQuery"];

        //    return _finacleDbContext.FinacleAtmItems
        //        .FromSqlRaw(@"
        //           SELECT sub_module_name atm_terminal_Id, variable_name||'NGN'||variable_Value ATM_GL FROM tbaadm.c_svrsetvar WHERE module_name='PSLATMCW'
        //           AND variable_name IN (SELECT sol_id FROM tbaadm.sol WHERE del_flg='N')
        //        ")
        //        .ToList();
        //}
    }
}