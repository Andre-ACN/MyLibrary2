
using System.Collections.Generic;
using MyLibrary2.Application.Interfaces;
using MyLibrary2.Domain;
using MyLibrary2.Domain.Errors;

namespace MyLibrary2.Infrastructure
{
    public class InMemoryBookRepository : IBookRepository
    {
        // In-memory storage for books
        private readonly Dictionary<int, Book> _books = new();
        // Simple sequential ID generator
        private int _currentId = 0;
        // Property to get the count of books
        public int Count => _books.Count;
        // Retrieve all books
        public IEnumerable<Book> GetAll() => _books.Values;
        // Retrieve a book by ID
        public Book? GetById(int id) => _books.TryGetValue(id, out var b) ? b : null;
        // Add a new book
        public Book Add(Book book)
        {
            _books.Add(book.Id, book);
            return book;
        }
        // Update an existing book
        public void Update(Book book)
        {
            if (!_books.ContainsKey(book.Id))
                throw new NotFoundException($"Book with ID {book.Id} not found.");
            _books[book.Id] = book;
        }
        // Remove a book by ID
        public void Remove(int id)
        {
            if (!_books.Remove(id))
                throw new NotFoundException($"Book with ID {id} not found.");
        }
        // Generate the next unique ID
        public int GetNextId() => ++_currentId;
    }
}
