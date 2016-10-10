//------------------------------------------------------------------------------
//
// file name：PCDataInput.cs
// author: mayanjun
// create date：2015/1/18 下午 08:03:13
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
namespace Book.Model
{
    /// <summary>
    /// 数据输入页
    /// </summary>
    [Serializable]
    public partial class PCDataInput
    {
        public IList<Model.PCOpticalMachine> PCOpticalMachineList = new List<Model.PCOpticalMachine>();
        public IList<Model.PCLaserMachine> PCLaserMachineList = new List<Model.PCLaserMachine>();
        public IList<Model.PCDefinition> PCDefinitionList = new List<Model.PCDefinition>();
        public IList<Model.PCPerspective> PCPerspectiveList = new List<Model.PCPerspective>();
        public IList<Model.PCHaze> PCHazeList = new List<Model.PCHaze>();
        public IList<Model.PCEuropeOptical> PCEuropeOpticalList = new List<Model.PCEuropeOptical>();
    }
}