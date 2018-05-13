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
    [Route("api/InvoiceTelsApi")]
    public class InvoiceTelsApiController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public InvoiceTelsApiController(SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _context = context;
        }

        // GET: api/InvoiceTelsApi
        [HttpGet]
        public IEnumerable<InvoiceTel> GetInvoiceTels()
        {
            var result = _context.InvoiceTels.Include(r => r.Month).Include(r => r.User).ToList();
            return result;
        }

        [HttpPost]
        public IEnumerable<InvoiceTelApi> PostInvoiceTels(string userName, string password)
        {
            IList<InvoiceTelApi> result = new List<InvoiceTelApi>();
            var user = _context.Users.Where(r => r.UserName == userName).FirstOrDefault();
            if (user != null)
            {
                var pass = _signInManager.PasswordSignInAsync(user, password, false, lockoutOnFailure: false).Result;
                if (pass.Succeeded)
                {
                    var data = _context.InvoiceTels.Include(r => r.User).Include(r => r.Month).ToList();
                    result = data.Select(r => new InvoiceTelApi() { Id = r.Id, InvoiceDate = r.InvoiceDate, ProviderName = r.ProviderName, Payment = r.Payment, Debt = r.Debt, Overpaid = r.Overpaid, MonthName = r.Month.Name, FlatNumber = r.User.FlatNumber, TelNumber = r.TelNumber }).ToList();
                }
            }
            return result;
        }

        // GET: api/InvoiceTelsApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoiceTel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var invoiceTel = await _context.InvoiceTels.SingleOrDefaultAsync(m => m.Id == id);

            if (invoiceTel == null)
            {
                return NotFound();
            }

            return Ok(invoiceTel);
        }

        // PUT: api/InvoiceTelsApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoiceTel([FromRoute] int id, [FromBody] InvoiceTel invoiceTel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != invoiceTel.Id)
            {
                return BadRequest();
            }

            _context.Entry(invoiceTel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceTelExists(id))
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

        // POST: api/InvoiceTelsApi
        //[HttpPost]
        //public async Task<IActionResult> PostInvoiceTel([FromBody] InvoiceTel invoiceTel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.InvoiceTels.Add(invoiceTel);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetInvoiceTel", new { id = invoiceTel.Id }, invoiceTel);
        //}

        // DELETE: api/InvoiceTelsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoiceTel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var invoiceTel = await _context.InvoiceTels.SingleOrDefaultAsync(m => m.Id == id);
            if (invoiceTel == null)
            {
                return NotFound();
            }

            _context.InvoiceTels.Remove(invoiceTel);
            await _context.SaveChangesAsync();

            return Ok(invoiceTel);
        }

        private bool InvoiceTelExists(int id)
        {
            return _context.InvoiceTels.Any(e => e.Id == id);
        }
    }
}