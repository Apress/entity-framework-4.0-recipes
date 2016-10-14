using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;

namespace Recipe12
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
                context.ExecuteStoreCommand("delete from chapter12.salesorder");
                context.ExecuteStoreCommand("delete from chapter12.customer");
            }
        }

        static void RunExample()
        {
            // bad order date
            using (var context = new EFRecipesEntities())
            {
                var customer = new Customer { Name = "Phil Marlowe" };
                var order = new SalesOrder { OrderDate = DateTime.Parse("3/12/18"), Amount = 19.95M, Status = "Approved", ShippingCharge = 3.95M, Customer = customer };
                context.Customers.AddObject(customer);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            // order shipped before it was ordered
            using (var context = new EFRecipesEntities())
            {
                var customer = new Customer { Name = "Phil Marlowe" };
                var order = new SalesOrder { OrderDate = DateTime.Parse("3/12/10"), Amount = 19.95M, Status = "Approved", ShippingCharge = 3.95M, Customer = customer };
                context.Customers.AddObject(customer);
                context.SaveChanges();
                try
                {
                    order.Shipped = true;
                    order.ShippedDate = DateTime.Parse("3/10/10");
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            // order shipped, but not approved
            using (var context = new EFRecipesEntities())
            {
                var customer = new Customer { Name = "Phil Marlowe" };
                var order = new SalesOrder { OrderDate = DateTime.Parse("3/12/10"), Amount = 19.95M, Status = "Pending", ShippingCharge = 3.95M, Customer = customer };
                context.Customers.AddObject(customer);
                context.SaveChanges();
                try
                {
                    order.Shipped = true;
                    order.ShippedDate = DateTime.Parse("3/13/10");
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            // order over $5,000 and shipping not free
            using (var context = new EFRecipesEntities())
            {
                var customer = new Customer { Name = "Phil Marlowe" };
                var order = new SalesOrder { OrderDate = DateTime.Parse("3/12/10"), Amount = 6200M, Status = "Approved", ShippingCharge = 59.95M, Customer = customer };
                context.Customers.AddObject(customer);
                context.SaveChanges();
                try
                {
                    order.Shipped = true;
                    order.ShippedDate = DateTime.Parse("3/13/10");
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            // order deleted after it was shipped
            using (var context = new EFRecipesEntities())
            {
                var customer = new Customer { Name = "Phil Marlowe" };
                var order = new SalesOrder { OrderDate = DateTime.Parse("3/12/10"), Amount = 19.95M, Status = "Approved", ShippingCharge = 3.95M, Customer = customer };
                context.Customers.AddObject(customer);
                context.SaveChanges();
                order.Shipped = true;
                order.ShippedDate = DateTime.Parse("3/13/10");
                context.SaveChanges();
                try
                {
                    context.DeleteObject(order);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }

    public partial class EFRecipesEntities
    {
        partial void OnContextCreated()
        {
            this.SavingChanges +=new EventHandler(EFRecipesEntities_SavingChanges);
        }

        private void EFRecipesEntities_SavingChanges(object sender, EventArgs e)
        {
            var entries = this.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified | EntityState.Deleted).Where(entry => entry.Entity is IValidator).Select(entry => entry).ToList();
            foreach (var entry in entries)
            {
                var entity = entry.Entity as IValidator;
                entity.Validate(entry);
            }
        }
    }

    public interface IValidator
    {
        void Validate(ObjectStateEntry entry);
    }

    public partial class SalesOrder : IValidator
    {
        public void Validate(ObjectStateEntry entry)
        {
            if (entry.State == EntityState.Added)
            {
                if (this.OrderDate > DateTime.Now)
                    throw new ApplicationException("OrderDate cannot be after the current date");
            }
            else if (entry.State == EntityState.Modified)
            {
                if (this.ShippedDate < this.OrderDate)
                {
                    throw new ApplicationException("ShippedDate cannot be before OrderDate");
                }
                if (this.Shipped.Value && this.Status != "Approved")
                {
                    throw new ApplicationException("Order cannot be shipped unless it is Approved");
                }
                if (this.Amount > 5000M && this.ShippingCharge != 0)
                {
                    throw new ApplicationException("Orders over $5000 ship for free");
                }
            }
            else if (entry.State == EntityState.Deleted)
            {
                if (this.Shipped.Value)
                    throw new ApplicationException("Shipped orders cannot be deleted");
            }
        }
    }
}
