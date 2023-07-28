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
    public class LingodexController : ControllerBase
    {
        private readonly TheDbContext _context;

        public LingodexController(TheDbContext context)
        {
            _context = context;
        }

        // GET: api/Lingodex
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lingodex>>> GetLingoDices()
        {
            return await _context.Lingodices.ToListAsync();
        }

        // GET: api/Lingodex/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lingodex>> GetLingodex(int id)
        {
            var lingoDex = await _context.Lingodices.FindAsync(id);

            if (lingoDex == null)
            {
                return NotFound();
            }

            return lingoDex;
        }

        // POST: api/Lingodex
        [HttpPost]
        public async Task<ActionResult<Lingodex>> PostLingodex(Lingodex lingoDex)
        {
            _context.Lingodices.Add(lingoDex);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLingodex", new { id = lingoDex.Id }, lingoDex);
        }

        // PUT: api/Lingodex/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLingodex(int id, Lingodex lingoDex)
        {
            if (id != lingoDex.Id)
            {
                return BadRequest();
            }

            _context.Entry(lingoDex).State = EntityState.Modified;

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

        // DELETE: api/Lingodex/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLingodex(int id)
        {
            var lingoDex = await _context.Lingodices.FindAsync(id);
            if (lingoDex == null)
            {
                return NotFound();
            }

            _context.Lingodices.Remove(lingoDex);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LingodexExists(int id)
        {
            return _context.Lingodices.Any(e => e.Id == id);
        }
    }
}
