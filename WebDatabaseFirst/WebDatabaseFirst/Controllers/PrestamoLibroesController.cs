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
    public class PrestamoLibroesController : Controller
    {
        private readonly BibliotecaContext _context;

        public PrestamoLibroesController(BibliotecaContext context)
        {
            _context = context;
        }

        // GET: PrestamoLibroes
        public async Task<IActionResult> Index()
        {
            var bibliotecaContext = _context.PrestamoLibros.Include(p => p.PrestamoLibroNavigation);
            return View(await bibliotecaContext.ToListAsync());
        }

        // GET: PrestamoLibroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestamoLibro = await _context.PrestamoLibros
                .Include(p => p.PrestamoLibroNavigation)
                .FirstOrDefaultAsync(m => m.PrestamoLibroId == id);
            if (prestamoLibro == null)
            {
                return NotFound();
            }

            return View(prestamoLibro);
        }

        // GET: PrestamoLibroes/Create
        public IActionResult Create()
        {
            ViewData["PrestamoLibroId"] = new SelectList(_context.Libros, "LibroId", "AutorLibro");
            return View();
        }

        // POST: PrestamoLibroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrestamoLibroId,LibroId,PrestamoId")] PrestamoLibro prestamoLibro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prestamoLibro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PrestamoLibroId"] = new SelectList(_context.Libros, "LibroId", "AutorLibro", prestamoLibro.PrestamoLibroId);
            return View(prestamoLibro);
        }

        // GET: PrestamoLibroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestamoLibro = await _context.PrestamoLibros.FindAsync(id);
            if (prestamoLibro == null)
            {
                return NotFound();
            }
            ViewData["PrestamoLibroId"] = new SelectList(_context.Libros, "LibroId", "AutorLibro", prestamoLibro.PrestamoLibroId);
            return View(prestamoLibro);
        }

        // POST: PrestamoLibroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PrestamoLibroId,LibroId,PrestamoId")] PrestamoLibro prestamoLibro)
        {
            if (id != prestamoLibro.PrestamoLibroId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prestamoLibro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrestamoLibroExists(prestamoLibro.PrestamoLibroId))
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
            ViewData["PrestamoLibroId"] = new SelectList(_context.Libros, "LibroId", "AutorLibro", prestamoLibro.PrestamoLibroId);
            return View(prestamoLibro);
        }

        // GET: PrestamoLibroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestamoLibro = await _context.PrestamoLibros
                .Include(p => p.PrestamoLibroNavigation)
                .FirstOrDefaultAsync(m => m.PrestamoLibroId == id);
            if (prestamoLibro == null)
            {
                return NotFound();
            }

            return View(prestamoLibro);
        }

        // POST: PrestamoLibroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prestamoLibro = await _context.PrestamoLibros.FindAsync(id);
            _context.PrestamoLibros.Remove(prestamoLibro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrestamoLibroExists(int id)
        {
            return _context.PrestamoLibros.Any(e => e.PrestamoLibroId == id);
        }
    }
}
