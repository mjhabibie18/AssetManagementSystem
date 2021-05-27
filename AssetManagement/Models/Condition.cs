using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace assetManagement.Models
{
    [Table("TB_T_Condition")]
    public class Condition
    {
        [Key]
        public int Id { get; set; }
        public string ConditionName { get; set; }
        public ICollection<ConditionItem> ConditionItems { get; set; }
    }
}
