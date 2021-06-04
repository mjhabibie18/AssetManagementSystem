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
    public class EmployeeController : BaseController<Employee, EmployeeRepository, int>
    {
        private EmployeeRepository employeeRepository;
        private readonly IGenericDapper dapper;
        public EmployeeController(EmployeeRepository employeeRepository, IGenericDapper dapper) : base(employeeRepository)
        {
            this.employeeRepository = employeeRepository;
            this.dapper = dapper;
        }
    }
}
