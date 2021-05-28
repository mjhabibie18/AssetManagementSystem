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
    public class ConditionController : BaseController<Condition, ConditionRepository, int>
    {
        private ConditionRepository conditionRepository;
        public ConditionController(ConditionRepository conditionRepository) : base(conditionRepository)
        {
            this.conditionRepository = conditionRepository;
        }
    }
}
