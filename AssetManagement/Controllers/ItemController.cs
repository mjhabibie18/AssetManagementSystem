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
    public class ItemController : BaseController<Item, ItemRepository, int>
    {
        private ItemRepository itemRepository;
        public ItemController(ItemRepository itemRepository) : base(itemRepository)
        {
            this.itemRepository = itemRepository;
        }
    }
}
