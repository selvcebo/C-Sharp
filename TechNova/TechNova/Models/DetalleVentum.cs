using System.ComponentModel.DataAnnotations;

namespace TechNova.Models
{
    public partial class DetalleVentum
    {
        public int DetalleId { get; set; }

        [Required(ErrorMessage = "La venta es obligatoria")]
        public int VentaId { get; set; }

        [Required(ErrorMessage = "El producto es obligatorio")]
        public int ProductoId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que cero")]
        public int Cantidad { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El precio unitario debe ser positivo")]
        public decimal PrecioUnitario { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El subtotal debe ser positivo")]
        public decimal Subtotal { get; set; }

        public virtual Producto Producto { get; set; } = null!;
        public virtual Venta Venta { get; set; } = null!;
    }
}
