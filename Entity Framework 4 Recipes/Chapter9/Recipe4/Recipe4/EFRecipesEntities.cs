using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Recipe4
{
    public class EFRecipesEntities : ObjectContext
    {
        public EFRecipesEntities()
            : base("name=EFRecipesEntities", "EFRecipesEntities")
        {
        }

        private ObjectSet<Post> posts;
        private ObjectSet<Comment> comments;

        public ObjectSet<Post> Posts
        {
            get { return posts ?? (posts = CreateObjectSet<Post>()); }
        }

        public ObjectSet<Comment> Comments
        {
            get { return comments ?? (comments = CreateObjectSet<Comment>()); }
        }
    }
}
