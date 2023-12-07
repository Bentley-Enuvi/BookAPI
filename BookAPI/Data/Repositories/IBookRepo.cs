using BookAPI.Data.Entities;

namespace BookAPI.Data.Repositories
{
    public interface IBookRepo
    {
        bool AddBook(Book book);
        bool Update(Book book);
        bool Delete(Book book);
        Book GetBook(int id);

        List<Book> GetAllBookList();

    }
}
