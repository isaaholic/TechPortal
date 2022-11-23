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
    public class PatentController : ControllerBase
    {
        private readonly ServerDbContext _context;

        public PatentController(ServerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Patent>>> GetPatent(Guid id)
        {
            var patents = await _context.Patents
                .Where(e => e.UserId == id).ToListAsync();
            return patents;
        }

        [HttpPost]
        public async Task<ActionResult<List<Patent>>> AddPatent(PatentDto patent)
        {
            if (patent == null)
                return BadRequest("Patent format is wrong");

            var newPatent = new Patent()
            {
                Id= patent.Id,
                UserId= patent.UserId,
                Name= patent.Name,
            };
            await _context.Patents.AddAsync(newPatent);
            await _context.SaveChangesAsync();
            return await GetPatent(newPatent.UserId);
        }

        [HttpPut]
        public async Task<ActionResult<List<Patent>>> UpdatePatent(int id,PatentDto patent)
        {
            if (patent == null)
                return BadRequest("Patent format is wrong");

            var uPatent = _context.Patents.Find(id);

            if (uPatent == null)
                return NotFound("Patent not found");

            uPatent.Name = patent.Name;


            _context.Patents.Update(uPatent);
            await _context.SaveChangesAsync();
            return await GetPatent(uPatent.UserId);
        }

    }
}
