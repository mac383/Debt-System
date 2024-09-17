using BusinessLayer.classes.validation;
using DataAccessLayer.models.PaidRecords_models;
using DataAccessLayer.repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.classes
{
    public class cls_PaidRecords
    {
        enum EN_Mode { New = 1, Update = 2 };
        EN_Mode _mode;

        public int PaidRecordId { get; set; }
        public int DebtRecordId { get; set; }
        public double PaymentAmount { get; set; }
        public string? Description { get; set; }
        public int ByUser { get; set; }
        public int CompanyId { get; set; }


        public cls_PaidRecords()
        {
            PaidRecordId = -1;
            DebtRecordId = -1;
            PaymentAmount = -1;
            Description = null;
            ByUser = -1;
            CompanyId = -1;

            _mode = EN_Mode.New;
        }

        public cls_PaidRecords(int paidRecordId, double paymentAmount, string? description, int byUser, int companyId)
        {
            PaidRecordId = paidRecordId;
            DebtRecordId = -1;
            PaymentAmount = paymentAmount;
            Description = description;
            ByUser = byUser;
            CompanyId = companyId;

            _mode = EN_Mode.Update;
        }

        private cls_PaidRecords(int paidRecordId, int debtRecordId, double paymentAmount, string? description, int byUser, int companyId)
        {
            PaidRecordId = paidRecordId;
            DebtRecordId = debtRecordId;
            PaymentAmount = paymentAmount;
            Description = description;
            ByUser = byUser;
            CompanyId = companyId;

            _mode = EN_Mode.Update;
        }

        public static async Task<int> GetPaidRecordsCountByDebtRecordIdAsync(int debtRecordId, int companyId)
        {
            return await cls_PaidRecords_D.GetPaidRecordsCountByDebtRecordIdAsync(debtRecordId, companyId);
        }


        public static async Task<List<md_PaidRecords>?> GetPaidRecordsAsync(int companyId)
        {
            return await cls_PaidRecords_D.GetPaidRecordsAsync(companyId);
        }


        public static async Task<List<md_PaidRecords>?> GetPaidRecordsByDebtRecordIdAsync(int debtRecordId, int companyId)
        {
            return await cls_PaidRecords_D.GetPaidRecordsByDebtRecordIdAsync(debtRecordId, companyId);
        }


        public bool ValidatePaidRecordObj()
        {
            if (!string.IsNullOrEmpty(this.Description))
                if (!cls_validation.CheckLength(1, 250, this.Description))
                    return false;

            if (PaymentAmount <= 0)
                return false;

            return true;
        }

        private async Task<bool> _NewPaidAsync()
        {

            if (!ValidatePaidRecordObj())
                return false;

            md_NewPaid paid = new md_NewPaid(DebtRecordId, PaymentAmount, Description, ByUser, CompanyId);
            this.PaidRecordId = await cls_PaidRecords_D.NewPaidAsync(paid);

            return this.PaidRecordId > 0;
        }


        private async Task<bool> _UpdatePaidAsync()
        {
            md_UpdatePaid paid = new md_UpdatePaid(PaidRecordId, PaymentAmount, Description, ByUser, CompanyId);
            return await cls_PaidRecords_D.UpdatePaidAsync(paid);
        }


        public async Task<bool> SaveAsync()
        {
            switch (_mode)
            {
                case EN_Mode.New:
                    if (await _NewPaidAsync())
                    {
                        _mode = EN_Mode.Update;
                        return true;
                    }
                    break;

                case EN_Mode.Update:
                    if (await _UpdatePaidAsync())
                        return true;
                    break;
            }

            return false;
        }

    }
}
