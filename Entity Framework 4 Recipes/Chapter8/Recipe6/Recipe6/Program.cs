using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data.Metadata.Edm;

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
                context.ExecuteStoreCommand("delete from chapter8.item");
            }
        }

        static void RunExample()
        {
            int itemId = 0;
            using (var context = new EFRecipesEntities())
            {
                var item = new Item { Name = "Xcel Camping Tent", UnitPrice = 99.95M };
                context.Items.AddObject(item);
                context.SaveChanges();

                // keep the item id for the next step
                itemId = item.ItemId;
                Console.WriteLine("Item: {0}, UnitPrice: {1}", item.Name, item.UnitPrice.ToString("C"));
            }

            using (var context = new EFRecipesEntities())
            {
                // pretend this is the updated 
                // item we received with the new price
                var item = new Item { ItemId = itemId, Name = "Xcel Camping Tent", UnitPrice = 129.95M };

                // use our method to get the entity set name
                var itemES = context.GetEntitySet(item);

                // create the entity key
                var key = context.CreateEntityKey(itemES.Name, item);

                // retrieve and update the item
                context.GetObjectByKey(key);
                context.ApplyCurrentValues(itemES.Name, item);
                context.SaveChanges();
            }
            using (var context = new EFRecipesEntities())
            {
                var item = context.Items.Single();
                Console.WriteLine("Item: {0}, UnitPrice: {1}", item.Name, item.UnitPrice.ToString("C"));
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }

    public class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class EFRecipesEntities : ObjectContext
    {
        public EFRecipesEntities()
            : base("name=EFRecipesEntities", "EFRecipesEntities")
        {
        }

        private ObjectSet<Item> items;
        public ObjectSet<Item> Items
        {
            get { return items ?? (items = CreateObjectSet<Item>()); }
        }

        // gets the entity set
        public EntitySetBase GetEntitySet(Object entityType)
        {
            var container = this.MetadataWorkspace.GetEntityContainer(this.DefaultContainerName,DataSpace.CSpace);
            var entitySet = container.BaseEntitySets.Single(es => es.ElementType.Name == entityType.GetType().Name);
            return entitySet;
        }
    }
}
