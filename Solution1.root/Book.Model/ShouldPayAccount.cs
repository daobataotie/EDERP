//------------------------------------------------------------------------------
//
// file name：ShouldPayAccount.cs
// author: mayanjun
// create date：2014/7/16 22:02:40
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 应付账款明细表
	/// </summary>
	[Serializable]
	public partial class ShouldPayAccount
	{
        System.Collections.Generic.IList<Model.ShouldPayAccountDetail> detail = new System.Collections.Generic.List<Model.ShouldPayAccountDetail>();

        public System.Collections.Generic.IList<Model.ShouldPayAccountDetail> Detail
        {
            get { return detail; }
            set { detail = value; }
        }
	}
}