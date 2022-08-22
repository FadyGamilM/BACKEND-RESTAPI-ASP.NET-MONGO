using AutoMapper;
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
var mongoDbSettings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbConfig>();
/* ------------------------ Register My Own Services ------------------------ */
// >> dotnet user-secrets init
// >> dotnet user-secrets set MongoDbSettings:Password ***********
builder.Services.AddScoped<IMongoClient>(
    ServiceProvider => {
        return new MongoClient(mongoDbSettings.ConnectionString);
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
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddHealthChecks()
                       .AddMongoDb(mongoDbSettings.ConnectionString, name: "mongodb", timeout: TimeSpan.FromSeconds(3));
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
app.MapHealthChecks("/health-check");

app.Run();
