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
                context.ExecuteStoreCommand("delete from chapter6.drug");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var exDrug1 = new Experimental { Name = "Nanoxol", PrincipalResearcher = "Dr. Susan James" };
                var exDrug2 = new Experimental { Name = "Percosol", PrincipalResearcher = "Dr. Bill Minor" };
                context.Drugs.AddObject(exDrug1);
                context.Drugs.AddObject(exDrug2);
                context.SaveChanges();

                // Nanoxol just got approved!
                exDrug1.PromoteToMedicine(DateTime.Now, 19.99M, "Treatall");
                context.Detach(exDrug1); // better not use this instance any longer
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("Experimental Drugs");
                foreach (var d in context.Drugs.OfType<Experimental>())
                {
                    Console.WriteLine("\t{0} ({1})", d.Name, d.PrincipalResearcher);
                }

                Console.WriteLine("Medicines");
                foreach (var d in context.Drugs.OfType<Medicine>())
                {
                    Console.WriteLine("\t{0} Retails for {1}", d.Name, d.TargetPrice.Value.ToString("C"));
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }

    public partial class Experimental
    {
        public void PromoteToMedicine(DateTime acceptedDate, decimal targetPrice, string marketingName)
        {
            var drug = new Medicine { DrugId = this.DrugId };
            using (var context = new EFRecipesEntities())
            {
                context.AttachTo("Drugs", drug);
                drug.AcceptedDate = acceptedDate;
                drug.TargetPrice = targetPrice;
                drug.Name = marketingName;
                context.SaveChanges();
            }
        }
    }
}
