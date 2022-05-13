using System.Threading.Tasks;

namespace AtmDynamicTerminalListWorker.Interfaces
{
    public interface IRpaDataService
    {
        Task UpdateAtmLists();
    }
}