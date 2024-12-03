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
    public class PollutionLevelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PollutionLevelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PollutionLevels
        public async Task<IActionResult> Index()
        {
            return View(await _context.PollutionLevel.ToListAsync());
        }

        // GET: PollutionLevels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pollutionLevel = await _context.PollutionLevel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pollutionLevel == null)
            {
                return NotFound();
            }

            return View(pollutionLevel);
        }

        // GET: PollutionLevels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PollutionLevels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Color,MinPoint,MaxPoint")] PollutionLevel pollutionLevel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pollutionLevel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pollutionLevel);
        }

        // GET: PollutionLevels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pollutionLevel = await _context.PollutionLevel.FindAsync(id);
            if (pollutionLevel == null)
            {
                return NotFound();
            }
            return View(pollutionLevel);
        }

        // POST: PollutionLevels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Color,MinPoint,MaxPoint")] PollutionLevel pollutionLevel)
        {
            if (id != pollutionLevel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pollutionLevel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PollutionLevelExists(pollutionLevel.Id))
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
            return View(pollutionLevel);
        }

        // GET: PollutionLevels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pollutionLevel = await _context.PollutionLevel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pollutionLevel == null)
            {
                return NotFound();
            }

            return View(pollutionLevel);
        }

        // POST: PollutionLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pollutionLevel = await _context.PollutionLevel.FindAsync(id);
            if (pollutionLevel != null)
            {
                _context.PollutionLevel.Remove(pollutionLevel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PollutionLevelExists(int id)
        {
            return _context.PollutionLevel.Any(e => e.Id == id);
        }
    }
}
