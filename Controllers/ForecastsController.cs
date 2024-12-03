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
    public class ForecastsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ForecastsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Forecasts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Forecast.ToListAsync());
        }

        // GET: Forecasts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var forecast = await _context.Forecast
                .FirstOrDefaultAsync(m => m.Id == id);
            if (forecast == null)
            {
                return NotFound();
            }

            return View(forecast);
        }

        // GET: Forecasts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Forecasts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PredictedAQI,ForecastDate")] Forecast forecast)
        {
            if (ModelState.IsValid)
            {
                _context.Add(forecast);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(forecast);
        }

        // GET: Forecasts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var forecast = await _context.Forecast.FindAsync(id);
            if (forecast == null)
            {
                return NotFound();
            }
            return View(forecast);
        }

        // POST: Forecasts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PredictedAQI,ForecastDate")] Forecast forecast)
        {
            if (id != forecast.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(forecast);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ForecastExists(forecast.Id))
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
            return View(forecast);
        }

        // GET: Forecasts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var forecast = await _context.Forecast
                .FirstOrDefaultAsync(m => m.Id == id);
            if (forecast == null)
            {
                return NotFound();
            }

            return View(forecast);
        }

        // POST: Forecasts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var forecast = await _context.Forecast.FindAsync(id);
            if (forecast != null)
            {
                _context.Forecast.Remove(forecast);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ForecastExists(int id)
        {
            return _context.Forecast.Any(e => e.Id == id);
        }
    }
}
