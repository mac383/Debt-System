using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.models.DebtRecords_model
{
    public class md_NewDebtRecord
    {
        public int CustomerId { get; set; }
        public string? Description { get; set; }
        public int ByUser { get; set; }
        public int CompanyId { get; set; }
        public DataTable DebtRecords_Products { get; set; }
        public md_NewDebtRecord(int customerId, string? description, int byUser, int companyId, DataTable debtRecord_Products)
        {
            CustomerId = customerId;
            Description = description;
            ByUser = byUser;
            CompanyId = companyId;
            DebtRecords_Products = debtRecord_Products;
        }
    }
}
