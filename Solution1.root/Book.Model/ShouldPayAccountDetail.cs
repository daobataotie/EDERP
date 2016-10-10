//------------------------------------------------------------------------------
//
// file name：ShouldPayAccountDetail.cs
// author: mayanjun
// create date：2014/7/16 22:02:40
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 应付账款明细表详细
	/// </summary>
	[Serializable]
	public partial class ShouldPayAccountDetail
	{
        /// <summary>
        /// 发票抬头
        /// </summary>
        public string HeaderName { get; set; }

        public readonly static string PRO_HeaderName = "HeaderName";
	}
}