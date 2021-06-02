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
    [Authorize]
    public class ItemController : BaseController<Item, ItemRepository, int>
    {
        private ItemRepository itemRepository;
        private readonly IGenericDapper dapper;
        public ItemController(ItemRepository itemRepository, IGenericDapper dapper) : base(itemRepository)
        {
            this.itemRepository = itemRepository;
            this.dapper = dapper;
        }
    }
}
