using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyOSBB.DAL.Data;
using MyOSBB.DAL.Interfaces;
using MyOSBB.DAL.Models;

namespace MyOSBB.Controllers
{
    [Produces("application/json")]
    [Route("api/AnnouncementsApi")]
    public class AnnouncementsApiController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public AnnouncementsApiController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IPasswordHasher<ApplicationUser> passwordHash, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _passwordHasher = passwordHash;
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/AnnouncementsApi
        [HttpGet]
        public IEnumerable<Announcement> GetAnnouncements()
        {
            var result = _unitOfWork.Announcements.GetDbSet().Include(r => r.User).ToList();
            //return _context.Announcements;
            return result;
        }

        // POST: api/AnnouncementsApi
        [HttpPost]
        public IEnumerable<AnnouncementApi> PostAnnouncements(string userName, string password)
        {
            IList<AnnouncementApi> result = new List<AnnouncementApi>();
            var user = _unitOfWork.Users.GetDbSet().Where(r => r.UserName == userName).FirstOrDefault();
            if (user != null)
            {
                var pass = _signInManager.PasswordSignInAsync(user, password, false, lockoutOnFailure: false).Result;
                if (pass.Succeeded)
                {
                    var data = _unitOfWork.Announcements.GetDbSet().Include(r => r.User).ToList();
                    result = data.Select(r => new AnnouncementApi() { Id = r.Id, Title = r.Title, Date = r.Date, Content = r.Content, UserName = r.User.UserName }).ToList();
                }
            }
            return result;
        }

        // GET: api/AnnouncementsApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnnouncement([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var announcement = await _unitOfWork.Announcements.GetDbSet().SingleOrDefaultAsync(m => m.Id == id);

            if (announcement == null)
            {
                return NotFound();
            }

            return Ok(announcement);
        }

        // PUT: api/AnnouncementsApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnnouncement([FromRoute] int id, [FromBody] Announcement announcement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != announcement.Id)
            {
                return BadRequest();
            }

            _unitOfWork.GetEntry(announcement).State = EntityState.Modified;

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnnouncementExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AnnouncementsApi
        //[HttpPost]
        //public async Task<IActionResult> PostAnnouncement([FromBody] Announcement announcement)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Announcements.Add(announcement);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetAnnouncement", new { id = announcement.Id }, announcement);
        //}

        // DELETE: api/AnnouncementsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var announcement = await _unitOfWork.Announcements.GetDbSet().SingleOrDefaultAsync(m => m.Id == id);
            if (announcement == null)
            {
                return NotFound();
            }

            _unitOfWork.Announcements.GetDbSet().Remove(announcement);
            await _unitOfWork.SaveChangesAsync();

            return Ok(announcement);
        }

        private bool AnnouncementExists(int id)
        {
            return _unitOfWork.Announcements.GetDbSet().Any(e => e.Id == id);
        }
    }
}