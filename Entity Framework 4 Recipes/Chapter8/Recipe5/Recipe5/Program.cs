using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Recipe5
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
                context.ExecuteStoreCommand("delete from chapter8.donation");
                context.ExecuteStoreCommand("delete from chapter8.donor");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var donation = context.CreateObject<Donation>();
                donation.Amount = 5000M;

                var donor1 = context.CreateObject<Donor>();
                donor1.Name = "Jill Rosenberg";
                var donor2 = context.CreateObject<Donor>();
                donor2.Name = "Robert Hewitt";
                
                // give Jill the credit for the donation and save
                donor1.Donations.Add(donation);
                context.Donors.AddObject(donor1);
                context.Donors.AddObject(donor2);
                context.SaveChanges();

                // now give Robert the credit
                donation.Donor = donor2;

                // report
                foreach (var donor in context.Donors)
                {
                    Console.WriteLine("{0} has given {1} donation(s)", donor.Name, donor.Donations.Count().ToString());
                }
                var entry = context.ObjectStateManager.GetObjectStateEntry(donation);
                Console.WriteLine("Original Donor Id: {0}", entry.OriginalValues["DonorId"]);
                Console.WriteLine("Current Donor Id: {0}", entry.CurrentValues["DonorId"]);
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }

    public class Donor
    {
        public virtual int DonorId { get; set; }
        public virtual string Name { get; set; }
        public virtual ICollection<Donation> Donations { get; set; }
    }

    public class Donation
    {
        public virtual int DonationId { get; set; }
        public virtual int? DonorId { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual Donor Donor { get; set; }
    }

    public class EFRecipesEntities : ObjectContext
    {
        public EFRecipesEntities()
            : base("name=EFRecipesEntities", "EFRecipesEntities")
        {
        }

        private ObjectSet<Donor> donors;
        private ObjectSet<Donation> donations;

        public ObjectSet<Donor> Donors
        {
            get { return donors ?? (donors = CreateObjectSet<Donor>()); }
        }

        public ObjectSet<Donation> Donations
        {
            get { return donations ?? (donations = CreateObjectSet<Donation>()); }
        }
    }
}
