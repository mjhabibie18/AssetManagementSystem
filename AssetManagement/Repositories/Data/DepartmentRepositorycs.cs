using AssetManagement.Context;
using AssetManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Repositories.Data
{
    public class DepartmentRepository : GeneralRepository<Department, MyContext, int>
    {
        private readonly MyContext myContext;
        public DepartmentRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
