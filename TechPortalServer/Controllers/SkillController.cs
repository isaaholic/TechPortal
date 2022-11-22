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
    public class SkillController : ControllerBase
    {
        private readonly ServerDbContext _context;

        public SkillController(ServerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Skill>>> GetSkill(Guid id)
        {
            var skill = await _context.Skills
                .Where(e => e.UserId == id).ToListAsync();
            return skill;
        }

        [HttpPost]
        public async Task<ActionResult<List<Skill>>> AddEducation(SkillDto skill)
        {
            if (skill == null)
                return BadRequest("Education format is wrong");

            var newSkill = new Skill()
            {
                Id= skill.Id,
                UserId= skill.UserId,
                Name= skill.Name,
            };
            await _context.Skills.AddAsync(newSkill);
            await _context.SaveChangesAsync();
            return await GetSkill(newSkill.UserId);
        }
    }
}
