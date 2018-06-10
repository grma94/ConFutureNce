﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConFutureNce.Models;
using Microsoft.AspNetCore.Identity;

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

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Review
                .Include(r => r.Paper)
                .SingleOrDefaultAsync(m => m.ReviewId == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Reviews/Create
        public IActionResult Create(int id)
        {
            var review = new Review
            {
                PaperId = id
            };
            return View(review);
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReviewId,Problems,WhyProblems,Solution,Achievements,NotMentioned,Grade,GeneralComments,PaperId")] Review review)
        {
            if (ModelState.IsValid)
            {
                review.Date = DateTime.Now;
                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(review);
        }
    }
}
