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
    [Route("api/InvoiceElectroesApi")]
    public class InvoiceElectroesApiController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public InvoiceElectroesApiController(SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _context = context;
        }

        // GET: api/InvoiceElectroesApi
        [HttpGet]
        public IEnumerable<InvoiceElectro> GetInvoiceElectros()
        {
            var result = _context.InvoiceElectros.Include(r => r.Month).Include(r => r.User).ToList();
            return result;
        }

        [HttpPost]
        public IEnumerable<InvoiceElectroApi> PostInvoiceElectros(string userName, string password)
        {
            IList<InvoiceElectroApi> result = new List<InvoiceElectroApi>();
            var user = _context.Users.Where(r => r.UserName == userName).FirstOrDefault();
            if (user != null)
            {
                var pass = _signInManager.PasswordSignInAsync(user, password, false, lockoutOnFailure: false).Result;
                if (pass.Succeeded)
                {
                    var data = _context.InvoiceElectros.Include(r => r.User).Include(r => r.Month).ToList();
                    result = data.Select(r => new InvoiceElectroApi() { Id = r.Id, InvoiceDate = r.InvoiceDate, ProviderName = r.ProviderName, Payment = r.Payment, Debt = r.Debt, Overpaid = r.Overpaid, PrevNumber = r.PrevNumber, CurrentNumber = r.CurrentNumber, MonthName = r.Month.Name, FlatNumber = r.User.FlatNumber }).ToList();
                }
            }
            return result;
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
        //[HttpPost]
        //public async Task<IActionResult> PostInvoiceElectro([FromBody] InvoiceElectro invoiceElectro)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.InvoiceElectros.Add(invoiceElectro);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetInvoiceElectro", new { id = invoiceElectro.Id }, invoiceElectro);
        //}

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