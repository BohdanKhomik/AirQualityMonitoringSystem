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
    public class UserAlertsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserAlertsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserAlerts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserAlert.Include(u => u.Alert).Include(u => u.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserAlerts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAlert = await _context.UserAlert
                .Include(u => u.Alert)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userAlert == null)
            {
                return NotFound();
            }

            return View(userAlert);
        }

        // GET: UserAlerts/Create
        public IActionResult Create()
        {
            ViewData["AlertId"] = new SelectList(_context.Alert, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id");
            return View();
        }

        // POST: UserAlerts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,AlertId")] UserAlert userAlert)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userAlert);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlertId"] = new SelectList(_context.Alert, "Id", "Id", userAlert.AlertId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", userAlert.UserId);
            return View(userAlert);
        }

        // GET: UserAlerts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAlert = await _context.UserAlert.FindAsync(id);
            if (userAlert == null)
            {
                return NotFound();
            }
            ViewData["AlertId"] = new SelectList(_context.Alert, "Id", "Id", userAlert.AlertId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", userAlert.UserId);
            return View(userAlert);
        }

        // POST: UserAlerts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,AlertId")] UserAlert userAlert)
        {
            if (id != userAlert.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userAlert);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserAlertExists(userAlert.Id))
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
            ViewData["AlertId"] = new SelectList(_context.Alert, "Id", "Id", userAlert.AlertId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", userAlert.UserId);
            return View(userAlert);
        }

        // GET: UserAlerts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAlert = await _context.UserAlert
                .Include(u => u.Alert)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userAlert == null)
            {
                return NotFound();
            }

            return View(userAlert);
        }

        // POST: UserAlerts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userAlert = await _context.UserAlert.FindAsync(id);
            if (userAlert != null)
            {
                _context.UserAlert.Remove(userAlert);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserAlertExists(int id)
        {
            return _context.UserAlert.Any(e => e.Id == id);
        }
    }
}
