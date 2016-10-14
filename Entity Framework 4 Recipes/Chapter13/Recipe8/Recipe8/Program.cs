using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

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
                context.ExecuteStoreCommand("delete from chapter13.resume");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var r1 = new Resume { Title = "C# Developer", Name = "Sally Jones", Body = "...very long resume goes here..." };
                context.Resumes.AddObject(r1);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                // using eSQL
                var query = "select value Recipe8.Resume(r.ResumeId, r.Title, r.Name,'') from Resumes as r";
                var result1 = context.CreateQuery<Resume>(query).Single();
                Console.WriteLine("Resume body: {0}", result1.Body);

                context.Resumes.MergeOption = MergeOption.OverwriteChanges;
                var result2 = context.Resumes.Single();
                Console.WriteLine("Resume body: {0}", result2.Body);
            }

            using (var context = new EFRecipesEntities())
            {
                // using ExecuteStoreQuery()
                var result1 = context.ExecuteStoreQuery<Resume>("select ResumeId, Title, Name,'' Body from chapter13.Resume", "Resumes", MergeOption.AppendOnly).Single();
                Console.WriteLine("Resume body: {0}", result1.Body);

                var result2 = context.ExecuteStoreQuery<Resume>("select * from chapter13.Resume", "Resumes", MergeOption.OverwriteChanges).Single();
                Console.WriteLine("Resume body: {0}", result2.Body);
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
