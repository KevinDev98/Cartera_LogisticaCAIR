using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LogisticaCAIR.Context;
using LogisticaCAIR.Models;

namespace LogisticaCAIR.Controllers
{
    public class PerfilesController : Controller
    {
        private readonly DB_LOGISTICA_CAIRContext _context;

        public PerfilesController(DB_LOGISTICA_CAIRContext context)
        {
            _context = context;
        }

        // GET: Perfiles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Perfiles.ToListAsync());
        }

        // GET: Perfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perfile = await _context.Perfiles
                .FirstOrDefaultAsync(m => m.IdPerfil == id);
            if (perfile == null)
            {
                return NotFound();
            }

            return View(perfile);
        }

        // GET: Perfiles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Perfiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPerfil,PerfilName,PermisoConfiguracion,PermisoCobroCliente,PermisoCobroProveedor,PermisoFacturas,PermisoCartas,PermisoTarifas")] Perfile perfile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(perfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(perfile);
        }

        // GET: Perfiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perfile = await _context.Perfiles.FindAsync(id);
            if (perfile == null)
            {
                return NotFound();
            }
            return View(perfile);
        }

        // POST: Perfiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPerfil,PerfilName,PermisoConfiguracion,PermisoCobroCliente,PermisoCobroProveedor,PermisoFacturas,PermisoCartas,PermisoTarifas")] Perfile perfile)
        {
            if (id != perfile.IdPerfil)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(perfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerfileExists(perfile.IdPerfil))
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
            return View(perfile);
        }

        // GET: Perfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perfile = await _context.Perfiles
                .FirstOrDefaultAsync(m => m.IdPerfil == id);
            if (perfile == null)
            {
                return NotFound();
            }

            return View(perfile);
        }

        // POST: Perfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var perfile = await _context.Perfiles.FindAsync(id);
            _context.Perfiles.Remove(perfile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PerfileExists(int id)
        {
            return _context.Perfiles.Any(e => e.IdPerfil == id);
        }
    }
}
