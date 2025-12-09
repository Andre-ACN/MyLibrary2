
using System.Collections.Generic;
using MyLibrary2.Domain;

namespace MyLibrary2.Application.Interfaces
{
    /// <summary>
    /// Defines operations for managing a collection of books, including adding, updating, removing, retrieving, and
    /// listing books.
    /// </summary>
    /// <remarks>Implementations of this interface should ensure thread safety if accessed concurrently. The
    /// interface does not specify persistence or storage details; behavior may vary depending on the
    /// implementation.</remarks>
    public interface IBookService
    {
        Book Add(string title, string author);
        void Update(int id, string newTitle, string newAuthor);
        void Remove(int id);
        Book Get(int id);
        IEnumerable<Book> List();
        int Count();
    }
}
