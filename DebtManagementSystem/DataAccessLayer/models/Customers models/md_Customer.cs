using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.models.Customers
{
    public class md_Customer
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string Phone1 { get; set; }
        public string? Phone2 { get; set; }
        public string CustomerCode { get; set; }
        public string? Address { get; set; }
        public bool CustomerStatus { get; set; }
        public string? TelegramId { get; set; }
        public int? ByUser { get; set; }
        public int CompanyId { get; set; }

        public md_Customer (int customerId, string fullName, string phone1, string? phone2, string customerCode,
            string? address, bool customerStatus, string? telegramId, int? byUser, int companyId)
        {
            this.CustomerId = customerId;
            this.FullName = fullName;
            this.Phone1 = phone1;
            this.Phone2 = phone2;
            this.CustomerCode = customerCode;
            this.Address = address;
            this.CustomerStatus = customerStatus;
            this.TelegramId = telegramId;
            this.ByUser = byUser;
            this.CompanyId = companyId;
        }
    }
}
