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
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        if (ProductStore.ProductList.FirstOrDefault(v => v.name.ToLower() == product.name.ToLower()) != null)
        {
            ModelState.AddModelError("ProductExist", "Producto ya existe"); //validacion personalizada
            return BadRequest(ModelState);
        }
        
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

    [HttpDelete("/id: int")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult deleteProduct(int id)
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

        ProductStore.ProductList.Remove(product);

        return NoContent(); 
    }
    
    //UPDATE
    [HttpPut("id: int")]
    public IActionResult updateProduct([FromBody] MProductDto productU, int id)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("NameRequired", "El Nombre es requerido");
            return BadRequest();
        }

        if (productU==null || productU.Id != id)
        {
            return BadRequest();
        }

        var result  = ProductStore.ProductList.FirstOrDefault(v => v.Id == id);
        result.nserie = productU.nserie;
        result.name = productU.name;
        result.isaviable = productU.isaviable;
        result.cantidad = productU.cantidad;

        return NoContent();
    }

}