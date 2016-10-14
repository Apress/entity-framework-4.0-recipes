using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Recipe4;
using System.Data.Objects;
using System.Data;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class Service1 : IService1
    {
        public void Cleanup()
        {
            using (var context = new EFRecipesEntities())
            {
                context.ExecuteStoreCommand("delete from chapter9.comment");
                context.ExecuteStoreCommand("delete from chapter9.post");
            }
        }

        public Post GetPostByTitle(string title)
        {
            using (var context = new EFRecipesEntities())
            {
                context.ContextOptions.ProxyCreationEnabled = false;
                var post = context.Posts.Include("Comments").Single(p => p.Title == title);
                return post;
            }
        }

        public Post SubmitPost(Post post)
        {
            using (var context = new EFRecipesEntities())
            {
                context.Posts.Attach(post);
                if (post.PostId == 0)
                {
                    // this must be an insert
                    context.ObjectStateManager.ChangeObjectState(post, EntityState.Added);
                }
                else
                {
                    context.ObjectStateManager.ChangeObjectState(post, EntityState.Modified);
                }
                context.SaveChanges();
                return post;
            }
        }

        public Comment SubmitComment(Comment comment)
        {
            using (var context = new EFRecipesEntities())
            {
                context.Comments.Attach(comment);
                if (comment.CommentId == 0)
                {
                    // this is an insert
                    context.ObjectStateManager.ChangeObjectState(comment, EntityState.Added);
                }
                else
                {
                    var entry = context.ObjectStateManager.GetObjectStateEntry(comment);
                    entry.SetModifiedProperty("CommentText");
                }
                context.SaveChanges();
                return comment;
            }
        }

        public void DeleteComment(Comment comment)
        {
            using (var context = new EFRecipesEntities())
            {
                context.Comments.Attach(comment);
                context.Comments.DeleteObject(comment);
                context.SaveChanges();
            }
        }
    }
}
