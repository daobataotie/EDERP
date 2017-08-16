//------------------------------------------------------------------------------
//
// file name:Employee.cs
// author: peidun
// create date:2008/6/6 10:00:36
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 员工
    /// </summary>
    [Serializable]
    public partial class Employee : IComparable
    {
        public override string ToString()
        {
            return this._employeeName;
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            if (obj is Employee)
            {
                Employee employee = (Employee)obj;
                return employee.EmployeeName.CompareTo(this.EmployeeName);
            }

            throw new ArgumentException("obj");
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj is Employee)
            {
                return (obj as Model.Employee)._employeeId == this._employeeId ? true : false;
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        private System.Collections.Generic.IList<Model.FamilyMembers> _familyMembers = new System.Collections.Generic.List<Model.FamilyMembers>();
        public System.Collections.Generic.IList<Model.FamilyMembers> FamilyMembers
        {
            set
            {
                _familyMembers = value;
            }
            get
            {
                return _familyMembers;
            }
        }

        public readonly static string PROPERTY_GENDERSEX = "GenderDesc";

        public string GenderDesc
        {
            get
            {
                string gender = string.Empty;
                switch (_employeeGender == null ? -1 : _employeeGender.Value)
                {
                    case 0:
                        gender = "女";
                        break;
                    case 1:
                        gender = "男";
                        break;
                    default:
                        break;
                }
                return gender;
            }
        }

        public readonly static string PROPERTY_BIRTHDAY = "GetBirThday";

        public string GetBirThday
        {
            get
            {
                string birthday = string.Empty;
                if (_employeeBirthday != null)
                    birthday = _employeeBirthday.Value.Year + "-" + _employeeBirthday.Value.Month.ToString().PadLeft(2, '0') + "-" + _employeeBirthday.Value.Day.ToString().PadLeft(2, '0');
                return birthday;
            }
        }

        public readonly static string PROPERTY_ISMARRIED = "IsMarried";

        public string IsMarried
        {
            get
            {
                string str = string.Empty;
                if (_employeeMarried == 0)
                    str = "已婚";
                else
                    str = "未婚";
                return str;
            }

        }

        public readonly static string PROPERTY_BLOODTYPEDESC = "BloodTypeDesc";

        public string BloodTypeDesc
        {
            get
            {
                string type = string.Empty;

                switch (_employeeBloodType == null ? -1 : _employeeBloodType.Value)
                {
                    case 0:
                        type = "A";
                        break;
                    case 1:
                        type = "B";
                        break;
                    case 2:
                        type = "O";
                        break;
                    case 3:
                        type = "AB";
                        break;
                    default:
                        break;
                }
                return type;
            }
        }

        public readonly static string PROPERTY_ISCHECKED = "IsChecked";

        private bool isChecked = false;
        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; }
        }

        /// <summary>
        /// 是否隔周休,文字表述
        /// </summary>
        public string HolidayInstitutionStr
        {
            get { return this._HolidayInstitution.HasValue && this._HolidayInstitution.Value ? "隔周休" : "不隔周休"; }
        }

        public string FYNumber { get; set; }

        public string QKStandard { get; set; }

        public string IDNo1
        {
            get
            {
                string s = string.Empty;
                if (!string.IsNullOrEmpty(EmployeeIdentityNO))
                {
                    s = EmployeeIdentityNO.Substring(0, 1);
                }
                return s;
            }
        }

        public string IDNo2
        {
            get
            {
                string s = string.Empty;
                if (!string.IsNullOrEmpty(EmployeeIdentityNO) && EmployeeIdentityNO.Length > 1)
                {
                    s = EmployeeIdentityNO.Substring(1, 1);
                }
                return s;
            }
        }

        public string IDNo3
        {
            get
            {
                string s = string.Empty;
                if (!string.IsNullOrEmpty(EmployeeIdentityNO) && EmployeeIdentityNO.Length > 2)
                {
                    s = EmployeeIdentityNO.Substring(2, 1);
                }
                return s;
            }
        }

        public string IDNo4
        {
            get
            {
                string s = string.Empty;
                if (!string.IsNullOrEmpty(EmployeeIdentityNO) && EmployeeIdentityNO.Length > 3)
                {
                    s = EmployeeIdentityNO.Substring(3, 1);
                }
                return s;
            }
        }

        public string IDNo5
        {
            get
            {
                string s = string.Empty;
                if (!string.IsNullOrEmpty(EmployeeIdentityNO) && EmployeeIdentityNO.Length > 4)
                {
                    s = EmployeeIdentityNO.Substring(4, 1);
                }
                return s;
            }
        }

        public string IDNo6
        {
            get
            {
                string s = string.Empty;
                if (!string.IsNullOrEmpty(EmployeeIdentityNO) && EmployeeIdentityNO.Length > 5)
                {
                    s = EmployeeIdentityNO.Substring(5, 1);
                }
                return s;
            }
        }

        public string IDNo7
        {
            get
            {
                string s = string.Empty;
                if (!string.IsNullOrEmpty(EmployeeIdentityNO) && EmployeeIdentityNO.Length > 6)
                {
                    s = EmployeeIdentityNO.Substring(6, 1);
                }
                return s;
            }
        }

        public string IDNo8
        {
            get
            {
                string s = string.Empty;
                if (!string.IsNullOrEmpty(EmployeeIdentityNO) && EmployeeIdentityNO.Length > 7)
                {
                    s = EmployeeIdentityNO.Substring(7, 1);
                }
                return s;
            }
        }

        public string IDNo9
        {
            get
            {
                string s = string.Empty;
                if (!string.IsNullOrEmpty(EmployeeIdentityNO) && EmployeeIdentityNO.Length > 8)
                {
                    s = EmployeeIdentityNO.Substring(8, 1);
                }
                return s;
            }
        }

        public string IDNo10
        {
            get
            {
                string s = string.Empty;
                if (!string.IsNullOrEmpty(EmployeeIdentityNO) && EmployeeIdentityNO.Length > 9)
                {
                    s = EmployeeIdentityNO.Substring(9, 1);
                }
                return s;
            }
        }

        /// <summary>
        /// 是否隔周休,文字表述
        /// </summary>
        public readonly static string PROPERTY_HOLIDAYINSTITUTIONSTR = "HolidayInstitutionStr";
    }
}
