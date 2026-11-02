using MySqlConnector;
using ProductosApi.Data;
using ProductosApi.Models;

namespace ProductosApi.Repositories;

/// <summary>
/// Implementación de IProductoRepository con ADO.NET (MySqlConnector).
/// Todas las sentencias usan parámetros para evitar inyección SQL.
/// </summary>
public class ProductoRepository : IProductoRepository
{
    private const string Columnas = "id, nombre, descripcion, categoria, precio, stock, fecha_registro";

    private readonly Conexion _conexion;

    public ProductoRepository(Conexion conexion)
    {
        _conexion = conexion;
    }

    // Punto 2: Consultas
    public List<Producto> ObtenerTodos()
    {
        using var cn = _conexion.Abrir();
        using var cmd = new MySqlCommand($"SELECT {Columnas} FROM productos ORDER BY id", cn);
        return LeerLista(cmd);
    }

    // Punto 6: Buscar (por id)
    public Producto? ObtenerPorId(int id)
    {
        using var cn = _conexion.Abrir();
        using var cmd = new MySqlCommand($"SELECT {Columnas} FROM productos WHERE id = @id", cn);
        cmd.Parameters.AddWithValue("@id", id);

        using var reader = cmd.ExecuteReader();
        return reader.Read() ? Mapear(reader) : null;
    }

    // Punto 6: Buscar (por nombre o categoría)
    public List<Producto> Buscar(string texto)
    {
        using var cn = _conexion.Abrir();
        using var cmd = new MySqlCommand(
            $"SELECT {Columnas} FROM productos WHERE nombre LIKE @texto OR categoria LIKE @texto ORDER BY nombre", cn);
        cmd.Parameters.AddWithValue("@texto", $"%{texto}%");
        return LeerLista(cmd);
    }

    // Punto 3: Insert. Devuelve el id generado.
    public int Insertar(Producto producto)
    {
        using var cn = _conexion.Abrir();
        using var cmd = new MySqlCommand(
            @"INSERT INTO productos (nombre, descripcion, categoria, precio, stock)
              VALUES (@nombre, @descripcion, @categoria, @precio, @stock)", cn);
        AgregarParametros(cmd, producto);
        cmd.ExecuteNonQuery();
        return (int)cmd.LastInsertedId;
    }

    // Punto 4: Actualizar
    public bool Actualizar(Producto producto)
    {
        using var cn = _conexion.Abrir();
        using var cmd = new MySqlCommand(
            @"UPDATE productos
              SET nombre = @nombre, descripcion = @descripcion, categoria = @categoria,
                  precio = @precio, stock = @stock
              WHERE id = @id", cn);
        AgregarParametros(cmd, producto);
        cmd.Parameters.AddWithValue("@id", producto.Id);
        return cmd.ExecuteNonQuery() > 0;
    }

    // Punto 5: Eliminar
    public bool Eliminar(int id)
    {
        using var cn = _conexion.Abrir();
        using var cmd = new MySqlCommand("DELETE FROM productos WHERE id = @id", cn);
        cmd.Parameters.AddWithValue("@id", id);
        return cmd.ExecuteNonQuery() > 0;
    }

    private static void AgregarParametros(MySqlCommand cmd, Producto p)
    {
        cmd.Parameters.AddWithValue("@nombre", p.Nombre);
        cmd.Parameters.AddWithValue("@descripcion", (object?)p.Descripcion ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@categoria", p.Categoria);
        cmd.Parameters.AddWithValue("@precio", p.Precio);
        cmd.Parameters.AddWithValue("@stock", p.Stock);
    }

    private static List<Producto> LeerLista(MySqlCommand cmd)
    {
        var lista = new List<Producto>();
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            lista.Add(Mapear(reader));
        return lista;
    }

    private static Producto Mapear(MySqlDataReader r) => new()
    {
        Id = r.GetInt32("id"),
        Nombre = r.GetString("nombre"),
        Descripcion = r.IsDBNull(r.GetOrdinal("descripcion")) ? null : r.GetString("descripcion"),
        Categoria = r.GetString("categoria"),
        Precio = r.GetDecimal("precio"),
        Stock = r.GetInt32("stock"),
        FechaRegistro = r.GetDateTime("fecha_registro")
    };
}
