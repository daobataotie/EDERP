//------------------------------------------------------------------------------
//
// file name：AtCashAccount.cs
// author: mayanjun
// create date：2014/8/11 19:50:50
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	public partial class AtCashAccount
	{
        System.Collections.Generic.IList<Model.AtCashAccountDetail> _detail = new System.Collections.Generic.List<Model.AtCashAccountDetail>();

        public System.Collections.Generic.IList<Model.AtCashAccountDetail> Detail
        {
            get { return _detail; }
            set { _detail = value; }
        }
	}
}