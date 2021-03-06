using AssetManagement.Context;
using AssetManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Repositories.Data
{
    public class ConditionItemRepository : GeneralRepository<ConditionItem, MyContext, int>
    {
        private readonly MyContext myContext;
        public ConditionItemRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
