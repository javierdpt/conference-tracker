using ConferenceTracker.Communications.Interfaces;
using ConferenceTracker.Data.Interfaces;
using ConferenceTracker.Data.Proxy.Services;
using ConferenceTracker.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceTracker.Web.Controllers
{
    public class SessionsController : Controller
    {
        private readonly ILogger<SessionsController> _logger;
        private readonly ISessionDataService _sessionDataService;
        private readonly ITwilioSmsService _twilioSmsService;

        public SessionsController(ILogger<SessionsController> logger, IProxyService proxyService)
        {
            _logger = logger;
            _sessionDataService = proxyService.SessionDataService();
            _twilioSmsService = proxyService.TwilioSmsService();
        }

        // GET: Sessions
        public async Task<IActionResult> Index()
        {
            return View(await _sessionDataService.GetAll(0, -1));
        }

        // GET: Sessions/Notify/{id}
        public async Task<IActionResult> Notify(Guid id)
        {
            var session = await _sessionDataService.Get(id);
            try
            {
                await Task.WhenAll(
                    session.Attendees
                        .Where(a => a.PhoneNumber != "5555555555")
                        .Select(a => _twilioSmsService.SendText(
                            a.PhoneNumber, $"The session \"{session.Title}\" will start in {session.Time.Subtract(DateTime.Now):g} min"))
                );
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Sessions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _sessionDataService.Get(id.Value);
            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        // GET: Sessions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sessions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Time,Location,Capacity,Duration,Id")] Session session)
        {
            if (ModelState.IsValid)
            {
                await _sessionDataService.Add(session);
                return RedirectToAction(nameof(Index));
            }
            return View(session);
        }

        // GET: Sessions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _sessionDataService.Get(id.Value);
            if (session == null)
            {
                return NotFound();
            }
            return View(session);
        }

        // POST: Sessions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Title,Description,Time,Location,Capacity,Duration,Id")] Session session)
        {
            if (id != session.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _sessionDataService.Update(id, session);
                return RedirectToAction(nameof(Index));
            }
            return View(session);
        }

        // GET: Sessions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _sessionDataService.Get(id.Value);
            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        // POST: Sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _sessionDataService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}