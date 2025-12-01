using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechNova.Models
{
    public partial class Cliente
    {
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "El nombre completo es obligatorio")]
        [StringLength(100)]
        public string NombreCompleto { get; set; } = null!;

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo electrónico válido")]
        public string CorreoElectronico { get; set; } = null!;

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(200)]
        public string Direccion { get; set; } = null!;

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [Phone(ErrorMessage = "Debe ingresar un número de teléfono válido")]
        public string Telefono { get; set; } = null!;

        // Relación: un cliente puede tener muchas ventas
        public virtual ICollection<Venta> Ventas { get; set; } = new List<Venta>();
    }
}
