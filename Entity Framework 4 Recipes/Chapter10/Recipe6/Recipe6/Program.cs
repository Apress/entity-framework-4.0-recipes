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
                context.ExecuteStoreCommand("delete from chapter10.magazine");
                context.ExecuteStoreCommand("delete from chapter10.dvd");
                context.ExecuteStoreCommand("delete from chapter10.media");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                context.MediaSet.AddObject(new Magazine { Title = "Field and Stream", PublicationDate = DateTime.Parse("6/12/1945") });
                context.MediaSet.AddObject(new Magazine { Title = "National Geographic", PublicationDate = DateTime.Parse("7/15/1976") });
                context.MediaSet.AddObject(new DVD { Title = "Harmony Road", PlayTime = "2 hours, 30 minutes" });
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                var allMedia = context.GetAllMedia();
                Console.WriteLine("All Media");
                Console.WriteLine("=========");
                foreach (var m in allMedia)
                {
                    if (m is Magazine)
                        Console.WriteLine("{0} Published: {1}", m.Title, ((Magazine)m).PublicationDate.ToShortDateString());
                    else if (m is DVD)
                        Console.WriteLine("{0} Play Time: {1}", m.Title, ((DVD)m).PlayTime);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
