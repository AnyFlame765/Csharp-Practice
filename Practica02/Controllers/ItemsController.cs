using Microsoft.AspNetCore.Mvc;
using Practica02.Datos;
using Practica02.Model;
using Practica02.Model.Dto;

namespace Practica02.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemsController : Controller
{

    [HttpGet]
    public ActionResult<IEnumerable<MProductDto>>  getAllItem()
    {
        return Ok(ProductStore.ProductList);
    }

    
    [HttpGet("id", Name = "GetProduct")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<MProductDto>  getItem(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }

        var product = ProductStore.ProductList.FirstOrDefault(v => v.Id == id);

        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<MProductDto> registerProduct([FromBody] MProductDto product)
    {
        if (product == null)
        {
            return BadRequest();
        }

        if (product.Id > 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        product.Id = ProductStore.ProductList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
        ProductStore.ProductList.Add(product);

        return CreatedAtRoute("GetProduct", new {id = product.Id}, product);
    }

}