 using catalog.Interfaces;
using catalog.Models;
using MongoDB.Driver;
namespace catalog.Repositories
{
   public class ItemRepo : IItemRepo
   {   
      // The Dependency Injection Pattern  
      private const string databaseName = "catalog";
      private const string collectionName = "items";
      private readonly IMongoCollection<Item> itemsCollection;    
      public ItemRepo(IMongoClient mongoClient)
      {
         var database = mongoClient.GetDatabase(databaseName);
         itemsCollection = database.GetCollection<Item>(collectionName);
      }

      public Task CreateItem(Item item)
      {
         throw new NotImplementedException();
      }

      public Task<Item> GetItemById(int ItemId)
      {
         throw new NotImplementedException();
      }

      public Task<IEnumerable<Item>> GetItems()
      {
         throw new NotImplementedException();
      }
   }
}