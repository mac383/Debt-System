using DataAccessLayer.models.DebtRecordsProducts_models;
using DataAccessLayer.repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.classes
{
    public class cls_DebtRecordsProducts
    {
        // Completed Testing.
        public static async Task<List<md_DebtRecordsProducts>?> GetDebtRecordsProductsAsync(int debtRecordId, int companyId)
        {
            return await cls_DebtRecordsProducts_D.GetDebtRecordsProductsAsync(debtRecordId, companyId);
        }
    }
}
