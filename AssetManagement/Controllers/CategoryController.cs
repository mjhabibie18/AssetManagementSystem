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
    [Authorize(Roles = "Admin, Manager")]
    public class CategoryController : BaseController<Category, CategoryRepository, int>
    {
        private CategoryRepository categoryRepository;
        private readonly IGenericDapper dapper;
        public CategoryController(CategoryRepository categoryRepository, IGenericDapper dapper) : base(categoryRepository)
        {
            this.categoryRepository = categoryRepository;
            this.dapper = dapper;
        }
    }
}
