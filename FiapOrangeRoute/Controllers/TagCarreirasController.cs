using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FiapOrangeRoute.Data;

namespace FiapOrangeRoute.Controllers
{
    public class TagCarreirasController : Controller
    {
        private readonly AppDbContext _context;

        public TagCarreirasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: TagCarreiras
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.TagsCarreira.Include(t => t.Tag).Include(t => t.TrilhaCarreira);
            return View(await appDbContext.ToListAsync());
        }

        // GET: TagCarreiras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tagCarreira = await _context.TagsCarreira
                .Include(t => t.Tag)
                .Include(t => t.TrilhaCarreira)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tagCarreira == null)
            {
                return NotFound();
            }

            return View(tagCarreira);
        }

        // GET: TagCarreiras/Create
        public IActionResult Create()
        {
            ViewData["IdTag"] = new SelectList(_context.Set<Tag>(), "Id", "Nome");
            ViewData["IdTrilhaCarreira"] = new SelectList(_context.TrilhasCarreira, "Id", "Titulo");
            return View();
        }

        // POST: TagCarreiras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdTrilhaCarreira,IdTag")] TagCarreira tagCarreira)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tagCarreira);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTag"] = new SelectList(_context.Set<Tag>(), "Id", "Nome", tagCarreira.IdTag);
            ViewData["IdTrilhaCarreira"] = new SelectList(_context.TrilhasCarreira, "Id", "Titulo", tagCarreira.IdTrilhaCarreira);
            return View(tagCarreira);
        }

        // GET: TagCarreiras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tagCarreira = await _context.TagsCarreira.FindAsync(id);
            if (tagCarreira == null)
            {
                return NotFound();
            }
            ViewData["IdTag"] = new SelectList(_context.Set<Tag>(), "Id", "Nome", tagCarreira.IdTag);
            ViewData["IdTrilhaCarreira"] = new SelectList(_context.TrilhasCarreira, "Id", "Titulo", tagCarreira.IdTrilhaCarreira);
            return View(tagCarreira);
        }

        // POST: TagCarreiras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdTrilhaCarreira,IdTag")] TagCarreira tagCarreira)
        {
            if (id != tagCarreira.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tagCarreira);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagCarreiraExists(tagCarreira.Id))
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
            ViewData["IdTag"] = new SelectList(_context.Set<Tag>(), "Id", "Nome", tagCarreira.IdTag);
            ViewData["IdTrilhaCarreira"] = new SelectList(_context.TrilhasCarreira, "Id", "Titulo", tagCarreira.IdTrilhaCarreira);
            return View(tagCarreira);
        }

        // GET: TagCarreiras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tagCarreira = await _context.TagsCarreira
                .Include(t => t.Tag)
                .Include(t => t.TrilhaCarreira)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tagCarreira == null)
            {
                return NotFound();
            }

            return View(tagCarreira);
        }

        // POST: TagCarreiras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tagCarreira = await _context.TagsCarreira.FindAsync(id);
            if (tagCarreira != null)
            {
                _context.TagsCarreira.Remove(tagCarreira);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TagCarreiraExists(int id)
        {
            return _context.TagsCarreira.Any(e => e.Id == id);
        }
    }
}
