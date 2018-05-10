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
    [Route("api/InvoiceElectroesApi")]
    public class InvoiceElectroesApiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoiceElectroesApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/InvoiceElectroesApi
        [HttpGet]
        public IEnumerable<InvoiceElectro> GetInvoiceElectros()
        {
            return _context.InvoiceElectros;
        }

        // GET: api/InvoiceElectroesApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoiceElectro([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var invoiceElectro = await _context.InvoiceElectros.SingleOrDefaultAsync(m => m.Id == id);

            if (invoiceElectro == null)
            {
                return NotFound();
            }

            return Ok(invoiceElectro);
        }

        // PUT: api/InvoiceElectroesApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoiceElectro([FromRoute] int id, [FromBody] InvoiceElectro invoiceElectro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != invoiceElectro.Id)
            {
                return BadRequest();
            }

            _context.Entry(invoiceElectro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceElectroExists(id))
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

        // POST: api/InvoiceElectroesApi
        [HttpPost]
        public async Task<IActionResult> PostInvoiceElectro([FromBody] InvoiceElectro invoiceElectro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.InvoiceElectros.Add(invoiceElectro);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInvoiceElectro", new { id = invoiceElectro.Id }, invoiceElectro);
        }

        // DELETE: api/InvoiceElectroesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoiceElectro([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var invoiceElectro = await _context.InvoiceElectros.SingleOrDefaultAsync(m => m.Id == id);
            if (invoiceElectro == null)
            {
                return NotFound();
            }

            _context.InvoiceElectros.Remove(invoiceElectro);
            await _context.SaveChangesAsync();

            return Ok(invoiceElectro);
        }

        private bool InvoiceElectroExists(int id)
        {
            return _context.InvoiceElectros.Any(e => e.Id == id);
        }
    }
}