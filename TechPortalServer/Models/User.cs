using TechPortalServer.Models.Enums;

namespace TechPortalServer.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string ImageUrl { get; set; }
        public string FullName { get; set; }
        public Positions Position { get; set; }
        public Faculties Faculty { get; set; }
        public Departments Department { get; set; }
        public string AcademicDegree { get; set; }
        public string Phone { get; set; }
        public string ResearchArea { get; set; }
        public string Email { get; set; }
        public List<Education>? Educations { get; set; }
        public List<Skill>? Skills { get; set; }
        public List<WorkExperience>? WorkExperiences { get; set; }
        public List<ScientificWork>? ScientificWorks { get; set; }
        public List<Diplom>? Diploms { get; set; }
        public List<Certificate>? Certificates { get; set; }
        public List<Project>? Projects { get; set; }
        public List<Invention>? Inventions { get; set; }
        public List<Patent>? Patents { get; set; }
    }
}
