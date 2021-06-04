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
    [Authorize(Roles = "Admin")]
    public class DepartmentController : BaseController<Department, DepartmentRepository, int>
    {
        private DepartmentRepository departmentRepository;
        private readonly IGenericDapper dapper;
        public DepartmentController(DepartmentRepository departmentRepository, IGenericDapper dapper) : base(departmentRepository)
        {
            this.departmentRepository = departmentRepository;
            this.dapper = dapper;
        }
    }
}
