using AssetManagement.Base;
using AssetManagement.Models;
using AssetManagement.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace AssetManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionController : BaseController<Transactions, TransactionRepository, int>
    {
        private TransactionRepository transactionRepository;
        public TransactionController(TransactionRepository transactionRepository) : base(transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }
    }
}
