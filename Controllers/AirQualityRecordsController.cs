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
    public class AirQualityRecordsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AirQualityRecordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AirQualityRecords
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AirQualityRecords.Include(a => a.LocationPoint);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AirQualityRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airQualityRecord = await _context.AirQualityRecords
                .Include(a => a.LocationPoint)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (airQualityRecord == null)
            {
                return NotFound();
            }

            return View(airQualityRecord);
        }

        // GET: AirQualityRecords/Create
        public IActionResult Create()
        {
            ViewData["LocationPointId"] = new SelectList(_context.LocationPoint, "Id", "Id");
            return View();
        }

        // POST: AirQualityRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TimeStamp,AQI,LocationPointId")] AirQualityRecord airQualityRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(airQualityRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationPointId"] = new SelectList(_context.LocationPoint, "Id", "Id", airQualityRecord.LocationPointId);
            return View(airQualityRecord);
        }

        // GET: AirQualityRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airQualityRecord = await _context.AirQualityRecords.FindAsync(id);
            if (airQualityRecord == null)
            {
                return NotFound();
            }
            ViewData["LocationPointId"] = new SelectList(_context.LocationPoint, "Id", "Id", airQualityRecord.LocationPointId);
            return View(airQualityRecord);
        }

        // POST: AirQualityRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TimeStamp,AQI,LocationPointId")] AirQualityRecord airQualityRecord)
        {
            if (id != airQualityRecord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(airQualityRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AirQualityRecordExists(airQualityRecord.Id))
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
            ViewData["LocationPointId"] = new SelectList(_context.LocationPoint, "Id", "Id", airQualityRecord.LocationPointId);
            return View(airQualityRecord);
        }

        // GET: AirQualityRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airQualityRecord = await _context.AirQualityRecords
                .Include(a => a.LocationPoint)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (airQualityRecord == null)
            {
                return NotFound();
            }

            return View(airQualityRecord);
        }

        // POST: AirQualityRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var airQualityRecord = await _context.AirQualityRecords.FindAsync(id);
            if (airQualityRecord != null)
            {
                _context.AirQualityRecords.Remove(airQualityRecord);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AirQualityRecordExists(int id)
        {
            return _context.AirQualityRecords.Any(e => e.Id == id);
        }
    }
}
