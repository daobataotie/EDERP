//------------------------------------------------------------------------------
//
// file name：PCFinishCheck.cs
// author: mayanjun
// create date：2011-11-12 15:10:07
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 成品检验单
    /// </summary>
    [Serializable]
    public partial class PCFinishCheck
    {
        public string Workhousename { get; set; }

        public string ProductName { get; set; }

        public string Employee0Name { get; set; }

        public string Employee1Name { get; set; }
    }
}
