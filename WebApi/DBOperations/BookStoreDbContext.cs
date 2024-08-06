using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.DBOperation
{
    public class BookStoreDbContext : DbContext, IBookStoreDbContext
    {
      
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        { }
      
       
        public DbSet<Book> Books { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<User> Users { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}

