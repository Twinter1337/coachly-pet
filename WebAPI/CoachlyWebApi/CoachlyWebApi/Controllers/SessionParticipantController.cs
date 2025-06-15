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
    public class SessionParticipantController : ControllerBase
    {
        private readonly CoachlyDbContext _context;

        public SessionParticipantController(CoachlyDbContext context)
        {
            _context = context;
        }

        // GET: api/SessionParticipant
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessionParticipant>>> GetSessionParticipants()
        {
            return await _context.SessionParticipants.ToListAsync();
        }

        // GET: api/SessionParticipant/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SessionParticipant>> GetSessionParticipant(int id)
        {
            var sessionParticipant = await _context.SessionParticipants.FindAsync(id);

            if (sessionParticipant == null)
            {
                return NotFound();
            }

            return sessionParticipant;
        }

        // PUT: api/SessionParticipant/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSessionParticipant(int id, SessionParticipant sessionParticipant)
        {
            if (id != sessionParticipant.Id)
            {
                return BadRequest();
            }

            _context.Entry(sessionParticipant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SessionParticipantExists(id))
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

        // POST: api/SessionParticipant
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SessionParticipant>> PostSessionParticipant(SessionParticipant sessionParticipant)
        {
            _context.SessionParticipants.Add(sessionParticipant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSessionParticipant", new { id = sessionParticipant.Id }, sessionParticipant);
        }

        // DELETE: api/SessionParticipant/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSessionParticipant(int id)
        {
            var sessionParticipant = await _context.SessionParticipants.FindAsync(id);
            if (sessionParticipant == null)
            {
                return NotFound();
            }

            _context.SessionParticipants.Remove(sessionParticipant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SessionParticipantExists(int id)
        {
            return _context.SessionParticipants.Any(e => e.Id == id);
        }
    }
}
