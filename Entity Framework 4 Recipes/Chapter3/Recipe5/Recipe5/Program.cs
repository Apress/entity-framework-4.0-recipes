using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe5
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
                context.ExecuteStoreCommand("delete from chapter3.comment");
                context.ExecuteStoreCommand("delete from chapter3.blogpost");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var post1 = new BlogPost { Title = "The Joy of LINQ", Description = "101 things you always wanted to know about LINQ" };
                var post2 = new BlogPost { Title = "LINQ as Dinner Conversation", Description = "What wine goes with a Lambda expression?" };
                var post3 = new BlogPost {Title = "LINQ and our Children", Description = "Why we need to teach LINQ in High School"};
                var comment1 = new Comment { Comments = "Great post, I wish more people would talk about LINQ" };
                var comment2 = new Comment { Comments = "You're right, we should teach LINQ in high school!" };
                post1.Comments.Add(comment1);
                post3.Comments.Add(comment2);
                context.BlogPosts.AddObject(post1);
                context.BlogPosts.AddObject(post2);
                context.BlogPosts.AddObject(post3);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("Blog Posts with comments...(LINQ)");
                var posts = from post in context.BlogPosts
                            where post.Comments.Any()
                            select post;
                foreach (var post in posts)
                {
                    Console.WriteLine("Blog Post: {0}", post.Title);
                    foreach (var comment in post.Comments)
                    {
                        Console.WriteLine("\t{0}", comment.Comments);
                    }
                }                
            }
            Console.WriteLine();

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("Blog Posts with comments...(ESQL)");
                var esql = "select value p from BlogPosts as p where exists(p.Comments)";
                var posts = context.CreateQuery<BlogPost>(esql);
                foreach (var post in posts)
                {
                    Console.WriteLine("Blog Post: {0}", post.Title);
                    foreach (var comment in post.Comments)
                    {
                        Console.WriteLine("\t{0}", comment.Comments);
                    }
                }

            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
