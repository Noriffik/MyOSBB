using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyOSBB.DAL.Data;
using MyOSBB.DAL.Models.Invoices;

namespace MyOSBB.Controllers
{
    public class InvoiceGazsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoiceGazsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InvoiceGazs
        public async Task<IActionResult> Index()
        {
            return View(await _context.InvoiceGazs.ToListAsync());
        }

        // GET: InvoiceGazs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceGaz = await _context.InvoiceGazs
                .SingleOrDefaultAsync(m => m.Id == id);
            if (invoiceGaz == null)
            {
                return NotFound();
            }

            return View(invoiceGaz);
        }

        // GET: InvoiceGazs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InvoiceGazs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,FlatNumber,InvoiceDate,ProviderName,Payment,ForPeriod,Debt,Overpaid,PrevNumber,CurrentNumber")] InvoiceGaz invoiceGaz)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoiceGaz);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(invoiceGaz);
        }

        // GET: InvoiceGazs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceGaz = await _context.InvoiceGazs.SingleOrDefaultAsync(m => m.Id == id);
            if (invoiceGaz == null)
            {
                return NotFound();
            }
            return View(invoiceGaz);
        }

        // POST: InvoiceGazs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,FlatNumber,InvoiceDate,ProviderName,Payment,ForPeriod,Debt,Overpaid,PrevNumber,CurrentNumber")] InvoiceGaz invoiceGaz)
        {
            if (id != invoiceGaz.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoiceGaz);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceGazExists(invoiceGaz.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(invoiceGaz);
        }

        // GET: InvoiceGazs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceGaz = await _context.InvoiceGazs
                .SingleOrDefaultAsync(m => m.Id == id);
            if (invoiceGaz == null)
            {
                return NotFound();
            }

            return View(invoiceGaz);
        }

        // POST: InvoiceGazs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoiceGaz = await _context.InvoiceGazs.SingleOrDefaultAsync(m => m.Id == id);
            _context.InvoiceGazs.Remove(invoiceGaz);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceGazExists(int id)
        {
            return _context.InvoiceGazs.Any(e => e.Id == id);
        }
    }
}
