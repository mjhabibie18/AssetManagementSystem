using AssetManagement.Base;
using AssetManagement.Context;
using AssetManagement.Models;
using AssetManagement.Repositories.Data;
using AssetManagement.Repositories.Interface;
using AssetManagement.Services;
using AssetManagement.ViewModels;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace AssetManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Employee, Manager, Admin")]
    public class TransactionController : BaseController<Transactions, TransactionRepository, int>
    {
        private TransactionRepository transactionRepository;
        private readonly IGenericDapper dapper;
        private readonly IConfiguration _config;
        private readonly MyContext _context;
        public TransactionController(TransactionRepository transactionRepository, IConfiguration config, MyContext context, IGenericDapper dapper) : base(transactionRepository)
        {
            this.transactionRepository = transactionRepository;
            this.dapper = dapper;
            _config = config;
            _context = context;
        }

        [HttpPost("request")]
        public IActionResult RequestItem(RequestAsset assets)
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            var jwtReader = new JwtSecurityTokenHandler();
            var jwt = jwtReader.ReadJwtToken(token);

            var email = jwt.Claims.First(c => c.Type == "email").Value;
            var isExist = _context.Employees.FirstOrDefault(u => u.Email == email);

            var employee = _context.Employees.Find(isExist.Id);
            var dbparams = new DynamicParameters();
            dbparams.Add("RequestDate", assets.Request, DbType.DateTime);
            dbparams.Add("ReturnDate", assets.Return, DbType.DateTime);
            dbparams.Add("Status", assets.Status, DbType.String);
            dbparams.Add("EmployeeId", isExist.Id, DbType.Int32);

            int transId = dapper.Get<int>("[dbo].[SP_GetTransactionId]", dbparams, CommandType.StoredProcedure);

            //string query = string.Empty;

            for(int i = 0; i < assets.items.Length; i++)
            {
                var paramTransItem = new DynamicParameters();
                paramTransItem.Add("ItemId", assets.items[i].ItemId, DbType.Int32);
                paramTransItem.Add("TransId", transId, DbType.Int32);
                //paramTransItem.Add("Qty", assets.items[i].Qty, DbType.Int32);
                //query += "INSERT INTO TB_T_TransactionItem (ItemId, TransactionsId, Quantity) VALUES (" + assets.items[i].ItemId + ", " + transId + ", " + assets.items[i].Qty + ")";
                var res = Task.FromResult(dapper.Insert<int>("[dbo].[SP_InsertTransItem]", paramTransItem, CommandType.StoredProcedure));
            }

            //EmailManager emailManager = new EmailManager(_config, _context);
            //string subject = "Konfirmasi Request Asset";
            //string body = "Detail request asset Request Date: " + assets.Request + " Return Date: " + assets.Return + " Status: " + assets.Status;
            //emailManager.SendEmail(_config.GetSection("MailSettings").GetSection("Mail").Value,
            //    subject, body, isExist.Email);
            return Ok(new { Message = "Request successful." });
        }

        [HttpGet("GetStatus")]
        public List<dynamic> GetStatus()
        {
            string query = "SELECT Status, COUNT(*) AS Total FROM TB_T_Transaction GROUP BY Status";
            var dbparam = new DynamicParameters();
            List<dynamic> result = dapper.GetAll<dynamic>(query, dbparam, CommandType.Text);

            return result;
        }

        [HttpGet("GetItem")]
        public List<dynamic> GetItem()
        {
            string query = "SELECT  ItemName AS 'Item', COUNT(*) as Total FROM TB_T_TransactionItem INNER JOIN TB_M_Item ON TB_T_TransactionItem.ItemId = TB_M_Item.Id INNER JOIN TB_T_Transaction ON TB_T_TransactionItem.TransactionsId = TB_T_Transaction.Id Group by ItemName";
            var dbparam = new DynamicParameters();
            List<dynamic> result = dapper.GetAll<dynamic>(query, dbparam, CommandType.Text);

            return result;
        }
    }
}
