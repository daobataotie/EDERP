//------------------------------------------------------------------------------
//
// file name：BGHandbookRange.cs
// author: mayanjun
// create date：2013-4-17 15:13:05
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
namespace Book.Model
{
    /// <summary>
    /// 手册归类
    /// </summary>
    [Serializable]
    public partial class BGHandbookRange
    {
        private IList<Model.BGHandbookRangeDetail> details = new List<Model.BGHandbookRangeDetail>();

        public IList<Model.BGHandbookRangeDetail> Details
        {
            get { return details; }
            set { details = value; }
        }
    }
}
