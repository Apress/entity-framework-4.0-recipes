using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe4
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
                context.ExecuteStoreCommand("delete from chapter6.person");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var teacher = new Teacher { Name = "Susan Smith", School = "Custer Baker Middle School" };
                var firefighter = new Firefighter { Name = "Joel Clark", FireStation = "Midtown" };
                var retired = new Retired { Name = "Joan Collins", FullTimeHobby = "Scapbooking" };
                context.People.AddObject(teacher);
                context.People.AddObject(firefighter);
                context.People.AddObject(retired);
                context.SaveChanges();
                firefighter.Hero = teacher;
                teacher.Hero = retired;
                retired.Hero = firefighter;
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                context.ContextOptions.LazyLoadingEnabled = true;
                foreach(var person in context.People)
                {
                    if (person.Hero != null)
                        Console.WriteLine("\n{0}, Hero is: {1}", person.Name, person.Hero.Name);
                    else
                        Console.WriteLine("{0}", person.Name);
                    if (person is Firefighter)
                        Console.WriteLine("Firefighter at station {0}", ((Firefighter)person).FireStation);
                    else if (person is Teacher)
                        Console.WriteLine("Teacher at {0}", ((Teacher)person).School);
                    else if (person is Retired)
                        Console.WriteLine("Retired, hobby is {0}", ((Retired)person).FullTimeHobby);
                    Console.WriteLine("Fans:");
                    foreach (var fan in person.Fans)
                    {
                        Console.WriteLine("\t{0}", fan.Name);
                    }
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
