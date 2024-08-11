using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
namespace Project2
{

    public class Library
    {
        public List<Book> booksList;

        public static Dictionary<int, string> GenreDic = new Dictionary<int, string>()
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
            string jsonFilePath = @"C:\Users\Administrator\Desktop\Training\Training\Stage-2\Project2\Project2\Books.json";
            try
            {
                string jsonContent = File.ReadAllText(jsonFilePath);
                booksList = JsonSerializer.Deserialize<List<Book>>(jsonContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Add a Book
        /// </summary>
        public void addBook()
        {
            Console.WriteLine("Enter Book's Details you Want Add :)");
            string bookAuthor = getValidAuthor();
  
            string bookTitle = getValidTitle();

            Console.Write("Book's Genre :");
            var bookGenre = GenreMenu();

            int bookYear = GetValidYearInput();

            Console.Write("Book's Summary : ");
            var bookSummary = Console.ReadLine();

            Book book1 = new Book(bookTitle, bookAuthor, bookGenre,bookYear, bookSummary);
            booksList.Add(book1);
            Console.WriteLine("................................");
            Console.WriteLine("Book added successfully.");
            Console.WriteLine("................................");

            updateJsonFile();
            SaveData();
        }

        /// <summary>
        /// View Books
        /// </summary>
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

        /// <summary>
        /// Update Books
        /// </summary>
        public void UpdateBook()
        {
            Console.WriteLine("------Updating Book ... loading");
            foreach (var bookTitle in booksList)
            {
                Console.WriteLine("--" + bookTitle.Title);
            }
            Console.WriteLine("................................");
            Console.WriteLine("Which book do you want to update?");

            var titlebook = Console.ReadLine();
            Book bookToUpdate = booksList.FirstOrDefault(b => b.Title.Equals(titlebook, StringComparison.OrdinalIgnoreCase));

            if (bookToUpdate != null)
            {
                Console.WriteLine("Book Found: ");
                bookToUpdate.toString();

                bool exit = false;
                while (!exit)
                {
                    updateMenu();
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
                            int newYear = GetValidYearInput();
                            bookToUpdate.YearOfPublication= newYear;

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

                updateJsonFile();
                SaveData();
            }
            else
            {
                Console.WriteLine("................................");
                Console.WriteLine("Book not found");
                Console.WriteLine("................................");
            }
        }

        public void DeleteBook()
        {
            Console.WriteLine("Deleting Book ... loading");
            int counter = 1;
            Console.WriteLine(".....Total Books in the Library.....");
            foreach (var bookTitle in booksList)
            {
                Console.WriteLine(counter + ". " + bookTitle.Title);
                counter++;
            }
            Console.WriteLine("................................");
            Console.WriteLine("Which book do you want to delete?");
            var deletedBook = Console.ReadLine();

            Book bookToRemove = null;
            foreach (var bookTitle in booksList)
            {
                if (bookTitle.Title.Equals(deletedBook, StringComparison.OrdinalIgnoreCase))
                {
                    bookToRemove = bookTitle;
                    break;
                }
            }

            if (bookToRemove != null)
            {
                booksList.Remove(bookToRemove);
                Console.WriteLine("Book deleted successfully.");
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
            updateJsonFile();
            SaveData();
        }

        public void SearchBook()
        {
            bool exit = false;
            while (!exit)
            {
                searchMenu();
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Write("Enter Author Name: ");
                        var searchAuthor = Console.ReadLine();
                        Book foundBookByAuthor = null;
                        foreach (var book in booksList)
                        {
                            if (book.Author.Equals(searchAuthor, StringComparison.OrdinalIgnoreCase))
                            {
                                foundBookByAuthor = book;
                                book.toString();
                            }
                        }
                        if (foundBookByAuthor == null)
                        {
                            Console.WriteLine("Book not found");
                        }
                        break;

                    case "2":
                        Console.Write("Enter Title Name: ");
                        var searchTitle = Console.ReadLine();
                        Book foundBookByTitle = null;
                        foreach (var book in booksList)
                        {
                            if (book.Title.Equals(searchTitle, StringComparison.OrdinalIgnoreCase))
                            {
                                foundBookByTitle = book;
                                book.toString();
                            }
                        }
                        if (foundBookByTitle == null)
                        {
                            Console.WriteLine("Book not found");
                        }
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


        public void SaveData()
        {
            string filepath = @"C:\Users\Administrator\Desktop\Training\Training\Stage-2\Project2\Project2\Books.txt";
            using (StreamWriter writer = new StreamWriter(filepath))
            {
                int i = 1;
                writer.WriteLine("--------------------------------------------------------------------------------------------------------------------");
                writer.WriteLine("                                                  WELCOME TO SALWA's LIBRARY");
                foreach (var book in booksList)
                {
                    writer.WriteLine("--------------------------------------------------------------------------------------------------------------------");
                    writer.WriteLine(" Book [ " +i+" ]");
                    writer.WriteLine("--------------------------------------------------------------------------------------------------------------------");
                    i++;
                    writer.WriteLine("Book's Author : " + book.Author);
                    writer.WriteLine("Title : " + book.Title);
                    writer.WriteLine("Genre : " + book.Genre);
                    writer.WriteLine("Year of Publication : " + book.YearOfPublication);
                    writer.WriteLine("Summary : " + book.Summary);
                }
            }

            Console.WriteLine("Books have been saved to the file.");
        }

        public void updateJsonFile()
        {
            string jsonFilePath = @"C:\Users\Administrator\Desktop\Training\Training\Stage-2\Project2\Project2\Books.json";
            try
            {
                string jsonContent;
                if (File.Exists(jsonFilePath))
                {
                    string updatedJsonContent = JsonSerializer.Serialize(booksList, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(jsonFilePath, updatedJsonContent);
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

        public static void LoadMenu()
        {
            Console.WriteLine(" 1] Add Book to the Library");
            Console.WriteLine(" 2] View All Books in the Library");
            Console.WriteLine(" 3] Update Book Details");
            Console.WriteLine(" 4] Delete a Book");
            Console.WriteLine(" 5] Search for a Book");
            Console.WriteLine(" 6] Save Library Data");
            Console.WriteLine(" 7] Exit");
            Console.WriteLine("................................");
            Console.Write("Enter the Choice : ");
        }
        public static void updateMenu()
        {
            Console.WriteLine("1] Update Author Name.");
            Console.WriteLine("2] Update Book Title.");
            Console.WriteLine("3] Update Book Genre.");
            Console.WriteLine("4] Update Year of Publication.");
            Console.WriteLine("5] Update Book Summary.");
            Console.WriteLine("6] Exit.");

        }

        public static void searchMenu()
        {
            Console.WriteLine("1] Search by Author Name.");
            Console.WriteLine("2] Search by Title Name.");
            Console.WriteLine("3] Exit.");
        }

        public static string GenreMenu()
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

        //////////////////////////////////////////////////////////////////////////////////////////////////////



        public static bool checkYear(int year)
        {
            if (year > DateTime.Now.Year)
            {
                return false;
            }
            else
                return true;

        }

        public static int GetValidYearInput()
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
                Console.Write("Book's Author : ");
                bookAutohr = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(bookAutohr) || bookAutohr.Any(char.IsDigit))
                {
                    Console.WriteLine("Write Valid Name");
                }
            }
            while (string.IsNullOrWhiteSpace(bookAutohr) || bookAutohr.Any(char.IsDigit));

            return bookAutohr;
        }

        public static string getValidTitle()
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
