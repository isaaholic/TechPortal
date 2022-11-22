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
    public class DiplomController : ControllerBase
    {
        private readonly ServerDbContext _context;

        public DiplomController(ServerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Diplom>>> GetDiplom(Guid id)
        {
            var diploms = await _context.Diploms
                .Where(e => e.UserId == id).ToListAsync();
            return diploms;
        }

        [HttpPost]
        public async Task<ActionResult<List<Diplom>>> AddDiplom(DiplomDto diplom)
        {
            if (diplom == null)
                return BadRequest("Education format is wrong");

            var newDiplom = new Diplom()
            {
                Id= diplom.Id,
                UserId= diplom.UserId,
                Catagory = diplom.Catagory,
                Degree=diplom.Degree,
                Specialization=diplom.Specialization,
                University=diplom.University,
            };
            await _context.Diploms.AddAsync(newDiplom);
            await _context.SaveChangesAsync();
            return await GetDiplom(newDiplom.UserId);
        }
    }
}