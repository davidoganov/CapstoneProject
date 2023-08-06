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
    public class EnduserController : ControllerBase
    {
        private readonly TheDbContext _context;

        public EnduserController(TheDbContext context)
        {
            _context = context;
        }

        // GET: api/Enduser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enduser>>> GetEndusers()
        {
          if (_context.Endusers == null)
          {
              return NotFound();
          }
            return await _context.Endusers.ToListAsync();
        }

        // GET: api/Enduser/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Enduser>> GetEnduser(string id)
        {
          if (_context.Endusers == null)
          {
              return NotFound();
          }
            var enduser = await _context.Endusers.FindAsync(id);

            if (enduser == null)
            {
                return NotFound();
            }

            return enduser;
        }

        // PUT: api/Enduser/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnduser(string id, Enduser enduser)
        {
            if (id != enduser.Id)
            {
                return BadRequest();
            }

            _context.Entry(enduser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnduserExists(id))
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

        // POST: api/Enduser
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Enduser>> PostEnduser(Enduser enduser)
        {
          if (_context.Endusers == null)
          {
              return Problem("Entity set 'TheDbContext.Endusers'  is null.");
          }
            _context.Endusers.Add(enduser);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EnduserExists(enduser.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEnduser", new { id = enduser.Id }, enduser);
        }

        // DELETE: api/Enduser/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnduser(string id)
        {
            if (_context.Endusers == null)
            {
                return NotFound();
            }
            var enduser = await _context.Endusers.FindAsync(id);
            if (enduser == null)
            {
                return NotFound();
            }

            _context.Endusers.Remove(enduser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EnduserExists(string id)
        {
            return (_context.Endusers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
