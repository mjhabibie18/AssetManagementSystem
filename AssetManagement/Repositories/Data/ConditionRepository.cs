using AssetManagement.Context;
using AssetManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Repositories.Data
{
    public class ConditionRepository : GeneralRepository<Condition, MyContext, int>
    {
        private readonly MyContext myContext;
        public ConditionRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
