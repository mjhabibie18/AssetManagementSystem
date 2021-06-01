using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.ViewModels
{
    public class AddProcurement
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        //public int Stock { get; set; }
        public string Price { get; set; }
        public DateTime ProcurementDate { get; set; }
        public string StatusProcurement { get; set; }
        public int CategoryId { get; set; }
    }
}
