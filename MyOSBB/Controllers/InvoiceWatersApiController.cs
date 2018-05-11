using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyOSBB.DAL.Data;
using MyOSBB.DAL.Models.Invoices;

namespace MyOSBB.Controllers
{
    [Produces("application/json")]
    [Route("api/InvoiceWatersApi")]
    public class InvoiceWatersApiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoiceWatersApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/InvoiceWatersApi
        [HttpGet]
        public IEnumerable<InvoiceWater> GetInvoiceWaters()
        {
            var result = _context.InvoiceWaters.Include(r => r.Month).Include(r => r.User).ToList();
            return result;
        }

        // GET: api/InvoiceWatersApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoiceWater([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var invoiceWater = await _context.InvoiceWaters.SingleOrDefaultAsync(m => m.Id == id);

            if (invoiceWater == null)
            {
                return NotFound();
            }

            return Ok(invoiceWater);
        }

        // PUT: api/InvoiceWatersApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoiceWater([FromRoute] int id, [FromBody] InvoiceWater invoiceWater)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != invoiceWater.Id)
            {
                return BadRequest();
            }

            _context.Entry(invoiceWater).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceWaterExists(id))
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

        // POST: api/InvoiceWatersApi
        [HttpPost]
        public async Task<IActionResult> PostInvoiceWater([FromBody] InvoiceWater invoiceWater)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.InvoiceWaters.Add(invoiceWater);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInvoiceWater", new { id = invoiceWater.Id }, invoiceWater);
        }

        // DELETE: api/InvoiceWatersApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoiceWater([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var invoiceWater = await _context.InvoiceWaters.SingleOrDefaultAsync(m => m.Id == id);
            if (invoiceWater == null)
            {
                return NotFound();
            }

            _context.InvoiceWaters.Remove(invoiceWater);
            await _context.SaveChangesAsync();

            return Ok(invoiceWater);
        }

        private bool InvoiceWaterExists(int id)
        {
            return _context.InvoiceWaters.Any(e => e.Id == id);
        }
    }
}