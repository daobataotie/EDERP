using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.UI.Accounting.AtBillsIncome
{
    public class Condition
    {
        public DateTime? KPStart { get; set; }

        public DateTime? KPEnd { get; set; }

        public DateTime? DQStart { get; set; }

        public DateTime? DQEnd { get; set; }

        public DateTime? YDStart { get; set; }

        public DateTime? YDEnd { get; set; }

        public string IdStart { get; set; }

        public string IdEnd { get; set; }

        public string Category { get; set; }

        public string InvoiceState { get; set; }

        public string BankNameStart { get; set; }

        public string BankNameEnd { get; set; }

        public string SupplierIdStart { get; set; }

        public string SupplierIdEnd { get; set; }

        public string CustomerIdStart { get; set; }

        public string CustomerIdEnd { get; set; }
    }
}
