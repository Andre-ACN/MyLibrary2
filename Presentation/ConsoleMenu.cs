
using System;
using MyLibrary2.Application.Interfaces;
using MyLibrary2.Domain.Errors;

namespace MyLibrary2.Presentation
{
    /// <summary>
    /// interactive console-based menu
    /// </summary>
    public class ConsoleMenu
    {
        // dependency injection of the book service
        private readonly IBookService _books;
        /// constructor
        public ConsoleMenu(IBookService books) => _books = books;
        /// main loop
        public void Run()
        {
            SeedSampleData();

            bool isRunning = true;
            while (isRunning)
            {
                PrintMenu();
                Console.Write("Enter 1 to 6: ");
                var choice = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("~~~");

                try
                {
                    switch (choice)
                    {
                        case "1": HandleAdd(); break;
                        case "2": HandleUpdate(); break;
                        case "3": HandleDelete(); break;
                        case "4": HandleList(); break;
                        case "5": HandleView(); break;
                        case "6":
                            isRunning = false;
                            Console.WriteLine("Exiting Library Application. Goodbye!");
                            break;
                        default:
                            Console.WriteLine("Invalid. Enter 1 to 6.");
                            break;
                    }
                }
                catch (ValidationException vex)
                {
                    Console.WriteLine($"Validation error: {vex.Message}");
                }
                catch (NotFoundException nfx)
                {
                    Console.WriteLine($"Not found: {nfx.Message}");
                }
                catch (Exception ex)
                {
                    // Generic safety net; in real apps, log this.
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }

                Console.WriteLine("~~~\n");
            }
        }

        private void PrintMenu()
        {
            Console.WriteLine("1. Add a new book");
            Console.WriteLine("2. Update an existing book");
            Console.WriteLine("3. Delete a book");
            Console.WriteLine("4. List all books");
            Console.WriteLine("5. View details of a specific book");
            Console.WriteLine("6. Exit Application");
        }

        private void HandleAdd()
        {
            Console.WriteLine("Add Book:");
            if (_books.Count() >= 100)
            {
                Console.WriteLine("Library is full. Cannot add more books.");
                return;
            }

            Console.Write("Enter Book Title: ");
            var title = Console.ReadLine();
            Console.Write("Enter Book Author: ");
            var author = Console.ReadLine();

            var created = _books.Add(title ?? string.Empty, author ?? string.Empty);
            Console.WriteLine($"Book '{created.Title}' by {created.Author} added with ID {created.Id}.");
        }

        private void HandleUpdate()
        {
            Console.WriteLine("Update Book:");
            if (_books.Count() == 0)
            {
                Console.WriteLine("No books available in the library.");
                return;
            }

            Console.Write("Enter Book ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID. Please enter a number.");
                return;
            }

            var existing = _books.Get(id); // throws NotFoundException if missing
            Console.Write($"Enter new title (current: {existing.Title}): ");
            var newTitle = Console.ReadLine() ?? string.Empty;
            Console.Write($"Enter new author (current: {existing.Author}): ");
            var newAuthor = Console.ReadLine() ?? string.Empty;

            _books.Update(id, newTitle, newAuthor);
            Console.WriteLine($"Book with ID \"{id}\" updated.");
        }

        private void HandleDelete()
        {
            Console.WriteLine("Delete Book:");
            if (_books.Count() == 0)
            {
                Console.WriteLine("No books available in the library.");
                return;
            }

            Console.Write("Enter Book ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID. Please enter a number.");
                return;
            }

            var b = _books.Get(id);
            _books.Remove(id);
            Console.WriteLine($"Book with ID \"{id}\", title \"{b.Title}\" deleted.");
        }

        private void HandleList()
        {
            Console.WriteLine("List of Books:");
            foreach (var book in _books.List())
                Console.WriteLine($"ID: {book.Id}, Title: {book.Title}, Author: {book.Author}");
        }

        private void HandleView()
        {
            Console.WriteLine("View Book Details:");
            if (_books.Count() == 0)
            {
                Console.WriteLine("No books available in the library.");
                return;
            }

            Console.Write("Enter Book ID for details: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID. Please enter a number.");
                return;
            }

            var book = _books.Get(id);
            Console.WriteLine($"ID: {book.Id}");
            Console.WriteLine($"Title: {book.Title}");
            Console.WriteLine($"Author: {book.Author}");
        }

        private void SeedSampleData()
        {
            if (_books.Count() > 0) return;
            _books.Add("Book 1", "Author 1");
            _books.Add("Book 2", "Author 2");
        }
    }
}