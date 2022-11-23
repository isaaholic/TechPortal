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
    public class CertificateController : ControllerBase
    {
        private readonly ServerDbContext _context;

        public CertificateController(ServerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Certificate>>> GetCertificate(Guid id)
        {
            var certificates = await _context.Certificates
                .Where(e => e.UserId == id).ToListAsync();
            return certificates;
        }

        [HttpPost]
        public async Task<ActionResult<List<Certificate>>> AddCertificate(CertificateDto certificate)
        {
            if (certificate == null)
                return BadRequest("Certificate format is wrong");

            var newCertificate = new Certificate()
            {
                Id = certificate.Id,
                UserId = certificate.UserId,
                Name = certificate.Name,
                Link = certificate.Link,
            };
            await _context.Certificates.AddAsync(newCertificate);
            await _context.SaveChangesAsync();
            return await GetCertificate(newCertificate.UserId);
        }
        [HttpPut]
        public async Task<ActionResult<List<Certificate>>> UpdateCertificate(int id, CertificateDto certificate)
        {
            if (certificate == null)
                return BadRequest("Certificate format is wrong");

            var uCertificate = _context.Certificates.Find(id);

            if (uCertificate == null)
                return NotFound("Certificate not found");

            uCertificate.Name = certificate.Name;


            _context.Certificates.Update(uCertificate);
            await _context.SaveChangesAsync();
            return await GetCertificate(uCertificate.UserId);
        }
    }
}
