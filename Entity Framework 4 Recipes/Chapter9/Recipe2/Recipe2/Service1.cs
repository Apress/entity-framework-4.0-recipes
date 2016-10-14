using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Collections;

namespace Recipe2
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        public Order InsertOrder()
        {
            using (var context = new EFRecipesEntities())
            {
                // remove previous test data
                context.ExecuteStoreCommand("delete from chapter9.[order]");

                var order = new Order { Product = "Camping Tent", Quantity = 3, Status = "Received" };
                context.Orders.AddObject(order);
                context.SaveChanges();
                return order;
            }
        }

        public void UpdateOrderWithoutRetrieving(Order order)
        {
            using (var context = new EFRecipesEntities())
            {
                context.Orders.Attach(order);
                if (order.Status == "Received")
                {
                    var entry = context.ObjectStateManager.GetObjectStateEntry(order);
                    entry.SetModifiedProperty("Quantity");
                    context.SaveChanges();
                }
            }
        }

        public void UpdateOrderByRetrieving(Order order)
        {
            using (var context = new EFRecipesEntities())
            {
                var dbOrder = context.Orders.Single(o => o.OrderId == order.OrderId);
                if (dbOrder != null && 
                    StructuralComparisons.StructuralEqualityComparer.Equals(order.TimeStamp, dbOrder.TimeStamp))
                {
                    dbOrder.Quantity = order.Quantity;
                    context.SaveChanges();
                }
            }
        }
    }
}
