using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe2
{
    public class Order
    {
        public int OrderId { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public byte[] TimeStamp { get; set; }
    }
}
