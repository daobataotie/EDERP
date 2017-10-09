//------------------------------------------------------------------------------
//
// file name：PCImpactCheckDetailAccessor.cs
// author: mayanjun
// create date：2011-11-15 14:09:35
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Book.DA.SQLServer
{
    /// <summary>
    /// Data accessor of PCImpactCheckDetail
    /// </summary>
    public partial class PCImpactCheckDetailAccessor : EntityAccessor, IPCImpactCheckDetailAccessor
    {
        public IList<Book.Model.PCImpactCheckDetail> Select(string PCImpactCheckId)
        {
            return sqlmapper.QueryForList<Model.PCImpactCheckDetail>("PCImpactCheckDetail.select_byPCImpactCheckID", PCImpactCheckId);
        }

        public void DeleteByPCImpactCheckId(string PCImpactCheckId)
        {
            sqlmapper.Delete("PCImpactCheckDetail.DeleteByPCImpactCheckId", PCImpactCheckId);
        }

        public IList<Book.Model.PCImpactCheckDetail> SelectByDateRage(DateTime StartDate, DateTime EndDate, Book.Model.Product product, string CusXOId)
        {
            //Hashtable ht = new Hashtable();
            //ht.Add("startdate", StartDate);
            //ht.Add("enddate", EndDate);
            //StringBuilder sql = new StringBuilder();
            //if (!string.IsNullOrEmpty(CusXOId))
            //    sql.Append(" and d.InvoiceCusXOId like '%" + CusXOId + "%'");
            //if (product != null)
            //    sql.Append(" and d.ProductId = '" + product.ProductId + "'");
            //ht.Add("sql", sql.ToString());
            //return sqlmapper.QueryForList<Model.PCImpactCheckDetail>("PCImpactCheckDetail.SelectByDateRange", ht);
            StringBuilder sql = new StringBuilder();
            sql.Append("select d.PCImpactCheckId,d.attrDate,d.attrBanBie,d.PronoteHeaderId,d.InvoiceCusXOId,d.PCImpactCheckQuantity,e.EmployeeName,pro.ProductName from PCImpactCheckDetail d left join PCImpactCheck p on d.PCImpactCheckId=p.PCImpactCheckId left join Employee e on e.EmployeeId=p.EmployeeId  left join Product pro on d.ProductId=pro.ProductId  where d.attrDate between '" + StartDate.ToString("yyyy-MM-dd") + "' and '" + EndDate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            if (!string.IsNullOrEmpty(CusXOId))
                sql.Append(" and d.InvoiceCusXOId = '" + CusXOId + "'");
            if (product != null)
                sql.Append(" and d.ProductId = '" + product.ProductId + "'");
            return this.DataReaderBind<Model.PCImpactCheckDetail>(sql.ToString(), null, CommandType.Text);
        }

        public string SelectChecker(string id)
        {
            return sqlmapper.QueryForObject<string>("PCImpactCheckDetail.SelectChecker", id);
        }

        public DataTable SelectByHeaderId(string Id)
        {
            string sql = "select pc.PCImpactCheckDetailId,pc.PCImpactCheckId,pc.attrDate,pc.attrBanBie,(case pc.attrGlassUpL when '0' then '√' when '1' then '△' when '2' then '×' end) AttrGlassUpLDis ,(case pc.attrGlassUpR when '0' then '√' when '1' then '△' when '2' then '×' end) AttrGlassUpRDis ,(case pc.attrGlassDownL when '0' then '√' when '1' then '△' when '2' then '×' end) AttrGlassDownLDis ,(case pc.attrGlassDownR when '0' then '√' when '1' then '△' when '2' then '×' end) AttrGlassDownRDis ,(case pc.attrGlassLeftL when '0' then '√' when '1' then '△' when '2' then '×' end) AttrGlassLeftLDis,(case pc.attrGlassLeftR when '0' then '√' when '1' then '△' when '2' then '×' end) AttrGlassLeftRDis,(case pc.attrGlassRightL when '0' then '√' when '1' then '△' when '2' then '×' end) AttrGlassRightLDis,(case pc.attrGlassRightR when '0' then '√' when '1' then '△' when '2' then '×' end) AttrGlassRightRDis,(case pc.attrCentralL when '0' then '√' when '1' then '△' when '2' then '×' end) AttrCentralLDis,(case pc.attrCentralR when '0' then '√' when '1' then '△' when '2' then '×' end) AttrCentralRDis,(case pc.attrJieHenL when '0' then '√' when '1' then '△' when '2' then '×' end) AttrJieHenLDis,(case pc.attrJieHenR when '0' then '√' when '1' then '△' when '2' then '×' end) AttrJieHenRDis,(case pc.attrWingL when '0' then '√' when '1' then '△' when '2' then '×' end) AttrWingLDis,(case pc.attrWingR when '0' then '√' when '1' then '△' when '2' then '×' end) AttrWingRDis,(case pc.attr_15L when '0' then '√' when '1' then '△' when '2' then '×' end) Attr_15LDis,(case pc.attr_15R when '0' then '√' when '1' then '△' when '2' then '×' end) Attr_15RDis,(case pc.attr0L when '0' then '√' when '1' then '△' when '2' then '×' end) Attr0LDis,(case pc.attr0R when '0' then '√' when '1' then '△' when '2' then '×' end) Attr0RDis,(case pc.attr15L when '0' then '√' when '1' then '△' when '2' then '×' end) Attr15LDis,(case pc.attr15R when '0' then '√' when '1' then '△' when '2' then '×' end) Attr15RDis,(case pc.attr30L when '0' then '√' when '1' then '△' when '2' then '×' end) Attr30LDis,(case pc.attr30R when '0' then '√' when '1' then '△' when '2' then '×' end) Attr30RDis,(case pc.attr45L when '0' then '√' when '1' then '△' when '2' then '×' end) Attr45LDis,(case pc.attr45R when '0' then '√' when '1' then '△' when '2' then '×' end) Attr45RDis,(case pc.attr60L when '0' then '√' when '1' then '△' when '2' then '×' end) Attr60LDis,(case pc.attr60R when '0' then '√' when '1' then '△' when '2' then '×' end) Attr60RDis,(case pc.attr75L when '0' then '√' when '1' then '△' when '2' then '×' end) Attr75LDis,(case pc.attr75R when '0' then '√' when '1' then '△' when '2' then '×' end) Attr75RDis,(case pc.attr90L when '0' then '√' when '1' then '△' when '2' then '×' end) Attr90LDis,(case pc.attr90R when '0' then '√' when '1' then '△' when '2' then '×' end) Attr90RDis,(case pc.attrNoseCentral when '0' then '√' when '1' then '△' when '2' then '×' end) AttrNoseCentralDis,(case pc.attrGuanZui when '0' then '√' when '1' then '△' when '2' then '×' end) AttrGuanZuiDis,(case pc.attrFootL when '0' then '√' when '1' then '△' when '2' then '×' end) AttrFootLDis,(case pc.attrFootR when '0' then '√' when '1' then '△' when '2' then '×' end) AttrFootRDis,(case pc.attr90T when '0' then '√' when '1' then '△' when '2' then '×' end) Attr90TDis,(case pc.attr90B when '0' then '√' when '1' then '△' when '2' then '×' end) Attr90BDis,pc.ProductId,pc.PronoteHeaderId,pc.PCImpactCheckQuantity,pc.InvoiceCusXOId,pc.InvoiceXOQuantity,pc.mCheckStandard,pc.ProductUnitId,p.ProductName as Product from PCImpactCheckDetail pc left join Product p on pc.ProductId=p.ProductId  where pc.PCImpactCheckId='" + Id + "'";
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlmapper.DataSource.ConnectionString);
            sda.Fill(dt);
            return dt;
        }
    }
}
