using System.Text.Json.Serialization;

namespace TechPortalServer.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}