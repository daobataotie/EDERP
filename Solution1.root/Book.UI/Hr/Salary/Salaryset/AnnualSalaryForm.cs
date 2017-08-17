using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Book.UI.Hr.Salary.Salaryset
{
    public partial class AnnualSalaryForm : DevExpress.XtraEditors.XtraForm
    {
        BL.MonthlySalaryManager monthlySalaryManager = new Book.BL.MonthlySalaryManager();
        private int hryear = 0;
        private int hrmonth = 0;
        private IList<Model.Employee> _emplist = new List<Model.Employee>();
        protected BL.EmployeeManager employeeManager = new Book.BL.EmployeeManager();
        private BL.HrDailyEmployeeAttendInfoManager _hrManager = new Book.BL.HrDailyEmployeeAttendInfoManager();
        private BL.AnnualHolidayManager annualHolidayManager = new Book.BL.AnnualHolidayManager();
        double KuangzhiFactor = 0;
        List<HelperEmp> heList = new List<HelperEmp>();

        public AnnualSalaryForm()
        {
            InitializeComponent();
        }

        private void PrintMonthSalary_Load(object sender, EventArgs e)
        {
            DateTime date = this.monthlySalaryManager.get_MaxIdentifyDateMonth();

            if (date.Year != 1)
            {
                DateTime strdate = date.AddYears(1);

                for (int i = 0; i < 10; i++)
                {
                    this.comboBoxEdit1.Properties.Items.Add(strdate.AddYears(-1).ToString("yyyy年"));
                    strdate = strdate.AddYears(-1);
                }
                this.comboBoxEdit1.SelectedIndex = 0;
                this.hryear = Int32.Parse(this.comboBoxEdit1.Text.Substring(0, 4));
                _emplist = this.employeeManager.SelectHrDailyAttendByMonth(new DateTime(hryear, 1, 1));
                this.bindingSource1.DataSource = _emplist;
            }
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.hryear = Int32.Parse(this.comboBoxEdit1.Text.Substring(0, 4));
            this._emplist = this.employeeManager.SelectHrDailyAttendByMonth(new DateTime(hryear, 1, 1));
            this.bindingSource1.DataSource = _emplist;
        }

        private void btn_PrintTotal_Click(object sender, EventArgs e)
        {
            this.gridView2.PostEditor();
            this.gridView2.UpdateCurrentRow();

            this.heList.Clear();
            HelperEmp he;
            foreach (var emp in _emplist)
            {
                he = new HelperEmp();
                he.Employee = emp;

                for (int i = 1; i <= 12; i++)
                {
                    this.hrmonth = i;

                    #region 賦值
                    switch (i)
                    {
                        case 1:
                            he.MonthSalaryClass1 = this.GetEmpSalary(emp);
                            break;
                        case 2:
                            he.MonthSalaryClass2 = this.GetEmpSalary(emp);
                            break;
                        case 3:
                            he.MonthSalaryClass3 = this.GetEmpSalary(emp);
                            break;
                        case 4:
                            he.MonthSalaryClass4 = this.GetEmpSalary(emp);
                            break;
                        case 5:
                            he.MonthSalaryClass5 = this.GetEmpSalary(emp);
                            break;
                        case 6:
                            he.MonthSalaryClass6 = this.GetEmpSalary(emp);
                            break;
                        case 7:
                            he.MonthSalaryClass7 = this.GetEmpSalary(emp);
                            break;
                        case 8:
                            he.MonthSalaryClass8 = this.GetEmpSalary(emp);
                            break;
                        case 9:
                            he.MonthSalaryClass9 = this.GetEmpSalary(emp);
                            break;
                        case 10:
                            he.MonthSalaryClass10 = this.GetEmpSalary(emp);
                            break;
                        case 11:
                            he.MonthSalaryClass11 = this.GetEmpSalary(emp);
                            break;
                        case 12:
                            he.MonthSalaryClass12 = this.GetEmpSalary(emp);
                            break;
                    }
                    #endregion
                }

                #region 加總
                he.MonthSalary1 = he.MonthSalaryClass1.mBasePay + he.MonthSalaryClass1.mDutyPay + he.MonthSalaryClass1.mAnnualHolidayFee;
                he.MonthSalary2 = he.MonthSalaryClass2.mBasePay + he.MonthSalaryClass2.mDutyPay + he.MonthSalaryClass2.mAnnualHolidayFee;
                he.MonthSalary3 = he.MonthSalaryClass3.mBasePay + he.MonthSalaryClass3.mDutyPay + he.MonthSalaryClass3.mAnnualHolidayFee;
                he.MonthSalary4 = he.MonthSalaryClass4.mBasePay + he.MonthSalaryClass4.mDutyPay + he.MonthSalaryClass4.mAnnualHolidayFee;
                he.MonthSalary5 = he.MonthSalaryClass5.mBasePay + he.MonthSalaryClass5.mDutyPay + he.MonthSalaryClass5.mAnnualHolidayFee;
                he.MonthSalary6 = he.MonthSalaryClass6.mBasePay + he.MonthSalaryClass6.mDutyPay + he.MonthSalaryClass6.mAnnualHolidayFee;
                he.MonthSalary7 = he.MonthSalaryClass7.mBasePay + he.MonthSalaryClass7.mDutyPay + he.MonthSalaryClass7.mAnnualHolidayFee;
                he.MonthSalary8 = he.MonthSalaryClass8.mBasePay + he.MonthSalaryClass8.mDutyPay + he.MonthSalaryClass8.mAnnualHolidayFee;
                he.MonthSalary9 = he.MonthSalaryClass9.mBasePay + he.MonthSalaryClass9.mDutyPay + he.MonthSalaryClass9.mAnnualHolidayFee;
                he.MonthSalary10 = he.MonthSalaryClass10.mBasePay + he.MonthSalaryClass10.mDutyPay + he.MonthSalaryClass10.mAnnualHolidayFee;
                he.MonthSalary11 = he.MonthSalaryClass11.mBasePay + he.MonthSalaryClass11.mDutyPay + he.MonthSalaryClass11.mAnnualHolidayFee;
                he.MonthSalary12 = he.MonthSalaryClass12.mBasePay + he.MonthSalaryClass12.mDutyPay + he.MonthSalaryClass12.mAnnualHolidayFee;
                he.MonthSalaryTotal = he.MonthSalary1 + he.MonthSalary2 + he.MonthSalary3 + he.MonthSalary4 + he.MonthSalary5 + he.MonthSalary6 + he.MonthSalary7 + he.MonthSalary8 + he.MonthSalary9 + he.MonthSalary10 + he.MonthSalary11 + he.MonthSalary12;

                he.KouKuan1 = he.MonthSalaryClass1.mLatePunish + he.MonthSalaryClass1.mJiuYuanKouJiao + he.MonthSalaryClass1.mOtherPunish;
                he.KouKuan2 = he.MonthSalaryClass2.mLatePunish + he.MonthSalaryClass2.mJiuYuanKouJiao + he.MonthSalaryClass2.mOtherPunish;
                he.KouKuan3 = he.MonthSalaryClass3.mLatePunish + he.MonthSalaryClass3.mJiuYuanKouJiao + he.MonthSalaryClass3.mOtherPunish;
                he.KouKuan4 = he.MonthSalaryClass4.mLatePunish + he.MonthSalaryClass4.mJiuYuanKouJiao + he.MonthSalaryClass4.mOtherPunish;
                he.KouKuan5 = he.MonthSalaryClass5.mLatePunish + he.MonthSalaryClass5.mJiuYuanKouJiao + he.MonthSalaryClass5.mOtherPunish;
                he.KouKuan6 = he.MonthSalaryClass6.mLatePunish + he.MonthSalaryClass6.mJiuYuanKouJiao + he.MonthSalaryClass6.mOtherPunish;
                he.KouKuan7 = he.MonthSalaryClass7.mLatePunish + he.MonthSalaryClass7.mJiuYuanKouJiao + he.MonthSalaryClass7.mOtherPunish;
                he.KouKuan8 = he.MonthSalaryClass8.mLatePunish + he.MonthSalaryClass8.mJiuYuanKouJiao + he.MonthSalaryClass8.mOtherPunish;
                he.KouKuan9 = he.MonthSalaryClass9.mLatePunish + he.MonthSalaryClass9.mJiuYuanKouJiao + he.MonthSalaryClass9.mOtherPunish;
                he.KouKuan10 = he.MonthSalaryClass10.mLatePunish + he.MonthSalaryClass10.mJiuYuanKouJiao + he.MonthSalaryClass10.mOtherPunish;
                he.KouKuan11 = he.MonthSalaryClass11.mLatePunish + he.MonthSalaryClass11.mJiuYuanKouJiao + he.MonthSalaryClass11.mOtherPunish;
                he.KouKuan12 = he.MonthSalaryClass12.mLatePunish + he.MonthSalaryClass12.mJiuYuanKouJiao + he.MonthSalaryClass12.mOtherPunish;
                he.KouKuanTotal = he.KouKuan1 + he.KouKuan2 + he.KouKuan3 + he.KouKuan4 + he.KouKuan5 + he.KouKuan6 + he.KouKuan7 + he.KouKuan8 + he.KouKuan9 + he.KouKuan10 + he.KouKuan11 + he.KouKuan12;

                he.LunchFeeTotal = he.MonthSalaryClass1.mLunchFee + he.MonthSalaryClass2.mLunchFee + he.MonthSalaryClass3.mLunchFee + he.MonthSalaryClass4.mLunchFee + he.MonthSalaryClass5.mLunchFee + he.MonthSalaryClass6.mLunchFee + he.MonthSalaryClass7.mLunchFee + he.MonthSalaryClass8.mLunchFee + he.MonthSalaryClass9.mLunchFee + he.MonthSalaryClass10.mLunchFee + he.MonthSalaryClass11.mLunchFee + he.MonthSalaryClass12.mLunchFee;

                he.OverFee1 = he.MonthSalaryClass1.mGeneralOverTimeFee + he.MonthSalaryClass1.mHolidayOverTimeFee;
                he.OverFee2 = he.MonthSalaryClass2.mGeneralOverTimeFee + he.MonthSalaryClass2.mHolidayOverTimeFee;
                he.OverFee3 = he.MonthSalaryClass3.mGeneralOverTimeFee + he.MonthSalaryClass3.mHolidayOverTimeFee;
                he.OverFee4 = he.MonthSalaryClass4.mGeneralOverTimeFee + he.MonthSalaryClass4.mHolidayOverTimeFee;
                he.OverFee5 = he.MonthSalaryClass5.mGeneralOverTimeFee + he.MonthSalaryClass5.mHolidayOverTimeFee;
                he.OverFee6 = he.MonthSalaryClass6.mGeneralOverTimeFee + he.MonthSalaryClass6.mHolidayOverTimeFee;
                he.OverFee7 = he.MonthSalaryClass7.mGeneralOverTimeFee + he.MonthSalaryClass7.mHolidayOverTimeFee;
                he.OverFee8 = he.MonthSalaryClass8.mGeneralOverTimeFee + he.MonthSalaryClass8.mHolidayOverTimeFee;
                he.OverFee9 = he.MonthSalaryClass9.mGeneralOverTimeFee + he.MonthSalaryClass9.mHolidayOverTimeFee;
                he.OverFee10 = he.MonthSalaryClass10.mGeneralOverTimeFee + he.MonthSalaryClass10.mHolidayOverTimeFee;
                he.OverFee11 = he.MonthSalaryClass11.mGeneralOverTimeFee + he.MonthSalaryClass11.mHolidayOverTimeFee;
                he.OverFee12 = he.MonthSalaryClass12.mGeneralOverTimeFee + he.MonthSalaryClass12.mHolidayOverTimeFee;
                he.OverFeeTotal = he.OverFee1 + he.OverFee2 + he.OverFee3 + he.OverFee4 + he.OverFee5 + he.OverFee6 + he.OverFee7 + he.OverFee8 + he.OverFee9 + he.OverFee10 + he.OverFee11 + he.OverFee12;

                #endregion

                heList.Add(he);
            }

            AnnualSalaryRO ro = new AnnualSalaryRO(this.heList, this.hryear.ToString());
            ro.ShowPreviewDialog();
        }

        private void btn_PrintSelected_Click(object sender, EventArgs e)
        {
            this.gridView2.PostEditor();
            this.gridView2.UpdateCurrentRow();
            IList<Model.Employee> list = this._emplist.Where(d => d.IsChecked == true).ToList();
            if (list == null || list.Count == 0)
            {
                MessageBox.Show("至少選擇一位員工！", this.Text, MessageBoxButtons.OK);
                return;
            }
            else
            {
                this.heList.Clear();
                HelperEmp he;
                foreach (var emp in list)
                {
                    he = new HelperEmp();
                    he.Employee = emp;

                    for (int i = 1; i <= 12; i++)
                    {
                        this.hrmonth = i;

                        #region 賦值
                        switch (i)
                        {
                            case 1:
                                he.MonthSalaryClass1 = this.GetEmpSalary(emp);
                                break;
                            case 2:
                                he.MonthSalaryClass2 = this.GetEmpSalary(emp);
                                break;
                            case 3:
                                he.MonthSalaryClass3 = this.GetEmpSalary(emp);
                                break;
                            case 4:
                                he.MonthSalaryClass4 = this.GetEmpSalary(emp);
                                break;
                            case 5:
                                he.MonthSalaryClass5 = this.GetEmpSalary(emp);
                                break;
                            case 6:
                                he.MonthSalaryClass6 = this.GetEmpSalary(emp);
                                break;
                            case 7:
                                he.MonthSalaryClass7 = this.GetEmpSalary(emp);
                                break;
                            case 8:
                                he.MonthSalaryClass8 = this.GetEmpSalary(emp);
                                break;
                            case 9:
                                he.MonthSalaryClass9 = this.GetEmpSalary(emp);
                                break;
                            case 10:
                                he.MonthSalaryClass10 = this.GetEmpSalary(emp);
                                break;
                            case 11:
                                he.MonthSalaryClass11 = this.GetEmpSalary(emp);
                                break;
                            case 12:
                                he.MonthSalaryClass12 = this.GetEmpSalary(emp);
                                break;
                        }
                        #endregion
                    }

                    #region 加總
                    he.MonthSalary1 = he.MonthSalaryClass1.mBasePay + he.MonthSalaryClass1.mDutyPay + he.MonthSalaryClass1.mAnnualHolidayFee;
                    he.MonthSalary2 = he.MonthSalaryClass2.mBasePay + he.MonthSalaryClass2.mDutyPay + he.MonthSalaryClass2.mAnnualHolidayFee;
                    he.MonthSalary3 = he.MonthSalaryClass3.mBasePay + he.MonthSalaryClass3.mDutyPay + he.MonthSalaryClass3.mAnnualHolidayFee;
                    he.MonthSalary4 = he.MonthSalaryClass4.mBasePay + he.MonthSalaryClass4.mDutyPay + he.MonthSalaryClass4.mAnnualHolidayFee;
                    he.MonthSalary5 = he.MonthSalaryClass5.mBasePay + he.MonthSalaryClass5.mDutyPay + he.MonthSalaryClass5.mAnnualHolidayFee;
                    he.MonthSalary6 = he.MonthSalaryClass6.mBasePay + he.MonthSalaryClass6.mDutyPay + he.MonthSalaryClass6.mAnnualHolidayFee;
                    he.MonthSalary7 = he.MonthSalaryClass7.mBasePay + he.MonthSalaryClass7.mDutyPay + he.MonthSalaryClass7.mAnnualHolidayFee;
                    he.MonthSalary8 = he.MonthSalaryClass8.mBasePay + he.MonthSalaryClass8.mDutyPay + he.MonthSalaryClass8.mAnnualHolidayFee;
                    he.MonthSalary9 = he.MonthSalaryClass9.mBasePay + he.MonthSalaryClass9.mDutyPay + he.MonthSalaryClass9.mAnnualHolidayFee;
                    he.MonthSalary10 = he.MonthSalaryClass10.mBasePay + he.MonthSalaryClass10.mDutyPay + he.MonthSalaryClass10.mAnnualHolidayFee;
                    he.MonthSalary11 = he.MonthSalaryClass11.mBasePay + he.MonthSalaryClass11.mDutyPay + he.MonthSalaryClass11.mAnnualHolidayFee;
                    he.MonthSalary12 = he.MonthSalaryClass12.mBasePay + he.MonthSalaryClass12.mDutyPay + he.MonthSalaryClass12.mAnnualHolidayFee;
                    he.MonthSalaryTotal = he.MonthSalary1 + he.MonthSalary2 + he.MonthSalary3 + he.MonthSalary4 + he.MonthSalary5 + he.MonthSalary6 + he.MonthSalary7 + he.MonthSalary8 + he.MonthSalary9 + he.MonthSalary10 + he.MonthSalary11 + he.MonthSalary12;

                    he.KouKuan1 = he.MonthSalaryClass1.mLatePunish + he.MonthSalaryClass1.mJiuYuanKouJiao + he.MonthSalaryClass1.mOtherPunish;
                    he.KouKuan2 = he.MonthSalaryClass2.mLatePunish + he.MonthSalaryClass2.mJiuYuanKouJiao + he.MonthSalaryClass2.mOtherPunish;
                    he.KouKuan3 = he.MonthSalaryClass3.mLatePunish + he.MonthSalaryClass3.mJiuYuanKouJiao + he.MonthSalaryClass3.mOtherPunish;
                    he.KouKuan4 = he.MonthSalaryClass4.mLatePunish + he.MonthSalaryClass4.mJiuYuanKouJiao + he.MonthSalaryClass4.mOtherPunish;
                    he.KouKuan5 = he.MonthSalaryClass5.mLatePunish + he.MonthSalaryClass5.mJiuYuanKouJiao + he.MonthSalaryClass5.mOtherPunish;
                    he.KouKuan6 = he.MonthSalaryClass6.mLatePunish + he.MonthSalaryClass6.mJiuYuanKouJiao + he.MonthSalaryClass6.mOtherPunish;
                    he.KouKuan7 = he.MonthSalaryClass7.mLatePunish + he.MonthSalaryClass7.mJiuYuanKouJiao + he.MonthSalaryClass7.mOtherPunish;
                    he.KouKuan8 = he.MonthSalaryClass8.mLatePunish + he.MonthSalaryClass8.mJiuYuanKouJiao + he.MonthSalaryClass8.mOtherPunish;
                    he.KouKuan9 = he.MonthSalaryClass9.mLatePunish + he.MonthSalaryClass9.mJiuYuanKouJiao + he.MonthSalaryClass9.mOtherPunish;
                    he.KouKuan10 = he.MonthSalaryClass10.mLatePunish + he.MonthSalaryClass10.mJiuYuanKouJiao + he.MonthSalaryClass10.mOtherPunish;
                    he.KouKuan11 = he.MonthSalaryClass11.mLatePunish + he.MonthSalaryClass11.mJiuYuanKouJiao + he.MonthSalaryClass11.mOtherPunish;
                    he.KouKuan12 = he.MonthSalaryClass12.mLatePunish + he.MonthSalaryClass12.mJiuYuanKouJiao + he.MonthSalaryClass12.mOtherPunish;
                    he.KouKuanTotal = he.KouKuan1 + he.KouKuan2 + he.KouKuan3 + he.KouKuan4 + he.KouKuan5 + he.KouKuan6 + he.KouKuan7 + he.KouKuan8 + he.KouKuan9 + he.KouKuan10 + he.KouKuan11 + he.KouKuan12;

                    he.LunchFeeTotal = he.MonthSalaryClass1.mLunchFee + he.MonthSalaryClass2.mLunchFee + he.MonthSalaryClass3.mLunchFee + he.MonthSalaryClass4.mLunchFee + he.MonthSalaryClass5.mLunchFee + he.MonthSalaryClass6.mLunchFee + he.MonthSalaryClass7.mLunchFee + he.MonthSalaryClass8.mLunchFee + he.MonthSalaryClass9.mLunchFee + he.MonthSalaryClass10.mLunchFee + he.MonthSalaryClass11.mLunchFee + he.MonthSalaryClass12.mLunchFee;

                    he.OverFee1 = he.MonthSalaryClass1.mGeneralOverTimeFee + he.MonthSalaryClass1.mHolidayOverTimeFee;
                    he.OverFee2 = he.MonthSalaryClass2.mGeneralOverTimeFee + he.MonthSalaryClass2.mHolidayOverTimeFee;
                    he.OverFee3 = he.MonthSalaryClass3.mGeneralOverTimeFee + he.MonthSalaryClass3.mHolidayOverTimeFee;
                    he.OverFee4 = he.MonthSalaryClass4.mGeneralOverTimeFee + he.MonthSalaryClass4.mHolidayOverTimeFee;
                    he.OverFee5 = he.MonthSalaryClass5.mGeneralOverTimeFee + he.MonthSalaryClass5.mHolidayOverTimeFee;
                    he.OverFee6 = he.MonthSalaryClass6.mGeneralOverTimeFee + he.MonthSalaryClass6.mHolidayOverTimeFee;
                    he.OverFee7 = he.MonthSalaryClass7.mGeneralOverTimeFee + he.MonthSalaryClass7.mHolidayOverTimeFee;
                    he.OverFee8 = he.MonthSalaryClass8.mGeneralOverTimeFee + he.MonthSalaryClass8.mHolidayOverTimeFee;
                    he.OverFee9 = he.MonthSalaryClass9.mGeneralOverTimeFee + he.MonthSalaryClass9.mHolidayOverTimeFee;
                    he.OverFee10 = he.MonthSalaryClass10.mGeneralOverTimeFee + he.MonthSalaryClass10.mHolidayOverTimeFee;
                    he.OverFee11 = he.MonthSalaryClass11.mGeneralOverTimeFee + he.MonthSalaryClass11.mHolidayOverTimeFee;
                    he.OverFee12 = he.MonthSalaryClass12.mGeneralOverTimeFee + he.MonthSalaryClass12.mHolidayOverTimeFee;
                    he.OverFeeTotal = he.OverFee1 + he.OverFee2 + he.OverFee3 + he.OverFee4 + he.OverFee5 + he.OverFee6 + he.OverFee7 + he.OverFee8 + he.OverFee9 + he.OverFee10 + he.OverFee11 + he.OverFee12;

                    #endregion

                    heList.Add(he);
                }

                AnnualSalaryRO ro = new AnnualSalaryRO(this.heList, this.hryear.ToString());
                ro.ShowPreviewDialog();
            }
        }

        private MonthSalaryClass GetEmpSalary(Model.Employee emp)
        {
            int mTemp = 0;
            int mHicount = 0;
            int mTimeBonus = 0;     //统计满足<时数补贴>的次数
            double mLateHalfCount = 0;
            StringBuilder strBU = new StringBuilder();
            int totalDay = DateTime.DaysInMonth(hryear, hrmonth);
            MonthSalaryClass _ms;
            //////////////////////////////////////////////////////////////////
            _ms = new MonthSalaryClass();
            _ms.mIdentifyDate = new DateTime(hryear, hrmonth, 1);
            _ms.mEmployeeId = emp.EmployeeId;
            _ms.mEmployeeName = emp.EmployeeName;
            _ms.mIDNo = emp.IDNo;
            _ms.mDepartmetName = emp.Department == null ? "" : emp.Department.DepartmentName;

            #region  取考勤记录
            //全勤天数
            int attendDays = 0;
            //加班天数
            int overDays = 0;
            //无薪假天数
            double noPayleaveDays = 0;
            //实际上班时间
            double onWorkHours = 0;
            //全勤，公假，年假，周末，週六休假，隔周休天数
            int hasPayDays = 0;
            //请假的天数
            int halfDays = 0;
            //请半天的日基数总和
            double halfDayFactors = 0;
            //请半天的班别津贴
            double halfSpecialBonus = 0;
            //外劳请假天数
            double MigrantWorkerLeaveDays = 0;
            //出勤半天的天数
            double halfattend = 0;
            //周末天数
            int WeekendDays = 0;
            //时数补贴事假,病假,曠職,婚假,特殊休(有薪) 喪假扣减
            double TimeBonus = 0;
            //旷职扣减
            double Kuangzhi = 0;
            //婚假，丧假，产假计算到底薪的出勤日中
            double hunSangChan = 0;
            //隔周休假
            double gezhouxiu = 0;
            //公假 ，年假，出差 天数
            double gnDays = 0;
            //周六天数
            int saturdays = 0;
            foreach (Model.HrDailyEmployeeAttendInfo attend in this._hrManager.SelectByEmpMonth(emp, hryear, hrmonth))
            {
                if (attend.LateInMinute.HasValue && attend.LateInMinute.Value != 0)
                {
                    strBU.Append(attend.LateInMinute.ToString() + "|");
                    mTemp = mTemp + attend.LateInMinute.Value;
                    //if (mTemp <= 10)
                    //{
                    //    mCount = mCount + 1;
                    //}
                    if (attend.LateInMinute.Value > 30)
                    {
                        mHicount = mHicount + 1;
                        if ((attend.LateInMinute.Value + 30) % 60 > 30)
                        {
                            mLateHalfCount = mLateHalfCount + (attend.LateInMinute.Value + 30) / 60 + 0.5;
                        }
                        else
                        {
                            mLateHalfCount = mLateHalfCount + (attend.LateInMinute.Value + 30) / 60;
                        }
                    }
                    //在职务津贴扣除符合条件假期
                }
                _ms.mNote = attend.Note;
                if (!string.IsNullOrEmpty(_ms.mNote))
                {
                    if (_ms.mNote != "週日休假" && _ms.mNote != "周日休假" && _ms.mNote.IndexOf("公假") < 0 && _ms.mNote.IndexOf("婚假") < 0 && _ms.mNote.IndexOf("喪假") < 0 && _ms.mNote.IndexOf("年假") < 0 && _ms.mNote.IndexOf("出差") < 0 && _ms.mNote.IndexOf("遲到") < 0)
                        _ms.mCount = _ms.mCount + 1;
                    else if (_ms.mNote.Contains("事假") || _ms.mNote.Contains("病假"))
                        _ms.mCount = _ms.mCount + 1;
                    //if (_ms.mNote.Contains("無薪假") && !(bool)emp.IsCadre && !emp.IsMigrantWorker)
                    if (_ms.mNote.Contains("無薪假") && !Convert.ToBoolean(emp.IsCadre))
                    {
                        if (_ms.mNote.Contains("整日"))
                            noPayleaveDays++;
                        else
                            noPayleaveDays += 0.5;
                    }
                    //if (_ms.mNote.Contains("公假") || _ms.mNote == "週日休假" || _ms.mNote == "周日休假" || _ms.mNote.Contains("年假") || _ms.mNote.Contains("出差") || (_ms.mNote.Contains("週六休假") && (emp.IDNo.ToUpper().StartsWith("J") || emp.IDNo.ToUpper().StartsWith("O"))))
                    if (_ms.mNote.Contains("公假") || _ms.mNote == "週日休假" || _ms.mNote == "周日休假" || _ms.mNote.Contains("年假") || _ms.mNote.Contains("出差") || (_ms.mNote.Contains("週六休假")))
                    {
                        hasPayDays++;
                        if (_ms.mNote == "週日休假" || _ms.mNote == "周日休假")
                            WeekendDays++;

                        if (_ms.mNote.Contains("週六休假"))
                            saturdays++;

                        if (_ms.mNote.Contains("公假") || _ms.mNote.Contains("年假") || _ms.mNote.Contains("出差"))
                        {
                            if (_ms.mNote.Contains("整日"))
                                gnDays++;
                            //else
                            //{
                            //    if (_ms.mNote.Contains("公假") && _ms.mNote.Contains("年假"))
                            //        gnDays++;
                            //    else
                            //        gnDays += 0.5;
                            //}
                        }
                    }
                    //if (_ms.mNote.Contains("隔周休假") && emp.GeZhouChuQinJJ)
                    //    hasPayDays++;
                    if (_ms.mNote.Contains("隔周休假"))
                    {
                        gezhouxiu++;
                        if (emp.GeZhouChuQinJJ)
                            hasPayDays++;
                    }
                    if (_ms.mNote.Contains("上半日") || _ms.mNote.Contains("下半日") || _ms.mNote.Contains("整日"))
                    {
                        if (!_ms.mNote.Contains("曠職"))
                        {
                            if (!_ms.mNote.Contains("無薪假"))
                            {
                                halfDays++;
                                halfDayFactors += Convert.ToDouble(attend.DayFactor);
                            }
                            if (_ms.mNote.Contains("整日"))
                                MigrantWorkerLeaveDays++;
                            else
                                MigrantWorkerLeaveDays += 0.5;
                        }
                        if (((_ms.mNote.Contains("上半日") && !_ms.mNote.Contains("下半日")) || (!_ms.mNote.Contains("上半日") && _ms.mNote.Contains("下半日"))) && !_ms.mNote.Contains("卻刷卡資料"))
                        {
                            onWorkHours += 4;
                            halfattend += 0.5;
                        }
                    }
                    if (_ms.mNote.Contains("上半日") || _ms.mNote.Contains("下半日"))
                        halfSpecialBonus += Convert.ToDouble(attend.SpecialBonus);
                    //if (VPerson.specialEmpOfAttendJJ.Contains(emp.EmployeeId) && this.hrSpecificHolidayManager.ISExistsByName(_ms.mNote))
                    //    hasPayDays++;
                    if ((emp.IDNo.ToUpper().StartsWith("J")) && this.annualHolidayManager.IsNationalHoliday(attend.DutyDate.Value, attend.Note))
                    {
                        hasPayDays++;
                        gnDays++;
                    }
                    if (_ms.mNote.Contains("曠職") || _ms.mNote.Contains("病假") || _ms.mNote.Contains("事假") || _ms.mNote.Contains("婚假") || _ms.mNote.Contains("喪假") || _ms.mNote.Contains("無薪假") || _ms.mNote.Contains("特殊休(有薪)") || _ms.mNote.Contains("公傷假") || _ms.mNote.Contains("週六休假") || _ms.mNote.Contains("產假") || _ms.mNote.Contains("選舉假") || _ms.mNote.Contains("產檢假") || _ms.mNote.Contains("過年大掃除") || _ms.mNote.Contains("陪產假") || _ms.mNote.Contains("國定假日補休") || _ms.mNote.Contains("颱風假") || _ms.mNote.Contains("育嬰假") || _ms.mNote.Contains("隔周休假") || _ms.mNote.Contains("留職停薪") || _ms.mNote.Contains("補休年假"))
                    {
                        //TimeBonus++;
                        if (_ms.mNote.Contains("整日"))
                            TimeBonus += 6;
                        else         //如果上下半日都有请假记录，若记录无公假，年假，出差，则说明都是扣时数补贴的假
                        {
                            if (_ms.mNote.Contains("上半日") && _ms.mNote.Contains("下半日"))
                            {
                                if (!_ms.mNote.Contains("公假") && !_ms.mNote.Contains("年假") && !_ms.mNote.Contains("出差"))
                                    TimeBonus += 6;
                                else
                                    TimeBonus += 3;
                            }
                            else
                                TimeBonus += 3;
                        }


                        if (_ms.mNote.Contains("曠職"))
                        {
                            if (_ms.mNote.Contains("整日"))
                            {
                                Kuangzhi += this.KuangzhiFactor;
                            }
                            else
                            {
                                Kuangzhi += this.KuangzhiFactor / 2;
                            }
                        }
                    }
                    if (_ms.mNote.Contains("婚假") || _ms.mNote.Contains("喪假") || _ms.mNote.Contains("產假"))
                    {
                        if (_ms.mNote.Contains("整日"))
                            hunSangChan++;
                        else
                        {
                            if (_ms.mNote.Contains("婚假") && (_ms.mNote.Contains("喪假") || _ms.mNote.Contains("產假")) || _ms.mNote.Contains("喪假") && (_ms.mNote.Contains("婚假") || _ms.mNote.Contains("產假")) || _ms.mNote.Contains("產假") && (_ms.mNote.Contains("喪假") || _ms.mNote.Contains("婚假")))
                                hunSangChan++;
                            else
                                hunSangChan += 0.5;

                        }
                    }
                }
                //计算没有请假，休假等的出勤天数
                if (attend.ShouldCheckIn != null)
                {
                    if (string.IsNullOrEmpty(_ms.mNote))
                        attendDays++;
                    else if (_ms.mNote == "遲到" || _ms.mNote == "早退" || _ms.mNote == ";遲到" || _ms.mNote == ";早退" || _ms.mNote == "遲到;" || _ms.mNote == "早退;")
                        attendDays++;
                }
                //加班天数
                if (attend.ShouldCheckIn == null && attend.ActualCheckIn != null && attend.ActualCheckOut != null)
                {
                    if (!string.IsNullOrEmpty(_ms.mNote))
                        overDays++;
                }

                if (attend.DutyDate.Value.DayOfWeek != DayOfWeek.Sunday)
                {
                    //if (!this._HrSpecificHolidays.Contains(_ms.mNote) && _ms.mNote.IndexOf("半日") < 0 && _ms.mNote.IndexOf("假") < 0 && _ms.mNote.IndexOf("曠職") < 0)
                    //    mTimeBonus = mTimeBonus + 1;
                }
            }
            mTimeBonus = attendDays;
            //全勤天数+公假天数+周末，计算出勤奖
            hasPayDays += attendDays;
            //判断是否给发时数补贴
            onWorkHours += attendDays * 7.5;
            #endregion

            #region 取薪资计算
            DataSet dsms = this.monthlySalaryManager.getMonthlySummaryFee(emp.EmployeeId, _ms.mIdentifyDate, hryear, hrmonth);
            if (dsms.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsms.Tables[0].Rows[0];
                _ms.mLunchFee = mStrToDouble(dr["LunchFee"]);                                 //午餐費m
                _ms.mOverTimeFee = mStrToDouble(dr["OverTimeFee"]);                           //加班費
                _ms.mGeneralOverTime = mStrToDouble(dr["GeneralOverTime"]);                   //平日加班(時數)
                _ms.mHolidayOverTime = mStrToDouble(dr["HolidayOverTime"]);                   //假日加班(時數)
                _ms.GeneralOverTimeCountBig = mStrToDouble(dr["GeneralOverTimeCountBig"]);    //平日加班2小时之外(時數)
                _ms.GeneralOverTimeCountSmall = mStrToDouble(dr["GeneralOverTimeCountSmall"]);//平日加班2小时以下(時數)
                _ms.HolidayOverTimeCountBig = mStrToDouble(dr["HolidayOverTimeCountBig"]);    //假日加班2小时之外(時數)
                _ms.HolidayOverTimeCountSmall = mStrToDouble(dr["HolidayOverTimeCountSmall"]);//假日加班2小时以下(時數)
                _ms.mLateCount = mStrToDouble(dr["LateCount"]);                               //遲到次數
                _ms.mTotalLateInMinute = mStrToDouble(dr["TotalLateInMinute"]);               //總遲到（分）
                _ms.mOverTimeBonus = mStrToDouble(dr["OverTimeBonus"]);                       //加班津貼
                _ms.mSpecialBonus = mStrToDouble(dr["SpecialBonus"]);                         //中夜班津貼
                //_ms.mDaysFactor = mStrToDouble(dr["DaysFactor"]);                           //總日基數
                _ms.mMonthFactor = mStrToDouble(dr["MonthFactor"]);                           //總月基數
                _ms.mDutyDateCount = mStrToDouble(dr["DutyDateCount"]);                       //總出勤記錄數
                _ms.mLeaveDate = (dr["LeaveDate"] == null || dr["LeaveDate"].ToString() == "") ? global::Helper.DateTimeParse.NullDate : Convert.ToDateTime(dr["LeaveDate"].ToString());                                               //离职日期
                _ms.mPunishLeaveCount = mStrToDouble(dr["PunishLeaveCount"]);                 //倒扣款假總數
                _ms.mLeaveCount = mStrToDouble(dr["LeaveCount"]);                             //請假總數
                _ms.mAbsentCount = mStrToDouble(dr["AbsentCount"]);                           //曠工總數
                _ms.mTotalHoliday = mStrToDouble(dr["TotalHoliday"]);                         //該月總例假數
                _ms.mLoanFee = mStrToDouble(dr["LoanFee"]);                                   //借支
                // int WuXinCount = Int32.Parse(dr["WuXinCount"].ToString());
                //考勤 不满一月  日基数 = 日基数-实际假数-扣款请假天数-旷职-无薪假      //矿工待处理  
                //if (_ms.mDutyDateCount < totalDay)
                //    _ms.mDaysFactor = _ms.mDaysFactor - _ms.mTotalHoliday;
                //    if (_ms.mDutyDateCount < totalDay && _ms.mLeaveDate != global::Helper.DateTimeParse.NullDate && _ms.mLeaveDate.ToString() != "")             //總出勤記錄數
                //        _ms.mDaysFactor = _ms.mMonthFactor - _ms.mTotalHoliday - _ms.mPunishLeaveCount - _ms.mAbsentCount - WuXinCount;
                //    else if ((_ms.mLeaveDate == global::Helper.DateTimeParse.NullDate || _ms.mLeaveDate.ToString() == "") && _ms.mDutyDateCount < totalDay)
                //        _ms.mDaysFactor = _ms.mMonthFactor - _ms.mTotalHoliday - _ms.mPunishLeaveCount - _ms.mAbsentCount - WuXinCount;
            }
            else
            {
                _ms.mLoanFee = 0;
                _ms.mLunchFee = 0;
                _ms.mOverTimeFee = 0;
                _ms.mGeneralOverTime = 0;
                _ms.mHolidayOverTime = 0;
                _ms.mLateCount = 0;
                _ms.mTotalLateInMinute = 0;
                _ms.mOverTimeBonus = 0;
                _ms.mSpecialBonus = 0;
                //_ms.mDaysFactor = 0;
                _ms.mMonthFactor = 0;
                _ms.mDutyDateCount = 0;
                _ms.mPunishLeaveCount = 0;
                _ms.mLeaveCount = 0;
                _ms.mTotalHoliday = 0;
            }
            dsms.Clear();
            #endregion

            #region 底薪
            DataSet dx_ds = this.monthlySalaryManager.getMonthlySalary(emp.EmployeeId, _ms.mIdentifyDate);//只有一行记录,故取第一行即可.
            if (dx_ds.Tables[0].Rows.Count > 0)
            {
                DataRow dx_dr = dx_ds.Tables[0].Rows[0];
                //_ms.mDailyPay = mStrToDouble(dx_dr["DailyPay"]); //日工资
                _ms.mMonthlyPay = mStrToDouble(dx_dr["MonthlyPay"]); //月工资
                if (VPerson.specialEmp.Contains(emp.EmployeeId))
                {
                    _ms.mDutyPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["DutyPay"]), 0);
                }
                else if (emp.EmployeeJoinDate <= Convert.ToDateTime(hryear.ToString() + "-" + hrmonth.ToString() + '-' + 01.ToString()) && (_ms.mLeaveDate > Convert.ToDateTime(hryear.ToString() + "-" + hrmonth.ToString() + '-' + totalDay.ToString()) || _ms.mLeaveDate == global::Helper.DateTimeParse.NullDate))
                {
                    _ms.mDutyPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["DutyPay"]) / (30 - WeekendDays - saturdays) * (30 - totalDay + attendDays + gnDays), 0);
                } //责任津贴   新版改为出勤奖金 后改为 伙食津贴  现改为  津贴. 改 年终

                _ms.mGivenDays = mStrToDouble(dx_dr["HolidayBonusGivenDays"]);  //年假(补休)天数
                _ms.mAnnualHolidayFee = this.GetSiSheWuRu(_ms.mMonthlyPay / 30 * _ms.mGivenDays, 0);         //年假(补休)金额，2016年3月2日16:06:28改为固定除以30天
                _ms.mOtherPunish = mStrToDouble(dx_dr["OtherPunish"]); //其它扣款
                _ms.mJiuYuanKouJiao = mStrToDouble(dx_dr["JiuYuanKouJiao"]); //就源扣缴

                if (VPerson.specialEmp.Contains(emp.EmployeeId))
                {
                    _ms.mBasePay = _ms.mMonthlyPay;
                }
                else if (emp.EmployeeJoinDate > Convert.ToDateTime(hryear.ToString() + "-" + hrmonth.ToString() + '-' + 01.ToString()) || (_ms.mLeaveDate != global::Helper.DateTimeParse.NullDate && _ms.mLeaveDate < Convert.ToDateTime(hryear.ToString() + "-" + hrmonth.ToString() + '-' + 01.ToString()).AddMonths(1)))
                {
                    _ms.mBasePay = this.GetSiSheWuRu(_ms.mMonthlyPay / 30 * (attendDays + halfattend - Kuangzhi), 0);
                }
                else
                {
                    if (emp.AttendanceDays.HasValue && emp.AttendanceDays.Value > Convert.ToDouble(attendDays) + halfattend + hunSangChan)
                        _ms.mBasePay = this.GetSiSheWuRu(_ms.mMonthlyPay - _ms.mMonthlyPay / 30 * (Kuangzhi + totalDay - _ms.mMonthFactor + noPayleaveDays + halfDays - halfDayFactors + WeekendDays), 0);
                    else
                        _ms.mBasePay = this.GetSiSheWuRu(_ms.mMonthlyPay - _ms.mMonthlyPay / 30 * (Kuangzhi + totalDay - _ms.mMonthFactor + noPayleaveDays + halfDays - halfDayFactors), 0);
                }

            }
            #endregion

            #region 迟到扣款
            int mIcount = 0;  //和小于10分 次数小于3次
            if ((_ms.mTotalLateInMinute > 10 || _ms.mLateCount > 2) && !VPerson.vipPerson.Contains(emp.EmployeeId) && !VPerson.specialEmp.Contains(emp.EmployeeId))
            {
                string[] strs = new string[31];
                IList<int> a = new List<int>();

                if (strBU.Length > 0)
                    strs = strBU.ToString(0, strBU.Length - 1).Split('|');
                for (int i = 0; i < strs.Length; i++)
                {
                    if (strs[i] != null)
                    {
                        if (strs[i] == "0")
                            continue;
                        else
                            a.Add(Int32.Parse(strs[i]));
                    }
                }

                int temp = 0;
                for (int i = 0; i < a.Count; i++)
                {
                    for (int j = i + 1; j < a.Count; j++)
                    {
                        if (a[i] > a[j])
                        {
                            temp = a[i];
                            a[i] = a[j];
                            a[j] = temp;
                        }

                    }
                }
                int sum = 0;
                int m;
                for (m = 0; m < a.Count; m++)
                {
                    sum = sum + a[m];
                    if (sum > 10)
                        break;
                }
                if (m > 2)
                {
                    m = 2;
                }
                mIcount = m;
                _ms.mTotalLateInHour = (_ms.mLateCount - mIcount - mHicount) * 0.5 + mLateHalfCount;
                _ms.mLatePunish = global::Helper.DateTimeParse.GetSiSheWuRu(_ms.mTotalLateInHour * _ms.mMonthlyPay / 30 / 8, 0);
            }
            else
            {
                _ms.mTotalLateInHour = 0;
                _ms.mLatePunish = 0;
                mIcount = (int)_ms.mLateCount;  // 10分钟内 3次之内 用于全勤奖
            }
            #endregion

            #region 假日&平日加班算法修改 2013年4月26日15:09:41 陈宁
            if (!VPerson.specialEmp.Contains(emp.EmployeeId))
            {
                //平日加班 小于2小时 为 时薪*1.33*加班小时,超出2小时部分 为 时薪*1.66*加班小时
                _ms.mGeneralOverTimeFee = GetSiSheWuRu((_ms.mMonthlyPay / 30 / 8) * (_ms.GeneralOverTimeCountSmall * 1.334 + _ms.GeneralOverTimeCountBig * 1.667), 0);
                //假日加班 一律 为时薪 两倍.
                _ms.mHolidayOverTimeFee = GetSiSheWuRu(((_ms.mMonthlyPay / 30 / 8) * 3 / 2) * _ms.mHolidayOverTime, 0);
            }

            #endregion

            return _ms;
        }

        private double mStrToDouble(object o)
        {
            return double.Parse(string.IsNullOrEmpty(o.ToString()) ? "0" : o.ToString());
        }

        private double GetSiSheWuRu(double objTarget, int mIndex)
        {
            double a1 = Math.Pow(10, mIndex);
            double a2 = Math.Pow(10, mIndex + 1);
            double b1 = Math.Truncate(objTarget * a1);
            double b2 = Math.Truncate(objTarget * a2);
            if (b2 % 10 >= 5 || b2 % 10 <= -5)
            {
                return objTarget > 0 ? (b1 + 1) / a1 : (b1 - 1) / a1;
            }
            else
            {
                return b1 / a1;
            }
        }
    }

    public class HelperEmp
    {
        public Model.Employee Employee { get; set; }

        public MonthSalaryClass MonthSalaryClass1 { get; set; }

        public MonthSalaryClass MonthSalaryClass2 { get; set; }

        public MonthSalaryClass MonthSalaryClass3 { get; set; }

        public MonthSalaryClass MonthSalaryClass4 { get; set; }

        public MonthSalaryClass MonthSalaryClass5 { get; set; }

        public MonthSalaryClass MonthSalaryClass6 { get; set; }

        public MonthSalaryClass MonthSalaryClass7 { get; set; }

        public MonthSalaryClass MonthSalaryClass8 { get; set; }

        public MonthSalaryClass MonthSalaryClass9 { get; set; }

        public MonthSalaryClass MonthSalaryClass10 { get; set; }

        public MonthSalaryClass MonthSalaryClass11 { get; set; }

        public MonthSalaryClass MonthSalaryClass12 { get; set; }

        public double MonthSalary1 { get; set; }

        public double MonthSalary2 { get; set; }

        public double MonthSalary3 { get; set; }

        public double MonthSalary4 { get; set; }

        public double MonthSalary5 { get; set; }

        public double MonthSalary6 { get; set; }

        public double MonthSalary7 { get; set; }

        public double MonthSalary8 { get; set; }

        public double MonthSalary9 { get; set; }

        public double MonthSalary10 { get; set; }

        public double MonthSalary11 { get; set; }

        public double MonthSalary12 { get; set; }

        public double MonthSalaryTotal { get; set; }

        public double KouKuan1 { get; set; }

        public double KouKuan2 { get; set; }

        public double KouKuan3 { get; set; }

        public double KouKuan4 { get; set; }

        public double KouKuan5 { get; set; }

        public double KouKuan6 { get; set; }

        public double KouKuan7 { get; set; }

        public double KouKuan8 { get; set; }

        public double KouKuan9 { get; set; }

        public double KouKuan10 { get; set; }

        public double KouKuan11 { get; set; }

        public double KouKuan12 { get; set; }

        public double KouKuanTotal { get; set; }

        public double LunchFeeTotal { get; set; }

        public double OverFee1 { get; set; }

        public double OverFee2 { get; set; }

        public double OverFee3 { get; set; }

        public double OverFee4 { get; set; }

        public double OverFee5 { get; set; }

        public double OverFee6 { get; set; }

        public double OverFee7 { get; set; }

        public double OverFee8 { get; set; }

        public double OverFee9 { get; set; }

        public double OverFee10 { get; set; }

        public double OverFee11 { get; set; }

        public double OverFee12 { get; set; }

        public double OverFeeTotal { get; set; }
    }
}