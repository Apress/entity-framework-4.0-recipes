using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe2
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
                context.ExecuteStoreCommand("delete from chapter13.painting");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                context.Paintings.AddObject(new Painting { AccessionNumber = "PN001", Name = "Sunflowers", Artist = "Rosemary Golden", LastSalePrice = 1250M });
                context.Paintings.AddObject(new Painting { AccessionNumber = "PN002", Name = "Red River", Artist = "Alex Jones", LastSalePrice = 1800M });
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                // let's assume we already know the key for the painting
                var p = context.GetObjectByKey(new System.Data.EntityKey("EFRecipesEntities.Paintings", "AccessionNumber", "PN001"));
                Painting painting = (Painting) p;
                Console.WriteLine("The painting with accession number {0}", painting.AccessionNumber);
                Console.WriteLine("\tName: {0}", painting.Name);
                Console.WriteLine("\tArtist: {0}", painting.Artist);
                Console.WriteLine("\tSale Price: {0}", painting.LastSalePrice.ToString("C"));
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
