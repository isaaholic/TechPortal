using Microsoft.EntityFrameworkCore;
using TechPortalServer.Models;
using TechPortalServer.Models.Dtos;

namespace TechPortalServer.Data
{
    public class ServerDbContext:DbContext
    {
        public ServerDbContext(DbContextOptions<ServerDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<ScientificWork> ScientificWorks { get; set; }
        public DbSet<Diplom> Diploms { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Invention> Inventions { get; set; }
        public DbSet<Patent> Patents { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }
    }
}
