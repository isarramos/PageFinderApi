using Mongo.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddMongo(builder.Configuration)
    .AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseAuthorization();

app.MapControllers();

app.Run();
