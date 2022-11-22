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
    public class WorkExperienceController : ControllerBase
    {
        private readonly ServerDbContext _context;

        public WorkExperienceController(ServerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<WorkExperience>>> GetExperience(Guid id)
        {
            var experience = await _context.WorkExperiences
                .Where(e => e.UserId == id).ToListAsync();
            return experience;
        }

        [HttpPost]
        public async Task<ActionResult<List<WorkExperience>>> AddEducation(WorkExperienceDto experience)
        {
            if (experience == null)
                return BadRequest("Education format is wrong");

            var newExperience = new Education()
            {
                Id= experience.Id,
                UserId= experience.UserId,
                Name= experience.Name,
            };
            await _context.Educations.AddAsync(newExperience);
            await _context.SaveChangesAsync();
            return await GetExperience(newExperience.UserId);
        }
    }
}
