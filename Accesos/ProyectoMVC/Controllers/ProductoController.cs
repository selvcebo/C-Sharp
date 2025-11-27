using CRUD_TRABAJO.Data;
using CRUD_TRABAJO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD_TRABAJO.Controllers
{
 public class ProductoController : Controller
 {
  private readonly ApplicationDbContext _context;

  public ProductoController(ApplicationDbContext context)
  {
   _context = context;
  }

  // LISTAR
  public async Task<IActionResult> Index()
  {
   return View(await _context.Productos.ToListAsync());
  }

  // CREAR
  public IActionResult Crear()
  {
   return View();
  }

  [HttpPost]
  public async Task<IActionResult> Crear(Producto producto)
  {
   if (ModelState.IsValid)
   {
    _context.Add(producto);
    await _context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
   }
   return View(producto);
  }

  // EDITAR
  public async Task<IActionResult> Editar(int id)
  {
   var producto = await _context.Productos.FindAsync(id);
   if (producto == null) return NotFound();

   return View(producto);
  }

  [HttpPost]
  public async Task<IActionResult> Editar(Producto producto)
  {
   if (ModelState.IsValid)
   {
    _context.Update(producto);
    await _context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
   }
   return View(producto);
  }

  // ELIMINAR
  public async Task<IActionResult> Eliminar(int id)
  {
   var producto = await _context.Productos.FindAsync(id);
   if (producto == null) return NotFound();

   return View(producto);
  }

  [HttpPost, ActionName("Eliminar")]
  public async Task<IActionResult> EliminarConfirmado(int id)
  {
   var producto = await _context.Productos.FindAsync(id);

   if (producto != null)
    _context.Productos.Remove(producto);

   await _context.SaveChangesAsync();

   return RedirectToAction(nameof(Index));
  }
 }
}
