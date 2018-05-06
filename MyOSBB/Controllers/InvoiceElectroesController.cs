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
    public class InvoiceElectroesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoiceElectroesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InvoiceElectroes
        public async Task<IActionResult> Index()
        {
            return View(await _context.InvoiceElectros.ToListAsync());
        }

        // GET: InvoiceElectroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceElectro = await _context.InvoiceElectros
                .SingleOrDefaultAsync(m => m.Id == id);
            if (invoiceElectro == null)
            {
                return NotFound();
            }

            return View(invoiceElectro);
        }

        // GET: InvoiceElectroes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InvoiceElectroes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,FlatNumber,InvoiceDate,ProviderName,Payment,ForPeriod,Debt,Overpaid,PrevNumber,CurrentNumber")] InvoiceElectro invoiceElectro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoiceElectro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(invoiceElectro);
        }

        // GET: InvoiceElectroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceElectro = await _context.InvoiceElectros.SingleOrDefaultAsync(m => m.Id == id);
            if (invoiceElectro == null)
            {
                return NotFound();
            }
            return View(invoiceElectro);
        }

        // POST: InvoiceElectroes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,FlatNumber,InvoiceDate,ProviderName,Payment,ForPeriod,Debt,Overpaid,PrevNumber,CurrentNumber")] InvoiceElectro invoiceElectro)
        {
            if (id != invoiceElectro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoiceElectro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceElectroExists(invoiceElectro.Id))
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
            return View(invoiceElectro);
        }

        // GET: InvoiceElectroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceElectro = await _context.InvoiceElectros
                .SingleOrDefaultAsync(m => m.Id == id);
            if (invoiceElectro == null)
            {
                return NotFound();
            }

            return View(invoiceElectro);
        }

        // POST: InvoiceElectroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoiceElectro = await _context.InvoiceElectros.SingleOrDefaultAsync(m => m.Id == id);
            _context.InvoiceElectros.Remove(invoiceElectro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceElectroExists(int id)
        {
            return _context.InvoiceElectros.Any(e => e.Id == id);
        }
    }
}
