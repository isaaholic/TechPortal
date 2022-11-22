using System.Text.Json.Serialization;

namespace TechPortalServer.Models.Dtos
{
    public class CertificateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public Guid UserId { get; set; }
    }
}