using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Net;
using System.Data.EntityClient;
namespace Recipe1
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
                context.ExecuteStoreCommand("delete from chapter7.product");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var product1 = new Product { SKU = "CAMP-136", ShortDesription = "High country camping tent", Description = "Use this tent on your next high country adventure.", UnitPrice = 199.95M };
                context.Products.AddObject(product1);
                context.SaveChanges();
                Console.WriteLine("Inserted Product {0}: {1}", product1.SKU, product1.ShortDesription);
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }

    public static class ConnectionStringManager
    {
        public static string EFConnection = GetConnection();

        private static string GetConnection()
        {
            var sqlBuilder = new SqlConnectionStringBuilder();

            // figure out the environment
            // strings here should come from a config file
            string myHost = Dns.GetHostName();
            if (myHost.ToLower().Contains("test"))
                sqlBuilder.DataSource = @"TestSql01";
            else if (myHost.ToLower().Contains("staging"))
                sqlBuilder.DataSource = @"StagingSql01";
            else if (myHost.ToLower().Contains("prod"))
                sqlBuilder.DataSource = @"ProdSql01";
            else
                sqlBuilder.DataSource = @"localhost";

            // fill in the rest
            sqlBuilder.InitialCatalog = "EFRecipes";
            sqlBuilder.IntegratedSecurity = true;
            sqlBuilder.MultipleActiveResultSets = true;

            var eBuilder = new EntityConnectionStringBuilder();
            eBuilder.Provider = "System.Data.SqlClient";
            eBuilder.Metadata = "res://*/Recipe1.csdl|res://*/Recipe1.ssdl|res://*/Recipe1.msl";
            eBuilder.ProviderConnectionString = sqlBuilder.ToString();
            return eBuilder.ToString();
        }
    }

    public partial class EFRecipesEntities
    {
        partial void OnContextCreated()
        {
            this.Connection.ConnectionString = ConnectionStringManager.EFConnection;
        }
    }
}
