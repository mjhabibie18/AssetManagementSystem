using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace assetManagement.Models
{
    [Table("TB_M_Item")]
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public string Price { get; set; }
        public Category Category { get; set; }
        public ICollection<ConditionItem> ConditionItems { get; set; }
        public ICollection<TransactionItem> TransactionItems { get; set; }
    }
}
