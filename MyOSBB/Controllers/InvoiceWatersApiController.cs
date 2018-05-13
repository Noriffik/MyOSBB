using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyOSBB.DAL.Data;
using MyOSBB.DAL.Models;
using MyOSBB.DAL.Models.Invoices;

namespace MyOSBB.Controllers
{
    [Produces("application/json")]
    [Route("api/InvoiceWatersApi")]
    public class InvoiceWatersApiController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public InvoiceWatersApiController(SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _context = context;
        }

        // GET: api/InvoiceWatersApi
        [HttpGet]
        public IEnumerable<InvoiceWater> GetInvoiceWaters()
        {
            var result = _context.InvoiceWaters.Include(r => r.Month).Include(r => r.User).ToList();
            return result;
        }

        [HttpPost]
        public IEnumerable<InvoiceWaterApi> PostInvoiceWaters(string userName, string password)
        {
            IList<InvoiceWaterApi> result = new List<InvoiceWaterApi>();
            var user = _context.Users.Where(r => r.UserName == userName).FirstOrDefault();
            if (user != null)
            {
                var pass = _signInManager.PasswordSignInAsync(user, password, false, lockoutOnFailure: false).Result;
                if (pass.Succeeded)
                {
                    var data = _context.InvoiceWaters.Include(r => r.User).Include(r => r.Month).ToList();
                    result = data.Select(r => new InvoiceWaterApi() { Id = r.Id, InvoiceDate = r.InvoiceDate, ProviderName = r.ProviderName, Payment = r.Payment, Debt = r.Debt, Overpaid = r.Overpaid, MonthName = r.Month.Name, FlatNumber = r.User.FlatNumber }).ToList();
                }
            }
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
        //[HttpPost]
        //public async Task<IActionResult> PostInvoiceWater([FromBody] InvoiceWater invoiceWater)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.InvoiceWaters.Add(invoiceWater);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetInvoiceWater", new { id = invoiceWater.Id }, invoiceWater);
        //}

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