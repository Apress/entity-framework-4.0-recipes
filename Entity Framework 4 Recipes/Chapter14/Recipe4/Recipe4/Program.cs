using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;

namespace Recipe4
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
                context.ExecuteStoreCommand("delete from chapter14.forumpost");
            }
        }

        static void RunExample()
        {
            int postId = 0;
            using (var context = new EFRecipesEntities())
            {
                // post is created
                var post = new ForumPost { ForumUser = "FastEddie27", IsActive = false, Post = "The moderator is a great guy." };
                context.ForumPosts.AddObject(post);
                context.SaveChanges();
                postId = post.PostingId;
            }

            using (var context = new EFRecipesEntities())
            {
                // moderator gets post to review
                var post = context.ForumPosts.First(p => p.PostingId  == postId);
                Console.WriteLine("Post by {0}: {1}", post.ForumUser, post.Post);

                // poster changes post out-of-band
                context.ExecuteStoreCommand(@"update chapter14.forumpost 
                         set post='The moderator''s mom dresses him funny.' 
                         where postingId = @p0", new object[] { postId.ToString() });
                Console.WriteLine("Fast Eddie changes the post");

                // moderator doesn't trust Fast Eddie
                if (string.Compare(post.ForumUser, "FastEddie27") == 0)
                    post.IsActive = false;
                else
                    post.IsActive = true;

                try
                {
                    // refresh any changes to the TimeStamp
                    context.ForumPosts.MergeOption = MergeOption.PreserveChanges;
                    post = context.ForumPosts.First(p => p.PostingId == postId);
                    context.SaveChanges();
                    Console.WriteLine("No concurrency exception.");
                }
                catch (OptimisticConcurrencyException)
                {
                    try
                    {
                        context.Refresh(RefreshMode.ClientWins, post);
                        context.SaveChanges();
                    }
                    catch (OptimisticConcurrencyException)
                    {
                        // we tried twice...do something else
                    }
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
