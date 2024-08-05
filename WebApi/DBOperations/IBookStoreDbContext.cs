using Microsoft.EntityFrameworkCore;
using WebApi.DBOperation;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public interface IBookStoreDbContext 
    {
        DbSet<Book> Books { get; }
        DbSet<Genre> Genres { get; }
        int SaveChanges();
    }
}
