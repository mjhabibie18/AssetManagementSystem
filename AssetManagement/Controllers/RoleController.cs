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
    public class RoleController : BaseController<Role, RoleRepository, int>
    {
        private RoleRepository roleRepository;
        private readonly IGenericDapper _dapper;
        public RoleController(RoleRepository roleRepository, IGenericDapper dapper) : base(roleRepository)
        {
            this.roleRepository = roleRepository;
            _dapper = dapper;
        }
    }
}
