using DataAccessLayer.database;
using DataAccessLayer.models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.repositories
{
    public class cls_Companies_D
    {

        // Completed Testing.
        public static async Task<bool> IsCompanyCodeExistAsync(string companyCode)
        {
            bool isExist = false;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT [dbo].[COMPANIES_FUN_IsCompanyCodeExist] (@companyCode)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@companyCode", SqlDbType.NVarChar, 14) { Value = companyCode });

                        // فتح الاتصال بقاعدة البيانات
                        await connection.OpenAsync();

                        // تنفيذ الاستعلام وقراءة ناتجه
                        object? returnValue = await command.ExecuteScalarAsync();

                        // اذا كانت ناتج الاستعلام لا يساوي null
                        if (returnValue != null && returnValue != DBNull.Value)
                            isExist = Convert.ToBoolean(returnValue);
                    }
                }
            }
            catch (Exception ex)
            {
                // جمع المعلومات عن الخطا
                string Parameters = cls_Errors_D.GetParams
                (
                    () => companyCode
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Companies_D", "IsCompanyCodeExistAsync", ex.StackTrace,
                    0, "Check If Company Code Exist. (has't companyId Param)", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return isExist;
        }

        // Completed Testing.
        public static async Task<bool> IsCompanyCodeExistWithOutCurrentCompanyAsync(int companyId, string companyCode)
        {
            bool isExist = false;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT [dbo].[COMPANIES_FUN_IsCompanyCodeExistWithOutCurrentCompany] (@companyId, @companyCode)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });
                        command.Parameters.Add(new SqlParameter("@companyCode", SqlDbType.NVarChar, 14) { Value = companyCode });

                        // فتح الاتصال بقاعدة البيانات
                        await connection.OpenAsync();

                        // تنفيذ الاستعلام وقراءة ناتجه
                        object? returnValue = await command.ExecuteScalarAsync();

                        // اذا كانت ناتج الاستعلام لا يساوي null
                        if (returnValue != null && returnValue != DBNull.Value)
                            isExist = Convert.ToBoolean(returnValue);

                    }
                }
            }
            catch (Exception ex)
            {
                // جمع المعلومات عن الخطا
                string Parameters = cls_Errors_D.GetParams
                (
                    () => companyId,
                    () => companyCode
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Companies_D", "IsCompanyCodeExistWithOutCurrentCompanyAsync", ex.StackTrace,
                    companyId, "Check If Company Code Exist With Out Current Company.", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return isExist;
        }

        // Completed Testing.
        public static async Task<bool> IsCompanyPhoneExistAsync(string companyPhone)
        {
            bool isExist = false;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT [dbo].[COMPANIES_FUN_IsCompanyPhoneExist] (@companyPhone)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@companyPhone", SqlDbType.NVarChar, 14) { Value = companyPhone });

                        // فتح الاتصال بقاعدة البيانات
                        await connection.OpenAsync();

                        // تنفيذ الاستعلام وقراءة ناتجه
                        object? returnValue = await command.ExecuteScalarAsync();

                        // اذا كانت ناتج الاستعلام لا يساوي null
                        if (returnValue != null && returnValue != DBNull.Value)
                            isExist = Convert.ToBoolean(returnValue);
                    }
                }
            }
            catch (Exception ex)
            {
                // جمع المعلومات عن الخطا
                string Parameters = cls_Errors_D.GetParams
                (
                    () => companyPhone
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Companies_D", "IsCompanyPhoneExistAsync", ex.StackTrace,
                    0, "Check If Company Phone Exist. (has't companyId Param)", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return isExist;
        }

        // Completed Testing.
        public static async Task<bool> IsCompanyPhoneExistWithOutCurrentCompanyAsync(int companyId, string companyPhone)
        {
            bool isExist = false;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT [dbo].[COMPANIES_FUN_IsCompanyPhoneExistWithOutCurrentCompany] (@companyId, @companyPhone)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });
                        command.Parameters.Add(new SqlParameter("@companyPhone", SqlDbType.NVarChar, 14) { Value = companyPhone });

                        // فتح الاتصال بقاعدة البيانات
                        await connection.OpenAsync();

                        // تنفيذ الاستعلام وقراءة ناتجه
                        object? returnValue = await command.ExecuteScalarAsync();

                        // اذا كانت ناتج الاستعلام لا يساوي null
                        if (returnValue != null && returnValue != DBNull.Value)
                            isExist = Convert.ToBoolean(returnValue);
                    }
                }
            }
            catch (Exception ex)
            {
                // جمع المعلومات عن الخطا
                string Parameters = cls_Errors_D.GetParams
                (
                    () => companyId,
                    () => companyPhone
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Companies_D", "IsCompanyPhoneExistWithOutCurrentCompany", ex.StackTrace,
                    companyId, "Check If Company Phone Exist With Out Current Company.", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return isExist;
        }

        // Completed Testing.
        public static async Task<int> GetCompaniesCountAsync()
        {
            int count = 0;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT [dbo].[COMPANIES_FUN_GetCompaniesCount] ()";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // فتح الاتصال بقاعدة البيانات
                        await connection.OpenAsync();

                        // تنفيذ الاستعلام وقراءة ناتجه
                        object returnValue = await command.ExecuteScalarAsync();

                        // اذا كانت ناتج الاستعلام لا يساوي null
                        if (returnValue != null && returnValue != DBNull.Value)
                            count = Convert.ToInt32(returnValue);
                    }
                }
            }
            catch (Exception ex)
            {
                // جمع المعلومات عن الخطا
                string Parameters = "Has't Any Params.";

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Companies_D", "GetCompaniesCountAsync", ex.StackTrace,
                    0, "Get Companies Count. (Has't CompanyId Param)", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return -1;
            }

            return count;
        }

        // Completed Testing.
        public static async Task<bool> IsSubscriptionActiveAsync(int companyId)
        {
            bool isActive = false;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT [dbo].[COMPANIES_FUN_IsSubscriptionActive] (@companyId)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });

                        // فتح الاتصال بقاعدة البيانات
                        await connection.OpenAsync();

                        // تنفيذ الاستعلام وقراءة ناتجه
                        object returnValue = await command.ExecuteScalarAsync();

                        // اذا كانت ناتج الاستعلام لا يساوي null
                        if (returnValue != null && returnValue != DBNull.Value)
                            isActive = Convert.ToBoolean(returnValue);

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
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Companies_D", "IsSubscriptionActiveAsync", ex.StackTrace,
                    companyId, "Check If Company Subscription Is Active", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return isActive;
        }

        // Completed Testing.
        public static async Task<List<md_Companies>?> GetCompaniesAsync()
        {
            List<md_Companies> companies = new List<md_Companies>();

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT * FROM [dbo].[COMPANIES_FUN_GetCompanies] ()";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // فتح الاتصال
                        await connection.OpenAsync();

                        // تنفيذ الاستعلام
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                companies.Add
                                    (
                                        new md_Companies
                                        (
                                            reader.GetInt32(reader.GetOrdinal("CompanyId")),
                                            reader.GetString(reader.GetOrdinal("ManagerFullName")),
                                            reader.GetString(reader.GetOrdinal("CompanyName")),
                                            reader.GetString(reader.GetOrdinal("CompanyCode")),
                                            reader.IsDBNull(reader.GetOrdinal("CompanyImage")) ? null : (byte[])reader["CompanyImage"],
                                            reader.GetString(reader.GetOrdinal("Phone1")),
                                            reader.IsDBNull(reader.GetOrdinal("Phone2")) ? null : reader["Phone2"].ToString(),
                                            reader.GetString(reader.GetOrdinal("Address")),
                                            Convert.ToDouble(reader.GetDecimal(reader.GetOrdinal("SubscriptionFee"))),
                                            reader.GetString(reader.GetOrdinal("Currency")),
                                            reader.GetDateTime(reader.GetOrdinal("RegistrationDate")),
                                            reader.GetString(reader.GetOrdinal("SubscriptionStatus")),
                                            reader.GetDateTime(reader.GetOrdinal("SubscriptionStartDate")),
                                            reader.GetDateTime(reader.GetOrdinal("SubscriptionEndDate")),
                                            reader.GetInt32(reader.GetOrdinal("RemainingSubscriptionDays")),
                                            reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader["Description"].ToString(),
                                            reader.GetString(reader.GetOrdinal("IsPaid")),
                                            reader.GetInt32(reader.GetOrdinal("ByAdmin")),
                                            reader.GetString(reader.GetOrdinal("Action"))
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
                string Parameters = "Has't Params.";

                // تسجيل الخطأ
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Companies_D", "GetCompaniesAsync", ex.StackTrace,
                    0, "Get All Companies. (has't companyId param.)", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return null;
            }

            return companies.Count > 0 ? companies : null;
        }

        // Completed Testing.
        public static async Task<md_Company?> GetCompanyByIdAsync(int companyId)
        {
            md_Company? company = null;

            try
            {
                using (SqlConnection connectin = cls_database.Connection())
                {
                    string query = @"SELECT * FROM [dbo].[COMPANIES_FUN_GetCompanyById] (@companyId)";

                    using (SqlCommand command = new SqlCommand(query, connectin))
                    {
                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });

                        // فتح الاتصال بقاعدة البيانات
                        await connectin.OpenAsync();

                        // تنفيذ الاستعلام
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                company = new md_Company
                                (
                                    reader.GetInt32(reader.GetOrdinal("CompanyId")),
                                    reader.GetString(reader.GetOrdinal("ManagerFullName")),
                                    reader.GetString(reader.GetOrdinal("CompanyName")),
                                    reader.GetString(reader.GetOrdinal("CompanyCode")),
                                    reader.IsDBNull(reader.GetOrdinal("CompanyImage")) ? null : (byte[])reader["CompanyImage"],
                                    reader.GetString(reader.GetOrdinal("Phone1")),
                                    reader.IsDBNull(reader.GetOrdinal("Phone2")) ? null : reader["Phone2"].ToString(),
                                    reader.GetString(reader.GetOrdinal("Address")),
                                    Convert.ToDouble(reader.GetDecimal(reader.GetOrdinal("SubscriptionFee"))),
                                    reader.GetString(reader.GetOrdinal("Currency")),
                                    reader.GetDateTime(reader.GetOrdinal("RegistrationDate")),
                                    reader.GetBoolean(reader.GetOrdinal("SubscriptionStatus")),
                                    reader.GetDateTime(reader.GetOrdinal("SubscriptionStartDate")),
                                    reader.GetDateTime(reader.GetOrdinal("SubscriptionEndDate")),
                                    reader.GetInt32(reader.GetOrdinal("RemainingSubscriptionDays")),
                                    reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader["Description"].ToString(),
                                    reader.GetBoolean(reader.GetOrdinal("IsPaid")),
                                    reader.GetInt32(reader.GetOrdinal("ByAdmin")),
                                    reader.GetString(reader.GetOrdinal("Action"))
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
                    () => companyId
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Companies_D", "GetCompanyByIdAsync", ex.StackTrace,
                    companyId, "Get Company By Id", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return null;
            }

            return company;
        }

        // Completed Testing.
        public static async Task<List<md_Companies>?> GetActiveCompaniesAsync()
        {
            List<md_Companies> companies = new List<md_Companies>();

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT * FROM [dbo].[COMPANIES_FUN_GetActiveCompanies] ()";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // فتح الاتصال
                        await connection.OpenAsync();

                        // تنفيذ الاستعلام
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                companies.Add
                                    (
                                        new md_Companies
                                        (
                                            reader.GetInt32(reader.GetOrdinal("CompanyId")),
                                            reader.GetString(reader.GetOrdinal("ManagerFullName")),
                                            reader.GetString(reader.GetOrdinal("CompanyName")),
                                            reader.GetString(reader.GetOrdinal("CompanyCode")),
                                            reader.IsDBNull(reader.GetOrdinal("CompanyImage")) ? null : (byte[])reader["CompanyImage"],
                                            reader.GetString(reader.GetOrdinal("Phone1")),
                                            reader.IsDBNull(reader.GetOrdinal("Phone2")) ? null : reader["Phone2"].ToString(),
                                            reader.GetString(reader.GetOrdinal("Address")),
                                            Convert.ToDouble(reader.GetDecimal(reader.GetOrdinal("SubscriptionFee"))),
                                            reader.GetString(reader.GetOrdinal("Currency")),
                                            reader.GetDateTime(reader.GetOrdinal("RegistrationDate")),
                                            reader.GetString(reader.GetOrdinal("SubscriptionStatus")),
                                            reader.GetDateTime(reader.GetOrdinal("SubscriptionStartDate")),
                                            reader.GetDateTime(reader.GetOrdinal("SubscriptionEndDate")),
                                            reader.GetInt32(reader.GetOrdinal("RemainingSubscriptionDays")),
                                            reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader["Description"].ToString(),
                                            reader.GetString(reader.GetOrdinal("IsPaid")),
                                            reader.GetInt32(reader.GetOrdinal("ByAdmin")),
                                            reader.GetString(reader.GetOrdinal("Action"))
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
                string Parameters = "Has't Params.";

                // تسجيل الخطأ
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Companies_D", "GetActiveCompaniesAsync", ex.StackTrace,
                    0, "Get Active Companies. (has't companyId param.)", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return null;
            }

            return companies.Count > 0 ? companies : null;
        }

        // Completed Testing.
        public static async Task<List<md_Companies>?> GetInActiveCompaniesAsync()
        {
            List<md_Companies> companies = new List<md_Companies>();

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT * FROM [dbo].[COMPANIES_FUN_GetInActiveCompanies] ()";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        // فتح الاتصال
                        await connection.OpenAsync();

                        // تنفيذ الاستعلام
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                companies.Add
                                    (
                                        new md_Companies
                                        (
                                            reader.GetInt32(reader.GetOrdinal("CompanyId")),
                                            reader.GetString(reader.GetOrdinal("ManagerFullName")),
                                            reader.GetString(reader.GetOrdinal("CompanyName")),
                                            reader.GetString(reader.GetOrdinal("CompanyCode")),
                                            reader.IsDBNull(reader.GetOrdinal("CompanyImage")) ? null : (byte[])reader["CompanyImage"],
                                            reader.GetString(reader.GetOrdinal("Phone1")),
                                            reader.IsDBNull(reader.GetOrdinal("Phone2")) ? null : reader["Phone2"].ToString(),
                                            reader.GetString(reader.GetOrdinal("Address")),
                                            Convert.ToDouble(reader.GetDecimal(reader.GetOrdinal("SubscriptionFee"))),
                                            reader.GetString(reader.GetOrdinal("Currency")),
                                            reader.GetDateTime(reader.GetOrdinal("RegistrationDate")),
                                            reader.GetString(reader.GetOrdinal("SubscriptionStatus")),
                                            reader.GetDateTime(reader.GetOrdinal("SubscriptionStartDate")),
                                            reader.GetDateTime(reader.GetOrdinal("SubscriptionEndDate")),
                                            reader.GetInt32(reader.GetOrdinal("RemainingSubscriptionDays")),
                                            reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader["Description"].ToString(),
                                            reader.GetString(reader.GetOrdinal("IsPaid")),
                                            reader.GetInt32(reader.GetOrdinal("ByAdmin")),
                                            reader.GetString(reader.GetOrdinal("Action"))
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
                string Parameters = "Has't Params.";

                // تسجيل الخطأ
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Companies_D", "GetInActiveCompaniesAsync", ex.StackTrace,
                    0, "Get InActive Companies. (has't companyId param.)", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return null;
            }

            return companies.Count > 0 ? companies : null;
        }

        // Completed Testing.
        public static async Task<List<md_Companies>?> GetCompaniesByAdminAsync(int adminId)
        {
            List<md_Companies> companies = new List<md_Companies>();

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT * FROM [dbo].[COMPANIES_FUN_GetCompaniesByAdmin] (@adminId)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@adminId", SqlDbType.Int) { Value = adminId });

                        // فتح الاتصال
                        await connection.OpenAsync();

                        // تنفيذ الاستعلام
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                companies.Add
                                    (
                                        new md_Companies
                                        (
                                            reader.GetInt32(reader.GetOrdinal("CompanyId")),
                                            reader.GetString(reader.GetOrdinal("ManagerFullName")),
                                            reader.GetString(reader.GetOrdinal("CompanyName")),
                                            reader.GetString(reader.GetOrdinal("CompanyCode")),
                                            reader.IsDBNull(reader.GetOrdinal("CompanyImage")) ? null : (byte[])reader["CompanyImage"],
                                            reader.GetString(reader.GetOrdinal("Phone1")),
                                            reader.IsDBNull(reader.GetOrdinal("Phone2")) ? null : reader["Phone2"].ToString(),
                                            reader.GetString(reader.GetOrdinal("Address")),
                                            Convert.ToDouble(reader.GetDecimal(reader.GetOrdinal("SubscriptionFee"))),
                                            reader.GetString(reader.GetOrdinal("Currency")),
                                            reader.GetDateTime(reader.GetOrdinal("RegistrationDate")),
                                            reader.GetString(reader.GetOrdinal("SubscriptionStatus")),
                                            reader.GetDateTime(reader.GetOrdinal("SubscriptionStartDate")),
                                            reader.GetDateTime(reader.GetOrdinal("SubscriptionEndDate")),
                                            reader.GetInt32(reader.GetOrdinal("RemainingSubscriptionDays")),
                                            reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader["Description"].ToString(),
                                            reader.GetString(reader.GetOrdinal("IsPaid")),
                                            reader.GetInt32(reader.GetOrdinal("ByAdmin")),
                                            reader.GetString(reader.GetOrdinal("Action"))
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
                    () => adminId
                );

                // تسجيل الخطأ
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Companies_D", "GetCompaniesByAdminAsync", ex.StackTrace,
                    0, "Get Companies By Admin Id. (has't companyId param.)", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return null;
            }

            return companies.Count > 0 ? companies : null;
        }

        // Completed Testing.
        public static async Task<bool> SetCompanyAsActiveAsync(int companyId, int adminId)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"[dbo].[COMPANIES_SP_SetCompanyAsActive]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // تحديد نوع query
                        command.CommandType = CommandType.StoredProcedure;

                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });
                        command.Parameters.Add(new SqlParameter("@adminId", SqlDbType.Int) { Value = adminId });

                        // تحديد نوع البيانات المرجعة
                        SqlParameter returnParameter = command.Parameters.Add("returnValue", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;

                        // فتح الاتصال مع قاعدة البيانات
                        await connection.OpenAsync();

                        // تنفيذ الاستعلام
                        await command.ExecuteScalarAsync();

                        // قراءة البيانات المرجعة 
                        rowsAffected = (int)returnParameter.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                // جمع المعلومات عن الخطا
                string Parameters = cls_Errors_D.GetParams
                (
                    () => adminId,
                    () => companyId
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Companies_D", "SetCompanyAsActiveAsync", ex.StackTrace,
                    companyId, "Set Company As Active", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return rowsAffected > 0;
        }

        // Completed Testing.
        public static async Task<bool> SetCompanyAsInActiveAsync(int companyId, int adminId)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"[dbo].[COMPANIES_SP_SetCompanyAsInActive]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // تحديد نوع query
                        command.CommandType = CommandType.StoredProcedure;

                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });
                        command.Parameters.Add(new SqlParameter("@adminId", SqlDbType.Int) { Value = adminId });

                        // تحديد نوع البيانات المرجعة
                        SqlParameter returnParameter = command.Parameters.Add("returnValue", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;

                        // فتح الاتصال مع قاعدة البيانات
                        await connection.OpenAsync();

                        // تنفيذ الاستعلام
                        await command.ExecuteScalarAsync();

                        // قراءة البيانات المرجعة 
                        rowsAffected = (int)returnParameter.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                // جمع المعلومات عن الخطا
                string Parameters = cls_Errors_D.GetParams
                (
                    () => adminId,
                    () => companyId
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Companies_D", "SetCompanyAsInActiveAsync", ex.StackTrace,
                    companyId, "Set Company As InActive", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return rowsAffected > 0;
        }

        // Completed Testing.
        public static async Task<bool> SetPaidStatusAsPaidAsync(int companyId, int adminId)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"[dbo].[COMPANIES_SP_SetPaidStatusAsPaid]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // تحديد نوع query
                        command.CommandType = CommandType.StoredProcedure;

                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });
                        command.Parameters.Add(new SqlParameter("@adminId", SqlDbType.Int) { Value = adminId });

                        // تحديد نوع البيانات المرجعة
                        SqlParameter returnParameter = command.Parameters.Add("returnValue", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;

                        // فتح الاتصال مع قاعدة البيانات
                        await connection.OpenAsync();

                        // تنفيذ الاستعلام
                        await command.ExecuteScalarAsync();

                        // قراءة البيانات المرجعة 
                        rowsAffected = (int)returnParameter.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                // جمع المعلومات عن الخطا
                string Parameters = cls_Errors_D.GetParams
                (
                    () => adminId,
                    () => companyId
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Companies_D", "SetPaidStatusAsPaidAsync", ex.StackTrace,
                    companyId, "Set Paid Status As Paid", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return rowsAffected > 0;
        }

        // Completed Testing.
        public static async Task<bool> SetPaidStatusAsUnPaidAsync(int companyId, int adminId)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"[dbo].[COMPANIES_SP_SetPaidStatusAsUnPaid]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // تحديد نوع query
                        command.CommandType = CommandType.StoredProcedure;

                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });
                        command.Parameters.Add(new SqlParameter("@adminId", SqlDbType.Int) { Value = adminId });

                        // تحديد نوع البيانات المرجعة
                        SqlParameter returnParameter = command.Parameters.Add("returnValue", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;

                        // فتح الاتصال مع قاعدة البيانات
                        await connection.OpenAsync();

                        // تنفيذ الاستعلام
                        await command.ExecuteScalarAsync();

                        // قراءة البيانات المرجعة 
                        rowsAffected = (int)returnParameter.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                // جمع المعلومات عن الخطا
                string Parameters = cls_Errors_D.GetParams
                (
                    () => adminId,
                    () => companyId
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Companies_D", "SetPaidStatusAsUnPaidAsync", ex.StackTrace,
                    companyId, "Set Paid Status As UnPaid", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return rowsAffected > 0;
        }

        // Completed Testing.
        public static async Task<int> NewCompanyAsync(md_Company company)
        {
            int insertedId = -1;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"[dbo].[COMPANIES_SP_NewCompany]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // تحديد نوع الاستعلام
                        command.CommandType = CommandType.StoredProcedure;

                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@managerFullName", SqlDbType.NVarChar, 50) { Value = company.ManagerFullName });
                        command.Parameters.Add(new SqlParameter("@companyName", SqlDbType.NVarChar, 50) { Value = company.CompanyName });
                        command.Parameters.Add(new SqlParameter("@companyCode", SqlDbType.NVarChar, 14) { Value = company.CompanyCode });
                        command.Parameters.Add(new SqlParameter("@companyImage", SqlDbType.VarBinary) { Value = company.CompanyImage ?? SqlBinary.Null });
                        command.Parameters.Add(new SqlParameter("@phone1", SqlDbType.NVarChar, 14) { Value = company.Phone1 });
                        command.Parameters.Add(new SqlParameter("@phone2", SqlDbType.NVarChar, 14) { Value = company.Phone2 ?? (object)DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@address", SqlDbType.NVarChar, 150) { Value = company.Address });
                        command.Parameters.Add(new SqlParameter("@subscriptionFee", SqlDbType.Money) { Value = company.SubscriptionFee });
                        command.Parameters.Add(new SqlParameter("@currency", SqlDbType.NVarChar, 14) { Value = company.Currency });
                        command.Parameters.Add(new SqlParameter("@subscriptionStatus", SqlDbType.Bit) { Value = company.SubscriptionStatus });
                        command.Parameters.Add(new SqlParameter("@subscriptionStartDate", SqlDbType.Date) { Value = company.SubscriptionStartDate });
                        command.Parameters.Add(new SqlParameter("@subscriptionEndDate", SqlDbType.Date) { Value = company.SubscriptionEndDate });
                        command.Parameters.Add(new SqlParameter("@description", SqlDbType.NVarChar, 250) { Value = company.Description ?? (object)DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@isPaid", SqlDbType.Bit) { Value = company.IsPaid });
                        command.Parameters.Add(new SqlParameter("@byAdmin", SqlDbType.Int) { Value = company.ByAdmin });

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
                    () => company.ManagerFullName,
                    () => company.CompanyName,
                    () => company.CompanyCode,
                    () => company.CompanyImage,
                    () => company.Phone1,
                    () => company.Phone2,
                    () => company.Address,
                    () => company.SubscriptionFee,
                    () => company.Currency,
                    () => company.SubscriptionStatus,
                    () => company.SubscriptionStartDate,
                    () => company.SubscriptionEndDate,
                    () => company.Description,
                    () => company.IsPaid,
                    () => company.ByAdmin
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Companies_D", "NewCompanyAsync", ex.StackTrace,
                    0, "Insert New Company. (Has't Company Id)", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return -1;
            }

            return insertedId;
        }

        // Completed Testing.
        public static async Task<bool> UpdateCompanyAsync(md_Company company)
        {
            int rowsAffected = -1;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"[dbo].[COMPANIES_SP_UpdateCompany]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // تحديد نوع الاستعلام
                        command.CommandType = CommandType.StoredProcedure;

                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = company.CompanyId });
                        command.Parameters.Add(new SqlParameter("@managerFullName", SqlDbType.NVarChar, 50) { Value = company.ManagerFullName });
                        command.Parameters.Add(new SqlParameter("@companyName", SqlDbType.NVarChar, 50) { Value = company.CompanyName });
                        command.Parameters.Add(new SqlParameter("@companyImage", SqlDbType.VarBinary) { Value = company.CompanyImage ?? SqlBinary.Null });
                        command.Parameters.Add(new SqlParameter("@phone1", SqlDbType.NVarChar, 14) { Value = company.Phone1 });
                        command.Parameters.Add(new SqlParameter("@phone2", SqlDbType.NVarChar, 14) { Value = company.Phone2 ?? (object)DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@address", SqlDbType.NVarChar, 150) { Value = company.Address });
                        command.Parameters.Add(new SqlParameter("@subscriptionFee", SqlDbType.Money) { Value = company.SubscriptionFee });
                        command.Parameters.Add(new SqlParameter("@currency", SqlDbType.NVarChar, 14) { Value = company.Currency });
                        command.Parameters.Add(new SqlParameter("@subscriptionStatus", SqlDbType.Bit) { Value = company.SubscriptionStatus });
                        command.Parameters.Add(new SqlParameter("@subscriptionStartDate", SqlDbType.Date) { Value = company.SubscriptionStartDate });
                        command.Parameters.Add(new SqlParameter("@subscriptionEndDate", SqlDbType.Date) { Value = company.SubscriptionEndDate });
                        command.Parameters.Add(new SqlParameter("@description", SqlDbType.NVarChar, 250) { Value = company.Description ?? (object)DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@isPaid", SqlDbType.Bit) { Value = company.IsPaid });
                        command.Parameters.Add(new SqlParameter("@byAdmin", SqlDbType.Int) { Value = company.ByAdmin });

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
                    () => company.CompanyId,
                    () => company.ManagerFullName,
                    () => company.CompanyName,
                    () => company.CompanyImage,
                    () => company.Phone1,
                    () => company.Phone2,
                    () => company.Address,
                    () => company.SubscriptionFee,
                    () => company.Currency,
                    () => company.SubscriptionStatus,
                    () => company.SubscriptionStartDate,
                    () => company.SubscriptionEndDate,
                    () => company.Description,
                    () => company.IsPaid,
                    () => company.ByAdmin
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Companies_D", "UpdateCompanyAsync", ex.StackTrace,
                    company.CompanyId, "Update Company", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return rowsAffected > 0;
        }

        // Completed Testing.
        public static async Task<bool> DeleteCompanyAsync(int companyId)
        {
            int rowsAffected = -1;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"[dbo].[COMPANIES_SP_DeleteCompany]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // تحديد نوع الاستعلام
                        command.CommandType = CommandType.StoredProcedure;

                        // اضافة المعلمات
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
                    () => companyId
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Companies_D", "DeleteCompanyAsync", ex.StackTrace,
                    companyId, "Delete Company", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return rowsAffected > 0;
        }


    }
}
