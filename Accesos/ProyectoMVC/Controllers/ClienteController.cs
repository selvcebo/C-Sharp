using CRUD_TRABAJO.Data;
using CRUD_TRABAJO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD_TRABAJO.Controllers
{
 public class ClienteController : Controller
 {
  private readonly ApplicationDbContext _context;

  public ClienteController(ApplicationDbContext context)
  {
   _context = context;
  }

  // LISTAR
  public async Task<IActionResult> Index()
  {
   return View(await _context.Clientes.ToListAsync());
  }

  // CREAR
  public IActionResult Crear()
  {
   return View();
  }

  [HttpPost]
  public async Task<IActionResult> Crear(Cliente cliente)
  {
   if (ModelState.IsValid)
   {
    _context.Add(cliente);
    await _context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
   }
   return View(cliente);
  }

  // EDITAR
  public async Task<IActionResult> Editar(int id)
  {
   var cliente = await _context.Clientes.FindAsync(id);
   if (cliente == null) return NotFound();

   return View(cliente);
  }

  [HttpPost]
  public async Task<IActionResult> Editar(Cliente cliente)
  {
   if (ModelState.IsValid)
   {
    _context.Update(cliente);
    await _context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
   }
   return View(cliente);
  }

  // ELIMINAR
  public async Task<IActionResult> Eliminar(int id)
  {
   var cliente = await _context.Clientes.FindAsync(id);
   if (cliente == null) return NotFound();

   return View(cliente);
  }

  [HttpPost, ActionName("Eliminar")]
  public async Task<IActionResult> EliminarConfirmado(int id)
  {
   var cliente = await _context.Clientes.FindAsync(id);

   if (cliente != null)
    _context.Clientes.Remove(cliente);

   await _context.SaveChangesAsync();

   return RedirectToAction(nameof(Index));
  }
 }
}
