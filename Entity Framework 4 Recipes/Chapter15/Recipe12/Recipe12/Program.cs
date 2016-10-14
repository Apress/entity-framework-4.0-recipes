using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe12
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
                context.ExecuteStoreCommand("delete from chapter15.article");
                context.ExecuteStoreCommand("delete from chapter15.video");
                context.ExecuteStoreCommand("delete from chapter15.media");
            }

        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var blogpost = new BlogPosting { Title = "ASP.NET MVC", Author = "Steven Grace", Post = "What's New", Comments = "50" };
                var story = new Story { Title = "Time in a Bottle", Author = "Emily Jones", Plot = "Murder on the high seas" };
                var ed = new EducationalVideo { Instructor = "Joseph Robins", ResourcePath = "\\videos\asp.wmv", Title = "ASP.NET Examples" };
                var movie = new RecreationalVideo { Title = "Archie's Place", Rating = 1, ResourcePath = "\\videos\archie.wmv" };
                context.Media.AddObject(blogpost);
                context.Media.AddObject(story);
                context.Media.AddObject(ed);
                context.Media.AddObject(movie);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("All of the media...");
                foreach(var m in context.Media)
                {
                    Console.WriteLine();
                    if (m is BlogPosting)
                    {
                        var post = (BlogPosting) m;
                        Console.WriteLine("Blog Posting");
                        Console.WriteLine("Title: {0}, Author: {1}, Post: {2}",post.Title,post.Author,post.Post);
                    }
                    else if (m is Story)
                    {
                        var story = (Story)m;
                        Console.WriteLine("Story");
                        Console.WriteLine("Title: {0}, Author: {1}, Plot: {2}", story.Title, story.Author, story.Plot);
                    }
                    else if (m is EducationalVideo)
                    {
                        var edvideo = (EducationalVideo)m;
                        Console.WriteLine("Educational Video");
                        Console.WriteLine("Title: {0}, Instructor: {1}", edvideo.Title, edvideo.Instructor);
                    }
                    else if (m is RecreationalVideo)
                    {
                        var video = (RecreationalVideo)m;
                        Console.WriteLine("Recreational Video");
                        Console.WriteLine("Title: {0}, Rating: {1}", video.Title, video.Rating.ToString());
                    }
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
