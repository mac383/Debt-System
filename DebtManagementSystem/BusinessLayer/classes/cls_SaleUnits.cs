using BusinessLayer.classes.validation;
using DataAccessLayer.models;
using DataAccessLayer.models.SaleUnit_models;
using DataAccessLayer.repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BusinessLayer.classes
{
    public class cls_SaleUnits
    {
        private enum EN_Mode { AddNew = 1, Update = 2 };
        EN_Mode _mode;

        public int SaleUnitID { get; set; }
        public string UnitName { get; set; }
        public int ByUser { get; set; }
        public int CompanyId { get; set; }

        public cls_SaleUnits()
        {
            SaleUnitID = -1;
            UnitName = string.Empty;
            ByUser = -1;
            CompanyId = -1;

            _mode = EN_Mode.AddNew;
        }

        public cls_SaleUnits(int saleunitid, string unitname,int byuser, int companyid)
        {
            this.SaleUnitID = saleunitid;
            this.UnitName = unitname;
            this.ByUser = byuser;
            this.CompanyId = companyid;

            _mode = EN_Mode.Update;
        }

        //Completed Testing
        public bool ValidateCategoryObj()
        {
            if (!cls_validation.CheckLength(1, 50, UnitName))
                return false;

            if (!cls_validation.IsInt(ByUser.ToString()))
                return false;

            if (!cls_validation.IsInt(CompanyId.ToString()))
                return false;

            return true;
        }

        //Completed Testing
        public static async Task<bool> IsSaleUnitExistAsync(string UnitName, int companyId)
        {
            return await cls_SaleUnits_D.IsSaleUnitExistAsync(UnitName, companyId);
        }

        //Completed Testing
        public static async Task<bool> IsSaleUnitExistWithOutCurrentSaleUnitAsync (int unitId, string unitName, int companyId)
        {
            return await cls_SaleUnits_D.IsSaleUnitExistWithOutCurrentSaleUnitAsync(unitId, unitName, companyId);
        }

        //Completed Testing
        public static async Task<int> GetSaleUnitsCountAsync(int companyId)
        {
            return await cls_SaleUnits_D.GetSaleUnitsCountAsync(companyId);
        }

        //Completed Testing
        public static async Task<bool> IsSaleUnitHasRelationsAsync(int UnitId, int companyId)
        {
            return await cls_SaleUnits_D.IsSaleUnitHasRelationsAsync(UnitId, companyId);
        }

        //Completed Testing
        public static async Task<List<md_SaleUnits>?> GetSaleUnitsAsync(int companyId)
        {
            return await cls_SaleUnits_D.GetSaleUnitsAsync(companyId);
        }

        //Completed Testing
        public static async Task<cls_SaleUnits?> GetSaleUnitByIdAsync(int unitId, int companyId)
        {
            md_SaleUnit? model = await cls_SaleUnits_D.GetSaleUnitByIdAsync(unitId, companyId);

            if (model == null)
                return null;

            return new cls_SaleUnits(model.SaleUnitID, model.UnitName,  model.ByUser, model.CompanyId);
        }

        //Completed Testing
        public static async Task<bool> DeleteSaleUnitAsync(int unitId, int companyId)
        {
            return await cls_SaleUnits_D.DeleteSaleUnitAsync(unitId, companyId);
        }

        //Completed Testing
        private async Task<bool> _NewSaleUnitAsync()
        {
            //// التحقق من صحة البيانات
            if (!ValidateCategoryObj())
                return false;

            // تجهيز البيانات التي سيتم اضافته
            md_NewSaleUnit saleunit = new md_NewSaleUnit
                (
                     UnitName, ByUser, CompanyId
                );

            // اضافة مستخدم جديد و الحصول علئ معرف المستخدم الذي تم اضافته
            int insertedId = await cls_SaleUnits_D.NewSaleUnitAsync(saleunit);

            // التحقق من الاضافة وخزن معرف المستخدم مع بياناته في الكائن الحالي في حال تم اضافة المستخدم بنجاح
            this.SaleUnitID = insertedId > 0 ? insertedId : -1;

            // (ارجاع حالة الاضافة (نجح الاضافة - فشل الاضافة
            return insertedId > 0;
        }

        //Completed Testing
        private async Task<bool> _UpdateSaleUnitAsync()
        {
            //// التحقق من صحة البيانات
            if (!ValidateCategoryObj())
                return false;

            // تجهيز البيانات التي سيتم اضافته
            md_SaleUnit saleUnit = new md_SaleUnit
                (
                    SaleUnitID, UnitName, ByUser, CompanyId
                );

            // (تحديث المستخدم وارجاع حالة التحديث (نجح التحديث - فشل التحديث
            return await cls_SaleUnits_D.UpdateSaleUnitAsync(saleUnit);
        }

        //Completed Testing
        public async Task<bool> SaveAsync()
        {
            switch (_mode)
            {
                case EN_Mode.AddNew:
                    if (await _NewSaleUnitAsync())
                    {
                        _mode = EN_Mode.Update;
                        return true;
                    }
                    break;

                case EN_Mode.Update:
                    if (await _UpdateSaleUnitAsync())
                        return true;
                    break;
            }
            return false;
        }
    }
}
