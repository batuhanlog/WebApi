using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using WebApi.Entities;

namespace WebApi.DBOperation
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return; // DB has been seeded
                }

                context.Books.AddRange(
                    new Book
                    {
                        Title = "Mona Lisa Overdrive",
                        GenreId = 1, // Science Fiction
                        PageCount = 360,
                        PublishDate = new DateTime(1988, 06, 12)
                    },
                    new Book
                    {
                        Title = "Count Zero",
                        GenreId = 2, // Science Fiction
                        PageCount = 256,
                        PublishDate = new DateTime(1986, 01, 14)
                    },
                    new Book
                    {
                        Title = "Neuromancer",
                        GenreId = 3, // Science Fiction
                        PageCount = 271,
                        PublishDate = new DateTime(1984, 07, 01)
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
