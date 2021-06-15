using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.produceManager.PCExportReportANSI
{
    /// <summary>
    /// 光学机
    /// </summary>
    public partial class PCOpticalMachineRO_2 : DevExpress.XtraReports.UI.XtraReport
    {
        public PCOpticalMachineRO_2()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 光学机打印，支持多笔
        /// </summary>
        /// <param name="PCOpticalMachineList"></param>
        /// <param name="pcDataInput"></param>
        public PCOpticalMachineRO_2(IList<Model.PCOpticalMachine> PCOpticalMachineList, Model.PCDataInput pcDataInput)
            : this()
        {
            this.TCDate.Text = pcDataInput.PCDataInputDate.HasValue ? pcDataInput.PCDataInputDate.Value.ToString("yyyy-MM-dd") : "";
            this.TCTestQuantity.Text = PCOpticalMachineList.Count.ToString();
            this.TCEmployee.Text = pcDataInput.Employee == null ? "" : pcDataInput.Employee.ToString();

            IList<Model.PCOpticalMachine> list1 = new List<Model.PCOpticalMachine>();  //分别存放奇偶数据
            IList<Model.PCOpticalMachine> list2 = new List<Model.PCOpticalMachine>();
            for (int i = 0; i < PCOpticalMachineList.Count; i++)
            {
                if (i % 2 == 0)
                    list1.Add(PCOpticalMachineList[i]);
                else
                    list2.Add(PCOpticalMachineList[i]);
            }

            this.xrSubreport1.ReportSource = new PCOpticalMachineRO_2_Sub(list1);
            this.xrSubreport2.ReportSource = new PCOpticalMachineRO_2_Sub(list2);
        }

    }
}
