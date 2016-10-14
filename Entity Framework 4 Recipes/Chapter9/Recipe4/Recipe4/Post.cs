using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Recipe4
{
    [DataContract(IsReference = true)]
    public class Post
    {
        [DataMember]
        public virtual int PostId { get; set; }
        [DataMember]
        public virtual string Title { get; set; }
        [DataMember]
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
