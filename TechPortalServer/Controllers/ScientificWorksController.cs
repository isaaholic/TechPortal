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
    public class ScientificWorksController : ControllerBase
    {
        private readonly ServerDbContext _context;

        public ScientificWorksController(ServerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ScientificWork>>> GetWork(Guid id)
        {
            var work = await _context.ScientificWorks
                .Where(e => e.UserId == id).ToListAsync();
            return work;
        }

        [HttpPost]
        public async Task<ActionResult<List<ScientificWork>>> AddWork(ScientificWorkDto work)
        {
            if (work == null)
                return BadRequest("Education format is wrong");

            var newWork = new ScientificWork()
            {
                Id= work.Id,
                UserId= work.UserId,
                Name= work.Name,
            };
            await _context.ScientificWorks.AddAsync(newWork);
            await _context.SaveChangesAsync();
            return await GetWork(newWork.UserId);
        }
    }
}
