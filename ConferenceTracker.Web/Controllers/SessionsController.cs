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
//    public class SessionsController : Controller
//    {
//        private readonly AppDbContext _context;

//        public SessionsController(AppDbContext context)
//        {
//            _context = context;
//        }

//        // GET: Sessions
//        public async Task<IActionResult> Index()
//        {
//            return View(await _context.Sessions.ToListAsync());
//        }

//        // GET: Sessions/Details/5
//        public async Task<IActionResult> Details(Guid? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var session = await _context.Sessions
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (session == null)
//            {
//                return NotFound();
//            }

//            return View(session);
//        }

//        // GET: Sessions/Create
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: Sessions/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Title,Description,Time,Location,Capacity,Duration,Id")] Session session)
//        {
//            if (ModelState.IsValid)
//            {
//                session.Id = Guid.NewGuid();
//                _context.Add(session);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            return View(session);
//        }

//        // GET: Sessions/Edit/5
//        public async Task<IActionResult> Edit(Guid? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var session = await _context.Sessions.FindAsync(id);
//            if (session == null)
//            {
//                return NotFound();
//            }
//            return View(session);
//        }

//        // POST: Sessions/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(Guid id, [Bind("Title,Description,Time,Location,Capacity,Duration,Id")] Session session)
//        {
//            if (id != session.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(session);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!SessionExists(session.Id))
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
//            return View(session);
//        }

//        // GET: Sessions/Delete/5
//        public async Task<IActionResult> Delete(Guid? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var session = await _context.Sessions
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (session == null)
//            {
//                return NotFound();
//            }

//            return View(session);
//        }

//        // POST: Sessions/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(Guid id)
//        {
//            var session = await _context.Sessions.FindAsync(id);
//            _context.Sessions.Remove(session);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool SessionExists(Guid id)
//        {
//            return _context.Sessions.Any(e => e.Id == id);
//        }
//    }
//}
