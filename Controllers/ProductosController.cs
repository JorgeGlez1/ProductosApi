using Microsoft.AspNetCore.Mvc;
using ProductosApi.Models;
using ProductosApi.Repositories;

namespace ProductosApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProductosController : ControllerBase
{
    private readonly IProductoRepository _repo;

    public ProductosController(IProductoRepository repo)
    {
        _repo = repo;
    }

    /// <summary>Consulta todos los productos.</summary>
    [HttpGet]
    public ActionResult<List<Producto>> ObtenerTodos()
    {
        return Ok(_repo.ObtenerTodos());
    }

    /// <summary>Busca un producto por su id.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Producto> ObtenerPorId(int id)
    {
        var producto = _repo.ObtenerPorId(id);
        return producto is null ? NotFound(new { mensaje = $"No existe el producto {id}" }) : Ok(producto);
    }

    /// <summary>Busca productos cuyo nombre o categoría contengan el texto.</summary>
    [HttpGet("buscar")]
    public ActionResult<List<Producto>> Buscar([FromQuery] string texto)
    {
        if (string.IsNullOrWhiteSpace(texto))
            return BadRequest(new { mensaje = "Indica un texto a buscar" });
        return Ok(_repo.Buscar(texto.Trim()));
    }

    /// <summary>Inserta un producto nuevo.</summary>
    /// <remarks>Id y FechaRegistro se ignoran; los asigna la base de datos.</remarks>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Producto> Insertar([FromBody] Producto producto)
    {
        var id = _repo.Insertar(producto);
        var creado = _repo.ObtenerPorId(id);
        return CreatedAtAction(nameof(ObtenerPorId), new { id }, creado);
    }

    /// <summary>Actualiza un producto existente.</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Producto> Actualizar(int id, [FromBody] Producto producto)
    {
        producto.Id = id;
        if (!_repo.Actualizar(producto))
            return NotFound(new { mensaje = $"No existe el producto {id}" });
        return Ok(_repo.ObtenerPorId(id));
    }

    /// <summary>Elimina un producto.</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Eliminar(int id)
    {
        return _repo.Eliminar(id) ? NoContent() : NotFound(new { mensaje = $"No existe el producto {id}" });
    }
}
