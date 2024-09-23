using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Farhaan.Models;
using Farhaan.Areas.Identity.Data;

namespace Farhaan.Controllers
{
    public class CarsController : Controller
    {
        private readonly FarhaanContext _context;

        public CarsController(FarhaanContext context)
        {
            _context = context;
        }

        // GET: Cars
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["CurrentSortOrder"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;

            ViewData["BrandSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Brand_desc" : "";//switches the brand sorting( ascending or descending)
            ViewData["YearSortParm"] = sortOrder == "Year" ? "Year_desc" : "Year";//switches the year sorting (ascending or descending)
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "Price_desc" : "Price";//switches the price sorting ( ascending or descending)

            var cars = from c in _context.Car
                       select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                cars = cars.Where(c => c.Brand.Contains(searchString) || c.Year.ToString().Contains(searchString));
            }
            // Sort the cars based on the specified sort order
            switch (sortOrder)
            {
                case "Brand_desc":
                    cars = cars.OrderByDescending(c => c.Brand); // Sort by brand in descending order
                    break;
                case "Year":
                    cars = cars.OrderBy(c => c.Year); // Sort by year in ascending order
                    break;
                case "Year_desc":
                    cars = cars.OrderByDescending(c => c.Year); // Sort by year in descending order
                    break;
                case "Price":
                    cars = cars.OrderBy(c => c.PricePerDay); // Sort by price in ascending order
                    break;
                case "Price_desc":
                    cars = cars.OrderByDescending(c => c.PricePerDay);// Sort by price in descending order
                    break;
                default:
                    cars = cars.OrderBy(c => c.Brand); // Default sorting by brand in ascending order
                    break;
            }

            return View(await cars.AsNoTracking().ToListAsync());
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .FirstOrDefaultAsync(m => m.CarID == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarID,Brand,Year,PricePerDay")] Car car)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: Cars/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarID,Brand,Year,PricePerDay")] Car car)
        {
            if (id != car.CarID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.CarID))
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
            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .FirstOrDefaultAsync(m => m.CarID == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Car.FindAsync(id);
            if (car != null)
            {
                _context.Car.Remove(car);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Car.Any(e => e.CarID == id);
        }
    }
}
