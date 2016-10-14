using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Recipe4
{
    [DataContract(IsReference=true)]
    public class Comment
    {
        [DataMember]
        public virtual int CommentId { get; set; }
        [DataMember]
        public virtual int PostId { get; set; }
        [DataMember]
        public virtual string CommentText { get; set; }
        [DataMember]
        public virtual Post Post { get; set; }
    }
}
