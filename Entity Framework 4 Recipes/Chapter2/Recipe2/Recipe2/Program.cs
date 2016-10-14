using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Recipe2
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
                context.ExecuteStoreCommand("delete from chapter2.poem");
                context.ExecuteStoreCommand("delete from chapter2.poet");
                context.ExecuteStoreCommand("delete from chapter2.meter");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var poet = new Poet { FirstName = "John", LastName = "Milton" };
                var poem = new Poem { Title = "Paradise Lost" };
                var meter = new Meter { MeterName = "Iambic Pentameter" };
                poem.Meter = meter;
                poem.Poet = poet;
                context.Poems.AddObject(poem);
                poem = new Poem { Title = "Paradise Regained" };
                poem.Meter = meter;
                poem.Poet = poet;
                context.Poems.AddObject(poem);

                poet = new Poet { FirstName = "Lewis", LastName = "Carroll" };
                poem = new Poem { Title = "The Hunting of the Shark" };
                meter = new Meter { MeterName = "Anapestic Tetrameter" };
                poem.Meter = meter;
                poem.Poet = poet;
                context.Poems.AddObject(poem);

                poet = new Poet { FirstName = "Lord", LastName = "Byron" };
                poem = new Poem { Title = "Don Juan" };
                poem.Meter = meter;
                poem.Poet = poet;
                context.Poems.AddObject(poem);

                context.SaveChanges();

            }

            using (var context = new EFRecipesEntities())
            {
                context.ContextOptions.LazyLoadingEnabled = true;
                var poets = from p in context.Poets select p;
                foreach (var poet in poets)
                {
                    Console.WriteLine("{0} {1}", poet.FirstName, poet.LastName);
                    foreach (var poem in poet.Poems)
                    {
                        Console.WriteLine("\t{0} ({1})", poem.Title, poem.Meter.MeterName);
                    }
                }
            }

            // using our vwLibrary view
            using (var context = new EFRecipesEntities())
            {
                var items = from i in context.vwLibraries select i;
                foreach (var item in items)
                {
                    Console.WriteLine("{0} {1}", item.FirstName, item.LastName);
                    Console.WriteLine("\t{0} ({1})", item.Title, item.MeterName);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
