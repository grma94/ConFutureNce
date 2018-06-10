using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using ConFutureNce;
using ConFutureNce.Controllers;
using Microsoft.EntityFrameworkCore;
using ConFutureNce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ConFutureNce.UnitTests
{
    [TestClass]
    class PapersControllerIndexTests
    {
        public PapersControllerIndexTests()
        {
            InitContext();
        }

        private ConFutureNceContext conFutureNceContext;
        private UserManager<ApplicationUser> userManager;
        public void InitContext()
        {
            var builder = new DbContextOptionsBuilder<ConFutureNceContext>().UseInMemoryDatabase("db");
            var context = new ConFutureNceContext(builder.Options);
            
        }
        [TestMethod]
        public void PapersIndexTests()
        {
            PapersController controller = new PapersController(conFutureNceContext, userManager);

            ViewResult result = controller.Create() as ViewResult;

            Assert.AreEqual("Create", result.ViewName);
        }
    }
}
