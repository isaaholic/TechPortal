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
        public async Task<ActionResult<List<Invention>>> AddInvention(InventionDto invention)
        {
            if (invention == null)
                return BadRequest("Invention format is wrong");

            var newInvention = new Invention()
            {
                Id= invention.Id,
                UserId= invention.UserId,
                Name= invention.Name,
                About=invention.About
            };
            await _context.Inventions.AddAsync(newInvention);
            await _context.SaveChangesAsync();
            return await GetInvention(newInvention.UserId);
        }

        [HttpPut]
        public async Task<ActionResult<List<Invention>>> UpdateInvention(int id, InventionDto invention)
        {
            if (invention == null)
                return BadRequest("Experience format is wrong");

            var uInvention = _context.Inventions.Find(id);

            if (uInvention == null)
                return NotFound("Experience not found");

            uInvention.Name = invention.Name;
            uInvention.About = invention.About;


            _context.Inventions.Update(uInvention);
            await _context.SaveChangesAsync();
            return await GetInvention(uInvention.UserId);
        }
    }
}
