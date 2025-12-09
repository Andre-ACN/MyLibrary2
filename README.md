# MyLibrary2
Sample library application

## Structure
**Domain Layer:** Contains Book and domain exceptions. Pure business logic, no dependencies on frameworks.

**Application Layer:** Contains BookService and interfaces (IBookService, IBookRepository). Implements use cases and enforces rules.

**Infrastructure Layer:** Contains InMemoryBookRepository. Deals with persistence details.

**Presentation Layer:** Contains ConsoleMenu. Handles user interaction only.

**Program.cs:** Wires everything together using Dependency Injection.

--
test
test
test
