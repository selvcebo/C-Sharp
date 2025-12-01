using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechNova.Models
{
    public partial class Producto
    {
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; } = null!;

        [StringLength(250)]
        public string? Descripcion { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El precio unitario debe ser mayor que cero")]
        public decimal PrecioUnitario { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El stock disponible no puede ser negativo")]
        public int StockDisponible { get; set; }

        // Relación: un producto puede estar en muchos detalles
        public virtual ICollection<DetalleVentum> Detalles { get; set; } = new List<DetalleVentum>();
    }
}
