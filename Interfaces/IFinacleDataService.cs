using System.Collections.Generic;
using AtmDynamicTerminalListWorker.Entities.Finacle;

namespace AtmDynamicTerminalListWorker.Interfaces
{
    public interface IFinacleDataService
    {
        IEnumerable<FinacleAtmItem> GetAtmItems();

        IEnumerable<FinacleBranchDetail> GetBranchDetails();
    }
}