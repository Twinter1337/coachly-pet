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
    public class TrainerAvailabilityController : ControllerBase
    {
        private readonly CoachlyDbContext _context;

        public TrainerAvailabilityController(CoachlyDbContext context)
        {
            _context = context;
        }

        // GET: api/TrainerAvailability
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainerAvailability>>> GetTrainerAvailabilities()
        {
            return await _context.TrainerAvailabilities.ToListAsync();
        }

        // GET: api/TrainerAvailability/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainerAvailability>> GetTrainerAvailability(int id)
        {
            var trainerAvailability = await _context.TrainerAvailabilities.FindAsync(id);

            if (trainerAvailability == null)
            {
                return NotFound();
            }

            return trainerAvailability;
        }

        // PUT: api/TrainerAvailability/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainerAvailability(int id, TrainerAvailability trainerAvailability)
        {
            if (id != trainerAvailability.Id)
            {
                return BadRequest();
            }

            _context.Entry(trainerAvailability).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainerAvailabilityExists(id))
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

        // POST: api/TrainerAvailability
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TrainerAvailability>> PostTrainerAvailability(TrainerAvailability trainerAvailability)
        {
            _context.TrainerAvailabilities.Add(trainerAvailability);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrainerAvailability", new { id = trainerAvailability.Id }, trainerAvailability);
        }

        // DELETE: api/TrainerAvailability/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainerAvailability(int id)
        {
            var trainerAvailability = await _context.TrainerAvailabilities.FindAsync(id);
            if (trainerAvailability == null)
            {
                return NotFound();
            }

            _context.TrainerAvailabilities.Remove(trainerAvailability);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrainerAvailabilityExists(int id)
        {
            return _context.TrainerAvailabilities.Any(e => e.Id == id);
        }
    }
}
