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
                context.ExecuteStoreCommand("delete from chapter10.product");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var book1 = new Book { Title = "A Day in the Life", Publisher = "Colorful Press" };
                var book2 = new Book { Title = "Spring in October", Publisher = "AnimalCover Press" };
                var dvd1 = new DVD { Title = "Saving Sergeant Pepper", Rating = "G" };
                var dvd2 = new DVD { Title = "Around The Block", Rating = "PG-13" };
                context.Products.AddObject(book1);
                context.Products.AddObject(book2); 
                context.Products.AddObject(dvd1); 
                context.Products.AddObject(dvd2);
                context.SaveChanges();

                // update a book and delete a dvd
                book1.Title = "A Day in the Life of Sergeant Pepper";
                context.DeleteObject(dvd2);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("All Products");
                Console.WriteLine("============");
                foreach (var product in context.Products)
                {
                    if (product is Book)
                        Console.WriteLine("'{0}' published by {1}", product.Title, ((Book)product).Publisher);
                    else if (product is DVD)
                        Console.WriteLine("'{0}' is rated {1}", product.Title, ((DVD)product).Rating);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
