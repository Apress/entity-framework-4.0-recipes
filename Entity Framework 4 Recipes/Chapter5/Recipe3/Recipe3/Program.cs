using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe3
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
                context.ExecuteStoreCommand("delete from chapter5.plumber");
                context.ExecuteStoreCommand("delete from chapter5.foreman");
                context.ExecuteStoreCommand("delete from chapter5.jobsite");
                context.ExecuteStoreCommand("delete from chapter5.location");
                context.ExecuteStoreCommand("delete from chapter5.tradesman");
                context.ExecuteStoreCommand("delete from chapter5.phone");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var foreman1 = new Foreman { Name = "Carl Ramsey" };
                var foreman2 = new Foreman { Name = "Nancy Ortega" };
                var phone = new Phone { Number = "817 867-5309" };
                var jobsite = new JobSite { JobSiteName = "City Arena", Address = "123 Main", City = "Anytown", State = "TX", ZIPCode = "76082", Phone = phone };
                jobsite.Foremen.Add(foreman1);
                jobsite.Foremen.Add(foreman2);
                var plumber = new Plumber { Name = "Jill Nichols", Email = "JNichols@plumbers.com", JobSite = jobsite };
                context.Tradesmen.AddObject(plumber);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                var plumber = context.Tradesmen.OfType<Plumber>().Include("JobSite.Phone").Include("JobSite.Foremen").First();
                Console.WriteLine("Plumber's Name: {0} ({1})", plumber.Name, plumber.Email);
                Console.WriteLine("Job Site: {0}", plumber.JobSite.JobSiteName);
                Console.WriteLine("Job Site Phone: {0}", plumber.JobSite.Phone.Number);
                Console.WriteLine("Job Site Foremen:");
                foreach (var boss in plumber.JobSite.Foremen)
                {
                    Console.WriteLine("\t{0}", boss.Name);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
