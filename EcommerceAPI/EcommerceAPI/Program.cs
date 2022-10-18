using EcommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Add connection string
var connectionString = builder.Configuration.GetConnectionString("dbEcommerce");
//Add DbContext
builder.Services.AddDbContext<dbEcommerceContext>(options =>
    options.UseSqlServer(connectionString));
var app = builder.Build();
//builder.Services.AddHttpClient("", opts =>
//{
//    opts.BaseAddress = new Uri("https://localhost:7137");
//}
//);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseRouting();
//app.MapRazorPages();
app.MapControllers();

app.Run();
