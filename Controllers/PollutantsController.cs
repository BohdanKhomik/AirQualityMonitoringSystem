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
    public class PollutantsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PollutantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pollutants
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pollutant.ToListAsync());
        }

        // GET: Pollutants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pollutant = await _context.Pollutant
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pollutant == null)
            {
                return NotFound();
            }

            return View(pollutant);
        }

        // GET: Pollutants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pollutants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Measure")] Pollutant pollutant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pollutant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pollutant);
        }

        // GET: Pollutants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pollutant = await _context.Pollutant.FindAsync(id);
            if (pollutant == null)
            {
                return NotFound();
            }
            return View(pollutant);
        }

        // POST: Pollutants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Measure")] Pollutant pollutant)
        {
            if (id != pollutant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pollutant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PollutantExists(pollutant.Id))
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
            return View(pollutant);
        }

        // GET: Pollutants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pollutant = await _context.Pollutant
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pollutant == null)
            {
                return NotFound();
            }

            return View(pollutant);
        }

        // POST: Pollutants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pollutant = await _context.Pollutant.FindAsync(id);
            if (pollutant != null)
            {
                _context.Pollutant.Remove(pollutant);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PollutantExists(int id)
        {
            return _context.Pollutant.Any(e => e.Id == id);
        }
    }
}
