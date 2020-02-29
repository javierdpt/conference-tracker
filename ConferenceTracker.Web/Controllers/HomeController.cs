using ConferenceTracker.Data.Interfaces;
using ConferenceTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ConferenceTracker.Proxy.Services;

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
            var sessions = await _sessionDataService.GetGrouped(0, 4);
            return View(sessions.OrderBy(s => s.Date));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}