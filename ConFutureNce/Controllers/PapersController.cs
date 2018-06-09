using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConFutureNce.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Identity;

namespace ConFutureNce.Controllers
{
    public class PapersController : Controller
    {
        private readonly ConFutureNceContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PapersController(ConFutureNceContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Papers
        public async Task<IActionResult> Index()
        {

            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            
            var papers = _context.Paper
                .Include(p => p.Author.ApplicationUser)
                .Include(p => p.PaperKeywords)
                .Include(p => p.Reviewer.ApplicationUser);

            // assumed one user one role
            var firstRole = (await _userManager.GetRolesAsync(currentUser)).First();
            switch (firstRole)
            {
                case "Author":
                {
                    var authorId = _context.Author.FirstOrDefault(a => a.ApplicationUserId == currentUser.Id).UserTypeId;
                        
                    var model = await papers.Where(p => p.AuthorId == authorId)
                        .OrderBy(p => p.TitleENG)
                        .ToListAsync();
                    
                
                    return View("Author", model);
                }
                case "Reviewer":
                {
                    var reviewerId = _context.Reviewer
                        .FirstOrDefaultAsync(a => a.ApplicationUserId == currentUser.Id)
                        .Id;
                    var model = await _context.Paper
                        .Include(p => p.PaperKeywords)
                        .Include(p => p.Reviewer)
                        .Where(p => p.ReviewerId == reviewerId)
                        .OrderBy(p => p.TitleENG)
                        .ToListAsync();

                        return View("Reviewer", model);
                }
                case "ProgrammeCommitteeMember":
                {
                    var reviewerId = _context.ProgrammeCommitteeMember
                        .FirstOrDefaultAsync(a => a.ApplicationUserId == currentUser.Id)
                        .Id;
                    var model = await _context.Paper
                        .Include(p => p.PaperKeywords)
                        .Include(p => p.Reviewer)
                        .OrderBy(p => p.TitleENG)
                        .ToListAsync();


                        return View("ProgrammeCommitteeMember", model);
                }
                default:
                    return View();
            }
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
            ICollection<Language> languageList = new List<Language>();
            languageList = (from language in _context.Language select language).ToList();
            ViewBag.ListofLanguages = languageList;
            ICollection<Author> authorsList = new List<Author>();
            authorsList = (from author in _context.Author select author).ToList();
            ViewBag.ListofAuthors = authorsList;
            //ViewBag.ViewData["AuthorId"] = new SelectList(_context.Set<Author>(), "UserTypeId", "Discriminator");
            //ViewData["LanguageId"] = new SelectList(_context.Set<Language>(), "LanguageId", "LanguageId");
            return View();
        }

        // POST: Papers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaperId,TitleENG,TitleORG,Authors,Abstract,OrgName,LanguageId,AuthorId")] Paper paper, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    paper.PaperFile = memoryStream.ToArray();
                }
                paper.SubmissionDate = DateTime.Now;
                paper.Status = 0;
                _context.Add(paper);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ICollection<Language> languageList = new List<Language>();
            languageList = (from language in _context.Language select language).ToList();
            ViewBag.ListofLanguages = languageList;
            ICollection<Author> authorsList = new List<Author>();
            authorsList = (from author in _context.Author select author).ToList();
            ViewBag.ListofAuthors = authorsList;
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

        //public string KeywordsToString(IEnumerable<PaperKeyword> PaperKeywords)
        //{
        //     return string.Join(", ", PaperKeywords.);
        //}
    }
}
