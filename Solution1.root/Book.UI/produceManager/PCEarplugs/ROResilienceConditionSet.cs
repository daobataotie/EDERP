using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCEarplugs
{
    public partial class ROResilienceConditionSet : DevExpress.XtraReports.UI.XtraReport
    {
        public ROResilienceConditionSet(string tkyName, string scrName, string PCEarplugsResilienceCheckId)
        {
            InitializeComponent();

            this.TC_TKYName.Text = tkyName;
            this.TC_SCRName.Text = scrName;

            Model.PCEarplugsResilienceConditionSet set = new BL.PCEarplugsResilienceConditionSetManager().mGetLast(PCEarplugsResilienceCheckId);
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
        }
    }
}
