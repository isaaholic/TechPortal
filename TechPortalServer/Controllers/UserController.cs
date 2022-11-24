using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TechPortalServer.Data;
using TechPortalServer.Models;
using TechPortalServer.Models.Dtos;

namespace TechPortalServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ServerDbContext _context;
        private readonly IConfiguration _config;

        public UserController(ServerDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            return Ok(await _context.Users
                .Include(u => u.Certificates)
                .Include(u => u.Diploms)
                .Include(u => u.Educations)
                .Include(u => u.Inventions)
                .Include(u => u.Patents)
                .Include(u => u.Projects)
                .Include(u => u.ScientificWorks)
                .Include(u => u.Skills)
                .Include(u => u.WorkExperiences)
                .ToListAsync());
        }

        [HttpGet("users")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            List<User> users = await _context.Users
    .Include(u => u.Certificates)
    .Include(u => u.Diploms)
    .Include(u => u.Educations)
    .Include(u => u.Inventions)
    .Include(u => u.Patents)
    .Include(u => u.Projects)
    .Include(u => u.ScientificWorks)
    .Include(u => u.Skills)
    .Include(u => u.WorkExperiences)
    .ToListAsync();
            var user = users.Where(u => u.Id == id);
            if (user == null)
                return NotFound();
            return Ok(user.ToList()[0]);
        }

        [HttpPut("changemail")]
        public async Task<ActionResult<User>> UpdateMail(Guid id,string mail)
        {
           var uUser = _context.Users.Find(id);
           if (uUser == null) return NotFound();

           uUser.Email= mail;

            _context.Users.Update(uUser);
            return Ok(uUser);
        }

        [HttpPut("changeimage")]
        public async Task<ActionResult<User>> UpdatePhoto(Guid id,string photo)
        {
           var uUser = _context.Users.Find(id);
           if (uUser == null) return NotFound();

           uUser.ImageUrl= photo;

            _context.Users.Update(uUser);
            return Ok(uUser);
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            if (user == null)
                return BadRequest("User format is wrong");

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> AddUser(UserDto request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var newUser = new User()
            {
                Id = Guid.NewGuid(),
                UserName = request.UserName,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash,
                FullName = request.FullName,
                Phone = request.Phone,
                Position = request.Position,
                Department= request.Department,
                Faculty= request.Faculty,
                Email = request.Email,
                AcademicDegree = request.AcademicDegree,
                ImageUrl = request.ImageUrl,
                ResearchArea = request.ResearchArea
            };
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return newUser;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            var existingUser = _context.Users.ToList().Find(u=>u.UserName==request.UserName);

            if (existingUser == null)
            {
                return BadRequest("User don't found");
            }

            if (!VerifyPasswordHash(request.Password, existingUser.PasswordHash, existingUser.PasswordSalt))
            {
                return BadRequest("Wrong Password");
            }

            string token = CreateToken(existingUser);
            return Ok(token);
        }

        private string CreateToken(User user)
        {
            string role = user.Position.ToString();

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);


            return jwt;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            };
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            };
        }
    }
}
