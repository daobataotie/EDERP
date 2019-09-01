//------------------------------------------------------------------------------
//
// file name：EmployeeManager.cs
// author: peidun
// create date：2008/6/6 10:00:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.Employee.
    /// </summary>
    public partial class EmployeeManager : BaseManager
    {
        private static readonly DA.IFamilyMembersAccessor familyMembersAccessor = (DA.IFamilyMembersAccessor)Accessors.Get("FamilyMembersAccessor");
        MonthlySalaryManager monthlySalaryManager = new MonthlySalaryManager();

        /// <summary>
        /// Delete Employee by primary key.
        /// </summary>
        public void Delete(string employeeId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(employeeId);
        }

        public void Delete(Model.Employee employee)
        {
            this.Delete(employee.EmployeeId);
        }

        /// <summary>
        /// Insert a Employee.
        /// </summary>
        public void Insert(Model.Employee employee)
        {
            Validate_add(employee);
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                employee.InsertTime = DateTime.Now;
                employee.UpdateTime = DateTime.Now;
                //employee.RoleId = employee.Role.RoleId;
                _Insert(employee);

                string invoiceKind = "emp";

                DateTime now = DateTime.Now;

                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, now.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, now.Year, now.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, now.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                //往薪资表插入数据
                Model.MonthlySalary monthlySalary = new Book.Model.MonthlySalary();
                monthlySalary.MonthlySalaryId = Guid.NewGuid().ToString();
                monthlySalary.IdentifyDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM") + "-1");
                monthlySalary.EmployeeId = employee.EmployeeId;
                monthlySalaryManager.Insert(monthlySalary);

                Model.MonthlySalary monthlySalary2 = new Book.Model.MonthlySalary();
                monthlySalary2.MonthlySalaryId = Guid.NewGuid().ToString();
                monthlySalary2.IdentifyDate = Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToString("yyyy-MM") + "-1");
                monthlySalary2.EmployeeId = employee.EmployeeId;
                monthlySalaryManager.Insert(monthlySalary2);

                BL.V.CommitTransaction();
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();
                throw ex;
            }
        }

        private static void _Insert(Model.Employee employee)
        {
            if (employee.AcademicBackGround != null)
                employee.AcademicBackGroundId = employee.AcademicBackGround.AcademicBackGroundId;
            if (employee.Bank != null)
                employee.BankId = employee.Bank.BankId;
            if (employee.BusinessHours != null)
                employee.BusinessHoursId = employee.BusinessHours.BusinessHoursId;
            if (employee.Company != null)
                employee.CompanyId = employee.Company.CompanyId;
            if (employee.Department != null)
                employee.DepartmentId = employee.Department.DepartmentId;
            if (employee.Duty != null)
                employee.DutyId = employee.Duty.DutyId;
            accessor.Insert(employee);

            foreach (Model.FamilyMembers member in employee.FamilyMembers)
            {
                member.EmployeeId = employee.EmployeeId;
                familyMembersAccessor.Insert(member);
            }
        }

        /// <summary>
        /// Update a Employee.
        /// </summary>
        public void Update(Model.Employee employee)
        {
            Validate_update(employee);
            try
            {
                BL.V.BeginTransaction();
                //this.Delete(employee.EmployeeId);
                employee.UpdateTime = DateTime.Now;
                DateTime? joinOld = accessor.Get(employee.EmployeeId).EmployeeJoinDate;
                DateTime? limitDate;  //最后考勤日
                if (DateTime.Now >= DateTime.Parse(DateTime.Now.ToShortDateString() + " 12:50"))
                {
                    limitDate = DateTime.Parse(DateTime.Now.AddDays(-1).ToShortDateString());
                }
                else
                {
                    limitDate = DateTime.Parse(DateTime.Now.AddDays(-2).ToShortDateString());
                }

                if (employee.EmployeeLeaveDate != null || joinOld.Value.Date != employee.EmployeeJoinDate.Value.Date)
                {
                    if (employee.EmployeeLeaveDate != null)  //删除对应的ERP账号
                    {
                        BL.OperatorsManager om = new OperatorsManager();
                        om.DeleteByEmployeeId(employee.EmployeeId);
                    }

                    //已經自動考勤過了，所以必需再重新考勤,到职日期调后,之前记录也删除
                    //如果 大于旧 到职日 并且旧的  已经考勤过  删除 新的之前  考勤记录

                    //if ((employee.EmployeeLeaveDate != null && limitDate >= employee.EmployeeLeaveDate.Value) || (joinOld.Value.Date != employee.EmployeeJoinDate.Value.Date && (employee.EmployeeJoinDate.Value.Date > joinOld.Value.Date && joinOld.Value.Date <= limitDate)))
                    //{
                    new BL.HrDailyEmployeeAttendInfoManager().DeleteForChangeLeaveDateEmpHrDay(employee);
                    //}

                    if (employee.EmployeeJoinDate.Value.Date < joinOld.Value.Date && employee.EmployeeJoinDate.Value.Date <= limitDate)
                    {
                        //如果 大于旧 到职日 并且 新的  已经考勤过   重新 中间 进行考勤 
                        TimeSpan t = joinOld.Value.Date - employee.EmployeeJoinDate.Value.Date;
                        for (int i = 0; i < t.Days; i++)
                            new BL.HrDailyEmployeeAttendInfoManager().Reatten_Controller(employee.EmployeeJoinDate.Value.Date.AddDays(i), employee);
                    }
                }
                employee.UpdateTime = DateTime.Now;
                accessor.Update(employee);
                //修改家属
                familyMembersAccessor.DeleteByEmployeeId(employee.EmployeeId);
                foreach (Model.FamilyMembers member in employee.FamilyMembers)
                {
                    member.EmployeeId = employee.EmployeeId;
                    familyMembersAccessor.Insert(member);
                }
                BL.V.CommitTransaction();
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();
                throw ex;
            }
        }

        public IList<Model.Employee> SelectOnActive()
        {
            //return accessor.SelectOnActive();

            IList<Model.Employee> atoH = new List<Model.Employee>();
            IList<Model.Employee> o = new List<Model.Employee>();
            IList<Model.Employee> itoZWithoutO = new List<Model.Employee>();

            atoH = accessor.SelectOnActiveAtoH();
            o = accessor.SelectOnActiveO();
            itoZWithoutO = accessor.SelectOnActiveItoZWithoutO();

            return atoH.Union(o).Union(itoZWithoutO).ToList();
        }

        public IList<Model.Employee> SelectLeaveActive()
        {
            return SortAToOIToZ(accessor.SelectLeaveJob());
        }

        void Validate_add(Model.Employee employee)
        {
            if (this.ExistsExceptInsert(employee))
            {
                throw new Helper.InvalidValueException(Model.Employee.PROPERTY_IDNO);
            }
            #region 非空验证
            if (string.IsNullOrEmpty(employee.IDNo))
            {
                throw new Helper.RequireValueException(Model.Employee.PROPERTY_IDNO);
            }
            if (string.IsNullOrEmpty(employee.EmployeeName))
            {
                throw new Helper.RequireValueException(Model.Employee.PROPERTY_EMPLOYEENAME);
            }
            if (string.IsNullOrEmpty(employee.EmployeeGender.ToString()))
            {
                throw new Helper.RequireValueException(Model.Employee.PROPERTY_EMPLOYEEGENDER);
            }
            if (!employee.EmployeeJoinDate.HasValue)
            {
                throw new Helper.RequireValueException(Model.Employee.PROPERTY_EMPLOYEEJOINDATE);
            }
            if (employee.DepartmentId == null)
            {
                throw new Helper.RequireValueException(Model.Employee.PROPERTY_DEPARTMENTID);
            }
            #endregion
            //编号判断
            try
            {
                int.Parse(employee.IDNo.Substring(1, 2));
            }
            catch
            {
                throw new Helper.InvalidValueException(Model.Employee.PROPERTY_IDNO + "1");
            }

        }

        void Validate_update(Model.Employee employee)
        {
            if (this.ExistsExceptUpdate(employee))
            {
                throw new Helper.InvalidValueException(Model.Employee.PROPERTY_IDNO);
            }
            #region 非空验证
            if (string.IsNullOrEmpty(employee.IDNo))
            {
                throw new Helper.RequireValueException(Model.Employee.PROPERTY_IDNO);
            }
            if (string.IsNullOrEmpty(employee.EmployeeName))
            {
                throw new Helper.RequireValueException(Model.Employee.PROPERTY_EMPLOYEENAME);
            }
            if (string.IsNullOrEmpty(employee.EmployeeGender.ToString()))
            {
                throw new Helper.RequireValueException(Model.Employee.PROPERTY_EMPLOYEEGENDER);
            }
            if (!employee.EmployeeJoinDate.HasValue)
            {
                throw new Helper.RequireValueException(Model.Employee.PROPERTY_EMPLOYEEJOINDATE);
            }
            #endregion
            //编号判断
            try
            {
                int.Parse(employee.IDNo.Substring(1, 2));
            }
            catch
            {
                throw new Helper.InvalidValueException(Model.Employee.PROPERTY_IDNO + "1");
            }
        }

        /// <summary>
        /// 查找员工编号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Model.Employee GetOldIDbyEmpID(string id)
        {
            return accessor.GetOldIDbyEmpID(id);
        }

        public bool ExistsExceptUpdate(Model.Employee emp)
        {
            return accessor.ExistsExceptUpdate(emp);
        }

        /// <summary>
        /// 添加时验证是否存在
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        public bool ExistsExceptInsert(Model.Employee emp)
        {
            return accessor.ExistsExceptInsert(emp);
        }

        public Book.Model.Employee GetByOperatorName(string name)
        {
            return accessor.GetByOperatorName(name);
        }

        public Model.Employee GetbyIdNo(string idno)
        {
            return accessor.GetbyIdNo(idno);
        }

        //protected override string GetInvoiceKind()
        //{
        //    return "Employee";
        //}

        //protected override string GetSettingId()
        //{
        //    return "EmployeeNumberRule";
        //}

        /// <summary>
        /// Select by primary key.
        /// </summary>		
        public Model.Employee Get(string employeeId)
        {
            Model.Employee employee = accessor.Get(employeeId);
            if (employee != null)
            {
                employee.FamilyMembers = familyMembersAccessor.Select(employee);
            }
            return employee;
        }

        public IList<Model.Employee> Select(string _roleId)
        {
            return SortAToOIToZ(accessor.Select(_roleId));
        }

        public IList<Book.Model.Employee> Select(Model.Department department)
        {
            return SortAToOIToZ(accessor.Select(department));
        }

        public Book.Model.Employee SelectByCardNo(string CardNo, DateTime dt)
        {
            return accessor.SelectByCardNo(CardNo, dt);
        }

        /// <summary>
        /// 根據編號首字符查詢
        /// </summary>
        /// <param name="charno"></param>
        /// <returns></returns>
        public IList<Model.Employee> SelectbyIDsubstring(string charno)
        {
            return SortAToOIToZ(accessor.SelectbyIDsubstring(charno));
        }

        public IList<Model.Employee> SelectbyPinYin(string pinyin)
        {
            return SortAToOIToZ(accessor.SelectbyPinYin(pinyin));
        }

        public DataTable SelectPinyin()
        {
            return accessor.SelectPinyin();
        }

        public DataSet SelectOnActiveDataSet()
        {
            return accessor.SelectOnActiveDataSet();
        }

        public void UpdateDataDataSet(DataSet dataSet)
        {
            try
            {
                BL.V.BeginTransaction();
                accessor.UpdateDataDataSet(dataSet);
                BL.V.CommitTransaction();
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();
                throw ex;
            }
        }

        public DataSet SelectOnActiveDataSetByEmployeeId(string employeeId)
        {
            return accessor.SelectOnActiveDataSetByEmployeeId(employeeId);
        }

        public IList<Model.Employee> selectLeaverPayActive()
        {
            return SortAToOIToZ(accessor.selectLeaverPayActive());
        }

        public IList<Model.Employee> selectEmployeeSearch(DateTime rzbegin, DateTime rzend, DateTime lzbegin, DateTime lzend, string type, int f)
        {
            return SortAToOIToZ(accessor.selectEmployeeSearch(rzbegin, rzend, lzbegin, lzend, type, f));
        }

        /// <summary>
        /// 根据日前查询 符合 月份发薪资的人员
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public IList<Model.Employee> SelectHrDailyAttend(DateTime date)
        {
            var h = accessor.SelectHrDailyAttend(date);
            IList<Model.Employee> a = new List<Model.Employee>();
            IList<Model.Employee> o = new List<Model.Employee>();
            IList<Model.Employee> i = new List<Model.Employee>();

            a = accessor.SelectHrDailyAttendA(date);
            o = accessor.SelectHrDailyAttendO(date);
            i = accessor.SelectHrDailyAttendI(date);

            return a.Union(o).Union(i).ToList();
        }

        /// <summary>
        /// 根据年份查询 符合 當年发薪资的人员
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public IList<Model.Employee> SelectHrDailyAttendByMonth(DateTime date)
        {
            var h = accessor.SelectHrDailyAttendByMonth(date);
            IList<Model.Employee> a = new List<Model.Employee>();
            IList<Model.Employee> o = new List<Model.Employee>();
            IList<Model.Employee> i = new List<Model.Employee>();

            a = h.Where(E => (E.IDNo.Substring(0, 1).ToCharArray()[0] >= 'A' && E.IDNo.Substring(0, 1).ToCharArray()[0] <= 'H') || (E.IDNo.Substring(0, 1).ToCharArray()[0] >= 'a' && E.IDNo.Substring(0, 1).ToCharArray()[0] <= 'h')).ToList();
            o = h.Where(E => E.IDNo.Substring(0, 1).ToCharArray()[0] == 'O' || E.IDNo.Substring(0, 1).ToCharArray()[0] == 'o').ToList();
            i = h.Where(E => (E.IDNo.Substring(0, 1).ToCharArray()[0] >= 'I' && E.IDNo.Substring(0, 1).ToCharArray()[0] <= 'Z') || (E.IDNo.Substring(0, 1).ToCharArray()[0] >= 'i' && E.IDNo.Substring(0, 1).ToCharArray()[0] <= 'z') && E.IDNo.Substring(0, 1).ToCharArray()[0] != 'O' && E.IDNo.Substring(0, 1).ToCharArray()[0] != 'o').ToList();

            return a.Union(o).Union(i).ToList();
        }

        /// <summary>
        /// 更新出勤记录查出员工列表
        /// </summary>
        /// <param name="CheckDateTime"></param>
        /// <returns></returns>
        public IList<Model.Employee> DailyEmployeeAttendInfo_EmpList(DateTime CheckDateTime)
        {
            return SortAToOIToZ(accessor.DailyEmployeeAttendInfo_EmpList(CheckDateTime));
        }

        /// <summary>
        /// 获取选定日期内还在职员工列表
        /// </summary>
        /// <param name="mdate">截止日期</param>
        /// <returns></returns>
        public IList<Model.Employee> GetHasThereEmp_ListByDateTime(DateTime mdate)
        {
            return SortAToOIToZ(accessor.GetHasThereEmp_ListByDateTime(mdate));
        }

        public IList<Model.Employee> SelectUseERPer()
        {
            return SortAToOIToZ(accessor.SelectUseERPer());
        }

        public IList<Model.Employee> SelectAllEmployee()
        {
            return accessor.SelectAllEmployee();
        }

        /// <summary>
        /// 员工排序按照A-H,O,I-Z
        /// </summary>
        /// <param name="empList"></param>
        /// <returns></returns>
        private IList<Model.Employee> SortAToOIToZ(IList<Model.Employee> empList)
        {
            if (empList == null || empList.Count == 0)
                return empList;
            List<Model.Employee> a = empList.Where(d => Help.AToH.Contains(d.IDNo.Substring(0, 1).ToLower())).ToList();
            List<Model.Employee> o = empList.Where(d => d.IDNo.Substring(0, 1).ToLower() == "o").ToList();
            List<Model.Employee> i = empList.Where(d => Help.ItoZ.Contains(d.IDNo.Substring(0, 1).ToLower())).ToList();

            if (a == null)
                a = new List<Book.Model.Employee>();
            if (o == null)
                o = new List<Book.Model.Employee>();
            if (i == null)
                i = new List<Book.Model.Employee>();

            return a.Union(o).Union(i).ToList();
        }


        #region 分类构建
        public Model.Employee mGetFirst()
        {
            return accessor.mGetFirst();
        }

        public Model.Employee mGetLast()
        {
            return accessor.mGetLast();
        }

        public Model.Employee mGetPrev(Model.Employee emp)
        {
            return accessor.mGetPrev(emp);
        }

        public Model.Employee mGetNext(Model.Employee emp)
        {
            return accessor.mGetNext(emp);
        }

        public bool mHasRows()
        {
            return accessor.mHasRows();
        }

        public bool mHasRowsBefore(Model.Employee emp)
        {
            return accessor.mHasRowsBefore(emp);
        }

        public bool mHasRowsAfter(Model.Employee emp)
        {
            return accessor.mHasRowsAfter(emp);
        }
        #endregion

        public Model.Employee SelectIdByNameAnId(string name, string id)
        {
            return accessor.SelectIdByNameAnId(name, id);
        }

        public IList<Model.Employee> SelectIdAndName()
        {
            return accessor.SelectIdAndName();
        }
    }

    public static class Help
    {
        public static List<string> AToH = new List<string> { "a", "b", "c", "d", "e", "f", "g", "h" };

        public static List<string> ItoZ = new List<string> { "i", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
    }
}

