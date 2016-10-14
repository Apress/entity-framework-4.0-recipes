using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects;

namespace Recipe8
{
    public class EFRecipesEntities : ObjectContext
    {
        private ObjectSet<Client> clients;
        public EFRecipesEntities()
            : base("name=EFRecipesEntities", "EFRecipesEntities")
        {
        }

        public ObjectSet<Client> Clients
        {
            get { return clients ?? (clients = CreateObjectSet<Client>()); }
        }
    }
}