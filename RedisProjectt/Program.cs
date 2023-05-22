using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
var redis = ConnectionMultiplexer.Connect("172.22.208.1");
// Add services to the container.
builder.Services.AddScoped(s=> redis.GetDatabase());
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
