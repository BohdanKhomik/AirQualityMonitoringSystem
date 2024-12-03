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
    public class LocationPointsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocationPointsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LocationPoints
        public async Task<IActionResult> Index()
        {
            return View(await _context.LocationPoint.ToListAsync());
        }

        // GET: LocationPoints/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locationPoint = await _context.LocationPoint
                .FirstOrDefaultAsync(m => m.Id == id);
            if (locationPoint == null)
            {
                return NotFound();
            }

            return View(locationPoint);
        }

        // GET: LocationPoints/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LocationPoints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Longtitude,Latitude")] LocationPoint locationPoint)
        {
            if (ModelState.IsValid)
            {
                _context.Add(locationPoint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(locationPoint);
        }

        // GET: LocationPoints/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locationPoint = await _context.LocationPoint.FindAsync(id);
            if (locationPoint == null)
            {
                return NotFound();
            }
            return View(locationPoint);
        }

        // POST: LocationPoints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Longtitude,Latitude")] LocationPoint locationPoint)
        {
            if (id != locationPoint.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(locationPoint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationPointExists(locationPoint.Id))
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
            return View(locationPoint);
        }

        // GET: LocationPoints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locationPoint = await _context.LocationPoint
                .FirstOrDefaultAsync(m => m.Id == id);
            if (locationPoint == null)
            {
                return NotFound();
            }

            return View(locationPoint);
        }

        // POST: LocationPoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var locationPoint = await _context.LocationPoint.FindAsync(id);
            if (locationPoint != null)
            {
                _context.LocationPoint.Remove(locationPoint);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationPointExists(int id)
        {
            return _context.LocationPoint.Any(e => e.Id == id);
        }
    }
}
