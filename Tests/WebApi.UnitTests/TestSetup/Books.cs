using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DBOperation;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(
            new Book { Title = "Mona Lisa Overdrive", GenreId = 1, PageCount = 360, PublishDate = new DateTime(1988, 06, 12) },
            new Book { Title = "Count Zero", GenreId = 2, PageCount = 256, PublishDate = new DateTime(1986, 01, 14) },
            new Book { Title = "Neuromancer", GenreId = 3, PageCount = 271, PublishDate = new DateTime(1984, 07, 01) });
           
        }
    }
}
