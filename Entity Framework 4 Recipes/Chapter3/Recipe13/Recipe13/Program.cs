using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe13
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
                context.ExecuteStoreCommand("delete from chapter3.media");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                context.Media.AddObject(new Article { Title = "Woodworkers' Favorite Tools" });
                context.Media.AddObject(new Article { Title = "Building a Cigar Chair" });
                context.Media.AddObject(new Video { Title = "Upholstering the Cigar Chair" });
                context.Media.AddObject(new Video { Title = "Applying Finish to the Cigar Chair" });
                context.Media.AddObject(new Picture { Title = "Photos of My Cigar Chair" });
                context.Media.AddObject(new Video { Title = "Tour of My Woodworking Shop" });
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                var allMedia = from m in context.Media
                               let mediatype = m is Article ? 1 :
                                               m is Video ? 2 : 3
                               orderby mediatype
                               select m;
                Console.WriteLine("All Media sorted by type...");
                foreach (var media in allMedia)
                {
                    Console.WriteLine("Title: {0} [{1}]", media.Title, media.GetType().Name);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
