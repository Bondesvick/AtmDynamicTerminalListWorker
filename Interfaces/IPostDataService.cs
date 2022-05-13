using System.Collections.Generic;
using AtmDynamicTerminalListWorker.Entities.Post;

namespace AtmDynamicTerminalListWorker.Interfaces
{
    public interface IPostDataService
    {
        IEnumerable<PostAtmItem> GetData();
    }
}