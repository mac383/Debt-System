using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.models.PaidRecords_models
{
    public class md_UpdatePaid
    {
        public int PaidRecordId { get; set; }
        public double PaymentAmount { get; set; }
        public string? Description { get; set; }
        public int ByUser { get; set; }
        public int CompanyId { get; set; }

        public md_UpdatePaid(int paidRecordId, double paymentAmount, string? description, int byUser, int companyId)
        {
            PaidRecordId = paidRecordId;
            PaymentAmount = paymentAmount;
            Description = description;
            ByUser = byUser;
            CompanyId = companyId;
        }
    }
}
