namespace LibraryManagementSystem
{
    // Interface for library operations, defining methods for managing library items and users
    public interface ILibraryOperations
    {
        // Methods for managing books
        void AddBook(Book book);
        bool RemoveBook(string isbn);
        Book? GetBookByIsbn(string isbn);
        List<Book> GetAllBooks();
        List<Book> SearchBooks(string query);

        bool BorrowBook(string isbn, string userId);
        bool ReturnBook(string isbn, string userId);

        // Methods for managing users
        void AddUser(User user);
        bool RemoveUser(string userId);
        User? GetUserById(string userId);
        List<User> GetAllUsers();
    }
}
