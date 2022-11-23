namespace TechPortalServer.Models.Dtos
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public Guid UserId { get; set; }
    }
}