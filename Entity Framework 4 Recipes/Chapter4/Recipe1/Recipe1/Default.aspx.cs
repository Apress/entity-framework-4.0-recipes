using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Recipe1
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var context = new EFRecipesEntities())
            {
                // delete any previous data we might have
                context.ExecuteStoreCommand("delete from chapter4.customer");

                // insert some data
                context.Customers.AddObject(new Customer { Name = "Robin Rosen", City = "Olathe", State = "KS" });
                context.Customers.AddObject(new Customer { Name = "John Wise", City = "Springtown", State = "TX" });
                context.Customers.AddObject(new Customer { Name = "Karen Carter", City = "Raytown", State = "MO" });
                context.SaveChanges();
            }
        }
    }
}