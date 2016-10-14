using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Recipe4
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var context = new EFRecipesEntities())
            {
                context.ExecuteStoreCommand("delete from chapter4.[order]");
                context.ExecuteStoreCommand("delete from chapter4.webcustomer");

                var cust1 = new WebCustomer { Name = "Joan Steward" };
                var cust2 = new WebCustomer { Name = "Allen Colbert" };
                var cust3 = new WebCustomer { Name = "Phil Marlowe" };
                var order1 = new Order { Amount = 29.95M, OrderDate = DateTime.Parse("3/18/2010") };
                var order2 = new Order { Amount = 84.99M, OrderDate = DateTime.Parse("3/20/2010") };
                var order3 = new Order { Amount = 99.95M, OrderDate = DateTime.Parse("4/10/2010") };
                order1.WebCustomer = cust1;
                order2.WebCustomer = cust2;
                order3.WebCustomer = cust3;
                context.Orders.AddObject(order1);
                context.Orders.AddObject(order2);
                context.Orders.AddObject(order3);
                context.SaveChanges();
            }
        }
    }
}