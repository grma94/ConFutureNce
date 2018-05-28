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

            var conference = new Conference { Name = "Great Conference" };
            context.Conference.Add(conference);
            context.SaveChanges();

            var users = new ApplicationUser[]
            {
                new ApplicationUser{Email="lala@gmail.com", Address="Plac Grunwaldzki 23, Wrocław, Polska", ConferenceName="Great Conference", EmailConfirmed=true, Name="Jan", Surname="Kowalski", UserName="lala@gmail.com"},
                 new ApplicationUser{Email="lala55@gmail.com", Address="Kościuszki 28, Wrocław, Polska", ConferenceName="Great Conference", EmailConfirmed=true, Name="Paweł", Surname="Nowak", UserName="lala55@gmail.com"}
            };
            foreach (ApplicationUser u in users)
            {
                context.ApplicationUser.Add(u);
            }
            context.SaveChanges();

            var reviewers = new Reviewer[]
            {
                new Reviewer{ScTitle="Ph.D.", OrgName="Wrocław University of Science and Technology", Language1Id=47, Language2Id=15, ApplicationUserId=context.ApplicationUser.First().Id}
            };
            foreach (Reviewer r in reviewers)
            {
                context.Reviewer.Add(r);
            }
            context.SaveChanges();

            var authors = new Author[]
            {
                new Author{ScTitle="Ph.D.", OrgName="Wrocław University of Science and Technology", ApplicationUserId=context.ApplicationUser.First().Id}
            };
            foreach (Author a in authors)
            {
                context.Author.Add(a);
            }
            context.SaveChanges();



        }
    }
}
