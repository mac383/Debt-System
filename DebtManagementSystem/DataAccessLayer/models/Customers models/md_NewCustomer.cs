using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.models.Customers
{
    public class md_NewCustomer
    {
        public string? Address { get; set; }
        public string FullName { get; set; }
        public string Phone1 { get; set; }
        public string? Phone2 { get; set; }
        public string? TelegramID { get; set; }
        public int CompanyId { get; set; }
        public int? ByUser { get; set; }

        public md_NewCustomer(string? address, string fullName, string phone1, string? phone2, string? telegramId,
            int companyId, int? byUser)
        {
            Address = address;
            FullName = fullName;
            Phone1 = phone1;
            Phone2 = phone2;
            TelegramID = telegramId;
            CompanyId = companyId;
            ByUser = byUser;
        }

    }
}
