using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConFutureNce.Models
{
    public static class DbInitializer
    {
        public static void Initialize(ConFutureNceContext context)
        {
            context.Database.EnsureCreated();

            if (context.Language.Any())
            {
                return;
            }

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

            var conference = new Conference {
                Name = "Great Conference",
                PaperDeadline = DateTime.Parse("2018-06-06 00:00"),
                ReviewDeadline = DateTime.Parse("2018-07-06 00:00"),
                SelectionDeadline = DateTime.Parse("2018-08-06 00:00"),
                AssignDeadline = DateTime.Parse("2018-09-06 00:00"),
                StartDate = DateTime.Parse("2018-09-13 09:00"),
                EndDate = DateTime.Parse("2018-09-24 20:00")
            };
            context.Conference.Add(conference);
            context.SaveChanges();
            // 10 Applicaton Users
            var users = new ApplicationUser[]
            {
                new ApplicationUser
                {
                    Email="lala1@gmail.com", Address="Plac Grunwaldzki 23, Wrocław, Polska", ConferenceName="Great Conference",
                    EmailConfirmed =true, Name="Jan", Surname="Kowalski", UserName="lala1@gmail.com"
                },
                new ApplicationUser
                {
                    Email="lala2@gmail.com", Address="Kościuszki 28, Wrocław, Polska", ConferenceName="Great Conference",
                    EmailConfirmed =true, Name="Paweł", Surname="Nowak", UserName="lala2@gmail.com"
                },
                new ApplicationUser
                {
                    Email="lala3@gmail.com", Address="Kochanowskiego 21, Bogatynia, Polska", ConferenceName="Great Conference",
                    EmailConfirmed =true, Name="Krzysztof", Surname="Kowalski", UserName="lala3@gmail.com"
                },new ApplicationUser
                {
                    Email="lala4@gmail.com", Address="Długa 28, Gdańsk, Polska", ConferenceName="Great Conference",
                    EmailConfirmed =true, Name="Michał", Surname="Gall", UserName="lala4@gmail.com"
                },new ApplicationUser
                {
                    Email="lala5@gmail.com", Address="Kościuszki 28, Wrocław, Polska", ConferenceName="Great Conference",
                    EmailConfirmed =true, Name="Addrian", Surname="Orski", UserName="lala5@gmail.com"
                },
                new ApplicationUser
                {
                    Email="lala6@gmail.com", Address="Hallera 28, Wrocław, Polska", ConferenceName="Great Conference",
                    EmailConfirmed =true, Name="Ola", Surname="Hnatkowska", UserName="lala6@gmail.com"
                },
                new ApplicationUser
                {
                    Email="lala7@gmail.com", Address="Koszarowa 28, Gdynia, Polska", ConferenceName="Great Conference",
                    EmailConfirmed =true, Name="Czesła", Surname="Bożemski", UserName="lala7@gmail.com"
                },
                new ApplicationUser
                {
                    Email="lala8@gmail.com", Address="Zielona 28, Wrocław, Polska", ConferenceName="Great Conference",
                    EmailConfirmed =true, Name="Filip", Surname="Noga", UserName="lala8@gmail.com"
                },
                new ApplicationUser
                {
                    Email="lala9@gmail.com", Address="Czereśniowa 28, Radom, Polska", ConferenceName="Great Conference",
                    EmailConfirmed =true, Name="Stanisław", Surname="Waśko", UserName="lala9@gmail.com"
                },
                new ApplicationUser
                {
                    Email="lala10@gmail.com", Address="Kościuszki 28, Katowice, Polska", ConferenceName="Great Conference",
                    EmailConfirmed =true, Name="Marcin", Surname="Huk", UserName="lala10@gmail.com"
                },
            };
            foreach (ApplicationUser u in users)
            {
                context.ApplicationUser.Add(u);
            }
            context.SaveChanges();
            
            // UserType creation
            // 1 ApplicationUser = 1 UserType

            // 2 organizers
            var organizers = new Organizer[]
            {
                new Organizer
                {
                    ApplicationUserId = context.ApplicationUser.FirstOrDefault(a=>a.Email == "lala1@gmail.com").Id,
                    EmployeePosition = "Master Organizer"
                },
                new Organizer
                {
                    ApplicationUserId = context.ApplicationUser.FirstOrDefault(a=>a.Email == "lala2@gmail.com").Id,
                    EmployeePosition = "Slave Organizer"
                }
            };
            foreach (var organizer in organizers)
            {
                context.Organizer.Add(organizer);
            }
            context.SaveChanges();
            // 2 programmeCommitteeMembers
            var programmeCommitteeMembers = new ProgrammeCommitteeMember[]
            {
                new ProgrammeCommitteeMember
                {
                    ApplicationUserId = context.ApplicationUser.FirstOrDefault(a => a.Email == "lala3@gmail.com").Id,
                    EmployeePosition = "Master Programme Committee Member"
                },
                new ProgrammeCommitteeMember
                {
                    ApplicationUserId = context.ApplicationUser.FirstOrDefault(a => a.Email == "lala4@gmail.com").Id,
                    EmployeePosition = "Slave Programme Committee Member"
                }
            };
            foreach (var programmeCommitteeMember in programmeCommitteeMembers)
            {
                context.ProgrammeCommitteeMember.Add(programmeCommitteeMember);
            }
            context.SaveChanges();
            // 4 authors
            var authors = new Author[]
            {
                new Author
                {
                    ApplicationUserId = context.ApplicationUser.FirstOrDefault(a => a.Email == "lala5@gmail.com").Id,
                    ScTitle="MSc",
                    OrgName="Wrocław University of Science and Technology"
                },
                new Author
                {
                    ApplicationUserId = context.ApplicationUser.FirstOrDefault(a => a.Email == "lala6@gmail.com").Id,
                    ScTitle="Ph.D.",
                    OrgName="Warsaw University of Science and Technology"
                },
                new Author
                {
                    ApplicationUserId = context.ApplicationUser.FirstOrDefault(a => a.Email == "lala7@gmail.com").Id,
                    ScTitle="MSc",
                    OrgName="Radom University of Science"
                },
                new Author
                {
                    ApplicationUserId = context.ApplicationUser.FirstOrDefault(a => a.Email == "lala8@gmail.com").Id,
                    ScTitle="Ph.D.",
                    OrgName="Sosnsowiec University of Technology"
                }
            };
            foreach (Author a in authors)
            {
                context.Author.Add(a);
            }
            context.SaveChanges();
            // 2 reviewers
            var reviewers = new Reviewer[]
            {
                new Reviewer
                {
                    ApplicationUserId = context.ApplicationUser.FirstOrDefault(a => a.Email == "lala9@gmail.com").Id,
                    ScTitle ="Ph.D.",
                    OrgName ="Wrocław University of Science and Technology",
                    Language1Id =47,
                    Language2Id =15
                },
                new Reviewer
                {
                    ApplicationUserId = context.ApplicationUser.FirstOrDefault(a => a.Email == "lala10@gmail.com").Id,
                    ScTitle ="Ph.D.",
                    OrgName ="Warsaw University of Science and Technology",
                    Language1Id = 23,
                    Language2Id = 10,
                    Language3Id = 50
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
                    TitleORG ="Teścik1",
                    Abstract ="taki tam sobie test enuma",
                    Authors ="Marek Granowicz",
                    LanguageId =47,
                    OrgName ="PWr",
                    Status =0,
                    AuthorId =context.Author.First().UserTypeId
                },
                new Paper
                {
                    TitleENG="New Big Bang Theory",
                    TitleORG ="Nowa teoria Wielkiego Wybuchu",
                    Authors ="Wocjciech Pęciak, Robert Lewandowski",
                    Abstract ="Skrót treści - abstract",
                    OrgName ="WAT",
                    SubmissionDate = DateTime.Now,
                    Status = Paper.ProcessStatus.Reviewed,
                    LanguageId = 60,
                    AuthorId = context.Author.FirstOrDefault(a => a.UserTypeId == 6).UserTypeId,
                    ReviewerId = context.Reviewer.FirstOrDefault(r => r.UserTypeId == 9).UserTypeId
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
                    PaperId = context.Paper.FirstOrDefault(p => p.PaperId == 2).PaperId
                }
            };
            foreach (var review in reviews)
            {
                context.Review.Add(review);
            }
            context.SaveChanges();
        }
    }
}
