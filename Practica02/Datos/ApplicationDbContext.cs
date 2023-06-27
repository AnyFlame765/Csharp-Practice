using Microsoft.EntityFrameworkCore;
using Practica02.Model;

namespace Practica02.Datos;

public class ApplicationDbContext : DbContext
{ 
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
         
    }
    
    public DbSet<MProduct> Product { get; set; }
    
    //Consola de administrador de paquete
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasPostgresExtension("uuid-ossp");
    }
}