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
    public class TypeController : ControllerBase
    {
        private readonly TheDbContext _context;

        public TypeController(TheDbContext context)
        {
            _context = context;
        }

        // GET: api/Type
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Data.Type>>> GetTypes()
        {
            return await _context.Types.ToListAsync();
        }

        // GET: api/Type/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Data.Type>> GetType(int id)
        {
            var type = await _context.Types.FindAsync(id);

            if (type == null)
            {
                return NotFound();
            }

            return type;
        }

        // POST: api/Type
        /*[HttpPost]
        public async Task<ActionResult<Data.Type>> PostType(Data.Type type)
        {
            _context.Types.Add(type);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetType", new { id = type.Id }, type);
        }

        // PUT: api/Type/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutType(int id, Data.Type type)
        {
            if (id != type.Id)
            {
                return BadRequest();
            }

            _context.Entry(type).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeExists(id))
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

        // DELETE: api/Type/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteType(int id)
        {
            var type = await _context.Types.FindAsync(id);
            if (type == null)
            {
                return NotFound();
            }

            _context.Types.Remove(type);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/

        private bool TypeExists(int id)
        {
            return _context.Types.Any(e => e.Id == id);
        }
    }
}
