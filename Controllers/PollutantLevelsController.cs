using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AQIViewer.Data;
using AQIViewer.Models;

namespace AQIViewer.Controllers
{
    public class PollutantLevelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PollutantLevelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PollutantLevels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PollutantLevel.Include(p => p.Pollutant).Include(p => p.PollutionLevel);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PollutantLevels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pollutantLevel = await _context.PollutantLevel
                .Include(p => p.Pollutant)
                .Include(p => p.PollutionLevel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pollutantLevel == null)
            {
                return NotFound();
            }

            return View(pollutantLevel);
        }

        // GET: PollutantLevels/Create
        public IActionResult Create()
        {
            ViewData["PollutantId"] = new SelectList(_context.Pollutant, "Id", "Id");
            ViewData["PollutionLevelId"] = new SelectList(_context.PollutionLevel, "Id", "Id");
            return View();
        }

        // POST: PollutantLevels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MinConcentration,MaxConcentration,PollutionLevelId,PollutantId,LevelRecomendation")] PollutantLevel pollutantLevel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pollutantLevel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PollutantId"] = new SelectList(_context.Pollutant, "Id", "Id", pollutantLevel.PollutantId);
            ViewData["PollutionLevelId"] = new SelectList(_context.PollutionLevel, "Id", "Id", pollutantLevel.PollutionLevelId);
            return View(pollutantLevel);
        }

        // GET: PollutantLevels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pollutantLevel = await _context.PollutantLevel.FindAsync(id);
            if (pollutantLevel == null)
            {
                return NotFound();
            }
            ViewData["PollutantId"] = new SelectList(_context.Pollutant, "Id", "Id", pollutantLevel.PollutantId);
            ViewData["PollutionLevelId"] = new SelectList(_context.PollutionLevel, "Id", "Id", pollutantLevel.PollutionLevelId);
            return View(pollutantLevel);
        }

        // POST: PollutantLevels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MinConcentration,MaxConcentration,PollutionLevelId,PollutantId,LevelRecomendation")] PollutantLevel pollutantLevel)
        {
            if (id != pollutantLevel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pollutantLevel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PollutantLevelExists(pollutantLevel.Id))
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
            ViewData["PollutantId"] = new SelectList(_context.Pollutant, "Id", "Id", pollutantLevel.PollutantId);
            ViewData["PollutionLevelId"] = new SelectList(_context.PollutionLevel, "Id", "Id", pollutantLevel.PollutionLevelId);
            return View(pollutantLevel);
        }

        // GET: PollutantLevels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pollutantLevel = await _context.PollutantLevel
                .Include(p => p.Pollutant)
                .Include(p => p.PollutionLevel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pollutantLevel == null)
            {
                return NotFound();
            }

            return View(pollutantLevel);
        }

        // POST: PollutantLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pollutantLevel = await _context.PollutantLevel.FindAsync(id);
            if (pollutantLevel != null)
            {
                _context.PollutantLevel.Remove(pollutantLevel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PollutantLevelExists(int id)
        {
            return _context.PollutantLevel.Any(e => e.Id == id);
        }
    }
}
