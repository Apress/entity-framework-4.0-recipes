using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Recipe7
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
                context.ExecuteStoreCommand("delete from chapter15.residence");
                context.ExecuteStoreCommand("delete from chapter15.relative");
                context.ExecuteStoreCommand("delete from chapter15.friend");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var res1 = new Residence { Address = "123 Main", City = "Anytown", State = "CA", Zip = "90210" };
                var res2 = new Residence { Address = "1200 East Street", City = "Big Town", State = "KS", Zip = "66026" };
                var f = new Friend { Name = "Joan Roland"};
                f.Residences.Add(res1);
                var r = new Relative { Name = "Billy Miner"};
                r.Residences.Add(res2);
                context.Friends.AddObject(f);
                context.Relatives.AddObject(r);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                context.ContextOptions.LazyLoadingEnabled = true;
                foreach (var r in context.Residences)
                {
                    if (r.Friends != null)
                        Console.WriteLine("My friend {0} lives at: ", r.Friends.Name);
                    else if (r.Relatives != null)
                        Console.WriteLine("My relative {0} lives at: ", r.Relatives.Name);
                    Console.WriteLine("\t{0}", r.Address);
                    Console.WriteLine("\t{0}, {1} {2}", r.City, r.State, r.Zip);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }

    public partial class EFRecipesEntities
    {
        partial void OnContextCreated()
        {
            this.SavingChanges += (o, s) =>
            {
                var residences =
                this.ObjectStateManager.GetObjectStateEntries(
                    EntityState.Modified |
                     EntityState.Added)
                     .Where(entry => entry.Entity is Residence)
                     .Select(entry => entry.Entity as Residence);

                foreach (var residence in residences) {
                    if ((residence.FriendId.HasValue || residence.Friends != null) &&
                        (residence.RelativeId.HasValue || residence.Relatives != null)) 
                    {
                        throw new ApplicationException("Relative or friend?");
                    }
                }
            };
        }
    }
}
