using catalog.Models;
namespace catalog.Interfaces
{
   public interface IItemRepo
   {
      Task<IEnumerable<Item>> GetItems();

      Task<Item> GetItemById(int ItemId);

      Task<bool> CreateItem(Item item);
   }
}