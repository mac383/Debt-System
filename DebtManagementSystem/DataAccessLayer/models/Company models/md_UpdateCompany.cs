using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.models.Company_models
{
    public class md_UpdateCompany
    {
        public int CompanyId { get; set; }
        public string ManagerFullName { get; set; }
        public string CompanyName { get; set; }
        public byte[]? CompanyImage { get; set; }
        public string Phone1 { get; set; }
        public string? Phone2 { get; set; }
        public string Address { get; set; }
        public double SubscriptionFee { get; set; }
        public string Currency { get; set; }
        public bool SubscriptionStatus { get; set; }
        public DateTime SubscriptionStartDate { get; set; }
        public DateTime SubscriptionEndDate { get; set; }
        public string? Description { get; set; }
        public bool IsPaid { get; set; }
        public int ByAdmin { get; set; }

        public md_UpdateCompany(int companyId, string managerFullName, string companyName, byte[]? companyImage,
            string phone1, string? phone2, string address, double subscriptionFee, string currency,
            bool subscriptionStatus, DateTime subscriptionStartDate, DateTime subscriptionEndDate, string? description,
            bool isPaid, int byAdmin)
        {
            CompanyId = companyId;
            ManagerFullName = managerFullName;
            CompanyName = companyName;
            CompanyImage = companyImage;
            Phone1 = phone1;
            Phone2 = phone2;
            Address = address;
            SubscriptionFee = subscriptionFee;
            Currency = currency;
            SubscriptionStatus = subscriptionStatus;
            SubscriptionStartDate = subscriptionStartDate;
            SubscriptionEndDate = subscriptionEndDate;
            Description = description;
            IsPaid = isPaid;
            ByAdmin = byAdmin;
        }
    }
}
