using System;

namespace TechNova.Models
{
    public class VentaResumen
    {
        public int VentaId { get; set; }
        public DateTime? FechaVenta { get; set; }
        public decimal TotalVenta { get; set; }    // si prefieres usar Total desde Venta
        public int ItemsCount { get; set; }        // número de líneas (detalles)
        public decimal SubtotalSum { get; set; }   // suma de subtotales de los detalles
    }
}