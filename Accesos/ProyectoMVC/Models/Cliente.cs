namespace CRUD_TRABAJO.Models
{
 public class Cliente
 {
  public int ClienteId { get; set; }
  public required string Nombre { get; set; }
  public required string Apellido { get; set; }
  public string? NumeroDocumento { get; set; }
  public string? Telefono { get; set; }
  public string? CorreoElectronico { get; set; }
  public string? Direccion { get; set; }


        public ICollection<Venta> Ventas { get; set; } = new List<Venta>();
 }
}
