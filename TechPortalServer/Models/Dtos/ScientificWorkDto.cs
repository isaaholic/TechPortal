using System.Text.Json.Serialization;

namespace TechPortalServer.Models.Dtos
{
    public class ScientificWorkDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Catagory { get; set; }
        public string Country { get; set; }
        public DateTime Date { get; set; }
        public string Link { get; set; }
        public Guid UserId { get; set; }

    }
}