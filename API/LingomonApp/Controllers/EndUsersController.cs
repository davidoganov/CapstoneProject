using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LingomonApp.Data;

namespace LingomonApp.Controllers
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
        public async Task<ActionResult<IEnumerable<EndUser>>> GetEndusers()
        {
          if (_context.Endusers == null)
          {
              return NotFound();
          }
            return await _context.Endusers.ToListAsync();
        }

        // GET: api/EndUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EndUser>> GetEndUser(string id)
        {
          if (_context.Endusers == null)
          {
              return NotFound();
          }
            var endUser = await _context.Endusers.FindAsync(id);

            if (endUser == null)
            {
                return NotFound();
            }

            return endUser;
        }

        // PUT: api/EndUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEndUser(string id, EndUser endUser)
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

        // POST: api/EndUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EndUser>> PostEndUser(EndUser endUser)
        {
          if (_context.Endusers == null)
          {
              return Problem("Entity set 'TheDbContext.Endusers'  is null.");
          }
            _context.Endusers.Add(endUser);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EndUserExists(endUser.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEndUser", new { id = endUser.Id }, endUser);
        }

        // DELETE: api/EndUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEndUser(string id)
        {
            if (_context.Endusers == null)
            {
                return NotFound();
            }
            var endUser = await _context.Endusers.FindAsync(id);
            if (endUser == null)
            {
                return NotFound();
            }

            _context.Endusers.Remove(endUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EndUserExists(string id)
        {
            return (_context.Endusers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
