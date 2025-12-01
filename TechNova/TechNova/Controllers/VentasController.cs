using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechNova.Models;
using System.Linq;
using System.Threading.Tasks;

namespace TechNova.Controllers
{
    public class VentasController : Controller
    {
        private readonly TiendaTechNovaDbContext _context;

        public VentasController(TiendaTechNovaDbContext context)
        {
            _context = context;
        }

        // GET: Ventas
        public async Task<IActionResult> Index()
        {
            var ventas = _context.Ventas
                                 .Include(v => v.Cliente)
                                 .Include(v => v.Detalles);
            return View(await ventas.ToListAsync());
        }

        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var venta = await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Detalles)
                    .ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(m => m.VentaId == id);

            if (venta == null) return NotFound();

            return View(venta);
        }

        // GET: Ventas/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "NombreCompleto");
            return View();
        }

        // POST: Ventas/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Venta venta)
        {
            Console.WriteLine($"[DEBUG] ClienteId recibido: {venta.ClienteId}");
            Console.WriteLine($"[DEBUG] ModelState válido: {ModelState.IsValid}");

            if (ModelState.IsValid)
            {
                venta.FechaVenta = DateTime.Now;
                venta.TotalVenta = 0;

                _context.Add(venta);
                await _context.SaveChangesAsync();

                Console.WriteLine($"[DEBUG] Venta guardada con ID: {venta.VentaId}");

                return RedirectToAction("AddDetalle", new { ventaId = venta.VentaId });
            }

            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine($"[DEBUG] Error de validación: {error.ErrorMessage}");
            }

            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "NombreCompleto", venta.ClienteId);
            return View(venta);
        }




        // GET: Ventas/AddDetalle
        public IActionResult AddDetalle(int ventaId)
        {
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "Nombre");
            return View(new DetalleVentum { VentaId = ventaId });
        }

        // POST: Ventas/AddDetalle
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDetalle([Bind("VentaId,ProductoId,Cantidad")] DetalleVentum detalle)
        {
            var producto = await _context.Productos.FindAsync(detalle.ProductoId);
            if (producto == null) return NotFound();

            detalle.PrecioUnitario = producto.PrecioUnitario;
            detalle.Subtotal = detalle.Cantidad * producto.PrecioUnitario;

            // 👇 Actualizar stock
            if (producto.StockDisponible < detalle.Cantidad)
            {
                ModelState.AddModelError("", "No hay suficiente stock disponible para este producto.");
                ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "Nombre", detalle.ProductoId);
                return View(detalle);
            }

            producto.StockDisponible -= detalle.Cantidad;
            _context.Update(producto);

            _context.Detalles.Add(detalle);
            await _context.SaveChangesAsync();

            // Recalcular total de la venta
            var venta = await _context.Ventas
                                      .Include(v => v.Detalles)
                                      .FirstOrDefaultAsync(v => v.VentaId == detalle.VentaId);

            if (venta != null)
            {
                venta.TotalVenta = venta.Detalles.Sum(d => d.Subtotal);
                _context.Update(venta);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", new { id = detalle.VentaId });
        }


        // GET: Ventas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null) return NotFound();

            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "NombreCompleto", venta.ClienteId);
            return View(venta);
        }

        // POST: Ventas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VentaId,ClienteId,FechaVenta,TotalVenta")] Venta venta)
        {
            if (id != venta.VentaId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Ventas.Any(e => e.VentaId == venta.VentaId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "NombreCompleto", venta.ClienteId);
            return View(venta);
        }

        // GET: Ventas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var venta = await _context.Ventas
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(m => m.VentaId == id);

            if (venta == null) return NotFound();

            return View(venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venta = await _context.Ventas
                                      .Include(v => v.Detalles)
                                      .FirstOrDefaultAsync(v => v.VentaId == id);

            if (venta != null)
            {
                // Eliminar primero los detalles asociados
                _context.Detalles.RemoveRange(venta.Detalles);

                // Luego eliminar la venta
                _context.Ventas.Remove(venta);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
