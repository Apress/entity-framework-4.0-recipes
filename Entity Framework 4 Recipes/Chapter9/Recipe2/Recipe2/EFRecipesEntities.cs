using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Recipe2
{
    public class EFRecipesEntities : ObjectContext
    {
        public EFRecipesEntities()
            : base("name=EFRecipesEntities", "EFRecipesEntities")
        {
        }

        private IObjectSet<Order> orders;

        public IObjectSet<Order> Orders
        {
            get { return orders ?? (orders = CreateObjectSet<Order>()); }
        }
    }
}
