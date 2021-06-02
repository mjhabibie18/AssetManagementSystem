using AssetManagement.Base;
using AssetManagement.Models;
using AssetManagement.Repositories.Data;
using AssetManagement.Repositories.Interface;
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
        private readonly IGenericDapper dapper;
        public TransactionItemController(TransactionItemRepository transactionitemRepository, IGenericDapper dapper) : base(transactionitemRepository)
        {
            this.transactionitemRepository = transactionitemRepository;
            this.dapper = dapper;
        }
    }
}
