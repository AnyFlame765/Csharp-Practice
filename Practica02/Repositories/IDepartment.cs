using Practica02.Model;

namespace Practica02.Repositories;

public interface IDepartment
{
    Task<IEnumerable<DepartmentModel>> getAllDepartment();
}   