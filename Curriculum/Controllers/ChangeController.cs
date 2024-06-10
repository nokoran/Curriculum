/*using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Curriculum.Models;
using Curriculum.Data;
using System.Threading.Tasks;
using System.Linq;
using Curriculum.Entities;

namespace Curriculum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChangeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChangeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Changes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Change>>> GetChanges()
        {
            return await _context.Changes.ToListAsync();
        }

        // GET: api/Changes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Change>> GetChange(int id)
        {
            var change = await _context.Changes.FindAsync(id);

            if (change == null)
            {
                return NotFound();
            }

            return change;
        }

        // PUT: api/Changes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChange(Guid id, Change change)
        {
            if (id != change.id)
            {
                return BadRequest();
            }

            _context.Entry(change).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChangeExists(id))
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

        // POST: api/Changes
        [HttpPost]
        public async Task<ActionResult<Change>> PostChange(Change change)
        {
            _context.Changes.Add(change);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChange", new { id = change.id }, change);
        }

        // DELETE: api/Changes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChange(int id)
        {
            var change = await _context.Changes.FindAsync(id);
            if (change == null)
            {
                return NotFound();
            }

            _context.Changes.Remove(change);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChangeExists(Guid id)
        {
            return _context.Changes.Any(e => e.id == id);
        }
    }
}*/