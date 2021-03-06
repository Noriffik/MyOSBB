﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyOSBB.DAL.Data;
using MyOSBB.DAL.Models;
using MyOSBB.DAL.Models.Invoices;

namespace MyOSBB.Controllers
{
    [Authorize(Roles = "Users")]
    public class InvoiceWatersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public InvoiceWatersController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: InvoiceWaters
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InvoiceWaters.Include(i => i.Month).Include(i => i.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: InvoiceWaters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceWater = await _context.InvoiceWaters
                .Include(i => i.Month)
                .Include(i => i.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (invoiceWater == null)
            {
                return NotFound();
            }

            return View(invoiceWater);
        }

        // GET: InvoiceWaters/Create
        public IActionResult Create()
        {
            ViewData["MonthId"] = new SelectList(_context.Months, "Id", "Name");
            var user = _userManager.GetUserAsync(User).Result;
            ViewData["UserId"] = _context.Users.Where(r => r.Id == user.Id).FirstOrDefaultAsync().Result.Id;
            return View();
        }

        // POST: InvoiceWaters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InvoiceDate,ProviderName,Payment,Debt,Overpaid,UserId,MonthId")] InvoiceWater invoiceWater)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoiceWater);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MonthId"] = new SelectList(_context.Months, "Id", "Name", invoiceWater.MonthId);
            ViewData["UserId"] = invoiceWater.UserId;
            return View(invoiceWater);
        }

        // GET: InvoiceWaters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceWater = await _context.InvoiceWaters.SingleOrDefaultAsync(m => m.Id == id);
            if (invoiceWater == null)
            {
                return NotFound();
            }
            ViewData["MonthId"] = new SelectList(_context.Months, "Id", "Name", invoiceWater.MonthId);
            ViewData["UserId"] = invoiceWater.UserId;
            return View(invoiceWater);
        }

        // POST: InvoiceWaters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InvoiceDate,ProviderName,Payment,Debt,Overpaid,UserId,MonthId")] InvoiceWater invoiceWater)
        {
            if (id != invoiceWater.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoiceWater);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceWaterExists(invoiceWater.Id))
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
            ViewData["MonthId"] = new SelectList(_context.Months, "Id", "Name", invoiceWater.MonthId);
            ViewData["UserId"] = invoiceWater.UserId;
            return View(invoiceWater);
        }

        // GET: InvoiceWaters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceWater = await _context.InvoiceWaters
                .Include(i => i.Month)
                .Include(i => i.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (invoiceWater == null)
            {
                return NotFound();
            }

            return View(invoiceWater);
        }

        // POST: InvoiceWaters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoiceWater = await _context.InvoiceWaters.SingleOrDefaultAsync(m => m.Id == id);
            _context.InvoiceWaters.Remove(invoiceWater);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceWaterExists(int id)
        {
            return _context.InvoiceWaters.Any(e => e.Id == id);
        }
    }
}
