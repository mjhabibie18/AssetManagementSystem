using AssetManagement.Context;
using AssetManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Repositories.Data
{
    public class ProcurementRepository : GeneralRepository<Procurement, MyContext, int>
    {
        private readonly MyContext myContext;
        public ProcurementRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
