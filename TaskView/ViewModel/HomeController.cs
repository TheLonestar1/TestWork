using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskView.Model;

namespace TaskView.ViewModel
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        DatabaseContext _dbContext;
        public HomeController(ILogger<HomeController> logger, DatabaseContext context)
        {
            _logger = logger;
            _dbContext = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_dbContext);
        }
    }
}
