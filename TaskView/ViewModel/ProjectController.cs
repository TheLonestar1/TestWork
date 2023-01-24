using Microsoft.AspNetCore.Mvc;
using TaskView.Model;
using Microsoft.EntityFrameworkCore;
using TaskView.Model;
namespace TaskView.ViewModel
{
    public class ProjectController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        DatabaseContext _databaseContext;
        public ProjectController(ILogger<HomeController> logger, DatabaseContext context)
        {
            _logger = logger;
            _databaseContext = context;
            
        }

        [HttpGet]
        public IActionResult AddProject()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddProject(Project project)
        {
            project.Id = Guid.NewGuid();
            _databaseContext.projects.Add(project);
            _databaseContext.SaveChangesAsync();
            return RedirectToRoute("default", new {Controller = "Home" , action = "Index"});
        }

        [HttpGet]
        public IActionResult ViewProject(Guid id) 
        {
            var project = _databaseContext.projects.FirstOrDefault(x => x.Id == id);
            List<Employer> employers = _databaseContext.employers.ToList();
            foreach (Employer emp in project.Employers)
            {
                employers.Remove(emp);
            }
            ViewBag.employers = employers;
            return View(project);
            
        }
        [HttpPost]
        public IActionResult ViewProject(string name, Guid  project,bool isDelete)
        {
            Project? NewProject = _databaseContext.projects.FirstOrDefault(p => p.Id == project);
            string[] fio = name.Split(' ');
            Employer employer = _databaseContext.employers.FirstOrDefault(p => p.Name == fio[0] && p.Surname == fio[1]);
            if (!isDelete)
            {
                if (NewProject.Employers.FirstOrDefault(p => p.Id == employer.Id) == null)
                {
                    NewProject.Employers.Add(employer);
                }
            }
            else
            {
                NewProject.Employers.Remove(employer);
            }
            List<Employer> employers = _databaseContext.employers.ToList();
            foreach(Employer emp in NewProject.Employers)
            {
                employers.Remove(emp);
            }
            ViewBag.employers = employers;
            _databaseContext.projects.Update(NewProject);
            _databaseContext.SaveChanges();
            return View(NewProject);
        }
        [HttpGet]
        public IActionResult DeleteProject(Guid id)
        {
            return View(id);
        }


        [HttpPost]
        public IActionResult DeleteProject(Guid id, bool isDeleted)
        {

            Project? project = _databaseContext.projects.FirstOrDefault(p => p.Id == id);
            if (project != null)
            {
                _databaseContext.Entry(project).State = EntityState.Deleted;
                _databaseContext.SaveChanges();
                return RedirectToRoute("default", new { Controller = "Home", action = "Index" });
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult EditProject(Guid id)
        {
            Project? project = _databaseContext.projects.FirstOrDefault(p => p.Id == id);
            if(project != null)
                return View(project);
            else
                return RedirectToRoute("default", new { Controller = "Home", action = "Index" });
        }
        [HttpPost]
        public IActionResult EditProject(Project NewProject)
        {
            

            if(NewProject != null)
            {
                NewProject = NewProject;
                _databaseContext.projects.Update(NewProject);
                _databaseContext.SaveChanges();
                return RedirectToRoute("default", new { Controller = "Home", action = "Index" });
            }
            else
                return RedirectToRoute("default", new { Controller = "Home", action = "Index" });
        }
    }
}
