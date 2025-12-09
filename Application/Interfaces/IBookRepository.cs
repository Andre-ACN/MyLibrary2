
using System.Collections.Generic;
using MyLibrary2.Domain;

namespace MyLibrary2.Application.Interfaces
{
    /// <summary>
    /// interface for adding, updating, and removing book entries.
    /// </summary>
    public interface IBookRepository
    {
        int Count { get; }
        IEnumerable<Book> GetAll();
        Book? GetById(int id);
        Book Add(Book book);        // returns the created book with assigned Id
        void Update(Book book);     // throws if not found
        void Remove(int id);        // throws if not found
        int GetNextId();            // simple sequential id generator
    }
}
