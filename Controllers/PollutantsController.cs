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

            return PartialView(pollutant);
        }

        // GET: Pollutants/Create
        public async Task<IActionResult> CreateOrEdit(int? id = null)
        {

            Pollutant? pollutant = null;
            if (id == null)
            {
                pollutant = new Pollutant();
            }
            else
            {
                pollutant = await _context.Pollutant.FindAsync(id);
                if (pollutant == null)
                {
                    return NotFound();
                }
            }
            return PartialView("CreateOrEditPollutant", pollutant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit(Pollutant pollutant)
        {
            if (pollutant.Id == 0)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(pollutant);

                }
                else
                {
                    return BadRequest("Not valid");
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Update(pollutant);
                }
                else
                {
                    return BadRequest("Not valid");
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Pollutants");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null|| _context.Pollutant == null)
            {
                return NotFound();
            }

            var pollutant = await _context.Pollutant
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pollutant == null)
            {
                return NotFound();
            }
            return PartialView("Delete", pollutant);
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
        
        
    }
}
