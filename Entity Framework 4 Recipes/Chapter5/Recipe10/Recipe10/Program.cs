using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe10
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
                context.ExecuteStoreCommand("delete from chapter5.movie");
                context.ExecuteStoreCommand("delete from chapter5.category");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var cat1 = new Category { Name = "Science Fiction", ReleaseType = "DVD" };
                var cat2 = new Category { Name = "Thriller", ReleaseType = "Blu-Ray" };
                new Movie { Name = "Return to the Moon", Category = cat1, Rating = "PG-13" };
                new Movie { Name = "Street Smarts", Category = cat2, Rating = "PG-13" };
                new Movie { Name = "Alien Revenge", Category = cat1, Rating = "R" };
                new Movie { Name = "Saturday Nights", Category = cat1, Rating = "PG-13" };
                context.Categories.AddObject(cat1);
                context.Categories.AddObject(cat2);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                // filter on ReleaseType and Rating
                // create collection of anonymous types
                var cats = from c in context.Categories
                           where c.ReleaseType == "DVD"
                           select new
                           {
                               category = c,
                               movies = c.Movies.Where(m => m.Rating == "PG-13")
                           };

                Console.WriteLine("PG-13 Movies Released on DVD");
                Console.WriteLine("============================");
                foreach (var c in cats)
                {
                    Category category = c.category;
                    Console.WriteLine("Category: {0}", category.Name);
                    foreach (var m in category.Movies)
                    {
                        Console.WriteLine("\tMovie: {0}", m.Name);
                    }
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
