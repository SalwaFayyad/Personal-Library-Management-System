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
        public static Dictionary<int, string> Genre = new Dictionary<int, string>()
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
            Console.Write("Book's Author : ");
            var bookAutohr = Console.ReadLine();
            Console.Write("Book's Title : ");
            var bookTitle = Console.ReadLine();
            Console.Write("Book's Genre :");
            var bookGenre = GenreMenu();


            Console.Write("Year of Publication : ");
            var bookYear = Console.ReadLine();

            while (Int32.Parse(bookYear) > 2024)
            {
                Console.WriteLine("Not Valid Year");
                Console.Write("Year of Publication : ");
                bookYear = Console.ReadLine();
            }
            Console.Write("Book's Summary : ");
            var bookSummary = Console.ReadLine();

            Book book1 = new Book(bookTitle, bookAutohr, bookGenre, Int32.Parse(bookYear), bookSummary);
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
                Console.WriteLine("--"+ bookTitle.Title);
            }
            Console.WriteLine("................................");
            Console.WriteLine("Which book do you want to update?");

            
            var titlebook = Console.ReadLine();
            Book BooktoUpdate = null;
            bool found = false;

            while (!found)
            {
                foreach (var bookTitle in booksList)
                {
                    if (bookTitle.Title.Equals(titlebook, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Book Founded : ");
                        BooktoUpdate = bookTitle;
                        bookTitle.toString();
                        bool exit = false;
                        while (!exit)
                        {
                            updateMenu();
                            found = true;
                            switch (Console.ReadLine())
                            {
                                case "1":
                                    Console.WriteLine("Enter new author name.");
                                    string newAuthor = Console.ReadLine();
                                    bookTitle.Author = newAuthor;
                                    break;

                                case "2":
                                    Console.WriteLine("Enter new Title name.");
                                    string newTitle = Console.ReadLine();
                                    bookTitle.Title = newTitle;
                                    break;
                                case "3":
                                    Console.WriteLine("Enter new Genre.");
                                    string newGenre = Console.ReadLine();
                                    bookTitle.SelectedGenre = newGenre;
                                    break;
                                case "4":
                                    Console.WriteLine("Enter new Year of Publication.");
                                    string newYear = Console.ReadLine();
                                    if (checkYear(Int32.Parse(newYear)))
                                    {
                                        bookTitle.YearOfPublication = Int32.Parse(newYear);
                                    }
                                    else
                                    {
                                        Console.WriteLine("not valid year");
                                    }
                                    break;
                                case "5":
                                    Console.WriteLine("Enter Updated Summary.");
                                    string newSummary = Console.ReadLine();
                                    bookTitle.Summary = newSummary;
                                    break;
                                case "6":
                                    Console.WriteLine("Updated Done :)");
                                    exit = true;
                                    break;
                                default:
                                    Console.WriteLine("Enter Valid Choice");
                                    break;
                            }
                        }

                        updateJsonFile();
                        SaveData();
                        break;
                    }
                }
            }
            if (found == false)
            {
                    Console.WriteLine("Book not found");  
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
                        Book searchBook = null;
                        foreach (var bookTitle in booksList)
                        {
                            if (bookTitle.Author.Equals(searchAuthor, StringComparison.OrdinalIgnoreCase))
                            {
                                searchBook = bookTitle;
                                bookTitle.toString();
                            }

                        }
                        break;
                    case "2":
                        Console.Write("Enter Title Name: ");
                        var searchTitle = Console.ReadLine();
                        Book searchsBook = null;
                        foreach (var bookTitle in booksList)
                        {
                            if (bookTitle.Title.Equals(searchTitle, StringComparison.OrdinalIgnoreCase))
                            {
                                searchsBook = bookTitle;
                                bookTitle.toString();
                            }
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
                writer.WriteLine("\n\n...............................................WELCOME TO SALWA's LIBRARY...............................................");
                foreach (var book in booksList)
                {
                    writer.WriteLine("--------------------------------------------------------------------------------------------------------------------");
                    writer.WriteLine(" Book [ " +i+" ]");
                    writer.WriteLine("--------------------------------------------------------------------------------------------------------------------");
                    i++;
                    writer.WriteLine("Book's Author : " + book.Author);
                    writer.WriteLine("Title : " + book.Title);
                    writer.WriteLine("Genre : " + book.SelectedGenre);
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
                    Console.WriteLine("New books have been added and saved to the JSON file.");
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
        /// <summary>
        /// Menus
        /// </summary>
        public void updateMenu()
        {
            Console.WriteLine("1] Update Author Name.");
            Console.WriteLine("2] Update Book Title.");
            Console.WriteLine("3] Update Book Genre.");
            Console.WriteLine("4] Update Year of Publication.");
            Console.WriteLine("5] Update Book Summary.");
            Console.WriteLine("6] Exit.");

        }

        public void searchMenu()
        {
            Console.WriteLine("1] Search by Author Name.");
            Console.WriteLine("2] Search by Title Name.");
            Console.WriteLine("6] Exit.");
        }

        public static string GenreMenu()
        {
            Console.WriteLine("Available Genres:");
            foreach (var genre in Genre)
            {
                Console.WriteLine($"{genre.Key}. {genre.Value}");
            }
            Console.Write("\nEnter the number corresponding to your desired genre: ");
            int selectedGenreKey;
            while (!int.TryParse(Console.ReadLine(), out selectedGenreKey) || !Genre.ContainsKey(selectedGenreKey))
            {
                Console.Write("Invalid input. Please enter a valid number from the list : ");
            }
            return Genre[selectedGenreKey];
        }

        public static bool checkYear(int year)
        {
            if (year > DateTime.Now.Year)
            {
                return false;
            }
            else
                return true;

        }
    }

}
