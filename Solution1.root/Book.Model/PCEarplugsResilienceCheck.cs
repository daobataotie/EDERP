//------------------------------------------------------------------------------
//
// file name：PCEarplugsResilienceCheck.cs
// author: mayanjun
// create date：2019/5/10 10:49:43
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	public partial class PCEarplugsResilienceCheck
	{
        System.Collections.Generic.IList<Model.PCEarplugsResilienceCheckDetail> _details = new System.Collections.Generic.List<Model.PCEarplugsResilienceCheckDetail>();

        public System.Collections.Generic.IList<Model.PCEarplugsResilienceCheckDetail> Details
        {
            get { return _details; }
            set { _details = value; }
        }
	}
}