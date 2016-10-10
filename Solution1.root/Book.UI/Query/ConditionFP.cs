using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.UI.Query
{
    /// <summary>
    /// 发票打印Help类
    /// </summary>
    public class ConditionFP : Condition
    {
        private DateTime _startDate;

        private DateTime _endDate;

        private Model.Supplier _supplier;

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public DateTime EndDate
        {
            get { return _endDate.Date.AddDays(1).AddSeconds(-1); }
            set { _endDate = value; }
        }

        public Model.Supplier Supplier
        {
            get { return _supplier; }
            set { _supplier = value; }
        }
    }
}
