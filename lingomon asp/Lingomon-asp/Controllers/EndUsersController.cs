using Lingomon_asp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingomon_asp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EndUsersController : ControllerBase
    {
        private readonly TheDbContext _context;

        public EndUsersController(TheDbContext context)
        {
            _context = context;
        }

        // GET: api/EndUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enduser>>> GetEndUsers()
        {
            return await _context.Endusers.ToListAsync();
        }

        // GET: api/EndUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Enduser>> GetEndUser(string id)
        {
            var endUser = await _context.Endusers.FindAsync(id);

            if (endUser == null)
            {
                return NotFound();
            }

            return endUser;
        }

        // POST: api/EndUsers
        [HttpPost]
        public async Task<ActionResult<Enduser>> PostEndUser(Enduser endUser)
        {
            _context.Endusers.Add(endUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEndUser), new { id = endUser.Id }, endUser);
        }

        // PUT: api/EndUsers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEndUser(string id, Enduser endUser)
        {
            if (id != endUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(endUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EndUserExists(id))
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

        // DELETE: api/EndUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEndUser(string id)
        {
            var endUser = await _context.Endusers.FindAsync(id);
            if (endUser == null)
            {
                return NotFound();
            }

            _context.Endusers.Remove(endUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/EndUsers/CheckExisting/{id}
        [HttpGet("CheckExisting/{id}")]
        public async Task<ActionResult<bool>> CheckExistingEndUser(string id)
        {
            var endUser = await _context.Endusers.FindAsync(id);
            return endUser != null;
        }

        // POST: api/EndUsers/Register
        [HttpPost("Register")]
        public async Task<ActionResult<Enduser>> RegisterEndUser(Enduser endUser)
        {
            var existingEndUser = await _context.Endusers.FindAsync(endUser.Id);
            if (existingEndUser != null)
            {
                return Conflict("End user already exists.");
            }

            _context.Endusers.Add(endUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEndUser), new { id = endUser.Id }, endUser);
        }

        private bool EndUserExists(string id)
        {
            return _context.Endusers.Any(e => e.Id == id);
        }
    }
}
