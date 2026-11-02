using ProductosApi.Models;

namespace ProductosApi.Repositories;

public interface IProductoRepository
{
    List<Producto> ObtenerTodos();
    Producto? ObtenerPorId(int id);
    int Insertar(Producto producto);
    bool Actualizar(Producto producto);
    bool Eliminar(int id);

    // Método adicional para el punto "Buscar" por texto
    List<Producto> Buscar(string texto);
}
