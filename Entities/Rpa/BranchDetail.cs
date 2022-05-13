using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmDynamicTerminalListWorker.Entities.Rpa
{
    [Table("RPA_BranchDetail")]
    public class BranchDetail
    {
        public long ID { get; set; }

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