using System;
using System.Collections.Generic;
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
    public class AnnouncementsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public AnnouncementsController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork /*ApplicationDbContext context*/)
        {
            _userManager = userManager;
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: Announcements
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _unitOfWork.Announcements.GetDbSet().Include(a => a.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Announcements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var announcement = await _unitOfWork.Announcements.GetDbSet()
                .Include(a => a.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (announcement == null)
            {
                return NotFound();
            }

            return View(announcement);
        }

        // GET: Announcements/Create
        public IActionResult Create()
        {
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            var user = _userManager.GetUserAsync(User).Result;
            ViewData["UserId"] = _unitOfWork.Users.GetDbSet().Where(r => r.Id == user.Id).FirstOrDefaultAsync().Result.Id;
            return View();
        }

        // POST: Announcements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,Date,UserId")] Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                //_unitOfWork.Announcements.GetDbSet().Add(announcement);
                _unitOfWork.Announcements.Insert(announcement);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = announcement.UserId;
            return View(announcement);
        }

        // GET: Announcements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var announcement = await _unitOfWork.Announcements.GetDbSet().SingleOrDefaultAsync(m => m.Id == id);
            if (announcement == null)
            {
                return NotFound();
            }
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", announcement.UserId);
            ViewData["UserId"] = announcement.UserId;
            return View(announcement);
        }

        // POST: Announcements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,Date,UserId")] Announcement announcement)
        {
            if (id != announcement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Announcements.Update(announcement);
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnnouncementExists(announcement.Id))
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
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", announcement.UserId);
            ViewData["UserId"] = announcement.UserId;
            return View(announcement);
        }

        // GET: Announcements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var announcement = await _unitOfWork.Announcements.GetDbSet()
                .Include(a => a.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (announcement == null)
            {
                return NotFound();
            }

            return View(announcement);
        }

        // POST: Announcements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var announcement = await _unitOfWork.Announcements.GetDbSet().SingleOrDefaultAsync(m => m.Id == id);
            _unitOfWork.Announcements.GetDbSet().Remove(announcement);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnnouncementExists(int id)
        {
            return _unitOfWork.Announcements.GetDbSet().Any(e => e.Id == id);
        }
    }
}
