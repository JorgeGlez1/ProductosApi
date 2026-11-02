using Microsoft.AspNetCore.Mvc;
using ProductosApi.Data;

namespace ProductosApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConexionController : ControllerBase
{
    private readonly Conexion _conexion;

    public ConexionController(Conexion conexion)
    {
        _conexion = conexion;
    }

    /// <summary>Prueba la conexión con MySQL.</summary>
    [HttpGet("probar")]
    public IActionResult Probar()
    {
        var ok = _conexion.Probar(out var mensaje);
        return ok
            ? Ok(new { conectado = true, mensaje })
            : StatusCode(StatusCodes.Status503ServiceUnavailable, new { conectado = false, mensaje });
    }
}
