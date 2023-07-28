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
    public class NpcsController : ControllerBase
    {
        private readonly TheDbContext _context;

        public NpcsController(TheDbContext context)
        {
            _context = context;
        }

        // GET: api/Npcs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Npc>>> GetNpcs()
        {
            return await _context.Npcs.ToListAsync();
        }

        // GET: api/Npcs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Npc>> GetNpc(int id)
        {
            var npc = await _context.Npcs.FindAsync(id);

            if (npc == null)
            {
                return NotFound();
            }

            return npc;
        }

        // POST: api/Npcs
        [HttpPost]
        public async Task<ActionResult<Npc>> PostNpc(Npc npc)
        {
            _context.Npcs.Add(npc);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNpc", new { id = npc.Id }, npc);
        }

        // PUT: api/Npcs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNpc(int id, Npc npc)
        {
            if (id != npc.Id)
            {
                return BadRequest();
            }

            _context.Entry(npc).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NpcExists(id))
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

        // DELETE: api/Npcs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNpc(int id)
        {
            var npc = await _context.Npcs.FindAsync(id);
            if (npc == null)
            {
                return NotFound();
            }

            _context.Npcs.Remove(npc);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NpcExists(int id)
        {
            return _context.Npcs.Any(e => e.Id == id);
        }
    }
}

