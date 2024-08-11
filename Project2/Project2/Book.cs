using System;
using System.Collections.Generic;

namespace Project2
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string SelectedGenre { get; set; } 
        public int YearOfPublication { get; set; }
        public string Summary { get; set; }

        public Book(string title, string author, string selectedGenre, int yearOfPublication, string summary = "")
        {
            Title = title;
            Author = author;
            SelectedGenre = selectedGenre;
            YearOfPublication = yearOfPublication;
            Summary = summary;
        }

        public void toString()
        {
            Console.WriteLine("................................");
            Console.WriteLine(" Book's Author : " + Author);
            Console.WriteLine(" Title : " + Title);
            Console.WriteLine(" Genre : " + SelectedGenre);
            Console.WriteLine(" Year of Publication : " + YearOfPublication);
            Console.WriteLine(" Summary : " + Summary);
            Console.WriteLine("................................\n");
        }
    }
}
