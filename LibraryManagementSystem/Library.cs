namespace LibraryManagementSystem
{
    // Clss representing a library, implementing the ILibraryOperations interface
    public class Library : ILibraryOperations
    {
        private List<Book> _books;
        private List<User> _users;

        // --------------------------------//
        // добавить коментарий после реализации подписки на события
        public delegate void BookBorrowedEventHandler(object sender, BookEventArgs e);
        public event BookBorrowedEventHandler? BookBorrowed;

        public delegate void BookReturnedEventHandler(object sender, BookEventArgs e);
        public event BookReturnedEventHandler? BookReturned;

        public Library()
        {
            _books = new List<Book>();
            _users = new List<User>();
        }

        // Method to add a book to the library
        public void AddBook(Book book)
        {
            // Check if the book already exists in the library
            if (_books.Any(b => b == book))
            {
                Console.WriteLine($"Book with ISBN {book.ISBN} already exists.");
                return;
            }
            _books.Add(book);
            Console.WriteLine($"Book '{book.Title}' added to the library.");
        }

        // Method to remove a book from the library
        public bool RemoveBook(string isbn)
        {
            var BookToRemove = _books.FirstOrDefault(b => b.ISBN == isbn); // Find the book by overridden operator == method

            if (BookToRemove != null)
            {
                // Checking that the book is not borrowed before removing it
                if (BookToRemove.AvailableCopies < BookToRemove.TotalCopies)
                {
                    Console.WriteLine($"Cannot remove book '{BookToRemove.Title}' as it has been borrowed.");
                    return false;
                }
                _books.Remove(BookToRemove);
                Console.WriteLine($"Book '{BookToRemove.Title}' removed from the library.");
                return true;
            }
            Console.WriteLine($"Book with ISBN {isbn} not found.");
            return false;
        }

        // Method to get a book by its ISBN
        public Book? GetBookByIsbn(string isbn)
        {
            var book = _books.FirstOrDefault(b => b.ISBN == isbn); // Find the book by ISBN
            if (book != null)
            {
                return book;
            }
            Console.WriteLine($"Book with ISBN {isbn} not found.");
            return null;
        }

        public List<Book> GetAllBooks()
        {
            return _books;
        }

        // Method to search for books by title, author, or ISBN
        public List<Book> SearchBooks(string query)
        {
            query = query.ToLower(); // Normalize the search query to lowercase
            return _books.Where( b => b.Title.ToLower().Contains(query) || // Search by title, author, or ISBN
                                      b.Author.ToLower().Contains(query) || 
                                      b.ISBN.Contains(query))
                         .ToList(); // Return a list of books that match the search query
        }

        // Method to add a user to the library
        public void AddUser(User user)
        {
            if (_users.Any(u => u == user)) // Check if the user already exists using overridden operator == method
            {
                Console.WriteLine($"User with ID {user.UserId} already exists.");
                return;
            }
            _users.Add(user);
            Console.WriteLine($"User '{user.FirstName} {user.LastName}' added to the library.");
        }
        // Method to remove a user from the library
        public bool RemoveUser(string userId)
        {
            var userToRemove = _users.FirstOrDefault(u => u.UserId == userId); // Find the user by UserId
            if (userToRemove != null)
            {
                if (userToRemove.BorrowedBooks.Any()) // Check if the user has borrowed books
                {
                    Console.WriteLine($"Cannot remove user '{userToRemove.FirstName} {userToRemove.LastName}' as they have borrowed books.");
                    return false;
                }
                _users.Remove(userToRemove);
                Console.WriteLine($"User '{userToRemove.FirstName} {userToRemove.LastName}' removed from the library.");
                return true;
            }
            Console.WriteLine($"User with ID {userId} not found.");
            return false;
        }

        public User? GetUserById(string userId)
        {
            var user = _users.FirstOrDefault(u => u.UserId == userId); // Find the user by UserId
            if (user != null)
            {
                return user;
            }
            Console.WriteLine($"User with ID {userId} not found.");
            return null;
        }

        public List<User> GetAllUsers()
        {
            return _users;
        }

        public bool BorrowBook(string isbn, string userId)
        {
            var book = GetBookByIsbn(isbn);
            var user = GetUserById(userId);

            if (book == null || user == null)
            {
                Console.WriteLine("Book or User not found.");
                return false;
            }
            if (book.AvailableCopies == 0) // Checking for book availability
            {
                Console.WriteLine($"No available copies of '{book.Title}' to borrow.");
                return false;
            }
            if (!user.BorrowedBooks.Contains(book)) // A simple check to avoid issuing the same book multiple times
            {
                Console.WriteLine($"User '{user.FirstName}' already borrowed '{book.Title}'.");
                return false;
            }
            book.AvailableCopies--;
            user.BorrowedBooks.Add(book);
            Console.WriteLine($"Book '{book.Title}' borrowed by '{user.FirstName} {user.LastName}'.");
            // --------------------------------//
            BookBorrowed?.Invoke(this, new BookEventArgs(book, user));
            return true;
        }

        public bool ReturnBook(string isbn, string userId)
        {
            var book = GetBookByIsbn(isbn);
            var user = GetUserById(userId);

            if (book == null || user == null)
            {
                Console.WriteLine("Book or User not found.");
                return false;
            }
            if (!user.BorrowedBooks.Contains(book)) // A simple check to avoid issuing the same book multiple times
            {
                Console.WriteLine($"User '{user.FirstName}' did not borrow '{book.Title}'.");
                return false;
            }
            book.AvailableCopies++; // We are increasing the number of books available in the library, since the user has successfully returned it.
            user.BorrowedBooks.Remove(book);
            Console.WriteLine($"Book '{book.Title}' returned by '{user.FirstName} {user.LastName}'.");
            // --------------------------------//
            BookReturned?.Invoke(this, new BookEventArgs(book, user));
            return true;
        }
    }
}
