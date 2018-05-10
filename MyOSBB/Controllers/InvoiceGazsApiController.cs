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
    [Route("api/InvoiceGazsApi")]
    public class InvoiceGazsApiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoiceGazsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/InvoiceGazsApi
        [HttpGet]
        public IEnumerable<InvoiceGaz> GetInvoiceGazs()
        {
            return _context.InvoiceGazs;
        }

        // GET: api/InvoiceGazsApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoiceGaz([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var invoiceGaz = await _context.InvoiceGazs.SingleOrDefaultAsync(m => m.Id == id);

            if (invoiceGaz == null)
            {
                return NotFound();
            }

            return Ok(invoiceGaz);
        }

        // PUT: api/InvoiceGazsApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoiceGaz([FromRoute] int id, [FromBody] InvoiceGaz invoiceGaz)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != invoiceGaz.Id)
            {
                return BadRequest();
            }

            _context.Entry(invoiceGaz).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceGazExists(id))
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

        // POST: api/InvoiceGazsApi
        [HttpPost]
        public async Task<IActionResult> PostInvoiceGaz([FromBody] InvoiceGaz invoiceGaz)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.InvoiceGazs.Add(invoiceGaz);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInvoiceGaz", new { id = invoiceGaz.Id }, invoiceGaz);
        }

        // DELETE: api/InvoiceGazsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoiceGaz([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var invoiceGaz = await _context.InvoiceGazs.SingleOrDefaultAsync(m => m.Id == id);
            if (invoiceGaz == null)
            {
                return NotFound();
            }

            _context.InvoiceGazs.Remove(invoiceGaz);
            await _context.SaveChangesAsync();

            return Ok(invoiceGaz);
        }

        private bool InvoiceGazExists(int id)
        {
            return _context.InvoiceGazs.Any(e => e.Id == id);
        }
    }
}