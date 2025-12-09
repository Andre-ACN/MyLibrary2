using MyLibrary2.Application.Interfaces;
using MyLibrary2.Domain;
using MyLibrary2.Domain.Errors;
using ValidationException = MyLibrary2.Domain.Errors.ValidationException;

namespace MyLibrary2.Application
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repo;

        public BookService(IBookRepository repo) => _repo = repo;

        public Book Add(string title, string author)
        {
            ValidateTitleAuthor(title, author);

            var book = new Book
            {
                Id = _repo.GetNextId(),
                Title = title.Trim(),
                Author = author.Trim()
            };
            return _repo.Add(book);
        }

        public void Update(int id, string newTitle, string newAuthor)
        {
            ValidateTitleAuthor(newTitle, newAuthor);

            var existing = _repo.GetById(id);
            if (existing is null)
                throw new NotFoundException($"Book with ID {id} not found.");

            existing.Title = newTitle.Trim();
            existing.Author = newAuthor.Trim();
            _repo.Update(existing);
        }

        public void Remove(int id)
        {
            var existing = _repo.GetById(id);
            if (existing is null)
                throw new NotFoundException($"Book with ID {id} not found.");
            _repo.Remove(id);
        }

        public Book Get(int id)
        {
            var existing = _repo.GetById(id);
            if (existing is null)
                throw new NotFoundException($"Book with ID {id} not found.");
            return existing;
        }

        public IEnumerable<Book> List() => _repo.GetAll();
        public int Count() => _repo.Count;

        private static void ValidateTitleAuthor(string? title, string? author)
        {
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author))
                throw new ValidationException("Title and Author cannot be empty.");
        }
    }
}
