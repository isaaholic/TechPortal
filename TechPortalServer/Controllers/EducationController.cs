﻿using Microsoft.AspNetCore.Http;
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
    }
}