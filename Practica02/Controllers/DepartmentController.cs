using Microsoft.AspNetCore.Mvc;
using Practica02.Model;
using Practica02.Repositories;

namespace Practica02.Controllers;


[Route("api/[Controller]")]
[ApiController]
public class DepartmentController: ControllerBase
{
    private IDepartment _departmentRepository;
    
    public DepartmentController(IDepartment departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DepartmentModel>>> GetAllDepartment()
    {
        var result = await _departmentRepository.getAllDepartment();
        return Ok(result);
    }
}