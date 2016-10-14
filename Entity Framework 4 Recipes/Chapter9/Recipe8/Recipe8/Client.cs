using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recipe8
{
    public class Client
    {
        public virtual int ClientId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
    }
}