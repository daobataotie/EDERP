//------------------------------------------------------------------------------
//
// file name：PCAssemblyInspection.cs
// author: mayanjun
// create date：2017-11-05 16:28:12
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	public partial class PCAssemblyInspection
	{
        private System.Collections.Generic.IList<Model.PCAssemblyInspectionDetail> details = new System.Collections.Generic.List<Model.PCAssemblyInspectionDetail>();

        public System.Collections.Generic.IList<Model.PCAssemblyInspectionDetail> Details
        {
            get { return details; }
            set { details = value; }
        }
	}
}