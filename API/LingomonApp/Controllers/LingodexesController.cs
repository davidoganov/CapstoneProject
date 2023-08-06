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
    public class LingodexesController : ControllerBase
    {
        private readonly TheDbContext _context;

        public LingodexesController(TheDbContext context)
        {
            _context = context;
        }

        // GET: api/Lingodexes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lingodex>>> GetLingodices()
        {
          if (_context.Lingodices == null)
          {
              return NotFound();
          }
            return await _context.Lingodices.ToListAsync();
        }

        // GET: api/Lingodexes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lingodex>> GetLingodex(int id)
        {
          if (_context.Lingodices == null)
          {
              return NotFound();
          }
            var lingodex = await _context.Lingodices.FindAsync(id);

            if (lingodex == null)
            {
                return NotFound();
            }

            return lingodex;
        }

        // PUT: api/Lingodexes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLingodex(int id, Lingodex lingodex)
        {
            if (id != lingodex.Id)
            {
                return BadRequest();
            }

            _context.Entry(lingodex).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LingodexExists(id))
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

        // POST: api/Lingodexes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Lingodex>> PostLingodex(Lingodex lingodex)
        {
          if (_context.Lingodices == null)
          {
              return Problem("Entity set 'TheDbContext.Lingodices'  is null.");
          }
            _context.Lingodices.Add(lingodex);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLingodex", new { id = lingodex.Id }, lingodex);
        }

        // DELETE: api/Lingodexes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLingodex(int id)
        {
            if (_context.Lingodices == null)
            {
                return NotFound();
            }
            var lingodex = await _context.Lingodices.FindAsync(id);
            if (lingodex == null)
            {
                return NotFound();
            }

            _context.Lingodices.Remove(lingodex);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LingodexExists(int id)
        {
            return (_context.Lingodices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
