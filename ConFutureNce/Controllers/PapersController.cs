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
using System.Data.SqlClient;
using ConFutureNce.Models.PaperViewModel;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? page)
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
            int pageSize = 10;
            // Searching propertie
            ViewData["CurrentFilter"] = searchString;

            var currentUserId = _userManager.GetUserId(HttpContext.User);

            var papers = _context.Paper
                .Include(p => p.Author.ApplicationUser)
                .Include(p => p.PaperKeywords)
                .Include(p => p.Reviewer.ApplicationUser)
                .Include(p => p.Payment);


            var currentUser = _context.ApplicationUser
                .Include(ap => ap.Users)
                .FirstOrDefault(ap => ap.Id == currentUserId);
            ViewData["UserString"] = currentUser.Users.FirstOrDefault().GetType().ToString();

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

                        // Delete unpaid papers
                        var toDelete = model.Where(p => p.Payment == null);
                        _context.Paper.RemoveRange(toDelete);
                        await _context.SaveChangesAsync();

                        model = SearchPapers(model, searchString);
                        model = SortPapers(model,sortOrder);
                        model = PaginatedList<Paper>.Create( model, page ?? 1, pageSize);


                            return View("Author", (PaginatedList<Paper>)model);
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

                            return View("Reviewer", (PaginatedList<Paper>)model);
                        }
                    case "ConFutureNce.Models.ProgrammeCommitteeMember":
                        {
                            IEnumerable<Paper> model = papers
                                .OrderBy(p => p.TitleENG);

                            model = SearchPapers(model, searchString);
                            model = SortPapers(model, sortOrder);
                            model = PaginatedList<Paper>.Create(model, page ?? 1, pageSize);

                            return View("ProgrammeCommitteeMember", (PaginatedList<Paper>) model);
                    }
                }
            }

            return RedirectToAction("AccessDenied", "Account", null);
        }

        // GET: Papers/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var paper = await _context.Paper
                .Include(p => p.Author.ApplicationUser)
                .Include(p => p.Language)
                .Include(p => p.PaperKeywords)
                .Include(p => p.Reviewer.ApplicationUser)
                .Include(p => p.Review)
                .SingleOrDefaultAsync(m => m.PaperId == id);
            if (paper == null)
            {
                return View("NotFound");
            }
            
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            var currentUser = _context.ApplicationUser
                .Include(ap => ap.Users)
                .FirstOrDefault(ap => ap.Id == currentUserId);
            ViewData["UserString"] = currentUser.Users.FirstOrDefault().GetType().ToString();
            foreach (var userType in currentUser.Users)
            {

                switch (userType.GetType().ToString())
                {
                    case "ConFutureNce.Models.Reviewer":
                        {
                            if (paper.ReviewerId != userType.UserTypeId)
                            {
                                return RedirectToAction("AccessDenied", "Account", null);
                            }
                            return View("DetailsReviewer", paper);
                        }
                    case "ConFutureNce.Models.Author":
                        {
                            if (paper.AuthorId != userType.UserTypeId)
                            {
                                return RedirectToAction("AccessDenied", "Account", null);
                            }
                            return View(paper);
                        }
                }
            }
            return View(paper);
        }

        // GET: Papers/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var currentUserId =  _userManager.GetUserId(HttpContext.User);
            var currentUser = _context.ApplicationUser
                .Include(ap => ap.Users)
                .FirstOrDefault(ap => ap.Id == currentUserId);
            ViewData["UserString"] = currentUser.Users.FirstOrDefault().GetType().ToString();

            foreach (var userType in currentUser.Users)
            {
                if (userType.GetType().ToString() == "ConFutureNce.Models.Author")
                {
                    var paperCount = _context.Paper
                        .Count(p => p.AuthorId == currentUser.Users
                                        .First(u => u.GetType() == typeof(Author))
                                        .UserTypeId);
                    if (paperCount >= 10)
                        break;
                    ICollection<Language> languageList = new List<Language>();
                    languageList = (from language in _context.Language select language).ToList();
                    ViewBag.ListofLanguages = languageList;
                    return View();
                }
            }
            return RedirectToAction("Index");
        }

        // POST: Papers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaperId,TitleENG,TitleORG,Authors,Abstract,OrgName,LanguageId,PaperKeywords")] ViewModels.PaperPaperKeyworsViewModel paperPaperKeyword, IFormFile file)
        {
            
            var userId = _userManager.GetUserId(HttpContext.User);

            var user = _context.ApplicationUser
                .Include(ap => ap.Users)
                .FirstOrDefault(ap => ap.Id == userId);

            foreach (var userType in user.Users)
            {
                if (userType.GetType().ToString() == "ConFutureNce.Models.Author")
                {

                    if (ModelState.IsValid & file != null)
                    {
                        var paper = new Paper
                        {
                            Abstract = paperPaperKeyword.Abstract,
                            TitleENG = paperPaperKeyword.TitleENG,
                            TitleORG = paperPaperKeyword.TitleORG,
                            Authors = paperPaperKeyword.Authors,
                            OrgName = paperPaperKeyword.OrgName,
                            LanguageId = paperPaperKeyword.LanguageId
                        };

                        var userTypeId = user.Users.First().UserTypeId;
                        paper.AuthorId = userTypeId;
                        if (paperPaperKeyword.PaperKeywords != null)
                        {
                            var paperKeywordsTableWithRepeats = paperPaperKeyword.PaperKeywords.Split(",");
                            for (int i = 0; i < paperKeywordsTableWithRepeats.Length; i++)
                            {
                                paperKeywordsTableWithRepeats[i] = paperKeywordsTableWithRepeats[i].Trim();
                            }
                            paperKeywordsTableWithRepeats = paperKeywordsTableWithRepeats.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                            var paperKeywordsTable = paperKeywordsTableWithRepeats.Distinct().ToArray();
                            List<PaperKeyword> ppk = new List<PaperKeyword>();

                            foreach (string keyword in paperKeywordsTable)
                            {
                                var paperKeywords = new PaperKeyword
                                {
                                    KeyWord = keyword,
                                    Paper = paper
                                };
                                ppk.Add(paperKeywords);
                            }
                            paper.PaperKeywords = ppk;
                        }
                        using (var memoryStream = new MemoryStream())
                        {
                            await file.CopyToAsync(memoryStream);
                            paper.PaperFile = memoryStream.ToArray();
                        }
                        paper.SubmissionDate = DateTime.Now;
                        paper.Status = 0;
                        _context.Add(paper);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Index", "Payments", new PaymentViewModel
                        {
                            UserName = user.Fullname,
                            BillingAddress = user.Address,
                            PaperId = paper.PaperId
                        });
                    }
                    ICollection<Language> languageList = new List<Language>();
                    languageList = (from language in _context.Language select language).ToList();
                    ViewBag.ListofLanguages = languageList;
                    return View(paperPaperKeyword);
                }
            }

            return RedirectToAction("Index");
        }
        [Authorize]
        public async Task<IActionResult> AssignReviewer()
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            var currentUser = _context.ApplicationUser
                .Include(ap => ap.Users)
                .FirstOrDefault(ap => ap.Id == currentUserId);
            ViewData["UserString"] = currentUser.Users.FirstOrDefault().GetType().ToString();
            foreach (var userType in currentUser.Users)
            {
                if (userType.GetType().ToString() == "ConFutureNce.Models.ProgrammeCommitteeMember")
                {
                    IEnumerable<Paper> model = _context.Paper
                .Include(p => p.Author.ApplicationUser)
                .Include(p => p.PaperKeywords)
                .Include(p => p.Reviewer.ApplicationUser)
                .Include(p => p.Language.ReviewersFirst)
                .Include(p => p.Language.ReviewersSecond)
                .Include(p => p.Language.ReviewersThird);


                    model = model.OrderBy(p => (p.Reviewer != null ? p.Reviewer.ApplicationUser.Fullname : string.Empty));

                    // SelectList data preparation
                    var papersLanguage = model
                        .GroupBy(p => p.LanguageId)
                        .Select(p => p.First())
                        .Select(p => new
                        {
                            langId = p.LanguageId,
                            reviewerslist = p.Language.AllReviewers
                        })
                        .OrderBy(pl => pl.langId);

                    var Vmodel = new List<AssignReviewerViewModel>();
                    var reviewers = _context.ApplicationUser;
                    foreach (var language in papersLanguage)
                    {
                        var tempList = language.reviewerslist
                            .Select(r => new ReviewerVM
                            {
                                ReviewerId = r.UserTypeId,
                                ReviewerName = reviewers.First(au => au.Id == r.ApplicationUserId).Fullname
                            })
                             .ToList();
                        tempList.Insert(0, new ReviewerVM
                        {
                            ReviewerId = -1,
                            ReviewerName = "SELECT REVIEWER"
                        });
                        Vmodel.Add(new AssignReviewerViewModel
                        {
                            LangId = language.langId,
                            reviewersPerLang = tempList
                        });
                    }

                    ViewBag.listOfReviewers = Vmodel;

                    return View(model);
                }
            }
            return RedirectToAction("AccessDenied", "Account", null);
        }

        // POST: PAPERS/ASSIGNREVIEWER
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignReviewer(IFormCollection form)
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            var currentUser = _context.ApplicationUser
                .Include(ap => ap.Users)
                .FirstOrDefault(ap => ap.Id == currentUserId);

            foreach (var userType in currentUser.Users)
            {
                if (userType.GetType().ToString() == "ConFutureNce.Models.ProgrammeCommitteeMember")
                {
                    if (ModelState.IsValid)
                    {
                        var assignedReviewers = Request.Form["item.ReviewerId"];
                        var papersToAssign = Request.Form["item.PaperId"];

                        var papers = _context.Paper
                            .Where(p => p.ReviewerId == null);

                        for (var i = 0; i < papersToAssign.Count; i++)
                        {
                            if (assignedReviewers[i] == "-1")
                                continue;

                            var paper = await _context.Paper
                                .FirstAsync(p => p.PaperId == Convert.ToInt32(papersToAssign[i]));

                            paper.ReviewerId = Convert.ToInt32(assignedReviewers[i]);
                            paper.Status = Paper.ProcessStatus.UnderReview;
                        }

                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> ChoosePaper()
        {
            var currentUserId =  _userManager.GetUserId(HttpContext.User);
            var currentUser = _context.ApplicationUser
                .Include(ap => ap.Users)
                .FirstOrDefault(ap => ap.Id == currentUserId);
            ViewData["UserString"] = currentUser.Users.FirstOrDefault().GetType().ToString();
            foreach (var userType in currentUser.Users)
            {
                if (userType.GetType().ToString() == "ConFutureNce.Models.ProgrammeCommitteeMember")
                {
                    IEnumerable<Paper> model = _context.Paper
                .Include(p => p.Author.ApplicationUser)
                .Include(p => p.PaperKeywords)
                .Include(p => p.Reviewer.ApplicationUser);

                    model = model.OrderBy(p => (p.Reviewer != null ? p.Reviewer.ApplicationUser.Fullname : string.Empty));

            // SelectList data preparation
            

            var statusList = new List<object>
            {
                new 
                {
                    StatusId = Convert.ToInt32(Paper.ProcessStatus.Qualified),
                    StatusName = "Qualified"
                },
                new 
                {
                    StatusId = Convert.ToInt32(Paper.ProcessStatus.Unqualified),
                    StatusName = "Unqualified"
                }
            };

            statusList.Insert(0, new { StatusId = -1, StatusName = "Select"});
            
            ViewBag.listOfStatus = statusList;

                    return View(model);
                }
            }
            return RedirectToAction("Index");
        }

        // POST: PAPERS/ASSIGNREVIEWER
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChoosePaper(IFormCollection form)
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            var currentUser = _context.ApplicationUser
                .Include(ap => ap.Users)
                .FirstOrDefault(ap => ap.Id == currentUserId);

            foreach (var userType in currentUser.Users)
            {
                if (userType.GetType().ToString() == "ConFutureNce.Models.ProgrammeCommitteeMember")
                {
                    if (ModelState.IsValid)
                    {
                        var assignedStatuses = Request.Form["item.Status"];
                        var papersToAssign = Request.Form["item.PaperId"];

                        var papers = _context.Paper
                            .Where(p => p.Status == Paper.ProcessStatus.Reviewed);

                        for (var i = 0; i < papersToAssign.Count; i++)
                        {
                            if (assignedStatuses[i] == "-1")
                                continue;

                            var paper = await _context.Paper
                                .FirstAsync(p => p.PaperId == Convert.ToInt32(papersToAssign[i]));

                            paper.Status = (Paper.ProcessStatus)Convert.ToInt32(assignedStatuses[i]);
                        }

                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    return View();
                }
            }
            return RedirectToAction("Index");
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

        [HttpGet]
        public FileContentResult DownloadFile(int id)
        {
            byte[] fileData;
            string fileName;
            var record = from p in _context.Paper
                         where p.PaperId == id
                         select p;
            fileData = record.First().PaperFile.ToArray();
            fileName = record.First().TitleORG + ".pdf";
            return File(fileData, "application/pdf", fileName);
        }

    }
}
