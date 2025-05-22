using System.Threading.Channels;
using IoC;

var builder = WebApplication.CreateBuilder(args);
// Acessar valores diretamente

var mongoConnection =
    builder.Configuration["MongoDb:ConnectionString"] ?? throw new Exception("MongoDb:ConnectionString");
var mongoDatabase = builder.Configuration["MongoDb:Database"] ?? throw new Exception("MongoDb:Database");

builder.Services.Configure(mongoConnection, mongoDatabase);
builder.Services.Repositories();
var productChannel = Channel.CreateUnbounded<Domain.Entities.Order>();
builder.Services.AddSingleton(productChannel);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();