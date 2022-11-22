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
    public class InventionController : ControllerBase
    {
        private readonly ServerDbContext _context;

        public InventionController(ServerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Invention>>> GetInvention(Guid id)
        {
            var invention = await _context.Inventions
                .Where(e => e.UserId == id).ToListAsync();
            return invention;
        }

        [HttpPost]
        public async Task<ActionResult<List<Invention>>> AddInvention(EducationDto invention)
        {
            if (invention == null)
                return BadRequest("Invention format is wrong");

            var newInvention = new Invention()
            {
                Id= invention.Id,
                UserId= invention.UserId,
                Name= invention.Name,
            };
            await _context.Inventions.AddAsync(newInvention);
            await _context.SaveChangesAsync();
            return await GetInvention(newInvention.UserId);
        }
    }
}
