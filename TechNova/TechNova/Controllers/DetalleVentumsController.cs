using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechNova.Models;

namespace TechNova.Controllers
{
    public class DetalleVentumsController : Controller
    {
        private readonly TiendaTechNovaDbContext _context;

        public DetalleVentumsController(TiendaTechNovaDbContext context)
        {
            _context = context;
        }

        // GET: DetalleVentums
        public async Task<IActionResult> Index()
        {
            var tiendaTechNovaDbContext = _context.Detalles.Include(d => d.Producto).Include(d => d.Venta);
            return View(await tiendaTechNovaDbContext.ToListAsync());
        }

        // GET: DetalleVentums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleVentum = await _context.Detalles
                .Include(d => d.Producto)
                .Include(d => d.Venta)
                .FirstOrDefaultAsync(m => m.DetalleId == id);
            if (detalleVentum == null)
            {
                return NotFound();
            }

            return View(detalleVentum);
        }

        // GET: DetalleVentums/Create
        public IActionResult Create()
        {
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "ProductoId");
            ViewData["VentaId"] = new SelectList(_context.Ventas, "VentaId", "VentaId");
            return View();
        }

        // POST: DetalleVentums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DetalleId,VentaId,ProductoId,Cantidad,PrecioUnitario,Subtotal")] DetalleVentum detalleVentum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalleVentum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "ProductoId", detalleVentum.ProductoId);
            ViewData["VentaId"] = new SelectList(_context.Ventas, "VentaId", "VentaId", detalleVentum.VentaId);
            return View(detalleVentum);
        }

        // GET: DetalleVentums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleVentum = await _context.Detalles.FindAsync(id);
            if (detalleVentum == null)
            {
                return NotFound();
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "ProductoId", detalleVentum.ProductoId);
            ViewData["VentaId"] = new SelectList(_context.Ventas, "VentaId", "VentaId", detalleVentum.VentaId);
            return View(detalleVentum);
        }

        // POST: DetalleVentums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DetalleId,VentaId,ProductoId,Cantidad,PrecioUnitario,Subtotal")] DetalleVentum detalleVentum)
        {
            if (id != detalleVentum.DetalleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalleVentum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleVentumExists(detalleVentum.DetalleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "ProductoId", detalleVentum.ProductoId);
            ViewData["VentaId"] = new SelectList(_context.Ventas, "VentaId", "VentaId", detalleVentum.VentaId);
            return View(detalleVentum);
        }

        // GET: DetalleVentums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleVentum = await _context.Detalles
                .Include(d => d.Producto)
                .Include(d => d.Venta)
                .FirstOrDefaultAsync(m => m.DetalleId == id);
            if (detalleVentum == null)
            {
                return NotFound();
            }

            return View(detalleVentum);
        }

        // POST: DetalleVentums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detalleVentum = await _context.Detalles.FindAsync(id);
            if (detalleVentum != null)
            {
                _context.Detalles.Remove(detalleVentum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleVentumExists(int id)
        {
            return _context.Detalles.Any(e => e.DetalleId == id);
        }
    }
}
