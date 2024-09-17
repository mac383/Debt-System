using BusinessLayer.classes.validation;
using DataAccessLayer.models;
using DataAccessLayer.repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.classes
{
    public class cls_Categories
    {
        private enum EN_Mode { AddNew = 1, Update = 2 };
        EN_Mode _mode;

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public byte[]? Image { get; set; }
        public int ByUser { get; set; }
        public int CompanyId { get; set; }

        public cls_Categories()
        {
            CategoryId = -1;
            CategoryName = string.Empty;
            Image = null;
            ByUser = -1;
            CompanyId = -1;

            _mode = EN_Mode.AddNew;
        }

        public cls_Categories(int categoryId, string categoryName, byte[]? image, int byUser, int companyId)
        {
            this.CategoryId = categoryId;
            this.CategoryName = categoryName;
            this.Image = image;
            this.ByUser = byUser;
            this.CompanyId = companyId;

            _mode = EN_Mode.Update;
        }

        // Completed Testing.
        public bool ValidateCategoryObject()
        {
            // تقليم النصوص وحفظها في متغيرات محلية
            string? CategoryName = this.CategoryName?.Trim();

            // التحقق من صحة الاسم الكامل
            if (!cls_validation.CheckLength(1, 50, CategoryName))
                return false;

            // جميع التحقق مر بنجاح
            return true;
        }

        // Completed Testing.
        public static async Task<bool> IsCategoryExistAsync(string categoryName, int companyId)
        {
            return await cls_Categories_D.IsCategoryExistAsync(categoryName, companyId);
        }

        // Completed Testing.
        public static async Task<bool> IsCategoryExistWithOutCurrentCategoryAsync(int categoryId, string categoryName, int companyId)
        {
            return await cls_Categories_D.IsCategoryExistWithOutCurrentCategoryAsync(categoryId, categoryName, companyId);
        }

        // Completed Testing.
        public static async Task<bool> IsCategoryHasRelationsAsync(int CategoryId, int companyId)
        {
            return await cls_Categories_D.IsCategoryHasRelationsAsync(CategoryId, companyId);
        }

        // Completed Testing.
        public static async Task<cls_Categories?> GetCategoryByIdAsync(int categoryId, int companyId)
        {
            md_Category? model = await cls_Categories_D.GetCategoryByIdAsync(categoryId, companyId);

            if (model == null)
                return null;

            return new cls_Categories(model.CategoryID, model.CategoryName, model.Image, model.ByUser, model.CompanyId);
        }

        // Completed Testing.
        public static async Task<List<md_Categories>?> GetCategoriesAsync(int companyId)
        {
            return await cls_Categories_D.GetCategoriesAsync(companyId);
        }

        // Completed Testing.
        public static async Task<bool> DeleteCategoryAsync(int categoryId, int companyId)
        {
            return await cls_Categories_D.DeleteCategoryAsync(categoryId, companyId);
        }

        // Completed Testing.
        public static async Task<int> GetCategoriesCountAsync(int companyId)
        {
            return await cls_Categories_D.GetCategoriesCountAsync(companyId);
        }

        // Completed Testing.
        private async Task<bool> _NewCategoryAsync()
        {
            // التحقق من صحة البيانات
            if (!ValidateCategoryObject())
                return false;

            // تجهيز البيانات التي سيتم اضافته
            md_Category category = new md_Category
                (
                    CategoryId, CategoryName, Image, ByUser, CompanyId
                );

            // اضافة مستخدم جديد و الحصول علئ معرف المستخدم الذي تم اضافته
            int insertedId = await cls_Categories_D.NewCategoryAsync(category);

            // التحقق من الاضافة وخزن معرف المستخدم مع بياناته في الكائن الحالي في حال تم اضافة المستخدم بنجاح
            this.CategoryId = insertedId > 0 ? insertedId : -1;

            // (ارجاع حالة الاضافة (نجح الاضافة - فشل الاضافة
            return insertedId > 0;
        }

        // Completed Testing.
        private async Task<bool> _UpdateCategoryAsync()
        {
            // التحقق من صحة البيانات
            if (!ValidateCategoryObject())
                return false;

            // تجهيز البيانات التي سيتم اضافته
            md_Category category = new md_Category
                (
                    CategoryId, CategoryName, Image, ByUser, CompanyId
                );

            // (تحديث المستخدم وارجاع حالة التحديث (نجح التحديث - فشل التحديث
            return await cls_Categories_D.UpdateCategoryAsync(category);
        }

        // Completed Testing.
        public async Task<bool> SaveAsync()
        {
            // التحقق من حالة الكائن الحالي
            switch (_mode)
            {
                // الكائن في وضع الاضافة
                case EN_Mode.AddNew:
                    // اضافة مستخدم جديد
                    if (await _NewCategoryAsync())
                    {
                        // تغيير حالة الكائن الئ وضع التحديث 
                        _mode = EN_Mode.Update;
                        return true;
                    }
                    break;

                // الكائن في وضع التحديث
                case EN_Mode.Update:
                    // تحديث المستخدم
                    return await _UpdateCategoryAsync();
            }

            return false;
        }

    }
}
