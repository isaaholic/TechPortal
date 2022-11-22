using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechPortalServer.Data;
using TechPortalServer.Models;
using TechPortalServer.Models.Dtos;

namespace TechPortalServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ServerDbContext _context;

        public ProjectController(ServerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Project>>> GetProject(Guid id)
        {
            var projects = await _context.Projects
                .Where(e => e.UserId == id).ToListAsync();
            return projects;
        }

        [HttpPost]
        public async Task<ActionResult<List<Project>>> AddProject(ProjectDto project)
        {
            if (project == null)
                return BadRequest("Education format is wrong");

            var newProject = new Project()
            {
                Id= project.Id,
                UserId= project.UserId,
                Name= project.Name,
            };
            await _context.Projects.AddAsync(newProject);
            await _context.SaveChangesAsync();
            return await GetProject(newProject.UserId);
        }
    }
}
