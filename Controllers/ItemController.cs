using Microsoft.AspNetCore.Mvc;
using catalog.Interfaces;
using AutoMapper;
using catalog.DTOs;
using catalog.Models;

namespace catalog.Controllers
{
   [ApiController]
   [Route("api/[Controller]")]
   public class ItemController : ControllerBase
   {
      private readonly IItemRepo _itemRepo;
      private readonly IMapper _mapper;
      public ItemController(IItemRepo itemRepo, IMapper mapper)
      {
         this._itemRepo = itemRepo;
         this._mapper = mapper;
      }

      [HttpGet("")]
      public async Task<IActionResult> GetItems()
      {
         var items = await this._itemRepo.GetItems();
         var itemsDto = this._mapper.Map<IEnumerable<ReadItemDto>>(items);
         return Ok(items);
      }

      [HttpGet("{Id}")]
      public async Task<IActionResult> GetItemById([FromRoute] Guid Id)
      {
         var item = await this._itemRepo.GetItemById(Id);
         if (item == null){
            return NotFound();
         }
         return Ok(this._mapper.Map<ReadItemDto>(item));
      }

      [HttpPost("")]
      public async Task<IActionResult> CreateItem([FromBody] CreateItemDto itemDto)
      {
         await this._itemRepo.CreateItem(this._mapper.Map<Item>(itemDto));
         return Ok("created");
      }

      [HttpPut("{Id}")]
      public async Task<IActionResult> UpdateItem ([FromBody] UpdateItemDto itemDto, [FromRoute] Guid Id)
      {
         var existingItem = await this._itemRepo.GetItemById(Id);
         if(existingItem == null){
            return NotFound();
         }
         var updatedItem = existingItem with {
            Name = itemDto.Name,
            Price = itemDto.Price
         };
         await this._itemRepo.UpdateItem(updatedItem, Id);
         return Ok("Updated");
      }

      [HttpDelete("{Id}")]
      public async Task<IActionResult> DeleteItem ([FromRoute] Guid Id)
      {
         var existingItem = await this._itemRepo.GetItemById(Id);
         if (existingItem == null){
            return NotFound();
         }
         await this._itemRepo.DeleteItem(Id);
         return Ok("Deleted");
      }
   }
}