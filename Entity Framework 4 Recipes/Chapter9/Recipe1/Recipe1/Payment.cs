using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe1
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public byte[] TimeStamp { get; set; }
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
    }
}
