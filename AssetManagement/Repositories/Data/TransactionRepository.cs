using AssetManagement.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace AssetManagement.Repositories.Data
{
    public class TransactionRepository : GeneralRepository<Transaction, MyContext, int>
    {
        private readonly MyContext myContext;
        public TransactionRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
