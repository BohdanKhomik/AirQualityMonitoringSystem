using AQIViewer.Data;
using AQIViewer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AQIViewer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _context = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Calculator()
        {
            var pollutants = _context.Pollutant.Include(p => p.PollutantLevels).ToList();
            return View("AQICalculator", pollutants);
        }

        [HttpPost]
        public IActionResult CalculateAQI([FromBody] PollutantConcentrationInput input)
        {
            var pollutantLevels = _context.PollutantLevel
                .Include(pl => pl.PollutionLevel)
                .Where(pl => pl.PollutantId == input.PollutantId)
                .ToList();

            if (!pollutantLevels.Any())
            {
                return Json(new { error = "No data found for the selected pollutant." });
            }

            double maxAQI = 0;
            string resultColor = "#000";

            foreach (var level in pollutantLevels)
            {
                if (input.Concentration >= level.MinConcentration && input.Concentration <= level.MaxConcentration)
                {
                    var C = input.Concentration;
                    var Clow = level.MinConcentration;
                    var Chigh = level.MaxConcentration;
                    var Ilow = level.PollutionLevel.MinPoint;
                    var Ihigh = level.PollutionLevel.MaxPoint;

                    double pollutantAQI = ((Ihigh - Ilow) / (Chigh - Clow)) * (C - Clow) + Ilow;

                    if (pollutantAQI > maxAQI)
                    {
                        maxAQI = pollutantAQI;
                        resultColor = level.PollutionLevel.Color;
                    }
                }
            }

            return Json(new { aqi = Math.Round(maxAQI, 2), color = resultColor });
        }

        public class PollutantConcentrationInput
    {
        public int PollutantId { get; set; }
        public double Concentration { get; set; }
    }

    public IActionResult DetailsPage()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
