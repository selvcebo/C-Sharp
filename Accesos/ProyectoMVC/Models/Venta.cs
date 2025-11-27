namespace CRUD_TRABAJO.Models
{
 public class Venta
 {
  public int VentaId { get; set; }
  public DateTime Fecha { get; set; }

  public int ClienteId { get; set; }
  public int ProductoId { get; set; }

  public required Cliente Cliente { get; set; }
  public required Producto Producto { get; set; }
 }
}
