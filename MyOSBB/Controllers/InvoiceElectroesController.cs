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
    public class InvoiceElectroesController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceElectroesController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        // GET: InvoiceElectroes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _unitOfWork.InvoiceElectroes.GetDbSet().Include(i => i.Month).Include(i => i.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: InvoiceElectroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceElectro = await _unitOfWork.InvoiceElectroes.GetDbSet()
                .Include(i => i.Month)
                .Include(i => i.User)
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
            ViewData["MonthId"] = new SelectList(_unitOfWork.Months.GetDbSet(), "Id", "Name");
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            var user = _userManager.GetUserAsync(User).Result;
            ViewData["UserId"] = _unitOfWork.Users.GetDbSet().Where(r => r.Id == user.Id).FirstOrDefaultAsync().Result.Id;
            return View();
        }

        // POST: InvoiceElectroes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InvoiceDate,ProviderName,Payment,Debt,Overpaid,PrevNumber,CurrentNumber,UserId,MonthId")] InvoiceElectro invoiceElectro)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Add(invoiceElectro);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MonthId"] = new SelectList(_unitOfWork.Months.GetDbSet(), "Id", "Name", invoiceElectro.MonthId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", invoiceElectro.UserId);
            ViewData["UserId"] = invoiceElectro.UserId;
            return View(invoiceElectro);
        }

        // GET: InvoiceElectroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceElectro = await _unitOfWork.InvoiceElectroes.GetDbSet().SingleOrDefaultAsync(m => m.Id == id);
            if (invoiceElectro == null)
            {
                return NotFound();
            }
            ViewData["MonthId"] = new SelectList(_unitOfWork.Months.GetDbSet(), "Id", "Name", invoiceElectro.MonthId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", invoiceElectro.UserId);
            ViewData["UserId"] = invoiceElectro.UserId;
            return View(invoiceElectro);
        }

        // POST: InvoiceElectroes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InvoiceDate,ProviderName,Payment,Debt,Overpaid,PrevNumber,CurrentNumber,UserId,MonthId")] InvoiceElectro invoiceElectro)
        {
            if (id != invoiceElectro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Update(invoiceElectro);
                    await _unitOfWork.SaveChangesAsync();
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
            ViewData["MonthId"] = new SelectList(_unitOfWork.Months.GetDbSet(), "Id", "Name", invoiceElectro.MonthId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", invoiceElectro.UserId);
            ViewData["UserId"] = invoiceElectro.UserId;
            return View(invoiceElectro);
        }

        // GET: InvoiceElectroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceElectro = await _unitOfWork.InvoiceElectroes.GetDbSet()
                .Include(i => i.Month)
                .Include(i => i.User)
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
            var invoiceElectro = await _unitOfWork.InvoiceElectroes.GetDbSet().SingleOrDefaultAsync(m => m.Id == id);
            _unitOfWork.InvoiceElectroes.GetDbSet().Remove(invoiceElectro);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceElectroExists(int id)
        {
            return _unitOfWork.InvoiceElectroes.GetDbSet().Any(e => e.Id == id);
        }
    }
}
