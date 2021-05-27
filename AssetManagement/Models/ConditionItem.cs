using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace assetManagement.Models
{
    [Table("TB_M_ConditionItem")]
    public class ConditionItem
    {
        [Key]
        public int Id { get; set; }
        public Item Item { get; set; }
        public Condition Condition { get; set; }

    }
}
