# ProductosApi — CRUD de productos (ASP.NET Core + MySQL + Swagger)

## Requisitos
- .NET 8 SDK
- MySQL 8 (o MariaDB)

## Pasos
1. Ejecuta `Database/script.sql` en MySQL (crea `tienda_db`, la tabla `productos` y 3 registros).
2. Ajusta usuario/contraseña en `appsettings.json` → `ConnectionStrings:MySql`.
3. En la carpeta del proyecto:
   ```bash
   dotnet run
   ```
4. Abre http://localhost:5080/swagger

## Estructura
| Punto       | Dónde                                                   |
|-------------|---------------------------------------------------------|
| Conexión    | `Data/Conexion.cs` · `GET /api/conexion/probar`         |
| Consultas   | `ObtenerTodos` · `GET /api/productos`                   |
| Insert      | `Insertar` · `POST /api/productos`                      |
| Actualizar  | `Actualizar` · `PUT /api/productos/{id}`                |
| Eliminar    | `Eliminar` · `DELETE /api/productos/{id}`               |
| Buscar      | `ObtenerPorId` · `GET /api/productos/{id}`<br>`Buscar` · `GET /api/productos/buscar?texto=...` |

La interfaz está en `Repositories/IProductoRepository.cs` y su implementación (ADO.NET con consultas parametrizadas) en `Repositories/ProductoRepository.cs`.

## Ejemplo de cuerpo para POST / PUT
```json
{
  "nombre": "Teclado mecánico",
  "descripcion": "Switches rojos, RGB",
  "categoria": "Accesorios",
  "precio": 899.90,
  "stock": 25
}
```
