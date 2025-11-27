namespace CRUD_TRABAJO.Models
{
 public class Producto
 {
  public int ProductoId { get; set; }
  public required string Nombre { get; set; }
  public decimal Precio { get; set; }

  public ICollection<Venta> Ventas { get; set; } = new List<Venta>();
 }
}
