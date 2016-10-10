//------------------------------------------------------------------------------
//
// file name：BGHandbookDetail2Accessor.cs
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
    /// Data accessor of BGHandbookDetail2
    /// </summary>
    public partial class BGHandbookDetail2Accessor : EntityAccessor, IBGHandbookDetail2Accessor
    {
        public IList<Book.Model.BGHandbookDetail2> Select(string pac)
        {
            return sqlmapper.QueryForList<Model.BGHandbookDetail2>("BGHandbookDetail2.select_byheader", pac);
        }

        public void UpdateCeIn(string bgid,string lid,double quantity)
        {
            Hashtable ht = new Hashtable();
            ht.Add("BGHandbookId",bgid);
            ht.Add("id", lid);
            ht.Add("ZhuanCeInQuantity", quantity);
             sqlmapper.Update("BGHandbookDetail2.update_CeIn", ht);
         }
        
    }
}
