using System.Data;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practica02.Datos;
using Practica02.Model.Dto;

namespace Practica02.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemsController : Controller
{

    private readonly ILogger<ItemsController> _logger;
    private readonly ApplicationDbContext _db;
    
    public ItemsController(ILogger<ItemsController>  logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    [HttpGet]
    public ActionResult<IEnumerable<MProductDto>>  getAllItem()
    {
        _logger.LogInformation("Obtener productos");

        var query = "SELECT * FROM items";
        
        var result = _db.Product.FromSqlRaw(query).ToList();
        
        return Ok(result);
    }

    
    [HttpGet("id:int", Name = "GetProduct")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<MProductDto>  getItem(int id)
    {
        if (id == 0)
        {
            _logger.LogError("Error de id: " + id);
            return BadRequest();
        }

        var query = "SELECT * FROM items WHERE id = @id";

        var result = _db.Product.FromSqlRaw(query, new Npgsql.NpgsqlParameter("@id", id));

        if (!result.Any())
        {
            return NotFound();
        }
        return Ok(result);
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
        if (product == null)
        {
            return BadRequest();
        }
        if (product.Id > 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    
        var query = "INSERT INTO items (nserie, name, isaviable, date, cantidad) VALUES (@nserie, @name, @isaviable, @date, @cantidad) RETURNING id";
        
        var idParameter = new Npgsql.NpgsqlParameter("@id", NpgsqlTypes.NpgsqlDbType.Integer);
        idParameter.Direction = ParameterDirection.Output;

        _db.Database.ExecuteSqlRaw(query, new Npgsql.NpgsqlParameter("@nserie", product.nserie),
            new Npgsql.NpgsqlParameter("@name", product.name),
            new Npgsql.NpgsqlParameter("@isaviable", product.isaviable),
            new Npgsql.NpgsqlParameter("@date", product.date),
            new Npgsql.NpgsqlParameter("@cantidad", product.cantidad),
            idParameter);

        int productId = (int)idParameter.Value;

        // Insertar datos en la tabla pivote
        var pivotQuery = "INSERT INTO items_details (id_item, id_user, date) VALUES (@id_item, 1, DATE (NOW()))";
        _db.Database.ExecuteSqlRaw(pivotQuery, new Npgsql.NpgsqlParameter("@id_item", productId));

        return Ok($"Producto registrado con ID: {productId}");
    }

    
    
    [HttpDelete("id: int")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult deleteProduct(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }

        var product = "DELETE FROM items WHERE id = @id";
        _db.Database.ExecuteSqlRaw(product, new Npgsql.NpgsqlParameter("@id", id));

        return NoContent(); 
    }
    
    //UPDATE
    [HttpPut("id: int")]
    public  IActionResult updateProduct([FromBody] MProductDto productU, int id)
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
        
        var query = "UPDATE items SET nserie = @nserie, name = @name, isaviable = @isaviable, date = @date, cantidad = @cantidad WHERE id = @id";
         _db.Database.ExecuteSqlRaw(query, new Npgsql.NpgsqlParameter("@nserie", productU.nserie),
            new Npgsql.NpgsqlParameter("@name", productU.name),
            new Npgsql.NpgsqlParameter("@isaviable", productU.isaviable),
            new Npgsql.NpgsqlParameter("@date", productU.date),
            new Npgsql.NpgsqlParameter("@cantidad", productU.cantidad),
            new Npgsql.NpgsqlParameter("@id", productU.Id));

        
        return NoContent();
    }

    [HttpPatch("id:int")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult patchProduct(int id, JsonPatchDocument<MProductDto> productPatch)
    {
        if (productPatch == null || id == 0)
        {
            return BadRequest("Campos Nulos o id invalido");
        }

        var oldProduct = ProductStore.ProductList.FirstOrDefault(v => v.Id == id);
        
        productPatch.ApplyTo(oldProduct, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return NoContent();
    }

}