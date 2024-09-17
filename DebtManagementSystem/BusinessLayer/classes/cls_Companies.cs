using BusinessLayer.classes.keys;
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
    public class cls_Companies
    {

        enum EN_Mode { New = 1, Update = 2 }
        EN_Mode _Mode;

        public enum EN_SubscriptionStatus { Active = 1, InActive = 0 }
        public enum EN_IsPaid { Paid = 1, UnPaid = 0 }

        public int CompanyId { get; set; }
        public string ManagerFullName { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        public byte[]? CompanyImage { get; set; }
        public string Phone1 { get; set; }
        public string? Phone2 { get; set; }
        public string Address { get; set; }
        public double SubscriptionFee { get; set; }
        public string Currency { get; set; }
        public DateTime RegistrationDate { get; set; }
        public EN_SubscriptionStatus SubscriptionStatus { get; set; }
        public DateTime SubscriptionStartDate { get; set; }
        public DateTime SubscriptionEndDate { get; set; }
        public int RemainingSubscriptionDays { get; set; }
        public string? Description { get; set; }
        public EN_IsPaid IsPaid { get; set; }
        public int ByAdmin { get; set; }
        public string Action { get; set; }

        // For New Company.
        public cls_Companies()
        {
            CompanyId = -1;
            ManagerFullName = string.Empty;
            CompanyName = string.Empty;
            CompanyCode = string.Empty;
            CompanyImage = null;
            Phone1 = string.Empty;
            Phone2 = null;
            Address = string.Empty;
            SubscriptionFee = -1;
            Currency = string.Empty;
            RegistrationDate = DateTime.MinValue;
            SubscriptionStatus = EN_SubscriptionStatus.Active;
            SubscriptionStartDate = DateTime.MinValue;
            SubscriptionEndDate = DateTime.MinValue;
            RemainingSubscriptionDays = -1;
            Description = null;
            IsPaid = EN_IsPaid.UnPaid;
            ByAdmin = -1;
            Action = string.Empty;

            _Mode = EN_Mode.New;
        }

        // For Update Company.
        public cls_Companies(int companyId, string managerFullName, string companyName, byte[]? companyImage,
            string phone1, string? phone2, string address, double subscriptionFee, string currency,
            bool subscriptionStatus, DateTime subscriptionStartDate, DateTime subscriptionEndDate, string? description,
            bool isPaid, int byAdmin)
        {
            CompanyId = companyId;
            ManagerFullName = managerFullName;
            CompanyName = companyName;
            CompanyCode = string.Empty;
            CompanyImage = companyImage;
            Phone1 = phone1;
            Phone2 = phone2;
            Address = address;
            SubscriptionFee = subscriptionFee;
            Currency = currency;
            RegistrationDate = DateTime.MinValue;
            SubscriptionStatus = subscriptionStatus ? EN_SubscriptionStatus.Active : EN_SubscriptionStatus.InActive;
            SubscriptionStartDate = subscriptionStartDate;
            SubscriptionEndDate = subscriptionEndDate;
            RemainingSubscriptionDays = -1;
            Description = description;
            IsPaid = isPaid ? EN_IsPaid.Paid : EN_IsPaid.UnPaid;
            ByAdmin = byAdmin;
            Action = string.Empty;

            _Mode = EN_Mode.Update;
        }

        // For Read Company
        private cls_Companies(int companyId, string managerFullName, string companyName, string companyCode, byte[]? companyImage,
            string phone1, string? phone2, string address, double subscriptionFee, string currency, DateTime registrationDate,
            bool subscriptionStatus, DateTime subscriptionStartDate, DateTime subscriptionEndDate, int remainingSubscriptionDays,
            string? description, bool isPaid, int byAdmin, string action)
        {
            CompanyId = companyId;
            ManagerFullName = managerFullName;
            CompanyName = companyName;
            CompanyCode = companyCode;
            CompanyImage = companyImage;
            Phone1 = phone1;
            Phone2 = phone2;
            Address = address;
            SubscriptionFee = subscriptionFee;
            Currency = currency;
            RegistrationDate = registrationDate;
            SubscriptionStatus = subscriptionStatus ? EN_SubscriptionStatus.Active : EN_SubscriptionStatus.InActive;
            SubscriptionStartDate = subscriptionStartDate;
            SubscriptionEndDate = subscriptionEndDate;
            RemainingSubscriptionDays = remainingSubscriptionDays;
            Description = description;
            IsPaid = isPaid ? EN_IsPaid.Paid : EN_IsPaid.UnPaid;
            ByAdmin = byAdmin;
            Action = action;

            this._Mode = EN_Mode.Update;
        }

        // Completed Testing.
        public bool ValidateCompanyObj()
        {
            if (!cls_validation.CheckLength(1, 50, ManagerFullName))
                return false;

            if (!cls_validation.CheckLength(1, 50, CompanyName))
                return false;

            if (!cls_validation.IsPhone1NumberValid(Phone1))
                return false;

            if (!cls_validation.IsPhone2NumberValid(Phone2))
                return false;

            if (!cls_validation.CheckLength(1, 150, Address))
                return false;

            if (!cls_validation.IsFloat(SubscriptionFee.ToString()))
                return false;

            if (!cls_validation.CheckLength(1, 14, Currency))
                return false;

            if (!string.IsNullOrEmpty(Description))
                if (!cls_validation.CheckLength(1, 250, Description))
                    return false;

            return true;
        }

        // Completed Testing.
        public static async Task<bool> IsCompanyCodeExistAsync(string companyCode)
        {
            return await cls_Companies_D.IsCompanyCodeExistAsync(companyCode);
        }

        // Completed Testing.
        public static async Task<bool> IsCompanyCodeExistWithOutCurrentCompanyAsync(int companyId, string companyCode)
        {
            return await cls_Companies_D.IsCompanyCodeExistWithOutCurrentCompanyAsync(companyId, companyCode);
        }

        // Completed Testing.
        public static async Task<bool> IsCompanyPhoneExistAsync(string companyPhone)
        {
            return await cls_Companies_D.IsCompanyPhoneExistAsync(companyPhone);
        }

        // Completed Testing.
        public static async Task<bool> IsCompanyPhoneExistWithOutCurrentCompanyAsync(int companyId, string companyPhone)
        {
            return await cls_Companies_D.IsCompanyPhoneExistWithOutCurrentCompanyAsync(companyId, companyPhone);
        }

        // Completed Testing.
        public static async Task<int> GetCompaniesCountAsync()
        {
            return await cls_Companies_D.GetCompaniesCountAsync();
        }

        // Completed Testing.
        public static async Task<bool> IsSubscriptionActiveAsync(int companyId)
        {
            return await cls_Companies_D.IsSubscriptionActiveAsync(companyId);
        }

        // Completed Testing.
        public static async Task<List<md_Companies>?> GetCompaniesAsync()
        {
            return await cls_Companies_D.GetCompaniesAsync();
        }

        // Completed Testing.
        public static async Task<cls_Companies?> GetCompanyByIdAsync(int companyId)
        {
            md_Company? company = await cls_Companies_D.GetCompanyByIdAsync(companyId);

            if (company == null)
                return null;

            return new cls_Companies
                (
                    company.CompanyId, company.ManagerFullName, company.CompanyName, company.CompanyCode, company.CompanyImage, company.Phone1,
                    company.Phone2, company.Address, company.SubscriptionFee, company.Currency, company.RegistrationDate, company.SubscriptionStatus,
                    company.SubscriptionStartDate, company.SubscriptionEndDate, company.RemainingSubscriptionDays, company.Description, company.IsPaid,
                    company.ByAdmin, company.Action
                );
        }

        // Completed Testing.
        public static async Task<List<md_Companies>?> GetActiveCompaniesAsync()
        {
            return await cls_Companies_D.GetActiveCompaniesAsync();
        }

        // Completed Testing.
        public static async Task<List<md_Companies>?> GetInActiveCompaniesAsync()
        {
            return await cls_Companies_D.GetInActiveCompaniesAsync();
        }

        // Completed Testing.
        public static async Task<List<md_Companies>?> GetCompaniesByAdminAsync(int adminId)
        {
            return await cls_Companies_D.GetCompaniesByAdminAsync(adminId);
        }

        // Completed Testing.
        public static async Task<bool> SetCompanyAsActiveAsync(int companyId, int adminId)
        {
            return await cls_Companies_D.SetCompanyAsActiveAsync(companyId, adminId);
        }

        // Completed Testing.
        public static async Task<bool> SetCompanyAsInActiveAsync(int companyId, int adminId)
        {
            return await cls_Companies_D.SetCompanyAsInActiveAsync(companyId, adminId);
        }

        // Completed Testing.
        public static async Task<bool> SetPaidStatusAsPaidAsync(int companyId, int adminId)
        {
            return await cls_Companies_D.SetPaidStatusAsPaidAsync(companyId, adminId);
        }

        // Completed Testing.
        public static async Task<bool> SetPaidStatusAsUnPaidAsync(int companyId, int adminId)
        {
            return await cls_Companies_D.SetPaidStatusAsUnPaidAsync(companyId, adminId);
        }

        // Completed Testing.
        private async Task<bool> _NewCompanyAsync()
        {

            // Check Company Code If Not Exist.
            do
            {

                CompanyCode = cls_Keys.GetKey(8, 1, cls_Keys.EN_KeyType.NumbersLetters);

            } while (await IsCompanyCodeExistAsync(CompanyCode));

            // Validate Company Object.
            if (!ValidateCompanyObj() || !cls_validation.CheckLength(8, 14, CompanyCode))
                return false;

            // Keep Company Data In Company Model For Insert It In Database.
            md_Company company = new md_Company
                (
                    CompanyId, ManagerFullName, CompanyName, CompanyCode, CompanyImage, Phone1, Phone2, Address, SubscriptionFee,
                    Currency, RegistrationDate, Convert.ToBoolean(SubscriptionStatus), SubscriptionStartDate, SubscriptionEndDate,
                    RemainingSubscriptionDays, Description, Convert.ToBoolean(IsPaid), ByAdmin, Action
                );

            // Insert Company In Database.
            int insertedId = await cls_Companies_D.NewCompanyAsync(company);

            // Get Inserted Id If The Company Is Inserted Successfully Or Get -1 If Not Inserted.
            CompanyId = insertedId > 0 ? insertedId : -1;

            // Return Is Company Inserted Successfully Or Not.
            return insertedId > 0;

        }

        // Completed Testing.
        private async Task<bool> _UpdateCompanyAsync()
        {
            if (!ValidateCompanyObj())
                return false;

            md_Company company = new md_Company
                (
                    CompanyId, ManagerFullName, CompanyName, CompanyCode, CompanyImage, Phone1, Phone2, Address, SubscriptionFee,
                    Currency, RegistrationDate, Convert.ToBoolean(SubscriptionStatus), SubscriptionStartDate, SubscriptionEndDate,
                    RemainingSubscriptionDays, Description, Convert.ToBoolean(IsPaid), ByAdmin, Action
                );

            return await cls_Companies_D.UpdateCompanyAsync(company);

        }

        // Completed Testing.
        public async Task<bool> SaveAsync()
        {
            switch (_Mode)
            {
                case EN_Mode.New:
                    if (await _NewCompanyAsync())
                    {
                        _Mode = EN_Mode.Update;
                        return true;
                    }
                    break;

                case EN_Mode.Update:
                    if (await _UpdateCompanyAsync())
                        return true;
                    break;
            }

            return false;
        }

        // Completed Testing.
        public static async Task<bool> DeleteCompanyAsync(int companyId)
        {
            return await cls_Companies_D.DeleteCompanyAsync(companyId);
        }

    }
}
