//------------------------------------------------------------------------------
//
// file name：PCDataInputManager.cs
// author: mayanjun
// create date：2015/1/18 下午 08:03:13
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCDataInput.
    /// </summary>
    public partial class PCDataInputManager
    {
        BL.PCOpticalMachineManager PCOpticalMachineManager = new PCOpticalMachineManager();
        BL.PCLaserMachineManager PCLaserMachineManager = new PCLaserMachineManager();
        BL.PCDefinitionManager PCDefinitionManager = new PCDefinitionManager();
        BL.PCPerspectiveManager PCPerspectiveManager = new PCPerspectiveManager();
        BL.PCHazeManager PCHazeManager = new PCHazeManager();
        BL.PCEuropeOpticalManager PCEuropeOpticalManager = new PCEuropeOpticalManager();
        /// <summary>
        /// Delete PCDataInput by primary key.
        /// </summary>
        public void Delete(string pCDataInputId)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                Model.PCDataInput model = this.GetDetails(pCDataInputId);
                if (model != null)
                    this.Delete(model);
                accessor.Delete(pCDataInputId);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public void Delete(Model.PCDataInput pCDataInput)
        {
            foreach (var item in pCDataInput.PCOpticalMachineList)
            {
                this.PCOpticalMachineManager.Delete(item.PCOpticalMachineId);
            }
            foreach (var item in pCDataInput.PCLaserMachineList)
            {
                this.PCLaserMachineManager.Delete(item.PCLaserMachineId);
            }
            foreach (var item in pCDataInput.PCDefinitionList)
            {
                this.PCDefinitionManager.Delete(item.PCDefinitionId);
            }
            foreach (var item in pCDataInput.PCPerspectiveList)
            {
                this.PCPerspectiveManager.Delete(item.PCPerspectiveId);
            }
            foreach (var item in pCDataInput.PCHazeList)
            {
                this.PCHazeManager.Delete(item.PCHazeId);
            }
            foreach (var item in pCDataInput.PCEuropeOpticalList)
            {
                this.PCEuropeOpticalManager.Delete(item.PCEuropeOpticalId);
            }
        }

        /// <summary>
        /// Insert a PCDataInput.
        /// </summary>
        public void Insert(Model.PCDataInput pCDataInput)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                pCDataInput.InsertTime = DateTime.Now;
                pCDataInput.UpdateTime = DateTime.Now;
                accessor.Insert(pCDataInput);

                //插入详细
                foreach (var item in pCDataInput.PCOpticalMachineList)
                {
                    item.PCDataInputId = pCDataInput.PCDataInputId;
                    this.PCOpticalMachineManager.Insert(item);
                }
                foreach (var item in pCDataInput.PCLaserMachineList)
                {
                    item.PCDataInputId = pCDataInput.PCDataInputId;
                    this.PCLaserMachineManager.Insert(item);
                }
                foreach (var item in pCDataInput.PCDefinitionList)
                {
                    item.PCDataInputId = pCDataInput.PCDataInputId;
                    this.PCDefinitionManager.Insert(item);
                }
                foreach (var item in pCDataInput.PCPerspectiveList)
                {
                    item.PCDataInputId = pCDataInput.PCDataInputId;
                    this.PCPerspectiveManager.Insert(item);
                }
                foreach (var item in pCDataInput.PCHazeList)
                {
                    item.PCDataInputId = pCDataInput.PCDataInputId;
                    this.PCHazeManager.Insert(item);
                }
                foreach (var item in pCDataInput.PCEuropeOpticalList)
                {
                    item.PCDataInputId = pCDataInput.PCDataInputId;
                    this.PCEuropeOpticalManager.Insert(item);
                }
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }


        /// <summary>
        /// Update a PCDataInput.
        /// </summary>
        public void Update(Model.PCDataInput pCDataInput)
        {
            //
            // todo: add other logic here.
            //
            try
            {
                BL.V.BeginTransaction();
                pCDataInput.UpdateTime = DateTime.Now;
                accessor.Update(pCDataInput);
                Model.PCDataInput model = this.GetDetails(pCDataInput.PCDataInputId);
                if (model != null)
                    this.Delete(model);
                //插入详细
                foreach (var item in pCDataInput.PCOpticalMachineList)
                {
                    item.PCDataInputId = pCDataInput.PCDataInputId;
                    this.PCOpticalMachineManager.Insert(item);
                }
                foreach (var item in pCDataInput.PCLaserMachineList)
                {
                    item.PCDataInputId = pCDataInput.PCDataInputId;
                    this.PCLaserMachineManager.Insert(item);
                }
                foreach (var item in pCDataInput.PCDefinitionList)
                {
                    item.PCDataInputId = pCDataInput.PCDataInputId;
                    this.PCDefinitionManager.Insert(item);
                }
                foreach (var item in pCDataInput.PCPerspectiveList)
                {
                    item.PCDataInputId = pCDataInput.PCDataInputId;
                    this.PCPerspectiveManager.Insert(item);
                }
                foreach (var item in pCDataInput.PCHazeList)
                {
                    item.PCDataInputId = pCDataInput.PCDataInputId;
                    this.PCHazeManager.Insert(item);
                }
                foreach (var item in pCDataInput.PCEuropeOpticalList)
                {
                    item.PCDataInputId = pCDataInput.PCDataInputId;
                    this.PCEuropeOpticalManager.Insert(item);
                }
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public Model.PCDataInput GetDetails(string id)
        {
            Model.PCDataInput model = this.Get(id);
            if (model != null)
            {
                model.PCOpticalMachineList = this.PCOpticalMachineManager.SelectByHeaderId(id);
                model.PCLaserMachineList = this.PCLaserMachineManager.SelectByHeaderId(id);
                model.PCDefinitionList = this.PCDefinitionManager.SelectByHeaderId(id);
                model.PCPerspectiveList = this.PCPerspectiveManager.SelectByHeaderId(id);
                model.PCHazeList = this.PCHazeManager.SelectByHeaderId(id);
                model.PCEuropeOpticalList = this.PCEuropeOpticalManager.SelectByHeaderId(id);
            }
            return model;
        }

        private void ValiData(Model.PCDataInput model)
        {
            if (string.IsNullOrEmpty(model.CheckStandard))
                throw new Helper.InvalidValueException(Model.PCDataInput.PRO_CheckStandard);
        }

        public IList<Model.PCDataInput> SelectByCondition(DateTime startDate, DateTime endDate, string PNTId, string CusXOId, string ProductId)
        {
            return accessor.SelectByCondition(startDate, endDate, PNTId, CusXOId, ProductId);
        }
    }
}
