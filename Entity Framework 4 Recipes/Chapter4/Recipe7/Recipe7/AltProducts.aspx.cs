using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Recipe7
{
    public partial class AltProducts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var context = new EFRecipesEntities())
            {
                // delete any previous test data
                context.ExecuteStoreCommand("delete from chapter4.item");
                context.ExecuteStoreCommand("delete from chapter4.itemcategory");

                // populate with some test data
                var cat1 = new ItemCategory { Name = "Tents" };
                var cat2 = new ItemCategory { Name = "Cooking Equipment" };
                context.Items.AddObject(new Item { Name = "Backpacking Tent", ItemCategory = cat1 });
                context.Items.AddObject(new Item { Name = "Camp Stove", ItemCategory = cat2 });
                context.Items.AddObject(new Item { Name = "Dutch Oven", ItemCategory = cat2 });
                context.Items.AddObject(new Item { Name = "Alpine Tent", ItemCategory = cat1 });
                context.Items.AddObject(new Item { Name = "Fire Starter", ItemCategory = cat2 });
                context.SaveChanges();
            }
        }
        protected void ProdFilter(object sender, QueryCreatedEventArgs e)
        {
            var catvalue = (string)Page.RouteData.Values["category"];
            e.Query = from p in e.Query.Cast<Item>()
                      where p.ItemCategory.Name == catvalue
                      select p;
        }
    }
}