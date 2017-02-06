using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using System.Threading;
using System.Linq;

namespace Book.UI.Hr.Salary.Salaryset
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军             完成时间:2009-10-26
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class CalculationForm : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 员工管理实例
        /// </summary>
        protected BL.EmployeeManager employeeManager = new Book.BL.EmployeeManager();
        /// <summary>
        /// 部门管理实例
        /// </summary>
        // protected BL.DepartmentManager departmentManager = new Book.BL.DepartmentManager();
        /// <summary>
        /// 当前待处理员工
        /// </summary>


        /// <summary>
        /// 請假管理
        /// </summary>
        BL.OverTimeManager overtimeman = new Book.BL.OverTimeManager();
        ///// <summary>
        ///// 出勤记录
        ///// </summary>
        //private BL.AttenManager attenmanage = new Book.BL.AttenManager();
        /// <summary>
        /// 请假管理
        /// </summary>
        private BL.LeaveManager leavemanage = new Book.BL.LeaveManager();

        private BL.OverTimeManager overtimemanage = new Book.BL.OverTimeManager();
        /// <summary>
        /// 午餐补助管理
        /// </summary>
        private BL.LunchDetailManager lunchdetailmanage = new Book.BL.LunchDetailManager();
        /// <summary>
        /// 借支管理
        /// </summary>
        private BL.LoanDetailManager loandetailManage = new Book.BL.LoanDetailManager();
        BL.MonthlySalaryManager monthlySalaryManager = new Book.BL.MonthlySalaryManager();
        private BL.HrDailyEmployeeAttendInfoManager _hrManager = new Book.BL.HrDailyEmployeeAttendInfoManager();
        // Model.HrAttendStat _hrAttendStat;
        private MonthSalaryClass _ms;
        private Model.MonthlySalary _monthlySalary = new Book.Model.MonthlySalary();
        private BL.HrAttendStatManager hrAttendManager = new Book.BL.HrAttendStatManager();
        private IList<Model.Employee> _emplist = new List<Model.Employee>();
        private BL.AnnualHolidayManager annualHolidayManager = new Book.BL.AnnualHolidayManager();
        private int hryear = 0;
        private int hrmonth = 0;
        public int totalDay;
        int turnOutSalary = 0;
        double AttendDays = 0;
        double KuangzhiFactor = 0;

        /// <summary>
        /// 实际工作天数
        /// </summary>
        //int workcount = 30;
        /// <summary>
        /// 底薪
        /// </summary>
        // decimal monthsum = 0M;
        /// <summary>
        /// 工作奖金
        /// </summary>
        // decimal WorkBonus = 0M;
        /// <summary>
        ///班别津贴
        /// </summary>
        // decimal bunbonus = 0M;
        /// <summary>
        /// 全勤奖
        /// </summary>
        //  decimal Perfectbonus = 0M;
        /// <summary>
        /// 绩效奖金
        /// </summary>
        //decimal jxbonus = 0M;
        /// <summary>
        /// 作业绩效奖金
        /// </summary>
        // decimal zyjsbonus = 0M;
        /// <summary>
        /// 扣薪天数
        /// </summary>
        //int Punishpay = 0;
        /// <summary>
        /// 底薪总计
        /// </summary>
        //decimal dxsum = 0M;
        /// <summary>
        /// 实领工资
        /// </summary>
        //decimal gongzslsum = 0M;
        /// <summary>
        /// 员工
        /// </summary>
        private Book.Model.Employee employee = null;
        /// <summary>
        /// 总日基数
        /// </summary>
        //decimal dayfactorSum = 0;
        ///// <summary>
        ///// 总月基数
        ///// </summary>
        //decimal monthFactorSum = 0;
        ///// <summary>
        ///// 总出勤数
        ///// </summary>
        //int DutyDateCount = 0;
        ///// <summary>
        ///// 加班津贴
        ///// </summary>
        //decimal OverTimeBonus = decimal.Zero;
        ///// <summary>
        ///// 平日加班酬薪
        ///// </summary>
        //decimal NormalFee = decimal.Zero;
        ///// <summary>
        ///// 假日加班酬薪
        ///// </summary>
        //decimal HolidayFee = decimal.Zero;
        /// <summary>
        /// 迟到扣款
        /// </summary>
        // decimal LatePunish = decimal.Zero;

        /// <summary>
        /// 全部国定休假
        /// </summary>
        private IList<string> _HrSpecificHolidays = new List<string>();

        public CalculationForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体构造函数
        /// </summary>
        /// <param name="employees"></param>
        public CalculationForm(Model.Employee employees)
        {
            this.employee = employees;
            Calulation(employees);
        }

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculationForm_Load(object sender, EventArgs e)
        {
            //this.SetBounds((Screen.GetBounds(this).Width / 2) - (this.Width / 2), (Screen.GetBounds(this).Height / 2) - (this.Height / 2), this.Width, this.Height, BoundsSpecified.Location);
            //this.StartPosition = FormStartPosition.CenterScreen;

            DateTime date = this.monthlySalaryManager.get_MaxIdentifyDateMonth();

            if (date.Year != 1)
            {
                DateTime strdate = date.AddMonths(1);

                for (int i = 1; i < 25; i++)
                {
                    this.comboBoxEdit1.Properties.Items.Add(strdate.AddMonths(-1).ToString("yyyy年MM月"));
                    strdate = strdate.AddMonths(-1);
                }
                this.comboBoxEdit1.SelectedIndex = 0;
                this.hryear = Int32.Parse(this.comboBoxEdit1.Text.Substring(0, 4));
                this.hrmonth = Int32.Parse(this.comboBoxEdit1.Text.Substring(5, 2));
                _emplist = this.employeeManager.SelectHrDailyAttend(new DateTime(hryear, hrmonth, 1));
                this.bindingSourceEmployee.DataSource = _emplist;
            }

            this._HrSpecificHolidays = (from item in new BL.HrSpecificHolidayManager().Select()
                                        select item.Name).ToList<string>();

            KuangzhiFactor = new BL.LeaveTypeManager().SelectPayRateByName("曠職");
        }

        /// <summary>
        /// 薪资
        /// </summary>
        private int flag = 0;

        //薪资计算于2013.10修改
        private void Calulation(Model.Employee emp)
        {
            //2015年1月26日10:53:19   出勤奖金 改为 伙食津贴
            this.lbl_turnOutSalary.Text = "";
            int mTemp = 0;
            int mHicount = 0;
            int mTimeBonus = 0;     //统计满足<时数补贴>的次数
            double mLateHalfCount = 0;
            StringBuilder strBU = new StringBuilder();
            totalDay = DateTime.DaysInMonth(hryear, hrmonth);
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
            //J,O周六天数
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
                    if (_ms.mNote.Contains("公假") || _ms.mNote == "週日休假" || _ms.mNote == "周日休假" || _ms.mNote.Contains("年假") || _ms.mNote.Contains("出差") || (_ms.mNote.Contains("週六休假") && (emp.IDNo.ToUpper().StartsWith("J") || emp.IDNo.ToUpper().StartsWith("O"))))
                    {
                        hasPayDays++;
                        if (_ms.mNote == "週日休假" || _ms.mNote == "周日休假")
                            WeekendDays++;

                        if (_ms.mNote.Contains("週六休假") && (emp.IDNo.ToUpper().StartsWith("J") || emp.IDNo.ToUpper().StartsWith("O")))
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
                    if (emp.IDNo.Contains("J") && this.annualHolidayManager.IsNationalHoliday(attend.DutyDate.Value, attend.Note))
                        hasPayDays++;
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
                //else
                //{
                //    //2013年5月20日16:26:17
                //    //没有请假.今日也不是例假则则算作<时数补贴>一次
                //    mTimeBonus++;
                //}

                //2013年5月28日11:25:28 CN
                //隔周休纳入方式: 
                //1.<隔周休>
                //   如果实施隔周休政策,将没有所谓的<时数补贴>.
                //   隔周休的员工,在双休的时候周六也有工资, 但是没有时数补贴。 
                //   若是单休周,周六却没有来。 则要算作请假之类来做扣款动作
                //   
                //2.<不隔周休>
                //   不隔周休的员工(每周都是单休),周一到周六每天给 <时薪*1.33*0.5*[符合次数]> 的时数补贴.
                //   周一~周六 哪一天请假(无论什么假,请假半天以上),则取消当天时数补贴.迟到不扣时数补贴. 只要请假就不发
                //   若是周六请假.则只针对周六做扣款之类的操作.对周内的时数补贴不做影响.

                //attend.DutyDate.Value.DayOfWeek != DayOfWeek.Saturday &&

                //2013.7.2  符合次数改为全勤天数
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
                //if (emp.IsMigrantWorker)
                //    _ms.mLunchFee = 0;
                //else
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
                if (VPerson.vipPerson.Contains(emp.EmployeeId) || VPerson.specialEmp.Contains(emp.EmployeeId))
                {
                    _ms.mPostPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["PostPay"]), 0);  //職務津貼
                }
                else if (emp.EmployeeJoinDate < Convert.ToDateTime(hryear.ToString() + "-" + hrmonth.ToString() + '-' + 01.ToString()) && (_ms.mLeaveDate > Convert.ToDateTime(hryear.ToString() + "-" + hrmonth.ToString() + '-' + totalDay.ToString()) || _ms.mLeaveDate == global::Helper.DateTimeParse.NullDate))
                {
                    //_ms.mDutyPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["DutyPay"]) * _ms.mMonthFactor / totalDay, 0);  //责任津贴
                    _ms.mPostPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["PostPay"]) * _ms.mMonthFactor / totalDay, 0);  //職務津貼
                }
                if (VPerson.specialEmp.Contains(emp.EmployeeId))
                {
                    _ms.mDutyPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["DutyPay"]), 0);
                }
                else if (emp.EmployeeJoinDate <= Convert.ToDateTime(hryear.ToString() + "-" + hrmonth.ToString() + '-' + 01.ToString()) && (_ms.mLeaveDate > Convert.ToDateTime(hryear.ToString() + "-" + hrmonth.ToString() + '-' + totalDay.ToString()) || _ms.mLeaveDate == global::Helper.DateTimeParse.NullDate))
                {
                    //if (emp.IDNo.Contains("J"))
                    //{
                    //    _ms.mDutyPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["DutyPay"]) - mStrToDouble(dx_dr["DutyPay"]) / 30 * (totalDay - hasPayDays), 0);
                    //}
                    ////员工编号以J开头的不管满不满足设定的出勤奖金天数 只扣除了年假，公假，国定假日之外的请假

                    //else
                    //{
                    //    if (emp.AttendanceJJDays.HasValue && attendDays + halfattend < emp.AttendanceJJDays)
                    //        _ms.mDutyPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["DutyPay"]) - mStrToDouble(dx_dr["DutyPay"]) / 30 * (totalDay - hasPayDays + WeekendDays), 0);
                    //    else
                    //        _ms.mDutyPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["DutyPay"]) - mStrToDouble(dx_dr["DutyPay"]) / 30 * (totalDay - hasPayDays), 0);
                    //}

                    //2017年1月24日 設定的年終值/（當月天數-當月星期天數）* 該員實際出勤天數(公假 年假應算出勤)=年終，半天不算
                    //以O, J 开头的员工 設定的年終值/（當月天數-當月星期6，日天數）* 該員實際出勤天數(公假 年假應算出勤)=年終
                    if (emp.IDNo.ToUpper().StartsWith("J") || emp.IDNo.ToUpper().StartsWith("O"))
                        _ms.mDutyPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["DutyPay"]) / (totalDay - WeekendDays - saturdays) * (attendDays + gnDays), 0);
                    else
                        _ms.mDutyPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["DutyPay"]) / (totalDay - WeekendDays) * (attendDays + gnDays), 0);
                } //责任津贴   新版改为出勤奖金 后改为 伙食津贴  现改为  津贴. 改 年终

                this.turnOutSalary = Convert.ToInt32(mStrToDouble(dx_dr["DutyPay"]));
                _ms.mGivenDays = mStrToDouble(dx_dr["HolidayBonusGivenDays"]);  //年假(补休)天数
                _ms.mAnnualHolidayFee = this.GetSiSheWuRu(_ms.mMonthlyPay / 30 * _ms.mGivenDays, 0);         //年假(补休)金额，2016年3月2日16:06:28改为固定除以30天
                _ms.mInsurance = mStrToDouble(dx_dr["insurance"]); //保险费
                //_ms.mTax = mStrToDouble(dx_dr["Tax"]);   //所得税     新版去掉
                _ms.mEffectFactor = mStrToDouble(dx_dr["EffectFactor"]); //績效係數
                _ms.mOtherPay = mStrToDouble(dx_dr["OtherPay"]);  //其它補款
                _ms.mOtherPunish = mStrToDouble(dx_dr["OtherPunish"]); //其它扣款
                _ms.mJiuYuanKouJiao = mStrToDouble(dx_dr["JiuYuanKouJiao"]); //就源扣缴
                //职场津贴算法：来公司未足月者按照实际上班天数计算而且加班也计入，足月按每月天数减去请假次数计算    新版去掉“餐费津贴”，也就是所谓的时数补贴
                //if (emp.EmployeeJoinDate > Convert.ToDateTime(hryear.ToString() + "-" + hrmonth.ToString() + '-' + 01.ToString()) || emp.EmployeeLeaveDate < Convert.ToDateTime(hryear.ToString() + "-" + hrmonth.ToString() + '-' + totalDay.ToString()))
                //    _ms.mFieldPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["FieldPay"]) * (attendDays + overDays) / totalDay, 0);//职场津贴
                //else
                //    _ms.mFieldPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["FieldPay"]) * (totalDay - _ms.mCount) / totalDay, 0);  //职场津贴
                //_ms.mBasePay = this.GetSiSheWuRu(_ms.mMonthlyPay * (_ms.mMonthFactor - noPayleaveDays - halfDays + halfDayFactors) / totalDay, 0);   
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
                    if (emp.IsMonthSalary)
                    {
                        //if (emp.IsMigrantWorker)
                        //{
                        //    if (emp.AttendanceDays.HasValue && emp.AttendanceDays.Value > Convert.ToDouble(attendDays) + halfattend + hunSangChan)
                        //        _ms.mBasePay = this.GetSiSheWuRu(_ms.mMonthlyPay - _ms.mMonthlyPay / 30 * (Kuangzhi + MigrantWorkerLeaveDays + totalDay - _ms.mDutyDateCount + WeekendDays), 0);
                        //    else
                        //        _ms.mBasePay = this.GetSiSheWuRu(_ms.mMonthlyPay - _ms.mMonthlyPay / 30 * (Kuangzhi + MigrantWorkerLeaveDays + totalDay - _ms.mDutyDateCount), 0);
                        //}
                        //else
                        //{
                        if (emp.AttendanceDays.HasValue && emp.AttendanceDays.Value > Convert.ToDouble(attendDays) + halfattend + hunSangChan)
                            _ms.mBasePay = this.GetSiSheWuRu(_ms.mMonthlyPay - _ms.mMonthlyPay / 30 * (Kuangzhi + totalDay - _ms.mMonthFactor + noPayleaveDays + halfDays - halfDayFactors + WeekendDays), 0);
                        else
                            _ms.mBasePay = this.GetSiSheWuRu(_ms.mMonthlyPay - _ms.mMonthlyPay / 30 * (Kuangzhi + totalDay - _ms.mMonthFactor + noPayleaveDays + halfDays - halfDayFactors), 0);
                        //}
                    }
                    else
                    {
                        //if (emp.IsMigrantWorker)
                        //{
                        //    if (emp.AttendanceDays.HasValue && emp.AttendanceDays.Value > Convert.ToDouble(attendDays) + halfattend + hunSangChan)
                        //        _ms.mBasePay = this.GetSiSheWuRu(_ms.mMonthlyPay * (_ms.mDutyDateCount - MigrantWorkerLeaveDays - WeekendDays - Kuangzhi) / 30, 0);
                        //    else
                        //        _ms.mBasePay = this.GetSiSheWuRu(_ms.mMonthlyPay * (_ms.mDutyDateCount - MigrantWorkerLeaveDays - Kuangzhi) / 30, 0);
                        //}
                        //else
                        //{
                        if (emp.AttendanceDays.HasValue && emp.AttendanceDays.Value > Convert.ToDouble(attendDays) + halfattend + hunSangChan)
                            _ms.mBasePay = this.GetSiSheWuRu(_ms.mMonthlyPay * (_ms.mMonthFactor - noPayleaveDays - halfDays + halfDayFactors - WeekendDays - Kuangzhi) / 30, 0);
                        else
                            _ms.mBasePay = this.GetSiSheWuRu(_ms.mMonthlyPay * (_ms.mMonthFactor - noPayleaveDays - halfDays + halfDayFactors - Kuangzhi) / 30, 0);
                        //}//底薪 新版只有月薪，无日薪

                    }
                }
                if (VPerson.specialEmp.Contains(emp.EmployeeId))
                {
                    _ms.TimeBonus = 0;
                    _ms.mSpecialBonus = 0;
                }
                else
                {
                    if (emp.IDNo.Contains("J"))  //员工编号开头为J时数补贴为0
                        _ms.TimeBonus = 0;
                    else if (!emp.IsMonthSalary || emp.IsMigrantWorker)
                    {
                        //if (this.leavemanage.GetMonthIsGeZhouXiu(emp.EmployeeId, _ms.mIdentifyDate) == 0 && onWorkHours >= 84 && !emp.IsMigrantWorker && !emp.IsMonthSalary) //没有隔周休并且实际上班时间大于84小时，并且不是外劳,不是月薪！
                        //    _ms.TimeBonus = this.GetSiSheWuRu(_ms.mMonthlyPay / 30 / 8 * 1 * 0.5 * mTimeBonus - _ms.mMonthlyPay / 30 / 8 * TimeBonus, 0) < 0 ? 0 : this.GetSiSheWuRu(_ms.mMonthlyPay / 30 / 8 * 1 * 0.5 * mTimeBonus - _ms.mMonthlyPay / 30 / 8 * TimeBonus, 0);      //时数补贴 = 时薪*1*0.5*[符合次数] 符合次数内不计算加班的天数，只算正常出勤。
                        //2016年3月19日 新算法
                        //if (Convert.ToDouble(emp.TimeBonusDays) <= Convert.ToDouble(attendDays) + halfattend)
                        //{
                        //    _ms.TimeBonus = this.GetSiSheWuRu(Convert.ToDouble(emp.TimeBonusTimes) * _ms.mMonthlyPay / 30 / 8 * 0.5, 0);
                        //}
                        //else
                        //{
                        //    _ms.TimeBonus = this.GetSiSheWuRu((Convert.ToDouble(emp.TimeBonusDays) - TimeBonus - gezhouxiu * 5) < 0 ? 0 : (Convert.ToDouble(emp.TimeBonusDays) - TimeBonus - gezhouxiu * 5) * _ms.mMonthlyPay / 30 / 8 * 0.5, 0);
                        //}
                        //if ((Convert.ToDouble(attendDays) + halfattend) <= TimeBonus)
                        //    _ms.TimeBonus = 0;
                        //else
                        //    _ms.TimeBonus = this.GetSiSheWuRu((Convert.ToDouble(attendDays) + halfattend - TimeBonus) * _ms.mMonthlyPay / 30 / 8 * 0.833, 0);

                        //2016年4月28日 时数补贴 改为按全勤天数计算  2017年1月24日 时数补贴 去掉
                        //_ms.TimeBonus = this.GetSiSheWuRu((Convert.ToDouble(attendDays)) * _ms.mMonthlyPay / 30 / 8 * 0.833, 0);
                    }

                    //班別津貼--->餐費補貼
                    if (emp.IsMigrantWorker)
                        _ms.mSpecialBonus = 0;
                    else
                        _ms.mSpecialBonus = _ms.mSpecialBonus - halfSpecialBonus;
                }
            }
            else
            {
                //_ms.mDailyPay = 0;
                _ms.mMonthlyPay = 0;
                _ms.mDutyPay = 0;
                _ms.mPostPay = 0;
                _ms.mGivenDays = 0;
                _ms.mAnnualHolidayFee = 0;
                _ms.mInsurance = 0;
                _ms.mTax = 0;
                _ms.mEffectFactor = 0;
                _ms.mOtherPay = 0;
                _ms.mOtherPunish = 0;
                _ms.mFieldPay = 0;
                _ms.mBasePay = 0;
                _ms.TimeBonus = 0;
            }
            dx_ds.Clear();
            #endregion
            #region 迟到扣款
            int mIcount = 0;  //和小于10分 次数小于3次
            if ((_ms.mTotalLateInMinute > 10 || _ms.mLateCount > 2) && !VPerson.vipPerson.Contains(emp.EmployeeId) && !VPerson.specialEmp.Contains(emp.EmployeeId))
            {
                //临时记录迟到

                //StringBuilder strBU = new StringBuilder();
                //foreach (Model.HrDailyEmployeeAttendInfo attend in this._hrManager.SelectByEmpMonth(this.employee, hryear, hrmonth))
                //{
                //    if (attend.LateInMinute.Value != 0)
                //    {
                //        strBU.Append(attend.LateInMinute.ToString() + "|");
                //        mTemp = mTemp + attend.LateInMinute.Value;
                //        if (mTemp <= 10)
                //        {
                //            mCount = mCount + 1;
                //        }
                //        if (attend.LateInMinute.Value > 30)
                //        {
                //            mHicount = mHicount + 1;
                //        }
                //    }
                //}
                // '遲到超過三次，或總遲到超過10分鐘
                //'化成小時，以0.5小時為刻度
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
            #region 全勤奖  新版去掉
            //if (VPerson.vipPerson.Contains(emp.EmployeeId))
            //{
            //    _ms.mAllAttendBonus = _ms.mMonthlyPay / totalDay * 3;
            //}
            //else
            //{
            //    if (emp.IsMigrantWorker)
            //        _ms.mAllAttendBonus = 0;
            //    else if (_ms.mDutyDateCount == totalDay)
            //    {
            //        //有做滿一個月：日薪三天
            //        if (_ms.mMonthFactor == totalDay)
            //        {
            //            if (_ms.mPunishLeaveCount == 0)
            //            {
            //                _ms.mAllAttendBonus = _ms.mMonthlyPay / totalDay * 3;
            //            }
            //            //請倒扣款假小於等於一天：日薪二天
            //            else if (_ms.mPunishLeaveCount <= 1)
            //            {
            //                _ms.mAllAttendBonus = _ms.mMonthlyPay / totalDay * 3;
            //            }
            //            //判斷遲到,扣除全勤獎金
            //            if (_ms.mLateCount - mIcount > 0)
            //            {
            //                _ms.mAllAttendBonus = _ms.mAllAttendBonus - _ms.mMonthlyPay / totalDay * (_ms.mLateCount - mIcount) >= 0 ? _ms.mAllAttendBonus - _ms.mMonthlyPay / totalDay * (_ms.mLateCount - mIcount) : 0;
            //            }
            //        }
            //        //有缺刷卡記錄                 
            //        else
            //        {
            //            _ms.mAllAttendBonus = 0;
            //        }
            //    }
            //    //未滿一個月（因到職或離職）
            //    else
            //    {
            //        _ms.mAllAttendBonus = 0;
            //    }
            //}
            #endregion
            #region 離職未做滿一整個月者，取消：「責任津貼」、「職務津貼」、「職場津貼」、「週日休假」
            if (_ms.mDutyDateCount < totalDay && _ms.mLeaveDate != global::Helper.DateTimeParse.NullDate && !VPerson.vipPerson.Contains(emp.EmployeeId))
            {
                //_ms.mDutyPay = 0;
                //_ms.mPostPay = 0;
                //_ms.mFieldPay = 0;
            }
            #endregion
            #region 平日加班 假日加班 酬薪加班津贴 及税额
            #region //弃用
            //_ms.mGeneralOverTimeFee = GetSiSheWuRu((_ms.mDailyPay / 6) * _ms.mGeneralOverTime, 0);                   //平日加班费
            //_ms.mHolidayOverTimeFee = GetSiSheWuRu((((_ms.mDailyPay / 2) * 3) / 8) * _ms.mHolidayOverTime, 0);       //假日加班费
            #endregion
            #region //启用 假日&平日加班算法修改 2013年4月26日15:09:41 陈宁
            //平日加班 0~2小时之类 时薪*hour*1.33  2>  时薪*hour*1.66
            //if (_ms.mGeneralOverTime < 2)
            //{
            //    _ms.mGeneralOverTimeFee = GetSiSheWuRu((_ms.mDailyPay / 8) * _ms.mGeneralOverTime * 1.33, 0);
            //}
            //else
            //{
            //_ms.mGeneralOverTimeFee = GetSiSheWuRu((_ms.mDailyPay / 8) * 2 * 1.33, 0) + GetSiSheWuRu((_ms.mDailyPay / 8) * (_ms.mGeneralOverTime - 2) * 1.66, 0);
            //}
            if (!VPerson.specialEmp.Contains(emp.EmployeeId))
            {
                //平日加班 小于2小时 为 时薪*1.33*加班小时,超出2小时部分 为 时薪*1.66*加班小时
                _ms.mGeneralOverTimeFee = GetSiSheWuRu((_ms.mMonthlyPay / 30 / 8) * (_ms.GeneralOverTimeCountSmall * 1.334 + _ms.GeneralOverTimeCountBig * 1.667), 0);
                //假日加班 一律 为时薪 两倍.
                _ms.mHolidayOverTimeFee = GetSiSheWuRu(((_ms.mMonthlyPay / 30 / 8) * 3 / 2) * _ms.mHolidayOverTime, 0);
            }
            #endregion
            #endregion
            #region 工作奖金,绩效奖金, 作业技术奖金 => 工作奖金=职务津贴＋责任津贴＋班别津贴+全勤奖    新版去掉
            //int[] months = { 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3 };
            //switch (months[hrmonth - 1])
            //{
            //    case 1:
            //        _ms.mWorkBonus = _ms.mPostPay + _ms.mDutyPay + _ms.mSpecialBonus + _ms.mAllAttendBonus;
            //        //label_gj.Text = _ms.mWorkBonus.ToString();
            //        //label_jxjj.Text = "0";
            //        //label_zyjj.Text = "0";
            //        break;
            //    case 2:
            //        _ms.mEffectBonus = _ms.mPostPay + _ms.mDutyPay + _ms.mSpecialBonus + _ms.mAllAttendBonus;
            //        //label_jxjj.Text = _ms.mEffectBonus.ToString();
            //        //label_gj.Text = "0";
            //        //label_zyjj.Text = "0";
            //        break;
            //    case 3:
            //        _ms.mTechBonus = _ms.mPostPay + _ms.mDutyPay + _ms.mSpecialBonus + _ms.mAllAttendBonus;
            //        //label_zyjj.Text = _ms.mTechBonus.ToString();
            //        //label_jxjj.Text = "0";
            //        //label_gj.Text = "0";
            //        break;
            //}

            #endregion
            ////////////////////////////////////////////////////////////////
            flag = 1;

            //姓名
            this.label_gname.Text = _ms.mEmployeeName;
            //姓名
            this.label_jiname.Text = _ms.mEmployeeName;
            //底薪
            this.label_dx.Text = _ms.mBasePay.ToString();
            //时数补贴
            //this.lbl_timebonus.Text = _ms.TimeBonus.ToString();
            //借支
            this.label_jiez.Text = _ms.mLoanFee.ToString();
            //保险 
            this.label_baox.Text = _ms.mInsurance.ToString();
            //所得税
            //this.label_JiuYuanJiaoKou.Text = _ms.mTax.ToString();
            //职场津贴 => [餐费津贴]
            //this.label_PingRiJiaBan.Text = _ms.mFieldPay.ToString();
            ////作业技术奖金
            //this.label_zyjj.Text = _ms.mTechBonus.ToString();
            //绩效奖金
            //this.label_jxjj.Text = _ms.mEffectBonus.ToString();
            //假日加班费
            this.label_JiaRiJiaBan.Text = _ms.mHolidayOverTimeFee.ToString();
            //平日加班费
            this.label_PingRiJiaBan.Text = _ms.mGeneralOverTimeFee.ToString();
            //加班津贴
            //this.label_jbjt.Text = _ms.mOverTimeBonus.ToString();
            //班别津贴
            this.label_banbiejintie.Text = _ms.mSpecialBonus.ToString();
            //职务津贴
            this.label_zhiwjt.Text = _ms.mPostPay.ToString();
            //年假(补休)天数
            this.txt_givenDays.Text = _ms.mGivenDays.ToString();
            //年假(补休)金额
            this.label_AnnualHolidayFee.Text = _ms.mAnnualHolidayFee.ToString();
            //餐费
            this.label_cf.Text = _ms.mLunchFee.ToString();
            //工作奖金(全) 2013年3月29日  工作奖金 带出全勤奖金
            //this.label_gj.Text = _ms.mAllAttendBonus.ToString();
            //责任津贴
            this.label_ChuQinJJ.Text = _ms.mDutyPay.ToString();
            //绩效系数
            this.txtEffectFactor.EditValue = _ms.mEffectFactor;
            //迟到扣款
            this.label_cdkk.Text = _ms.mLatePunish.ToString("N0");
            //其它补款
            this.txtOtherPay.EditValue = _ms.mOtherPay;
            //其它扣款
            this.txtOtherPunish.EditValue = _ms.mOtherPunish;
            //出勤奖金
            this.label_ChuQinJJ.Text = _ms.mDutyPay.ToString();
            //实领奖金
            this.label_BonusTotal.Text = _ms.BonusTotal.ToString();
            //就源扣缴
            this.txt_JiuYuanJiaoKou.Text = _ms.mJiuYuanKouJiao.ToString();
            //小计
            //this.label_xiaoji.Text = _ms.XiaoJI.ToString();
            //加班
            //this.label_jiaban.Text = this.GetSiSheWuRu(_ms.JiaBan, 0).ToString();
            //总计
            this.label_SubTotal.Text = this.GetSiSheWuRu(_ms.SubTotal, 0).ToString();
            //实领金额
            this.label_SalaryTotal.Text = this.GetSiSheWuRu(_ms.mSalaryTotal, 0).ToString();

            flag = 0;
        }

        //单击作业技术奖金
        private void label_gj_Click(object sender, EventArgs e)
        {
            if (this._ms == null) return;
            Form f = new WordBonusList();
            if (IsShowForm(f.GetType().FullName))
            {
                if (this._ms == null) return;
                f = new WordBonusList(this._ms);
                f.MdiParent = this.MdiParent;
                f.Show();
            }
        }

        //双击查看加班
        private void label_jrjb_Click(object sender, EventArgs e)
        {
            if (this._ms == null) return;
            Form f = new OverTimeForm();
            if (IsShowForm(f.GetType().FullName))
            {
                if (this._ms == null) return;
                f = new OverTimeForm(this._ms, this.employee);
                f.MdiParent = this.MdiParent;
                f.Show();
            }
        }

        //单击底薪
        private void label_dx_Click(object sender, EventArgs e)
        {
            if (this._ms == null) return;
            Form f = new CalculationListForm();
            if (IsShowForm(f.GetType().FullName))
            {
                if (this.employee == null) return;
                f = new CalculationListForm(this._ms);
                f.MdiParent = this.MdiParent;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
        }

        private bool IsShowForm(string frName)
        {
            foreach (Form frm in this.ParentForm.MdiChildren)
            {
                if (frm.GetType().FullName.EndsWith(frName))
                {
                    frm.BringToFront();
                    return false;
                }
            }
            return true;
        }

        private void gridView2_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            Model.Employee Employee = _emplist[e.ListSourceRowIndex];
            if (e.Column.Name == this.gridColumnBusinessTime.Name && Employee.BusinessHours != null)
                e.DisplayText = Convert.ToDateTime(Employee.BusinessHours.Fromtime).ToString("HH:mm") + "~" + Convert.ToDateTime(Employee.BusinessHours.ToTime).ToString("HH:mm");
        }

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            //this._IsKeyIn = false;
            labelControl14.Text = comboBoxEdit1.Text + Properties.Resources.JJInformation;
            labelControl15.Text = comboBoxEdit1.Text + Properties.Resources.MoneyDetail;
            this.employee = this.bindingSourceEmployee.Current as Model.Employee;
            if (this.employee == null) return;
            this.Calulation(this.employee);
        }

        //单击查看餐费
        private void label_cf_Click(object sender, EventArgs e)
        {
            if (this._ms == null) return;
            Form f = new LunchDetailForm();
            if (IsShowForm(f.GetType().FullName))
            {
                if (this.employee == null) return;
                f = new LunchDetailForm(this._ms);
                f.MdiParent = this.MdiParent;
                f.BringToFront();
                f.Show();
            }
        }

        #region //private void spinEditEffectFactor_EditValueChanged(object sender, EventArgs e)
        //{
        //    if (this.flag != 1)
        //    {
        //        double a = 0;
        //        double.TryParse(this.txtEffectFactor.Text, out a);
        //        _ms.mEffectFactor = a;
        //        this.label_BonusTotal.Text = _ms.BonusTotal.ToString();
        //    }
        //}
        #endregion

        //保存
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (this.employee == null) return;
                Model.MonthlySalaryStruct monthlySalaryStruct = new Book.Model.MonthlySalaryStruct();
                monthlySalaryStruct.MonthlySalaryId = Guid.NewGuid().ToString();
                monthlySalaryStruct.EmployeeId = this.employee.EmployeeId;
                monthlySalaryStruct.IdentifyDate = new DateTime(hryear, hrmonth, 1);

                decimal.TryParse(this.txtEffectFactor.Text, out monthlySalaryStruct.EffectFactor);
                decimal.TryParse(this.txtOtherPay.Text, out monthlySalaryStruct.OtherPay);
                decimal.TryParse(this.txtOtherPunish.Text, out monthlySalaryStruct.OtherPunish);
                float.TryParse(this.txt_givenDays.Text, out monthlySalaryStruct.HolidayBonusGivenDays);

                monthlySalaryStruct.IdentifyDate = this._ms.mIdentifyDate;//new DateTime(Int32.Parse(this.comboBoxEdit1.Text.Substring(0, 4)), Int32.Parse(this.comboBoxEdit1.Text.Substring(5, 2)), 1);
                decimal.TryParse(this.txt_JiuYuanJiaoKou.Text, out monthlySalaryStruct.JiuYuanKouJiao);
                this.monthlySalaryManager.UpdateDataSet(monthlySalaryStruct);
                MessageBox.Show(Properties.Resources.SuccessfullySaved, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.Calulation(this.employee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //打印
        private MonthSalaryClass CalulationNoText(Model.Employee emp)
        {
            int mTemp = 0;
            int mHicount = 0;
            int mTimeBonus = 0;     //统计满足<时数补贴>的次数
            double mLateHalfCount = 0;
            StringBuilder strBU = new StringBuilder();
            int totalDay = DateTime.DaysInMonth(hryear, hrmonth);
            //////////////////////////////////////////////////////////////////
            MonthSalaryClass _ms = new MonthSalaryClass();
            _ms.mIdentifyDate = new DateTime(hryear, hrmonth, 1);
            _ms.mEmployeeId = emp.EmployeeId;
            _ms.mEmployeeName = emp.EmployeeName;
            _ms.mIDNo = emp.IDNo;
            _ms.mDepartmetName = emp.Department == null ? "" : emp.Department.DepartmentName;

            //------------------------------ From计算方法 ----Start----------------------------------------------------//
            #region  取考勤记录
            //全勤天数
            int attendDays = 0;
            //加班天数
            int overDays = 0;
            //无薪假天数
            double noPayleaveDays = 0;
            //全勤，公假，年假，周末
            int hasPayDays = 0;
            //实际上班时间
            double onWorkHours = 0;
            //请半天的天数
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
            //婚假，丧假，产假要计算到底薪的出勤天数
            double hunSangChan = 0;
            //隔周休假
            double gezhouxiu = 0;
            //公假 ，年假 天数
            double gnDays = 0;
            //J,O周六天数
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
                    if (_ms.mNote.Contains("公假") || _ms.mNote == "週日休假" || _ms.mNote == "周日休假" || _ms.mNote.Contains("年假") || _ms.mNote.Contains("出差") || (_ms.mNote.Contains("週六休假") && (emp.IDNo.ToUpper().StartsWith("J") || emp.IDNo.ToUpper().StartsWith("O"))))
                    {
                        hasPayDays++;
                        if (_ms.mNote == "週日休假" || _ms.mNote == "周日休假")
                            WeekendDays++;

                        if (_ms.mNote.Contains("週六休假") && (emp.IDNo.ToUpper().StartsWith("J") || emp.IDNo.ToUpper().StartsWith("O")))
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
                    if (emp.IDNo.Contains("J") && this.annualHolidayManager.IsNationalHoliday(attend.DutyDate.Value, attend.Note))
                        hasPayDays++;
                    //if (_ms.mNote.Contains("曠職") || _ms.mNote.Contains("病假") || _ms.mNote.Contains("事假") || _ms.mNote.Contains("婚假") || _ms.mNote.Contains("喪假") || _ms.mNote.Contains("無薪假") || _ms.mNote.Contains("特殊休(有薪)") || _ms.mNote.Contains("婚假") || _ms.mNote.Contains("喪假") || _ms.mNote.Contains("產假") || _ms.mNote.Contains("選舉假") || _ms.mNote.Contains("產檢假") || _ms.mNote.Contains("過年大掃除") || _ms.mNote.Contains("陪產假"))
                    //{
                    //    TimeBonus++;
                    //    //if (_ms.mNote.Contains("整日"))
                    //    //    TimeBonus += 8;
                    //    //else         //如果上下半日都有请假记录，若记录无公家，年假，无薪假，留職停薪等等，则说明都是扣时数补贴的假
                    //    //{
                    //    //    if (_ms.mNote.Contains("上半日") && _ms.mNote.Contains("下半日"))
                    //    //    {
                    //    //        if (!_ms.mNote.Contains("公假") && !_ms.mNote.Contains("年假") && !_ms.mNote.Contains("留職停薪") && !_ms.mNote.Contains("補休年假") && !_ms.mNote.Contains("出差") && !_ms.mNote.Contains("育嬰假") && !_ms.mNote.Contains("隔周休假"))
                    //    //            TimeBonus += 8;
                    //    //        else
                    //    //            TimeBonus += 4;
                    //    //    }
                    //    //    else
                    //    //        TimeBonus += 4;
                    //    //}
                    //    if (_ms.mNote.Contains("曠職"))
                    //    {
                    //        if (_ms.mNote.Contains("整日"))
                    //        {
                    //            Kuangzhi += this.KuangzhiFactor;
                    //        }
                    //        else
                    //        {
                    //            Kuangzhi += this.KuangzhiFactor / 2;
                    //        }
                    //    }
                    //}
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
                    else if (_ms.mNote == "遲到" || _ms.mNote == "早退")
                        attendDays++;
                }
                //加班天数
                if (attend.ShouldCheckIn == null && attend.ActualCheckIn != null && attend.ActualCheckOut != null)
                {
                    if (!string.IsNullOrEmpty(_ms.mNote))
                        overDays++;
                }
                //else
                //{
                //    //2013年5月20日16:26:17
                //    //没有请假.今日也不是例假则则算作<时数补贴>一次
                //    mTimeBonus++;
                //}

                //2013年5月28日11:25:28 CN
                //隔周休纳入方式: 
                //1.<隔周休>
                //   如果实施隔周休政策,将没有所谓的<时数补贴>.
                //   隔周休的员工,在双休的时候周六也有工资, 但是没有时数补贴。 
                //   若是单休周,周六却没有来。 则要算作请假之类来做扣款动作
                //   
                //2.<不隔周休>
                //   不隔周休的员工(每周都是单休),周一到周五每天给 <时薪*1.33*0.5*[符合次数]> 的时数补贴.
                //   周一~周五 哪一天请假(无论什么假,请假半天以上),则取消当天时数补贴.迟到不扣时数补贴. 只要请假就不发
                //   若是周六请假.则只针对周六做扣款之类的操作.对周内的时数补贴不做影响.
                //attend.DutyDate.Value.DayOfWeek != DayOfWeek.Saturday &&
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

            //this.AttendDays = attendDays + halfattend + WeekendDays;
            #endregion
            #region 取薪资计算
            DataSet dsms = this.monthlySalaryManager.getMonthlySummaryFee(emp.EmployeeId, _ms.mIdentifyDate, hryear, hrmonth);
            if (dsms.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsms.Tables[0].Rows[0];
                //if (emp.IsMigrantWorker)
                //    _ms.mLunchFee = 0;
                //else
                _ms.mLunchFee = mStrToDouble(dr["LunchFee"]);                                 //餐費
                _ms.mOverTimeFee = mStrToDouble(dr["OverTimeFee"]);                           //加班費
                _ms.mGeneralOverTime = mStrToDouble(dr["GeneralOverTime"]);                   //平日加班(時數)
                _ms.mHolidayOverTime = mStrToDouble(dr["HolidayOverTime"]);                   //假日加班(時數)
                _ms.GeneralOverTimeCountBig = mStrToDouble(dr["GeneralOverTimeCountBig"]);    //平日加班2小时之外(時數)
                _ms.GeneralOverTimeCountSmall = mStrToDouble(dr["GeneralOverTimeCountSmall"]);//平日加班2小时以下(時數)
                _ms.HolidayOverTimeCountBig = mStrToDouble(dr["HolidayOverTimeCountBig"]);    //假日加班2小时之外(時數)
                _ms.HolidayOverTimeCountSmall = mStrToDouble(dr["HolidayOverTimeCountSmall"]);//假日加班2小时以下(時數)
                _ms.mLateCount = mStrToDouble(dr["LateCount"]);                               //遲到次數
                _ms.mTotalLateInMinute = mStrToDouble(dr["TotalLateInMinute"]);               //總遲到(分)
                _ms.mOverTimeBonus = mStrToDouble(dr["OverTimeBonus"]);                       //加班津貼
                _ms.mSpecialBonus = mStrToDouble(dr["SpecialBonus"]);                         //中夜班津貼
                _ms.mDaysFactor = mStrToDouble(dr["DaysFactor"]);                             //總日基數
                _ms.mMonthFactor = mStrToDouble(dr["MonthFactor"]);                           //總月基數
                _ms.mDutyDateCount = mStrToDouble(dr["DutyDateCount"]);                       //總出勤記錄數
                _ms.mLeaveDate = (dr["LeaveDate"] == null || dr["LeaveDate"].ToString() == "") ? global::Helper.DateTimeParse.NullDate : Convert.ToDateTime(dr["LeaveDate"].ToString());                                                   //离职日期
                _ms.mPunishLeaveCount = mStrToDouble(dr["PunishLeaveCount"]);                 //倒扣款假總數
                _ms.mLeaveCount = mStrToDouble(dr["LeaveCount"]);                             //請假總數
                _ms.mAbsentCount = mStrToDouble(dr["AbsentCount"]);                           //曠工總數
                _ms.mTotalHoliday = mStrToDouble(dr["TotalHoliday"]);                         //該月總例假數
                _ms.mLoanFee = mStrToDouble(dr["LoanFee"]);
                // int WuXinCount = Int32.Parse(dr["WuXinCount"].ToString());
                //考勤 不满一月  日基数 =月基数-实际假数-扣款请假天数-旷职-无薪假          //矿工待处理  
                if (_ms.mDutyDateCount < totalDay)
                    _ms.mDaysFactor = _ms.mDaysFactor - _ms.mTotalHoliday;
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
                _ms.mDaysFactor = 0;
                _ms.mMonthFactor = 0;
                _ms.mDutyDateCount = 0;
                _ms.mPunishLeaveCount = 0;
                _ms.mLeaveCount = 0;
                _ms.mTotalHoliday = 0;
            }
            dsms.Clear();
            #endregion
            #region 底薪
            DataSet dx_ds = this.monthlySalaryManager.getMonthlySalary(emp.EmployeeId, _ms.mIdentifyDate);
            if (dx_ds.Tables[0].Rows.Count > 0)
            {
                _ms.mMonthFactor = _ms.mMonthFactor;
                DataRow dx_dr = dx_ds.Tables[0].Rows[0];
                //_ms.mDailyPay = mStrToDouble(dx_dr["DailyPay"]); //日工资
                _ms.mMonthlyPay = mStrToDouble(dx_dr["MonthlyPay"]); //月工资
                if (VPerson.vipPerson.Contains(emp.EmployeeId) || VPerson.specialEmp.Contains(emp.EmployeeId))
                {
                    //_ms.mDutyPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["DutyPay"]), 0);  //责任津贴
                    _ms.mPostPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["PostPay"]), 0);  //職務津貼
                }
                else if (emp.EmployeeJoinDate < Convert.ToDateTime(hryear.ToString() + "-" + hrmonth.ToString() + '-' + 01.ToString()) && (_ms.mLeaveDate > Convert.ToDateTime(hryear.ToString() + "-" + hrmonth.ToString() + '-' + totalDay.ToString()) || _ms.mLeaveDate == global::Helper.DateTimeParse.NullDate))
                {
                    //_ms.mDutyPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["DutyPay"]) * _ms.mMonthFactor / totalDay, 0);  //责任津贴
                    _ms.mPostPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["PostPay"]) * _ms.mMonthFactor / totalDay, 0);  //職務津貼
                }
                if (VPerson.specialEmp.Contains(emp.EmployeeId))
                {
                    _ms.mDutyPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["DutyPay"]), 0);
                }
                else if (emp.EmployeeJoinDate < Convert.ToDateTime(hryear.ToString() + "-" + hrmonth.ToString() + '-' + 01.ToString()) && (_ms.mLeaveDate > Convert.ToDateTime(hryear.ToString() + "-" + hrmonth.ToString() + '-' + totalDay.ToString()) || _ms.mLeaveDate == global::Helper.DateTimeParse.NullDate))
                {
                    //if (emp.IDNo.Contains("J"))
                    //{
                    //    _ms.mDutyPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["DutyPay"]) - mStrToDouble(dx_dr["DutyPay"]) / 30 * (totalDay - hasPayDays), 0);
                    //}
                    ////员工编号以J开头的不管满不满足设定的出勤奖金天数 只扣除了年假，公假之外的请假，其他都给
                    //else
                    //{
                    //    if (emp.AttendanceJJDays.HasValue && attendDays + halfattend < emp.AttendanceJJDays)
                    //        _ms.mDutyPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["DutyPay"]) - mStrToDouble(dx_dr["DutyPay"]) / 30 * (totalDay - hasPayDays + WeekendDays), 0);
                    //    else
                    //        _ms.mDutyPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["DutyPay"]) - mStrToDouble(dx_dr["DutyPay"]) / 30 * (totalDay - hasPayDays), 0);
                    //    //_ms.mDutyPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["DutyPay"]) / (totalDay - WeekendDays) * (attendDays + halfattend + gnDays), 0);
                    //}
                    //2017年1月24日 設定的年終值/（當月天數-當月星期天數）* 該員實際出勤天數(公假 年假應算出勤)=年終，半天不算
                    //以O, J 开头的员工 設定的年終值/（當月天數-當月星期6，日天數）* 該員實際出勤天數(公假 年假應算出勤)=年終
                    if (emp.IDNo.ToUpper().StartsWith("J") || emp.IDNo.ToUpper().StartsWith("O"))
                        _ms.mDutyPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["DutyPay"]) / (totalDay - WeekendDays - saturdays) * (attendDays + gnDays), 0);
                    else
                        _ms.mDutyPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["DutyPay"]) / (totalDay - WeekendDays) * (attendDays + gnDays), 0);
                } //责任津贴   新版改为出勤奖金   又改为 伙食津贴 改为 年終
                _ms.mGivenDays = mStrToDouble(dx_dr["HolidayBonusGivenDays"]);  //年假(补休)天数
                _ms.mAnnualHolidayFee = this.GetSiSheWuRu(_ms.mMonthlyPay / 30 * _ms.mGivenDays, 0);         //年假(补休)金额
                _ms.mInsurance = mStrToDouble(dx_dr["insurance"]); //保险费
                //_ms.mTax = mStrToDouble(dx_dr["Tax"]);   //所得税
                _ms.mEffectFactor = mStrToDouble(dx_dr["EffectFactor"]); //績效係數
                _ms.mOtherPay = mStrToDouble(dx_dr["OtherPay"]);  //其它補款
                _ms.mOtherPunish = mStrToDouble(dx_dr["OtherPunish"]); //其它扣款
                _ms.mJiuYuanKouJiao = mStrToDouble(dx_dr["JiuYuanKouJiao"]); //就源扣缴
                //职场津贴算法：来公司未足月者按照实际上班天数计算而且加班也计入，足月按每月天数减去请假次数计算
                //if (emp.EmployeeJoinDate > Convert.ToDateTime(hryear.ToString() + "-" + hrmonth.ToString() + '-' + 01.ToString()) || emp.EmployeeLeaveDate < Convert.ToDateTime(hryear.ToString() + "-" + hrmonth.ToString() + '-' + totalDay.ToString()))
                //    _ms.mFieldPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["FieldPay"]) * (attendDays + overDays) / totalDay, 0);//职场津贴
                //else
                //    _ms.mFieldPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["FieldPay"]) * (totalDay - _ms.mCount) / totalDay, 0);//职场津贴
                //_ms.mBasePay = this.GetSiSheWuRu(_ms.mMonthlyPay * (_ms.mMonthFactor - noPayleaveDays - halfDays + halfDayFactors) / totalDay, 0);                
                if (VPerson.specialEmp.Contains(emp.EmployeeId))
                {
                    _ms.mBasePay = _ms.mMonthlyPay;
                }
                else if (emp.EmployeeJoinDate > Convert.ToDateTime(hryear.ToString() + "-" + hrmonth.ToString() + '-' + 01.ToString()) || (_ms.mLeaveDate != global::Helper.DateTimeParse.NullDate && _ms.mLeaveDate < Convert.ToDateTime(hryear.ToString() + "-" + hrmonth.ToString() + '-' + 01.ToString()).AddMonths(1)))
                {
                    _ms.mBasePay = this.GetSiSheWuRu(_ms.mMonthlyPay / 30 * (attendDays + halfattend - Kuangzhi), 0);
                    this.AttendDays = attendDays + halfattend - Kuangzhi;
                }
                else
                {
                    if (emp.IsMonthSalary)     //底薪，月薪员工用月薪-未出勤扣减
                    {
                        //if (emp.IsMigrantWorker)
                        //{
                        //    if (emp.AttendanceDays.HasValue && emp.AttendanceDays.Value > Convert.ToDouble(attendDays) + halfattend + hunSangChan)
                        //    {
                        //        _ms.mBasePay = this.GetSiSheWuRu(_ms.mMonthlyPay - _ms.mMonthlyPay / 30 * (Kuangzhi + MigrantWorkerLeaveDays + totalDay - _ms.mDutyDateCount + WeekendDays), 0);
                        //        this.AttendDays = _ms.mDutyDateCount - MigrantWorkerLeaveDays - WeekendDays - Kuangzhi;
                        //    }
                        //    else
                        //    {
                        //        _ms.mBasePay = this.GetSiSheWuRu(_ms.mMonthlyPay - _ms.mMonthlyPay / 30 * (Kuangzhi + MigrantWorkerLeaveDays + totalDay - _ms.mDutyDateCount), 0);
                        //        this.AttendDays = _ms.mDutyDateCount - MigrantWorkerLeaveDays - Kuangzhi;
                        //    }

                        //}
                        //else
                        //{
                        if (emp.AttendanceDays.HasValue && emp.AttendanceDays.Value > Convert.ToDouble(attendDays) + halfattend + hunSangChan)
                        {
                            _ms.mBasePay = this.GetSiSheWuRu(_ms.mMonthlyPay - _ms.mMonthlyPay / 30 * (Kuangzhi + totalDay - _ms.mMonthFactor + noPayleaveDays + halfDays - halfDayFactors + WeekendDays), 0);
                            this.AttendDays = _ms.mMonthFactor - noPayleaveDays - halfDays + halfDayFactors - WeekendDays - Kuangzhi;
                        }
                        else
                        {
                            _ms.mBasePay = this.GetSiSheWuRu(_ms.mMonthlyPay - _ms.mMonthlyPay / 30 * (Kuangzhi + totalDay - _ms.mMonthFactor + noPayleaveDays + halfDays - halfDayFactors), 0);
                            this.AttendDays = _ms.mMonthFactor - noPayleaveDays - halfDays + halfDayFactors - Kuangzhi;
                        }
                        //}
                    }
                    else                     //不是月薪为  日薪*实际出勤天数
                    {
                        //if (emp.IsMigrantWorker)
                        //{
                        //    if (emp.AttendanceDays.HasValue && emp.AttendanceDays.Value > Convert.ToDouble(attendDays) + halfattend + hunSangChan)
                        //    {
                        //        this.AttendDays = _ms.mDutyDateCount - MigrantWorkerLeaveDays - WeekendDays - Kuangzhi;
                        //        _ms.mBasePay = this.GetSiSheWuRu(_ms.mMonthlyPay * (this.AttendDays) / 30, 0);
                        //    }
                        //    else
                        //    {
                        //        this.AttendDays = _ms.mDutyDateCount - MigrantWorkerLeaveDays - Kuangzhi;
                        //        _ms.mBasePay = this.GetSiSheWuRu(_ms.mMonthlyPay * (this.AttendDays) / 30, 0);
                        //    }//_ms.mDutyDateCount区别于totalDay的地方在于离职后_ms.mDutyDateCount不会增加，记录的是实际出勤次数
                        //}
                        //else
                        //{
                        if (emp.AttendanceDays.HasValue && emp.AttendanceDays.Value > Convert.ToDouble(attendDays) + halfattend + hunSangChan)
                        {
                            this.AttendDays = _ms.mMonthFactor - noPayleaveDays - halfDays + halfDayFactors - WeekendDays - Kuangzhi;
                            _ms.mBasePay = this.GetSiSheWuRu(_ms.mMonthlyPay * (this.AttendDays) / 30, 0);
                        }
                        else
                        {
                            this.AttendDays = _ms.mMonthFactor - noPayleaveDays - halfDays + halfDayFactors - Kuangzhi;
                            _ms.mBasePay = this.GetSiSheWuRu(_ms.mMonthlyPay * (this.AttendDays) / 30, 0);
                        }
                        //}//底薪 新版只有月薪，无日薪

                    }
                }
                if (VPerson.specialEmp.Contains(emp.EmployeeId))
                {
                    _ms.TimeBonus = 0;
                    _ms.mSpecialBonus = 0;
                }
                else
                {
                    if (emp.IDNo.Contains("J"))
                        _ms.TimeBonus = 0;
                    else if (!emp.IsMonthSalary || emp.IsMigrantWorker)
                    {
                        //if (this.leavemanage.GetMonthIsGeZhouXiu(emp.EmployeeId, _ms.mIdentifyDate) == 0 && onWorkHours >= 84 && !emp.IsMigrantWorker && !emp.IsMonthSalary) //没有隔周休并且实际上班时间大于84小时，并且不是外劳,不是月薪！
                        //    _ms.TimeBonus = this.GetSiSheWuRu(_ms.mMonthlyPay / 30 / 8 * 1 * 0.5 * mTimeBonus - _ms.mMonthlyPay / 30 / 8 * TimeBonus, 0) < 0 ? 0 : this.GetSiSheWuRu(_ms.mMonthlyPay / 30 / 8 * 1 * 0.5 * mTimeBonus - _ms.mMonthlyPay / 30 / 8 * TimeBonus, 0);      //时数补贴 = 时薪*1*0.5*[符合次数] 符合次数内不计算加班的天数，只算正常出勤。
                        //2016年3月19日 新算法
                        //if (Convert.ToDouble(emp.TimeBonusDays) < Convert.ToDouble(attendDays) + halfattend)
                        //{
                        //    _ms.TimeBonus = this.GetSiSheWuRu(Convert.ToDouble(emp.TimeBonusTimes) * _ms.mMonthlyPay / 30 / 8 * 0.5, 0);
                        //}
                        //else
                        //{
                        //    _ms.TimeBonus = this.GetSiSheWuRu((Convert.ToDouble(emp.TimeBonusDays) - TimeBonus - gezhouxiu * 5) < 0 ? 0 : (Convert.ToDouble(emp.TimeBonusDays) - TimeBonus - gezhouxiu * 5) * _ms.mMonthlyPay / 30 / 8 * 0.5, 0);
                        //}
                        //if ((Convert.ToDouble(attendDays) + halfattend) <= TimeBonus)
                        //    _ms.TimeBonus = 0;
                        //else
                        //    _ms.TimeBonus = this.GetSiSheWuRu((Convert.ToDouble(attendDays) + halfattend - TimeBonus) * _ms.mMonthlyPay / 30 / 8 * 0.833, 0); 
                        //2016年4月28日18:30:36  时数补贴 改为按实际出勤天数计算  2017年1月24日 去掉 时数补贴
                        //_ms.TimeBonus = this.GetSiSheWuRu((Convert.ToDouble(attendDays)) * _ms.mMonthlyPay / 30 / 8 * 0.833, 0);
                    }
                    if (emp.IsMigrantWorker)
                        _ms.mSpecialBonus = 0;
                    else
                        _ms.mSpecialBonus = _ms.mSpecialBonus - halfSpecialBonus;
                }
                //if (this.leavemanage.GetMonthIsGeZhouXiu(emp.EmployeeId, _ms.mIdentifyDate) == 0)
                //    _ms.TimeBonus = this.GetSiSheWuRu(_ms.mDailyPay / 8 * 1 * 0.5 * mTimeBonus, 0);      //时数补贴 = 时薪*1*0.5*[符合次数] 符合次数内不计算加班的天数，只算正常出勤。

            }
            else
            {
                //_ms.mDailyPay = 0;
                _ms.mMonthlyPay = 0;
                _ms.mDutyPay = 0;
                _ms.mPostPay = 0;
                _ms.mGivenDays = 0;
                _ms.mAnnualHolidayFee = 0;
                _ms.mInsurance = 0;
                _ms.mTax = 0;
                _ms.mEffectFactor = 0;
                _ms.mOtherPay = 0;
                _ms.mOtherPunish = 0;
                _ms.mFieldPay = 0;
                _ms.mBasePay = 0;
                _ms.TimeBonus = 0;
            }
            dx_ds.Clear();
            #endregion
            #region 迟到扣款
            int mIcount = 0;  //和小于10分 次数
            if ((_ms.mTotalLateInMinute > 10 || _ms.mLateCount > 2) && !VPerson.vipPerson.Contains(emp.EmployeeId) && !VPerson.specialEmp.Contains(emp.EmployeeId))
            {
                //临时记录迟到

                //StringBuilder strBU = new StringBuilder();
                //foreach (Model.HrDailyEmployeeAttendInfo attend in this._hrManager.SelectByEmpMonth(this.employee, hryear, hrmonth))
                //{
                //    if (attend.LateInMinute.Value != 0)
                //    {
                //        strBU.Append(attend.LateInMinute.ToString() + "|");
                //        mTemp = mTemp + attend.LateInMinute.Value;
                //        if (mTemp <= 10)
                //        {
                //            mCount = mCount + 1;
                //        }
                //        if (attend.LateInMinute.Value > 30)
                //        {
                //            mHicount = mHicount + 1;
                //        }
                //    }
                //}
                // '遲到超過三次，或總遲到超過10分鐘
                //'化成小時，以0.5小時為刻度
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
                mIcount = (int)_ms.mLateCount;// 10分钟内 次数 用于全勤奖
            }
            #endregion
            #region 全勤奖 新版去掉
            //if (VPerson.vipPerson.Contains(emp.EmployeeId))
            //{
            //    _ms.mAllAttendBonus = _ms.mDailyPay * 3;
            //}
            //else
            //{
            //    if (emp.IsMigrantWorker)
            //        _ms.mAllAttendBonus = 0;
            //    else if (_ms.mDutyDateCount == totalDay)
            //    {
            //        //有做滿一個月：日薪三天
            //        if (_ms.mMonthFactor == totalDay)
            //        {
            //            if (_ms.mPunishLeaveCount == 0)
            //            {
            //                _ms.mAllAttendBonus = _ms.mDailyPay * 3;
            //            }
            //            //請倒扣款假小於等於一天：日薪二天   
            //            else if (_ms.mPunishLeaveCount <= 1)
            //            {
            //                _ms.mAllAttendBonus = _ms.mDailyPay * 2;
            //            }
            //            //判斷遲到,扣除全勤獎金
            //            if (_ms.mLateCount - mIcount > 0)
            //            {
            //                _ms.mAllAttendBonus = _ms.mAllAttendBonus - _ms.mDailyPay * (_ms.mLateCount - mIcount) >= 0 ? _ms.mAllAttendBonus - _ms.mDailyPay * (_ms.mLateCount - mIcount) : 0;
            //            }
            //        }
            //        //有缺刷卡記錄                 
            //        else
            //        {
            //            _ms.mAllAttendBonus = 0;
            //        }
            //    }
            //    //未滿一個月（因到職或離職）
            //    else
            //    {
            //        _ms.mAllAttendBonus = 0;
            //    }
            //}
            #endregion
            #region 離職未做滿一整個月者，取消：「責任津貼」、「職務津貼」、「職場津貼」、「週日休假」
            if (_ms.mDutyDateCount < totalDay && _ms.mLeaveDate != global::Helper.DateTimeParse.NullDate && !VPerson.vipPerson.Contains(emp.EmployeeId))
            {
                //_ms.mDutyPay = 0;
                //_ms.mPostPay = 0;
                //_ms.mFieldPay = 0;
            }
            #endregion
            #region 平日加班 假日加班 酬薪加班津贴 及税额
            #region //弃用
            //_ms.mGeneralOverTimeFee = GetSiSheWuRu((_ms.mDailyPay / 6) * _ms.mGeneralOverTime, 0);                   //平日加班费
            //_ms.mHolidayOverTimeFee = GetSiSheWuRu((((_ms.mDailyPay / 2) * 3) / 8) * _ms.mHolidayOverTime, 0);       //假日加班费
            #endregion
            #region //启用 假日&平日加班算法修改 2013年4月26日15:09:41 陈宁
            //平日加班 0~2小时之类 时薪*hour*1.33  2>  时薪*hour*1.66
            //if (_ms.mGeneralOverTime < 2)
            //{
            //    _ms.mGeneralOverTimeFee = GetSiSheWuRu((_ms.mDailyPay / 8) * _ms.mGeneralOverTime * 1.33, 0);
            //}
            //else
            //{
            //    _ms.mGeneralOverTimeFee = GetSiSheWuRu((_ms.mDailyPay / 8) * 2 * 1.33, 0) + GetSiSheWuRu((_ms.mDailyPay / 8) * (_ms.mGeneralOverTime - 2) * 1.66, 0);
            //}
            if (!VPerson.specialEmp.Contains(emp.EmployeeId))
            {
                _ms.mGeneralOverTimeFee = GetSiSheWuRu((_ms.mMonthlyPay / 30 / 8) * (_ms.GeneralOverTimeCountSmall * 1.334 + _ms.GeneralOverTimeCountBig * 1.667), 0);
                //假日加班 一律 为时薪 两倍.
                _ms.mHolidayOverTimeFee = GetSiSheWuRu(((_ms.mMonthlyPay / 30 / 8) * 3 / 2) * _ms.mHolidayOverTime, 0);
            }
            #endregion
            #endregion
            #region 工作奖金,绩效奖金, 作业技术奖金 => 工作奖金=职务津贴＋责任津贴＋班别津贴+全勤奖 去掉
            //int[] months = { 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3 };
            //switch (months[hrmonth - 1])
            //{
            //    case 1:
            //        _ms.mWorkBonus = _ms.mPostPay + _ms.mDutyPay + _ms.mSpecialBonus + _ms.mAllAttendBonus;
            //        break;
            //    case 2:
            //        _ms.mEffectBonus = _ms.mPostPay + _ms.mDutyPay + _ms.mSpecialBonus + _ms.mAllAttendBonus;
            //        break;
            //    case 3:
            //        _ms.mTechBonus = _ms.mPostPay + _ms.mDutyPay + _ms.mSpecialBonus + _ms.mAllAttendBonus;
            //        break;
            //}
            #endregion

            return _ms;
            //------------------------------ From计算方法 ----End----------------------------------------------------//

            #region //------------------------------ 原有方法---Start-----------------------------------------------------//
            //#region  取考勤记录

            //DataRow drms;
            //int lateInMinute = 0;
            //DataSet ds = this.monthlySalaryManager.getAttendInfoList(emp.EmployeeId, hryear, hrmonth);
            //{
            //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //    {
            //        drms = ds.Tables[0].Rows[i];


            //        if (drms["LateInMinute"] != null && int.Parse(drms["LateInMinute"].ToString()) != 0)
            //        {
            //            lateInMinute = int.Parse(drms["LateInMinute"].ToString());
            //            strBU.Append(drms["LateInMinute"].ToString() + "|");
            //            mTemp = mTemp + lateInMinute;
            //            //if (mTemp <= 10)
            //            //{
            //            //    mCount = mCount + 1;
            //            //}
            //            if (lateInMinute > 30)
            //            {
            //                mHicount = mHicount + 1;
            //                if ((lateInMinute + 30) % 60 > 30)
            //                {
            //                    mLateHalfCount = mLateHalfCount + (lateInMinute + 30) / 60 + 0.5;
            //                }
            //                else
            //                {
            //                    mLateHalfCount = mLateHalfCount + (lateInMinute + 30) / 60;
            //                }
            //            }
            //            //在职务津贴扣除符合条件假期
            //        }
            //        _ms.mNote = drms["Note"].ToString();
            //        if (!string.IsNullOrEmpty(_ms.mNote))
            //        {
            //            if (_ms.mNote != "周日休假" && _ms.mNote.IndexOf("公假") < 0 && _ms.mNote.IndexOf("婚假") < 0 && _ms.mNote.IndexOf("喪假") < 0 && _ms.mNote.IndexOf("年假") < 0 && _ms.mNote.IndexOf("出差") < 0 && _ms.mNote.IndexOf("遲到") < 0)
            //                _ms.mCount = _ms.mCount + 1;

            //        }

            //    }
            //}
            //ds.Clear();
            //ds.Dispose();
            //#endregion
            //#region 取薪资计算
            //DataSet dsms = this.monthlySalaryManager.getMonthlySummaryFee(emp.EmployeeId, _ms.mIdentifyDate, hryear, hrmonth);
            //if (dsms.Tables[0].Rows.Count > 0)
            //{
            //    DataRow dr = dsms.Tables[0].Rows[0];
            //    _ms.mLunchFee = mStrToDouble(dr["LunchFee"]);  // 午餐費
            //    _ms.mOverTimeFee = mStrToDouble(dr["OverTimeFee"]);// 加班費
            //    _ms.mGeneralOverTime = mStrToDouble(dr["GeneralOverTime"]); // 平日加班（時數）
            //    _ms.mHolidayOverTime = mStrToDouble(dr["HolidayOverTime"]); // 假日加班（時數）
            //    _ms.mLateCount = mStrToDouble(dr["LateCount"]); // 遲到次數
            //    _ms.mTotalLateInMinute = mStrToDouble(dr["TotalLateInMinute"]);            // 總遲到（分）
            //    _ms.mOverTimeBonus = mStrToDouble(dr["OverTimeBonus"]);                     // 加班津貼
            //    _ms.mSpecialBonus = mStrToDouble(dr["SpecialBonus"]);                       // 中夜班津貼
            //    _ms.mDaysFactor = mStrToDouble(dr["DaysFactor"]);                           // 總日基數
            //    _ms.mMonthFactor = mStrToDouble(dr["MonthFactor"]);                         // 總月基數
            //    _ms.mDutyDateCount = mStrToDouble(dr["DutyDateCount"]);                     // 總出勤記錄數
            //    _ms.mLeaveDate = (dr["LeaveDate"] == null || dr["LeaveDate"].ToString() == "") ? global::Helper.DateTimeParse.NullDate : Convert.ToDateTime(dr["LeaveDate"].ToString()); // 离职日期
            //    _ms.mPunishLeaveCount = mStrToDouble(dr["PunishLeaveCount"]);               // 倒扣款假總數
            //    _ms.mLeaveCount = mStrToDouble(dr["LeaveCount"]);                           // 請假總數
            //    _ms.mAbsentCount = mStrToDouble(dr["AbsentCount"]);                         // 曠工總數
            //    _ms.mTotalHoliday = mStrToDouble(dr["TotalHoliday"]);                       // 該月總例假數
            //    _ms.mLoanFee = mStrToDouble(dr["LoanFee"]);
            //    //  int WuXinCount = Int32.Parse(dr["WuXinCount"].ToString());
            //    //考勤 不满一月  日基数 =月基数-实际假数-扣款请假天数-旷职-无薪假     //矿工待处理  
            //    if (_ms.mDutyDateCount < totalDay)
            //        _ms.mDaysFactor = _ms.mDaysFactor - _ms.mTotalHoliday;
            //    //    if (_ms.mDutyDateCount < totalDay && _ms.mLeaveDate != global::Helper.DateTimeParse.NullDate && _ms.mLeaveDate.ToString() != "")             //總出勤記錄數
            //    //        _ms.mDaysFactor = _ms.mMonthFactor - _ms.mTotalHoliday - _ms.mPunishLeaveCount - _ms.mAbsentCount - WuXinCount;
            //    //    else if ((_ms.mLeaveDate == global::Helper.DateTimeParse.NullDate || _ms.mLeaveDate.ToString() == "") && _ms.mDutyDateCount < totalDay)
            //    //        _ms.mDaysFactor = _ms.mMonthFactor - _ms.mTotalHoliday - _ms.mPunishLeaveCount - _ms.mAbsentCount - WuXinCount;
            //}
            //else
            //{
            //    _ms.mLoanFee = 0;
            //    _ms.mLunchFee = 0;
            //    _ms.mOverTimeFee = 0;
            //    _ms.mGeneralOverTime = 0;
            //    _ms.mHolidayOverTime = 0;
            //    _ms.mLateCount = 0;
            //    _ms.mTotalLateInMinute = 0;
            //    _ms.mOverTimeBonus = 0;
            //    _ms.mSpecialBonus = 0;
            //    _ms.mDaysFactor = 0;
            //    _ms.mMonthFactor = 0;
            //    _ms.mDutyDateCount = 0;
            //    _ms.mPunishLeaveCount = 0;
            //    _ms.mLeaveCount = 0;
            //    _ms.mTotalHoliday = 0;
            //}
            //dsms.Clear();
            //dsms.Dispose();
            //#endregion
            //#region 底薪
            //DataSet dx_ds = this.monthlySalaryManager.getMonthlySalary(emp.EmployeeId, _ms.mIdentifyDate);
            //if (dx_ds.Tables[0].Rows.Count > 0)
            //{
            //    _ms.mMonthFactor = _ms.mMonthFactor;
            //    DataRow dx_dr = dx_ds.Tables[0].Rows[0];
            //    _ms.mDailyPay = mStrToDouble(dx_dr["DailyPay"]);                          //日工资
            //    _ms.mMonthlyPay = mStrToDouble(dx_dr["MonthlyPay"]);                     //月工资
            //    _ms.mDutyPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["DutyPay"]) * _ms.mMonthFactor / totalDay, 0); //责任津贴
            //    _ms.mPostPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["PostPay"]) * _ms.mMonthFactor / totalDay, 0); //職務津貼
            //    _ms.mInsurance = mStrToDouble(dx_dr["insurance"]);                                      //保险费
            //    _ms.mTax = mStrToDouble(dx_dr["Tax"]);                                                  //所得税
            //    _ms.mEffectFactor = mStrToDouble(dx_dr["EffectFactor"]);   //績效係數
            //    _ms.mOtherPay = mStrToDouble(dx_dr["OtherPay"]);           //其它補款
            //    _ms.mOtherPunish = mStrToDouble(dx_dr["OtherPunish"]);     //其它扣款
            //    _ms.mFieldPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["FieldPay"]) * (totalDay - _ms.mCount) / totalDay, 0); //职场津贴
            //    _ms.mBasePay = _ms.mMonthlyPay * _ms.mMonthFactor / totalDay + _ms.mDailyPay * _ms.mDaysFactor;                //底薪
            //}
            //else
            //{
            //    _ms.mDailyPay = 0;
            //    _ms.mMonthlyPay = 0;
            //    _ms.mDutyPay = 0;
            //    _ms.mPostPay = 0;
            //    _ms.mInsurance = 0;
            //    _ms.mTax = 0;
            //    _ms.mEffectFactor = 0;
            //    _ms.mOtherPay = 0;
            //    _ms.mOtherPunish = 0;
            //    _ms.mFieldPay = 0;
            //    _ms.mBasePay = 0;
            //}
            //dx_ds.Clear();
            //dx_ds.Dispose();
            //#endregion
            //#region 迟到扣款
            //int mIcount = 0;  //和小于10分 次数
            //if (_ms.mTotalLateInMinute > 10 || _ms.mLateCount > 2)
            //{
            //    //临时记录迟到

            //    //StringBuilder strBU = new StringBuilder();
            //    //foreach (Model.HrDailyEmployeeAttendInfo attend in this._hrManager.SelectByEmpMonth(this.employee, hryear, hrmonth))
            //    //{
            //    //    if (attend.LateInMinute.Value != 0)
            //    //    {
            //    //        strBU.Append(attend.LateInMinute.ToString() + "|");
            //    //        mTemp = mTemp + attend.LateInMinute.Value;
            //    //        if (mTemp <= 10)
            //    //        {
            //    //            mCount = mCount + 1;
            //    //        }
            //    //        if (attend.LateInMinute.Value > 30)
            //    //        {
            //    //            mHicount = mHicount + 1;
            //    //        }
            //    //    }
            //    //}
            //    // '遲到超過三次，或總遲到超過10分鐘
            //    //'化成小時，以0.5小時為刻度
            //    string[] strs = new string[31];
            //    IList<int> a = new List<int>();

            //    if (strBU.Length > 0)
            //        strs = strBU.ToString(0, strBU.Length - 1).Split('|');
            //    for (int i = 0; i < strs.Length; i++)
            //    {
            //        if (strs[i] != null)
            //        {
            //            if (strs[i] == "0")
            //                continue;
            //            else
            //                a.Add(Int32.Parse(strs[i]));
            //        }
            //    }

            //    int temp = 0;
            //    for (int i = 0; i < a.Count; i++)
            //    {
            //        for (int j = i + 1; j < a.Count; j++)
            //        {
            //            if (a[i] > a[j])
            //            {
            //                temp = a[i];
            //                a[i] = a[j];
            //                a[j] = temp;
            //            }

            //        }
            //    }
            //    int sum = 0;
            //    int m;
            //    for (m = 0; m < a.Count; m++)
            //    {
            //        sum = sum + a[m];
            //        if (sum > 10)
            //            break;
            //    }
            //    if (m > 2)
            //    {
            //        m = 2;
            //    }
            //    mIcount = m;
            //    _ms.mTotalLateInHour = (_ms.mLateCount - mIcount - mHicount) * 0.5 + mLateHalfCount;
            //    _ms.mLatePunish = _ms.mTotalLateInHour * double.Parse(_ms.mDailyPay.ToString()) / 8;
            //}
            //else
            //{
            //    _ms.mTotalLateInHour = 0;
            //    _ms.mLatePunish = 0;
            //    mIcount = (int)_ms.mLateCount;// 10分钟内 次数 用于全勤奖
            //}
            //#endregion
            //#region 全勤奖
            //if (_ms.mDutyDateCount == totalDay)
            //{
            //    //有做滿一個月：日薪三天
            //    if (_ms.mMonthFactor == totalDay)
            //    {
            //        if (_ms.mPunishLeaveCount == 0)
            //        {
            //            _ms.mAllAttendBonus = _ms.mDailyPay * 3;
            //        }
            //        //請倒扣款假小於等於一天：日薪二天   
            //        else if (_ms.mPunishLeaveCount <= 1)
            //        {
            //            _ms.mAllAttendBonus = _ms.mDailyPay * 2;
            //        }
            //        //判斷遲到,扣除全勤獎金
            //        if (_ms.mLateCount - mIcount > 0)
            //        {
            //            _ms.mAllAttendBonus = _ms.mAllAttendBonus - _ms.mDailyPay * (_ms.mLateCount - mIcount) >= 0 ? _ms.mAllAttendBonus - _ms.mDailyPay * (_ms.mLateCount - mIcount) : 0;
            //        }
            //    }
            //    //有缺刷卡記錄                 
            //    else
            //    {
            //        _ms.mAllAttendBonus = 0;
            //    }
            //}
            ////未滿一個月（因到職或離職）
            //else
            //{
            //    _ms.mAllAttendBonus = 0;
            //}
            //#endregion
            //#region 離職未做滿一整個月者，取消：「責任津貼」、「職務津貼」、「職場津貼」、「週日休假」
            //if (_ms.mDutyDateCount < totalDay && _ms.mLeaveDate != global::Helper.DateTimeParse.NullDate)
            //{
            //    _ms.mDutyPay = 0;
            //    _ms.mPostPay = 0;
            //    _ms.mFieldPay = 0;
            //}
            //#endregion
            //#region 平日加班 假日加班 酬薪加班津贴 及税额
            //_ms.mGeneralOverTimeFee = GetSiSheWuRu((_ms.mDailyPay / 6) * _ms.mGeneralOverTime, 0);                   //平日加班费
            //_ms.mHolidayOverTimeFee = GetSiSheWuRu((((_ms.mDailyPay / 2) * 3) / 8) * _ms.mHolidayOverTime, 0);       //假日加班费


            //#endregion
            //#region 工作奖金,绩效奖金, 作业技术奖金 => 工作奖金=职务津贴＋责任津贴＋班别津贴+全勤奖
            //int[] months = { 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3 };
            //switch (months[hrmonth - 1])
            //{
            //    case 1:
            //        _ms.mWorkBonus = _ms.mPostPay + _ms.mDutyPay + _ms.mSpecialBonus + _ms.mAllAttendBonus;
            //        break;
            //    case 2:
            //        _ms.mEffectBonus = _ms.mPostPay + _ms.mDutyPay + _ms.mSpecialBonus + _ms.mAllAttendBonus;
            //        break;
            //    case 3:
            //        _ms.mTechBonus = _ms.mPostPay + _ms.mDutyPay + _ms.mSpecialBonus + _ms.mAllAttendBonus;
            //        break;
            //}

            //return _ms;
            //#endregion
            #endregion------------------------------ 原有方法- End-------------------------------------------------------//

            #region 取考勤note (取消不用)
            //foreach (Model.HrDailyEmployeeAttendInfo attend in this._hrManager.SelectByEmpMonth(emp, hryear, hrmonth))
            //{
            //    if (attend.LateInMinute.HasValue && attend.LateInMinute.Value != 0)
            //    {
            //        strBU.Append(attend.LateInMinute.ToString() + "|");
            //        mTemp = mTemp + attend.LateInMinute.Value;
            //        //if (mTemp <= 10)
            //        //{
            //        //    mCount = mCount + 1;
            //        //}
            //        if (attend.LateInMinute.Value > 30)
            //        {
            //            mHicount = mHicount + 1;
            //            if ((attend.LateInMinute.Value + 30) % 60 > 30)
            //            {
            //                mLateHalfCount = mLateHalfCount + (attend.LateInMinute.Value + 30) / 60 + 0.5;
            //            }
            //            else
            //            {
            //                mLateHalfCount = mLateHalfCount + (attend.LateInMinute.Value + 30) / 60;
            //            }
            //        }
            //        //在职务津贴扣除符合条件假期
            //    }
            //    _ms.mNote = attend.Note;
            //    if (!string.IsNullOrEmpty(_ms.mNote))
            //    {
            //        if (_ms.mNote != "周日休假" && _ms.mNote.IndexOf("公假") < 0 && _ms.mNote.IndexOf("婚假") < 0 && _ms.mNote.IndexOf("喪假") < 0 && _ms.mNote.IndexOf("年假") < 0 && _ms.mNote.IndexOf("出差") < 0 && _ms.mNote.IndexOf("遲到") < 0)
            //            _ms.mCount = _ms.mCount + 1;

            //    }
            //}
            #endregion

        }

        //列印总月报表
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Model.MonthlySalary monthlySalarys = new Book.Model.MonthlySalary();
            DataSet1.MonthlySalarysDataTable table = new DataSet1.MonthlySalarysDataTable();
            DataRow _drT;
            //  DataSet dx_ds = this.monthlySalaryManager.GetMonthlySummaryByMonth(hryear, hrmonth);
            if (_emplist != null && _emplist.Count > 0)// (dx_ds.Tables[0].Rows.Count > 0)
            {
                foreach (Model.Employee emp in _emplist) // for (int i = 0; i < dx_ds.Tables[0].Rows.Count; i++)
                {
                    MonthSalaryClass _ms = this.CalulationNoText(emp);
                    //赋值
                    _drT = table.NewRow();
                    _drT["EmployeeId"] = _ms.mEmployeeId;
                    //_drT["DailyPay"] = _ms.mDailyPay;
                    _drT["IDNo"] = _ms.mIDNo;
                    _drT["DepartmentName"] = _ms.mDepartmetName;
                    _drT["CompanyName"] = emp.Company == null ? "" : emp.Company.ToString();
                    _drT["MonthlyPay"] = _ms.mMonthlyPay;
                    _drT["BasePay"] = _ms.mBasePay;
                    _drT["TimeBonus"] = _ms.TimeBonus;
                    //_drT["FieldPay"] = _ms.mFieldPay;
                    _drT["SubTotal"] = GetSiSheWuRu(_ms.SubTotal, 0);
                    _drT["LunchFee"] = _ms.mLunchFee;
                    _drT["Insurance"] = _ms.mInsurance;
                    _drT["LoanFee"] = _ms.mLoanFee;
                    //_drT["Tax"] = _ms.mTax;
                    _drT["SalaryTotal"] = _ms.mSalaryTotal;
                    _drT["DutyPay"] = _ms.mDutyPay;
                    _drT["PostPay"] = _ms.mPostPay;
                    //_drT["AllAttendBonus"] = _ms.mAllAttendBonus;
                    _drT["SpecialBonus"] = _ms.mSpecialBonus;
                    //_drT["WorkBonus"] = _ms.mWorkBonus;
                    //_drT["EffectBonus"] = _ms.mEffectBonus;
                    //_drT["TechBonus"] = _ms.mTechBonus;
                    _drT["EffectFactor"] = _ms.mEffectFactor;
                    _drT["GeneralOverTime"] = _ms.mGeneralOverTime;
                    //_drT["GeneralOverTime"] = _ms.OverTimeCountBig + _ms.OverTimeCounttotalDaySmall;
                    _drT["HolidayOverTime"] = _ms.mHolidayOverTime;
                    _drT["GeneralOverTimeFee"] = _ms.mGeneralOverTimeFee;
                    //_drT["GeneralOverTimeFee"] = GetSiSheWuRu(_ms.JiaBan, 0);
                    _drT["HolidayOverTimeFee"] = _ms.mHolidayOverTimeFee;
                    _drT["OverTimeFee"] = _ms.JiaBan;
                    _drT["OverTimeBonus"] = _ms.mOverTimeBonus;
                    _drT["GivenDays"] = _ms.mGivenDays;
                    _drT["AnnualHolidayFee"] = GetSiSheWuRu(_ms.mAnnualHolidayFee, 0);
                    _drT["OtherPay"] = _ms.mOtherPay;
                    _drT["OtherPunish"] = _ms.mOtherPunish;
                    _drT["BonusTotal"] = _ms.BonusTotal;
                    _drT["ShouldPay"] = _ms.mShouldPay;
                    _drT["LatePunish"] = _ms.mLatePunish;
                    _drT["LateCount"] = _ms.mLateCount;
                    _drT["TotalLateInMinute"] = _ms.mTotalLateInMinute;
                    _drT["TotalLateInHour"] = _ms.mTotalLateInHour;
                    _drT["PunishCount"] = _ms.mPunishCount;
                    _drT["EmployeeName"] = _ms.mEmployeeName;
                    _drT["MonthFactor"] = this.AttendDays;
                    //_drT["DaysFactor"] = _ms.mDaysFactor;
                    _drT["AnnualPay"] = _ms.mActualPay;
                    _drT["AnnualPay"] = _ms.mSalaryTotal + _ms.BonusTotal;
                    //_drT["XiaoJI"] = GetSiSheWuRu(_ms.XiaoJI, 0);
                    //_drT["XiaoJI"] = GetSiSheWuRu(_ms.XiaoJI, 0);
                    _drT["JiuYuanKouJiao"] = _ms.mJiuYuanKouJiao;
                    _drT["Total"] = _ms.Total;
                    //_drT["JiaBan"] = GetSiSheWuRu(_ms.JiaBan, 0);
                    //if (Convert.ToInt16(_drT["JiaBan"]) == 0)
                    //    _drT["JiaBanDesc"] = string.Empty;
                    //else
                    //    //_drT["JiaBanDesc"] = _ms.OverTimeCountSmall.ToString() + "H * 1.33 + " + _ms.OverTimeCountBig.ToString() + "H * 1.66";
                    //    _drT["JiaBanDesc"] = "平=" + _ms.GeneralOverTimeCountSmall.ToString() + "H * 1.33 + " + _ms.GeneralOverTimeCountBig.ToString() + "H * 1.66,假=" + _ms.mHolidayOverTime + "H * 2";
                    table.Rows.Add(_drT);
                }
                CalCrystalReportForm f = new CalCrystalReportForm(table, hryear, hrmonth);
                //table.Clear();
                //table.Dispose();
                f.Show();
            }
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.hryear = Int32.Parse(this.comboBoxEdit1.Text.Substring(0, 4));
            this.hrmonth = Int32.Parse(this.comboBoxEdit1.Text.Substring(5, 2));
            this._emplist = this.employeeManager.SelectHrDailyAttend(new DateTime(hryear, hrmonth, 1));
            this.bindingSourceEmployee.DataSource = _emplist;
        }

        /// <summary>
        /// 四舍五入方法.
        /// </summary>
        /// <param name="objTarget">要操作的double类型数字</param>
        /// <param name="mIndex">欲保留小数位数</param>
        /// <returns></returns>
        public double GetSiSheWuRu(double objTarget, int mIndex)
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

        //标识是否是手动输入,true 手动输入 需要重新计算保存,flase 不是手动输入 不需要保存
        bool _IsKeyIn;

        //其它补款
        private void txtOtherPay_EditValueChanged(object sender, EventArgs e)
        {
            //if (this.txtOtherPay.IsEditorActive)
            //    _IsKeyIn = true;
            //else
            //    _IsKeyIn = false;
            //this.mSaveToChange();
        }

        //其它扣款
        private void txtOtherPunish_EditValueChanged(object sender, EventArgs e)
        {
            //if (this.txtOtherPunish.IsEditorActive)
            //    _IsKeyIn = true;
            //else
            //    _IsKeyIn = false;
            //this.mSaveToChange();
        }

        //绩效系数更改
        private void txtEffectFactor_EditValueChanged(object sender, EventArgs e)
        {
            //if (this.txtEffectFactor.IsEditorActive)
            //    _IsKeyIn = true;
            //else
            //    _IsKeyIn = false;
            //this.mSaveToChange();
        }

        //年假(补休)天数更改
        private void txt_givenDays_EditValueChanged(object sender, EventArgs e)
        {
            //if (this.txt_givenDays.IsEditorActive)
            //    _IsKeyIn = true;
            //else
            //    _IsKeyIn = false;

            //this.mSaveToChange();
        }

        private void txt_JiuYuanJiaoKou_EditValueChanged(object sender, EventArgs e)
        {
            //if (this.txt_JiuYuanJiaoKou.IsEditorActive)
            //    _IsKeyIn = true;
            //else
            //    _IsKeyIn = false;
            //this.mSaveToChange();
        }

        private void mSaveToChange()
        {
            try
            {
                //if (_IsKeyIn)
                //{
                if (this.employee == null) return;
                Model.MonthlySalaryStruct monthlySalaryStruct = new Book.Model.MonthlySalaryStruct();
                monthlySalaryStruct.MonthlySalaryId = Guid.NewGuid().ToString();
                monthlySalaryStruct.EmployeeId = this.employee.EmployeeId;

                decimal.TryParse(this.txtEffectFactor.Text, out monthlySalaryStruct.EffectFactor);
                decimal.TryParse(this.txtOtherPay.Text, out monthlySalaryStruct.OtherPay);
                decimal.TryParse(this.txtOtherPunish.Text, out monthlySalaryStruct.OtherPunish);
                float.TryParse(this.txt_givenDays.Text, out monthlySalaryStruct.HolidayBonusGivenDays);
                decimal.TryParse(this.txt_JiuYuanJiaoKou.Text, out monthlySalaryStruct.JiuYuanKouJiao);

                monthlySalaryStruct.IdentifyDate = this._ms.mIdentifyDate;
                this.monthlySalaryManager.UpdateDataSet(monthlySalaryStruct);
                this.Calulation(this.employee);
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 对象转Double
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private double mStrToDouble(object o)
        {
            return double.Parse(string.IsNullOrEmpty(o.ToString()) ? "0" : o.ToString());
        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gridControl2_Click(object sender, EventArgs e)
        {

        }

        private void label_ChuQinJJ_Click(object sender, EventArgs e)
        {
            this.lbl_turnOutSalary.Text = this.turnOutSalary.ToString();
        }

    }

    /// <summary>
    /// 享特殊待遇员工
    /// </summary>
    public static class VPerson
    {
        /// <summary>
        /// VIP员工：辛旻瓊
        /// </summary>
        public static readonly IList<string> vipPerson = new List<string> { "B212C2AD-E291-41C1-ABBC-79C7496BFC55" };

        /// <summary>
        /// 特殊员工:林雄正  董宏仁 黃明華 辛旻瓊 黃盟舜 康振忠 柯欣茂，魏貝恩 底薪，年终固定，没有班别津贴和时数补贴
        /// </summary>
        public static readonly IList<string> specialEmp = new List<string> { "F9A22FFE-15E5-4D52-A039-3F5BAC57ACFA", "A534791D-0C81-4DFC-83D1-03801DDBB446", "3B7A16B8-2A24-49D2-AD85-E7BA3F4E5700", "97F1E465-AA14-4D62-A514-A85EA1A8AACB", "195933C9-F363-4C76-B6EE-55EC06FFE651", "B212C2AD-E291-41C1-ABBC-79C7496BFC55", "D4AE743A-390C-43A3-B45C-90781E8DC115", "43BEB178-38C9-4CBB-B3B9-A22EBEA6BBF9" };

        /// <summary>
        /// 六位员工国定假日不扣出勤奖：王明欽,陳藝鈴,蘇嘉鴻,王乙蓉,陳政瑋,林家菁 2016年6月7日11:58:43：改为员工编号“J”开头的都不扣
        /// </summary>
        public static readonly IList<string> specialEmpOfAttendJJ = new List<string> { "a23fe8a8-f3fd-45ec-a1e1-ac80635ca2f1", "3637487b-e417-4d38-85bd-89f135194015", "9da24020-b736-4b98-baa2-7217d016d8f1", "6A9A467C-F60D-46FE-BA61-F5DEBF41339A", "660295fa-0b96-47ba-ad7c-da9a92968ded", "63C7C24A-790A-4990-8C42-98AA515AE40C" };
    }
}


#region ===========注释备用==========
#region @ old Print
//DataRow dr;//内存表
//DataRow row; //monthlySalary薪资表
//DataRow row1;// 薪资统计表中HrAttendStat的行 只有1行
//DataSet dataset = this.monthlySalaryManager.GetMonthlySummaryByMonth(hryear, hrmonth);
//Model.Employee employees;

//decimal PunishLeaveCount = 0;

//if (dataset.Tables[0].Rows.Count > 0)
//{
//    for (int j = 0; j < dataset.Tables[0].Rows.Count; j++)
//    {
//        row = dataset.Tables[0].Rows[j];
//        employees = this.employeeManager.Get(row["EmployeeId"].ToString());

//        _hrAttendStat = this.hrAttendManager.SelectHrAttendStatByEmpidAndYearMonth(employees, hryear, hrmonth);
//        row1 = this.monthlySalaryManager.GetMonthlySummaryFee(employees.EmployeeId, hryear, hrmonth).Tables[0].Rows[0];

//        //monthFactor = this._hrManager.SelectDayMonthSum(hryear, hrmonth, employees);
//        dr = table.NewRow();
//        if (_hrAttendStat != null)   //考勤统计表
//        {
//            dr["LoanFee"] = this._hrAttendStat.LoanFee == null ? 0 : this._hrAttendStat.LoanFee.Value;

//            dr["LunchFee"] = this._hrAttendStat.LunchFee == null ? 0 : this._hrAttendStat.LunchFee.Value;

//            dr["OverTimeFee"] = this._hrAttendStat.OverTimeFee == null ? 0 : this._hrAttendStat.OverTimeFee.Value;
//            //row1["OverTimeFee"] == null || row1["OverTimeFee"].ToString() == "" ? 0 : decimal.Parse(row1["OverTimeFee"].ToString());
//            dr["OverTimeBonus"] = this._hrAttendStat.OverTimeBonus == null ? 0 : this._hrAttendStat.OverTimeBonus.Value;//row1["OverTimeBonus"] == null || row1["OverTimeBonus"].ToString() == "" ? 0 : decimal.Parse(row1["OverTimeBonus"].ToString()); 
//            dr["SpecialBonus"] = this._hrAttendStat.SpecialBonus == null ? 0 : this._hrAttendStat.SpecialBonus.Value; //row1["SpecialBonus"] == null || row1["SpecialBonus"].ToString() == "" ? 0 : decimal.Parse(row1["SpecialBonus"].ToString());
//            row1["DutyDateCount"] = this._hrAttendStat.DutyDateCount == null ? 0 : this._hrAttendStat.DutyDateCount.Value;
//            row1["TotalHoliday"] = this._hrAttendStat.TotalHoliday == null ? 0 : this._hrAttendStat.TotalHoliday.Value;
//            row1["AbsentCount"] = this._hrAttendStat.AbsentCount == null ? 0 : this._hrAttendStat.AbsentCount.Value;
//        }

//        dr["CompanyName"] = BL.Settings.CompanyChineseName;
//        dr["EmployeeName"] = employees.EmployeeName;
//        dr["IDNo"] = employees.IDNo;

//        dr["EffectFactor"] = row["EffectFactor"] == null || row["EffectFactor"].ToString() == "" ? 0 : decimal.Parse(row["EffectFactor"].ToString());

//        //日基数
//        dr["DaysFactor"] = row1["DaysFactor"] == null || row1["DaysFactor"].ToString() == "" ? 0 : decimal.Parse(row1["DaysFactor"].ToString());
//        //非离职出勤不到满月  排除例假数。请假数，旷职数

//        if (row1["DutyDateCount"] != null && row1["DutyDateCount"].ToString() != "" && decimal.Parse(row1["DutyDateCount"].ToString()) < DateTime.DaysInMonth(hryear, hrmonth))
//        {
//            dr["DaysFactor"] = decimal.Parse(row1["MonthFactor"].ToString()) - (row1["TotalHoliday"] == null || row1["TotalHoliday"].ToString() == "" ? 0 : decimal.Parse(row1["TotalHoliday"].ToString())) - (row1["LeaveCount"] == null || row1["LeaveCount"].ToString() == "" ? 0 : decimal.Parse(row1["LeaveCount"].ToString())) - (row1["AbsentCount"] == null || row1["AbsentCount"].ToString() == "" ? 0 : decimal.Parse(row1["AbsentCount"].ToString()));
//        }

//        //月基数
//        dr["MonthFactor"] = row1["MonthFactor"] == null || row1["MonthFactor"].ToString() == "" ? 0 : decimal.Parse(row1["MonthFactor"].ToString());

//        #region 底薪
//        // 倒扣款例假数


//        PunishLeaveCount = 0;//倒扣款例假總數

//        //小於兩天不扣
//        if (row1["PunishLeaveCount"] != null && row1["PunishLeaveCount"].ToString() != "" && decimal.Parse(row1["PunishLeaveCount"].ToString()) < 2)
//            PunishLeaveCount = 0;
//        //2-2.5天扣1個例假日，3-3.5扣2個例假日，4-4.5天扣3個例假日，5-5.5天扣4個例假日
//        else
//        {

//            row1["TotalHoliday"] = row1["TotalHoliday"] == null || row1["TotalHoliday"].ToString() == "" ? 0 : row1["TotalHoliday"];

//            if (row1["PunishLeaveCount"] != null && row1["PunishLeaveCount"].ToString() != "" && decimal.Parse(row1["PunishLeaveCount"].ToString()) - decimal.Parse("1.05") >= decimal.Parse(row1["TotalHoliday"].ToString()))
//                PunishLeaveCount = decimal.Parse(row1["TotalHoliday"].ToString());
//            else
//                PunishLeaveCount = decimal.Parse(row1["TotalHoliday"].ToString()) - decimal.Parse("1.05");
//        }
//        dr["DaysFactor"] = decimal.Parse(dr["DaysFactor"].ToString()) - PunishLeaveCount;
//        //底薪=月薪/当前月天数*月基数 + 日薪*（日基数-旷职日数-扣例假数）
//        dr["BasePay"] = employees.MonthlyPay.Value / DateTime.DaysInMonth(hryear, hrmonth) * decimal.Parse(dr["MonthFactor"].ToString()) + employees.DailyPay.Value * (decimal.Parse(dr["DaysFactor"].ToString()));

//        #endregion
//        dr["GivenDays"] = row["HolidayBonusGivenDays"] == null || row["HolidayBonusGivenDays"].ToString() == "" ? 0 : decimal.Parse(row["HolidayBonusGivenDays"].ToString());
//        dr["OtherPay"] = row["OtherPay"] == null || row["OtherPay"].ToString() == "" ? 0 : decimal.Parse(row["OtherPay"].ToString());
//        dr["OtherPunish"] = row["OtherPunish"] == null || row["OtherPunish"].ToString() == "" ? 0 : decimal.Parse(row["OtherPunish"].ToString());

//        dr["PostPay"] = employees.PostPay == null ? 0 : employees.PostPay.Value;
//        dr["DutyPay"] = employees.DutyPay == null ? 0 : employees.DutyPay.Value;
//        #region/职场津贴





//        // decimal SpecificHoliday = this.leavemanage.SelectSpecificHolidayMonthEmp(employees, hryear, hrmonth);
//        //HFieldPay = (employees.FieldPay.Value / DateTime.DaysInMonth(hryear, hrmonth) * (dayfactorSum - SpecificHoliday))==0?0:(employees.FieldPay.Value / DateTime.DaysInMonth(hryear, hrmonth) * (dayfactorSum - SpecificHoliday));
//        //如果旷职 扣除周末 职场津贴
//        if (row1["AbsentCount"] != null && row1["AbsentCount"].ToString() != "" && decimal.Parse(row1["AbsentCount"].ToString()) > 0)
//        {
//            dr["FieldPay"] = employees.FieldPay.Value;// / DateTime.DaysInMonth(hryear, hrmonth) * (decimal.Parse(row1["DaysFactor"].ToString()) - this.leavemanage.SelectSpecificHolidayMonthEmp(employees, hryear, hrmonth) - this.leavemanage.SelectTotalHolidayMonthEmp(employees, hryear, hrmonth));
//        }
//        else
//        {
//            dr["FieldPay"] = employees.FieldPay.Value;
//        }
//        #endregion




//        #region    全勤奖
//        // decimal AllAttendBonus = decimal.Zero;

//        if (row1["DutyDateCount"] != null && row1["DutyDateCount"].ToString() != "" && decimal.Parse(row1["DutyDateCount"].ToString()) == DateTime.DaysInMonth(hryear, hrmonth))
//        {

//            //有做滿一個月：日薪三天
//            if (decimal.Parse(dr["MonthFactor"].ToString()) == DateTime.DaysInMonth(hryear, hrmonth))
//            {
//                if (row1["PunishLeaveCount"] != null && row1["PunishLeaveCount"].ToString() != "" && decimal.Parse(row1["PunishLeaveCount"].ToString()) == 0)
//                {
//                    dr["AllAttendBonus"] = employees.DailyPay.Value * 3;
//                }
//                //請倒扣款假小於等於一天：日薪二天   
//                else if (row1["PunishLeaveCount"] != null && row1["PunishLeaveCount"].ToString() != "" && decimal.Parse(row1["PunishLeaveCount"].ToString()) <= 1)
//                {
//                    dr["AllAttendBonus"] = employees.DailyPay.Value * 2;
//                }
//                else
//                    dr["AllAttendBonus"] = 0;

//            }

//             //有缺刷卡記錄                 
//            else
//            {
//                dr["AllAttendBonus"] = 0;
//            }
//        }
//        //未滿一個月（因到職或離職）
//        else
//        {
//            dr["AllAttendBonus"] = 0;
//        }
//        #endregion
//        // dr["WorkBonus"] = 1000;
//        dr["EffectBonus"] = 1000;


//        #region //加班
//        decimal OverTimeFee = decimal.Zero;
//        decimal OverTimeBonus = decimal.Zero;
//        DataSet overtimeData = this.overtimemanage.SelectOverTimeInfoByEmployeeId(employees.EmployeeId, new DateTime(hryear, hrmonth, 01));
//        for (int i = 0; i < overtimeData.Tables[0].Rows.Count; i++)
//        {
//            OverTimeFee += decimal.Parse(overtimeData.Tables[0].Rows[i][Model.OverTime.PROPERTY_OVERTIMEFEE].ToString());
//            OverTimeBonus += decimal.Parse(overtimeData.Tables[0].Rows[i][Model.OverTime.PROPERTY_OVERTIMEBONUS].ToString());
//        }
//        dr["OverTimeFee"] = OverTimeFee;
//        dr["OverTimeBonus"] = OverTimeBonus;

//        #endregion
//        dr["AnnualHolidayFee"] = 0;


//        dr["LatePunish"] = 0;
//        dr["DailyPay"] = employees.DailyPay == null ? 0 : employees.DailyPay.Value;
//        dr["Insurance"] = employees.Insurance == null ? 0 : employees.Insurance.Value;
//        // dr["LoanFee"] = row1["LoanFee"] == null || row1["LoanFee"].ToString() == "" ? 0 : decimal.Parse(row1["LoanFee"].ToString()); // this.loandetailManage.SelectFeeSum(employees, hryear, hrmonth);
//        // dr["LunchFee"] = row1["LunchFee"] == null || row1["LunchFee"].ToString() == "" ? 0 : decimal.Parse(row1["LunchFee"].ToString());// this.lunchdetailmanage.SelectFeeSum(employees, hryear, hrmonth);
//        #region 判断为空
//        dr["Tax"] = employees.Tax == null ? 0 : employees.Tax.Value;

//        dr["BasePay"] = dr["BasePay"] == null || dr["BasePay"].ToString() == "" ? 0 : dr["BasePay"];
//        dr["AllAttendBonus"] = dr["AllAttendBonus"] == null || dr["AllAttendBonus"].ToString() == "" ? 0 : dr["AllAttendBonus"];
//        dr["DutyPay"] = dr["DutyPay"] == null || dr["DutyPay"].ToString() == "" ? 0 : dr["DutyPay"];
//        dr["PostPay"] = dr["PostPay"] == null || dr["PostPay"].ToString() == "" ? 0 : dr["PostPay"];
//        dr["FieldPay"] = dr["FieldPay"] == null || dr["FieldPay"].ToString() == "" ? 0 : dr["FieldPay"];
//        dr["OverTimeFee"] = dr["OverTimeFee"] == null || dr["OverTimeFee"].ToString() == "" ? 0 : dr["OverTimeFee"];
//        dr["OverTimeBonus"] = dr["OverTimeBonus"] == null || dr["OverTimeBonus"].ToString() == "" ? 0 : dr["OverTimeBonus"];
//        dr["SpecialBonus"] = dr["SpecialBonus"] == null || dr["SpecialBonus"].ToString() == "" ? 0 : dr["SpecialBonus"];
//        dr["EffectFactor"] = dr["EffectFactor"] == null || dr["EffectFactor"].ToString() == "" ? 0 : dr["EffectFactor"];

//        dr["AnnualHolidayFee"] = dr["AnnualHolidayFee"] == null || dr["AnnualHolidayFee"].ToString() == "" ? 0 : dr["AnnualHolidayFee"];
//        dr["OtherPay"] = dr["OtherPay"] == null || dr["OtherPay"].ToString() == "" ? 0 : dr["OtherPay"];
//        dr["OtherPunish"] = dr["OtherPunish"] == null || dr["OtherPunish"].ToString() == "" ? 0 : dr["OtherPunish"];
//        dr["LatePunish"] = dr["LatePunish"] == null || dr["LatePunish"].ToString() == "" ? 0 : dr["LatePunish"];


//        dr["ShouldPay"] = dr["ShouldPay"] == null || dr["ShouldPay"].ToString() == "" ? 0 : dr["ShouldPay"];

//        dr["Insurance"] = dr["Insurance"] == null || dr["Insurance"].ToString() == "" ? 0 : dr["Insurance"];
//        dr["LunchFee"] = dr["LunchFee"] == null || dr["LunchFee"].ToString() == "" ? 0 : dr["LunchFee"];
//        dr["LoanFee"] = dr["LoanFee"] == null || dr["LoanFee"].ToString() == "" ? 0 : dr["LoanFee"];
//        #endregion

//        dr["ShouldPay"] = decimal.Parse(dr["BasePay"].ToString()) + decimal.Parse(dr["AllAttendBonus"].ToString()) + decimal.Parse(dr["DutyPay"].ToString()) + decimal.Parse(dr["PostPay"].ToString()) + decimal.Parse(dr["FieldPay"].ToString()) + decimal.Parse(dr["OverTimeFee"].ToString()) + decimal.Parse(dr["OverTimeBonus"].ToString()) + decimal.Parse(dr["SpecialBonus"].ToString()) + decimal.Parse(dr["EffectFactor"].ToString()) + decimal.Parse(dr["AnnualHolidayFee"].ToString()) + decimal.Parse(dr["OtherPay"].ToString()) - decimal.Parse(dr["OtherPunish"].ToString()) - decimal.Parse(dr["LatePunish"].ToString());

//        dr["BonusTotal"] = decimal.Parse(dr["ShouldPay"].ToString()) - decimal.Parse(dr["Insurance"].ToString()) - decimal.Parse(dr["LunchFee"].ToString()) - decimal.Parse(dr["LoanFee"].ToString()) - decimal.Parse(dr["Tax"].ToString());
//        //dr["BonusTotal"] = 2222;
//        table.Rows.Add(dr);
//    }

//    CalCrystalReportForm f = new CalCrystalReportForm(table, hryear, hrmonth);
//    f.Show();
//}
#endregion
//#region @初始化
////monthsum = 0;

//#endregion
//#region @底薪
//////总出勤数量
////DutyDateCount = this._hrManager.SelectDutyDateCount(hryear, hrmonth, this.employee);
//////日基数
////dayfactorSum = this._hrManager.SelectDayFactorSum(hryear, hrmonth, this.employee);
////monthFactorSum = this._hrManager.SelectDayMonthSum(hryear, hrmonth, this.employee);

////// 倒扣款例假数

////decimal PunishCount = this.leavemanage.SelectPunishByMonthEmp(this.employee, hryear, hrmonth);// 倒扣款假總數
////decimal TotalHoliday = this.leavemanage.SelectTotalHolidayMonthEmp(this.employee, hryear, hrmonth);//出勤记录月份假日和
////decimal PunishLeaveCount = PunishCount;//倒扣款例假總數


////#region 小於兩天不扣
//////if (PunishLeaveCount < 2)
//////    PunishLeaveCount = 0;
////////2-2.5天扣1個例假日，3-3.5扣2個例假日，4-4.5天扣3個例假日，5-5.5天扣4個例假日
//////else
//////{
//////    if (PunishLeaveCount - decimal.Parse("1.05") >= TotalHoliday)
//////        PunishLeaveCount = TotalHoliday;
//////    else
//////        PunishLeaveCount = TotalHoliday - decimal.Parse("1.05");
//////}
////#endregion
//////底薪=月薪/当前月天数*月基数 + 日薪*（日基数-旷职日数-扣例假数）
////this.employee.MonthlyPay = this.employee.MonthlyPay == null ? 0 : this.employee.MonthlyPay;
////this.employee.DailyPay = this.employee.DailyPay == null ? 0 : this.employee.DailyPay;

////monthsum = this.employee.MonthlyPay.Value / DateTime.DaysInMonth(hryear, hrmonth) * this.monthFactorSum + this.employee.DailyPay.Value * (this.dayfactorSum - PunishLeaveCount);


//#endregion
//#region @加班津贴

//#endregion
//#region        @班别津贴

////decimal BusinessHourPay = decimal.Zero;
////BusinessHourPay = this._hrManager.SelectBusinessHourPaySum(hryear, hrmonth, this.employee.EmployeeId);
//#endregion
//#region @平日加班 假日加班 酬薪加班津贴 及税额

////DataSet overtimeData = this.overtimemanage.SelectOverTimeInfoByEmployeeId(this.employee.EmployeeId, new DateTime(hryear, hrmonth, 01));
////for (int i = 0; i < overtimeData.Tables[0].Rows.Count; i++)
////{

////    if ((bool)overtimeData.Tables[0].Rows[i][Model.OverTime.PROPERTY_ISHOLIDAY])
////    {
////        HolidayFee += decimal.Parse(overtimeData.Tables[0].Rows[i][Model.OverTime.PROPERTY_OVERTIMEFEE].ToString());

////    }
////    else
////    {

////        NormalFee += decimal.Parse(overtimeData.Tables[0].Rows[i][Model.OverTime.PROPERTY_OVERTIMEFEE].ToString());
////    }
////    OverTimeBonus += decimal.Parse(overtimeData.Tables[0].Rows[i][Model.OverTime.PROPERTY_OVERTIMEBONUS].ToString());

////}

////this.label_prjb.Text = NormalFee.ToString("N0");
////this.label_jrjb.Text = HolidayFee.ToString("N0");
////this.label_jbjt.Text = OverTimeBonus.ToString("N0");
////this.employee.Tax = this.employee.Tax == null ? 0 : this.employee.Tax;
////this.labelTax.Text = this.employee.Tax.Value.ToString("N0");
//#endregion
//#region @迟到扣款
////if (this._hrManager.SelectLateSum(hryear, hrmonth, this.employee) > 10 || this._hrManager.SelectLateCount(hryear, hrmonth, this.employee) > 3)
////{
////    LatePunish = 0;
////    //临时记录迟到
////    int mCount = 0;  //和小于10分 次数
////    //StringBuilder strBU = new StringBuilder();
////    foreach (Model.HrDailyEmployeeAttendInfo attend in this._hrManager.SelectByEmpMonth(this.employee, hryear, hrmonth))
////    {
////        if (attend.LateInMinute.Value != 0)
////        {
////            strBU.Append(attend.LateInMinute.ToString() + "|");
////            mTemp = mTemp + attend.LateInMinute.Value;
////            if (mTemp <= 10)
////            {
////                mCount = mCount + 1;
////            }
////            if (attend.LateInMinute.Value > 30)
////            {
////                mHicount = mHicount + 1;
////            }
////        }
////    }
////    // '遲到超過三次，或總遲到超過10分鐘
////    //'化成小時，以0.5小時為刻度
////    string[] strs = new string[31];
////    IList<int> a = new List<int>();

////    if (strBU.Length > 0)
////        strs = strBU.ToString(0, strBU.Length - 1).Split('|');
////    for (int i = 0; i < strs.Length; i++)
////    {
////        if (strs[i] != null)
////        {
////            if (strs[i] == "0")
////                continue;
////            else
////                a.Add(Int32.Parse(strs[i]));
////        }
////    }

////    int temp = 0;
////    for (int i = 0; i < a.Count; i++)
////    {
////        for (int j = i + 1; j < a.Count; j++)
////        {
////            if (a[i] > a[j])
////            {
////                temp = a[i];
////                a[i] = a[j];
////                a[j] = temp;
////            }

////        }
////    }
////    int sum = 0;
////    int m;
////    for (m = 0; m < a.Count; m++)
////    {
////        sum = sum + a[m];
////        if (sum > 10)
////            break;
////    }
////    if (m > 3)
////    {
////        m = 3;
////    }
////    mCount = m;
////    decimal TotalLateInHour = decimal.Parse(((this._hrManager.SelectLateCount(hryear, hrmonth, this.employee) - mCount - mHicount) * 0.5 + mHicount).ToString());
////    LatePunish = TotalLateInHour * this.employee.DailyPay.Value / 8;

////}
////this.label_cdkk.Text = LatePunish.ToString("N0");


//#endregion
//#region    @全勤奖
////decimal AllAttendBonus = decimal.Zero;

////if (DutyDateCount == DateTime.DaysInMonth(hryear, hrmonth))
////{

////    //有做滿一個月：日薪三天
////    if (monthFactorSum == DateTime.DaysInMonth(hryear, hrmonth))
////    {
////        if (PunishCount == 0)
////        {
////            AllAttendBonus = this.employee.DailyPay.Value * 3;
////        }
////        //請倒扣款假小於等於一天：日薪二天   
////        else if (PunishCount <= 1)
////        {
////            AllAttendBonus = this.employee.DailyPay.Value * 2;
////        }
////    }

////     //有缺刷卡記錄                 
////    else
////    {
////        AllAttendBonus = 0;
////    }
////}
//////未滿一個月（因到職或離職）
////else
////{
////    AllAttendBonus = 0;
////}



//#endregion
//#region @工作奖金,绩效奖金, 作业技术奖金
//////工作奖金=职务津贴＋责任津贴＋班别津贴+全勤奖
////this.employee.DutyPay = this.employee.DutyPay == null ? 0 : this.employee.DutyPay;
////this.employee.PostPay = this.employee.PostPay == null ? 0 : this.employee.PostPay;
////WorkBonus = this.employee.PostPay.Value + this.employee.DutyPay.Value + BusinessHourPay + AllAttendBonus;



////int[] months = { 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3 };
////switch (months[hrmonth - 1])
////{
////    case 1:
////        label_gj.Text = WorkBonus.ToString("N0");
////        label_jxjj.Text = "0";
////        label_zyjj.Text = "0";
////        break;
////    case 2:
////        label_jxjj.Text = WorkBonus.ToString("N0");
////        label_gj.Text = "0";
////        label_zyjj.Text = "0";
////        break;
////    case 3:
////        label_zyjj.Text = WorkBonus.ToString("N0");
////        label_jxjj.Text = "0";
////        label_gj.Text = "0";
////        break;
////}

//#endregion
//#region @职场津贴
////decimal HFieldPay = decimal.Zero;
////decimal SpecificHoliday = this.leavemanage.SelectSpecificHolidayMonthEmp(this.employee, hryear, hrmonth);
////this.employee.FieldPay = this.employee.FieldPay == null ? 0 : this.employee.FieldPay;
////HFieldPay = this.employee.FieldPay.Value / DateTime.DaysInMonth(hryear, hrmonth) * (dayfactorSum - SpecificHoliday);


//////如果旷职 扣除周末 职场津贴
////if (this._hrManager.SelectAbsentCount(hryear, hrmonth, this.employee) > 0)
////{
////    HFieldPay = this.employee.FieldPay.Value / DateTime.DaysInMonth(hryear, hrmonth) * (dayfactorSum - this.leavemanage.SelectSpecificHolidayMonthEmp(this.employee, hryear, hrmonth) - this.leavemanage.SelectTotalHolidayMonthEmp(this.employee, hryear, hrmonth));
////}
//#endregion
//#region @出勤未做滿一整個月者，取消：  「責任津貼」、「職務津貼」、「職場津貼」、「週日休假」
////if (this._hrManager.SelectDutyDateCount(hryear, hrmonth, this.employee) < DateTime.DaysInMonth(hryear, hrmonth))
////{
////    this.employee.DutyPay = 0;
////    this.employee.PostPay = 0;   //职务津贴
////    HFieldPay = 0; //职场津贴
////}
//#endregion
//#region @绩效奖金
////绩效奖金 = 全勤奖金 
////jxbonus = Punishpay;

////label_jxjj.Text = jxbonus.ToString();
//#endregion
//#region @作业技术奖金
////Model.Department dep = (new BL.DepartmentManager()).Get(employee.DepartmentId);
////if (dep.DepartmentName.Contains("射出"))
////{
////    zyjsbonus = (this.employee.DailyPay.Value) * 3;
////}

////label_zyjj.Text = "0";// zyjsbonus.ToString();
//#endregion
//#region  @绩效系数补款扣款
////this._monthlySalary = this.monthlySalaryManager.GetByeEmpIdMonth(this.employee, hryear, hrmonth);

////if (this._monthlySalary != null)
////{
////    this.spinEditEffectFactor.Value = this._monthlySalary.EffectFactor == null ? 0 : this._monthlySalary.EffectFactor.Value;
////    this.calcEditOtherPay.Value = this._monthlySalary.OtherPay == null ? 0 : this._monthlySalary.OtherPay.Value;
////    this.calcEditOtherPunish.Value = this._monthlySalary.OtherPunish == null ? 0 : this._monthlySalary.OtherPunish.Value;
////}
////else
////{
////    this.spinEditEffectFactor.Value = 0;
////    this.calcEditOtherPay.Value = 0;
////    this.calcEditOtherPunish.Value = 0;

////}
//#endregion
#endregion