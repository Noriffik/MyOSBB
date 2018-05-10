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
    [Route("api/InvoiceServicesApi")]
    public class InvoiceServicesApiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoiceServicesApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/InvoiceServicesApi
        [HttpGet]
        public IEnumerable<InvoiceService> GetInvoiceServices()
        {
            return _context.InvoiceServices;
        }

        // GET: api/InvoiceServicesApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoiceService([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var invoiceService = await _context.InvoiceServices.SingleOrDefaultAsync(m => m.Id == id);

            if (invoiceService == null)
            {
                return NotFound();
            }

            return Ok(invoiceService);
        }

        // PUT: api/InvoiceServicesApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoiceService([FromRoute] int id, [FromBody] InvoiceService invoiceService)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != invoiceService.Id)
            {
                return BadRequest();
            }

            _context.Entry(invoiceService).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceServiceExists(id))
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

        // POST: api/InvoiceServicesApi
        [HttpPost]
        public async Task<IActionResult> PostInvoiceService([FromBody] InvoiceService invoiceService)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.InvoiceServices.Add(invoiceService);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInvoiceService", new { id = invoiceService.Id }, invoiceService);
        }

        // DELETE: api/InvoiceServicesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoiceService([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var invoiceService = await _context.InvoiceServices.SingleOrDefaultAsync(m => m.Id == id);
            if (invoiceService == null)
            {
                return NotFound();
            }

            _context.InvoiceServices.Remove(invoiceService);
            await _context.SaveChangesAsync();

            return Ok(invoiceService);
        }

        private bool InvoiceServiceExists(int id)
        {
            return _context.InvoiceServices.Any(e => e.Id == id);
        }
    }
}