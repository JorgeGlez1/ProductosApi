using System.ComponentModel.DataAnnotations;

namespace ProductosApi.Models;

/// <summary>Entidad que representa un producto de la tienda.</summary>
public class Producto
{
    /// <summary>Identificador (lo genera MySQL).</summary>
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(255)]
    public string? Descripcion { get; set; }

    [Required, StringLength(50)]
    public string Categoria { get; set; } = string.Empty;

    [Range(0.01, 9999999.99)]
    public decimal Precio { get; set; }

    [Range(0, int.MaxValue)]
    public int Stock { get; set; }

    /// <summary>Fecha de alta (la asigna MySQL).</summary>
    public DateTime FechaRegistro { get; set; }
}
