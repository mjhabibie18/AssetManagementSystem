using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace assetManagement.Models
{
    [Table("TB_T_Transaction")]
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public DateTime Request { get; set; }
        public DateTime Return { get; set; }
        public string Status { get; set; }
        public Employee Employee { get; set; }
        public ICollection<TransactionItem> TransactionItems { get; set; }
    }
}
