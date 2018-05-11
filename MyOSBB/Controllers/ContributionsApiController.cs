using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyOSBB.DAL.Data;
using MyOSBB.DAL.Models;

namespace MyOSBB.Controllers
{
    [Produces("application/json")]
    [Route("api/ContributionsApi")]
    public class ContributionsApiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContributionsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ContributionsApi
        [HttpGet]
        public IEnumerable<Contribution> GetContributions()
        {
            var result = _context.Contributions.Include(r => r.Month).Include(r => r.User).ToList();
            return result;
        }

        // GET: api/ContributionsApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContribution([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contribution = await _context.Contributions.SingleOrDefaultAsync(m => m.Id == id);

            if (contribution == null)
            {
                return NotFound();
            }

            return Ok(contribution);
        }

        // PUT: api/ContributionsApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContribution([FromRoute] int id, [FromBody] Contribution contribution)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contribution.Id)
            {
                return BadRequest();
            }

            _context.Entry(contribution).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContributionExists(id))
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

        // POST: api/ContributionsApi
        [HttpPost]
        public async Task<IActionResult> PostContribution([FromBody] Contribution contribution)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Contributions.Add(contribution);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContribution", new { id = contribution.Id }, contribution);
        }

        // DELETE: api/ContributionsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContribution([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contribution = await _context.Contributions.SingleOrDefaultAsync(m => m.Id == id);
            if (contribution == null)
            {
                return NotFound();
            }

            _context.Contributions.Remove(contribution);
            await _context.SaveChangesAsync();

            return Ok(contribution);
        }

        private bool ContributionExists(int id)
        {
            return _context.Contributions.Any(e => e.Id == id);
        }
    }
}