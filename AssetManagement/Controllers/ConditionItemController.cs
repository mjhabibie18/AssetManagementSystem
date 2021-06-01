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
    [Authorize(Roles = "Admin")]
    public class ConditionItemController : BaseController<ConditionItem, ConditionItemRepository, int>
    {
        private ConditionItemRepository conditionitemRepository;
        private readonly IGenericDapper dapper;
        public ConditionItemController(ConditionItemRepository conditionitemRepository, IGenericDapper dapper) : base(conditionitemRepository)
        {
            this.conditionitemRepository = conditionitemRepository;
            this.dapper = dapper;
        }

        [HttpPost("Insert-ConditionItem")]
        [Authorize(Roles = "Admin")]
        public IActionResult InsertProcurement(AddCondItem condItem)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("ItemId", condItem.ItemId, DbType.Int32);
                for(int i = 0; i < condItem.moreConditions.Count; i++)
                {
                    dbparams.Add("ConditionId", condItem.moreConditions[i].ConditionId, DbType.Int32);
                    var res = Task.FromResult(dapper.Insert<int>("[dbo].[SP_InsertConditionItem]", dbparams, CommandType.StoredProcedure));
                }
                return Ok("Data Success Insert");
            }
            catch (Exception e)
            {
                return BadRequest("Data Not Success Insert" + e.Message);
            }
        }
    }
}
