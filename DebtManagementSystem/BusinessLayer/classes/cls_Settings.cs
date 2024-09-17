using BusinessLayer.classes.validation;
using DataAccessLayer.models.Settings_models;
using DataAccessLayer.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.classes
{
    public class cls_Settings
    {
        public int SettingId { get; set; }
        public string CompanyName { get; set; }
        public string? Description { get; set; }
        public byte[]? Logo { get; set; }
        public string Currency { get; set; }
        public string? PaymentRequestMessage { get; set; }
        public int CompanyId { get; set; }

        public cls_Settings(string companyName, string? description, byte[]? logo,
            string currency, string? paymentRequestMessage, int companyId)
        {
            this.CompanyName = companyName;
            this.Description = description;
            this.Logo = logo;
            this.Currency = currency;
            this.PaymentRequestMessage = paymentRequestMessage;
            this.CompanyId = companyId;
        }

        private cls_Settings(int settingId, string companyName, string? description, byte[]? logo,
            string currency, string? paymentRequestMessage, int companyId)
        {
            this.SettingId = settingId;
            this.CompanyName = companyName;
            this.Description = description;
            this.Logo = logo;
            this.Currency = currency;
            this.PaymentRequestMessage = paymentRequestMessage;
            this.CompanyId = companyId;
        }

        // Completed Testing.
        public bool ValidateSettingObject()
        {

            string? companyName = this.CompanyName?.Trim();
            string? currency = this.Currency?.Trim();
            string? paymentRequestMessage = this.PaymentRequestMessage?.Trim();

            if (cls_validation.IsTextNullOrEmpty(companyName) || (companyName?.Length ?? 0) > 50)
                return false;

            if (cls_validation.IsTextNullOrEmpty(currency) || (currency?.Length ?? 0) > 14)
                return false;

            if (!cls_validation.IsTextNullOrEmpty(paymentRequestMessage) && (paymentRequestMessage?.Length ?? 0) > 250)
                return false;
            return true;
        }

        // Completed Testing.
        public static async Task<cls_Settings?> GetSettingsAsync(int companyId)
        {
            md_Setting? settings = await cls_Settings_D.GetSettingsAsync(companyId);
            return (settings == null) ? null : new cls_Settings
                (
                    settings.SettingId, settings.CompanyName, settings.Description, settings.Logo,
                    settings.Currency, settings.PaymentRequestMessage, settings.CompanyId
                );
        }

        // Completed Testing.
        public async Task<bool> UpdateSettingsAsync()
        {

            if (!ValidateSettingObject())
                return false;

            md_UpdateSetting settings = new md_UpdateSetting
                (
                    this.CompanyName, this.Description, this.Logo,
                    this.Currency, this.PaymentRequestMessage, this.CompanyId
                );
            return await cls_Settings_D.UpdateSettingsAsync(settings);
        }
    }
}
