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
    public class TrainerDocumentController : ControllerBase
    {
        private readonly CoachlyDbContext _context;

        public TrainerDocumentController(CoachlyDbContext context)
        {
            _context = context;
        }

        // GET: api/TrainerDocument
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainerDocument>>> GetTrainerDocuments()
        {
            return await _context.TrainerDocuments.ToListAsync();
        }

        // GET: api/TrainerDocument/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainerDocument>> GetTrainerDocument(int id)
        {
            var trainerDocument = await _context.TrainerDocuments.FindAsync(id);

            if (trainerDocument == null)
            {
                return NotFound();
            }

            return trainerDocument;
        }

        // PUT: api/TrainerDocument/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainerDocument(int id, TrainerDocument trainerDocument)
        {
            if (id != trainerDocument.Id)
            {
                return BadRequest();
            }

            _context.Entry(trainerDocument).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainerDocumentExists(id))
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

        // POST: api/TrainerDocument
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TrainerDocument>> PostTrainerDocument(TrainerDocument trainerDocument)
        {
            _context.TrainerDocuments.Add(trainerDocument);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrainerDocument", new { id = trainerDocument.Id }, trainerDocument);
        }

        // DELETE: api/TrainerDocument/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainerDocument(int id)
        {
            var trainerDocument = await _context.TrainerDocuments.FindAsync(id);
            if (trainerDocument == null)
            {
                return NotFound();
            }

            _context.TrainerDocuments.Remove(trainerDocument);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrainerDocumentExists(int id)
        {
            return _context.TrainerDocuments.Any(e => e.Id == id);
        }
    }
}
