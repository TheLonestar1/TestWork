using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using TaskView.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
        public IActionResult Index(string name, string company, int sortOrder = 0)
        {
            IEnumerable<Project> users = _dbContext.projects.ToList();
            if (!String.IsNullOrEmpty(company) && company != "All")
            {
                users = users.Where(p => p.NameCompanyOrder.Contains(company));
            }
            if (!String.IsNullOrEmpty(name))
            {
                users = users.Where(p => p.Name.Contains(name));
            }
            switch (sortOrder)
            {
                case 0:
                    ViewBag.sortOrder = 1;
                    break;
                case 1:
                    users = users.OrderBy(x => x.priority);
                    ViewBag.sortOrder = 2;
                    break;
                case 2:
                    users = users.OrderByDescending(x => x.priority);
                    ViewBag.sortOrder = 1;
                    break;
                default:
                    break;
            }

            List<string> companies = new List<string>();

            var projects = _dbContext.projects;
            foreach (Project project in projects)
            {
                companies.Add(project.NameCompanyOrder);
            }
            // устанавливаем начальный элемент, который позволит выбрать всех
            companies.Insert(0, "All");

            ViewFilterList viewModel = new ViewFilterList
            {
                project = users,
                Companies = new SelectList(companies, "Name"),
                Name = name
            };

            ViewBag.employer = _dbContext.employers.ToList();

            return View(viewModel);
        }
    }
}
