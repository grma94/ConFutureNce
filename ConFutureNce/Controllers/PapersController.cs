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
    public class PapersController : Controller
    {
        private readonly ConFutureNceContext _context;

        public PapersController(ConFutureNceContext context)
        {
            _context = context;
        }

        // GET: Papers
        public async Task<IActionResult> Index()
        {
            var conFutureNceContext = _context.Paper.Include(p => p.Author).Include(p => p.Language).Include(p => p.Reviewer);
            return View(await conFutureNceContext.ToListAsync());
        }

        // GET: Papers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paper = await _context.Paper
                .Include(p => p.Author)
                .Include(p => p.Language)
                .Include(p => p.Reviewer)
                .SingleOrDefaultAsync(m => m.PaperId == id);
            if (paper == null)
            {
                return NotFound();
            }

            return View(paper);
        }

        // GET: Papers/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Set<Author>(), "UserTypeId", "Discriminator");
            ViewData["LanguageId"] = new SelectList(_context.Set<Language>(), "LanguageId", "LanguageId");
            return View();
        }

        // POST: Papers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaperId,TitleENG,TitleORG,Authors,Abstract,OrgName,PaperFile,LanguageId,AuthorId")] Paper paper)
        {
            if (ModelState.IsValid)
            { 
                paper.SubmissionDate = DateTime.Now;
                _context.Add(paper);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Set<Author>(), "UserTypeId", "Discriminator", paper.AuthorId);
            ViewData["LanguageId"] = new SelectList(_context.Set<Language>(), "LanguageId", "LanguageId", paper.LanguageId);
            return View(paper);
        }

        // GET: Papers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paper = await _context.Paper.SingleOrDefaultAsync(m => m.PaperId == id);
            if (paper == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Set<Author>(), "UserTypeId", "Discriminator", paper.AuthorId);
            ViewData["LanguageId"] = new SelectList(_context.Set<Language>(), "LanguageId", "LanguageId", paper.LanguageId);
            ViewData["ReviewerId"] = new SelectList(_context.Set<Reviewer>(), "UserTypeId", "Discriminator", paper.ReviewerId);
            return View(paper);
        }

        // POST: Papers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaperId,TitleENG,TitleORG,Authors,Abstract,OrgName,SubmissionDate,PaperFile,LanguageId,AuthorId,ReviewerId")] Paper paper)
        {
            if (id != paper.PaperId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paper);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaperExists(paper.PaperId))
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
            ViewData["AuthorId"] = new SelectList(_context.Set<Author>(), "UserTypeId", "Discriminator", paper.AuthorId);
            ViewData["LanguageId"] = new SelectList(_context.Set<Language>(), "LanguageId", "LanguageId", paper.LanguageId);
            ViewData["ReviewerId"] = new SelectList(_context.Set<Reviewer>(), "UserTypeId", "Discriminator", paper.ReviewerId);
            return View(paper);
        }

        // GET: Papers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paper = await _context.Paper
                .Include(p => p.Author)
                .Include(p => p.Language)
                .Include(p => p.Reviewer)
                .SingleOrDefaultAsync(m => m.PaperId == id);
            if (paper == null)
            {
                return NotFound();
            }

            return View(paper);
        }

        // POST: Papers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paper = await _context.Paper.SingleOrDefaultAsync(m => m.PaperId == id);
            _context.Paper.Remove(paper);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaperExists(int id)
        {
            return _context.Paper.Any(e => e.PaperId == id);
        }
    }
}
