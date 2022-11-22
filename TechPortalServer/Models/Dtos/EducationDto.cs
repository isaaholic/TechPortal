using System.Text.Json.Serialization;

namespace TechPortalServer.Models.Dtos
{
    public class EducationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Range { get; set; }
        public Guid UserId { get; set; }
    }
}