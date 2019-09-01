using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.produceManager.PCExportReportANSI
{
    public partial class PCDefinitionRO : DevExpress.XtraReports.UI.XtraReport
    {
        public PCDefinitionRO()
        {
            InitializeComponent();
        }

        public PCDefinitionRO(IList<Model.PCDefinition> list)
            : this()
        {
            this.DataSource = list;

            this.TCNoId.DataBindings.Add("Text", this.DataSource, Model.PCDefinition.PRO_NoId);
            this.TCLeftDefinition.DataBindings.Add("Text", this.DataSource, Model.PCDefinition.PRO_LeftDefinition);
            this.TCRightDefinition.DataBindings.Add("Text", this.DataSource, Model.PCDefinition.PRO_RightDefinition);
            this.TCLeftD1.DataBindings.Add("Text", this.DataSource, Model.PCDefinition.PRO_LeftD1);
            this.TCRightD1.DataBindings.Add("Text", this.DataSource, Model.PCDefinition.PRO_RightD1);
            this.TCLeftD2.DataBindings.Add("Text", this.DataSource, Model.PCDefinition.PRO_LeftD2);
            this.TCRightD2.DataBindings.Add("Text", this.DataSource, Model.PCDefinition.PRO_RightD2);
            this.TCLeftVisibility.DataBindings.Add("Text", this.DataSource, Model.PCDefinition.PRO_LeftVisibility);
            this.TCRightVisibility.DataBindings.Add("Text", this.DataSource, Model.PCDefinition.PRO_RightVisibility);
        }
    }
}
