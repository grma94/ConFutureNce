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

namespace ConFutureNce.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly ConFutureNceContext _context;

        public PaymentsController(ConFutureNceContext context)
        {
            _context = context;
        }

        // GET: Payments
        public async Task<IActionResult> Index(PaymentViewModel payment)
        {
            return View(payment);
        }

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
        public async Task<IActionResult> PaymentError()
        {
            return View();
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment
                .Include(p => p.Paper)
                .SingleOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            ViewData["PaperId"] = new SelectList(_context.Paper, "PaperId", "PaperId");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentId,IsDone,PaperId")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaperId"] = new SelectList(_context.Paper, "PaperId", "PaperId", payment.PaperId);
            return View(payment);
        }

        private bool PaymentExists(int id)
        {
            return _context.Payment.Any(e => e.PaymentId == id);
        }
    }
}
