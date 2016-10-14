using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Recipe18
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
                context.ExecuteStoreCommand("delete from chapter3.patron");
            }
        }

        public enum SponsorTypes
        {
            ContributesMoney = 1,
            Volunteers = 2,
            IsABoardMember = 4
        };

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                context.Patrons.AddObject(new Patron { Name = "Jill Roberts", SponsorType = (int)SponsorTypes.ContributesMoney });
                context.Patrons.AddObject(new Patron { Name = "Ryan Keyes", SponsorType = (int)(SponsorTypes.ContributesMoney | SponsorTypes.IsABoardMember)});
                context.Patrons.AddObject(new Patron {Name = "Karen Rosen", SponsorType = (int)SponsorTypes.Volunteers});
                context.Patrons.AddObject(new Patron {Name = "Steven King", SponsorType = (int)(SponsorTypes.ContributesMoney | SponsorTypes.Volunteers)});
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("Using LINQ...");
                var sponsors = from p in context.Patrons
                               where (p.SponsorType & (int)SponsorTypes.ContributesMoney) != 0
                               select p;
                Console.WriteLine("Patrons who contribute money");
                foreach (var sponsor in sponsors)
                {
                    Console.WriteLine("\t{0}", sponsor.Name);
                }
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("\nUsing Entity SQL...");
                var esql = @"select value p from Patrons as p
                             where BitWiseAnd(p.SponsorType, @type) <> 0";
                var sponsors = context.CreateQuery<Patron>(esql, new ObjectParameter("type", (int)SponsorTypes.ContributesMoney));
                Console.WriteLine("Patrons who contribute money");
                foreach (var sponsor in sponsors)
                {
                    Console.WriteLine("\t{0}", sponsor.Name);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
