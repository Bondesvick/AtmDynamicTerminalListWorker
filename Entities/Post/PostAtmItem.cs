using Microsoft.EntityFrameworkCore;

namespace AtmDynamicTerminalListWorker.Entities.Post
{
    [Keyless]
    public class PostAtmItem
    {
        public string TerminalID { get; set; }
        public string IPAddress { get; set; }
        public string OnlineStatus { get; set; }
        public string BranchCode { get; set; }
        public string TerminalBrand { get; set; }
    }
}