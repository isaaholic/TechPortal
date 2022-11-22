using System.Text.Json.Serialization;

namespace TechPortalServer.Models
{
    public class Certificate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}