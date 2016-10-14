using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Recipe1
{
    public class EFRecipesEntities : ObjectContext
    {
        public EFRecipesEntities()
            : base("name=EFRecipesEntities", "EFRecipesEntities")
        {
        }

        private ObjectSet<Payment> payments;
        private ObjectSet<Invoice> invoices;

        public ObjectSet<Payment> Payments
        {
            get { return payments ?? (payments = CreateObjectSet<Payment>()); }
        }

        public ObjectSet<Invoice> Invoices
        {
            get { return invoices ?? (invoices = CreateObjectSet<Invoice>()); }
        }
    }
}
