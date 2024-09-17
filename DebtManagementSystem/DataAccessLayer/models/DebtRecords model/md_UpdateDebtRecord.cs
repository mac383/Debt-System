using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.models.DebtRecords_model
{
    public class md_UpdateDebtRecord
    {
        public int DebtRecordId { get; set; }
        public int CustomerId { get; set; }
        public string? Description { get; set; }
        public int ByUser { get; set; }
        public int CompanyId { get; set; }
        public DataTable DebtRecord_Products { get; set; }

        public md_UpdateDebtRecord(int debtRecordId, int customerId, string? description, int byUser, int companyId, DataTable debtRecord_Product)
        {
            DebtRecordId = debtRecordId;
            CustomerId = customerId;
            Description = description;
            ByUser = byUser;
            CompanyId = companyId;
            DebtRecord_Products = debtRecord_Product;
        }
    }
}
