using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using ConFutureNce.Controllers;
using ConFutureNce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ConFutureNce.UnitTests
{
    [TestClass]
    public class PaperControllerTests
    {
        private ConFutureNceContext context;
        private UserManager<ApplicationUser> userManager;
        public PaperControllerTests()
        {
            InitContext();
        }
        public void InitContext()
        {
            if (context != null)
                return;
            // UserManager initialization
            var userStore = new Mock<IUserStore<ApplicationUser>>();
            userManager = new UserManager<ApplicationUser>(userStore.Object, null, null, null, null, null, null, null, null);
            // DB in memory initialization
            var option = new DbContextOptionsBuilder<ConFutureNceContext>().UseInMemoryDatabase("db").Options;
            context = new ConFutureNceContext(option);
            // DB data creation if ApplicationUser table is empty
            if (context.ApplicationUser.Any())
                return;

            #region DB content
            
            var languages = new Language[]
            {
                new Language{LanguageName="Afrikanns"},
                new Language{LanguageName="Albanian"},
                new Language{LanguageName="Arabic"},
                new Language{LanguageName="Armenian"},
                new Language{LanguageName="Basque"},
                new Language{LanguageName="Bengali"},
                new Language{LanguageName="Bulgarian"},
                new Language{LanguageName="Catalan"},
                new Language{LanguageName="Cambodian"},
                new Language{LanguageName="Chinese (Mandarin)"},
                new Language{LanguageName="Croation"},
                new Language{LanguageName="Czech"},
                new Language{LanguageName="Danish"},
                new Language{LanguageName="Dutch"},
                new Language{LanguageName="English"},
                new Language{LanguageName="Estonian"},
                new Language{LanguageName="Fiji"},
                new Language{LanguageName="Finnish"},
                new Language{LanguageName="French"},
                new Language{LanguageName="Georgian"},
                new Language{LanguageName="German"},
                new Language{LanguageName="Greek"},
                new Language{LanguageName="Gujarati"},
                new Language{LanguageName="Hebrew"},
                new Language{LanguageName="Hindi"},
                new Language{LanguageName="Hungarian"},
                new Language{LanguageName="Icelandic"},
                new Language{LanguageName="Indonesian"},
                new Language{LanguageName="Irish"},
                new Language{LanguageName="Italian"},
                new Language{LanguageName="Japanese"},
                new Language{LanguageName="Javanese"},
                new Language{LanguageName="Korean"},
                new Language{LanguageName="Latin"},
                new Language{LanguageName="Latvian"},
                new Language{LanguageName="Lithuanian"},
                new Language{LanguageName="Macedonian"},
                new Language{LanguageName="Malay"},
                new Language{LanguageName="Malayalam"},
                new Language{LanguageName="Maltese"},
                new Language{LanguageName="Maori"},
                new Language{LanguageName="Marathi"},
                new Language{LanguageName="Mongolian"},
                new Language{LanguageName="Nepali"},
                new Language{LanguageName="Norwegian"},
                new Language{LanguageName="Persian"},
                new Language{LanguageName="Polish"},
                new Language{LanguageName="Portuguese"},
                new Language{LanguageName="Punjabi"},
                new Language{LanguageName="Quechua"},
                new Language{LanguageName="Romanian"},
                new Language{LanguageName="Russian"},
                new Language{LanguageName="Samoan"},
                new Language{LanguageName="Serbian"},
                new Language{LanguageName="Slovak"},
                new Language{LanguageName="Slovenian"},
                new Language{LanguageName="Spanish"},
                new Language{LanguageName="Swahili"},
                new Language{LanguageName="Swedish "},
                new Language{LanguageName="Tamil"},
                new Language{LanguageName="Tatar"},
                new Language{LanguageName="Telugu"},
                new Language{LanguageName="Thai"},
                new Language{LanguageName="Tibetan"},
                new Language{LanguageName="Tonga"},
                new Language{LanguageName="Turkish"},
                new Language{LanguageName="Ukranian"},
                new Language{LanguageName="Urdu"},
                new Language{LanguageName="Uzbek"},
                new Language{LanguageName="Vietnamese"},
                new Language{LanguageName="Welsh"},
                new Language{LanguageName="Xhosa"}
             };
            foreach (Language l in languages)
            {
                context.Language.Add(l);
            }
            context.SaveChanges();

            // 4 Applicaton Users in each UserType
            var users = new ApplicationUser[]
            {
                new ApplicationUser
                {
                    Name="Author",Email="lala1@gmail.com", Address="Plac Grunwaldzki 23, Wroc³aw, Polska", ConferenceName="Great Conference",
                    EmailConfirmed =true,  UserName="lala1@gmail.com"
                },
                new ApplicationUser
                {
                    Name="Reviewer",Email="lala2@gmail.com", Address="Koœciuszki 28, Wroc³aw, Polska", ConferenceName="Great Conference",
                    EmailConfirmed =true,  UserName="lala2@gmail.com"
                },
                new ApplicationUser
                {
                    Name="ProgrammeCommitteeMember",Email="lala3@gmail.com", Address="Kochanowskiego 21, Bogatynia, Polska", ConferenceName="Great Conference", EmailConfirmed =true,  UserName="lala3@gmail.com"
                },new ApplicationUser
                {
                    Name="Organizer",Email="lala4@gmail.com", Address="D³uga 28, Gdañsk, Polska", ConferenceName="Great Conference",
                    EmailConfirmed =true,  UserName="lala4@gmail.com"
                }
            };
            foreach (ApplicationUser u in users)
            {
                context.ApplicationUser.Add(u);
            }
            context.SaveChanges();

            // UserType creation
            // 1 ApplicationUser = 1 UserType

            // 1 organizers
            var organizers = new Organizer[]
            {
                new Organizer
                {
                    ApplicationUserId = context.ApplicationUser.First(ap => ap.Name == "Organizer").Id,
                    EmployeePosition = "Master Organizer"
                }
            };
            foreach (var organizer in organizers)
            {
                context.Organizer.Add(organizer);
            }
            context.SaveChanges();
            // 1 programmeCommitteeMembers
            var programmeCommitteeMembers = new ProgrammeCommitteeMember[]
            {
                new ProgrammeCommitteeMember
                {
                    ApplicationUserId = context.ApplicationUser.First(ap => ap.Name == "ProgrammeCommitteeMember").Id,
                    EmployeePosition = "Slave Programme Committee Member"
                }
            };
            foreach (var programmeCommitteeMember in programmeCommitteeMembers)
            {
                context.ProgrammeCommitteeMember.Add(programmeCommitteeMember);
            }
            context.SaveChanges();
            // 1 author
            var authors = new Author[]
            {
                new Author
                {
                    ApplicationUserId = context.ApplicationUser.First(ap => ap.Name == "Author").Id,
                    ScTitle="MSc",
                    OrgName="Wroc³aw University of Science and Technology"
                }
            };
            foreach (Author a in authors)
            {
                context.Author.Add(a);
            }
            context.SaveChanges();
            // 1 reviewer
            var reviewers = new Reviewer[]
            {
                new Reviewer
                {
                    ApplicationUserId = context.ApplicationUser.First(ap => ap.Name == "Reviewer").Id,
                    ScTitle ="Ph.D.",
                    OrgName ="Wroc³aw University of Science and Technology",
                    Language1Id =47,
                    Language2Id =15
                }
            };
            foreach (Reviewer r in reviewers)
            {
                context.Reviewer.Add(r);
            }
            context.SaveChanges();

            var papers = new Paper[]
            {
                new Paper
                {
                    TitleENG="Test1",
                    TitleORG ="Teœcik1",
                    Abstract ="taki tam sobie test enuma",
                    Authors ="Marek Granowicz",
                    LanguageId =47,
                    OrgName ="PWr",
                    Status = Paper.ProcessStatus.Reviewed,
                    AuthorId =context.Author.First().UserTypeId,
                    ReviewerId = context.Reviewer.First().UserTypeId
                },
                new Paper
                {
                    TitleENG="New Big Bang Theory",
                    TitleORG ="Nowa teoria Wielkiego Wybuchu",
                    Authors ="Wocjciech Pêciak, Robert Lewandowski",
                    Abstract ="Skrót treœci - abstract",
                    OrgName ="WAT",
                    SubmissionDate = DateTime.Now,
                    Status = Paper.ProcessStatus.UnderReview,
                    LanguageId = 15,
                    AuthorId = context.Author.First().UserTypeId
                },
                new Paper
                {
                    TitleENG = "Test1",
                    TitleORG = "Teœcik1",
                    Abstract = "taki tam sobie test enuma",
                    Authors = "Marek Granowicz",
                    LanguageId = 47,
                    OrgName = "PWr",
                    Status = Paper.ProcessStatus.UnderReview,
                    AuthorId = context.Author.First().UserTypeId,
                    ReviewerId = context.Reviewer.First().UserTypeId
                },
                new Paper
                {
                    TitleENG = "Test2",
                    TitleORG = "Teœcik2",
                    Abstract = "taki tam sobie test enuma",
                    Authors = "Marek Granowicz",
                    LanguageId = 47,
                    OrgName = "PWr",
                    Status = 0,
                    AuthorId = context.Author.First().UserTypeId
                },
                new Paper
                {
                    TitleENG = "Test3",
                    TitleORG = "Teœcik3",
                    Abstract = "taki tam sobie test enuma",
                    Authors = "Marek Granowicz",
                    LanguageId = 47,
                    OrgName = "PWr",
                    Status = 0,
                    AuthorId = context.Author.First().UserTypeId
                }
        };
            foreach (Paper p in papers)
            {
                context.Paper.Add(p);
            }
            context.SaveChanges();

            var keywords = new PaperKeyword[]
            {
                new PaperKeyword
                {
                    KeyWord = "physics",
                    PaperId = context.Paper.FirstOrDefault(p => p.PaperId == 1).PaperId
                },
                new PaperKeyword
                {
                    KeyWord = "frog",
                    PaperId = context.Paper.FirstOrDefault(p => p.PaperId == 1).PaperId
                },
                new PaperKeyword
                {
                    KeyWord = "wood",
                    PaperId = context.Paper.FirstOrDefault(p => p.PaperId == 1).PaperId
                },
                new PaperKeyword
                {
                    KeyWord = "rock",
                    PaperId = context.Paper.FirstOrDefault(p => p.PaperId == 1).PaperId
                },
                new PaperKeyword
                {
                    KeyWord = "music",
                    PaperId = context.Paper.FirstOrDefault(p => p.PaperId == 2).PaperId
                },
                new PaperKeyword
                {
                    KeyWord = "lake",
                    PaperId = context.Paper.FirstOrDefault(p => p.PaperId == 2).PaperId
                },
                new PaperKeyword
                {
                    KeyWord = "mutant",
                    PaperId = context.Paper.FirstOrDefault(p => p.PaperId == 2).PaperId
                },
                new PaperKeyword
                {
                    KeyWord = "leaf",
                    PaperId = context.Paper.FirstOrDefault(p => p.PaperId == 2).PaperId
                },

            };
            foreach (var keyword in keywords)
            {
                context.PaperKeyword.Add(keyword);
            }
            context.SaveChanges();
            // Make payment for each paper
            foreach (var paper in papers)
            {
                context.Payment.Add(new Payment
                {
                    IsDone = true,
                    PaperId = paper.PaperId
                });
            }
            context.SaveChanges();

            var reviews = new Review[]
            {
                new Review
                {
                    Problems = "What was the problem?",
                    WhyProblems = "Why it was a problem?",
                    Solution = "What is a proposed solution?",
                    Achievements = "What is outcome?",
                    NotMentioned = "Gaps?",
                    Grade = "On scale 0 to 10.",
                    GeneralComments = "It was lovely day.",
                    Date = DateTime.Now,
                    PaperId = context.Paper.First(p => p.Status == Paper.ProcessStatus.Reviewed).PaperId
                }
            };
            foreach (var review in reviews)
            {
                context.Review.Add(review);
            }
            context.SaveChanges();

            #endregion
        }

        [DataTestMethod]
        [DataRow("Author", "Author", DisplayName = "Test for Author")]
        [DataRow("ProgrammeCommitteeMember", "ProgrammeCommitteeMember", DisplayName = "Test for ProgrammeCommitteeMember")]
        [DataRow("Reviewer", "Reviewer", DisplayName = "Test for Reviewer")]
        [DataRow("Organizer", null, DisplayName = "Test for Organizer")]
        public void IndexRoutingToUserTypesView(string currentUserType, string resultViewName)
        {
            //------------Preparation

            // HttpContext mockup -> ApplictionUser with Name = "Author"
            // assigned as current user in HttpContext
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,
                            // You can assign current user to ApplicationUser.id, which is refering to dessired UserType
                            context.ApplicationUser.First(ap => ap.Name == currentUserType).Id)

            };
            var identity = new ClaimsIdentity(claims, "Test");
            var claimsPrinicipal = new ClaimsPrincipal(identity);
            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(hc => hc.User).Returns(claimsPrinicipal);

            // Transfer HttpContext to new PaperController
            var controllerContext = new ControllerContext(){ HttpContext = httpContextMock.Object };
            var controller = new PapersController(context, userManager){ ControllerContext = controllerContext };

            //------------Action
            var result = controller.Index(null,null,null,null).Result as ViewResult;

            //------------Assertion
            Assert.AreEqual(resultViewName, result.ViewName);
        }
    }
}
