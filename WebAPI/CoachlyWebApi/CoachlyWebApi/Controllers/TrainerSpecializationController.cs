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
    public class TrainerSpecializationController : ControllerBase
    {
        private readonly CoachlyDbContext _context;

        public TrainerSpecializationController(CoachlyDbContext context)
        {
            _context = context;
        }

        // GET: api/TrainerSpecialization
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainerSpecialization>>> GetTrainerSpecializations()
        {
            return await _context.TrainerSpecializations.ToListAsync();
        }

        // GET: api/TrainerSpecialization/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainerSpecialization>> GetTrainerSpecialization(int id)
        {
            var trainerSpecialization = await _context.TrainerSpecializations.FindAsync(id);

            if (trainerSpecialization == null)
            {
                return NotFound();
            }

            return trainerSpecialization;
        }

        // PUT: api/TrainerSpecialization/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainerSpecialization(int id, TrainerSpecialization trainerSpecialization)
        {
            if (id != trainerSpecialization.Id)
            {
                return BadRequest();
            }

            _context.Entry(trainerSpecialization).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainerSpecializationExists(id))
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

        // POST: api/TrainerSpecialization
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TrainerSpecialization>> PostTrainerSpecialization(TrainerSpecialization trainerSpecialization)
        {
            _context.TrainerSpecializations.Add(trainerSpecialization);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrainerSpecialization", new { id = trainerSpecialization.Id }, trainerSpecialization);
        }

        // DELETE: api/TrainerSpecialization/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainerSpecialization(int id)
        {
            var trainerSpecialization = await _context.TrainerSpecializations.FindAsync(id);
            if (trainerSpecialization == null)
            {
                return NotFound();
            }

            _context.TrainerSpecializations.Remove(trainerSpecialization);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrainerSpecializationExists(int id)
        {
            return _context.TrainerSpecializations.Any(e => e.Id == id);
        }
    }
}
