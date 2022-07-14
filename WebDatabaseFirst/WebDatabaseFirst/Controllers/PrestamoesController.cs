using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebDatabaseFirst.Models;

namespace WebDatabaseFirst.Controllers
{
    public class PrestamoesController : Controller
    {
        private readonly BibliotecaContext _context;

        public PrestamoesController(BibliotecaContext context)
        {
            _context = context;
        }

        // GET: Prestamoes
        public async Task<IActionResult> Index()
        {
            var bibliotecaContext = _context.Prestamos.Include(p => p.Prestamo1).Include(p => p.PrestamoNavigation);
            return View(await bibliotecaContext.ToListAsync());
        }

        // GET: Prestamoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos
                .Include(p => p.Prestamo1)
                .Include(p => p.PrestamoNavigation)
                .FirstOrDefaultAsync(m => m.PrestamoId == id);
            if (prestamo == null)
            {
                return NotFound();
            }

            return View(prestamo);
        }

        // GET: Prestamoes/Create
        public IActionResult Create()
        {
            ViewData["PrestamoId"] = new SelectList(_context.PrestamoLibros, "PrestamoLibroId", "PrestamoLibroId");
            ViewData["PrestamoId"] = new SelectList(_context.Clientes, "ClienteId", "CedulaCliente");
            return View();
        }

        // POST: Prestamoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrestamoId,Fecha,ClienteId,PrestamoLibroId")] Prestamo prestamo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prestamo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PrestamoId"] = new SelectList(_context.PrestamoLibros, "PrestamoLibroId", "PrestamoLibroId", prestamo.PrestamoId);
            ViewData["PrestamoId"] = new SelectList(_context.Clientes, "ClienteId", "CedulaCliente", prestamo.PrestamoId);
            return View(prestamo);
        }

        // GET: Prestamoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos.FindAsync(id);
            if (prestamo == null)
            {
                return NotFound();
            }
            ViewData["PrestamoId"] = new SelectList(_context.PrestamoLibros, "PrestamoLibroId", "PrestamoLibroId", prestamo.PrestamoId);
            ViewData["PrestamoId"] = new SelectList(_context.Clientes, "ClienteId", "CedulaCliente", prestamo.PrestamoId);
            return View(prestamo);
        }

        // POST: Prestamoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PrestamoId,Fecha,ClienteId,PrestamoLibroId")] Prestamo prestamo)
        {
            if (id != prestamo.PrestamoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prestamo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrestamoExists(prestamo.PrestamoId))
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
            ViewData["PrestamoId"] = new SelectList(_context.PrestamoLibros, "PrestamoLibroId", "PrestamoLibroId", prestamo.PrestamoId);
            ViewData["PrestamoId"] = new SelectList(_context.Clientes, "ClienteId", "CedulaCliente", prestamo.PrestamoId);
            return View(prestamo);
        }

        // GET: Prestamoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos
                .Include(p => p.Prestamo1)
                .Include(p => p.PrestamoNavigation)
                .FirstOrDefaultAsync(m => m.PrestamoId == id);
            if (prestamo == null)
            {
                return NotFound();
            }

            return View(prestamo);
        }

        // POST: Prestamoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prestamo = await _context.Prestamos.FindAsync(id);
            _context.Prestamos.Remove(prestamo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrestamoExists(int id)
        {
            return _context.Prestamos.Any(e => e.PrestamoId == id);
        }
    }
}
