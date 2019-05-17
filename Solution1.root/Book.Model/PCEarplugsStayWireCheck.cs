//------------------------------------------------------------------------------
//
// file name：PCEarplugsStayWireCheck.cs
// author: mayanjun
// create date：2019/5/14 09:57:06
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 耳塞拉线测试表
	/// </summary>
	[Serializable]
	public partial class PCEarplugsStayWireCheck
	{
        System.Collections.Generic.IList<Model.PCEarplugsStayWireCheckDetail> details = new System.Collections.Generic.List<Model.PCEarplugsStayWireCheckDetail>();

        public System.Collections.Generic.IList<Model.PCEarplugsStayWireCheckDetail> Details
        {
            get { return details; }
            set { details = value; }
        }
	}
}