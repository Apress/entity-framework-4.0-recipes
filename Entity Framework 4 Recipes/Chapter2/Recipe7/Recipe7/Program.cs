using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe7
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
                context.ExecuteStoreCommand("delete from chapter2.photograph");
            }
        }

        static void RunExample()
        {
            byte[] thumbBits = new byte[100];
            byte[] fullBits = new byte[2000];
            using (var context = new EFRecipesEntities())
            {
                var photo = new Photograph { PhotoId = 1, Title = "My Dog", ThumbnailBits = thumbBits };
                var fullImage = new PhotographFullImage { PhotoId = 1, HighResolutionBits = fullBits };
                photo.PhotographFullImage = fullImage;
                context.Photographs.AddObject(photo);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                foreach (var photo in context.Photographs)
                {
                    Console.WriteLine("Photo: {0}, ThumbnailSize {1} bytes", photo.Title, photo.ThumbnailBits.Length.ToString());

                    // explicitly load the "expensive" entity, PhotographFullImage
                    photo.PhotographFullImageReference.Load();
                    Console.WriteLine("Full Image Size: {0} bytes", photo.PhotographFullImage.HighResolutionBits.Length.ToString());
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
