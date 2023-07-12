using Microsoft.EntityFrameworkCore;
using Practica02.Datos;
using Practica02.Model;

namespace Practica02.Repositories;

public class DepartmentRepository: IDepartment
{
    
    private readonly ApplicationDbContext db;
    
    public DepartmentRepository(ApplicationDbContext db)
    {
        this.db = db;
    }
    
    public async Task<IEnumerable<DepartmentModel>> getAllDepartment()
    {
        var query = "SELECT * FROM department";
        var result = await db.Department.FromSqlRaw(query).ToListAsync();
        return result;
    }
}