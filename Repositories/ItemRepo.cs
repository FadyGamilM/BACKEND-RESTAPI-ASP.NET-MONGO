 using catalog.Interfaces;
using catalog.Models;
using MongoDB.Bson;
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

      public async Task CreateItem(Item item)
      {
         await this.itemsCollection.InsertOneAsync(item);
      }

      public async Task<Item> GetItemById(Guid ItemId)
      {
         var item = await this.itemsCollection.Find<Item>(item => item.Id == ItemId).FirstOrDefaultAsync();
         return item;
      }


      public async Task<IEnumerable<Item>> GetItems()
      {
         var items = await this.itemsCollection.Find(item => true).ToListAsync();
         return items;
      }
   }
}