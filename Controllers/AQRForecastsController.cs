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
    public class AQRForecastsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AQRForecastsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AQRForecasts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AQRForecast.Include(a => a.AirQualityRecord).Include(a => a.Forecast);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AQRForecasts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aQRForecast = await _context.AQRForecast
                .Include(a => a.AirQualityRecord)
                .Include(a => a.Forecast)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aQRForecast == null)
            {
                return NotFound();
            }

            return View(aQRForecast);
        }

        // GET: AQRForecasts/Create
        public IActionResult Create()
        {
            ViewData["AQRId"] = new SelectList(_context.AirQualityRecords, "Id", "Id");
            ViewData["ForecastId"] = new SelectList(_context.Forecast, "Id", "Id");
            return View();
        }

        // POST: AQRForecasts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AQRId,ForecastId")] AQRForecast aQRForecast)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aQRForecast);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AQRId"] = new SelectList(_context.AirQualityRecords, "Id", "Id", aQRForecast.AQRId);
            ViewData["ForecastId"] = new SelectList(_context.Forecast, "Id", "Id", aQRForecast.ForecastId);
            return View(aQRForecast);
        }

        // GET: AQRForecasts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aQRForecast = await _context.AQRForecast.FindAsync(id);
            if (aQRForecast == null)
            {
                return NotFound();
            }
            ViewData["AQRId"] = new SelectList(_context.AirQualityRecords, "Id", "Id", aQRForecast.AQRId);
            ViewData["ForecastId"] = new SelectList(_context.Forecast, "Id", "Id", aQRForecast.ForecastId);
            return View(aQRForecast);
        }

        // POST: AQRForecasts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AQRId,ForecastId")] AQRForecast aQRForecast)
        {
            if (id != aQRForecast.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aQRForecast);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AQRForecastExists(aQRForecast.Id))
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
            ViewData["AQRId"] = new SelectList(_context.AirQualityRecords, "Id", "Id", aQRForecast.AQRId);
            ViewData["ForecastId"] = new SelectList(_context.Forecast, "Id", "Id", aQRForecast.ForecastId);
            return View(aQRForecast);
        }

        // GET: AQRForecasts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aQRForecast = await _context.AQRForecast
                .Include(a => a.AirQualityRecord)
                .Include(a => a.Forecast)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aQRForecast == null)
            {
                return NotFound();
            }

            return View(aQRForecast);
        }

        // POST: AQRForecasts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aQRForecast = await _context.AQRForecast.FindAsync(id);
            if (aQRForecast != null)
            {
                _context.AQRForecast.Remove(aQRForecast);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AQRForecastExists(int id)
        {
            return _context.AQRForecast.Any(e => e.Id == id);
        }
    }
}
