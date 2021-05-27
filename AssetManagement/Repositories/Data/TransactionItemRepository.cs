using AssetManagement.Context;
using AssetManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Repositories.Data
{
    public class TransactionItemRepository : GeneralRepository<TransactionItem, MyContext, int>
    {
        private readonly MyContext myContext;
        public TransactionItemRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
