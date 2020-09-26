//------------------------------------------------------------------------------
//
// file name：ProduceOtherInDepotManager.cs
// author: peidun
// create date：2010-1-8 13:43:35
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProduceOtherInDepot.
    /// </summary>
    public partial class ProduceOtherInDepotManager : BaseManager
    {
        private static readonly DA.IProduceOtherInDepotDetailAccessor ProduceOtherInDepotDetailAccessor = (DA.IProduceOtherInDepotDetailAccessor)Accessors.Get("ProduceOtherInDepotDetailAccessor");
        private static readonly DA.IStockAccessor stockAccessor = (DA.IStockAccessor)Accessors.Get("StockAccessor");
        private static readonly DA.IProduceOtherCompactDetailAccessor produceOtherCompactDetailAccessor = (DA.IProduceOtherCompactDetailAccessor)Accessors.Get("ProduceOtherCompactDetailAccessor");
        private ProductManager productManager = new ProductManager();
        BL.AtSummonManager atSummonManager = new Book.BL.AtSummonManager();
        private static readonly DA.IAtSummonAccessor atSummonAccessor = (DA.IAtSummonAccessor)Accessors.Get("AtSummonAccessor");
        BL.AtSummonDetailManager atSummonDetailManager = new Book.BL.AtSummonDetailManager();

        private void CancelAffect(Model.ProduceOtherInDepot produceOtherInDepot, int tag)
        {
            foreach (Model.ProduceOtherInDepotDetail item in ProduceOtherInDepotDetailAccessor.Select(produceOtherInDepot))
            {
                if (!string.IsNullOrEmpty(item.DepotPositionId))
                {
                    stockAccessor.Increment(new BL.DepotPositionManager().Get(item.DepotPositionId), item.Product, -item.ProduceInDepotQuantity);
                    this.productManager.UpdateProduct_Stock(item.Product);
                }
                Model.ProduceOtherCompactDetail CompactDetail = produceOtherCompactDetailAccessor.Get(item.ProduceOtherCompactDetailId);
                if (CompactDetail != null)
                {
                    CompactDetail.InDepotCount = CompactDetail.InDepotCount == null ? 0 : CompactDetail.InDepotCount - item.ProduceQuantity;
                    CompactDetail.ArrivalInQuantity = CompactDetail.ArrivalInQuantity == null ? 0 : CompactDetail.ArrivalInQuantity - item.ProduceQuantity;
                    if (CompactDetail.InDepotCount >= CompactDetail.OtherCompactCount)
                    {
                        CompactDetail.DetailsFlag = 2;
                    }
                    else
                    {
                        if (CompactDetail.InDepotCount > 0)
                        {
                            CompactDetail.DetailsFlag = 1;
                        }
                        else
                        {
                            CompactDetail.DetailsFlag = 0;
                        }
                    }
                    produceOtherCompactDetailAccessor.Update(CompactDetail);
                    if (tag == 1)
                        UpdateProduceOtherCompactFlag(CompactDetail.ProduceOtherCompact);
                }
            }
        }
        /// <summary>
        /// Delete ProduceOtherInDepot by primary key.
        /// </summary>
        public void Delete(string produceOtherInDepotId)
        {
            try
            {
                BL.V.BeginTransaction();
                CancelAffect(accessor.Get(produceOtherInDepotId), 1);
                accessor.Delete(produceOtherInDepotId);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }
        public void Delete(Model.ProduceOtherInDepot produceOtherInDepot)
        {

            this.Delete(produceOtherInDepot.ProduceOtherInDepotId);

        }
        public Model.ProduceOtherInDepot GetDetails(string produceOtherInDepotId)
        {
            Model.ProduceOtherInDepot produceOtherInDepot = accessor.Get(produceOtherInDepotId);
            if (produceOtherInDepot != null)
                produceOtherInDepot.Details = ProduceOtherInDepotDetailAccessor.Select(produceOtherInDepot);
            return produceOtherInDepot;
        }
        /// <summary>
        /// Insert a ProduceOtherInDepot.
        /// </summary>
        public void Insert(Model.ProduceOtherInDepot produceOtherInDepot)
        {
            //
            // todo:add other logic here
            //
            Validate(produceOtherInDepot);

            try
            {
                produceOtherInDepot.InsertTime = DateTime.Now;
                produceOtherInDepot.UpdateTime = DateTime.Now;
                TiGuiExists(produceOtherInDepot);

                BL.V.BeginTransaction();

                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, produceOtherInDepot.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, produceOtherInDepot.InsertTime.Value.Year, produceOtherInDepot.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, produceOtherInDepot.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);


                accessor.Insert(produceOtherInDepot);

                foreach (Model.ProduceOtherInDepotDetail produceOtherInDepotDetail in produceOtherInDepot.Details)
                {
                    if (produceOtherInDepotDetail.Product == null || string.IsNullOrEmpty(produceOtherInDepotDetail.Product.ProductId))
                        throw new Exception("貨品不為空");
                    produceOtherInDepotDetail.ProduceOtherInDepotId = produceOtherInDepot.ProduceOtherInDepotId;
                    ProduceOtherInDepotDetailAccessor.Insert(produceOtherInDepotDetail);

                    if (!string.IsNullOrEmpty(produceOtherInDepotDetail.DepotPositionId))
                    {
                        //Model.Stock stock = stockAccessor.GetStockByProductIdAndDepotPositionId(produceOtherInDepotDetail.ProductId, produceOtherInDepotDetail.DepotPositionId);
                        stockAccessor.Increment(new BL.DepotPositionManager().Get(produceOtherInDepotDetail.DepotPositionId), produceOtherInDepotDetail.Product, produceOtherInDepotDetail.ProduceInDepotQuantity);
                        productManager.UpdateProduct_Stock(produceOtherInDepotDetail.Product);
                    }
                    Model.ProduceOtherCompactDetail CompactDetail = produceOtherCompactDetailAccessor.Get(produceOtherInDepotDetail.ProduceOtherCompactDetailId);
                    if (CompactDetail != null)
                    {
                        if (CompactDetail.InDepotCount == null)
                            CompactDetail.InDepotCount = 0;
                        CompactDetail.InDepotCount += produceOtherInDepotDetail.ProduceQuantity;
                        if (CompactDetail.InDepotCount >= CompactDetail.OtherCompactCount)
                        {
                            CompactDetail.DetailsFlag = 2;
                        }
                        else
                        {
                            if (CompactDetail.InDepotCount > 0)
                            {
                                CompactDetail.DetailsFlag = 1;
                            }
                            else
                            {
                                CompactDetail.DetailsFlag = 0;
                            }
                        }

                        //进货数量
                        if (CompactDetail.ArrivalInQuantity == null)
                            CompactDetail.ArrivalInQuantity = 0;
                        CompactDetail.ArrivalInQuantity += produceOtherInDepotDetail.ProduceQuantity;

                        produceOtherCompactDetailAccessor.Update(CompactDetail);
                        UpdateProduceOtherCompactFlag(CompactDetail.ProduceOtherCompact);
                    }
                }

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }
        public void UpdateProduceOtherCompactFlag(Model.ProduceOtherCompact produceOtherCompact)
        {
            int flag = 0;
            IList<Model.ProduceOtherCompactDetail> list = produceOtherCompactDetailAccessor.Select(produceOtherCompact);
            foreach (Model.ProduceOtherCompactDetail detail in list)
            {
                flag += detail.DetailsFlag == null ? 0 : detail.DetailsFlag.Value;
            }
            if (flag == 0)
                produceOtherCompact.InvoiceDetailFlag = 0;
            else if (flag < list.Count * 2)
                produceOtherCompact.InvoiceDetailFlag = 1;
            else if (flag == list.Count * 2)
                produceOtherCompact.InvoiceDetailFlag = 2;
            new BL.ProduceOtherCompactManager().UpdateOtherCompact(produceOtherCompact);
        }
        /// <summary>
        /// Update a ProduceOtherInDepot.
        /// </summary>
        public void Update(Model.ProduceOtherInDepot produceOtherInDepot)
        {
            //
            // todo: add other logic here.
            //
            Validate(produceOtherInDepot);
            try
            {
                BL.V.BeginTransaction();
                if (produceOtherInDepot != null)
                {
                    //返回
                    CancelAffect(produceOtherInDepot, 0);
                    //修改头

                    produceOtherInDepot.UpdateTime = DateTime.Now;
                    accessor.Update(produceOtherInDepot);
                    //删除详细
                    ProduceOtherInDepotDetailAccessor.Delete(produceOtherInDepot);
                    //添加详细
                    foreach (Model.ProduceOtherInDepotDetail produceOtherInDepotDetail in produceOtherInDepot.Details)
                    {
                        if (produceOtherInDepotDetail.Product == null || string.IsNullOrEmpty(produceOtherInDepotDetail.Product.ProductId))
                            continue;
                        produceOtherInDepotDetail.ProduceOtherInDepotId = produceOtherInDepot.ProduceOtherInDepotId;
                        ProduceOtherInDepotDetailAccessor.Insert(produceOtherInDepotDetail);
                        if (!string.IsNullOrEmpty(produceOtherInDepotDetail.DepotPositionId))
                        {
                            stockAccessor.Increment(new BL.DepotPositionManager().Get(produceOtherInDepotDetail.DepotPositionId), produceOtherInDepotDetail.Product, produceOtherInDepotDetail.ProduceInDepotQuantity);
                            productManager.UpdateProduct_Stock(produceOtherInDepotDetail.Product);
                        }
                        Model.ProduceOtherCompactDetail CompactDetail = produceOtherCompactDetailAccessor.Get(produceOtherInDepotDetail.ProduceOtherCompactDetailId);
                        if (CompactDetail != null)
                        {
                            if (CompactDetail.InDepotCount == null)
                                CompactDetail.InDepotCount = 0;
                            CompactDetail.InDepotCount += produceOtherInDepotDetail.ProduceQuantity;
                            if (CompactDetail.InDepotCount >= CompactDetail.OtherCompactCount)
                            {
                                CompactDetail.DetailsFlag = 2;
                            }
                            else
                            {
                                if (CompactDetail.InDepotCount > 0)
                                {
                                    CompactDetail.DetailsFlag = 1;
                                }
                                else
                                {
                                    CompactDetail.DetailsFlag = 0;
                                }
                            }

                            //进货数量
                            if (CompactDetail.ArrivalInQuantity == null)
                                CompactDetail.ArrivalInQuantity = 0;
                            CompactDetail.ArrivalInQuantity += produceOtherInDepotDetail.ProduceQuantity;

                            produceOtherCompactDetailAccessor.Update(CompactDetail);
                            UpdateProduceOtherCompactFlag(CompactDetail.ProduceOtherCompact);
                        }
                    }
                }
                BL.V.CommitTransaction();
            }

            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }
        private void Validate(Model.ProduceOtherInDepot produceOtherInDepot)
        {
            if (string.IsNullOrEmpty(produceOtherInDepot.ProduceOtherInDepotId))
            {
                throw new Helper.RequireValueException(Model.ProduceOtherInDepot.PRO_ProduceOtherInDepotId);
            }
            if (string.IsNullOrEmpty(produceOtherInDepot.SupplierId))
            {
                throw new Helper.InvalidValueException(Model.ProduceOtherInDepot.PRO_SupplierId);
            }

            foreach (var item in produceOtherInDepot.Details)
            {
                if (item.ProduceQuantity == null || item.ProduceQuantity == 0)
                    throw new Helper.InvalidValueException(Model.ProduceOtherInDepotDetail.PRO_ProduceQuantity);
                if ((item.ProduceTransferQuantity == null || item.ProduceTransferQuantity <= 0) && (item.ProduceInDepotQuantity == null || item.ProduceInDepotQuantity <= 0))
                    throw new Helper.InvalidValueException(Model.ProduceOtherInDepotDetail.PRO_ProduceTransferQuantity);
            }
            //if (string.IsNullOrEmpty(produceOtherInDepot.WorkHouseId))
            //{
            //    throw new Helper.RequireValueException(Model.ProduceOtherInDepot.PROPERTY_WORKHOUSEID);
            //}
        }

        public IList<Model.ProduceOtherInDepot> SelectByCondition(DateTime startdate, DateTime enddate, Model.Supplier supper1, Model.Supplier supper2, string ProduceOtherCompactId1, string ProduceOtherCompactId2, Model.Product startPro, Model.Product endPro, string invouceCusidStart, string invouceCusidEnd)
        {
            return accessor.SelectByCondition(startdate, enddate, supper1, supper2, ProduceOtherCompactId1, ProduceOtherCompactId2, startPro, endPro, invouceCusidStart, invouceCusidEnd);
        }

        public IList<Model.ProduceOtherInDepot> SelectByDateRange(DateTime startdate, DateTime enddate)
        {
            return accessor.SelectByDateRange(startdate, enddate);
        }
        protected override string GetSettingId()
        {
            return "podRule";
        }
        protected override string GetInvoiceKind()
        {
            return "pod";
        }
        private void TiGuiExists(Model.ProduceOtherInDepot model)
        {
            if (this.ExistsPrimary(model.ProduceOtherInDepotId))
            {
                //设置KEY值
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, model.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, model.InsertTime.Value.Year, model.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, model.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                model.ProduceOtherInDepotId = this.GetId(model.InsertTime.Value);
                TiGuiExists(model);
                //throw new Helper.InvalidValueException(Model.Product.PRO_Id);               
            }

        }


        #region 生成对应的会计传票

        public void InsertAtSummon(Model.ProduceOtherInDepot produceOtherInDepot, Dictionary<string, string> dic)
        {
            Model.AtSummon atSummon = new Book.Model.AtSummon();
            atSummon.SummonId = Guid.NewGuid().ToString();
            atSummon.SummonDate = DateTime.Now;
            atSummon.SummonCategory = "轉帳傳票";
            atSummon.InsertTime = DateTime.Now;
            atSummon.UpdateTime = DateTime.Now;
            //atSummon.Id = this.atSummonManager.GetId();
            atSummon.Id = this.atSummonManager.GetConsecutiveId(DateTime.Now);
            atSummon.ProduceOtherInDepotId = produceOtherInDepot.ProduceOtherInDepotId;

            atSummon.Details = new List<Model.AtSummonDetail>();

            Model.AtSummonDetail detail1 = new Model.AtSummonDetail();
            detail1.SummonDetailId = Guid.NewGuid().ToString();
            detail1.SummonCatetory = atSummon.SummonCategory;
            detail1.Lending = "借";
            detail1.AMoney = Convert.ToDecimal(produceOtherInDepot.ProduceAmount);
            //detail1.SubjectId = dic["進貨"];
            detail1.SubjectId = dic[this.GetSubjectNameBySupplier(produceOtherInDepot.Supplier)];
            detail1.InsertTime = DateTime.Now;
            detail1.UpdateTime = DateTime.Now;
            atSummon.Details.Add(detail1);

            if (produceOtherInDepot.ProduceTax > 0)
            {
                Model.AtSummonDetail detail2 = new Model.AtSummonDetail();
                detail2.SummonDetailId = Guid.NewGuid().ToString();
                detail2.SummonCatetory = atSummon.SummonCategory;
                detail2.Lending = "借";
                detail2.AMoney = Convert.ToDecimal(produceOtherInDepot.ProduceTax);
                detail2.SubjectId = dic["進項稅額"];
                detail2.InsertTime = DateTime.Now;
                detail2.UpdateTime = DateTime.Now;
                atSummon.Details.Add(detail2);
            }

            Model.AtSummonDetail detail3 = new Model.AtSummonDetail();
            detail3.SummonDetailId = Guid.NewGuid().ToString();
            detail3.SummonCatetory = atSummon.SummonCategory;
            detail3.Lending = "貸";
            detail3.AMoney = Convert.ToDecimal(produceOtherInDepot.ProduceTotal);
            detail3.SubjectId = dic[string.Format("應付帳款-{0}", produceOtherInDepot.Supplier.SupplierShortName)];
            detail3.InsertTime = DateTime.Now;
            detail3.UpdateTime = DateTime.Now;
            atSummon.Details.Add(detail3);


            foreach (var item in atSummon.Details)
            {
                if (item.Lending == "借")
                    item.Id = "A" + atSummon.Details.IndexOf(item);
                else
                    item.Id = "B" + atSummon.Details.IndexOf(item);
            }

            atSummonManager.TiGuiExists(atSummon);

            //插入
            string invoiceKind = "ats";
            string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, atSummon.SummonDate.Value.Year);
            string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, atSummon.SummonDate.Value.Year, atSummon.SummonDate.Value.Month);
            string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, atSummon.SummonDate.Value.ToString("yyyy-MM-dd"));
            string sequencekey = string.Format(invoiceKind);

            SequenceManager.Increment(sequencekey_y);
            SequenceManager.Increment(sequencekey_m);
            SequenceManager.Increment(sequencekey_d);
            SequenceManager.Increment(sequencekey);

            atSummon.TotalDebits = atSummon.Details.Where(d => d.Lending == "借").Sum(d => d.AMoney);
            atSummon.CreditTotal = atSummon.Details.Where(d => d.Lending == "貸").Sum(d => d.AMoney);

            atSummonAccessor.Insert(atSummon);

            foreach (Model.AtSummonDetail atSummonDetail in atSummon.Details)
            {
                atSummonDetail.SummonId = atSummon.SummonId;
                atSummonDetailManager.Insert(atSummonDetail);
            }
        }

        public void UpdateAtSummon(Model.ProduceOtherInDepot produceOtherInDepot, Dictionary<string, string> dic)
        {
            Model.AtSummon atSummon = atSummonManager.GetByProduceOtherInDepotId(produceOtherInDepot.ProduceOtherInDepotId);
            if (atSummon != null)
            {
                atSummon.UpdateTime = DateTime.Now;

                atSummon.Details = atSummonDetailManager.Select(atSummon);

                foreach (var item in atSummon.Details)
                {
                    if (dic.Values.Contains(item.SubjectId))
                    {
                        if (item.Lending == "借")
                        {
                            if (item.Subject.SubjectName == "進項稅額")
                            {
                                if (produceOtherInDepot.ProduceTax > 0)
                                {
                                    if (item.AMoney != Convert.ToDecimal(produceOtherInDepot.ProduceTax))
                                    {
                                        item.AMoney = Convert.ToDecimal(produceOtherInDepot.ProduceTax);
                                        item.UpdateTime = DateTime.Now;

                                        atSummonDetailManager.Update(item);
                                    }
                                }
                                else
                                    atSummonDetailManager.Delete(item.SummonDetailId);
                            }
                            else       //供應商分類對應的科目
                            {
                                if (item.AMoney != Convert.ToDecimal(produceOtherInDepot.ProduceAmount))
                                {
                                    item.AMoney = Convert.ToDecimal(produceOtherInDepot.ProduceAmount);
                                    item.UpdateTime = DateTime.Now;

                                    atSummonDetailManager.Update(item);
                                }
                            }
                        }
                        else          //應付賬款-廠商
                        {
                            if (item.AMoney != Convert.ToDecimal(produceOtherInDepot.ProduceTotal))
                            {
                                item.AMoney = Convert.ToDecimal(produceOtherInDepot.ProduceTotal);
                                item.UpdateTime = DateTime.Now;

                                atSummonDetailManager.Update(item);
                            }
                        }
                    }
                }

                if (produceOtherInDepot.ProduceTax > 0 && atSummon.Details.Count == 2 && !atSummon.Details.Any(d => d.Subject.SubjectName == "進項稅額"))
                {
                    Model.AtSummonDetail detail2 = new Model.AtSummonDetail();
                    detail2.SummonId = atSummon.SummonId;
                    detail2.SummonDetailId = Guid.NewGuid().ToString();
                    detail2.SummonCatetory = atSummon.SummonCategory;
                    detail2.Lending = "借";
                    detail2.Id = "A2";
                    detail2.AMoney = Convert.ToDecimal(produceOtherInDepot.ProduceTax);
                    detail2.SubjectId = dic["進項稅額"];
                    detail2.InsertTime = DateTime.Now;
                    detail2.UpdateTime = DateTime.Now;
                    atSummon.Details.Add(detail2);

                    atSummonDetailManager.Insert(detail2);
                }

                atSummon.TotalDebits = atSummon.Details.Where(d => d.Lending == "借").Sum(d => d.AMoney);
                atSummon.CreditTotal = atSummon.Details.Where(d => d.Lending == "貸").Sum(d => d.AMoney);

                atSummonAccessor.Update(atSummon);
            }
        }

        public void DeleteAtSummon(Model.ProduceOtherInDepot produceOtherInDepot)
        {
            Model.AtSummon atSummon = atSummonManager.GetByProduceOtherInDepotId(produceOtherInDepot.ProduceOtherInDepotId);
            if (atSummon != null)
            {
                atSummonManager.Delete(atSummon);
            }
        }
        #endregion
    }
}

