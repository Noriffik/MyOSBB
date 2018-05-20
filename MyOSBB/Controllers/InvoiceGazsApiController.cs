using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyOSBB.DAL.Data;
using MyOSBB.DAL.Interfaces;
using MyOSBB.DAL.Models;
using MyOSBB.DAL.Models.Invoices;

namespace MyOSBB.Controllers
{
    [Produces("application/json")]
    [Route("api/InvoiceGazsApi")]
    public class InvoiceGazsApiController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceGazsApiController(SignInManager<ApplicationUser> signInManager, IUnitOfWork unitOfWork)
        {
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        // GET: api/InvoiceGazsApi
        [HttpGet]
        public IEnumerable<InvoiceGaz> GetInvoiceGazs()
        {
            var result = _unitOfWork.InvoiceGazs.GetDbSet().Include(r => r.Month).Include(r => r.User).ToList();
            return result;
        }

        [HttpPost]
        public IEnumerable<InvoiceGazApi> PostInvoiceGazs(string userName, string password)
        {
            IList<InvoiceGazApi> result = new List<InvoiceGazApi>();
            var user = _unitOfWork.Users.GetDbSet().Where(r => r.UserName == userName).FirstOrDefault();
            if (user != null)
            {
                var pass = _signInManager.PasswordSignInAsync(user, password, false, lockoutOnFailure: false).Result;
                if (pass.Succeeded)
                {
                    var data = _unitOfWork.InvoiceGazs.GetDbSet().Include(r => r.User).Include(r => r.Month).ToList();
                    result = data.Select(r => new InvoiceGazApi() { Id = r.Id, InvoiceDate = r.InvoiceDate, ProviderName = r.ProviderName, Payment = r.Payment, Debt = r.Debt, Overpaid = r.Overpaid, PrevNumber = r.PrevNumber, CurrentNumber = r.CurrentNumber, MonthName = r.Month.Name, FlatNumber = r.User.FlatNumber }).ToList();
                }
            }
            return result;
        }

        // GET: api/InvoiceGazsApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoiceGaz([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var invoiceGaz = await _unitOfWork.InvoiceGazs.GetDbSet().SingleOrDefaultAsync(m => m.Id == id);

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

            _unitOfWork.Entry(invoiceGaz).State = EntityState.Modified;

            try
            {
                await _unitOfWork.SaveChangesAsync();
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
        //[HttpPost]
        //public async Task<IActionResult> PostInvoiceGaz([FromBody] InvoiceGaz invoiceGaz)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.InvoiceGazs.Add(invoiceGaz);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetInvoiceGaz", new { id = invoiceGaz.Id }, invoiceGaz);
        //}

        // DELETE: api/InvoiceGazsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoiceGaz([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var invoiceGaz = await _unitOfWork.InvoiceGazs.GetDbSet().SingleOrDefaultAsync(m => m.Id == id);
            if (invoiceGaz == null)
            {
                return NotFound();
            }

            _unitOfWork.Remove(invoiceGaz);
            await _unitOfWork.SaveChangesAsync();

            return Ok(invoiceGaz);
        }

        private bool InvoiceGazExists(int id)
        {
            return _unitOfWork.InvoiceGazs.GetDbSet().Any(e => e.Id == id);
        }
    }
}