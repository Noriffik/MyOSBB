using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyOSBB.DAL.Data;
using MyOSBB.DAL.Models;

namespace MyOSBB.Controllers
{
    [Authorize(Roles = "Users")]
    public class ContributionsController : Controller
    {        
        private UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ContributionsController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Contributions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Contributions.Include(c => c.Month).Include(c => c.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Contributions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contribution = await _context.Contributions
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
            ViewData["MonthId"] = new SelectList(_context.Months, "Id", "Name");
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");

            var user = _userManager.GetUserAsync(User).Result;
            ViewData["UserId"] = _context.Users.Where(r => r.Id == user.Id).FirstOrDefaultAsync().Result.Id;
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
                _context.Add(contribution);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MonthId"] = new SelectList(_context.Months, "Id", "Name", contribution.MonthId);
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

            var contribution = await _context.Contributions.SingleOrDefaultAsync(m => m.Id == id);
            if (contribution == null)
            {
                return NotFound();
            }
            ViewData["MonthId"] = new SelectList(_context.Months, "Id", "Name", contribution.MonthId);
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
                    _context.Update(contribution);
                    await _context.SaveChangesAsync();
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
            ViewData["MonthId"] = new SelectList(_context.Months, "Id", "Name", contribution.MonthId);
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

            var contribution = await _context.Contributions
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
            var contribution = await _context.Contributions.SingleOrDefaultAsync(m => m.Id == id);
            _context.Contributions.Remove(contribution);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContributionExists(int id)
        {
            return _context.Contributions.Any(e => e.Id == id);
        }
    }
}
