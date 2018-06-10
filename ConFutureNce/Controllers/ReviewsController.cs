using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConFutureNce.Models;

namespace ConFutureNce.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly ConFutureNceContext _context;

        public ReviewsController(ConFutureNceContext context)
        {
            _context = context;
        }

        // GET: Reviews
        public async Task<IActionResult> Index()
        {
            var conFutureNceContext = _context.Review.Include(r => r.Paper);
            return View(await conFutureNceContext.ToListAsync());
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
