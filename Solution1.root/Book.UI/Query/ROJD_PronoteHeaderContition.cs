using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.UI.Query
{
    public class ROJD_PronoteHeaderContition : Query.Condition
    {
        public DateTime InvoiceDate_Start { get; set; }

        public DateTime InvoiceDate_End { get; set; }

        public string PronoteHeaderId { get; set; }

        public Model.Customer XSCustomer { get; set; }

        public string CusInvoiceXOId { get; set; }

        public Model.Employee EmployeeYW { get; set; }

        public string PronoteHeaderType { get; set; }

        public Model.WorkHouse ProduceWorkHouse { get; set; }

        public Model.Product ProduceProduct { get; set; }

        /// <summary>
        /// 是否已结案
        /// </summary>
        public bool? IsJieAn { get; set; }
    }
}
