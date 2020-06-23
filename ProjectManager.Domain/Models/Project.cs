using System.Collections.Generic;

namespace ProjectManager.Domain.Models
{
    public class Project : ProjectComponent
    {
        public string OwnerId { get; set; }
        public IEnumerable<ProjectTask> Tasks { get; set; }
    }
}
