using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AtmDynamicTerminalListWorker.Entities.Finacle
{
    [Keyless]
    public class FinacleAtmItem
    {
        [Column("ATM_TERMINAL_ID")]
        public string TerminalID { get; set; }
        [Column("ATM_GL")]
        public string AtmGL { get; set; }
    }
}