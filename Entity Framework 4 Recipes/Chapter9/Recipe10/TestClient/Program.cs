using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestClient.ServiceReference1;
using MediaEntities;
namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new Service1Client();
            service.Initialize();
            var media1 = service.GetMediaByTitle("How to Design a Brick Fireplace");
            var media2 = service.GetMediaByTitle("Repairing a Brick Oven");
            var category = new Category { Name = "Brick Construction" };
            category.Media.Add(media1);
            category.Media.Add(media2);

            // at this point, media1 and media2 both
            // reference the "Article" MediaType, but there
            // are two instances of this object each with 
            // the same key, we need to fix this
            if (media1.MediaType.MediaTypeId == media2.MediaType.MediaTypeId)
            {
                // apply fix
                media2.StopTracking();
                media2.MediaType.StopTracking();
                media1.MediaType.StopTracking();
                media2.MediaType = media1.MediaType;
                media2.StartTracking();
                media2.MediaType.StartTracking();
            }
            service.SubmitCategory(category);
        }
    }
}
