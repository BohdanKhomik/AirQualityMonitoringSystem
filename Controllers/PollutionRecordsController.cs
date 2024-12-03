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
    public class PollutionRecordsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PollutionRecordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PollutionRecords
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PollutionRecord.Include(p => p.AirQualityRecord).Include(p => p.Pollutant);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PollutionRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pollutionRecord = await _context.PollutionRecord
                .Include(p => p.AirQualityRecord)
                .Include(p => p.Pollutant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pollutionRecord == null)
            {
                return NotFound();
            }

            return View(pollutionRecord);
        }

        // GET: PollutionRecords/Create
        public IActionResult Create()
        {
            ViewData["AQRId"] = new SelectList(_context.AirQualityRecords, "Id", "Id");
            ViewData["PollutantId"] = new SelectList(_context.Pollutant, "Id", "Id");
            return View();
        }

        // POST: PollutionRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Concentration,PollutantId,AQRId")] PollutionRecord pollutionRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pollutionRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AQRId"] = new SelectList(_context.AirQualityRecords, "Id", "Id", pollutionRecord.AQRId);
            ViewData["PollutantId"] = new SelectList(_context.Pollutant, "Id", "Id", pollutionRecord.PollutantId);
            return View(pollutionRecord);
        }

        // GET: PollutionRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pollutionRecord = await _context.PollutionRecord.FindAsync(id);
            if (pollutionRecord == null)
            {
                return NotFound();
            }
            ViewData["AQRId"] = new SelectList(_context.AirQualityRecords, "Id", "Id", pollutionRecord.AQRId);
            ViewData["PollutantId"] = new SelectList(_context.Pollutant, "Id", "Id", pollutionRecord.PollutantId);
            return View(pollutionRecord);
        }

        // POST: PollutionRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Concentration,PollutantId,AQRId")] PollutionRecord pollutionRecord)
        {
            if (id != pollutionRecord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pollutionRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PollutionRecordExists(pollutionRecord.Id))
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
            ViewData["AQRId"] = new SelectList(_context.AirQualityRecords, "Id", "Id", pollutionRecord.AQRId);
            ViewData["PollutantId"] = new SelectList(_context.Pollutant, "Id", "Id", pollutionRecord.PollutantId);
            return View(pollutionRecord);
        }

        // GET: PollutionRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pollutionRecord = await _context.PollutionRecord
                .Include(p => p.AirQualityRecord)
                .Include(p => p.Pollutant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pollutionRecord == null)
            {
                return NotFound();
            }

            return View(pollutionRecord);
        }

        // POST: PollutionRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pollutionRecord = await _context.PollutionRecord.FindAsync(id);
            if (pollutionRecord != null)
            {
                _context.PollutionRecord.Remove(pollutionRecord);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PollutionRecordExists(int id)
        {
            return _context.PollutionRecord.Any(e => e.Id == id);
        }
    }
}
