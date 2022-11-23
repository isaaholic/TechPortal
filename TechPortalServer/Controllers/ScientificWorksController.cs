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
                Date=work.Date,
                Catagory=work.Catagory,
                Link=work.Link,
                Country=work.Country,
            };
            await _context.ScientificWorks.AddAsync(newWork);
            await _context.SaveChangesAsync();
            return await GetWork(newWork.UserId);
        }

        [HttpPut]
        public async Task<ActionResult<List<ScientificWork>>> UpdateWork(int id, ScientificWork work)
        {
            if (work == null)
                return BadRequest("Work format is wrong");

            var uWork = _context.ScientificWorks.Find(id);

            if (uWork == null)
                return NotFound("Work not found");

            uWork.Name = work.Name;
            uWork.Date = work.Date;
            uWork.Catagory = work.Catagory;
            uWork.Country = work.Country;
            uWork.Link = work.Link;


            _context.ScientificWorks.Update(uWork);
            await _context.SaveChangesAsync();
            return await GetWork(uWork.UserId);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteWork(int id)
        {
            var work = _context.ScientificWorks.Find(id);

            if (work == null)
                return NotFound("Work doesn't exist");

            _context.Remove(work);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
