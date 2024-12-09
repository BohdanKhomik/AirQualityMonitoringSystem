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

        [HttpGet]
        public IActionResult Index()
        {
            var points = _context.LocationPoint
                .Select(lp => new
                {
                    lp.Id,
                    lp.Name,
                    lp.Longtitude,
                    lp.Latitude,
                    AQI = lp.AirQualityRecords
                        .OrderByDescending(r => r.TimeStamp)
                        .Select(r => r.AQI)
                        .FirstOrDefault()
                })
                .ToList();

            return View(points);
        }


        [HttpGet]
        public IActionResult DetailsPage(int id)
        {
            // Fetch the main point by ID
            var mainPoint = _context.LocationPoint
                .Where(lp => lp.Id == id)
                .Select(lp => new
                {
                    lp.Id,
                    lp.Name,
                    lp.Latitude,
                    lp.Longtitude,
                    AQI = lp.AirQualityRecords
                        .OrderByDescending(r => r.TimeStamp)
                        .Select(r => r.AQI)
                        .FirstOrDefault()
                })
                .FirstOrDefault();

            if (mainPoint == null)
                return NotFound();

            var points = _context.LocationPoint
                .Select(lp => new
                {
                    lp.Id,
                    lp.Name,
                    lp.Longtitude,
                    lp.Latitude,
                    AQI = lp.AirQualityRecords
                        .OrderByDescending(r => r.TimeStamp)
                        .Select(r => r.AQI)
                        .FirstOrDefault()
                })
                .ToList();

            // Fetch all other points
            var otherPoints = _context.LocationPoint
                .Where(lp => lp.Id != id)
                .Select(lp => new
                {
                    lp.Id,
                    lp.Name,
                    lp.Latitude,
                    lp.Longtitude,
                    AQI = lp.AirQualityRecords
                        .OrderByDescending(r => r.TimeStamp)
                        .Select(r => r.AQI)
                        .FirstOrDefault()
                })
                .ToList();

            ViewBag.MainPoint = mainPoint;
            ViewBag.OtherLocations = otherPoints;

            return View(points);
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
            string resultRecommendation = " ";

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
                        resultRecommendation = level.LevelRecomendation;
                    }
                }
            }

            return Json(new { aqi = Math.Round(maxAQI), color = resultColor, recommendation = resultRecommendation });
        }
        public class PollutantConcentrationInput
        {
            public int PollutantId { get; set; }
            public double Concentration { get; set; }
        }


        public IActionResult AboutPage()
        {
            return View("About");
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
