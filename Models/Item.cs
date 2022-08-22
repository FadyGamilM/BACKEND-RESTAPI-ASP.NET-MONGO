namespace catalog.Models
{
   public record Item
   {
      // i used the init so we can't modify the Id property after creation
      public Guid Id {get; init;}
      
      public string Name {get; init;}

      public decimal Price {get; init;}

      public DateTimeOffset CreatedDate {get; init;}
   }
}