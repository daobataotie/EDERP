//------------------------------------------------------------------------------
//
// file name：BGHandbookRangeDetailAccessor.cs
// author: mayanjun
// create date：2013-4-17 15:13:04
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
    /// Data accessor of BGHandbookRangeDetail
    /// </summary>
    public partial class BGHandbookRangeDetailAccessor : EntityAccessor, IBGHandbookRangeDetailAccessor
    {
        public IList<Model.BGHandbookRangeDetail> SelectByBGHandbookId(string Id)
        {
            return sqlmapper.QueryForList<Model.BGHandbookRangeDetail>("BGHandbookRangeDetail.SelectByBGHandbookId", Id);
        }

        public void DeleteByBGHandbookId(string Id)
        {
            sqlmapper.Delete("BGHandbookRangeDetail.DeleteByBGHandbookId", Id);
        }
    }
}
