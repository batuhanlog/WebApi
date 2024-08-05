using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;

namespace WebApi.DBOperation
{
    public class BookStoreDbContext : DbContext
    {
      
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        { }
      
       
        public DbSet<Book> Books { get; set; }

        public DbSet<Genre> Genres { get; set; }

      

    }
}

