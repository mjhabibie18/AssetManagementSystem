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
    public class ProcurementController : BaseController<Procurement, ProcurementRepository, int>
    {
        private ProcurementRepository procurementRepository;
        public ProcurementController(ProcurementRepository procurementRepository) : base(procurementRepository)
        {
            this.procurementRepository = procurementRepository;
        }
    }
}
