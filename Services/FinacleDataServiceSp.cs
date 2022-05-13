using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AtmDynamicTerminalListWorker.Entities.Finacle;
using AtmDynamicTerminalListWorker.Interfaces;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;

namespace AtmDynamicTerminalListWorker.Services
{
    public class FinacleDataServiceSp : IFinacleDataService
    {
        private readonly FinacleDbContext _finacleDbContext;

        public FinacleDataServiceSp(FinacleDbContext finacleDbContext)
        {
            _finacleDbContext = finacleDbContext;
        }

        public IEnumerable<FinacleAtmItem> GetAtmItems()
        {
            var atmGls = new OracleParameter("atmGlsParam", OracleDbType.RefCursor, ParameterDirection.Output);
            return _finacleDbContext.FinacleAtmItems
                .FromSqlRaw("BEGIN SP_GET_ATM_GL(:atmGlsParam); END;", new object[] { atmGls })  // Please do not touch; works exactly as expected
                .ToList();
        }

        public IEnumerable<FinacleBranchDetail> GetBranchDetails()
        {
            throw new System.NotImplementedException();
        }
    }
}