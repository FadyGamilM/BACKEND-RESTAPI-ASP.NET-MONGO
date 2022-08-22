using catalog.Models;
namespace catalog.Interfaces
{
   public interface IItemRepo
   {
      Task<IEnumerable<Item>> GetItems();

      Task<Item> GetItemById(Guid ItemId);

      Task CreateItem(Item item);
   }
}