using AssetManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.ViewModels
{
    public class AddCondItem
    {
        public int ItemId { get; set; }
        public List<MoreCondition> moreConditions { get; set; }
    }

    public class MoreCondition
    {
        public int ConditionId { get; set; }
    }
}
