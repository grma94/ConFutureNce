using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ConFutureNce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ConFutureNce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ConFutureNceContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ConFutureNceContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            var currentUser = _context.ApplicationUser
                .Include(ap => ap.Users)
                .FirstOrDefault(ap => ap.Id == currentUserId);

            if(currentUser!=null)ViewData["UserString"] = currentUser.Users.FirstOrDefault().GetType().ToString();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
