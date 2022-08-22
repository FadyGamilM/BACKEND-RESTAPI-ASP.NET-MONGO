using Microsoft.AspNetCore.Mvc;
using catalog.Interfaces;
namespace catalog.Controllers
{
   [ApiController]
   [Route("api/[Controller]")]
   public class ItemController : ControllerBase
   {
      private readonly IItemRepo _itemRepo;
      public ItemController(IItemRepo itemRepo)
      {
         this._itemRepo = itemRepo;
      }

      [HttpGet("")]
      public async Task<IActionResult> GetItems()
      {
         var items = await this._itemRepo.GetItems();
         return Ok(items);
      }

      [HttpGet("{Id:int}")]
      public async Task<IActionResult> GetItemById([FromRoute] int Id)
      {
         var item = await this._itemRepo.GetItemById(Id);
         if (item == null){
            return NotFound();
         }
         return Ok(item);
      }
   }
}