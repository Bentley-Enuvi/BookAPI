using BookAPI.Data.Entities;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace BookAPI.Data.Repositories
{
    public class BookRepo : IBookRepo
    {
        private readonly BookDbContext _context;
        public BookRepo(BookDbContext context)
        {
            _context = context;
        }

        //Add a book
        public bool AddBook(Book book)
        {
            _context.Add(book);
            _context.SaveChanges();
            return true;
        }


        //Delete a book
        public bool Delete(Book book)
        {
            _context.Remove(book);
            _context.SaveChanges();
            return true;
        }


        //Get all books
        public List<Book> GetAllBookList()
        {
            return _context.Books.ToList();
        }


        //Get a single book
        public Book GetBook(int id)
        {
            var result = _context.Books.FirstOrDefault(x => x.BookId == id);
            if (result != null)
            {
                return result;
            }
            return new Book();
        }


        // Update book details
        public bool Update(Book book)
        {
            _context.Update(book);
            _context.SaveChanges();
            return true;
        }
    }
}
