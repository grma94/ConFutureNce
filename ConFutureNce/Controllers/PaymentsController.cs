using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConFutureNce.Models;
using ConFutureNce.Models.PaperViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ConFutureNce.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly ConFutureNceContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public PaymentsController(ConFutureNceContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index(PaymentViewModel payment)
        {
            return View(payment);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IFormCollection form)
        {
            var error = (new Random().Next(1, 9)) > 7;
            var paperId = Convert.ToInt32(Request.Form["PaperId"]);

            if (!error )
            {
                var billingAddress = Request.Form["BillingAddress"];
                var userName = Request.Form["UserName"];
                var taxNumber = Request.Form["TaxNumber"];

                // Create payment
                var payment = new Payment
                {
                    IsDone = false
                };
                // Create invoice
                var invoice = new Invoice
                {
                    Name = userName,
                    BillingAddress = billingAddress,
                    TaxNumber = taxNumber,
                    Payment = payment
                };
                // Upadete payment
                payment.Invoice = invoice;
                payment.PaperId = paperId;
                payment.IsDone = true;

                _context.Invoice.Add(invoice);
                _context.Payment.Add(payment);
                _context.SaveChanges();
                return RedirectToAction("Index", "Papers");

            }
            var paper = await _context.Paper.FirstAsync(p => p.PaperId == paperId);

            _context.Paper.Remove(paper);
            _context.SaveChanges();
            return RedirectToAction("PaymentError");
            
        }
        // GET: Payments
        [Authorize]
        public async Task<IActionResult> PaymentError()
        {
            return View();
        }

        // GET: Payments/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var payment = await _context.Payment
                .Include(p => p.Paper.AuthorId)
                .SingleOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return View("NotFound");
            }

            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            currentUser = _context.ApplicationUser
                .Include(ap => ap.Users)
                .FirstOrDefault(ap => ap.Id == currentUser.Id);

            foreach (var userType in currentUser.Users)
            {
                if (payment.Paper.AuthorId == userType.UserTypeId)
                {
                    return View(payment);
                }
            }
            return RedirectToAction("AccessDenied", "Account", null);
        }

        private bool PaymentExists(int id)
        {
            return _context.Payment.Any(e => e.PaymentId == id);
        }
    }
}
