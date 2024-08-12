using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.IO;
using System.Reflection;
namespace Project2
{
    public class Library
    {
        private List<Book> booksList;

        private static Dictionary<int, string> GenreDic = new Dictionary<int, string>()
        {
            {1, "Romantic"},
            {2, "Fantasy"},
            {3, "History"},
            {4, "Classic"},
            {5, "Horror"},
            {6, "Others"}
        };
        public Library()
        {
            booksList = new List<Book>();
            ReadFile();
        }
        public void AddBook()
        {
            Console.WriteLine("Enter Book's Details you Want to Add :)");

            string bookAuthor = getValidAuthor();
            string bookTitle = getValidTitle();

            foreach (var book in booksList)
            {
                if (book.Author.Equals(bookAuthor, StringComparison.OrdinalIgnoreCase) && book.Title.Equals(bookTitle, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("--------------------------------------------------------");
                    Console.WriteLine("Can't add a new book with the same title and author name.");
                    Console.WriteLine("--------------------------------------------------------");

                    return;
                }
            }
            Console.Write("Book's Genre: ");
            var bookGenre = GenreMenu();

            int bookYear = getValidYearInput();

            Console.Write("Book's Summary: ");
            var bookSummary = Console.ReadLine();

            Book newBook = new Book(bookTitle, bookAuthor, bookGenre, bookYear, bookSummary);
            booksList.Add(newBook);

            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("Book added successfully.");
            Console.WriteLine("--------------------------------------------------------");

            SaveFile();
        }
        public void ViewBooks()
        {
            try
            {
                if (booksList.Count == 0)
                {
                    Console.WriteLine("No Books in the Library");
                }
                else
                {
                    foreach (var book in booksList)
                    {
                        book.toString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        public void UpdateBook()
        {
            if (booksList.Count == 0)
            {
                Console.WriteLine("No Books in the Libray ");
                return;
            }
            Console.WriteLine("------Updating Book ... loading");
            foreach (var bookTitle in booksList)
            {
                Console.WriteLine("--" + bookTitle.Title);
            }
            Console.WriteLine("Which book do you want to update?");
            var ubdatedTitleBook = Console.ReadLine();

            // Find all books with the matching title
            var booksWithTitle = booksList.Where(b => b.Title.Equals(ubdatedTitleBook, StringComparison.OrdinalIgnoreCase)).ToList();

            // If more than one book has the same title, ask for the author
            Book bookToUpdate = null;
            if (booksWithTitle.Count > 1)
            {
                string author = getValidAuthor();
                bookToUpdate = booksWithTitle.FirstOrDefault(b => b.Author.Equals(author, StringComparison.OrdinalIgnoreCase));
            }
            else if (booksWithTitle.Count == 1)
            {
                bookToUpdate = booksWithTitle.First();
            }

            if (bookToUpdate != null)
            {
                Console.WriteLine("Book Found: ");
                bookToUpdate.toString();

                bool exit = false;
                while (!exit)
                {
                    UpdateMenu();
                    switch (Console.ReadLine())
                    {
                        case "1":
                            string newAuthor = getValidAuthor();
                            bookToUpdate.Author = newAuthor;
                            break;

                        case "2":
                            string newTitle = getValidTitle();
                            bookToUpdate.Title = newTitle;
                            break;
                        case "3":
                            Console.WriteLine("Enter new Genre.");
                            var newGenre = GenreMenu();
                            bookToUpdate.Genre = newGenre;
                            break;
                        case "4":
                            Console.WriteLine("Enter new Year of Publication.");
                            int newYear = getValidYearInput();
                            bookToUpdate.YearOfPublication = newYear;

                            break;
                        case "5":
                            Console.WriteLine("Enter Updated Summary.");
                            string newSummary = Console.ReadLine();
                            bookToUpdate.Summary = newSummary;
                            break;
                        case "6":
                            Console.WriteLine("Update Done :)");
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Enter Valid Choice");
                            break;
                    }
                }

                SaveFile();
            }
            else
            {
                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine("Book not found");
                Console.WriteLine("--------------------------------------------------------");
            }
        }
        public void DeleteBook()
        {
            Console.WriteLine("Deleting Book... loading");
            int counter = 1;

            if (booksList.Count==0)
            {
                Console.WriteLine("No Books in the Libray ");
                return;
            }
            Console.WriteLine(".....Total Books in the Library.....");

            foreach (var book in booksList)
            {
                Console.WriteLine(counter + ". " + book.Title);
                counter++;
            }

            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("Which book do you want to delete?");
            var deletedBookTitle = Console.ReadLine();

            // Find all books with the matching title
            var booksWithTitle = booksList.Where(b => b.Title.Equals(deletedBookTitle, StringComparison.OrdinalIgnoreCase)).ToList();

            // If more than one book has the same title, ask for the author
            Book bookToRemove = null;
            if (booksWithTitle.Count > 1)
            {
                string author = getValidAuthor();
                bookToRemove = booksWithTitle.FirstOrDefault(b => b.Author.Equals(author, StringComparison.OrdinalIgnoreCase));
            }
            else if (booksWithTitle.Count == 1)
            {
                bookToRemove = booksWithTitle.First(); 
            }

            if (bookToRemove != null)
            {
                booksList.Remove(bookToRemove);
                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine("Book deleted successfully.");
                Console.WriteLine("--------------------------------------------------------");
            }
            else
            {
                Console.WriteLine("Book not found.");
            }

            SaveFile();
        }
        public void SearchBook()
        {
            bool exit = false;
            while (!exit)
            {
                SearchMenu();
                switch (Console.ReadLine())
                {
                    case "1":
                        string searchAuthor = getValidAuthor();
                        SearchByAuthor(searchAuthor);
                        break;

                    case "2":
                        Console.Write("Enter Title Name: ");
                        var searchTitle = Console.ReadLine();
                        SearchByTitle(searchTitle);
                        break;

                    case "3":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Enter Valid Choice");
                        break;
                }
            }
        }
        private void ReadFile()
        {
            string filePath = @"C:\Users\Administrator\Desktop\Training\Training\Stage-2\Project2\Project2\Books.txt";
            try
            {
                string[] lines = File.ReadAllLines(filePath).Skip(3).ToArray(); 
                Book currentBook = null;

                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    if (line.StartsWith("Title :"))
                    {
                        if (currentBook != null)
                        {
                            booksList.Add(currentBook);
                        }
                        currentBook = new Book();
                        currentBook.Title = line.Substring("Title :".Length).Trim();
                    }
                    else if (currentBook != null)
                    {
                        if (line.StartsWith("Author :"))
                        {
                            currentBook.Author = line.Substring("Author :".Length).Trim();
                        }
                        else if (line.StartsWith("Genre :"))
                        {
                            currentBook.Genre = line.Substring("Genre :".Length).Trim();
                        }
                        else if (line.StartsWith("Year of Publication :"))
                        {
                            currentBook.YearOfPublication = int.Parse(line.Substring("Year of Publication :".Length).Trim());
                        }
                        else if (line.StartsWith("Summary :"))
                        {
                            currentBook.Summary = line.Substring("Summary :".Length).Trim();
                        }
                    }

                    if (line.StartsWith("----") && currentBook != null)
                    {
                        booksList.Add(currentBook);
                        currentBook = null;
                    }
                }

                // Add the last book if not added
                if (currentBook != null)
                {
                    booksList.Add(currentBook);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        public void SaveFile()
        {
            string filepath = @"C:\Users\Administrator\Desktop\Training\Training\Stage-2\Project2\Project2\Books.txt";
            using (StreamWriter writer = new StreamWriter(filepath))
            {
                writer.WriteLine("--------------------------------------------------------------------------------------------------------------------");
                writer.WriteLine("                                                  WELCOME TO SALWA's LIBRARY");
                writer.WriteLine("--------------------------------------------------------------------------------------------------------------------");

                foreach (var book in booksList)
                {
                    writer.WriteLine("Title : " + book.Title);
                    writer.WriteLine("Author : " + book.Author);
                    writer.WriteLine("Genre : " + book.Genre);
                    writer.WriteLine("Year of Publication : " + book.YearOfPublication);
                    writer.WriteLine("Summary : " + book.Summary);
                    writer.WriteLine("--------------------------------------------------------------------------------------------------------------------");

                }
            }
        }
        public void SaveJsonFile()
        {
            string jsonFilePath = @"C:\Users\Administrator\Desktop\Training\Training\Stage-2\Project2\Project2\Books.json";
            try
            {
                string jsonContent;
                if (File.Exists(jsonFilePath))
                {
                    string updatedJsonContent = JsonSerializer.Serialize(booksList, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(jsonFilePath, updatedJsonContent);
                    Console.WriteLine("--------------------------------------------------------");
                    Console.WriteLine("Data Exported to JSON File ");
                    Console.WriteLine("--------------------------------------------------------");

                }
                else
                {
                    jsonContent = "[]";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        private void UpdateMenu()
        {
            Console.WriteLine("1] Update Author Name.");
            Console.WriteLine("2] Update Book Title.");
            Console.WriteLine("3] Update Book Genre.");
            Console.WriteLine("4] Update Year of Publication.");
            Console.WriteLine("5] Update Book Summary.");
            Console.WriteLine("6] Exit.");

        }
        private void SearchMenu()
        {
            Console.WriteLine("1] Search by Author Name.");
            Console.WriteLine("2] Search by Title Name.");
            Console.WriteLine("3] Exit.");
        }
        private string GenreMenu()
        {
            Console.WriteLine("Available Genres:");
            foreach (var genre in Library.GenreDic)
            {
                Console.WriteLine($"{genre.Key}. {genre.Value}");
            }
            Console.Write("\nEnter the number corresponding to your desired genre: ");
            int selectedGenreKey;
            while (!int.TryParse(Console.ReadLine(), out selectedGenreKey) || !Library.GenreDic.ContainsKey(selectedGenreKey))
            {
                Console.Write("Invalid input. Please enter a valid number from the list : ");
            }
            return Library.GenreDic[selectedGenreKey];
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        private void SearchByAuthor(string Author)
        {
            Book foundBookByAuthor = null;
            foreach (var book in booksList)
            {
                if (book.Author.Contains(Author))
                {
                    foundBookByAuthor = book;
                    book.toString();
                }
            }
            if (foundBookByAuthor == null)
            {
                Console.WriteLine("Book not found");
            }
        }
        private void SearchByTitle(string Title)
        {
            Book foundBookByTitle = null;
            foreach (var book in booksList)
            {
                if (book.Title.Contains(Title))
                {
                    foundBookByTitle = book;
                    book.toString();
                }
            }
            if (foundBookByTitle == null)
            {
                Console.WriteLine("Book not found");
            }
        }
        private bool CheckYear(int year)
        {
            if (year > DateTime.Now.Year)
            {
                return false;
            }
            else
                return true;

        }
        private int getValidYearInput()
        {
            int bookYear = 0;
            bool isValidYear = false;
            int earliestYear = 1450;

            while (!isValidYear)
            {
                Console.Write("Year of Publication: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out bookYear))
                {
                    if (bookYear >= earliestYear && bookYear <= DateTime.Now.Year)
                    {
                        isValidYear = true;
                    }
                    else
                    {
                        Console.WriteLine($"Not a valid year.");
                    }
                }
                else
                {
                    Console.WriteLine("Not a valid year. Please enter a numeric value.");
                }
            }

            return bookYear;
        }
        public static string getValidAuthor()
        {
            string bookAutohr;
            do
            {
                Console.Write("Enter Author Name: ");
                bookAutohr = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(bookAutohr) || bookAutohr.Any(char.IsDigit))
                {
                    Console.WriteLine("Write Valid Name");
                }
            }
            while (string.IsNullOrWhiteSpace(bookAutohr) || bookAutohr.Any(char.IsDigit));

            return bookAutohr;
        }
        private string getValidTitle()
        {
            string bookTitle;
            do
            {
                Console.Write("Book's Title : ");
                bookTitle = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(bookTitle))
                {
                    Console.WriteLine("Write Valid Title");
                }
            }
            while (string.IsNullOrWhiteSpace(bookTitle));

            return bookTitle;
        }

    }

}
