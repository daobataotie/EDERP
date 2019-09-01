using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCEarplugs
{
    public partial class ROResilienceConditionSet : DevExpress.XtraReports.UI.XtraReport
    {
        public string PCEarplugsResilienceCheckDetailId { get; set; }

        public ROResilienceConditionSet(string tkyName, string scrName)
        {
            InitializeComponent();

            this.TC_TKYName.Text = tkyName;
            this.TC_SCRName.Text = scrName;

        }

        private void ROResilienceConditionSet_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //鐵塊壓，手搓揉
            Model.PCEarplugsResilienceConditionSet set = new BL.PCEarplugsResilienceConditionSetManager().mGetLast(PCEarplugsResilienceCheckDetailId);
            if (set != null)
            {
                this.TC_TKY1.Text = set.TKY1.HasValue ? set.TKY1.Value.ToString("0.#") + " 秒" : "";
                this.TC_TKY2.Text = set.TKY2.HasValue ? set.TKY2.Value.ToString("0.#") + " 秒" : "";
                this.TC_TKY3.Text = set.TKY3.HasValue ? set.TKY3.Value.ToString("0.#") + " 秒" : "";
                this.TC_TKY4.Text = set.TKY4.HasValue ? set.TKY4.Value.ToString("0.#") + " 秒" : "";
                this.TC_TKY5.Text = set.TKY5.HasValue ? set.TKY5.Value.ToString("0.#") + " 秒" : "";
                this.TC_TKY6.Text = set.TKY6.HasValue ? set.TKY6.Value.ToString("0.#") + " 秒" : "";


                this.TC_SCR1.Text = set.SCR1.HasValue ? set.SCR1.Value.ToString("0.#") + " 秒" : "";
                this.TC_SCR2.Text = set.SCR2.HasValue ? set.SCR2.Value.ToString("0.#") + " 秒" : "";
                this.TC_SCR3.Text = set.SCR3.HasValue ? set.SCR3.Value.ToString("0.#") + " 秒" : "";
                this.TC_SCR4.Text = set.SCR4.HasValue ? set.SCR4.Value.ToString("0.#") + " 秒" : "";
                this.TC_SCR5.Text = set.SCR5.HasValue ? set.SCR5.Value.ToString("0.#") + " 秒" : "";
                this.TC_SCR6.Text = set.SCR6.HasValue ? set.SCR6.Value.ToString("0.#") + " 秒" : "";
            }
            else
            {
                this.TC_TKY1.Text = "";
                this.TC_TKY2.Text = "";
                this.TC_TKY3.Text = "";
                this.TC_TKY4.Text = "";
                this.TC_TKY5.Text = "";
                this.TC_TKY6.Text = "";


                this.TC_SCR1.Text = "";
                this.TC_SCR2.Text = "";
                this.TC_SCR3.Text = "";
                this.TC_SCR4.Text = "";
                this.TC_SCR5.Text = "";
                this.TC_SCR6.Text = "";
            }

            //重量
            Model.PCEarplugsResilienceWeight weight = new BL.PCEarplugsResilienceWeightManager().mGetLast(PCEarplugsResilienceCheckDetailId);
            if (weight != null)
            {
                this.TC_Weight1.Text = weight.Weight1.HasValue ? weight.Weight1.Value.ToString("0.#") : "";
                this.TC_Weight2.Text = weight.Weight2.HasValue ? weight.Weight2.Value.ToString("0.#") : "";
                this.TC_Weight3.Text = weight.Weight3.HasValue ? weight.Weight3.Value.ToString("0.#") : "";
                this.TC_Weight4.Text = weight.Weight4.HasValue ? weight.Weight4.Value.ToString("0.#") : "";
                this.TC_Weight5.Text = weight.Weight5.HasValue ? weight.Weight5.Value.ToString("0.#") : "";
                this.TC_Weight6.Text = weight.Weight6.HasValue ? weight.Weight6.Value.ToString("0.#") : "";
            }
            else
            {
                this.xrTable4.Visible = false;
                this.xrTable4.HeightF = 0;
                this.ReportHeader.HeightF = 180;
            }
        }
    }
}
