using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using catalog.Interfaces;
using catalog.Repositories;
using catalog.Configurations;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/* ------------------------ Register My Own Services ------------------------ */
builder.Services.AddScoped<IMongoClient>(
    ServiceProvider => {
        var settings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbConfig>();
        return new MongoClient(settings.ConnectionString);
    }
);
BsonSerializer.RegisterSerializer(
    new GuidSerializer(
        BsonType.String
    )
);
BsonSerializer.RegisterSerializer(
    new DateTimeOffsetSerializer(
        BsonType.String
    )
);
builder.Services.AddScoped<IItemRepo, ItemRepo>();
/* -------------------------------------------------------------------------- */

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
