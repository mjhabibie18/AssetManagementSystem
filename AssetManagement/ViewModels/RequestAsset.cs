using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.ViewModels
{
    public class RequestAsset
    {
        public int EmployeeId { get; set; }
        public DateTime Request { get; set; }
        public DateTime Return { get; set; }
        public string Status { get; set; }

        public List<TransItem> items { get; set; }
    }

    public class TransItem
    {
        public int ItemId { get; set; }
        //public int Qty { get; set; }
    }
}
