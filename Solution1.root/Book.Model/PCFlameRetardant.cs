//------------------------------------------------------------------------------
//
// file name：PCFlameRetardant.cs
// author: mayanjun
// create date：2019/1/4 10:22:35
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	public partial class PCFlameRetardant
	{
        System.Collections.Generic.IList<Model.PCFlameRetardantDetail> _details = new System.Collections.Generic.List<Model.PCFlameRetardantDetail>();

        public System.Collections.Generic.IList<Model.PCFlameRetardantDetail> Details
        {
            get { return _details; }
            set { _details = value; }
        }
	}
}