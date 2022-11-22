namespace TechPortalServer.Models.Dtos
{
    public class DiplomDto
    {
        public int Id { get; set; }
        public string Degree { get; set; }
        public string University { get; set; }
        public string Specialization { get; set; }
        public string Catagory { get; set; }
        public Guid UserId { get; set; }
    }
}