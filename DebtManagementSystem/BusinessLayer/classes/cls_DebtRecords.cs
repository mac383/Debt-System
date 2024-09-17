using BusinessLayer.classes.validation;
using DataAccessLayer.models.DebtRecords_model;
using DataAccessLayer.repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.classes
{
    public class cls_DebtRecords
    {
        enum EN_Mode { New = 1, Update = 2 }
        public enum EN_IsPaid { Paid = 1, UnPaid = 0 }

        EN_Mode _Mode;

        public int DebtRecordId { get; set; }
        public double TotalPrice { get; set; }
        public double RemainingAmount { get; set; }
        public EN_IsPaid IsPaid { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string? Description { get; set; }
        public int CompanyId { get; set; }

        public cls_Customers CustomerObj { get; set; }
        public cls_Users UserObj { get; set; }


        // for add new debt record.
        public cls_DebtRecords()
        {
            DebtRecordId = -1;
            TotalPrice = -1;
            RemainingAmount = -1;
            IsPaid = EN_IsPaid.UnPaid;
            RegistrationDate = DateTime.Now;
            Description = null;
            CompanyId = -1;

            CustomerObj = new cls_Customers { CustomerId = -1 };
            UserObj = new cls_Users { UserId = -1 };

            _Mode = EN_Mode.New;
        }

        // for update debt record.
        public cls_DebtRecords(int debtRecordId, int customerId, string description, int byUser, int companyId)
        {
            DebtRecordId = debtRecordId;
            Description = description;
            CompanyId = companyId;

            CustomerObj = new cls_Customers { CustomerId = customerId };
            UserObj = new cls_Users { UserId = byUser };

            _Mode = EN_Mode.Update;
        }

        // for read debt record.
        private cls_DebtRecords(int debtRecordId, cls_Customers customer, double totalPrice, double remainingAmount, bool isPaid,
            DateTime registrationDate, string description, cls_Users user, int companyId)
        {
            DebtRecordId = debtRecordId;
            CustomerObj = customer;
            TotalPrice = totalPrice;
            RemainingAmount = remainingAmount;
            IsPaid = isPaid ? EN_IsPaid.Paid : EN_IsPaid.UnPaid;
            RegistrationDate = registrationDate;
            Description = description;
            UserObj = user;
            CompanyId = companyId;
        }

        public static async Task<cls_DebtRecords?> GetDebtRecordByIdAsync(int debtRecordId, int companyId)
        {
            // read debt record by id.
            md_DebtRecord? record = await cls_DebtRecords_D.GetDebtRecordByIdAsync(debtRecordId, companyId);

            if (record == null) return null;

            // read customer and user objects.
            cls_Customers? customer = await cls_Customers.GetCustomerByIdAsync(record.CustomerId, record.CompanyId);
            cls_Users? user = await cls_Users.GetUserByIdAsync(record.ByUser, record.CompanyId);

            // return debt record.
            return new cls_DebtRecords
                (
                    record.DebtRecordId, customer, record.TotalPrice, record.RemainingAmount, record.IsPaid, record.RegistrationDate,
                    record.Description, user, record.CompanyId
                );
        }


        public static async Task<List<md_DebtRecords>?> GetDebtRecordsAsync(int companyId)
        {
            return await cls_DebtRecords_D.GetDebtRecordsAsync(companyId);
        }

        public static async Task<List<md_DebtRecords>?> GetPaidDebtRecordsAsync(int companyId)
        {
            return await cls_DebtRecords_D.GetPaidDebtRecordsAsync(companyId);
        }

        public static async Task<List<md_DebtRecords>?> GetUnPaidDebtRecordsAsync(int companyId)
        {
            return await cls_DebtRecords_D.GetUnPaidDebtRecordsAsync(companyId);
        }

        public static async Task<int> GetDebtRecordsCountAsync(int companyId)
        {
            return await cls_DebtRecords_D.GetDebtRecordsCountAsync(companyId);
        }

        public static async Task<double> GetTotalDebtAsync(int companyId)
        {
            return await cls_DebtRecords_D.GetTotalDebtAsync(companyId);
        }

        public static async Task<bool> IsDebtRecordHasRelationsAsync(int debtRecordId, int companyId)
        {
            return await cls_DebtRecords_D.IsDebtRecordHasRelationsAsync(debtRecordId, companyId);
        }

        public static async Task<bool> DeleteDebtRecordAsync(int debtRecordId, int companyId)
        {
            return await cls_DebtRecords_D.DeleteDebtRecordAsync(debtRecordId, companyId);
        }


        private async Task<bool> _NewDebtRecordAsync(DataTable details)
        {
            if (!string.IsNullOrEmpty(Description))
                if (!cls_validation.CheckLength(1, 250, Description))
                    return false;

            md_NewDebtRecord record = new md_NewDebtRecord
                (
                    CustomerObj.CustomerId, Description, UserObj.UserId, CompanyId, details
                );

            this.DebtRecordId = await cls_DebtRecords_D.NewDebtRecordAsync(record);

            return this.DebtRecordId > 0;
        }

        private async Task<bool> _UpdateDebtRecordAsync(DataTable details)
        {
            if (!string.IsNullOrEmpty(Description))
                if (!cls_validation.CheckLength(1, 250, Description))
                    return false;

            md_UpdateDebtRecord record = new md_UpdateDebtRecord
                (
                    DebtRecordId, CustomerObj.CustomerId, Description, UserObj.UserId, CompanyId, details
                );

            return await cls_DebtRecords_D.UpdateDebtRecordAsync(record);
        }

        public async Task<bool> SaveAsync(DataTable details)
        {
            switch (_Mode)
            {
                case EN_Mode.New:
                    if (await _NewDebtRecordAsync(details))
                    {
                        _Mode = EN_Mode.Update;
                        return true;
                    }
                    break;

                case EN_Mode.Update:
                    if (await _UpdateDebtRecordAsync(details))
                        return true;
                    break;
            }

            return false;
        }

    }
}
