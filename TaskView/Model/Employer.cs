using Microsoft.AspNetCore.Components;

namespace TaskView.Model
{
    public class Employer
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = "";

        public string Surname { get; set; } = "";

        public string Patronymic { get; set; } = "";

    }
}
