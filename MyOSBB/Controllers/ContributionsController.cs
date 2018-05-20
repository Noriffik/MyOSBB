using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyOSBB.DAL.Data;
using MyOSBB.DAL.Interfaces;
using MyOSBB.DAL.Models;

namespace MyOSBB.Controllers
{
    [Authorize(Roles = "Users")]
    public class ContributionsController : Controller
    {        
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public ContributionsController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: Contributions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _unitOfWork.Contributions.GetDbSet().Include(c => c.Month).Include(c => c.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Contributions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contribution = await _unitOfWork.Contributions.GetDbSet()
                .Include(c => c.Month)
                .Include(c => c.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (contribution == null)
            {
                return NotFound();
            }

            return View(contribution);
        }

        // GET: Contributions/Create
        public IActionResult Create()
        {
            ViewData["MonthId"] = new SelectList(_unitOfWork.Months.GetDbSet(), "Id", "Name");
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");

            var user = _userManager.GetUserAsync(User).Result;
            ViewData["UserId"] = _unitOfWork.Users.GetDbSet().Where(r => r.Id == user.Id).FirstOrDefaultAsync().Result.Id;
            return View();
        }

        // POST: Contributions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Payment,PaymentDate,UserId,MonthId")] Contribution contribution)
        {
            if (ModelState.IsValid)
            {
                //_unitOfWork.Contributions.GetDbSet().Add(contribution);
                _unitOfWork.Contributions.Insert(contribution);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MonthId"] = new SelectList(_unitOfWork.Months.GetDbSet(), "Id", "Name", contribution.MonthId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", contribution.UserId);
            ViewData["UserId"] = contribution.UserId;
            return View(contribution);
        }

        // GET: Contributions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contribution = await _unitOfWork.Contributions.GetDbSet().SingleOrDefaultAsync(m => m.Id == id);
            if (contribution == null)
            {
                return NotFound();
            }
            ViewData["MonthId"] = new SelectList(_unitOfWork.Months.GetDbSet(), "Id", "Name", contribution.MonthId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", contribution.UserId);
            ViewData["UserId"] = contribution.UserId;
            return View(contribution);
        }

        // POST: Contributions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Payment,PaymentDate,UserId,MonthId")] Contribution contribution)
        {
            if (id != contribution.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Update(contribution);
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContributionExists(contribution.Id))
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
            ViewData["MonthId"] = new SelectList(_unitOfWork.Months.GetDbSet(), "Id", "Name", contribution.MonthId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", contribution.UserId);
            ViewData["UserId"] = contribution.UserId;
            return View(contribution);
        }

        // GET: Contributions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contribution = await _unitOfWork.Contributions.GetDbSet()
                .Include(c => c.Month)
                .Include(c => c.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (contribution == null)
            {
                return NotFound();
            }

            return View(contribution);
        }

        // POST: Contributions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contribution = await _unitOfWork.Contributions.GetDbSet().SingleOrDefaultAsync(m => m.Id == id);
            _unitOfWork.Contributions.GetDbSet().Remove(contribution);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContributionExists(int id)
        {
            return _unitOfWork.Contributions.GetDbSet().Any(e => e.Id == id);
        }
    }
}
