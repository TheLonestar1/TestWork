using Microsoft.AspNetCore.Components;

namespace TaskView.Model
{
    public class Project
    {
        
        public Guid Id { get; set; }

        public string Name { get; set; } = "";

        public string NameCompanyOrder { get; set; } = "";

        public string NameExecuterCompany { get; set; } = "";

        public List<Employer>? Employers { get; set; } = new List<Employer>();

        public Employer? Manager { get; set; }

        public DateTime? StartProjectDate { get; set; }

        public DateTime? EndProjectDate { get; set;}

        public int priority { get; set; }
    }
}
