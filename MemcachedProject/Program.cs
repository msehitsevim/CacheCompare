using Enyim.Caching;
using MemcachedProject.Models;
using MemcachedProject.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CalismaVeriTabaniContext>();


builder.Services.AddLogging();

builder.Services.AddEnyimMemcached(options =>
{
    options.AddServer("localhost", 11211);
});
builder.Services.AddScoped<IErrorRepository, ErrorRepository>();
builder.Services.AddScoped<IMemcachedClient, MemcachedClient>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
