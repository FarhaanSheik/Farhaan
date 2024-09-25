using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Farhaan.Areas.Identity.Data;
using Farhaan.Models;

namespace Farhaan.Controllers
{
    public class BookingsController : Controller
    {
        private readonly FarhaanContext _context;

        public BookingsController(FarhaanContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index(string sortOrder, string searchString, string carBrand, DateTime? startDate, DateTime? endDate)
        {
            ViewData["CurrentFilter"] = searchString; //saves the search info for name even if you relaod the page
            ViewData["CurrentCarBrand"] = carBrand;//saves currents search changes for the brand even after reloading the page
            ViewData["CurrentStartDate"] = startDate.HasValue ? startDate.Value.ToString("yyyy-MM-dd") : "";//saves current search changes for the start date
            ViewData["CurrentEndDate"] = endDate.HasValue ? endDate.Value.ToString("yyyy-MM-dd") : "";//saves current serach changes for the brand even after reloading the page
            ViewData["CurrentSortOrder"] = sortOrder;

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";//arranges the name in ascending and descending order when clicked
            ViewData["DateSortParm"] = sortOrder == "Date" ? "Date_desc" : "Date";//arranges the date in ascending and descending order when the date label is clicked

            var bookings = from b in _context.Booking.Include(b => b.Car).Include(b => b.appUser)
                           select b;

            //Filters by name 
            if (!String.IsNullOrEmpty(searchString))
            {
                bookings = bookings.Where(b => b.appUser.FirstName.Contains(searchString) || b.appUser.LastName.Contains(searchString));
            }

            //Filters by car brand
            if (!String.IsNullOrEmpty(carBrand))
            {
                bookings = bookings.Where(b => b.Car.Brand.Contains(carBrand));
            }

            //Filters by the start date
            if (startDate.HasValue)
            {
                bookings = bookings.Where(b => b.Date >= startDate.Value);
            }

            //filters by the end date
            if (endDate.HasValue)
            {
                bookings = bookings.Where(b => b.Date <= endDate.Value);
            }

            switch (sortOrder)
            {
                case "Name_desc":
                    bookings = bookings.OrderByDescending(b => b.appUser.FirstName);//sorts the username in descending order
                    break;
                case "Date":
                    bookings = bookings.OrderBy(b => b.Date);//sorts the date by ascending order
                    break;
                case "Date_desc":
                    bookings = bookings.OrderByDescending(b => b.Date);//sorts the date by descending order
                    break;
                default:
                    bookings = bookings.OrderBy(b => b.appUser.FirstName);//this is default where the username will remain asecnding order until it is changed to descending order
                    break;
            }

            return View(await bookings.AsNoTracking().ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.Car)
                .Include(b => b.appUser)
                .FirstOrDefaultAsync(m => m.BookingID == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewData["CarID"] = new SelectList(_context.Car, "CarID", "Brand");
            ViewData["appUserID"] = new SelectList(_context.Users, "Id", "FirstName");
            return View();
        }

        // POST: Bookings/Create
       
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingID,appUserID,CarID,Date,Time,Location,TotalPrice")] Booking booking)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarID"] = new SelectList(_context.Car, "CarID", "CarID", booking.Car.Brand);
            ViewData["appUserID"] = new SelectList(_context.Users, "Id", "Id", booking.appUser.FirstName);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            ViewData["CarID"] = new SelectList(_context.Car, "CarID", "Brand", booking.CarID);
            ViewData["appUserID"] = new SelectList(_context.Users, "Id", "FirstName", booking.appUserID);
            return View(booking);
        }

        // POST: Bookings/Edit/5
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingID,appUserID,CarID,Date,Time,Location,TotalPrice")] Booking booking)
        {
            if (id != booking.BookingID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.BookingID))
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
            ViewData["CarID"] = new SelectList(_context.Car, "CarID", "Brand", booking.CarID);
            ViewData["appUserID"] = new SelectList(_context.Users, "Id", "FirstName", booking.appUserID);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.Car)
                .Include(b => b.appUser)
                .FirstOrDefaultAsync(m => m.BookingID == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            if (booking != null)
            {
                _context.Booking.Remove(booking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Booking.Any(e => e.BookingID == id);
        }
    }
}
