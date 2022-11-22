using System.Text.Json.Serialization;

namespace TechPortalServer.Models
{
    public class Diplom
    {
        public int Id { get; set; }
        public string Degree { get; set; }
        public string University { get; set; }
        public string Specialization { get; set; }
        public string Catagory { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}