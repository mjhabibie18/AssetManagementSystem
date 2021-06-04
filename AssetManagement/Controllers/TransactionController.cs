using AssetManagement.Base;
using AssetManagement.Models;
using AssetManagement.Repositories.Data;
using AssetManagement.Repositories.Interface;
using AssetManagement.ViewModels;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
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
        private readonly IGenericDapper dapper;
        public TransactionController(TransactionRepository transactionRepository, IGenericDapper dapper) : base(transactionRepository)
        {
            this.transactionRepository = transactionRepository;
            this.dapper = dapper;
        }

        [HttpPost("request")]
        public IActionResult RequestItem(RequestAsset assets)
        {
                var dbparams = new DynamicParameters();
                dbparams.Add("RequestDate", assets.Request, DbType.DateTime);
                dbparams.Add("ReturnDate", assets.Return, DbType.DateTime);
                dbparams.Add("Status", assets.Status, DbType.String);
                dbparams.Add("EmployeeId", assets.EmployeeId, DbType.Int32);

                int transId = dapper.Get<int>("[dbo].[SP_GetTransactionId]", dbparams, CommandType.StoredProcedure);

                string query = string.Empty;

                for (int i = 0; i < assets.items.Count; i++)
                {
                    var paramTransItem = new DynamicParameters();
                    paramTransItem.Add("ItemId", assets.items[i].ItemId, DbType.Int32);
                    paramTransItem.Add("TransId", transId, DbType.Int32);
                    //paramTransItem.Add("Qty", assets.items[i].Qty, DbType.Int32);
                    //query += "INSERT INTO TB_T_TransactionItem (ItemId, TransactionsId, Quantity) VALUES (" + assets.items[i].ItemId + ", " + transId + ", " + assets.items[i].Qty + ")";
                    var res = Task.FromResult(dapper.Insert<int>("[dbo].[SP_InsertTransItem]", paramTransItem, CommandType.StoredProcedure));
                }
                return Ok(new { Message = "Request successful." });
        }
    }
}
