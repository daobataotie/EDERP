//------------------------------------------------------------------------------
//
// file name：PCEarplugsDecibelCheck.cs
// author: mayanjun
// create date：2019/5/12 14:43:54
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 耳塞分贝测试
	/// </summary>
	[Serializable]
	public partial class PCEarplugsDecibelCheck
	{
        System.Collections.Generic.IList<Model.PCEarplugsDecibelCheckDetail> details = new System.Collections.Generic.List<Model.PCEarplugsDecibelCheckDetail>();

        public System.Collections.Generic.IList<Model.PCEarplugsDecibelCheckDetail> Details
        {
            get { return details; }
            set { details = value; }
        }
	}
}