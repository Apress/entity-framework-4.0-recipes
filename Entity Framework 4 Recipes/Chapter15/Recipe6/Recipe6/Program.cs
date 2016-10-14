using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe6
{
    class Program
    {
        static void Main(string[] args)
        {
            Cleanup();
            RunExample();
        }

        static void Cleanup()
        {
            using (var context = new EFRecipesEntities())
            {
                context.ExecuteStoreCommand("delete from chapter15.authorbook");
                context.ExecuteStoreCommand("delete from chapter15.book");
                context.ExecuteStoreCommand("delete from chapter15.author");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var b1 = new Book { ISBN = "978-1-847193-81-0", Title = "jQuery Reference Guide" };
                var b2 = new Book { ISBN = "978-1-29298333", Title = "jQuery Tips and Tricks" };
                var b3 = new Book { ISBN = "978-1033988429", Title = "Silverlight 2" };
                var a1 = new Author { Name = "Jonathan Chaffer" };
                var a2 = new Author { Name = "Chad Campbell" };
                var ab1 = new AuthorBook { Author = a1, Book = b1, IsPrimary = true };
                var ab2 = new AuthorBook { Author = a1, Book = b2, IsPrimary = false };
                var ab3 = new AuthorBook { Author = a2, Book = b3, IsPrimary = false };
                context.AuthorBooks.AddObject(ab1);
                context.AuthorBooks.AddObject(ab2);
                context.AuthorBooks.AddObject(ab3);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                context.ContextOptions.LazyLoadingEnabled = true;
                Console.WriteLine("Authors and Their Books...");
                foreach (var author in context.Authors)
                {
                    Console.WriteLine("{0}", author.Name);
                    foreach (var book in author.Books)
                    {
                        Console.WriteLine("\t{0}, ISBN = {1}", book.Title, book.ISBN);
                    }
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
