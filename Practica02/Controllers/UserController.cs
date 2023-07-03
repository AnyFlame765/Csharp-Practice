using Microsoft.AspNetCore.Mvc;
using Practica02.Model;
using Practica02.Repositories;

namespace Practica02.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class UserController: ControllerBase
{

    private IUser _userRepository;

    public UserController(IUser userRepository)
    {
        _userRepository = userRepository;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserModel>>> GetAllUser()
    {
        var result = await _userRepository.GetAllUser();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserModel>> GetUser(int id)
    {
        var result = await _userRepository.GetUser(id);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<ActionResult<bool>> CreateUser(UserModel userModel)
    {
        if (userModel.Id > 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        if (!ModelState.IsValid || userModel == null)
        {
            return BadRequest();
        }
        
        await _userRepository.CreateUser(userModel);
        return Ok("Usuario creado");
    }
    
    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteUser(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }

        var result = await _userRepository.DeleteUser(id);
        return Ok("Usuario eliminado");
    }

}