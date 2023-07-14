using Microsoft.AspNetCore.Mvc;
using MVCStartup.Models;
using MVCStartup.Models.DB;
using MVCStartup.Models.DB.Entities;
using MVCStartup.Models.DB.Repositories;
using System.Diagnostics;

namespace MVCStartup.Controllers
{
    public class RequestsController : Controller
    {
        private readonly IRequestRepository _repo;
        private readonly ILogger<RequestsController> _logger;
        public RequestsController(ILogger<RequestsController> loggerSet, IRequestRepository repoSet)
        {
            _repo = repoSet;
            _logger = loggerSet;
        }
        [Route("/logs")]
        public async Task<IActionResult> Index()
        {
            var requests = await _repo.GetRequests();
            return View(requests);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}