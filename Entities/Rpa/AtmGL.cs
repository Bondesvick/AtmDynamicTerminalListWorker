using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmDynamicTerminalListWorker.Entities.Rpa
{
    [Table("RPA_ATM_GL")]
    public class AtmGL
    {
        public Guid Id { get; set; }

        [Column("ATM_TerminalID")]
        public string TerminalId { get; set; }

        [Column("ATM_GL")]
        public string GL { get; set; }
    }
}