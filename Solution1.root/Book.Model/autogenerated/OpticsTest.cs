﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：OpticsTest.autogenerated.cs
// author: mayanjun
// create date：2012-5-15 16:05:19
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    public partial class OpticsTest
    {
        #region Data

        /// <summary>
        /// 主键编号
        /// </summary>
        private string _opticsTestId;

        /// <summary>
        /// 主键编号2
        /// </summary>
        private string _pCPGOnlineCheckDetailId;

        /// <summary>
        /// 插入时间
        /// </summary>
        private DateTime? _insertTime;

        /// <summary>
        /// 修改时间
        /// </summary>
        private DateTime? _updateTime;

        /// <summary>
        /// 机器名称
        /// </summary>
        private string _machineName;

        /// <summary>
        /// LattrS
        /// </summary>
        private double? _lattrS;

        /// <summary>
        /// LattrC
        /// </summary>
        private double? _lattrC;

        /// <summary>
        /// LattrA
        /// </summary>
        private double? _lattrA;

        /// <summary>
        /// LinPSM
        /// </summary>
        private double? _linPSM;

        /// <summary>
        /// LoutPSM
        /// </summary>
        private double? _loutPSM;

        /// <summary>
        /// LupPSM
        /// </summary>
        private double? _lupPSM;

        /// <summary>
        /// LdownPSM
        /// </summary>
        private double? _ldownPSM;

        /// <summary>
        /// RattrS
        /// </summary>
        private double? _rattrS;

        /// <summary>
        /// RattrC
        /// </summary>
        private double? _rattrC;

        /// <summary>
        /// RattrA
        /// </summary>
        private double? _rattrA;

        /// <summary>
        /// RinPSM
        /// </summary>
        private double? _rinPSM;

        /// <summary>
        /// RoutPSM
        /// </summary>
        private double? _routPSM;

        /// <summary>
        /// RupPSM
        /// </summary>
        private double? _rupPSM;

        /// <summary>
        /// RdowmPSM
        /// </summary>
        private double? _rdowmPSM;

        /// <summary>
        /// Condition
        /// </summary>
        private string _condition;

        /// <summary>
        /// 
        /// </summary>
        private DateTime? _optiscTestDate;

        /// <summary>
        /// 
        /// </summary>
        private string _employeeId;

        /// <summary>
        /// 人工手动编号
        /// </summary>
        private string _manualId;

        /// <summary>
        /// 组装成品检验单编号
        /// </summary>
        private string _pCFinishCheckId;

        private string _leftLevelJudge;

        private string _rightLevelJudge;

        private string _leftVerticalJudge;

        private string _rightVerticalJudge;


        private string _pCFirstOnlineCheckDetailId;


        /// <summary>
        /// 品管线上检查详细
        /// </summary>
        private PCPGOnlineCheckDetail _pCPGOnlineCheckDetail;

        /// <summary>
        /// 员工
        /// </summary>
        private Employee _employee;

        #endregion

        #region Properties

        /// <summary>
        /// 主键编号
        /// </summary>
        public string OpticsTestId
        {
            get
            {
                return this._opticsTestId;
            }
            set
            {
                this._opticsTestId = value;
            }
        }

        /// <summary>
        /// 主键编号2
        /// </summary>
        public string PCPGOnlineCheckDetailId
        {
            get
            {
                return this._pCPGOnlineCheckDetailId;
            }
            set
            {
                this._pCPGOnlineCheckDetailId = value;
            }
        }

        /// <summary>
        /// 插入时间
        /// </summary>
        public DateTime? InsertTime
        {
            get
            {
                return this._insertTime;
            }
            set
            {
                this._insertTime = value;
            }
        }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateTime
        {
            get
            {
                return this._updateTime;
            }
            set
            {
                this._updateTime = value;
            }
        }

        /// <summary>
        /// 机器名称
        /// </summary>
        public string MachineName
        {
            get
            {
                return this._machineName;
            }
            set
            {
                this._machineName = value;
            }
        }

        /// <summary>
        /// LattrS
        /// </summary>
        public double? LattrS
        {
            get
            {
                return this._lattrS;
            }
            set
            {
                this._lattrS = value;
            }
        }

        /// <summary>
        /// LattrC
        /// </summary>
        public double? LattrC
        {
            get
            {
                return this._lattrC;
            }
            set
            {
                this._lattrC = value;
            }
        }

        /// <summary>
        /// LattrA
        /// </summary>
        public double? LattrA
        {
            get
            {
                return this._lattrA;
            }
            set
            {
                this._lattrA = value;
            }
        }

        /// <summary>
        /// 水平坐標(左)
        /// </summary>
        public double? LinPSM
        {
            get
            {
                return this._linPSM;
            }
            set
            {
                this._linPSM = value;
            }
        }

        /// <summary>
        /// LoutPSM
        /// </summary>
        public double? LoutPSM
        {
            get
            {
                return this._loutPSM;
            }
            set
            {
                this._loutPSM = value;
            }
        }

        /// <summary>
        /// 垂直坐標(左)
        /// </summary>
        public double? LupPSM
        {
            get
            {
                return this._lupPSM;
            }
            set
            {
                this._lupPSM = value;
            }
        }

        /// <summary>
        /// LdownPSM
        /// </summary>
        public double? LdownPSM
        {
            get
            {
                return this._ldownPSM;
            }
            set
            {
                this._ldownPSM = value;
            }
        }

        /// <summary>
        /// RattrS
        /// </summary>
        public double? RattrS
        {
            get
            {
                return this._rattrS;
            }
            set
            {
                this._rattrS = value;
            }
        }

        /// <summary>
        /// RattrC
        /// </summary>
        public double? RattrC
        {
            get
            {
                return this._rattrC;
            }
            set
            {
                this._rattrC = value;
            }
        }

        /// <summary>
        /// RattrA
        /// </summary>
        public double? RattrA
        {
            get
            {
                return this._rattrA;
            }
            set
            {
                this._rattrA = value;
            }
        }

        /// <summary>
        /// RinPSM
        /// </summary>
        public double? RinPSM
        {
            get
            {
                return this._rinPSM;
            }
            set
            {
                this._rinPSM = value;
            }
        }

        /// <summary>
        /// RoutPSM
        /// </summary>
        public double? RoutPSM
        {
            get
            {
                return this._routPSM;
            }
            set
            {
                this._routPSM = value;
            }
        }

        /// <summary>
        /// RupPSM
        /// </summary>
        public double? RupPSM
        {
            get
            {
                return this._rupPSM;
            }
            set
            {
                this._rupPSM = value;
            }
        }

        /// <summary>
        /// RdowmPSM
        /// </summary>
        public double? RdowmPSM
        {
            get
            {
                return this._rdowmPSM;
            }
            set
            {
                this._rdowmPSM = value;
            }
        }

        /// <summary>
        /// Condition
        /// </summary>
        public string Condition
        {
            get
            {
                return this._condition;
            }
            set
            {
                this._condition = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? OptiscTestDate
        {
            get
            {
                return this._optiscTestDate;
            }
            set
            {
                this._optiscTestDate = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string EmployeeId
        {
            get
            {
                return this._employeeId;
            }
            set
            {
                this._employeeId = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ManualId
        {
            get
            {
                return this._manualId;
            }
            set
            {
                this._manualId = value;
            }
        }

        /// <summary>
        /// 组装成品检验单编号
        /// </summary>
        public string PCFinishCheckId
        {
            get { return _pCFinishCheckId; }
            set { _pCFinishCheckId = value; }
        }

        /// <summary>
        /// 水平判定L
        /// </summary>
        public string LeftLevelJudge
        {
            get { return _leftLevelJudge; }
            set { _leftLevelJudge = value; }
        }

        /// <summary>
        /// 水平判定R
        /// </summary>
        public string RightLevelJudge
        {
            get { return _rightLevelJudge; }
            set { _rightLevelJudge = value; }
        }

        /// <summary>
        /// 垂直判定L
        /// </summary>
        public string LeftVerticalJudge
        {
            get { return _leftVerticalJudge; }
            set { _leftVerticalJudge = value; }
        }

        /// <summary>
        /// 垂直判定R
        /// </summary>
        public string RightVerticalJudge
        {
            get { return _rightVerticalJudge; }
            set { _rightVerticalJudge = value; }
        }

        /// <summary>
        /// 首件上线检查表明细Id
        /// </summary>
        public string PCFirstOnlineCheckDetailId
        {
            get { return _pCFirstOnlineCheckDetailId; }
            set { _pCFirstOnlineCheckDetailId = value; }
        }

        /// <summary>
        /// 品管线上检查详细
        /// </summary>
        public virtual PCPGOnlineCheckDetail PCPGOnlineCheckDetail
        {
            get
            {
                return this._pCPGOnlineCheckDetail;
            }
            set
            {
                this._pCPGOnlineCheckDetail = value;
            }

        }
        /// <summary>
        /// 员工
        /// </summary>
        public virtual Employee Employee
        {
            get
            {
                return this._employee;
            }
            set
            {
                this._employee = value;
            }

        }
        /// <summary>
        /// 主键编号
        /// </summary>
        public readonly static string PRO_OpticsTestId = "OpticsTestId";

        /// <summary>
        /// 主键编号2
        /// </summary>
        public readonly static string PRO_PCPGOnlineCheckDetailId = "PCPGOnlineCheckDetailId";

        /// <summary>
        /// 插入时间
        /// </summary>
        public readonly static string PRO_InsertTime = "InsertTime";

        /// <summary>
        /// 修改时间
        /// </summary>
        public readonly static string PRO_UpdateTime = "UpdateTime";

        /// <summary>
        /// 机器名称
        /// </summary>
        public readonly static string PRO_MachineName = "MachineName";

        /// <summary>
        /// LattrS
        /// </summary>
        public readonly static string PRO_LattrS = "LattrS";

        /// <summary>
        /// LattrC
        /// </summary>
        public readonly static string PRO_LattrC = "LattrC";

        /// <summary>
        /// LattrA
        /// </summary>
        public readonly static string PRO_LattrA = "LattrA";

        /// <summary>
        /// LinPSM
        /// </summary>
        public readonly static string PRO_LinPSM = "LinPSM";

        /// <summary>
        /// LoutPSM
        /// </summary>
        public readonly static string PRO_LoutPSM = "LoutPSM";

        /// <summary>
        /// LupPSM
        /// </summary>
        public readonly static string PRO_LupPSM = "LupPSM";

        /// <summary>
        /// LdownPSM
        /// </summary>
        public readonly static string PRO_LdownPSM = "LdownPSM";

        /// <summary>
        /// RattrS
        /// </summary>
        public readonly static string PRO_RattrS = "RattrS";

        /// <summary>
        /// RattrC
        /// </summary>
        public readonly static string PRO_RattrC = "RattrC";

        /// <summary>
        /// RattrA
        /// </summary>
        public readonly static string PRO_RattrA = "RattrA";

        /// <summary>
        /// RinPSM
        /// </summary>
        public readonly static string PRO_RinPSM = "RinPSM";

        /// <summary>
        /// RoutPSM
        /// </summary>
        public readonly static string PRO_RoutPSM = "RoutPSM";

        /// <summary>
        /// RupPSM
        /// </summary>
        public readonly static string PRO_RupPSM = "RupPSM";

        /// <summary>
        /// RdowmPSM
        /// </summary>
        public readonly static string PRO_RdowmPSM = "RdowmPSM";

        /// <summary>
        /// Condition
        /// </summary>
        public readonly static string PRO_Condition = "Condition";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_OptiscTestDate = "OptiscTestDate";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_EmployeeId = "EmployeeId";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_ManualId = "ManualId";

        /// <summary>
        /// 组装成品检验单编号
        /// </summary>
        public readonly static string PRO_PCFinishCheckId = "PCFinishCheckId";

        public readonly static string PRO_LeftLevelJudge = "LeftLevelJudge";

        public readonly static string PRO_RightLevelJudge = "RightLevelJudge";

        public readonly static string PRO_LeftVerticalJudge = "LeftVerticalJudge";

        public readonly static string PRO_RightVerticalJudge = "RightVerticalJudge";

        public readonly static string PRO_PCFirstOnlineCheckDetailId = "PCFirstOnlineCheckDetailId";
        #endregion
    }
}
