namespace LibraryManagementSystem
{
    // Book class inherits from LibraryItem and represents a book in the library system
    public class Book : LibraryItem
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublicationYear { get; set; }
        public string ISBN { get; set; }
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }

        public Book(string title, string author, int publicationYear, string isbn, int totalCopies)
        {
            Title = title;
            Author = author;
            PublicationYear = publicationYear;
            ISBN = isbn;
            TotalCopies = totalCopies;
            AvailableCopies = totalCopies; // initially all copies are available
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo(); // call the base class method to display the ID
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Author: {Author}");
            Console.WriteLine($"Publication Year: {PublicationYear}");
            Console.WriteLine($"ISBN: {ISBN}");
            Console.WriteLine($"Total Copies: {TotalCopies}");
            Console.WriteLine($"Available Copies: {AvailableCopies}");
        }

        // Override Equals method to compare Book objects based on their properties
        public static bool operator ==(Book? book1, Book? book2)
        {
            if (ReferenceEquals(book1, null)) return ReferenceEquals(book2, null); // if both are null, they are equal
            return book1.Equals(book2);
        }

        public static bool operator !=(Book? book1, Book? book2)
        {
            return !(book1 == book2);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Book other = (Book)obj;
            return ISBN == other.ISBN;
        }
        /// <summary>
        /// Computes a hash code for the current Book instance, based on its ISBN.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return ISBN.GetHashCode();
        }
    }
}
