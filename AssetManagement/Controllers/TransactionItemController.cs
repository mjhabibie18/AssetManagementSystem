using AssetManagement.Base;
using AssetManagement.Models;
using AssetManagement.Repositories.Data;
using AssetManagement.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
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

        [HttpGet("GetDataTransaction")]
        public List<dynamic> GetPosition()
        {
            string query = string.Format("SELECT Id, TransactionsId AS 'TransId', ItemName AS 'Item', Status AS 'Status', RequestDate AS 'Request', ReturnDate 'Return' FROM TB_T_TransactionItem INNER JOIN TB_M_Item ON TB_T_TransactionItem.ItemId = TB_M_Item.Id INNER JOIN TB_T_Transaction ON TB_T_TransactionItem.TransactionsId = TB_T_Transaction.Id");

            List<dynamic> get = dapper.GetAllNoParam<dynamic>(query, CommandType.Text);

            return get;
        }
    }
}
