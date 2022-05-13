using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AtmDynamicTerminalListWorker.Entities.Finacle
{
    [Keyless]
    public class FinacleBranchDetail
    {
        [Column("BranchSol")]
        public string BranchSol { get; set; }

        [Column("BranchName")]
        public string BranchName { get; set; }

        [Column("BranchAddress")]
        public string BranchAddress { get; set; }

        [Column("State")]
        public string State { get; set; }

        [Column("BranchEmail")]
        public string BranchEmail { get; set; }
    }
}