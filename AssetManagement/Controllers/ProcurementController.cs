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

namespace AssetManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, Manager")]
    public class ProcurementController : BaseController<Procurement, ProcurementRepository, int>
    {
        private ProcurementRepository procurementRepository;
        private readonly IGenericDapper _dapper;
        public ProcurementController(ProcurementRepository procurementRepository, IGenericDapper dapper) : base(procurementRepository)
        {
            this.procurementRepository = procurementRepository;
            _dapper = dapper;
        }

        [HttpPost("Approve-Procurement")]
        [Authorize(Roles = "Manager")]
        public IActionResult ApproveProcurement(int ProcurementId)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("ProcurementId", ProcurementId, DbType.Int32);
                var result = Task.FromResult(_dapper.Insert<int>("[dbo].[SP_ApproveProcurement]", dbparams, CommandType.StoredProcedure));
                return Ok("Procurement Approve");
            }
            catch (Exception e)
            {

                return BadRequest("Procurement Reject" + e.Message);
            }
        }

        [HttpPost("Insert-Procurement")]
        [Authorize(Roles = "Admin, Manager")]
        public IActionResult InsertProcurement(AddProcurement procurement)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("ItemName", procurement.ItemName, DbType.String);
                dbparams.Add("Description", procurement.Description, DbType.String);
                //dbparams.Add("Stock", procurement.Stock, DbType.Int32);
                dbparams.Add("Price", procurement.Price, DbType.String);
                dbparams.Add("ProcurementDate", procurement.ProcurementDate, DbType.DateTime);
                dbparams.Add("StatusProcurement", procurement.StatusProcurement, DbType.String);
                dbparams.Add("CategoryId", procurement.CategoryId, DbType.Int32);
                var result = Task.FromResult(_dapper.Insert<int>("[dbo].[SP_InsertProcurement]", dbparams, CommandType.StoredProcedure));
                return Ok("Data Success Insert");
            }catch (Exception e)
            {
                return BadRequest("Data Not Success Insert" + e.Message);
            }
        }
    }
}
