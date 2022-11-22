using System.Text.Json.Serialization;

namespace TechPortalServer.Models
{
    public class Patent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}