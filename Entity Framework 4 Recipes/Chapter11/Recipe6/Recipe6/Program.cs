using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Linq.Expressions;
using System.Reflection;
using System.Data.Objects.DataClasses;
using System.Data.Common;
using System.Data.EntityClient;

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
                context.ExecuteStoreCommand("delete from chapter11.patientvisit");
                context.ExecuteStoreCommand("delete from chapter11.patient");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                string hospital = "Oakland General";
                var p1 = new Patient { Name = "Robin Rosen", Age = 41 };
                var p2 = new Patient { Name = "Alex Jones", Age = 39 };
                var p3 = new Patient { Name = "Susan Kirby", Age = 54 };
                var v1 = new PatientVisit { Cost = 98.38M, Hospital = hospital, Patient = p1 };
                var v2 = new PatientVisit { Cost = 1122.98M, Hospital = hospital, Patient = p1 };
                var v3 = new PatientVisit { Cost = 2292.72M, Hospital = hospital, Patient = p2 };
                var v4 = new PatientVisit { Cost = 1145.73M, Hospital = hospital, Patient = p3 };
                var v5 = new PatientVisit { Cost = 2891.07M, Hospital = hospital, Patient = p3 };
                context.Patients.AddObject(p1);
                context.Patients.AddObject(p2);
                context.Patients.AddObject(p3);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("Query using eSql...");
                var esql = @"Select value ps from EFRecipesEntities.Patients as p join EFRecipesModel.GetVisitSummary() as ps on p.Name = ps.Name where p.Age > 40";
                var patients = context.CreateQuery<VisitSummary>(esql);
                foreach (var patient in patients)
                {
                    Console.WriteLine("{0}, Visits: {1}, Total Bill: {2}",
                        patient.Name, patient.TotalVisits.ToString(), patient.TotalCost.ToString("C"));
                }
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine();
                Console.WriteLine("Query using LINQ...");
                var patients = from p in context.Patients
                               join ps in context.GetVisitSummary() on p.Name equals ps.Name
                               where p.Age >= 40
                               select ps;
                foreach (var patient in patients)
                {
                    Console.WriteLine("{0}, Visits: {1}, Total Bill: {2}",
                        patient.Name, patient.TotalVisits.ToString(), patient.TotalCost.ToString("C"));
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }

    partial class EFRecipesEntities
    {
        [EdmFunction("EFRecipesModel", "GetVisitSummary")]
        public IQueryable<VisitSummary> GetVisitSummary()
        {
            return this.QueryProvider.CreateQuery<VisitSummary>(
                Expression.Call(Expression.Constant(this), 
                  (MethodInfo)MethodInfo.GetCurrentMethod()));
        }
    }
}
