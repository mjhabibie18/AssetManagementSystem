using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Models
{
    [Table("TB_T_TransactionItem")]
    public class TransactionItem
    {
        [Key]
        public int Id { get; set; }
        //public int Quantity { get; set; }
        public Transactions Transactions { get; set; }
        public Item Item { get; set; }

    }
}
