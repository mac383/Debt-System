using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.models.DebtRecords_model
{
    public class md_DebtRecord
    {
        public int DebtRecordId { get; set; }
        public int CustomerId { get; set; }
        public double TotalPrice { get; set; }
        public double RemainingAmount { get; set; }
        public bool IsPaid { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string? Description { get; set; }
        public int ByUser { get; set; }
        public int CompanyId { get; set; }

        public md_DebtRecord(int debtRecordId, int customerId, double totalPrice, double remainingAmount, bool isPaid,
            DateTime registrationDate, string? description, int byUser, int companyId)
        {
            DebtRecordId = debtRecordId;
            CustomerId = customerId;
            TotalPrice = totalPrice;
            RemainingAmount = remainingAmount;
            IsPaid = isPaid;
            RegistrationDate = registrationDate;
            Description = description;
            ByUser = byUser;
            CompanyId = companyId;
        }
    }
}
