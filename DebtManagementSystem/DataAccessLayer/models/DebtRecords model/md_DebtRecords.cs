using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.models.DebtRecords_model
{
    public class md_DebtRecords
    {
        public int DebtRecordId { get; set; }
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public double TotalPrice { get; set; }
        public double RemainingAmount { get; set; }
        public string IsPaid { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string? Description { get; set; }
        public string ByUser { get; set; }

        public md_DebtRecords(int debtRecordId, int customerId, string fullName, double totalPrice, double remainingAmount, string isPaid,
            DateTime registrationDate, string? description, string byUser)
        {
            DebtRecordId = debtRecordId;
            CustomerId = customerId;
            FullName = fullName;
            TotalPrice = totalPrice;
            RemainingAmount = remainingAmount;
            IsPaid = isPaid;
            RegistrationDate = registrationDate;
            Description = description;
            ByUser = byUser;
        }
    }
}
