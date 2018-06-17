using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConFutureNce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace ConFutureNce.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly ConFutureNceContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReviewsController(ConFutureNceContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        // GET: Reviews
        public async Task<IActionResult> Index()
        {
            var conFutureNceContext = _context.Review.Include(r => r.Paper);

            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            currentUser = _context.ApplicationUser
                .Include(ap => ap.Users)
                .FirstOrDefault(ap => ap.Id == currentUser.Id);

            var reviews = _context.Review
                .Include(p => p.Paper);
            ViewData["UserString"] = currentUser.Users.FirstOrDefault().GetType().ToString();
            foreach (var userType in currentUser.Users)
            {
                switch (userType.GetType().ToString())
                {
                    case "ConFutureNce.Models.Reviewer":
                        {
                            var reviewerId = userType.UserTypeId;
                            IEnumerable<Review> model = reviews
                                .Where(p => p.Paper.ReviewerId == reviewerId)
                                .OrderBy(p => p.Grade);

                            return View("IndexReviewer", model);
                        }
                    case "ConFutureNce.Models.ProgrammeCommitteeMember":
                        {
                            return View("IndexPCM", await conFutureNceContext.ToListAsync());
                        }
                }
            }

            return View("AccessDenied");
        }

        [Authorize]
        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var review = await _context.Review
                .Include(r => r.Paper)
                .SingleOrDefaultAsync(m => m.ReviewId == id);

            if (review == null)
            {
                return View("NotFound");
            }

            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            currentUser = _context.ApplicationUser
                .Include(ap => ap.Users)
                .FirstOrDefault(ap => ap.Id == currentUser.Id);
            ViewData["UserString"] = currentUser.Users.FirstOrDefault().GetType().ToString();
            foreach (var userType in currentUser.Users)
            {
                if (userType.UserTypeId == review.Paper.ReviewerId)
                {
                    return View(review);
                }
            }
            return View("AccessDenied");
        }

        [Authorize]
        // GET: Reviews/Create
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            currentUser = _context.ApplicationUser
                .Include(ap => ap.Users)
                .FirstOrDefault(ap => ap.Id == currentUser.Id);
            ViewData["UserString"] = currentUser.Users.FirstOrDefault().GetType().ToString();
            if (_context.Paper.Find(id) == null)
            {
                return View("NotFound");
            }
            var reviewerId = _context.Paper.Find(id).ReviewerId;
            if (reviewerId == null)
            {

            }
            foreach (var userType in currentUser.Users)
            {
                if (userType.UserTypeId == reviewerId)
                {
                    if (_context.Review.FirstOrDefault(r => r.PaperId == id) == null)
                    {
                        var review = new Review
                        {
                            PaperId = (int)id
                        };
                        return View(review);
                    }
                }
            }
            return RedirectToAction("Details", "Papers", new { id });
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReviewId,Problems,WhyProblems,Solution,Achievements,NotMentioned,Grade,GeneralComments,PaperId")] Review review)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            currentUser = _context.ApplicationUser
                .Include(ap => ap.Users)
                .FirstOrDefault(ap => ap.Id == currentUser.Id);

            var reviewerId = _context.Paper.Find(review.PaperId).ReviewerId;

            foreach (var userType in currentUser.Users)
            {
                if (userType.UserTypeId == reviewerId)
                {
                    if (ModelState.IsValid)
                    {
                        _context.Paper.Find(review.PaperId).Status = Paper.ProcessStatus.Reviewed;
                        review.Date = DateTime.Now;
                        _context.Add(review);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    return View(review);
                }
            }
            return View("AccessDenied");
        }
    }
}
