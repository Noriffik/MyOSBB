using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyOSBB.DAL.Interfaces;
using MyOSBB.DAL.Models;
using MyOSBB.DAL.Models.Invoices;

namespace MyOSBB.Controllers
{
    [Authorize(Roles = "Users")]
    public class InvoiceGazsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceGazsController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        // GET: InvoiceGazs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _unitOfWork.InvoiceGazs.GetDbSet().Include(i => i.Month).Include(i => i.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: InvoiceGazs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceGaz = await _unitOfWork.InvoiceGazs.GetDbSet()
                .Include(i => i.Month)
                .Include(i => i.User)
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
            ViewData["MonthId"] = new SelectList(_unitOfWork.Months.GetDbSet(), "Id", "Name");
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            var user = _userManager.GetUserAsync(User).Result;
            ViewData["UserId"] = _unitOfWork.Users.GetDbSet().Where(r => r.Id == user.Id).FirstOrDefaultAsync().Result.Id;
            return View();
        }

        // POST: InvoiceGazs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InvoiceDate,ProviderName,Payment,Debt,Overpaid,PrevNumber,CurrentNumber,UserId,MonthId")] InvoiceGaz invoiceGaz)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Add(invoiceGaz);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MonthId"] = new SelectList(_unitOfWork.Months.GetDbSet(), "Id", "Name", invoiceGaz.MonthId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", invoiceGaz.UserId);
            ViewData["UserId"] = invoiceGaz.UserId;
            return View(invoiceGaz);
        }

        // GET: InvoiceGazs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceGaz = await _unitOfWork.InvoiceGazs.GetDbSet().SingleOrDefaultAsync(m => m.Id == id);
            if (invoiceGaz == null)
            {
                return NotFound();
            }
            ViewData["MonthId"] = new SelectList(_unitOfWork.Months.GetDbSet(), "Id", "Name", invoiceGaz.MonthId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", invoiceGaz.UserId);
            ViewData["UserId"] = invoiceGaz.UserId;
            return View(invoiceGaz);
        }

        // POST: InvoiceGazs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InvoiceDate,ProviderName,Payment,Debt,Overpaid,PrevNumber,CurrentNumber,UserId,MonthId")] InvoiceGaz invoiceGaz)
        {
            if (id != invoiceGaz.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Update(invoiceGaz);
                    await _unitOfWork.SaveChangesAsync();
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
            ViewData["MonthId"] = new SelectList(_unitOfWork.Months.GetDbSet(), "Id", "Name", invoiceGaz.MonthId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", invoiceGaz.UserId);
            ViewData["UserId"] = invoiceGaz.UserId;
            return View(invoiceGaz);
        }

        // GET: InvoiceGazs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceGaz = await _unitOfWork.InvoiceGazs.GetDbSet()
                .Include(i => i.Month)
                .Include(i => i.User)
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
            var invoiceGaz = await _unitOfWork.InvoiceGazs.GetDbSet().SingleOrDefaultAsync(m => m.Id == id);
            _unitOfWork.Remove(invoiceGaz);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceGazExists(int id)
        {
            return _unitOfWork.InvoiceGazs.GetDbSet().Any(e => e.Id == id);
        }
    }
}
