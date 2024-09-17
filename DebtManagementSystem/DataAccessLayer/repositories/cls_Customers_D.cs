using DataAccessLayer.database;
using DataAccessLayer.models;
using DataAccessLayer.models.Customers;
using DataAccessLayer.repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace DataAccessLayer.repositories
{
    public class cls_Customers_D
    {
        // Completed Testing.
        public static async Task<List<md_Customers>?> GetCustomersAsync(int companyId)
        {
            List<md_Customers> customers = new List<md_Customers>();

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT * FROM [dbo].[CUSTOMERS_FUN_GetCustomers] (@companyId)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });

                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                customers.Add
                                    (
                                        new md_Customers
                                        (
                                            reader.GetInt32(reader.GetOrdinal("CustomerId")),
                                            reader.GetString(reader.GetOrdinal("FullName")),
                                            reader.GetString(reader.GetOrdinal("Phone1")),
                                            reader.IsDBNull(reader.GetOrdinal("Phone2")) ? null : reader["Phone2"].ToString(),
                                            reader.GetString(reader.GetOrdinal("CustomerCode")),
                                            reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader["Address"].ToString(),
                                            reader.GetString(reader.GetOrdinal("CustomerStatus")),
                                            reader.IsDBNull(reader.GetOrdinal("TelegramID")) ? null : reader["TelegramID"].ToString(),
                                            reader.IsDBNull(reader.GetOrdinal("ByUser")) ? null : reader["ByUser"].ToString()
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
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Customers_D", "GetCustomersAsync", ex.StackTrace,
                    companyId, "Get All Customers", Parameters);

                await cls_Errors_D.LogErrorAsync(error);
                return null;
            }

            return customers.Count > 0 ? customers : null;

        }

        // Completed Testing.
        public static async Task<List<md_Customers>?> GetActiveCustomersAsync(int companyId)
        {
            List<md_Customers> customers = new List<md_Customers>();

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT * FROM[dbo].[CUSTOMERS_FUN_GetActiveCustomers](@companyId)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });
                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                customers.Add
                                    (
                                        new md_Customers
                                        (
                                            reader.GetInt32(reader.GetOrdinal("CustomerId")),
                                            reader.GetString(reader.GetOrdinal("FullName")),
                                            reader.GetString(reader.GetOrdinal("Phone1")),
                                            reader.IsDBNull(reader.GetOrdinal("Phone2")) ? null : reader["Phone2"].ToString(),
                                            reader.GetString(reader.GetOrdinal("CustomerCode")),
                                            reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader["Address"].ToString(),
                                            reader.GetString(reader.GetOrdinal("CustomerStatus")),
                                            reader.IsDBNull(reader.GetOrdinal("TelegramID")) ? null : reader["TelegramID"].ToString(),
                                            reader.IsDBNull(reader.GetOrdinal("ByUser")) ? null : reader["ByUser"].ToString()
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
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Customers_D", "GetActiveCustomersAsync", ex.StackTrace,
                    companyId, "Get Active Customers", Parameters);

                await cls_Errors_D.LogErrorAsync(error);
                return null;
            }

            return customers.Count > 0 ? customers : null;
        }

        // Completed Testing.
        public static async Task<List<md_Customers>?> GetInActiveCustomersAsync(int companyId)
        {
            List<md_Customers> customers = new List<md_Customers>();

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT * FROM[dbo].[CUSTOMERS_FUN_GetInActiveCustomers](@companyId)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });
                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                customers.Add
                                    (
                                        new md_Customers
                                        (
                                            reader.GetInt32(reader.GetOrdinal("CustomerId")),
                                            reader.GetString(reader.GetOrdinal("FullName")),
                                            reader.GetString(reader.GetOrdinal("Phone1")),
                                            reader.IsDBNull(reader.GetOrdinal("Phone2")) ? null : reader["Phone2"].ToString(),
                                            reader.GetString(reader.GetOrdinal("CustomerCode")),
                                            reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader["Address"].ToString(),
                                            reader.GetString(reader.GetOrdinal("CustomerStatus")),
                                            reader.IsDBNull(reader.GetOrdinal("TelegramID")) ? null : reader["TelegramID"].ToString(),
                                            reader.IsDBNull(reader.GetOrdinal("ByUser")) ? null : reader["ByUser"].ToString()
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
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Customers_D", "GetInActiveCustomersAsync", ex.StackTrace,
                    companyId, "Get In Active Customers", Parameters);

                await cls_Errors_D.LogErrorAsync(error);
                return null;
            }

            return customers.Count > 0 ? customers : null;



        }

        // Completed Testing.
        public static async Task<md_Customer?> GetCustomerByIdAsync(int customerId, int companyId)
        {
            md_Customer? customer = null;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT * FROM[dbo].[CUSTOMERS_FUN_GetCustomerById](@customerId,@companyId)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@customerId", SqlDbType.Int) { Value = customerId });
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });

                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                customer = new md_Customer
                                (
                                    reader.GetInt32(reader.GetOrdinal("CustomerId")),
                                    reader.GetString(reader.GetOrdinal("FullName")),
                                    reader.GetString(reader.GetOrdinal("Phone1")),
                                    reader.IsDBNull(reader.GetOrdinal("Phone2")) ? null : reader["Phone2"].ToString(),
                                    reader.GetString(reader.GetOrdinal("CustomerCode")),
                                    reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader["Address"].ToString(),
                                    reader.GetBoolean(reader.GetOrdinal("CustomerStatus")),
                                    reader.IsDBNull(reader.GetOrdinal("TelegramID")) ? null : reader["TelegramID"].ToString(),
                                    reader.IsDBNull(reader.GetOrdinal("ByUser")) ? null : reader.GetInt32(reader.GetOrdinal("ByUser")),
                                    reader.GetInt32(reader.GetOrdinal("CompanyId"))
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
                    () => customerId,
                    () => companyId
                );

                // تسجيل الخطأ
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Customers_D", "GetCustomerByIdAsync", ex.StackTrace,
                    customerId, "Get Customer By Id", Parameters);

                await cls_Errors_D.LogErrorAsync(error);
                return null;
            }

            return customer;



        }

        // Completed Testing.
        public static async Task<bool> SetCustomerAsActiveAsync(int customerId, int companyId)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"[dbo].[CUSTOMERS_SP_SetCustomerAsActive]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // تحديد نوع query
                        command.CommandType = CommandType.StoredProcedure;

                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@customerId", SqlDbType.Int) { Value = customerId });
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });

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
                    () => customerId,
                    () => companyId
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Customer_D", "SetCustomerAsActiveAsync", ex.StackTrace,
                    companyId, "Set Customer As Active", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return rowsAffected > 0;
        }

        // Completed Testing.
        public static async Task<bool> SetCustomerAsInActiveAsync(int customerId, int companyId)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"[dbo].[CUSTOMERS_SP_SetCustomerAsInActive]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // تحديد نوع query
                        command.CommandType = CommandType.StoredProcedure;

                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@customerId", SqlDbType.Int) { Value = customerId });
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });

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
                    () => customerId,
                    () => companyId
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Customer_D", "SetCustomerAsInActiveAsync", ex.StackTrace,
                    companyId, "Set Customer As In Active", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return rowsAffected > 0;
        }

        // Completed Testing.
        public static async Task<bool> IsCustomerCodeExistAsync(string customerCode)
        {
            bool isExist = false;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT [dbo].[CUSTOMERS_FUN_IsCustomerCodeExist] (@customerCode)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@customerCode", SqlDbType.NVarChar, 14) { Value = customerCode });

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
                    () => customerCode
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Customer_D", "IsCustomerCodeExistAsync", ex.StackTrace,
                   0, "Check Is Customer Code Exist? (Has not company Id).", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return isExist;
        }

        // Completed Testing.
        public static async Task<int> GetCustomersCountAsync(int companyId)
        {
            int count = 0;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT [dbo].[CUSTOMERS_FUN_GetCustomersCount](@companyId)";

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
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Customer_D", "GetCustomersCountAsync", ex.StackTrace,
                    companyId, "Get Customers Count ", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return -1;
            }

            return count;
        }

        // Completed Testing.
        public static async Task<bool> IsCustomerHasRelationsAsync(int customerId, int companyId)
        {
            bool hasRelations = false;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    // Call the scalar function
                    string query = @"SELECT dbo.CUSTOMERS_FUN_IsCustomerHasRelations(@customerId, @companyId)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Adding parameters
                        command.Parameters.Add(new SqlParameter("@customerId", SqlDbType.Int) { Value = customerId });
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });

                        // Open the connection
                        await connection.OpenAsync();

                        // Execute the query and get the result
                        object? result = await command.ExecuteScalarAsync();

                        // Convert the result to a boolean value
                        if (result != null && result != DBNull.Value)
                        {
                            hasRelations = Convert.ToBoolean(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Collect error information
                string parameters = cls_Errors_D.GetParams(() => customerId, () => companyId);

                // Log the error
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Customer_D", "IsCustomerHasRelationsAsync", ex.StackTrace, customerId, "Is Customer Has Relations", parameters);

                // Save the error to the database
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return hasRelations;
        }

        // Completed Testing.
        public static async Task<bool> DeleteCustomerAsync(int customerId, int companyId)
        {
            int rowsAffected = -1;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"[dbo].[CUSTOMERS_SP_DeleteCustomer]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Set the command type to stored procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add the parameters for the stored procedure
                        command.Parameters.Add(new SqlParameter("@customerId", SqlDbType.Int) { Value = customerId });
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });

                        // Add a parameter to capture the return value
                        SqlParameter returnParameter = command.Parameters.Add("@returnValue", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;

                        // Open the connection and execute the command
                        await connection.OpenAsync();
                        await command.ExecuteScalarAsync();

                        // Get the return value
                        rowsAffected = (int)returnParameter.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                // Collect error information
                string parameters = cls_Errors_D.GetParams
                    (
                        () => customerId,
                        () => companyId
                    );

                // Log the error
                md_Errors error = new md_Errors(
                    ex.Message,
                    ex.Source,
                    "cls_Customer_D",
                    "DeleteCustomerAsync",
                    ex.StackTrace,
                    companyId,
                    "Delete Customer",
                    parameters
                );

                // Save the error to the database
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            // Return true if rowsAffected is greater than 0, indicating success
            return rowsAffected > 0;
        }

        // Completed Testing.
        public static async Task<int> NewCustomerAsync(md_Customer customer)
        {
            int insertedId = -1;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"[dbo].[CUSTOMERS_SP_NewCustomer]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // تحديد نوع الاستعلام
                        command.CommandType = CommandType.StoredProcedure;

                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@customerCode", SqlDbType.NVarChar, 14) { Value = customer.CustomerCode });
                        command.Parameters.Add(new SqlParameter("@address", SqlDbType.NVarChar, 100) { Value = customer.Address ?? (object)DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@fullName", SqlDbType.NVarChar, 50) { Value = customer.FullName });
                        command.Parameters.Add(new SqlParameter("@phone1", SqlDbType.NVarChar, 14) { Value = customer.Phone1 });
                        command.Parameters.Add(new SqlParameter("@phone2", SqlDbType.NVarChar, 14) { Value = customer.Phone2 ?? (object)DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@telegramId", SqlDbType.NVarChar, 25) { Value = customer.TelegramId ?? (object)DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = customer.CompanyId });
                        command.Parameters.Add(new SqlParameter("@byUser", SqlDbType.Int) { Value = customer.ByUser });

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
                    () => customer.CustomerCode,
                    () => customer.Address,
                    () => customer.FullName,
                    () => customer.Phone1,
                    () => customer.Phone2,
                    () => customer.TelegramId,
                    () => customer.CompanyId,
                    () => customer.ByUser
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Customer_D", "NewCustomerAsync", ex.StackTrace,
                    customer.CompanyId, "Insert New Customer", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return -1;
            }

            return insertedId;
        }
        
        // Completed Testing.
        public static async Task<bool> UpdateCustomerAsync(md_Customer customer)
        {
            int rowsAffected = -1;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"[dbo].[CUSTOMERS_SP_UpdateCustomer]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // تحديد نوع الاستعلام
                        command.CommandType = CommandType.StoredProcedure;

                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@customerId", SqlDbType.Int) { Value = customer.CustomerId });
                        command.Parameters.Add(new SqlParameter("@address", SqlDbType.NVarChar, 100) { Value = customer.Address ?? (object)DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@customerStatus", SqlDbType.Bit) { Value = customer.CustomerStatus });
                        command.Parameters.Add(new SqlParameter("@fullName", SqlDbType.NVarChar, 50) { Value = customer.FullName });
                        command.Parameters.Add(new SqlParameter("@phone1", SqlDbType.NVarChar, 14) { Value = customer.Phone1 });
                        command.Parameters.Add(new SqlParameter("@phone2", SqlDbType.NVarChar, 14) { Value = customer.Phone2 ?? (object)DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@telegramId", SqlDbType.NVarChar, 25) { Value = customer.TelegramId ?? (object)DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = customer.CompanyId });
                        command.Parameters.Add(new SqlParameter("@byUser", SqlDbType.Int) { Value = customer.ByUser });


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
                    () => customer.CustomerId,
                    () => customer.Address,
                    () => customer.CustomerStatus,
                    () => customer.FullName,
                    () => customer.Phone1,
                    () => customer.Phone2,
                    () => customer.TelegramId,
                    () => customer.CompanyId,
                    () => customer.ByUser

                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Customers_D", "UpdateCustomerAsync", ex.StackTrace,
                    customer.CompanyId, "Update Customer", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return rowsAffected > 0;
        }
    }

}

