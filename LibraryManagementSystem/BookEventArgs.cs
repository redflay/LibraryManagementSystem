namespace LibraryManagementSystem
{
    // Class for event arguments that carries data when an event is triggered.
    public class BookEventArgs : EventArgs
    {
        public Book Book { get; } // Property for storing the book involved in the event
        public User User { get; } // Property for storing the user involved in the event

        /// <summary>
        /// Constructor for BookEventArgs that initializes the Book and User properties.
        /// </summary>
        /// <param name="book"></param>
        /// <param name="user"></param>
        public BookEventArgs(Book book, User user)
        {
            Book = book;
            User = user;
        }
    }
}
