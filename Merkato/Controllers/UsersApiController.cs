using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Merkato.Lib.Models;

namespace Merkato.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    //[ApiController]
    public class UsersApiController : Controller
    {
        private readonly MerkatoDbContext _context;

        public UsersApiController(MerkatoDbContext context)
        {
            _context = context;
        }

        // GET: api/UsersApi
        //[Route("Api/UsersApi/GetAll")]
        [HttpGet]
        public IEnumerable<User> GetUser()
        {
            return _context.User;
        }

        // GET: api/UsersApi/5
        //[Route("api/UsersApi/GetOne")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromHeader] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // GET: api/UsersApi/phone/password
        //[Route("api/UsersApi/Login")]
        [HttpPost("login/{login}")]
        public async Task<ApiResult<Agent>> Login([FromBody] LoginModel login)
        {
            ApiResult<Agent> result = new ApiResult<Agent>();
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            try
            {
                var agent = await _context.Agent.Where(c => c.AgentAppUserName.Equals(login.AgentUserName) && c.AgentAppPassword.Equals(login.AgentPassword)).SingleOrDefaultAsync();

                result.Model = agent;
                result.Successfull=1;
            }
            catch (Exception ex)
            {
                result.Successfull = 0;
                result.InternalError = ex.Message;
                result.Error = "Login failed";
            }

           

            return result;
        }

        // PUT: api/UsersApi/5
        //[Route("api/UsersApi/Update")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromHeader] int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UsersApi
        //[Route("api/UsersApi/Add")]
        [HttpPost("user")]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/UsersApi/5
        //[Route("api/UsersApi/Delete")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}