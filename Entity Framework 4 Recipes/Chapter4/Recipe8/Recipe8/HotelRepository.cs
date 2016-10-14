using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recipe8
{
    public class HotelRepository
    {
        private EFRecipesEntities context;

        public HotelRepository()
        {
            this.context = new EFRecipesEntities();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }

        public List<Hotel> GetHotels()
        {
            return this.context.Hotels.OrderBy(h => h.Name).ToList();
        }
    }
}