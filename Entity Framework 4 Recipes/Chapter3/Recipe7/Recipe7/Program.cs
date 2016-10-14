using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Objects;

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
                context.ExecuteStoreCommand("delete from chapter3.bid");
                context.ExecuteStoreCommand("delete from chapter3.job");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var job1 = new Job { JobDetails = "Re-surface Parking Log" };
                var job2 = new Job { JobDetails = "Build Driveway" };
                job1.Bids.Add(new Bid { Amount = 948M, Bidder = "ABC Paving" });
                job1.Bids.Add(new Bid { Amount = 1028M, Bidder = "TopCoat Paving" });
                job2.Bids.Add(new Bid { Amount = 502M, Bidder = "Ace Concrete" });
                context.Jobs.AddObject(job1);
                context.Jobs.AddObject(job2);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                var cs = @"Data Source=.;Initial Catalog=EFRecipes;Integrated Security=True";
                var conn = new SqlConnection(cs);
                var cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Chapter3.GetBidDetails";
                conn.Open();
                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                var jobs = context.Translate<Job>(reader, "Jobs", MergeOption.AppendOnly).ToList();
                reader.NextResult();
                context.Translate<Bid>(reader, "Bids", MergeOption.AppendOnly).ToList();
                foreach (var job in jobs)
                {
                    Console.WriteLine("\nJob: {0}", job.JobDetails);
                    foreach (var bid in job.Bids)
                    {
                        Console.WriteLine("\tBid: {0} from {1}", bid.Amount.ToString("C"), bid.Bidder);
                    }
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
