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
        double AttendDays = 0;
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
                    he.MSList.Add(this.GetEmpSalary(emp));
                }

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
                        he.MSList.Add(this.GetEmpSalary(emp));
                    }

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
        public HelperEmp()
        {
            if (this.MSList == null)
                this.MSList = new List<MonthSalaryClass>();
        }

        public Model.Employee Employee { get; set; }

        public List<MonthSalaryClass> MSList { get; set; }
    }
}