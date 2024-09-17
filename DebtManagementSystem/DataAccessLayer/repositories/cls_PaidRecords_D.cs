using DataAccessLayer.database;
using DataAccessLayer.models.PaidRecords_models;
using DataAccessLayer.models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.repositories
{
    public class cls_PaidRecords_D
    {

        public static async Task<int> GetPaidRecordsCountByDebtRecordIdAsync(int debtRecordId, int companyId)
        {
            int count = 0;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT [dbo].[PAIDRECORDS_FUN_GetPaidRecordsCountByDebtRecordId] (@debtRecordId, @companyId)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@debtRecordId", SqlDbType.Int) { Value = debtRecordId });
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });

                        // فتح الاتصال بقاعدة البيانات
                        await connection.OpenAsync();

                        // تنفيذ الاستعلام وقراءة ناتجه
                        object? returnValue = await command.ExecuteScalarAsync();

                        // اذا كانت ناتج الاستعلام لا يساوي null
                        if (returnValue != null && returnValue != DBNull.Value)
                            count = Convert.ToInt32(returnValue);
                    }
                }
            }
            catch (Exception ex)
            {
                // جمع المعلومات عن الخطا
                string Parameters = cls_Errors_D.GetParams
                (
                    () => debtRecordId,
                    () => companyId
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_PaidRecords_D", "GetPaidRecordsCountByDebtRecordIdAsync", ex.StackTrace,
                    companyId, "Get Paid Records Count By Debt Record Id", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return -1;
            }

            return count;
        }


        public static async Task<List<md_PaidRecords>?> GetPaidRecordsAsync(int companyId)
        {
            List<md_PaidRecords> paidRecords = new List<md_PaidRecords>();

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT * FROM [dbo].[PAIDRECORDS_FUN_GetPaidRecords] (@companyId)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // إضافة المعامل
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });

                        // فتح الاتصال
                        await connection.OpenAsync();

                        // تنفيذ الاستعلام
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                paidRecords.Add
                                    (
                                        new md_PaidRecords
                                        (
                                            reader.GetInt32(reader.GetOrdinal("PaidRecordId")),
                                            reader.GetInt32(reader.GetOrdinal("DebtRecordId")),
                                            reader.GetInt32(reader.GetOrdinal("CustomerId")),
                                            reader.GetString(reader.GetOrdinal("CustomerName")),
                                            Convert.ToDouble(reader.GetDecimal(reader.GetOrdinal("TotalPrice"))),
                                            Convert.ToDouble(reader.GetDecimal(reader.GetOrdinal("RemainingAmount"))),
                                            reader.GetString(reader.GetOrdinal("IsPaid")),
                                            reader.GetString(reader.GetOrdinal("PaymentType")),
                                            Convert.ToDouble(reader.GetDecimal(reader.GetOrdinal("PaymentAmount"))),
                                            reader.GetDateTime(reader.GetOrdinal("RegistrationDate")),
                                            reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader["Description"].ToString(),
                                            reader.GetString(reader.GetOrdinal("ByUser"))
                                        )
                                    );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // جمع المعلومات الخاصة بالخطأ
                string Parameters = cls_Errors_D.GetParams
                (
                    () => companyId
                );

                // تسجيل الخطأ
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_PaidRecords_D", "GetPaidRecordsAsync", ex.StackTrace,
                    companyId, "Get All Paid Records", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return null;
            }

            return paidRecords.Count > 0 ? paidRecords : null;
        }

        public static async Task<List<md_PaidRecords>?> GetPaidRecordsByDebtRecordIdAsync(int debtRecordId, int companyId)
        {
            List<md_PaidRecords> paidRecords = new List<md_PaidRecords>();

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT * FROM [dbo].[PAIDRECORDS_FUN_GetPaidRecordsByDebtRecordId] (@debtRecordId, @companyId)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // إضافة المعامل
                        command.Parameters.Add(new SqlParameter("@debtRecordId", SqlDbType.Int) { Value = debtRecordId });
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });

                        // فتح الاتصال
                        await connection.OpenAsync();

                        // تنفيذ الاستعلام
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                paidRecords.Add
                                    (
                                        new md_PaidRecords
                                        (
                                            reader.GetInt32(reader.GetOrdinal("PaidRecordId")),
                                            reader.GetInt32(reader.GetOrdinal("DebtRecordId")),
                                            reader.GetInt32(reader.GetOrdinal("CustomerId")),
                                            reader.GetString(reader.GetOrdinal("CustomerName")),
                                            Convert.ToDouble(reader.GetDecimal(reader.GetOrdinal("TotalPrice"))),
                                            Convert.ToDouble(reader.GetDecimal(reader.GetOrdinal("RemainingAmount"))),
                                            reader.GetString(reader.GetOrdinal("IsPaid")),
                                            reader.GetString(reader.GetOrdinal("PaymentType")),
                                            Convert.ToDouble(reader.GetDecimal(reader.GetOrdinal("PaymentAmount"))),
                                            reader.GetDateTime(reader.GetOrdinal("RegistrationDate")),
                                            reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader["Description"].ToString(),
                                            reader.GetString(reader.GetOrdinal("ByUser"))
                                        )
                                    );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // جمع المعلومات الخاصة بالخطأ
                string Parameters = cls_Errors_D.GetParams
                (
                    () => debtRecordId,
                    () => companyId
                );

                // تسجيل الخطأ
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_PaidRecords_D", "GetPaidRecordsByDebtRecordIdAsync", ex.StackTrace,
                    companyId, "Get Paid Records By Debt Record Id", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return null;
            }

            return paidRecords.Count > 0 ? paidRecords : null;
        }


        public static async Task<int> NewPaidAsync(md_NewPaid paid)
        {
            int insertedId = -1;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"[dbo].[PAIDRECORDS_SP_NewPaid]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // تحديد نوع الاستعلام
                        command.CommandType = CommandType.StoredProcedure;

                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@debtRecordId", SqlDbType.Int) { Value = paid.DebtRecordId });
                        command.Parameters.Add(new SqlParameter("@paymentAmount", SqlDbType.Int) { Value = paid.PaymentAmount });
                        command.Parameters.Add(new SqlParameter("@description", SqlDbType.NVarChar, 250) { Value = paid.Description ?? (object)DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@byUser", SqlDbType.Int) { Value = paid.ByUser });
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = paid.CompanyId });

                        // اضافة المعامل الخاص بقيمة الارجاع
                        SqlParameter returnParameter = command.Parameters.Add("returnValue", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;

                        // فتح الاتصال وتنفيذ الاستعلام
                        await connection.OpenAsync();
                        await command.ExecuteScalarAsync();

                        // الحصول علئ قيمة الارجاع
                        insertedId = (int)returnParameter.Value;
                    }
                }
            }

            catch (Exception ex)
            {
                // جمع المعلومات عن الخطا
                string Parameters = cls_Errors_D.GetParams
                (
                    () => paid.DebtRecordId,
                    () => paid.PaymentAmount,
                    () => paid.Description,
                    () => paid.ByUser,
                    () => paid.CompanyId
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_PaidRecords_D", "NewPaidAsync", ex.StackTrace,
                    paid.CompanyId, "Insert New Paid Record", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return -1;
            }

            return insertedId;
        }


        public static async Task<bool> UpdatePaidAsync(md_UpdatePaid paid)
        {
            int rowsAffected = -1;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"[dbo].[PAIDRECORDS_SP_UpdatePaidRecord]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // تحديد نوع الاستعلام
                        command.CommandType = CommandType.StoredProcedure;

                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@paidRecordId", SqlDbType.Int) { Value = paid.PaidRecordId });
                        command.Parameters.Add(new SqlParameter("@paymentAmount", SqlDbType.Int) { Value = paid.PaymentAmount });
                        command.Parameters.Add(new SqlParameter("@description", SqlDbType.NVarChar, 250) { Value = paid.Description ?? (object)DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@byUser", SqlDbType.Int) { Value = paid.ByUser });
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = paid.CompanyId });

                        // اضافة المعامل الخاص بقيمة الارجاع
                        SqlParameter returnParameter = command.Parameters.Add("returnValue", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;

                        // فتح الاتصال وتنفيذ الاستعلام
                        await connection.OpenAsync();
                        await command.ExecuteScalarAsync();

                        // الحصول علئ قيمة الارجاع
                        rowsAffected = (int)returnParameter.Value;
                    }
                }
            }

            catch (Exception ex)
            {
                // جمع المعلومات عن الخطا
                string Parameters = cls_Errors_D.GetParams
                (
                    () => paid.PaidRecordId,
                    () => paid.PaymentAmount,
                    () => paid.Description,
                    () => paid.ByUser,
                    () => paid.CompanyId
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_PaidRecords_D", "UpdatePaidAsync", ex.StackTrace,
                    paid.CompanyId, "Update Paid Record", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return rowsAffected > 0;
        }

    }
}
