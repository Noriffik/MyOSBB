using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyOSBB.DAL.Models;
using MyOSBB.DAL.Data;

namespace MyOSBB.Controllers
{
    [Authorize(Roles = "Admins,Users")]
    public class ContributionsController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;

        public ContributionsController(ApplicationDbContext context)
        {
            _context = context;
            _unitOfWork = new UnitOfWork(_context);
        }

        // GET: Contributions
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.Contributions.Get().ToListAsync());
        }

        // GET: Contributions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contribution = await _unitOfWork.Contributions.Get()
                .SingleOrDefaultAsync(m => m.Id == id);
            if (contribution == null)
            {
                return NotFound();
            }

            return View(contribution);
        }

        // GET: Contributions/Create
        [Authorize(Roles = "Admins")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contributions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admins")]
        public async Task<IActionResult> Create([Bind("Id,FlatNumber,Sum")] Contribution contribution)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Contributions.Get().Add(contribution);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contribution);
        }

        // GET: Contributions/Edit/5
        [Authorize(Roles = "Admins")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contribution = await _unitOfWork.Contributions.Get().SingleOrDefaultAsync(m => m.Id == id);
            if (contribution == null)
            {
                return NotFound();
            }
            return View(contribution);
        }

        // POST: Contributions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admins")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FlatNumber,Sum")] Contribution contribution)
        {
            if (id != contribution.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Contributions.Update(contribution);
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
            return View(contribution);
        }

        // GET: Contributions/Delete/5
        [Authorize(Roles = "Admins")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contribution = await _unitOfWork.Contributions.Get()
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
        [Authorize(Roles = "Admins")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contribution = await _unitOfWork.Contributions.Get().SingleOrDefaultAsync(m => m.Id == id);
            _unitOfWork.Contributions.Get().Remove(contribution);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContributionExists(int id)
        {
            return _unitOfWork.Contributions.Get().Any(e => e.Id == id);
        }
    }
}
