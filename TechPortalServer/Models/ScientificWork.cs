using System.Text.Json.Serialization;

namespace TechPortalServer.Models
{
    public class ScientificWork
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Catagory { get; set; }
        public string Country { get; set; }
        public DateTime Date { get; set; }
        public string Link { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public Guid UserId { get; set; }

    }
}