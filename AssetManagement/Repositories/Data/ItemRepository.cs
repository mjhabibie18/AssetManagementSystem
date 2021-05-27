using AssetManagement.Context;
using assetManaAssetManagementgement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Repositories.Data
{
    public class ItemRepository : GeneralRepository<Item, MyContext, int>
    {
        private readonly MyContext myContext;
        public ItemRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
