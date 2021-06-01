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
    [Authorize(Roles = "Admin")]

    public class ConditionItemController : BaseController<ConditionItem, ConditionItemRepository, int>
    {
        private ConditionItemRepository conditionitemRepository;
        public ConditionItemController(ConditionItemRepository conditionitemRepository) : base(conditionitemRepository)
        {
            this.conditionitemRepository = conditionitemRepository;
        }
    }
}
