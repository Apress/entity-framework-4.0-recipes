using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe8
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
                context.ExecuteStoreCommand("delete from chapter15.friend");
                context.ExecuteStoreCommand("delete from chapter15.residence");
                context.ExecuteStoreCommand("delete from chapter15.relative");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var res1 = new FriendResidence { Address = "123 Main", City = "Anytown", State = "CA", ZIP = "90210" };
                var res2 = new RelativeResidence { Address = "1200 East Street", City = "Big Town", State = "KS", ZIP = "66026" };
                var f = new Friend { Name = "Joan Roland", FriendResidence = res1 };
                var r = new Relative { Name = "Billy Miner", RelativeResidence = res2 };
                context.Friends.AddObject(f);
                context.Relatives.AddObject(r);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                context.ContextOptions.LazyLoadingEnabled = true;
                foreach (var r in context.Residences)
                {
                    if (r is FriendResidence)
                        Console.WriteLine("My friend {0} lives at: ", ((FriendResidence)r).Friend.Name);
                    else if (r is RelativeResidence)
                        Console.WriteLine("My relative {0} lives at: ", ((RelativeResidence)r).Relative.Name);
                    Console.WriteLine("\t{0}", r.Address);
                    Console.WriteLine("\t{0}, {1} {2}", r.City, r.State, r.ZIP);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
