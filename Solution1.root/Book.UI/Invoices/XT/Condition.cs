using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.UI.Invoices.XT
{
    public class Condition
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CustomerId { get; set; }
        public string InvoiceCusId { get; set; }
    }
}
