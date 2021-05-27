using AssetManagement.Base;
using AssetManagement.Models;
using AssetManagement.Repositories.Data;
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
    public class CategoryController : BaseController<Category, CategoryRepository, int>
    {
        private CategoryRepository categoryRepository;
        public CategoryController(CategoryRepository categoryRepository) : base(categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
    }
}
