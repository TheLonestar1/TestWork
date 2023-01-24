using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskView.Model;

namespace TaskView.ViewModel
{
    public class EmployerController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        DatabaseContext _dbContext;
        public EmployerController(ILogger<HomeController> logger, DatabaseContext context)
        {
            _logger = logger;
            _dbContext = context;
        }

        public IActionResult AddEmployer()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddEmployer(Employer employer)
        {
            employer.Id = Guid.NewGuid();
            _dbContext.employers.Add(employer);
            _dbContext.SaveChangesAsync();
            return RedirectToRoute("default", new { Controller = "Home", action = "Index" });
        }
        [HttpGet]

        public IActionResult DeleteEmployer(Guid id)
        {
            return View(id);
        }

        [HttpGet]
        public IActionResult ViewEmployer(Guid id)
        {
            return View(_dbContext.employers.FirstOrDefault(p => p.Id == id));
        }

        [HttpPost]
        public  IActionResult DeleteEmployer(Guid id, bool isDeleted)
        {
            
                Employer? employer = _dbContext.employers.FirstOrDefault(p => p.Id == id);
                if (employer != null)
                {
                    _dbContext.Entry(employer).State = EntityState.Deleted;
                    _dbContext.SaveChanges();
                    return RedirectToRoute("default", new { Controller = "Home", action = "Index" });
                }
            
            return NotFound();
        }
        [HttpGet]
        public IActionResult EditEmployer(Guid id)
        {
            Employer? employer = _dbContext.employers.FirstOrDefault(p => p.Id == id);
            if (employer != null)
                return View(employer);
            else
                return RedirectToRoute("default", new { Controller = "Home", action = "Index" });
        }
        [HttpPost]
        public IActionResult EditEmployer(Employer employer)
        {
            if (employer != null)
            {
                _dbContext.employers.Update(employer);
                _dbContext.SaveChanges();
                return RedirectToRoute("default", new { Controller = "Home", action = "Index" });
            }
            else
                return RedirectToRoute("default", new { Controller = "Home", action = "Index" });
        }
    }
}
