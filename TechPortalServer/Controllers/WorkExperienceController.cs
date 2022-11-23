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
        public async Task<ActionResult<List<WorkExperience>>> AddExperience(WorkExperienceDto experience)
        {
            if (experience == null)
                return BadRequest("Experience format is wrong");

            var newExperience = new WorkExperience()
            {
                Id = experience.Id,
                UserId = experience.UserId,
                Name = experience.Name,
                Range=experience.Range
            };
            await _context.WorkExperiences.AddAsync(newExperience);
            await _context.SaveChangesAsync();
            return await GetExperience(newExperience.UserId);
        }

        [HttpPut]
        public async Task<ActionResult<List<WorkExperience>>> UpdateExperince(int id, WorkExperienceDto experience)
        {
            if (experience == null)
                return BadRequest("Experience format is wrong");

            var uExperience = _context.WorkExperiences.Find(id);

            if (uExperience == null)
                return NotFound("Experience not found");

            uExperience.Name = experience.Name;
            uExperience.Range = experience.Range;


            _context.WorkExperiences.Update(uExperience);
            await _context.SaveChangesAsync();
            return await GetExperience(uExperience.UserId);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteExperience(int id)
        {
            var experience = _context.WorkExperiences.Find(id);

            if (experience == null)
                return NotFound("Experience doesn't exist");

            _context.Remove(experience);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
