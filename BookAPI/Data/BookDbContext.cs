using BookAPI.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BookAPI;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace BookAPI.Data
{
    public class BookDbContext : IdentityDbContext<AppUser>
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) 
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
    }
}
