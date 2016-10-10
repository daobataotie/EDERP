using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.UI.Settings.StockLimitations
{
    public class StockCheckCondition
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public String Invoiceid { get; set; }
    }
}
