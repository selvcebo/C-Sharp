using CRUD_TRABAJO.Data;
using CRUD_TRABAJO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRUD_TRABAJO.Controllers
{
 public class VentaController : Controller
 {
  private readonly ApplicationDbContext _context;

  public VentaController(ApplicationDbContext context)
  {
   _context = context;
  }

  // LISTAR
  public async Task<IActionResult> Index()
  {
   var ventas = _context.Ventas
       .Include(v => v.Cliente)
       .Include(v => v.Producto);

   return View(await ventas.ToListAsync());
  }

  // CREAR
  public IActionResult Crear()
  {
   ViewBag.Clientes = new SelectList(_context.Clientes, "ClienteId", "Nombre");
   ViewBag.Productos = new SelectList(_context.Productos, "ProductoId", "Nombre");

   return View();
  }

  [HttpPost]
  public async Task<IActionResult> Crear(Venta venta)
  {
   if (ModelState.IsValid)
   {
    _context.Ventas.Add(venta);
    await _context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
   }

   ViewBag.Clientes = new SelectList(_context.Clientes, "ClienteId", "Nombre", venta.ClienteId);
   ViewBag.Productos = new SelectList(_context.Productos, "ProductoId", "Nombre", venta.ProductoId);
   return View(venta);
  }

  // EDITAR
  public async Task<IActionResult> Editar(int id)
  {
   var venta = await _context.Ventas.FindAsync(id);
   if (venta == null) return NotFound();

   ViewBag.Clientes = new SelectList(_context.Clientes, "ClienteId", "Nombre", venta.ClienteId);
   ViewBag.Productos = new SelectList(_context.Productos, "ProductoId", "Nombre", venta.ProductoId);

   return View(venta);
  }

  [HttpPost]
  public async Task<IActionResult> Editar(Venta venta)
  {
   if (ModelState.IsValid)
   {
    _context.Update(venta);
    await _context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
   }

   ViewBag.Clientes = new SelectList(_context.Clientes, "ClienteId", "Nombre", venta.ClienteId);
   ViewBag.Productos = new SelectList(_context.Productos, "ProductoId", "Nombre", venta.ProductoId);

   return View(venta);
  }

  // ELIMINAR
  public async Task<IActionResult> Eliminar(int id)
  {
   var venta = await _context.Ventas
       .Include(v => v.Cliente)
       .Include(v => v.Producto)
       .FirstOrDefaultAsync(v => v.VentaId == id);

   if (venta == null) return NotFound();

   return View(venta);
  }

  [HttpPost, ActionName("Eliminar")]
  public async Task<IActionResult> EliminarConfirmado(int id)
  {
   var venta = await _context.Ventas.FindAsync(id);

   if (venta != null)
    _context.Ventas.Remove(venta);

   await _context.SaveChangesAsync();

   return RedirectToAction(nameof(Index));
  }
 }
}
