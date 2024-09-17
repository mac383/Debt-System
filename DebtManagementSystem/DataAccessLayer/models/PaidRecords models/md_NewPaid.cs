using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.models.PaidRecords_models
{
    public class md_NewPaid
    {
        public int DebtRecordId { get; set; }
        public double PaymentAmount { get; set; }
        public string? Description { get; set; }
        public int ByUser { get; set; }
        public int CompanyId { get; set; }

        public md_NewPaid(int debtRecordId, double paymentAmount, string? description, int byUser, int companyId)
        {
            DebtRecordId = debtRecordId;
            PaymentAmount = paymentAmount;
            Description = description;
            ByUser = byUser;
            CompanyId = companyId;
        }
    }
}
