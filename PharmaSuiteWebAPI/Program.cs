using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PharmaSuiteWebAPI.Data;
using PharmaSuiteWebAPI.Repo;
using PharmaSuiteWebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Register database context
builder.Services.AddDbContext<PharmaSuiteDBContext>


(
    options => options.UseSqlServer
        (
            builder.Configuration.GetConnectionString("dbconn")
        )
    );
builder.Services.AddScoped<ISaleRepo, SaleService>();
builder.Services.AddAutoMapper(typeof(MappingData));

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
