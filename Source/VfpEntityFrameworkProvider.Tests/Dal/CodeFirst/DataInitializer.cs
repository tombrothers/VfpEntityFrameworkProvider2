using System.Data.Entity;
using System.Linq;
using VfpEntityFrameworkProvider.Tests.Dal.CodeFirst.Models;

namespace VfpEntityFrameworkProvider.Tests.Dal.CodeFirst {
    internal class DataInitializer : DropCreateDatabaseAlways<CodeFirstContext> {
        protected override void Seed(CodeFirstContext context) {
            AddArtists(context);
            AddUsers(context);

            context.SaveChanges();
        }

        private static void AddUsers(CodeFirstContext context) {
            Enumerable.Range(0, 5)
                      .Select(x => new User {
                          FirstName = "First" + x,
                          LastName = "Last" + x
                      })
                      .ToList()
                      .ForEach(x => context.Users.Add(x));
        }

        private static void AddArtists(CodeFirstContext context) {
            Enumerable.Range(0, 5)
                      .Select(x => new Artist { Name = "Artist" + x })
                      .ToList()
                      .ForEach(x => context.Artists.Add(x));
        }
    }
}