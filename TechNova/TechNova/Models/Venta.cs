using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechNova.Models
{
    public partial class Venta
    {
        public int VentaId { get; set; }

        // 👇 Ahora nullable
        [Required(ErrorMessage = "El cliente es obligatorio")]
        public int? ClienteId { get; set; }

        [Required(ErrorMessage = "La fecha de la venta es obligatoria")]
        public DateTime FechaVenta { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El total de la venta no puede ser negativo")]
        public decimal TotalVenta { get; set; }

        // 👇 La navegación puede ser null
        public virtual Cliente? Cliente { get; set; }

        // Relación maestro-detalle
        public virtual ICollection<DetalleVentum> Detalles { get; set; } = new List<DetalleVentum>();
    }
}
