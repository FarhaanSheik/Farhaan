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
            // Set sorting parameters
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "Date_desc" : "Date";

            // Start querying bookings
            var bookings = from b in _context.Booking.Include(b => b.Car).Include(b => b.appUser)
                           select b;
            // Apply search filter if needed
            if (!String.IsNullOrEmpty(searchString))
            {
                bookings = bookings.Where(b => b.appUser.FirstName.Contains(searchString) || b.appUser.LastName.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(carBrand))
            {
                bookings = bookings.Where(b => b.Car.Brand.Contains(carBrand));
            }

            if (startDate.HasValue)
            {
                bookings = bookings.Where(b => b.Date >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                bookings = bookings.Where(b => b.Date <= endDate.Value);
            }

            // Apply sorting based on sortOrder
            switch (sortOrder)
            {
                case "Name_desc":
                    bookings = bookings.OrderByDescending(b => b.appUser.FirstName);
                    break;
                case "Date":
                    bookings = bookings.OrderBy(b => b.Date);
                    break;
                case "Date_desc":
                    bookings = bookings.OrderByDescending(b => b.Date);
                    break;
                default:
                    bookings = bookings.OrderBy(b => b.appUser.FirstName);
                    break;
            }

            // Apply search filter if needed
            if (!String.IsNullOrEmpty(searchString))
            {
                bookings = bookings.Where(b => b.appUser.FirstName.Contains(searchString));
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
