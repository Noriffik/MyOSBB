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
    [Route("api/ContributionsApi")]
    public class ContributionsApiController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;

        public ContributionsApiController(SignInManager<ApplicationUser> signInManager, IUnitOfWork unitOfWork)
        {
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        // GET: api/ContributionsApi
        [HttpGet]
        public IEnumerable<Contribution> GetContributions()
        {
            var result = _unitOfWork.Contributions.GetDbSet().Include(r => r.Month).Include(r => r.User).ToList();
            return result;
        }

        [HttpPost]
        public IEnumerable<ContributionApi> PostContributions(string userName, string password)
        {
            IList<ContributionApi> result = new List<ContributionApi>();
            var user = _unitOfWork.Users.GetDbSet().Where(r => r.UserName == userName).FirstOrDefault();
            if (user != null)
            {
                var pass = _signInManager.PasswordSignInAsync(user, password, false, lockoutOnFailure: false).Result;
                if (pass.Succeeded)
                {
                    var data = _unitOfWork.Contributions.GetDbSet().Include(r => r.User).Include(r => r.Month).ToList();
                    result = data.Select(r => new ContributionApi() { FlatNumber = r.User.FlatNumber, UserName = r.User.UserName, Payment = r.Payment, PaymentDate = r.PaymentDate, MonthName = r.Month.Name }).ToList();
                }
            }
            return result;
        }

        // GET: api/ContributionsApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContribution([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contribution = await _unitOfWork.Contributions.GetDbSet().SingleOrDefaultAsync(m => m.Id == id);

            if (contribution == null)
            {
                return NotFound();
            }

            return Ok(contribution);
        }

        // PUT: api/ContributionsApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContribution([FromRoute] int id, [FromBody] Contribution contribution)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contribution.Id)
            {
                return BadRequest();
            }

            _unitOfWork.Entry(contribution).State = EntityState.Modified;

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContributionExists(id))
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

        // POST: api/ContributionsApi
        //[HttpPost]
        //public async Task<IActionResult> PostContribution([FromBody] Contribution contribution)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Contributions.Add(contribution);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetContribution", new { id = contribution.Id }, contribution);
        //}

        // DELETE: api/ContributionsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContribution([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contribution = await _unitOfWork.Contributions.GetDbSet().SingleOrDefaultAsync(m => m.Id == id);
            if (contribution == null)
            {
                return NotFound();
            }

            _unitOfWork.Contributions.GetDbSet().Remove(contribution);
            await _unitOfWork.SaveChangesAsync();

            return Ok(contribution);
        }

        private bool ContributionExists(int id)
        {
            return _unitOfWork.Contributions.GetDbSet().Any(e => e.Id == id);
        }
    }
}