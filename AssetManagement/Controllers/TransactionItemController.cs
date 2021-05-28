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

namespace AssetManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionItemController : BaseController<TransactionItem, TransactionItemRepository, int>
    {
        private TransactionItemRepository transactionitemRepository;
        public TransactionItemController(TransactionItemRepository transactionitemRepository) : base(transactionitemRepository)
        {
            this.transactionitemRepository = transactionitemRepository;
        }
    }
}
