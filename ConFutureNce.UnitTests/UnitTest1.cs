using ConFutureNce.Controllers;
using ConFutureNce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConFutureNce.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            InitContext();
        }

        private ConFutureNceContext conFutureNceContext;
        private UserManager<ApplicationUser> userManager;

        public void InitContext()
        {
            var builder = new DbContextOptionsBuilder<ConFutureNceContext>().UseInMemoryDatabase("db");
            var context = new ConFutureNceContext(builder.Options);
            conFutureNceContext = context;

        }

        [TestMethod]
        public void TestMethod1()
        {
            PapersController controller = new PapersController(conFutureNceContext, userManager);

            ViewResult result = controller.Index(null,null,null,null).AsyncState as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
        }
    }
}
