using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.models.Settings_models
{
    public class md_Setting
    {
        public int SettingId { get; set; }
        public string CompanyName { get; set; }
        public string? Description { get; set; }
        public byte[]? Logo { get; set; }
        public string Currency { get; set; }
        public string? PaymentRequestMessage { get; set; }
        public int CompanyId { get; set; }

        public md_Setting(int settingId, string companyName, string? description, byte[]? logo, string currency, string? paymentRequestMessage, int companyId)
        {
            this.SettingId = settingId;
            this.CompanyName = companyName;
            this.Description = description;
            this.Logo = logo;
            this.Currency = currency;
            this.PaymentRequestMessage = paymentRequestMessage;
            this.CompanyId = companyId;
        }

    }
}
