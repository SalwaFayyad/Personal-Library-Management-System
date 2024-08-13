using System;

namespace Project2
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; } 
        public int YearOfPublication { get; set; }
        public string Summary { get; set; }

        public Book()
        {

        }
        public Book(string title, string author, string genre, int yearOfPublication, string summary = "")
        {
            Title = title;
            Author = author;
            Genre = genre;
            YearOfPublication = yearOfPublication;
            Summary = summary;
        }

        public void toString()
        {
            Console.WriteLine("................................");
            Console.WriteLine(" Book's Author : " + Author);
            Console.WriteLine(" Title : " + Title);
            Console.WriteLine(" Genre : " + Genre);
            Console.WriteLine(" Year of Publication : " + YearOfPublication);
            Console.WriteLine(" Summary : " + Summary);
            Console.WriteLine("................................\n");
        }
    }
}
