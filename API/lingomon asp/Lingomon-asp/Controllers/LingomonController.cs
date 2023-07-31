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
    public class LingomonController : ControllerBase
    {
        private readonly TheDbContext _context;

        public LingomonController(TheDbContext context)
        {
            _context = context;
        }

        // GET: api/Lingomon
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lingomon>>> GetLingomons()
        {
            return await _context.Lingomons.ToListAsync();
        }

        // GET: api/Lingomon/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lingomon>> GetLingomon(int id)
        {
            var lingomon = await _context.Lingomons.FindAsync(id);

            if (lingomon == null)
            {
                return NotFound();
            }

            return lingomon;
        }

        // POST: api/Lingomon
        [HttpPost]
        public async Task<ActionResult<Lingomon>> PostLingomon(Lingomon lingomon)
        {
            _context.Lingomons.Add(lingomon);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLingomon", new { id = lingomon.Id }, lingomon);
        }

        // PUT: api/Lingomon/5
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

        // DELETE: api/Lingomon/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLingomon(int id)
        {
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
            return _context.Lingomons.Any(e => e.Id == id);
        }
    }
}

