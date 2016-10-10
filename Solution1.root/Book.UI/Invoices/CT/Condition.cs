using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.UI.Invoices.CT
{
    public class Condition : Book.UI.Query.Condition
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string StartCOId { get; set; }

        public string EndCOId { get; set; }

        public string StartCTId { get; set; }

        public string EndCTId { get; set; }

        public string CusId { get; set; }

        public string SupplierId { get; set; }
    }
}
