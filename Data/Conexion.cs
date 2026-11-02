using MySqlConnector;

namespace ProductosApi.Data;

/// <summary>
/// Punto 1: Conexión. Lee la cadena de conexión de appsettings.json
/// y entrega conexiones abiertas a MySQL.
/// </summary>
public class Conexion
{
    private readonly string _cadena;

    public Conexion(IConfiguration configuration)
    {
        _cadena = configuration.GetConnectionString("MySql")
            ?? throw new InvalidOperationException("Falta la cadena de conexión 'MySql' en appsettings.json");
    }

    /// <summary>Crea y abre una nueva conexión. Quien la usa debe liberarla (using).</summary>
    public MySqlConnection Abrir()
    {
        var conexion = new MySqlConnection(_cadena);
        conexion.Open();
        return conexion;
    }

    /// <summary>Verifica que el servidor MySQL responda.</summary>
    public bool Probar(out string mensaje)
    {
        try
        {
            using var conexion = Abrir();
            mensaje = $"Conectado a MySQL {conexion.ServerVersion}, base de datos '{conexion.Database}'";
            return true;
        }
        catch (Exception ex)
        {
            mensaje = ex.Message;
            return false;
        }
    }
}
