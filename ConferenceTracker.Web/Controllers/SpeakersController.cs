using ConferenceTracker.Data.Interfaces;
using ConferenceTracker.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ConferenceTracker.Proxy.Services;

namespace ConferenceTracker.Web.Controllers
{
    public class SpeakersController : Controller
    {
        private readonly ISpeakerDataService _speakerDataService;

        public SpeakersController(IProxyService proxyService)
        {
            _speakerDataService = proxyService.SpeakerDataService();
        }

        // GET: Speakers
        public async Task<IActionResult> Index()
        {
            return View(await _speakerDataService.GetAll(0, -1));
        }

        // GET: Speakers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speaker = await _speakerDataService.Get(id.Value);
            if (speaker == null)
            {
                return NotFound();
            }

            return View(speaker);
        }

        // GET: Speakers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Speakers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Bio,FirstName,LastName,Id")] Speaker speaker)
        {
            if (ModelState.IsValid)
            {
                await _speakerDataService.Add(speaker);
                return RedirectToAction(nameof(Index));
            }
            return View(speaker);
        }

        // GET: Speakers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speaker = await _speakerDataService.Get(id.Value);
            if (speaker == null)
            {
                return NotFound();
            }
            return View(speaker);
        }

        // POST: Speakers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Bio,FirstName,LastName,Id")] Speaker speaker)
        {
            if (id != speaker.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _speakerDataService.Update(id, speaker);
                return RedirectToAction(nameof(Index));
            }
            return View(speaker);
        }

        // GET: Speakers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speaker = await _speakerDataService.Get(id.Value);
            if (speaker == null)
            {
                return NotFound();
            }

            return View(speaker);
        }

        // POST: Speakers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _speakerDataService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}