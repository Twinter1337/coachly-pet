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
    public class UserSubscribtionController : ControllerBase
    {
        private readonly CoachlyDbContext _context;

        public UserSubscribtionController(CoachlyDbContext context)
        {
            _context = context;
        }

        // GET: api/UserSubscribtion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserSubscribtion>>> GetUserSubscribtions()
        {
            return await _context.UserSubscribtions.ToListAsync();
        }

        // GET: api/UserSubscribtion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserSubscribtion>> GetUserSubscribtion(int id)
        {
            var userSubscribtion = await _context.UserSubscribtions.FindAsync(id);

            if (userSubscribtion == null)
            {
                return NotFound();
            }

            return userSubscribtion;
        }

        // PUT: api/UserSubscribtion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserSubscribtion(int id, UserSubscribtion userSubscribtion)
        {
            if (id != userSubscribtion.Id)
            {
                return BadRequest();
            }

            _context.Entry(userSubscribtion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserSubscribtionExists(id))
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

        // POST: api/UserSubscribtion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserSubscribtion>> PostUserSubscribtion(UserSubscribtion userSubscribtion)
        {
            _context.UserSubscribtions.Add(userSubscribtion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserSubscribtion", new { id = userSubscribtion.Id }, userSubscribtion);
        }

        // DELETE: api/UserSubscribtion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserSubscribtion(int id)
        {
            var userSubscribtion = await _context.UserSubscribtions.FindAsync(id);
            if (userSubscribtion == null)
            {
                return NotFound();
            }

            _context.UserSubscribtions.Remove(userSubscribtion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserSubscribtionExists(int id)
        {
            return _context.UserSubscribtions.Any(e => e.Id == id);
        }
    }
}
