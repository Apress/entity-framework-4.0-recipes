using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Metadata.Edm;
using System.Data.SqlClient;
using System.Data.EntityClient;
using System.Xml;
using System.Data.Mapping;
using System.Data.Objects;

namespace Recipe2
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
            using (var context = ContextFactory.CreateContext())
            {
                context.ExecuteStoreCommand("delete from chapter7.Customer");
            }
        }

        static void RunExample()
        {
            using (var context = ContextFactory.CreateContext())
            {
                context.Customers.AddObject(new Customer { Name = "Jill Nickels" });
                context.Customers.AddObject(new Customer { Name = "Robert Cole" });
                context.SaveChanges();
            }

            using (var context = ContextFactory.CreateContext())
            {
                Console.WriteLine("Customers");
                Console.WriteLine("---------");
                foreach (var customer in context.Customers)
                {
                    Console.WriteLine("{0}", customer.Name);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }

    public class Customer
    {
        public virtual int CustomerId { get; set; }
        public virtual string Name { get; set; }
    }

    public class EFRecipesEntities : ObjectContext
    {
        private ObjectSet<Customer> customers;
        public EFRecipesEntities(EntityConnection cn)
            : base(cn)
        {
        }

        public ObjectSet<Customer> Customers
        {
            get
            {
                return customers ?? (customers = CreateObjectSet<Customer>());
            }
        }
    }
    
    public static class ContextFactory
    {
        static string connString = @"Data Source=localhost;Initial Catalog=EFRecipes;Integrated Security=True;";
        private static MetadataWorkspace workspace = CreateWorkSpace();

        public static EFRecipesEntities CreateContext()
        {
            var conn = new EntityConnection(workspace, new SqlConnection(connString));
            return new EFRecipesEntities(conn);
        }

        private static MetadataWorkspace CreateWorkSpace()
        {
            string sql = @"select csdl,msl,ssdl from Chapter7.Definitions";
            XmlReader csdlReader = null;
            XmlReader mslReader = null;
            XmlReader ssdlReader = null;

            using (var cn = new SqlConnection(connString))
            {
                using (var cmd = new SqlCommand(sql, cn))
                {
                    cn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        csdlReader = reader.GetSqlXml(0).CreateReader();
                        mslReader = reader.GetSqlXml(1).CreateReader();
                        ssdlReader = reader.GetSqlXml(2).CreateReader();
                    }
                }
            }

            var workspace = new MetadataWorkspace();
            var edmCollection = new EdmItemCollection(new XmlReader[] { csdlReader });
            var ssdlCollection = new StoreItemCollection(new XmlReader[] { ssdlReader });
            var mappingCollection = new StorageMappingItemCollection(edmCollection, ssdlCollection, new XmlReader[] { mslReader });

            workspace.RegisterItemCollection(edmCollection);
            workspace.RegisterItemCollection(ssdlCollection);
            workspace.RegisterItemCollection(mappingCollection);
            return workspace;
        }
    }
}
