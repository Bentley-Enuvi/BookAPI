using BookAPI.Migrations;
using Microsoft.AspNetCore.Mvc;
using BookAPI.Data.Entities;
using BookAPI.Data.Repositories;
using BookAPI.DTOs;

namespace BookAPI.Controllers
{
    public class BookController : ControllerBase
    {
        private readonly IBookRepo _bookRepo;

        public BookController(IBookRepo bookRepo)
        {
            _bookRepo = bookRepo;
        }


        [HttpPost("add-a-book")]
        public IActionResult AddNewBook([FromBody] AddBookDto model)
        {
            if (ModelState.IsValid)
            {
                if (model.Title == "string" || model.Title == "")
                    return BadRequest("Invalid entry");

                var Date = DateTime.Parse(model.DatePublished);

                var bookToAdd = new Book
                {
                    Title = model.Title,
                    AuthorFirstName = model.AuthorFirstName,
                    AuthorLastName = model.AuthorLastName,
                    Description = model.Description,
                    PageCount = model.PageCount,
                    DatePublished = Date,
                };
                if (_bookRepo.AddBook(bookToAdd))
                {
                    return Ok("Book successfully added!");
                }
                return BadRequest("Book not added!");
            }
            return BadRequest(ModelState);

        }


        // Get a single book by ID
        [HttpGet("single/{id}")]
        public IActionResult GetSingleBook(int id)
        {
            var book = _bookRepo.GetBook(id);
            if (book.BookId > 0)
            {
                var result = new ReturnBookDto
                {
                    Title = book.Title,
                    AuthorFirstName = book.AuthorFirstName,
                    AuthorLastName = book.AuthorLastName,
                    Description = book.Description,
                    PageCount = book.PageCount,
                    DatePublished = book.DatePublished.ToString()

                };
                return Ok(result);
            }
            return BadRequest($"Result not found for book with id number: {id}");
        }



        //Get all the list of books available
        [HttpGet("get-all")]
        public IActionResult GetAllBooks()
        {
            var books = _bookRepo.GetAllBookList();

            var result = books.Select(ab => new ReturnBookDto
            {
                Title = ab.Title,
                AuthorFirstName = ab.AuthorFirstName,
                AuthorLastName = ab.AuthorLastName,
                Description = ab.Description,
                PageCount = ab.PageCount,
                DatePublished = ab.DatePublished.ToString()
            });
            return Ok(result);
        }


        //Edit a book
        [HttpPut("update-a-book")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookDto model)
        {
            var book = _bookRepo.GetBook(id);
            if (book != null)
            {
                book.Title = model.Title;
                book.AuthorFirstName = model.AuthorFirstName;
                book.AuthorLastName = model.AuthorLastName;

                if (_bookRepo.Update(book))
                {
                    return Ok("Updated successfully!");
                }

                return BadRequest("Update failed!");
            }

            return BadRequest($"Update failed: Could not update book with id {id}");
        }



        //Delete a book
        [HttpDelete("delete-a-book")]
        public IActionResult DeleteBook(int id, [FromBody] UpdateBookDto model)
        {
            var book = _bookRepo.GetBook(id);
            if (book != null)
            {
                if (_bookRepo.Delete(book))
                {
                    return Ok("Deleted successfully");
                }

                return BadRequest("Delete failed!");
            }

            return BadRequest($"Delete failed: Could not get book with id {id}");
        }
    }
}
