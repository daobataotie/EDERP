//------------------------------------------------------------------------------
//
// file name：AtSummonManager.cs
// author: mayanjun
// create date：2010-11-24 09:40:42
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AtSummon.
    /// </summary>
    public partial class AtSummonManager : BaseManager
    {
        BL.AtSummonDetailManager _atSummonDetailManager = new AtSummonDetailManager();

        public void Delete(string summonId)
        {
            accessor.Delete(summonId);
        }

        public void Delete(Model.AtSummon atSummon)
        {
            try
            {
                BL.V.BeginTransaction();

                _atSummonDetailManager.DeleteByHeadId(atSummon.SummonId);

                this.Delete(atSummon.SummonId);

                //2020年9月9日23:52:04：删除不扣减编号，在新增的时候自动补足删除编号
                //string invoiceKind = this.GetInvoiceKind().ToLower();
                //string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, atSummon.SummonDate.Value.Year);
                //string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, atSummon.SummonDate.Value.Year, atSummon.SummonDate.Value.Month);
                //string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, atSummon.SummonDate.Value.ToString("yyyy-MM-dd"));
                //string sequencekey = string.Format(invoiceKind);

                //SequenceManager.Decrement(sequencekey_y);
                //SequenceManager.Decrement(sequencekey_m);
                //SequenceManager.Decrement(sequencekey_d);
                //SequenceManager.Decrement(sequencekey);

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public Model.AtSummon GetDetails(string SummonId)
        {
            Model.AtSummon AtSummon = accessor.Get(SummonId);
            if (AtSummon != null)
                AtSummon.Details = _atSummonDetailManager.Select(AtSummon);
            return AtSummon;
        }

        public void Insert(Model.AtSummon atSummon)
        {
            if (atSummon.SummonId == null)
                atSummon.SummonId = Guid.NewGuid().ToString();
            Validate(atSummon);
            try
            {
                atSummon.InsertTime = DateTime.Now;
                atSummon.UpdateTime = DateTime.Now;
                BL.V.BeginTransaction();
                TiGuiExists(atSummon);
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, atSummon.SummonDate.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, atSummon.SummonDate.Value.Year, atSummon.SummonDate.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, atSummon.SummonDate.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);


                accessor.Insert(atSummon);

                foreach (Model.AtSummonDetail atSummonDetail in atSummon.Details)
                {
                    //if (atSummonDetail.SummonDetailId == null)
                    //    continue;
                    if (atSummonDetail.Lending == null || atSummonDetail.SubjectId == null)
                    {
                        throw new global::Helper.MessageValueException("請輸入傳票詳細資料！！");
                    }
                    if (string.IsNullOrEmpty(atSummonDetail.SummonDetailId))
                        atSummonDetail.SummonDetailId = Guid.NewGuid().ToString();

                    atSummonDetail.InsertTime = DateTime.Now;
                    atSummonDetail.SummonCatetory = atSummon.SummonCategory;
                    atSummonDetail.BillCode = atSummon.BIllCode;
                    atSummonDetail.SummonId = atSummon.SummonId;

                    _atSummonDetailManager.Insert(atSummonDetail);
                }

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public void Update(Model.AtSummon atSummon)
        {
            Validate(atSummon);
            try
            {
                BL.V.BeginTransaction();
                if (atSummon != null)
                {
                    _atSummonDetailManager.DeleteByHeadId(atSummon.SummonId);
                    foreach (Model.AtSummonDetail d in atSummon.Details)
                    {
                        if (d.Lending == null || d.SubjectId == null)
                            throw new global::Helper.MessageValueException("請輸入傳票詳細資料！！");
                        d.SummonId = atSummon.SummonId;
                        d.UpdateTime = DateTime.Now;
                        _atSummonDetailManager.Insert(d);
                    }

                    atSummon.UpdateTime = DateTime.Now;
                    accessor.Update(atSummon);
                }
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        protected override string GetSettingId()
        {
            return "atsRule";
        }

        protected override string GetInvoiceKind()
        {
            return "ats";
        }

        private void Validate(Model.AtSummon atSummon)
        {
            if (string.IsNullOrEmpty(atSummon.Id))
            {
                throw new Helper.RequireValueException(Model.AtSummon.PRO_Id);
            }
            if (string.IsNullOrEmpty(atSummon.SummonCategory))
            {
                throw new Helper.RequireValueException(Model.AtSummon.PRO_SummonCategory);
            }
        }


        public IList<Model.AtSummon> SelectByDateRage(DateTime startdate, DateTime enddate)
        {
            return accessor.SelectByDateRage(startdate, enddate);
        }

        public void TiGuiExists(Model.AtSummon model)
        {
            if (this.Exists(model.Id))
            {
                //设置KEY值
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, model.SummonDate.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, model.SummonDate.Value.Year, model.SummonDate.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, model.SummonDate.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                model.Id = this.GetConsecutiveId(model.SummonDate.Value);
                TiGuiExists(model);
                //throw new Helper.InvalidValueException(Model.Product.PRO_Id);               
            }

        }

        public void TiGuiExistsForUpdate(Model.AtSummon model)
        {
            if (IsExistsIdUpdate(model))
            {
                //设置KEY值
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, model.SummonDate.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, model.SummonDate.Value.Year, model.SummonDate.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, model.SummonDate.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                model.Id = this.GetConsecutiveId(model.SummonDate.Value);
                TiGuiExistsForUpdate(model);
            }

        }
        //public override string GetId(DateTime dateTime)
        //{
        //    string a=base.GetId(dateTime);
        //    return a.Substring(0, 6) + dateTime.Date.Day.ToString("d2") + a.Substring(6);
        //} 


        public bool IsExistsId(string Id)
        {
            return accessor.IsExistsId(Id);
        }

        public bool IsExistsIdUpdate(Model.AtSummon model)
        {
            return accessor.IsExistsIdUpdate(model);
        }

        public IList<Model.AtSummon> SelectByCondition(DateTime startDate, DateTime endDate, string startId, string endId, string StartSubjectId, string EndSubjectId)
        {
            return accessor.SelectByCondition(startDate, endDate, startId, endId, StartSubjectId, EndSubjectId);
        }

        public Model.AtSummon GetByInvoiceCGId(string invoiceCGId)
        {
            return accessor.GetByInvoiceCGId(invoiceCGId);
        }

        public Model.AtSummon GetByInvoiceXSId(string invoiceXSId)
        {
            return accessor.GetByInvoiceXSId(invoiceXSId);
        }

        public IList<Model.AtSummon> GetByShouldPayAccountId(string shouldPayAccountId)
        {
            return accessor.GetByShouldPayAccountId(shouldPayAccountId);
        }

        public Model.AtSummon GetByInvoiceCTId(string invoiceCTId)
        {
            return accessor.GetByInvoiceCTId(invoiceCTId);
        }

        public Model.AtSummon GetByProduceOtherInDepotId(string produceOtherInDepotId)
        {
            return accessor.GetByProduceOtherInDepotId(produceOtherInDepotId);
        }

        public Model.AtSummon SelectById(string id)
        {
            return accessor.SelectById(id);
        }

        /// <summary>
        /// 获取连续Id，比如说20200909001，20200909002，删除001后，再新增还是001
        /// </summary>
        /// <returns></returns>
        public string GetConsecutiveId(DateTime dt)
        {
            string settingId = this.GetSettingId();
            string invoiceKind = this.GetInvoiceKind().ToLower();
            DateTime datetime = dt;
            if (string.IsNullOrEmpty(invoiceKind) || string.IsNullOrEmpty(settingId))
                return string.Empty;

            string rule = Settings.Get(settingId);

            if (string.IsNullOrEmpty(rule))
                return string.Empty;

            string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, datetime.Year);
            string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, datetime.Year, datetime.Month);
            string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, datetime.ToString("yyyy-MM-dd"));
            string sequencekey = invoiceKind;
            if (rule.IndexOf("{D2}") >= 0)
                sequencekey = sequencekey_d;
            else if (rule.IndexOf("{M2}") >= 0)
                sequencekey = sequencekey_m;
            else if (rule.IndexOf("{Y2}") >= 0 || rule.IndexOf("{Y4}") >= 0)
                sequencekey = sequencekey_y;
            else
                sequencekey = invoiceKind;


            string d2 = string.Format("{0:d2}", datetime.Day);
            string m2 = string.Format("{0:d2}", datetime.Month);
            string y2 = string.Format("{0:d2}", datetime.Year);
            string y4 = string.Format("{0:d4}", datetime.Year);


            Func<int, string> getId = (sequenceval) =>
            {
                string n1 = string.Format("{0:d1}", sequenceval);
                string n2 = string.Format("{0:d2}", sequenceval);
                string n3 = string.Format("{0:d3}", sequenceval);
                string n4 = string.Format("{0:d4}", sequenceval);
                string n5 = string.Format("{0:d5}", sequenceval);
                string n6 = string.Format("{0:d6}", sequenceval);
                string n7 = string.Format("{0:d7}", sequenceval);
                string n8 = string.Format("{0:d8}", sequenceval);
                string n9 = string.Format("{0:d9}", sequenceval);
                string n10 = string.Format("{0:d10}", sequenceval);

                return rule.Replace("{D2}", d2).Replace("{M2}", m2).Replace("{Y2}", y2).Replace("{Y4}", y4).Replace("{N}", n4).Replace("{N1}", n1).Replace("{N2}", n2).Replace("{N3}", n3).Replace("{N4}", n4).Replace("{N5}", n5).Replace("{N6}", n6).Replace("{N7}", n7).Replace("{N8}", n8).Replace("{N9}", n9).Replace("{N10}", n10);
            };

            //int sequenceval = 1;
            int currentVal = SequenceManager.GetCurrentVal(sequencekey);
            //sequenceval++;

            string id = "";
            for (int i = 1; i <= currentVal; i++)
            {
                id = getId(i);

                if (!IsExistsId(id))
                    return id;
            }

            id = getId(++currentVal);

            return id;
        }
    }
}

