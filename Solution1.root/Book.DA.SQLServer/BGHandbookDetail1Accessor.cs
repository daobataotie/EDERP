//------------------------------------------------------------------------------
//
// file name：BGHandbookDetail1Accessor.cs
// author: mayanjun
// create date：2013-4-16 11:58:59
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
    /// Data accessor of BGHandbookDetail1
    /// </summary>
    public partial class BGHandbookDetail1Accessor : EntityAccessor, IBGHandbookDetail1Accessor
    {

        public IList<Book.Model.BGHandbookDetail1> Select(string pac)
        {
            return sqlmapper.QueryForList<Model.BGHandbookDetail1>("BGHandbookDetail1.select_byheader", pac);
        }
    }
}
