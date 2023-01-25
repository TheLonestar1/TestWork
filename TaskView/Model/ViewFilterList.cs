using Microsoft.AspNetCore.Mvc.Rendering;

namespace TaskView.Model
{
    public class ViewFilterList
    {
        public IEnumerable<Project> project;
        public SelectList Companies { get; set; } 
        public string Name { get; set; }
    }
}
