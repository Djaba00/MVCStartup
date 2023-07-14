using Microsoft.AspNetCore.Mvc;
using MVCStartup.Models.DB.Entities;
using MVCStartup.Models.DB.Repositories;

namespace MVCStartup.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository _repo;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> loggerSet, IUserRepository repoSet)
        {
            _logger = loggerSet;
            _repo = repoSet;
        }

        public async Task<IActionResult> Index()
        {
            var authors = await _repo.GetUsers();
                    
            return View(authors);

        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User newUser)
        {
            await _repo.AddUser(newUser);

            return View(newUser);
        }


    }
}