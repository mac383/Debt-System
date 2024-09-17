using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.models.Customers
{
    public class md_Customers
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string Phone1 { get; set; }
        public string? Phone2 { get; set; }
        public string CustomerCode { get; set; }
        public string? Address { get; set; }
        public string CustomerStatus { get; set; }
        public string? TelegramId { get; set; }
        public string? ByUser { get; set; }

        public md_Customers(int customerId, string fullName, string phone1, string? phone2, string customerCode,
            string? address, string customerStatus, string? telegramId, string? byUser)
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
        }
    }
}
