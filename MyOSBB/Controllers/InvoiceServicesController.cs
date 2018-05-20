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
    public class InvoiceServicesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public InvoiceServicesController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: InvoiceServices
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InvoiceServices.Include(i => i.Month).Include(i => i.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: InvoiceServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceService = await _context.InvoiceServices
                .Include(i => i.Month)
                .Include(i => i.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (invoiceService == null)
            {
                return NotFound();
            }

            return View(invoiceService);
        }

        // GET: InvoiceServices/Create
        public IActionResult Create()
        {
            ViewData["MonthId"] = new SelectList(_context.Months, "Id", "Name");
            var user = _userManager.GetUserAsync(User).Result;
            ViewData["UserId"] = _context.Users.Where(r => r.Id == user.Id).FirstOrDefaultAsync().Result.Id;
            return View();
        }

        // POST: InvoiceServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InvoiceDate,ProviderName,Payment,Debt,Overpaid,UserId,MonthId")] InvoiceService invoiceService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoiceService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MonthId"] = new SelectList(_context.Months, "Id", "Name", invoiceService.MonthId);
            ViewData["UserId"] = invoiceService.UserId;
            return View(invoiceService);
        }

        // GET: InvoiceServices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceService = await _context.InvoiceServices.SingleOrDefaultAsync(m => m.Id == id);
            if (invoiceService == null)
            {
                return NotFound();
            }
            ViewData["MonthId"] = new SelectList(_context.Months, "Id", "Name", invoiceService.MonthId);
            ViewData["UserId"] = invoiceService.UserId;
            return View(invoiceService);
        }

        // POST: InvoiceServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InvoiceDate,ProviderName,Payment,Debt,Overpaid,UserId,MonthId")] InvoiceService invoiceService)
        {
            if (id != invoiceService.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoiceService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceServiceExists(invoiceService.Id))
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
            ViewData["MonthId"] = new SelectList(_context.Months, "Id", "Name", invoiceService.MonthId);
            ViewData["UserId"] = invoiceService.UserId;
            return View(invoiceService);
        }

        // GET: InvoiceServices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceService = await _context.InvoiceServices
                .Include(i => i.Month)
                .Include(i => i.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (invoiceService == null)
            {
                return NotFound();
            }

            return View(invoiceService);
        }

        // POST: InvoiceServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoiceService = await _context.InvoiceServices.SingleOrDefaultAsync(m => m.Id == id);
            _context.InvoiceServices.Remove(invoiceService);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceServiceExists(int id)
        {
            return _context.InvoiceServices.Any(e => e.Id == id);
        }
    }
}
