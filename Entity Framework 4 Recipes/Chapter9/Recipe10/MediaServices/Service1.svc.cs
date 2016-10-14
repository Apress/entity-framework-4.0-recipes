using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.Entity;
using MediaEntities;
namespace MediaServices
{
    public class Service1 : IService1
    {
        public void Initialize()
        {
            using (var context = new EFRecipesEntities())
            {
                // clean up
                context.ExecuteStoreCommand("delete from chapter9.mediacategory");
                context.ExecuteStoreCommand("delete from chapter9.category");
                context.ExecuteStoreCommand("delete from chapter9.media");
                context.ExecuteStoreCommand("delete from chapter9.mediatype");

                // insert some test data
                var mediaType = new MediaType { MediaTypeId = 1, Description = "Article" };
                var media1 = new Medium { Title = "How to Design a Brick Fireplace", MediaType = mediaType };
                var media2 = new Medium { Title = "Repairing a Brick Oven", MediaType = mediaType };
                context.Media.AddObject(media1);
                context.Media.AddObject(media2);
                context.SaveChanges();
            }
        }

        public Medium GetMediaByTitle(string title)
        {
            using (var context = new EFRecipesEntities())
            {
                return context.Media.Include("MediaType").First(m => m.Title == title);
            }
        }

        public void SubmitCategory(Category category)
        {
            using (var context = new EFRecipesEntities())
            {
                context.Categories.ApplyChanges(category);
                context.SaveChanges();
            }
        }
    }
}
