using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.models.Customers_models
{
    public class md_UpdateCustomer
    {
        public int CustomerId { get; set; }
        public string? Address { get; set; }
        public bool CustomerStatus { get; set; }
        public string FullName { get; set; }
        public string Phone1 { get; set; }
        public string? Phone2 { get; set; }
        public string? TelegramID { get; set; }
        public int CompanyId { get; set; }
        public int? ByUser { get; set; }

        public md_UpdateCustomer(int customerId, string? address, bool customerStatus, string fullName, string phone1, string? phone2, string? telegramId,
            int companyId, int? byUser)
        {
            CustomerId = customerId;
            Address = address;
            CustomerStatus = customerStatus;
            FullName = fullName;
            Phone1 = phone1;
            Phone2 = phone2;
            TelegramID = telegramId;
            CompanyId = companyId;
            ByUser = byUser;
        }
    }
}
