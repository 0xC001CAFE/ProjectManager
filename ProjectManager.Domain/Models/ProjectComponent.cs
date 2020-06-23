namespace ProjectManager.Domain.Models
{
    public class ProjectComponent : DomainBase
    {
        public DateRange DateRange { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
