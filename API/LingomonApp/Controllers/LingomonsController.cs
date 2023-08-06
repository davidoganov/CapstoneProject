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
    public class LingomonsController : ControllerBase
    {
        private readonly TheDbContext _context;

        public LingomonsController(TheDbContext context)
        {
            _context = context;
        }

        // GET: api/Lingomons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lingomon>>> GetLingomons()
        {
          if (_context.Lingomons == null)
          {
              return NotFound();
          }
            return await _context.Lingomons.ToListAsync();
        }

        // GET: api/Lingomons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lingomon>> GetLingomon(int id)
        {
          if (_context.Lingomons == null)
          {
              return NotFound();
          }
            var lingomon = await _context.Lingomons.FindAsync(id);

            if (lingomon == null)
            {
                return NotFound();
            }

            return lingomon;
        }

        // PUT: api/Lingomons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLingomon(int id, Lingomon lingomon)
        {
            if (id != lingomon.Id)
            {
                return BadRequest();
            }

            _context.Entry(lingomon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LingomonExists(id))
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

        // POST: api/Lingomons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Lingomon>> PostLingomon(Lingomon lingomon)
        {
          if (_context.Lingomons == null)
          {
              return Problem("Entity set 'TheDbContext.Lingomons'  is null.");
          }
            _context.Lingomons.Add(lingomon);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLingomon", new { id = lingomon.Id }, lingomon);
        }

        // DELETE: api/Lingomons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLingomon(int id)
        {
            if (_context.Lingomons == null)
            {
                return NotFound();
            }
            var lingomon = await _context.Lingomons.FindAsync(id);
            if (lingomon == null)
            {
                return NotFound();
            }

            _context.Lingomons.Remove(lingomon);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LingomonExists(int id)
        {
            return (_context.Lingomons?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
