using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Models
{
    [Table("TB_T_Procurement")]
    public class Procurement
    {
        [Key]
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public string Price { get; set; }
        public DateTime ProcurementDate { get; set; }
        public string StatusProcurement { get; set; }
        public Category Category { get; set; }
    }
}
