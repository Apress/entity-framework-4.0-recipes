using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

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
                context.ExecuteStoreCommand("delete from chapter8.orderdetail");
                context.ExecuteStoreCommand("delete from chapter8.[order]");
                context.ExecuteStoreCommand("delete from chapter8.product");
                context.ExecuteStoreCommand("delete from chapter8.customer");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var tea = new Product { ProductName = "Green Tea", UnitPrice = 1.09M };
                var coffee = new Product {ProductName = "Colombian Coffee", UnitPrice = 2.15M};
                var customer = new Customer { ContactName = "Karen Marlowe" };
                var order1 = new Order { OrderDate = DateTime.Parse("4/19/10") };
                order1.OrderDetails.Add(new OrderDetail { Product = tea, Quantity = 4, UnitPrice = 1.00M });
                order1.OrderDetails.Add(new OrderDetail { Product = coffee, Quantity = 3, UnitPrice = 2.15M });
                customer.Orders.Add(order1);
                context.Customers.AddObject(customer);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                var query = context.Customers.Include("Orders.OrderDetails.Product");
                foreach (var customer in query)
                {
                    Console.WriteLine("Orders for {0}", customer.ContactName);
                    foreach (var order in customer.Orders)
                    {
                        Console.WriteLine("--Order Date: {0}--", order.OrderDate.ToShortDateString());
                        foreach (var detail in order.OrderDetails)
                        {
                            Console.WriteLine("\t{0}, {1} units at {2} each, unit discount: {3}", 
                                detail.Product.ProductName, 
                                detail.Quantity.ToString(), 
                                detail.UnitPrice.ToString("C"), 
                                (detail.Product.UnitPrice - detail.UnitPrice).ToString("C"));
                        }
                    }
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }

    public class Customer
    {
        public int CustomerId { get; set; }
        public string ContactName { get; set; }
        public ISet<Order> Orders {get; set;}
        public Customer()
        {
            this.Orders = new HashSet<Order>();
        }
    }

    public class Order
    {
        public int OrderId {get; set;}
        public int CustomerId {get; set;}
        public DateTime OrderDate {get; set;}
        public Customer Customer {get; set;}
        public ISet<OrderDetail> OrderDetails {get; set;}
        public Order()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }
    }
    
    public class OrderDetail
    {
        public int OrderId {get; set;}
        public int ProductId {get; set;}
        public decimal UnitPrice {get; set;}
        public int Quantity {get; set;}
        public Order Order {get; set;}
        public Product Product {get; set;}
    }

    public class Product
    {
        public int ProductId {get; set;}
        public string ProductName {get; set;}
        public decimal UnitPrice {get; set;}
    }

    public class EFRecipesEntities : ObjectContext
    {
        private ObjectSet<Customer> _customers;
        private ObjectSet<Order> _orders;
        private ObjectSet<OrderDetail> _orderdetails;
        private ObjectSet<Product> _products;

        public EFRecipesEntities() : base("name=EFRecipesEntities","EFRecipesEntities")
        {
            _orders = CreateObjectSet<Order>();
            _orderdetails = CreateObjectSet<OrderDetail>();
            _products = CreateObjectSet<Product>();
        }

        public ObjectSet<Customer> Customers
        {
            get { return _customers ?? (_customers = CreateObjectSet<Customer>()); }
        }

        public ObjectSet<Order> Orders
        {
            get { return _orders; }
        }

        public ObjectSet<OrderDetail> OrderDetails
        {
            get { return _orderdetails; }
        }

        public ObjectSet<Product> Products
        {
            get { return _products; }
        }
    }
}
