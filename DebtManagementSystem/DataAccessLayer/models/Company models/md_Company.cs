using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.models
{
    public class md_Company
    {

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
        public bool SubscriptionStatus { get; set; }
        public DateTime SubscriptionStartDate { get; set; }
        public DateTime SubscriptionEndDate { get; set; }
        public int RemainingSubscriptionDays { get; set; }
        public string? Description { get; set; }
        public bool IsPaid { get; set; }
        public int ByAdmin { get; set; }
        public string Action { get; set; }

        public md_Company(int companyId, string managerFullName, string companyName, string companyCode, byte[]? companyImage,
            string phone1, string? phone2, string address, double subscriptionFee, string currency, DateTime registrationDate,
            bool subscriptionStatus, DateTime subscriptionStartDate, DateTime subscriptionEndDate, int remainingSubscriptionDays, string? description,
            bool isPaid, int byAdmin, string action)
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
            SubscriptionStatus = subscriptionStatus;
            SubscriptionStartDate = subscriptionStartDate;
            SubscriptionEndDate = subscriptionEndDate;
            RemainingSubscriptionDays = remainingSubscriptionDays;
            Description = description;
            IsPaid = isPaid;
            ByAdmin = byAdmin;
            Action = action;
        }

    }
}
