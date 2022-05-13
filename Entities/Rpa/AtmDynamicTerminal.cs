using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmDynamicTerminalListWorker.Entities.Rpa
{
    [Table("RPA_ATM_Dynamic_List")]
    public class AtmDynamicTerminal
    {
        public Guid Id { get; set; }

        [Column("TerminalID")]
        public string TerminalId { get; set; }

        [Column("IPAddress")]
        public string IpAddress { get; set; }

        [Column("OnlineStatus")]
        public string OnlineStatus { get; set; }

        [Column("BranchCode")]
        public string BranchCode { get; set; }

        [Column("TerminalBrand")]
        public string TerminalBrand { get; set; }
    }
}