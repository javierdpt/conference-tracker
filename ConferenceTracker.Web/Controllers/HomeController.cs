using System;
using ConferenceTracker.Data.Interfaces;
using ConferenceTracker.Data.Proxy.Services;
using ConferenceTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceTracker.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISessionDataService _sessionDataService;

        public HomeController(IProxyService proxyService)
        {
            _sessionDataService = proxyService.SessionDataService();
        }

        public async Task<IActionResult> Index()
        {
            var sessions = await _sessionDataService.GetAll(0, -1);
            var groups = from s in sessions
                group s by new DateTime(s.Time.Year, s.Time.Month, s.Time.Day, s.Time.Hour, 0, 0)
                into grp
                orderby grp.Key
                select grp;
            return View(groups);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}