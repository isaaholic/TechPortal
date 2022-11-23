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
                return BadRequest("Project format is wrong");

            var newProject = new Project()
            {
                Id = project.Id,
                UserId = project.UserId,
                Name = project.Name,
                About= project.About,
            };
            await _context.Projects.AddAsync(newProject);
            await _context.SaveChangesAsync();
            return await GetProject(newProject.UserId);
        }

        [HttpPut]
        public async Task<ActionResult<List<Project>>> UpdateProject(int id, ProjectDto project)
        {
            if (project == null)
                return BadRequest("Project format is wrong");

            var uProject = _context.Projects.Find(id);

            if (uProject == null)
                return NotFound("Project not found");

            uProject.Name = project.Name;
            uProject.About= project.About;

            _context.Projects.Update(uProject);
            await _context.SaveChangesAsync();
            return await GetProject(uProject.UserId);
        }
    }
}
