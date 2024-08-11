using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace Project2
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Library library = new Library();

            Console.WriteLine(".............................................WELCOME TO SALWA's LIBRARY.............................................");
            bool exit = false;
            while (!exit)
            {
                LoadMenu();
                switch (Console.ReadLine())
                {
                    case "1":
                        library.addBook();
                        break;
                    case "2":
                        library.ViewBooks();
                        break;
                    case "3":
                        library.UpdateBook();
                        break;

                    case "4":
                        library.DeleteBook();
                        break;
                    case "5":
                        library.SearchBook();
                        break;
                        case "6":
                        library.SaveData();
                        break;
                    case "7":
                        Console.WriteLine("...............Thank you Using our Library...............");
                        Console.WriteLine("Exit from the Program");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Enter Valid Number");
                        break;
                }
            }
        }

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


        

    }
}
