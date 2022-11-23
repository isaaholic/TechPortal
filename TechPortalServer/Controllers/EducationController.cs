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
    public class EducationController : ControllerBase
    {
        private readonly ServerDbContext _context;

        public EducationController(ServerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Education>>> GetEducation(Guid id)
        {
            var educations = await _context.Educations
                .Where(e => e.UserId == id).ToListAsync();
            return educations;
        }

        [HttpPost]
        public async Task<ActionResult<List<Education>>> AddEducation(EducationDto education)
        {
            if (education == null)
                return BadRequest("Education format is wrong");

            var newEducation = new Education()
            {
                Id=education.Id,
                UserId=education.UserId,
                Name=education.Name,
                Range = education.Range
            };
            await _context.Educations.AddAsync(newEducation);
            await _context.SaveChangesAsync();
            return await GetEducation(newEducation.UserId);
        }

        [HttpPut]
        public async Task<ActionResult<List<Education>>> UpdateEducation(int id, EducationDto education)
        {
            if (education == null)
                return BadRequest("Experience format is wrong");

            var uEducation = _context.Educations.Find(id);

            if (uEducation == null)
                return NotFound("Experience not found");

            uEducation.Name = education.Name;
            uEducation.Range= education.Range;


            _context.Educations.Update(uEducation);
            await _context.SaveChangesAsync();
            return await GetEducation(uEducation.UserId);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteEducation(int id)
        {
            var education = _context.Educations.Find(id);

            if (education == null)
                return NotFound("Skill doesn't exist");

            _context.Remove(education);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
