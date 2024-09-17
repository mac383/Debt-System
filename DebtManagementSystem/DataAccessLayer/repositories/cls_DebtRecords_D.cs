using DataAccessLayer.database;
using DataAccessLayer.models.DebtRecords_model;
using DataAccessLayer.models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DataAccessLayer.repositories
{
    public class cls_DebtRecords_D
    {

        public static async Task<md_DebtRecord?> GetDebtRecordByIdAsync(int debtRecordId, int companyId)
        {
            md_DebtRecord? record = null;

            try
            {
                using (SqlConnection connectin = cls_database.Connection())
                {
                    string query = @"SELECT * FROM [dbo].[DEBTRECORDS_FUN_GetDebtRecordById] (@debtRecordId, @companyId)";

                    using (SqlCommand command = new SqlCommand(query, connectin))
                    {
                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@debtRecordId", SqlDbType.Int) { Value = debtRecordId });
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });

                        // فتح الاتصال بقاعدة البيانات
                        await connectin.OpenAsync();

                        // تنفيذ الاستعلام
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                // The record was found
                                // قراءة البيانات وحفضها في debt record model
                                record = new md_DebtRecord
                                (
                                    Convert.ToInt32(reader["DebtRecordId"]),
                                    Convert.ToInt32(reader["CustomerId"]),
                                    Convert.ToDouble(reader["TotalPrice"]),
                                    Convert.ToDouble(reader["RemainingAmount"]),
                                    Convert.ToBoolean(reader["IsPaid"]),
                                    Convert.ToDateTime(reader["RegistrationDate"]),
                                    reader["Description"].ToString(),
                                    Convert.ToInt32(reader["ByUser"]),
                                    Convert.ToInt32(reader["CompanyId"])
                                );
                            }
                        }
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
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_DebtRecords_D", "GetDebtRecordByIdAsync", ex.StackTrace,
                    companyId, "Get DebtRecord By Id", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return null;
            }

            return record;
        }

        public static async Task<List<md_DebtRecords>?> GetDebtRecordsAsync(int companyId)
        {
            List<md_DebtRecords> debtRecords = new List<md_DebtRecords>();

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT * FROM [dbo].[DEBTRECORDS_FUN_GetDebtRecords] (@companyId)";

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
                                debtRecords.Add
                                    (
                                        new md_DebtRecords
                                        (
                                            Convert.ToInt32(reader["DebtRecordId"]),
                                            Convert.ToInt32(reader["CustomerId"]),
                                            reader.GetString(reader.GetOrdinal("FullName")),
                                            Convert.ToDouble(reader["TotalPrice"]),
                                            Convert.ToDouble(reader["RemainingAmount"]),
                                            reader.GetString(reader.GetOrdinal("IsPaid")),
                                            Convert.ToDateTime(reader["RegistrationDate"]),
                                            reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
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
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_DebtRecords_D", "GetDebtRecordsAsync", ex.StackTrace,
                    companyId, "Get Debt Records", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return null;
            }

            return debtRecords.Count > 0 ? debtRecords : null;
        }


        public static async Task<List<md_DebtRecords>?> GetPaidDebtRecordsAsync(int companyId)
        {
            List<md_DebtRecords> debtRecords = new List<md_DebtRecords>();

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT * FROM [dbo].[DEBTRECORDS_FUN_GetPaidDebtRecords] (@companyId)";

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
                                debtRecords.Add
                                    (
                                        new md_DebtRecords
                                        (
                                            Convert.ToInt32(reader["DebtRecordId"]),
                                            Convert.ToInt32(reader["CustomerId"]),
                                            reader.GetString(reader.GetOrdinal("FullName")),
                                            Convert.ToDouble(reader["TotalPrice"]),
                                            Convert.ToDouble(reader["RemainingAmount"]),
                                            reader.GetString(reader.GetOrdinal("IsPaid")),
                                            Convert.ToDateTime(reader["RegistrationDate"]),
                                            reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
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
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_DebtRecords_D", "GetPaidDebtRecordsAsync", ex.StackTrace,
                    companyId, "Get Paid Debt Records", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return null;
            }

            return debtRecords.Count > 0 ? debtRecords : null;
        }


        public static async Task<List<md_DebtRecords>?> GetUnPaidDebtRecordsAsync(int companyId)
        {
            List<md_DebtRecords> debtRecords = new List<md_DebtRecords>();

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT * FROM [dbo].[DEBTRECORDS_FUN_GetUnPaidDebtRecords] (@companyId)";

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
                                debtRecords.Add
                                    (
                                        new md_DebtRecords
                                        (
                                            Convert.ToInt32(reader["DebtRecordId"]),
                                            Convert.ToInt32(reader["CustomerId"]),
                                            reader.GetString(reader.GetOrdinal("FullName")),
                                            Convert.ToDouble(reader["TotalPrice"]),
                                            Convert.ToDouble(reader["RemainingAmount"]),
                                            reader.GetString(reader.GetOrdinal("IsPaid")),
                                            Convert.ToDateTime(reader["RegistrationDate"]),
                                            reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
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
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_DebtRecords_D", "GetUnPaidDebtRecordsAsync", ex.StackTrace,
                    companyId, "Get UnPaid Debt Records", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return null;
            }

            return debtRecords.Count > 0 ? debtRecords : null;
        }


        public static async Task<int> GetDebtRecordsCountAsync(int companyId)
        {
            int count = 0;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT [dbo].[DEBTRECORDS_FUN_GetDebtRecordsCount](@companyId)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // اضافة المعلمات
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
                    () => companyId
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_DebtRecords_D", "GetDebtRecordsCountAsync", ex.StackTrace,
                    companyId, "Get Debt Records Count", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return -1;
            }

            return count;
        }


        public static async Task<double> GetTotalDebtAsync(int companyId)
        {
            double total = 0;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT [dbo].[DEBTRECORDS_FUN_GetTotalDebt](@companyId)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });

                        // فتح الاتصال بقاعدة البيانات
                        await connection.OpenAsync();

                        // تنفيذ الاستعلام وقراءة ناتجه
                        object? returnValue = await command.ExecuteScalarAsync();

                        // اذا كانت ناتج الاستعلام لا يساوي null
                        if (returnValue != null && returnValue != DBNull.Value)
                            total = Convert.ToDouble(returnValue);
                    }
                }
            }
            catch (Exception ex)
            {
                // جمع المعلومات عن الخطا
                string Parameters = cls_Errors_D.GetParams
                (
                    () => companyId
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_DebtRecords_D", "GetTotalDebtAsync", ex.StackTrace,
                    companyId, "Get Total Debt", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return -1;
            }

            return total;
        }


        public static async Task<bool> IsDebtRecordHasRelationsAsync(int debtRecordId, int companyId)
        {
            bool hasRelations = false;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT [dbo].[DEBTRECORDS_FUN_IsDebtRecordsHasRelations] (@debtRecordId, @companyId)";

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
                            hasRelations = Convert.ToBoolean(returnValue);
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
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_DebtRecords_D", "IsDebtRecordHasRelationsAsync", ex.StackTrace,
                    companyId, "Check If Debt Record Has Relations.", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return hasRelations;
        }


        public static async Task<bool> DeleteDebtRecordAsync(int debtRecordId, int companyId)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"[dbo].[DEBTRECORDS_SP_DeleteDebtRecord]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // تحديد نوع الاستعلام
                        command.CommandType = CommandType.StoredProcedure;

                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@debtRecordId", SqlDbType.Int) { Value = debtRecordId });
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });

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
                    () => debtRecordId,
                    () => companyId
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_DebtRecords_D", "DeleteDebtRecordAsync", ex.StackTrace,
                    companyId, "Delete Debt Record", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }


            return rowsAffected > 0;
        }

 
        public static async Task<int> NewDebtRecordAsync(md_NewDebtRecord record)
        {
            int insertedId = 0;

            if (record.DebtRecords_Products.Rows.Count == 0)
                return 0;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"[dbo].[DEBTRECORDS_SP_NewDebtRecord]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // تحديد نوع الاستعلام
                        command.CommandType = CommandType.StoredProcedure;

                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@customerId", SqlDbType.Int) { Value = record.CustomerId });
                        command.Parameters.Add(new SqlParameter("@description", SqlDbType.NVarChar, 250) { Value = record.Description ?? (object)DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@byUser", SqlDbType.Int) { Value = record.ByUser });
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = record.CompanyId });
                        command.Parameters.Add(new SqlParameter("@debtRecordsProducts", SqlDbType.Structured) { Value = record.DebtRecords_Products });

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
                    () => record.CustomerId,
                    () => record.Description,
                    () => record.ByUser,
                    () => record.CompanyId,
                    () => record.DebtRecords_Products
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_DebtRecords_D", "NewDebtRecordAsync", ex.StackTrace,
                    record.CompanyId, "Insert New Debt Record", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return -1;
            }

            return insertedId;
        }


        public static async Task<bool> UpdateDebtRecordAsync(md_UpdateDebtRecord record)
        {
            int rowsAffected = 0;

            if (record.DebtRecord_Products.Rows.Count == 0)
                return false;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"[dbo].[DEPTRECORDS_SP_UpdateDebtRecord]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // تحديد نوع الاستعلام
                        command.CommandType = CommandType.StoredProcedure;

                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@debtRecordId", SqlDbType.Int) { Value = record.DebtRecordId });
                        command.Parameters.Add(new SqlParameter("@customerId", SqlDbType.Int) { Value = record.CustomerId });
                        command.Parameters.Add(new SqlParameter("@description", SqlDbType.NVarChar, 250) { Value = record.Description ?? (object)DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@byUser", SqlDbType.Int) { Value = record.ByUser });
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = record.CompanyId });
                        command.Parameters.Add(new SqlParameter("@debtRecordsProductsToUpdate", SqlDbType.Structured) { Value = record.DebtRecord_Products });

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
                    () => record.DebtRecordId,
                    () => record.CustomerId,
                    () => record.Description,
                    () => record.ByUser,
                    () => record.CompanyId,
                    () => record.DebtRecord_Products
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_DebtRecords_D", "UpdateDebtRecordAsync", ex.StackTrace,
                    record.CompanyId, "Update Debt Record", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return rowsAffected > 0;
        }

    }
}
