﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConFutureNce.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
        public async Task<IActionResult> Index(string sortOrder,string searchString, string currentFilter, int? page)
        {
            // Sorting properties
            ViewData["TitleENGSortParam"] = String.IsNullOrEmpty(sortOrder) ? "TitleENGDesc" : "";
            ViewData["AuthorSortParam"] = sortOrder == "AuthorAsc" ? "AuthorDesc" : "AuthorAsc";
            ViewData["AuthorsSortParam"] = sortOrder == "AuthorsAsc" ? "AuthorsDesc" : "AuthorsAsc";
            ViewData["ReviewerSortParam"] = sortOrder == "ReviewerAsc" ? "ReviewerDesc" : "ReviewerAsc";
            ViewData["StatusSortParam"] = sortOrder == "StatusAsc" ? "StatusDesc" : "StatusAsc";
            // Pagination propertie
            ViewData["CurrentSort"] = sortOrder;
            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;
            int pageSize = 3;
            // Searching propertie
            ViewData["CurrentFilter"] = searchString;
            
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            
            var papers = _context.Paper
                .Include(p => p.Author.ApplicationUser)
                .Include(p => p.PaperKeywords)
                .Include(p => p.Reviewer.ApplicationUser);


            currentUser = _context.ApplicationUser
                .Include(ap => ap.Users)
                .FirstOrDefault(ap => ap.Id == currentUser.Id);

            foreach (var userType in currentUser.Users)
            {

                switch (userType.GetType().ToString())
                {
                    case "ConFutureNce.Models.Author":
                    {
                        var authorId = userType.UserTypeId;

                        IEnumerable<Paper> model = papers
                            .Where(p => p.AuthorId == authorId)
                            .OrderBy(p => p.TitleENG);

                        model = SearchPapers(model, searchString);
                        model = SortPapers(model,sortOrder);
                        model = PaginatedList<Paper>.Create( model, page ?? 1, pageSize);


                        return View("Author", (PaginatedList<Paper>) model);
                    }
                    case "ConFutureNce.Models.Reviewer":
                    {
                        var reviewerId = userType.UserTypeId;
                        IEnumerable<Paper> model = papers
                            .Where(p => p.ReviewerId == reviewerId)
                            .OrderBy(p => p.TitleENG);

                        model = SearchPapers(model, searchString);
                        model = SortPapers(model, sortOrder);
                        model = PaginatedList<Paper>.Create(model, page ?? 1, pageSize);

                            return View("Reviewer", (PaginatedList<Paper>) model);
                    }
                    case "ConFutureNce.Models.ProgrammeCommitteeMember":
                    {
                        IEnumerable<Paper> model =  papers
                            .OrderBy(p => p.TitleENG);

                        model = SearchPapers(model, searchString);
                        model = SortPapers(model, sortOrder);
                        model = PaginatedList<Paper>.Create(model, page ?? 1, pageSize);

                            return View("ProgrammeCommitteeMember", (PaginatedList<Paper>) model);
                    }
                }
            }
            
            return View();
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
        // Need loaded Authors and Reviever object with their ApplicationUser objects
        private IEnumerable<Paper> SortPapers(IEnumerable<Paper> papersToSort, string sortOrder)

        {
            switch (sortOrder)
            {
                case "TitleENGDesc":
                {
                    papersToSort = papersToSort.OrderByDescending(p => p.TitleENG);
                    break;
                }
                case "AuthorDesc":
                {
                    papersToSort = papersToSort.OrderByDescending(p => p.Author.ApplicationUser.Fullname);
                    break;
                }
                case "AuthorAsc":
                {
                    papersToSort = papersToSort.OrderBy(p => p.Author.ApplicationUser.Fullname);
                    break;
                }
                case "AuthorsDesc":
                {
                    papersToSort = papersToSort.OrderByDescending(p => p.Authors);
                    break;
                }
                case "AuthorsAsc":
                {
                    papersToSort = papersToSort.OrderBy(p => p.Authors);
                    break;
                }
                case "ReviewerDesc":
                {
                    papersToSort = papersToSort
                        .OrderByDescending(p => p.Reviewer != null ? p.Reviewer.ApplicationUser.Fullname : string.Empty);
                    break;
                }
                case "ReviewerAsc":
                {
                    papersToSort = papersToSort
                        .OrderBy(p => p.Reviewer != null ? p.Reviewer.ApplicationUser.Fullname : string.Empty);
                    break;
                }
                case "StatusDesc":
                {
                    papersToSort = papersToSort.OrderByDescending(p => p.Status);
                    break;
                }
                case "StatusAsc":
                {
                    papersToSort = papersToSort.OrderBy(p => p.Status);
                    break;
                }
                default:
                {
                    papersToSort = papersToSort.OrderBy(p => p.TitleENG);
                    break;
                }
                    
            }

            return papersToSort;
        }
        // Need loaded Authors and Reviever object with their ApplicationUser objects
        private IEnumerable<Paper> SearchPapers(IEnumerable<Paper> papersToFilter, string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                papersToFilter = papersToFilter
                    .Where(p => p.TitleENG.Contains(searchString)
                                || p.Author.ApplicationUser.Fullname.Contains(searchString)
                                || p.Authors.Contains(searchString)
                                || (p.Reviewer != null ? p.Reviewer.ApplicationUser.Fullname.Contains(searchString) : false)
                                || p.KeywordsToString.Contains(searchString)
                                || p.Status.ToString().Contains(searchString));
            }

            return papersToFilter;
        }
    }
}
