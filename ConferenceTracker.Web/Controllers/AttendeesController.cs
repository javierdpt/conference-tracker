//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using ConferenceTracker.Model;
//using ConferenceTracker.Web.Data;

//namespace ConferenceTracker.Web.Controllers
//{
//    public class AttendeesController : Controller
//    {
//        private readonly AppDbContext _context;

//        public AttendeesController(AppDbContext context)
//        {
//            _context = context;
//        }

//        // GET: Attendees
//        public async Task<IActionResult> Index()
//        {
//            return View(await _context.Attendee.ToListAsync());
//        }

//        // GET: Attendees/Details/5
//        public async Task<IActionResult> Details(Guid? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var attendee = await _context.Attendee
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (attendee == null)
//            {
//                return NotFound();
//            }

//            return View(attendee);
//        }

//        // GET: Attendees/Create
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: Attendees/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Company,Email,PhoneNumber,FirstName,LastName,Id")] Attendee attendee)
//        {
//            if (ModelState.IsValid)
//            {
//                attendee.Id = Guid.NewGuid();
//                _context.Add(attendee);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            return View(attendee);
//        }

//        // GET: Attendees/Edit/5
//        public async Task<IActionResult> Edit(Guid? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var attendee = await _context.Attendee.FindAsync(id);
//            if (attendee == null)
//            {
//                return NotFound();
//            }
//            return View(attendee);
//        }

//        // POST: Attendees/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(Guid id, [Bind("Company,Email,PhoneNumber,FirstName,LastName,Id")] Attendee attendee)
//        {
//            if (id != attendee.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(attendee);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!AttendeeExists(attendee.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            return View(attendee);
//        }

//        // GET: Attendees/Delete/5
//        public async Task<IActionResult> Delete(Guid? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var attendee = await _context.Attendee
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (attendee == null)
//            {
//                return NotFound();
//            }

//            return View(attendee);
//        }

//        // POST: Attendees/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(Guid id)
//        {
//            var attendee = await _context.Attendee.FindAsync(id);
//            _context.Attendee.Remove(attendee);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool AttendeeExists(Guid id)
//        {
//            return _context.Attendee.Any(e => e.Id == id);
//        }
//    }
//}
