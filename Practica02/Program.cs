using Microsoft.EntityFrameworkCore;
using Practica02;
using Practica02.Datos;
using Practica02.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//CONFIGURATION DB
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
        );
});

builder.Services.AddScoped<IUser, UserRepository>();
builder.Services.AddScoped<IDepartment, DepartmentRepository>();

//Agregamsos el servicio de AutoMapper
builder.Services.AddAutoMapper(typeof(MappingConfig));

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