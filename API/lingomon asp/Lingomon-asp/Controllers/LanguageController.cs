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
    public class LanguageController : ControllerBase
    {
        private readonly TheDbContext _context;

        public LanguageController(TheDbContext context)
        {
            _context = context;
        }

        // GET: api/Language
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Language>>> GetLanguages()
        {
            return await _context.Languages.ToListAsync();
        }

        // GET: api/Language/5
        [HttpGet("{name}")]
        public async Task<ActionResult<Language>> GetLanguage(string name)
        {
            var language = await _context.Languages.FindAsync(name);

            if (language == null)
            {
                return NotFound();
            }

            return language;
        }

        // POST: api/Language
        /*[HttpPost]
        public async Task<ActionResult<Language>> PostLanguage(Language language)
        {
            _context.Languages.Add(language);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLanguage", new { name = language.Name }, language);
        }

        // PUT: api/Language/5
        [HttpPut("{name}")]
        public async Task<IActionResult> PutLanguage(string name, Language language)
        {
            if (name != language.Name)
            {
                return BadRequest();
            }

            _context.Entry(language).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LanguageExists(name))
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

        // DELETE: api/Language/5
        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteLanguage(string name)
        {
            var language = await _context.Languages.FindAsync(name);
            if (language == null)
            {
                return NotFound();
            }

            _context.Languages.Remove(language);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/

        private bool LanguageExists(string name)
        {
            return _context.Languages.Any(e => e.Name == name);
        }
    }
}
