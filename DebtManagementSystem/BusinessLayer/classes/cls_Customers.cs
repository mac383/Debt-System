using BusinessLayer.classes.encryption;
using BusinessLayer.classes.keys;
using BusinessLayer.classes.validation;
using DataAccessLayer.models;
using DataAccessLayer.models.Customers;
using DataAccessLayer.repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static BusinessLayer.classes.keys.cls_Keys;
using static System.Net.Mime.MediaTypeNames;

namespace BusinessLayer.classes
{
    public class cls_Customers
    {
        private enum EN_Mode { AddNew = 1, Update = 2 };
        EN_Mode _mode;

        public enum EN_CustomerStatus { InActive = 0, Active = 1 }

        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string Phone1 { get; set; }
        public string? Phone2 { get; set; }
        public string CustomerCode { get; set; }
        public string? Address { get; set; }
        public EN_CustomerStatus CustomerStatus { get; set; }
        public string? TelegramId { get; set; }
        public int? ByUser { get; set; }
        public int CompanyId { get; set; }

        public cls_Customers()
        {
            CustomerId = -1;
            FullName = string.Empty;
            Phone1 = string.Empty;
            Phone2 = null;
            CustomerCode = string.Empty;
            Address = null;
            CustomerStatus = EN_CustomerStatus.Active;
            TelegramId = null;
            ByUser = -1;
            CompanyId = -1;

            _mode = EN_Mode.AddNew;
        }

        public cls_Customers(int customerId, string fullName, string phone1, string? phone2,
           string? address, bool customerStatus, string? telegramId, int? byUser, int companyId)
        {
            this.CustomerId = customerId;
            this.FullName = fullName;
            this.Phone1 = phone1;
            this.Phone2 = phone2;
            this.CustomerCode = string.Empty;
            this.Address = address;
            this.CustomerStatus = customerStatus ? EN_CustomerStatus.Active : EN_CustomerStatus.InActive;
            this.TelegramId = telegramId;
            this.ByUser = byUser;
            this.CompanyId = companyId;

            _mode = EN_Mode.Update;
        }

        private cls_Customers(int customerId, string fullName, string phone1, string? phone2, string customerCode,
            string? address, bool customerStatus, string? telegramId, int? byUser, int companyId)
        {
            this.CustomerId = customerId;
            this.FullName = fullName;
            this.Phone1 = phone1;
            this.Phone2 = phone2;
            this.CustomerCode = customerCode;
            this.Address = address;
            this.CustomerStatus = customerStatus ? EN_CustomerStatus.Active : EN_CustomerStatus.InActive;
            this.TelegramId = telegramId;
            this.ByUser = byUser;
            this.CompanyId = companyId;

            _mode = EN_Mode.Update;
        }

        public bool ValidateCustomerObject()
        {
            // تقليم النصوص وحفظها في متغيرات محلية
            string? address = this.Address?.Trim();
            string? fullName = this.FullName?.Trim();
            string? phone1 = this.Phone1?.Trim();
            string? phone2 = this.Phone2?.Trim();
            string? telegramId = this.TelegramId?.Trim();

            // التحقق من صحة الاسم الكامل
            if (!cls_validation.IsText(fullName) || !cls_validation.CheckLength(1, 50, fullName))
                return false;
            if (!string.IsNullOrEmpty(address))
                if (address.Length > 100)
                    return false;
            // التحقق من صحة الهاتف 1
            if (!cls_validation.IsPhone1NumberValid(phone1))
                return false;

            // التحقق من صحة الهاتف 2 إذا لم يكن فارغًا
            if (!cls_validation.IsPhone2NumberValid(phone2))
                return false;

            // التحقق من صحة معرف التليجرام إذا لم يكن فارغًا
            if (!cls_validation.IsTelegramIdValid(telegramId))
                return false;

            // جميع التحقق مر بنجاح
            return true;
        }


        // Completed Testing.
        public static async Task<List<md_Customers>?> GetCustomersAsync(int companyId)
        {
            return await cls_Customers_D.GetCustomersAsync(companyId);
        }

        // Completed Testing.
        public static async Task<List<md_Customers>?> GetActiveCustomersAsync(int companyId)
        {
            return await cls_Customers_D.GetActiveCustomersAsync(companyId);
        }

        // Completed Testing.
        public static async Task<List<md_Customers>?> GetInActiveCustomersAsync(int companyId)
        {
            return await cls_Customers_D.GetInActiveCustomersAsync(companyId);
        }

        // Completed Testing.
        public static async Task<cls_Customers?> GetCustomerByIdAsync(int customerID, int companyId)
        {
            md_Customer? customer = await cls_Customers_D.GetCustomerByIdAsync(customerID, companyId);

            if (customer == null)
                return null;

            return new cls_Customers
                (
                    customer.CustomerId, customer.FullName, customer.Phone1, customer.Phone2, customer.CustomerCode,
                    customer.Address, customer.CustomerStatus, customer.TelegramId, customer.ByUser, customer.CompanyId
                );
        }

        // Completed Testing.
        public static async Task<bool> SetCustomerAsActiveAsync(int CustomerId, int companyId)
        {
            return await cls_Customers_D.SetCustomerAsActiveAsync(CustomerId, companyId);
        }

        // Completed Testing.
        public static async Task<bool> SetCustomerAsInActiveAsync(int CustomerId, int companyId)
        {
            return await cls_Customers_D.SetCustomerAsInActiveAsync(CustomerId, companyId);
        }

        // Completed Testing.
        public static async Task<bool> IsCustomerCodeExistAsync(string customerCode)
        {
            return await cls_Customers_D.IsCustomerCodeExistAsync(customerCode);
        }

        // Completed Testing.
        public static async Task<int> GetCustomersCountAsync(int companyId)
        {
            return await cls_Customers_D.GetCustomersCountAsync(companyId);
        }

        // Completed Testing.
        public static async Task<bool> IsCustomerHasRelationsAsync(int CustomerId, int companyId)
        {
            return await cls_Customers_D.IsCustomerHasRelationsAsync(CustomerId, companyId);
        }

        // Completed Testing.
        public static async Task<bool> DeleteCustomerAsync(int CustomerId, int companyId)
        {
            return await cls_Customers_D.DeleteCustomerAsync(CustomerId, companyId);
        }

        // Completed Testing.
        private async Task<bool> _NewCustomerAsync()
        {
            this.CustomerCode = cls_Keys.GetKey(8, 1, EN_KeyType.NumbersLetters);

            while (await IsCustomerCodeExistAsync(this.CustomerCode))
                this.CustomerCode = cls_Keys.GetKey(8, 1, EN_KeyType.NumbersLetters);

            // التحقق من صحة البيانات
            if (!ValidateCustomerObject() || !cls_validation.CheckLength(8, 14, CustomerCode))
                return false;

            // تجهيز البيانات التي سيتم اضافته
            md_Customer customer = new md_Customer
                (
                    CustomerId, FullName, Phone1, Phone2, CustomerCode, Address, Convert.ToBoolean(CustomerStatus), TelegramId, ByUser, CompanyId
                );

            // اضافة مستخدم جديد و الحصول علئ معرف المستخدم الذي تم اضافته
            int insertedId = await cls_Customers_D.NewCustomerAsync(customer);

            // التحقق من الاضافة وخزن معرف المستخدم مع بياناته في الكائن الحالي في حال تم اضافة المستخدم بنجاح
            this.CustomerId = insertedId > 0 ? insertedId : -1;

            // (ارجاع حالة الاضافة (نجح الاضافة - فشل الاضافة
            return insertedId > 0;
        }

        // Completed Testing.
        private async Task<bool> _UpdateCustomerAsync()
        {
            // التحقق من صحة البيانات
            if (!ValidateCustomerObject())
                return false;

            // تجهيز البيانات التي سيتم اضافته
            md_Customer customer = new md_Customer
                (
                    CustomerId, FullName, Phone1, Phone2, CustomerCode, Address, Convert.ToBoolean(CustomerStatus), TelegramId, ByUser,
                    CompanyId
                );

            // (تحديث المستخدم وارجاع حالة التحديث (نجح التحديث - فشل التحديث
            return await cls_Customers_D.UpdateCustomerAsync(customer);
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
                    if (await _NewCustomerAsync())
                    {
                        // تغيير حالة الكائن الئ وضع التحديث 
                        _mode = EN_Mode.Update;
                        return true;
                    }
                    break;

                // الكائن في وضع التحديث
                case EN_Mode.Update:
                    // تحديث المستخدم
                    return await _UpdateCustomerAsync();
            }

            return false;
        }
    }
}
