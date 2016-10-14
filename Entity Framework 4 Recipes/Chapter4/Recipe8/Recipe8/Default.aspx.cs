using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Recipe8
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                using (var context = new EFRecipesEntities())
                {
                    // delete all test data
                    context.ExecuteStoreCommand("delete from chapter4.reservation");
                    context.ExecuteStoreCommand("delete from chapter4.hotel");

                    // insert new test data
                    var h1 = new Hotel { Name = "Riverside Inn" };
                    var h2 = new Hotel { Name = "Greenville Inn" };
                    context.Reservations.AddObject(new Reservation { Name = "Robin Rosen", ReservationDate = DateTime.Parse("4/20/2010"), Rate = 99.95M, Hotel = h1 });
                    context.Reservations.AddObject(new Reservation { Name = "James Marlowe", ReservationDate = DateTime.Parse("5/18/2010"), Rate = 105.00M, Hotel = h2 });
                    context.SaveChanges();
                }
            }
        }
    }
}