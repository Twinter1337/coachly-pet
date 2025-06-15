using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoachlyBackEnd.Models;

namespace CoachlyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribtionController : ControllerBase
    {
        private readonly CoachlyDbContext _context;

        public SubscribtionController(CoachlyDbContext context)
        {
            _context = context;
        }

        // GET: api/Subscribtion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subscribtion>>> GetSubscribtions()
        {
            return await _context.Subscribtions.ToListAsync();
        }

        // GET: api/Subscribtion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Subscribtion>> GetSubscribtion(int id)
        {
            var subscribtion = await _context.Subscribtions.FindAsync(id);

            if (subscribtion == null)
            {
                return NotFound();
            }

            return subscribtion;
        }

        // PUT: api/Subscribtion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubscribtion(int id, Subscribtion subscribtion)
        {
            if (id != subscribtion.Id)
            {
                return BadRequest();
            }

            _context.Entry(subscribtion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubscribtionExists(id))
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

        // POST: api/Subscribtion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Subscribtion>> PostSubscribtion(Subscribtion subscribtion)
        {
            _context.Subscribtions.Add(subscribtion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubscribtion", new { id = subscribtion.Id }, subscribtion);
        }

        // DELETE: api/Subscribtion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubscribtion(int id)
        {
            var subscribtion = await _context.Subscribtions.FindAsync(id);
            if (subscribtion == null)
            {
                return NotFound();
            }

            _context.Subscribtions.Remove(subscribtion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubscribtionExists(int id)
        {
            return _context.Subscribtions.Any(e => e.Id == id);
        }
    }
}
