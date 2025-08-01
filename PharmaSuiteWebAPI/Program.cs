using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PharmaSuite.Repositories;
using PharmaSuiteWebAPI.Data;
using PharmaSuiteWebAPI.Repo;
using PharmaSuiteWebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingData));
builder.Services.AddScoped<IPurchasesRepo, PurchasesServices>();
// Register database context
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddDbContext<PharmaSuiteDBContext>


(
    options => options.UseSqlServer
        (
            builder.Configuration.GetConnectionString("dbconn")
        )
    );
builder.Services.AddScoped<ISaleRepo, SaleService>();
builder.Services.AddScoped<ISupplierRepo, SupplierService>();

builder.Services.AddAutoMapper(typeof(MappingData));
builder.Services.AddScoped<ICustomerRepo, CustomerService>();
builder.Services.AddScoped<MedicineRepo, MedicineService>();



builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();


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
