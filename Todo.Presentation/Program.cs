using Microsoft.EntityFrameworkCore;
using Serilog;
using Todo.Application.IService;
using Todo.Application.Service;
using Todo.Infrastructure.Context;


var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()    
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day) 
    .CreateLogger();
builder.Host.UseSerilog();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("TodoDb"));
builder.Services.AddScoped<ITodoService, TodoService>();
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
