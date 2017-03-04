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
    public partial class CalCrystalReportForm1 : DevExpress.XtraEditors.XtraForm
    {
        Model.HrAttendStat _hrAttendStat;
        private Model.MonthlySalary _monthlySalary = new Book.Model.MonthlySalary();
        protected BL.EmployeeManager employeeManager = new Book.BL.EmployeeManager();
        BL.MonthlySalaryManager monthlySalaryManager = new Book.BL.MonthlySalaryManager();
        private BL.HrAttendStatManager hrAttendManager = new Book.BL.HrAttendStatManager();
        private BL.OverTimeManager OverTimeManger = new BL.OverTimeManager();
        private BL.HrDailyEmployeeAttendInfoManager _hrManager = new Book.BL.HrDailyEmployeeAttendInfoManager();
        private BL.LeaveManager leavemanage = new Book.BL.LeaveManager();
        private BL.AnnualHolidayManager annualHolidayManager = new Book.BL.AnnualHolidayManager();
        double AttendDays = 0;
        double KuangzhiFactor = 0;
        int hryear = 0;
        int hrmonth = 0;


        /// <summary>
        /// 全部国定休假
        /// </summary>
        private IList<string> _HrSpecificHolidays = new List<string>();

        private DataSet1.MonthlySalarysDataTable table = new DataSet1.MonthlySalarysDataTable();

        public CalCrystalReportForm1()
        {
            InitializeComponent();
        }

        public CalCrystalReportForm1(IList<Model.Employee> emplist, int year, int month)
            : this()
        {
            CalculationForm f = new CalculationForm();
            int totalDay = f.totalDay;
            hryear = year;
            hrmonth = month;
            Model.MonthlySalary monthlySalarys = new Book.Model.MonthlySalary();
            DataRow _drT;
            //Model.Employee emp = new Book.Model.Employee();
            //DataSet dx_ds = this.monthlySalaryManager.GetMonthlySummaryByMonth(hryear, hrmonth);
            //if (dx_ds.Tables[0].Rows.Count > 0)
            //{
            //    for (int i = 0; i < dx_ds.Tables[0].Rows.Count; i++)
            //    {
            if (emplist != null && emplist.Count > 0)
            {
                foreach (Model.Employee emp in emplist)
                {
                    //emp = this.employeeManager.Get(dx_ds.Tables[0].Rows[i]["EmployeeId"].ToString());
                    MonthSalaryClass _ms = this.Calulation(emp);
                    //赋值
                    _drT = table.NewRow();
                    _drT["EmployeeId"] = emp.EmployeeId;
                    //_drT["DailyPay"] = _ms.mDailyPay;
                    _drT["IDNo"] = emp.IDNo;
                    _drT["DepartmentName"] = emp.Department == null ? "" : emp.Department.DepartmentName;
                    _drT["CompanyName"] = emp.Company == null ? "" : emp.Company.CompanyName;
                    _drT["MonthlyPay"] = _ms.mMonthlyPay;
                    _drT["BasePay"] = _ms.mBasePay;
                    _drT["TimeBonus"] = _ms.TimeBonus;
                    //_drT["FieldPay"] = _ms.mFieldPay;
                    _drT["SubTotal"] = this.GetSiSheWuRu(_ms.SubTotal, 0);
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
                    _drT["HolidayOverTime"] = _ms.mHolidayOverTime;
                    _drT["GeneralOverTimeFee"] = _ms.mGeneralOverTimeFee;
                    _drT["HolidayOverTimeFee"] = _ms.mHolidayOverTimeFee;
                    _drT["OverTimeFee"] = _ms.mOverTimeFee;
                    _drT["OverTimeBonus"] = _ms.mOverTimeBonus;
                    _drT["GivenDays"] = _ms.mGivenDays;
                    _drT["AnnualHolidayFee"] = _ms.mAnnualHolidayFee;
                    _drT["OtherPay"] = _ms.mOtherPay;
                    _drT["OtherPunish"] = _ms.mOtherPunish;
                    _drT["BonusTotal"] = _ms.BonusTotal;
                    _drT["ShouldPay"] = _ms.mShouldPay;
                    _drT["LatePunish"] = _ms.mLatePunish.ToString("N0");
                    _drT["LateCount"] = _ms.mLateCount;
                    _drT["TotalLateInMinute"] = _ms.mTotalLateInMinute;
                    _drT["TotalLateInHour"] = _ms.mTotalLateInHour;
                    _drT["PunishCount"] = _ms.mPunishCount;
                    _drT["EmployeeName"] = emp.EmployeeName;
                    //_drT["MonthFactor"] = _ms.mMonthFactor;
                    _drT["MonthFactor"] = this.AttendDays;
                    //_drT["DaysFactor"] = _ms.mDaysFactor;
                    _drT["BusinessHoursName"] = emp.BusinessHours == null ? "" : emp.BusinessHours.BusinessHoursName;
                    //_drT["XiaoJI"] = _ms.XiaoJI;
                    _drT["JiuYuanKouJiao"] = _ms.mJiuYuanKouJiao;
                    _drT["DutyPayTotal"] = _ms.DutyPayTotal;
                    //_drT["JiaBan"] = GetSiSheWuRu(_ms.JiaBan, 0);
                    ////if (Convert.ToInt16(_drT["JiaBan"]) == 0)
                    //    _drT["JiaBanDesc"] = string.Empty;
                    //else
                    //_drT["JiaBanDesc"] = _ms.OverTimeCountSmall.ToString() + "H * 1.33 + " + _ms.OverTimeCountBig.ToString() + "H * 1.66";
                    //_drT["JiaBanDesc"] = "平=" + _ms.GeneralOverTimeCountSmall.ToString() + "H * 1.33 + " + _ms.GeneralOverTimeCountBig.ToString() + "H * 1.66,假=" + _ms.mHolidayOverTime + "H * 2";
                    //平日加班费
                    _drT["JiaBanDesc"] = _ms.GeneralOverTimeCountSmall.ToString() + " H * 1.33 + " + _ms.GeneralOverTimeCountBig.ToString() + " H * 1.66";
                    //假日加班费
                    _drT["JiaBanDesc2"] = _ms.mHolidayOverTime + " H * 2";
                    table.Rows.Add(_drT);
                }
            }
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            //int hryear = DateTime.Now.Date.AddMonths(-1).Date.Year;
            //int hrmonth = DateTime.Now.Date.AddMonths(-1).Date.Month;
            //CreateDataSet();                                             //由以前的直接打印，改为条件打印，条件在构造函数中传值过来   

            CrystalReport3 cr = new CrystalReport3();
            DataTable tables = table;
            cr.SetDataSource(tables);
            cr.SetParameterValue("BoundTitle", hryear.ToString() + "年" + hrmonth.ToString() + "月獎金明細表");
            cr.SetParameterValue("SalaryTitle", hryear.ToString() + "年" + hrmonth.ToString() + "月薪資單");


            //this._HrSpecificHolidays = (from item in new BL.HrSpecificHolidayManager().Select()
            //select item.Name).ToList<string>();

            this.crystalReportViewer1.ReportSource = cr;
        }

        private void CalCrystalReportForm1_Load(object sender, EventArgs e)
        {

        }

        private void CreateDataSet()
        {
            CalculationForm f = new CalculationForm();
            int totalDay = f.totalDay;
            int hryear = DateTime.Now.Date.AddMonths(-1).Date.Year;
            int hrmonth = DateTime.Now.Date.AddMonths(-1).Date.Month;
            Model.MonthlySalary monthlySalarys = new Book.Model.MonthlySalary();
            DataRow _drT;
            Model.Employee emp = new Book.Model.Employee();
            DataSet dx_ds = this.monthlySalaryManager.GetMonthlySummaryByMonth(hryear, hrmonth);
            if (dx_ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dx_ds.Tables[0].Rows.Count; i++)
                {
                    emp = this.employeeManager.Get(dx_ds.Tables[0].Rows[i]["EmployeeId"].ToString());
                    MonthSalaryClass _ms = this.Calulation(emp);
                    //赋值
                    _drT = table.NewRow();
                    _drT["EmployeeId"] = emp.EmployeeId;
                    //_drT["DailyPay"] = _ms.mDailyPay;
                    _drT["IDNo"] = emp.IDNo;
                    _drT["DepartmentName"] = emp.Department == null ? "" : emp.Department.DepartmentName;
                    _drT["CompanyName"] = emp.Company == null ? "" : emp.Company.CompanyName;
                    _drT["MonthlyPay"] = _ms.mMonthlyPay;
                    _drT["BasePay"] = _ms.mBasePay;
                    _drT["TimeBonus"] = _ms.TimeBonus;
                    //_drT["FieldPay"] = _ms.mFieldPay;
                    _drT["SubTotal"] = this.GetSiSheWuRu(_ms.SubTotal, 0);
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
                    _drT["HolidayOverTime"] = _ms.mHolidayOverTime;
                    _drT["GeneralOverTimeFee"] = _ms.mGeneralOverTimeFee;
                    _drT["HolidayOverTimeFee"] = _ms.mHolidayOverTimeFee;
                    _drT["OverTimeFee"] = _ms.mOverTimeFee;
                    _drT["OverTimeBonus"] = _ms.mOverTimeBonus;
                    _drT["GivenDays"] = _ms.mGivenDays;
                    _drT["AnnualHolidayFee"] = _ms.mAnnualHolidayFee;
                    _drT["OtherPay"] = _ms.mOtherPay;
                    _drT["OtherPunish"] = _ms.mOtherPunish;
                    _drT["BonusTotal"] = _ms.BonusTotal;
                    _drT["ShouldPay"] = _ms.mShouldPay;
                    _drT["LatePunish"] = _ms.mLatePunish.ToString("N0");
                    _drT["LateCount"] = _ms.mLateCount;
                    _drT["TotalLateInMinute"] = _ms.mTotalLateInMinute;
                    _drT["TotalLateInHour"] = _ms.mTotalLateInHour;
                    _drT["PunishCount"] = _ms.mPunishCount;
                    _drT["EmployeeName"] = emp.EmployeeName;
                    //_drT["MonthFactor"] = _ms.mMonthFactor;
                    _drT["MonthFactor"] = this.AttendDays;
                    //_drT["DaysFactor"] = _ms.mDaysFactor;
                    _drT["BusinessHoursName"] = emp.BusinessHours == null ? "" : emp.BusinessHours.BusinessHoursName;
                    //_drT["XiaoJI"] = _ms.XiaoJI;
                    _drT["JiuYuanKouJiao"] = _ms.mJiuYuanKouJiao;
                    _drT["DutyPayTotal"] = _ms.DutyPayTotal;
                    //_drT["JiaBan"] = GetSiSheWuRu(_ms.JiaBan, 0);
                    ////if (Convert.ToInt16(_drT["JiaBan"]) == 0)
                    //    _drT["JiaBanDesc"] = string.Empty;
                    //else
                    //_drT["JiaBanDesc"] = _ms.OverTimeCountSmall.ToString() + "H * 1.33 + " + _ms.OverTimeCountBig.ToString() + "H * 1.66";
                    //_drT["JiaBanDesc"] = "平=" + _ms.GeneralOverTimeCountSmall.ToString() + "H * 1.33 + " + _ms.GeneralOverTimeCountBig.ToString() + "H * 1.66,假=" + _ms.mHolidayOverTime + "H * 2";
                    //平日加班费
                    _drT["JiaBanDesc"] = _ms.GeneralOverTimeCountSmall.ToString() + " H * 1.33 + " + _ms.GeneralOverTimeCountBig.ToString() + " H * 1.66";
                    //假日加班费
                    _drT["JiaBanDesc2"] = _ms.mHolidayOverTime + " H * 2";
                    table.Rows.Add(_drT);
                }
            }
        }

        /// <summary>
        /// 计算取值
        /// </summary>
        /// <param name="emp"></param>
        private MonthSalaryClass Calulation(Model.Employee emp)
        {
            KuangzhiFactor = new BL.LeaveTypeManager().SelectPayRateByName("曠職");
            int mTemp = 0;
            int mHicount = 0;
            int mTimeBonus = 0;     //统计满足<时数补贴>的次数
            double mLateHalfCount = 0;
            StringBuilder strBU = new StringBuilder();
            //int hryear = DateTime.Now.Date.AddMonths(-1).Date.Year;
            //int hrmonth = DateTime.Now.Date.AddMonths(-1).Date.Month;
            //int hryear = 2011;// DateTime.Now.Year;
            //int hrmonth = 12;// DateTime.Now.Month - 1;
            int totalDay = DateTime.DaysInMonth(hryear, hrmonth);
            //////////////////////////////////////////////////////////////////
            MonthSalaryClass _ms = new MonthSalaryClass();
            _ms.mIdentifyDate = new DateTime(hryear, hrmonth, 1);
            //_ms.mEmployeeId = emp.EmployeeId;
            //_ms.mEmployeeName = emp.EmployeeName;
            //_ms.mIDNo = emp.IDNo;
            //_ms.mDepartmetName = emp.Department.DepartmentName;

            //------------------------------ From计算方法 ----Start----------------------------------------------------//
            #region  取考勤记录
            //全勤天数
            int attendDays = 0;
            //加班天数
            int overDays = 0;
            //无薪假天数
            double noPayleaveDays = 0;
            //实际上班时间
            double onWorkHours = 0;
            //全勤，公假，年假，周末
            int hasPayDays = 0;
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
            //时数补贴事假,病假,曠職,婚假 喪假扣减
            double TimeBonus = 0;
            //旷职扣减
            double Kuangzhi = 0;
            //婚假，丧假，产假计算到底薪的出勤天数中
            double hunSangChan = 0;
            //隔周休假
            double gezhouxiu = 0;
            //公假 ，年假 天数
            double gnDays = 0;
            //J,O周六天数
            int saturdays = 0;
            //DataSet ds = this.monthlySalaryManager.getAttendInfoList(emp.EmployeeId, hryear, hrmonth);                   
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
                    //员工编号为J开头，并且是国定假日给出勤奖
                    if ((emp.IDNo.ToUpper().StartsWith("J") || emp.IDNo.ToUpper().StartsWith("O")) && this.annualHolidayManager.IsNationalHoliday(attend.DutyDate.Value, attend.Note))
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
                //if (attend.DutyDate.Value.DayOfWeek != DayOfWeek.Sunday)
                //{
                //    if (!this._HrSpecificHolidays.Contains(_ms.mNote) && _ms.mNote.IndexOf("半日") < 0 && _ms.mNote.IndexOf("假") < 0 && _ms.mNote.IndexOf("曠職") < 0)
                //        mTimeBonus = mTimeBonus + 1;
                //}
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
                _ms.mLunchFee = mStrToDouble(dr["LunchFee"]);                                //午餐費
                _ms.mOverTimeFee = mStrToDouble(dr["OverTimeFee"]);                          //加班費
                _ms.mGeneralOverTime = mStrToDouble(dr["GeneralOverTime"]);                  //平日加班（時數）
                _ms.mHolidayOverTime = mStrToDouble(dr["HolidayOverTime"]);                  //假日加班（時數）
                _ms.GeneralOverTimeCountBig = mStrToDouble(dr["GeneralOverTimeCountBig"]);    //平日加班2小时之外(時數)
                _ms.GeneralOverTimeCountSmall = mStrToDouble(dr["GeneralOverTimeCountSmall"]);//平日加班2小时以下(時數)
                _ms.HolidayOverTimeCountBig = mStrToDouble(dr["HolidayOverTimeCountBig"]);    //假日加班2小时之外(時數)
                _ms.HolidayOverTimeCountSmall = mStrToDouble(dr["HolidayOverTimeCountSmall"]);//假日加班2小时以下(時數)
                _ms.mLateCount = mStrToDouble(dr["LateCount"]);                              //遲到次數
                _ms.mTotalLateInMinute = mStrToDouble(dr["TotalLateInMinute"]);              //總遲到（分）
                _ms.mOverTimeBonus = mStrToDouble(dr["OverTimeBonus"]);                      //加班津貼
                _ms.mSpecialBonus = mStrToDouble(dr["SpecialBonus"]);                        //中夜班津貼
                //_ms.mDaysFactor = mStrToDouble(dr["DaysFactor"]);                            //總日基數
                _ms.mMonthFactor = mStrToDouble(dr["MonthFactor"]);                          //總月基數
                _ms.mDutyDateCount = mStrToDouble(dr["DutyDateCount"]);                      //總出勤記錄數
                _ms.mLeaveDate = (dr["LeaveDate"] == null || dr["LeaveDate"].ToString() == "") ? global::Helper.DateTimeParse.NullDate : Convert.ToDateTime(dr["LeaveDate"].ToString());                                                  //离职日期
                _ms.mPunishLeaveCount = mStrToDouble(dr["PunishLeaveCount"]);                //倒扣款假總數
                _ms.mLeaveCount = mStrToDouble(dr["LeaveCount"]);                            //請假總數
                _ms.mAbsentCount = mStrToDouble(dr["AbsentCount"]);                          //曠工總數
                _ms.mTotalHoliday = mStrToDouble(dr["TotalHoliday"]);                        //該月總例假數
                _ms.mLoanFee = mStrToDouble(dr["LoanFee"]);                                  //借支
                // int WuXinCount = Int32.Parse(dr["WuXinCount"].ToString());
                //考勤 不满一月  日基数 =月基数-实际假数-扣款请假天数-旷职-无薪假         //矿工待处理  
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
                //_ms.mMonthFactor = _ms.mMonthFactor;
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
                    _ms.mDutyPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["DutyPay"]), 0);
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
                    //if (emp.IDNo.ToUpper().StartsWith("J") || emp.IDNo.ToUpper().StartsWith("O"))
                    //_ms.mDutyPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["DutyPay"]) / (30 - WeekendDays - saturdays) * (attendDays + gnDays), 0);
                    //else
                    //_ms.mDutyPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["DutyPay"]) / (totalDay - WeekendDays) * (attendDays + gnDays), 0);
                    /*2017-3-4  
                     * 普：設定的年終值/（30-六日天数）*（30-六日天数-国定假日天数-请假天数）
                     * J,O：設定的年終值/（30-六日天数）*（30-六日天数-请假天数）
                     * 公假 年假 出差 不算請假
                     * 请假天数算法：月总天数-六日天数-全勤天数-公假，年假，出差天数-国定假日天数
                     * 总算法：年终值/(30-六日天数)*（30-月总天数+全勤天数+公假，年假，出差天数+【国假天数】）
                     * J,O 的 gnDays 已经加上了国假天数
                     */
                    _ms.mDutyPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["DutyPay"]) / (30 - WeekendDays - saturdays) * (30 - totalDay + attendDays + gnDays), 0);
                } //责任津贴   新版改为出勤奖金 改为 年终
                _ms.DutyPayTotal = mStrToDouble(dx_dr["DutyPay"]);
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
                //    _ms.mFieldPay = this.GetSiSheWuRu(mStrToDouble(dx_dr["FieldPay"]) * (totalDay - _ms.mCount) / totalDay, 0);  //职场津贴
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
                else       //底薪 新版只有月薪，无日薪
                {
                    //if (emp.IsMonthSalary)     //底薪，月薪员工用月薪-未出勤扣减
                    //{
                    //    //if (emp.IsMigrantWorker)
                    //    //{
                    //    //    if (emp.AttendanceDays.HasValue && emp.AttendanceDays.Value > Convert.ToDouble(attendDays) + halfattend + hunSangChan)
                    //    //    {
                    //    //        _ms.mBasePay = this.GetSiSheWuRu(_ms.mMonthlyPay - _ms.mMonthlyPay / 30 * (Kuangzhi + MigrantWorkerLeaveDays + totalDay - _ms.mDutyDateCount + WeekendDays), 0);
                    //    //        this.AttendDays = _ms.mDutyDateCount - MigrantWorkerLeaveDays - WeekendDays - Kuangzhi;
                    //    //    }
                    //    //    else
                    //    //    {
                    //    //        _ms.mBasePay = this.GetSiSheWuRu(_ms.mMonthlyPay - _ms.mMonthlyPay / 30 * (Kuangzhi + MigrantWorkerLeaveDays + totalDay - _ms.mDutyDateCount), 0);
                    //    //        this.AttendDays = _ms.mDutyDateCount - MigrantWorkerLeaveDays - Kuangzhi;
                    //    //    }

                    //    //}
                    //    //else
                    //    //{
                    //    if (emp.AttendanceDays.HasValue && emp.AttendanceDays.Value > Convert.ToDouble(attendDays) + halfattend + hunSangChan)
                    //    {
                    //        _ms.mBasePay = this.GetSiSheWuRu(_ms.mMonthlyPay - _ms.mMonthlyPay / 30 * (Kuangzhi + totalDay - _ms.mMonthFactor + noPayleaveDays + halfDays - halfDayFactors + WeekendDays), 0);
                    //        this.AttendDays = _ms.mMonthFactor - noPayleaveDays - halfDays + halfDayFactors - WeekendDays - Kuangzhi;
                    //    }
                    //    else
                    //    {
                    //        _ms.mBasePay = this.GetSiSheWuRu(_ms.mMonthlyPay - _ms.mMonthlyPay / 30 * (Kuangzhi + totalDay - _ms.mMonthFactor + noPayleaveDays + halfDays - halfDayFactors), 0);
                    //        this.AttendDays = _ms.mMonthFactor - noPayleaveDays - halfDays + halfDayFactors - Kuangzhi;
                    //    }
                    //    //}
                    //}
                    //else                     //不是月薪为  日薪*实际出勤天数
                    //{
                    //    //if (emp.IsMigrantWorker)
                    //    //{
                    //    //    if (emp.AttendanceDays.HasValue && emp.AttendanceDays.Value > Convert.ToDouble(attendDays) + halfattend + hunSangChan)
                    //    //    {
                    //    //        this.AttendDays = _ms.mDutyDateCount - MigrantWorkerLeaveDays - WeekendDays - Kuangzhi;
                    //    //        _ms.mBasePay = this.GetSiSheWuRu(_ms.mMonthlyPay * (this.AttendDays) / 30, 0);
                    //    //    }
                    //    //    else
                    //    //    {
                    //    //        this.AttendDays = _ms.mDutyDateCount - MigrantWorkerLeaveDays - Kuangzhi;
                    //    //        _ms.mBasePay = this.GetSiSheWuRu(_ms.mMonthlyPay * (this.AttendDays) / 30, 0);
                    //    //    }//_ms.mDutyDateCount区别于totalDay的地方在于离职后_ms.mDutyDateCount不会增加，记录的是实际出勤次数
                    //    //}
                    //    //else
                    //    //{
                    //    if (emp.AttendanceDays.HasValue && emp.AttendanceDays.Value > Convert.ToDouble(attendDays) + halfattend + hunSangChan)
                    //    {
                    //        this.AttendDays = _ms.mMonthFactor - noPayleaveDays - halfDays + halfDayFactors - WeekendDays - Kuangzhi;
                    //        _ms.mBasePay = this.GetSiSheWuRu(_ms.mMonthlyPay * (this.AttendDays) / 30, 0);
                    //    }
                    //    else
                    //    {
                    //        this.AttendDays = _ms.mMonthFactor - noPayleaveDays - halfDays + halfDayFactors - Kuangzhi;
                    //        _ms.mBasePay = this.GetSiSheWuRu(_ms.mMonthlyPay * (this.AttendDays) / 30, 0);
                    //    }
                    //    //}//底薪 新版只有月薪，无日薪

                    //}
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
                        //2016年4月28日18:30:36  时数补贴 改为按全勤天数计算  2017年1月24日 去掉 时数补贴
                        //_ms.TimeBonus = this.GetSiSheWuRu((Convert.ToDouble(attendDays)) * _ms.mMonthlyPay / 30 / 8 * 0.833, 0);
                    }
                    if (emp.IsMigrantWorker)
                        _ms.mSpecialBonus = 0;
                    else
                        _ms.mSpecialBonus = _ms.mSpecialBonus - halfSpecialBonus;
                }
            }
            else
            {
                //m = 0;
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
            #region 工作奖金,绩效奖金, 作业技术奖金 => 工作奖金=职务津贴＋责任津贴＋班别津贴+全勤奖  新版去掉
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

            #region //------------------------------备注-----------------------------------------------------------//
            //#region  取考勤记录
            //// DataSet ds = this.monthlySalaryManager.getAttendInfoList(employee.EmployeeId, hryear, hrmonth);
            ////foreach (Model.HrDailyEmployeeAttendInfo attend in this._hrManager.SelectByEmpMonth(emp, hryear, hrmonth))
            ////{
            ////    if (attend.LateInMinute.HasValue && attend.LateInMinute.Value != 0)
            ////    {

            ////        strBU.Append(attend.LateInMinute.ToString() + "|");
            ////        mTemp = mTemp + attend.LateInMinute.Value;
            ////        //if (mTemp <= 10)
            ////        //{
            ////        //    mCount = mCount + 1;
            ////        //}
            ////        if (attend.LateInMinute.Value > 30)
            ////        {
            ////            mHicount = mHicount + 1;
            ////            if ((attend.LateInMinute.Value + 30) % 60 > 30)
            ////            {
            ////                mLateHalfCount = mLateHalfCount + (attend.LateInMinute.Value + 30) / 60 + 0.5;
            ////            }
            ////            else
            ////            {
            ////                mLateHalfCount = mLateHalfCount + (attend.LateInMinute.Value + 30) / 60;
            ////            }
            ////        }

            ////        //在职务津贴扣除符合条件假期

            ////    }
            ////    _ms.mNote = attend.Note;
            ////    if (!string.IsNullOrEmpty(_ms.mNote))
            ////    {
            ////        if (_ms.mNote != "周日休假" && _ms.mNote.IndexOf("公假") < 0 && _ms.mNote.IndexOf("婚假") < 0 && _ms.mNote.IndexOf("喪假") < 0 && _ms.mNote.IndexOf("年假") < 0 && _ms.mNote.IndexOf("出差") < 0 && _ms.mNote.IndexOf("遲到") < 0)
            ////            _ms.mCount = _ms.mCount + 1;

            ////    }
            ////}
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
            //    _ms.mLunchFee = double.Parse(dr["LunchFee"].ToString());                                 // 午餐費
            //    _ms.mOverTimeFee = double.Parse(dr["OverTimeFee"].ToString());                           // 加班費
            //    _ms.mGeneralOverTime = double.Parse(dr["GeneralOverTime"].ToString());                   // 平日加班（時數）
            //    _ms.mHolidayOverTime = double.Parse(dr["HolidayOverTime"].ToString());                   // 假日加班（時數）
            //    _ms.mLateCount = double.Parse(dr["LateCount"].ToString());                               // 遲到次數
            //    _ms.mTotalLateInMinute = double.Parse(dr["TotalLateInMinute"].ToString());               // 總遲到（分）
            //    _ms.mOverTimeBonus = double.Parse(dr["OverTimeBonus"].ToString());                       // 加班津貼
            //    _ms.mSpecialBonus = double.Parse(dr["SpecialBonus"].ToString());                         // 中夜班津貼
            //    _ms.mDaysFactor = double.Parse(dr["DaysFactor"].ToString());                             // 總日基數
            //    _ms.mMonthFactor = double.Parse(dr["MonthFactor"].ToString());                           // 總月基數
            //    _ms.mDutyDateCount = double.Parse(dr["DutyDateCount"].ToString());                       // 總出勤記錄數
            //    _ms.mLeaveDate = (dr["LeaveDate"] == null || dr["LeaveDate"].ToString() == "") ? global::Helper.DateTimeParse.NullDate : Convert.ToDateTime(dr["LeaveDate"].ToString()); // 离职日期
            //    _ms.mPunishLeaveCount = double.Parse(dr["PunishLeaveCount"].ToString());                 // 倒扣款假總數
            //    _ms.mLeaveCount = double.Parse(dr["LeaveCount"].ToString());                             // 請假總數
            //    _ms.mAbsentCount = double.Parse(dr["AbsentCount"].ToString());                           // 曠工總數
            //    _ms.mTotalHoliday = double.Parse(dr["TotalHoliday"].ToString());                         // 該月總例假數
            //    _ms.mLoanFee = double.Parse(dr["LoanFee"].ToString());
            //    // int WuXinCount = Int32.Parse(dr["WuXinCount"].ToString());
            //    //考勤 不满一月  日基数 =月基数-实际假数-扣款请假天数-旷职-无薪假     //矿工待处理  
            //    if (_ms.mDutyDateCount < totalDay)
            //        _ms.mDaysFactor = _ms.mDaysFactor - _ms.mTotalHoliday;
            //    //if (_ms.mDutyDateCount < totalDay && _ms.mLeaveDate != global::Helper.DateTimeParse.NullDate && _ms.mLeaveDate.ToString() != "")             //總出勤記錄數
            //    //    _ms.mDaysFactor = _ms.mMonthFactor - _ms.mTotalHoliday - _ms.mPunishLeaveCount - _ms.mAbsentCount - WuXinCount;
            //    //else if ((_ms.mLeaveDate == global::Helper.DateTimeParse.NullDate || _ms.mLeaveDate.ToString() == "") && _ms.mDutyDateCount < totalDay)
            //    //    _ms.mDaysFactor = _ms.mMonthFactor - _ms.mTotalHoliday - _ms.mPunishLeaveCount - _ms.mAbsentCount - WuXinCount;


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

            //#endregion
            //#region 底薪
            //DataSet dx_ds = this.monthlySalaryManager.getMonthlySalary(emp.EmployeeId, _ms.mIdentifyDate);
            //if (dx_ds.Tables[0].Rows.Count > 0)
            //{
            //    _ms.mMonthFactor = _ms.mMonthFactor;
            //    DataRow dx_dr = dx_ds.Tables[0].Rows[0];
            //    _ms.mDailyPay = double.Parse(dx_dr["DailyPay"].ToString());                                         //日工资
            //    _ms.mMonthlyPay = double.Parse(dx_dr["MonthlyPay"].ToString());                                     //月工资
            //    _ms.mDutyPay = double.Parse(dx_dr["DutyPay"].ToString()) * _ms.mMonthFactor / totalDay;                        //责任津贴
            //    _ms.mPostPay = double.Parse(dx_dr["PostPay"].ToString()) * _ms.mMonthFactor / totalDay;                        //職務津貼
            //    _ms.mInsurance = double.Parse(dx_dr["insurance"].ToString());                                       //保险费
            //    _ms.mTax = double.Parse(dx_dr["Tax"].ToString());                                                   //所得税
            //    _ms.mEffectFactor = double.Parse(dx_dr["EffectFactor"] == null || dx_dr["EffectFactor"].ToString() == "" ? "0" : dx_dr["EffectFactor"].ToString());                                 //績效係數
            //    _ms.mOtherPay = double.Parse(dx_dr["OtherPay"] == null || dx_dr["OtherPay"].ToString() == "" ? "0" : dx_dr["OtherPay"].ToString());                                         //其它補款
            //    _ms.mOtherPunish = double.Parse(dx_dr["OtherPunish"] == null || dx_dr["OtherPunish"].ToString() == "" ? "0" : dx_dr["OtherPunish"].ToString());                                   //其它扣款
            //    _ms.mFieldPay = double.Parse(dx_dr["FieldPay"] == null || dx_dr["FieldPay"].ToString() == "" ? "0" : dx_dr["FieldPay"].ToString()) * (totalDay - _ms.mCount) / totalDay;    //职场津贴
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

            //#endregion
            #endregion //------------------------------备注-----------------------------------------------------------//
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

        /// <summary>
        /// 对象转Double
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private double mStrToDouble(object o)
        {
            return double.Parse(string.IsNullOrEmpty(o.ToString()) ? "0" : o.ToString());
        }
    }
}

#region =========注释备用=======
//     DataRow dr;//内存表
//     DataRow row; //monthlySalary薪资表
//     DataRow row1;// 薪资统计表中的行 只有1行
//    DataSet  dataset = this.monthlySalaryManager.GetMonthlySummaryByMonth(hryear, hrmonth);

//    Model.Employee employees;

//    decimal PunishLeaveCount = 0;

//    if (dataset.Tables[0].Rows.Count>0)
//    {
//        for(int j=0;j<dataset.Tables[0].Rows.Count;j++)
//        {
//            row = dataset.Tables[0].Rows[j];
//            employees = this.employeeManager.Get(row["EmployeeId"].ToString());

//            _hrAttendStat = this.hrAttendManager.SelectHrAttendStatByEmpidAndYearMonth(employees, hryear, hrmonth);//考勤统计表
//            row1 = this.monthlySalaryManager.GetMonthlySummaryFee(employees.EmployeeId, hryear, hrmonth).Tables[0].Rows[0] ;

//         //   monthFactor = this._hrManager.SelectDayMonthSum(hryear, hrmonth, employees);


//            dr = table.NewRow();
//            if (_hrAttendStat != null)   //考勤统计表
//            {
//                dr["LoanFee"] = this._hrAttendStat.LoanFee == null ? 0 : this._hrAttendStat.LoanFee.Value;

//                dr["LunchFee"] = this._hrAttendStat.LunchFee == null ? 0 : this._hrAttendStat.LunchFee.Value;

//                dr["OverTimeFee"] = this._hrAttendStat.OverTimeFee == null ? 0 : this._hrAttendStat.OverTimeFee.Value;
//                //row1["OverTimeFee"] == null || row1["OverTimeFee"].ToString() == "" ? 0 : decimal.Parse(row1["OverTimeFee"].ToString());
//                dr["OverTimeBonus"] = this._hrAttendStat.OverTimeBonus == null ? 0 : this._hrAttendStat.OverTimeBonus.Value;//row1["OverTimeBonus"] == null || row1["OverTimeBonus"].ToString() == "" ? 0 : decimal.Parse(row1["OverTimeBonus"].ToString()); 
//                dr["SpecialBonus"] = this._hrAttendStat.SpecialBonus == null ? 0 : this._hrAttendStat.SpecialBonus.Value; //row1["SpecialBonus"] == null || row1["SpecialBonus"].ToString() == "" ? 0 : decimal.Parse(row1["SpecialBonus"].ToString());
//                row1["DutyDateCount"] = this._hrAttendStat.DutyDateCount == null ? 0 : this._hrAttendStat.DutyDateCount.Value;
//                row1["TotalHoliday"] = this._hrAttendStat.TotalHoliday == null ? 0 : this._hrAttendStat.TotalHoliday.Value;
//                row1["AbsentCount"] = this._hrAttendStat.AbsentCount == null ? 0 : this._hrAttendStat.AbsentCount.Value;
//            }

//            dr["BoundTitle"] = hryear + "年" + hrmonth + "月獎金明細表";
//            dr["SalaryTitle"] = hryear + "年" + hrmonth + "月薪資單";
//        dr["CompanyName"] = BL.Settings.CompanyChineseName;
//        dr["EmployeeName"] = employees.EmployeeName;
//        dr["IDNo"] = employees.IDNo;       

//        dr["EffectFactor"] =row["EffectFactor"] == null || row["EffectFactor"].ToString() == ""?0:decimal.Parse(row["EffectFactor"].ToString());

//        //日基数
//        dr["DaysFactor"] = row1["DaysFactor"] == null || row1["DaysFactor"].ToString() == "" ? "0" : decimal.Parse(row1["DaysFactor"].ToString()).ToString("0.#");
//        //非离职出勤不到满月  排除例假数。请假数，旷职数

//        if (row1["DutyDateCount"] != null && row1["DutyDateCount"].ToString() != "" && decimal.Parse(row1["DutyDateCount"].ToString()) < DateTime.DaysInMonth(hryear, hrmonth))
//        {
//            dr["DaysFactor"] = decimal.Parse(row1["MonthFactor"].ToString()) - (row1["TotalHoliday"] == null || row1["TotalHoliday"].ToString() == "" ? 0 : decimal.Parse(row1["TotalHoliday"].ToString())) - (row1["LeaveCount"] == null || row1["LeaveCount"].ToString() == "" ? 0 : decimal.Parse(row1["LeaveCount"].ToString())) - (row1["AbsentCount"] == null || row1["AbsentCount"].ToString() == "" ? 0 : decimal.Parse(row1["AbsentCount"].ToString()));
//        }

//        //月基数
//        dr["MonthFactor"] = row1["MonthFactor"] == null || row1["MonthFactor"].ToString() == "" ? 0 : decimal.Parse(row1["MonthFactor"].ToString());

//        #region 底薪
//        // 倒扣款例假数


//         PunishLeaveCount = 0;//倒扣款例假總數

//        //小於兩天不扣
//        if (row1["PunishLeaveCount"] != null && row1["PunishLeaveCount"].ToString() != "" && decimal.Parse(row1["PunishLeaveCount"].ToString()) < 2)
//            PunishLeaveCount = 0;
//        //2-2.5天扣1個例假日，3-3.5扣2個例假日，4-4.5天扣3個例假日，5-5.5天扣4個例假日
//        else
//        {

//            row1["TotalHoliday"] = row1["TotalHoliday"] == null || row1["TotalHoliday"].ToString() == "" ? 0 : row1["TotalHoliday"];

//            if (row1["PunishLeaveCount"] != null && row1["PunishLeaveCount"].ToString() != "" && decimal.Parse(row1["PunishLeaveCount"].ToString()) - decimal.Parse("1.05") >=    decimal.Parse( row1["TotalHoliday"].ToString()))
//                PunishLeaveCount = decimal.Parse(row1["TotalHoliday"].ToString());
//            else
//                PunishLeaveCount = decimal.Parse(row1["TotalHoliday"].ToString()) - decimal.Parse("1.05");
//        }
//        dr["DaysFactor"] = decimal.Parse(dr["DaysFactor"].ToString()) - PunishLeaveCount;
//        //底薪=月薪/当前月天数*月基数 + 日薪*（日基数-旷职日数-扣例假数）
//        dr["BasePay"] = employees.MonthlyPay.Value / DateTime.DaysInMonth(hryear, hrmonth) * decimal.Parse(dr["MonthFactor"].ToString()) + employees.DailyPay.Value * (decimal.Parse(dr["DaysFactor"].ToString()));

//        #endregion
//        dr["GivenDays"] = row["HolidayBonusGivenDays"] == null||row["HolidayBonusGivenDays"].ToString()=="" ? 0 : decimal.Parse( row["HolidayBonusGivenDays"].ToString());
//        dr["OtherPay"] = row["OtherPay"] == null || row["OtherPay"].ToString() == "" ? 0 :decimal.Parse( row["OtherPay"].ToString());
//        dr["OtherPunish"] = row["OtherPunish"] == null || row["OtherPunish"].ToString() == "" ? 0 : decimal.Parse(row["OtherPunish"].ToString());

//        dr["PostPay"] = employees.PostPay == null ? 0 : employees.PostPay.Value;
//        dr["DutyPay"] = employees.DutyPay == null ? 0 : employees.DutyPay.Value;
//        dr["DailyPay"] = employees.DailyPay == null ? 0 : employees.DailyPay.Value;
//        dr["MonthlyPay"] = employees.MonthlyPay == null ? 0 : employees.MonthlyPay.Value;
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
//       // decimal AllAttendBonus = decimal.Zero;

//        if (row1["DutyDateCount"] != null && row1["DutyDateCount"].ToString() != "" && decimal.Parse(row1["DutyDateCount"].ToString()) == DateTime.DaysInMonth(hryear, hrmonth))
//        {

//            //有做滿一個月：日薪三天
//            if (decimal.Parse( dr["MonthFactor"].ToString()) == DateTime.DaysInMonth(hryear, hrmonth))
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
//             dr["AllAttendBonus"] = 0;
//        }
//        #endregion
//       // dr["WorkBonus"] = 1000;
//        dr["EffectBonus"] = 1000;


//        #region //加班
//        DataSet overtimeData = this.OverTimeManger.SelectOverTimeInfoByEmployeeId(employees.EmployeeId, new DateTime(hryear, hrmonth, 01));



//    decimal NormalHour=decimal.Zero;
//    decimal NormalFee=decimal.Zero;
//    decimal HolidayCount=decimal.Zero;
//    decimal HolidayFee=decimal.Zero;
//       for (int i = 0; i < overtimeData.Tables[0].Rows.Count; i++)
//       {
//           //假日加班          
//          if ((bool)overtimeData.Tables[0].Rows[i][Model.OverTime.PROPERTY_ISHOLIDAY])
//          {
//              HolidayFee += decimal.Parse(overtimeData.Tables[0].Rows[i][Model.OverTime.PROPERTY_OVERTIMEFEE].ToString());
//              HolidayCount += decimal.Parse(overtimeData.Tables[0].Rows[i][Model.OverTime.PROPERTY_EOVERTIME].ToString());
//          }
//          else//平日加班
//          { 
//             NormalHour+=decimal.Parse( overtimeData.Tables[0].Rows[i][Model.OverTime.PROPERTY_EOVERTIME].ToString());
//             NormalFee += decimal.Parse(overtimeData.Tables[0].Rows[i][Model.OverTime.PROPERTY_OVERTIMEFEE].ToString());
//          }

//       }
//       dr["HolidayOverTimeFee"] = HolidayFee;
//       dr["HolidayOverTime"] =HolidayCount;
//       dr["GeneralOverTime"] =NormalHour;
//       dr["GeneralOverTimeFee"]=NormalFee;


//        #endregion
//         dr["AnnualHolidayFee"] = 0;


//        dr["LatePunish"] = 0;
//        dr["DailyPay"] = employees.DailyPay == null ? 0 : employees.DailyPay.Value;
//        dr["Insurance"] = employees.Insurance == null ? 0 : employees.Insurance.Value;
//       // dr["LoanFee"] = row1["LoanFee"] == null || row1["LoanFee"].ToString() == "" ? 0 : decimal.Parse(row1["LoanFee"].ToString()); // this.loandetailManage.SelectFeeSum(employees, hryear, hrmonth);
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

//}
#endregion