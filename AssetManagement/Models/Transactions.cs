using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Models
{
    [Table("TB_T_Transaction")]
    public class Transactions
    {
        [Key]
        public int Id { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Status { get; set; }
        public Employee Employee { get; set; }
        public ICollection<TransactionItem> TransactionItems { get; set; }
    }
}
