using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.models.PaidRecords_models
{
    public class md_PaidRecords
    {
        public int PaidRecordId { get; set; }
        public int DebtRecordId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public double TotalPrice { get; set; }
        public double RemainingAmount { get; set; }
        public string IsPaid { get; set; }
        public string PaymentType { get; set; }
        public double PaymentAmount { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string? Description { get; set; }
        public string ByUser { get; set; }

        public md_PaidRecords(int paidRecordId, int debtRecordId, int customerId, string customerName, double totalPrice, double remainingAmount,
            string isPaid, string paymentType, double paymentAmount, DateTime registrationDate, string? description, string byUser)
        {
            PaidRecordId = paidRecordId;
            DebtRecordId = debtRecordId;
            CustomerId = customerId;
            CustomerName = customerName;
            TotalPrice = totalPrice;
            RemainingAmount = remainingAmount;
            IsPaid = isPaid;
            PaymentType = paymentType;
            PaymentAmount = paymentAmount;
            RegistrationDate = registrationDate;
            Description = description;
            ByUser = byUser;
        }
    }
}
