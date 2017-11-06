//------------------------------------------------------------------------------
//
// file name：PCAssemblyInspectionDetailAccessor.cs
// author: mayanjun
// create date：2017-11-05 16:28:12
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
    /// Data accessor of PCAssemblyInspectionDetail
    /// </summary>
    public partial class PCAssemblyInspectionDetailAccessor : EntityAccessor, IPCAssemblyInspectionDetailAccessor
    {
        public IList<Model.PCAssemblyInspectionDetail> SelectByHeaderId(string id)
        {
            return sqlmapper.QueryForList<Model.PCAssemblyInspectionDetail>("PCAssemblyInspectionDetail.SelectByHeaderId", id);
        }

        public void DeleteByHeaderId(string id)
        {
            sqlmapper.Delete("PCAssemblyInspectionDetail.DeleteByHeaderId", id);
        }

        public IList<Model.PCAssemblyInspectionDetail> SelectByCondition(DateTime startDate, DateTime endDate, string startPId, string endPId, string invoiceCusId)
        {
            StringBuilder sb = new StringBuilder("select * from PCAssemblyInspectionDetail pcad left join PCAssemblyInspection pca on pcad.PCAssemblyInspectionId=pca.PCAssemblyInspectionId where pca.PCAssemblyInspectionDate between '" + startDate.ToString("yyyy-MM-dd") + "' and '" + endDate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            if (!string.IsNullOrEmpty(startPId) || !string.IsNullOrEmpty(endPId))
            {
                if (!string.IsNullOrEmpty(startPId) && !string.IsNullOrEmpty(endPId))
                {
                    sb.Append(" and pcad.ProductId in (select ProductId from Product where Id between '" + startPId + "' and '" + endPId + "') ");
                }
                else
                    sb.Append(" and pcad.ProductId in (select ProductId from Product where Id ='" + (string.IsNullOrEmpty(startPId) ? endPId : startPId) + "') ");
            }
            if (!string.IsNullOrEmpty(invoiceCusId))
            {
                sb.Append(" and pca.InvoiceCusId='" + invoiceCusId + "'");
            }

            return this.DataReaderBind<Model.PCAssemblyInspectionDetail>(sb.ToString(),null, CommandType.Text);
        }
    }
}
