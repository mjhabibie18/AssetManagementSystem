using AssetManagement.Context;
using AssetManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Repositories.Data
{
    public class CategoryRepository : GeneralRepository<Category, MyContext, int>
    {
        private readonly MyContext myContext;
        public CategoryRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
