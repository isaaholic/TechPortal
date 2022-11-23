using TechPortalServer.Models.Enums;

namespace TechPortalServer.Models.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ImageUrl { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public Positions Position { get; set; }
        public Faculties Faculty{ get; set; }
        public Departments Department { get; set; }
        public string AcademicDegree { get; set; }
        public string Phone { get; set; }
        public string ResearchArea { get; set; }
        public string Email { get; set; }
    }
}
