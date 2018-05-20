using System.Linq;
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
    public class InvoiceTelsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public InvoiceTelsController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: InvoiceTels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InvoiceTels.Include(i => i.Month).Include(i => i.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: InvoiceTels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceTel = await _context.InvoiceTels
                .Include(i => i.Month)
                .Include(i => i.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (invoiceTel == null)
            {
                return NotFound();
            }

            return View(invoiceTel);
        }

        // GET: InvoiceTels/Create
        public IActionResult Create()
        {
            ViewData["MonthId"] = new SelectList(_context.Months, "Id", "Name");
            var user = _userManager.GetUserAsync(User).Result;
            ViewData["UserId"] = _context.Users.Where(r => r.Id == user.Id).FirstOrDefaultAsync().Result.Id;
            return View();
        }

        // POST: InvoiceTels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InvoiceDate,ProviderName,Payment,Debt,Overpaid,TelNumber,UserId,MonthId")] InvoiceTel invoiceTel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoiceTel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MonthId"] = new SelectList(_context.Months, "Id", "Name", invoiceTel.MonthId);
            ViewData["UserId"] = invoiceTel.UserId;
            return View(invoiceTel);
        }

        // GET: InvoiceTels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceTel = await _context.InvoiceTels.SingleOrDefaultAsync(m => m.Id == id);
            if (invoiceTel == null)
            {
                return NotFound();
            }
            ViewData["MonthId"] = new SelectList(_context.Months, "Id", "Name", invoiceTel.MonthId);
            ViewData["UserId"] = invoiceTel.UserId;
            return View(invoiceTel);
        }

        // POST: InvoiceTels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InvoiceDate,ProviderName,Payment,Debt,Overpaid,TelNumber,UserId,MonthId")] InvoiceTel invoiceTel)
        {
            if (id != invoiceTel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoiceTel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceTelExists(invoiceTel.Id))
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
            ViewData["MonthId"] = new SelectList(_context.Months, "Id", "Name", invoiceTel.MonthId);
            ViewData["UserId"] = invoiceTel.UserId;
            return View(invoiceTel);
        }

        // GET: InvoiceTels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceTel = await _context.InvoiceTels
                .Include(i => i.Month)
                .Include(i => i.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (invoiceTel == null)
            {
                return NotFound();
            }

            return View(invoiceTel);
        }

        // POST: InvoiceTels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoiceTel = await _context.InvoiceTels.SingleOrDefaultAsync(m => m.Id == id);
            _context.InvoiceTels.Remove(invoiceTel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceTelExists(int id)
        {
            return _context.InvoiceTels.Any(e => e.Id == id);
        }
    }
}
