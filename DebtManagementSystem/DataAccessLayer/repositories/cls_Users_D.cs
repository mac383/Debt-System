using DataAccessLayer.database;
using DataAccessLayer.models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.models.User_models;
using Microsoft.Identity.Client;

namespace DataAccessLayer.repositories
{
    public class cls_Users_D
    {
        //Completed Testing
        public static async Task<List<md_Users>?> GetUsersAsync(int companyId)
        {
            List<md_Users> users = new List<md_Users>();

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT * FROM [dbo].[USERS_FUN_GetUsers] (@companyId)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });
                        await connection.OpenAsync();
                     
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                users.Add (            
                                        new md_Users
                                        (
                                            reader.GetInt32(reader.GetOrdinal("UserId")),
                                            reader.GetString(reader.GetOrdinal("FullName")),
                                            reader.GetString(reader.GetOrdinal("UserName")),
                                            reader.GetString(reader.GetOrdinal("Phone1")),
                                            reader.IsDBNull(reader.GetOrdinal("Phone2")) ? null : reader.GetString("Phone2"),
                                            reader.IsDBNull(reader.GetOrdinal("TelegramID")) ? null : reader.GetString("TelegramID"),
                                            reader.GetInt64(reader.GetOrdinal("Permissions")),
                                            reader.IsDBNull(reader.GetOrdinal("Image")) ? null : (byte[])reader["Image"],
                                            reader.GetString(reader.GetOrdinal("IsActive")),
                                            reader.IsDBNull(reader.GetOrdinal("ByUser")) ? null : reader.GetString("ByUser")
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
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Users_D", "GetUsersAsync", ex.StackTrace,
                    companyId, "Get All Users", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return null;
            }

            return users.Count > 0 ? users : null ;
        }

        //Completed Testing
        public static async Task<List<md_Users>?> GetActiveUsersAsync(int companyId)
        {
            List<md_Users> users = new List<md_Users>();

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT * FROM [dbo].[USERS_FUN_GetActiveUsers] (@companyId)";

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
                                users.Add(

                                        new md_Users
                                        (
                                            reader.GetInt32(reader.GetOrdinal("UserId")),
                                            reader.GetString(reader.GetOrdinal("FullName")),
                                            reader.GetString(reader.GetOrdinal("UserName")),
                                            reader.GetString(reader.GetOrdinal("Phone1")),
                                            reader.IsDBNull(reader.GetOrdinal("Phone2")) ? null : reader.GetString("Phone2"),
                                            reader.IsDBNull(reader.GetOrdinal("TelegramID")) ? null : reader.GetString("TelegramID"),
                                            reader.GetInt64(reader.GetOrdinal("Permissions")),
                                            reader.IsDBNull(reader.GetOrdinal("Image")) ? null : (byte[])reader["Image"],
                                            reader.GetString(reader.GetOrdinal("IsActive")),
                                            reader.IsDBNull(reader.GetOrdinal("ByUser")) ? null : reader.GetString("ByUser")
                                         )
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
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Users_D", "GetActiveUsersAsync", ex.StackTrace,
                    companyId, "Get Active Users", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return null;
            }

            return users.Count > 0 ? users : null;
        }

        //Completed Testing
        public static async Task<List<md_Users>?> GetInActiveUsersAsync(int companyId)
        {
            List<md_Users> users = new List<md_Users>();

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT * FROM [dbo].[USERS_FUN_GetInActiveUsers] (@companyId)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });

                        // فتح الاتصال
                        await connection.OpenAsync();

                        // تنفيذ الاستعلام
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                users.Add(
                                        new md_Users
                                        (
                                            reader.GetInt32(reader.GetOrdinal("UserId")),
                                            reader.GetString(reader.GetOrdinal("FullName")),
                                            reader.GetString(reader.GetOrdinal("UserName")),
                                            reader.GetString(reader.GetOrdinal("Phone1")),
                                            reader.IsDBNull(reader.GetOrdinal("Phone2")) ? null : reader.GetString("Phone2"),
                                            reader.IsDBNull(reader.GetOrdinal("TelegramID")) ? null : reader.GetString("TelegramID"),
                                            reader.GetInt64(reader.GetOrdinal("Permissions")),
                                            reader.IsDBNull(reader.GetOrdinal("Image")) ? null : (byte[])reader["Image"],
                                            reader.GetString(reader.GetOrdinal("IsActive")),
                                            reader.IsDBNull(reader.GetOrdinal("ByUser")) ? null : reader.GetString("ByUser")
                                         )

                                    );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // جمع معلومات عن الخطا
                string Parameters = cls_Errors_D.GetParams
                (
                    () => companyId
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Users_D", "GetInActiveUsersAsync", ex.StackTrace,
                    companyId, "Get InActive Users", Parameters);

                // خفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return null;
            }

            return users.Count > 0 ? users : null;
        }

        //Completed Testing
        public static async Task<md_User?> GetUserByIdAsync(int userId, int companyId)
        {
            md_User? user = null;

            try
            {
                using (SqlConnection connectin = cls_database.Connection())
                {
                    string query = @"SELECT * FROM [dbo].[USERS_FUN_GetUserById] (@userId, @companyId)";

                    using (SqlCommand command = new SqlCommand(query, connectin))
                    {
                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@userId", SqlDbType.Int) { Value = userId });
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });

                        // فتح الاتصال بقاعدة البيانات
                        await connectin.OpenAsync();

                        // تنفيذ الاستعلام
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                user = new md_User
                                (
                                    reader.GetInt32(reader.GetOrdinal("UserId")),
                                    reader.GetString(reader.GetOrdinal("FullName")),
                                    reader.GetString(reader.GetOrdinal("UserName")),
                                    reader.GetString(reader.GetOrdinal("Phone1")),
                                    reader.IsDBNull(reader.GetOrdinal("Phone2")) ? null : reader.GetString("Phone2"),
                                    reader.IsDBNull(reader.GetOrdinal("TelegramID")) ? null : reader.GetString("TelegramID"),
                                    reader.GetInt64(reader.GetOrdinal("Permissions")),
                                    reader.IsDBNull(reader.GetOrdinal("Image")) ? null : (byte[])reader["Image"],
                                    reader.GetBoolean(reader.GetOrdinal("IsActive")),
                                    reader.IsDBNull(reader.GetOrdinal("ByUser")) ? null : reader.GetInt32("ByUser"),
                                    reader.GetInt32(reader.GetOrdinal("CompanyId"))
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
                    () => userId,
                    () => companyId
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Users_D", "GetUserByIdAsync", ex.StackTrace,
                    companyId, "Get User By Id", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return null;
            }

            return user;
        }

        //Completed Testing
        public static async Task<md_UserAuth?> GetUserByLogInInfoAsync(string userName, string password)
        {
            md_UserAuth? user = null;

            try
            {
                using (SqlConnection connectin = cls_database.Connection())
                {
                    string query = @"SELECT * FROM [dbo].[USERS_FUN_GetUserByLoginInfo] (@userName, @password)";

                    using (SqlCommand command = new SqlCommand(query, connectin))
                    {
                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@userName", SqlDbType.NVarChar, 14) { Value = userName });
                        command.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar, 100) { Value = password });

                        // فتح الاتصال بقاعدة البيانات
                        await connectin.OpenAsync();

                        // تنفيذ الاستعلام
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                user = new md_UserAuth
                                (
                                    reader.GetInt32(reader.GetOrdinal("UserId")),
                                    reader.GetString(reader.GetOrdinal("FullName")),
                                    reader.GetString(reader.GetOrdinal("UserName")),
                                    reader.GetString(reader.GetOrdinal("Password")),
                                    reader.GetString(reader.GetOrdinal("Phone1")),
                                    reader.IsDBNull(reader.GetOrdinal("Phone2")) ? null : reader.GetString("Phone2"),
                                    reader.IsDBNull(reader.GetOrdinal("TelegramID")) ? null : reader.GetString("TelegramID"),
                                    reader.GetInt64(reader.GetOrdinal("Permissions")),
                                    reader.IsDBNull(reader.GetOrdinal("Image")) ? null : (byte[])reader["Image"],
                                    reader.GetBoolean(reader.GetOrdinal("IsActive")),
                                    reader.IsDBNull(reader.GetOrdinal("ByUser")) ? null : reader.GetInt32("ByUser"),
                                    reader.GetInt32(reader.GetOrdinal("CompanyId"))
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
                    () => userName,
                    () => password
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Users_D", "GetUserByLogInInfoAsync", ex.StackTrace,
                    -1, "Get User By Login Info (Has Not CompanyId Param)", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return null;
            }

            return user;
        }

        //Completed Testing
        public static async Task<int> GetUsersCountAsync(int companyId)
        {
            int count = 0;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT [dbo].[USERS_FUN_GetUsersCount](@companyId)";

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
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Users_D", "GetUsersCountAsync", ex.StackTrace,
                    companyId, "Get Users Count", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return -1;
            }

            return count;
        }

        //Completed Testing
        public static async Task<bool> IsUserNameExistAsync(string userName, int companyId)
        {
            bool isExist = false;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT [dbo].[USERS_FUN_IsUserNameExist] (@userName, @companyId)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@userName", SqlDbType.NVarChar, 14) { Value = userName });
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });

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
                    () => userName,
                    () => companyId
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Users_D", "IsUserNameExistAsync", ex.StackTrace,
                    companyId, "Check UserName", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return isExist;
        }

        //Completed Testing
        public static async Task<bool> IsUserNameExistWithOutCurrentUserAsync(int userId, string userName, int companyId)
        {
            bool isExist = false;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT [dbo].[USERS_FUN_IsUserNameExistWithOutCurrentUser](@userId, @userName, @companyId)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@userId", SqlDbType.Int) { Value = userId });
                        command.Parameters.Add(new SqlParameter("@userName", SqlDbType.NVarChar, 14) { Value = userName });
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });

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
                    () => userId,
                    () => userName,
                    () => companyId
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Users_D", "IsUserNameExistWithOutCurrentUserAsync", ex.StackTrace,
                    companyId, "Check UserName Without Current User", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return isExist;
        }

        //Completed Testing
        public static async Task<bool> SetUserAsActiveAsync(int userId, int companyId)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"[dbo].[USERS_SP_SetUserAsActive]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // تحديد نوع query
                        command.CommandType = CommandType.StoredProcedure;

                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@userId", SqlDbType.Int) { Value = userId });
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
                    () => userId,
                    () => companyId
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Users_D", "SetUserAsActiveAsync", ex.StackTrace,
                    companyId, "Set User As Active", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return rowsAffected > 0;
        }

        //Completed Testing
        public static async Task<bool> SetUserAsInActiveAsync(int userId, int companyId)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"[dbo].[USERS_SP_SetUserAsInActive]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // تحديد نوع query
                        command.CommandType = CommandType.StoredProcedure;

                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@userId", SqlDbType.Int) { Value = userId });
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
                    () => userId,
                    () => companyId
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Users_D", "SetUserAsInActiveAsync", ex.StackTrace,
                    companyId, "Set User As InActive", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }
            return rowsAffected > 0;
        }

        //Completed Testing
        public static async Task<int> NewUserAsync(md_NewUser user)
        {
            int insertedId = -1;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"[dbo].[USERS_SP_NewUser]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // تحديد نوع الاستعلام
                        command.CommandType = CommandType.StoredProcedure;

                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@fullName", SqlDbType.NVarChar, 50) { Value = user.FullName });
                        command.Parameters.Add(new SqlParameter("@phone1", SqlDbType.NVarChar, 14) { Value = user.Phone1 });
                        command.Parameters.Add(new SqlParameter("@phone2", SqlDbType.NVarChar, 14) { Value = user.Phone2 ?? (object)DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@telegramId", SqlDbType.NVarChar, 25) { Value = user.TelegramId ?? (object)DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = user.CompanyId });
                        command.Parameters.Add(new SqlParameter("@byUser", SqlDbType.Int) { Value = user.ByUser.HasValue? user.ByUser : (object)DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@userName", SqlDbType.NVarChar, 14) { Value = user.UserName });
                        command.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar, 100) { Value = user.Password });
                        command.Parameters.Add(new SqlParameter("@permissions", SqlDbType.BigInt) { Value = user.Permissions });
                        command.Parameters.Add(new SqlParameter("@image", SqlDbType.VarBinary) { Value = user.Image ?? SqlBinary.Null });

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
                    () => user.FullName,
                    () => user.Phone1,
                    () => user.Phone2,
                    () => user.TelegramId,
                    () => user.CompanyId,
                    () => user.ByUser,
                    () => user.UserName,
                    () => user.Password,
                    () => user.Permissions,
                    () => user.Image
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Users_D", "NewUserAsync", ex.StackTrace,
                    user.CompanyId, "Insert New User", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return -1;
            }

            return insertedId;
        }

        //Completed Testing
        public static async Task<bool> UpdateUserAsync(md_UpdateUser user)
        {
            int rowsAffected = -1;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"[dbo].[USERS_SP_UpdateUser]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // تحديد نوع الاستعلام
                        command.CommandType = CommandType.StoredProcedure;

                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@userId", SqlDbType.Int) { Value = user.UserId });
                        command.Parameters.Add(new SqlParameter("@fullName", SqlDbType.NVarChar, 50) { Value = user.FullName });
                        command.Parameters.Add(new SqlParameter("@phone1", SqlDbType.NVarChar, 14) { Value = user.Phone1 });
                        command.Parameters.Add(new SqlParameter("@phone2", SqlDbType.NVarChar, 14) { Value = user.Phone2 ?? (object)DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@telegramId", SqlDbType.NVarChar, 25) { Value = user.TelegramId ?? (object)DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = user.CompanyId });
                        command.Parameters.Add(new SqlParameter("@byUser", SqlDbType.Int) { Value = user.ByUser });
                        command.Parameters.Add(new SqlParameter("@userName", SqlDbType.NVarChar, 14) { Value = user.UserName });
                        command.Parameters.Add(new SqlParameter("@permissions", SqlDbType.BigInt) { Value = user.Permissions });
                        command.Parameters.Add(new SqlParameter("@image", SqlDbType.VarBinary) { Value = user.Image ?? SqlBinary.Null });
                        command.Parameters.Add(new SqlParameter("@isActive", SqlDbType.Bit) { Value = user.IsActive });

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
                    () => user.UserId,
                    () => user.FullName,
                    () => user.Phone1,
                    () => user.Phone2,
                    () => user.TelegramId,
                    () => user.CompanyId,
                    () => user.ByUser,
                    () => user.UserName,
                    () => user.Permissions,
                    () => user.Image,
                    () => user.IsActive
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Users_D", "UpdateUserAsync", ex.StackTrace,
                    user.CompanyId, "Update User", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return rowsAffected > 0;
        }

        //Completed Testing
        public static async Task<bool> UpdateCurrentUserAsync(md_UpdateCurrentUser user)
        {
            int rowsAffected = -1;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"[dbo].[USERS_SP_UpdateCurrentUser]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // تحديد نوع الاستعلام
                        command.CommandType = CommandType.StoredProcedure;

                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@userId", SqlDbType.Int) { Value = user.UserId });
                        command.Parameters.Add(new SqlParameter("@fullName", SqlDbType.NVarChar, 50) { Value = user.FullName });
                        command.Parameters.Add(new SqlParameter("@phone1", SqlDbType.NVarChar, 14) { Value = user.Phone1 });
                        command.Parameters.Add(new SqlParameter("@phone2", SqlDbType.NVarChar, 14) { Value = user.Phone2 ?? (object)DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@telegramId", SqlDbType.NVarChar, 25) { Value = user.TelegramId ?? (object)DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = user.CompanyId });
                        command.Parameters.Add(new SqlParameter("@byUser", SqlDbType.Int) { Value = user.ByUser });
                        command.Parameters.Add(new SqlParameter("@userName", SqlDbType.NVarChar, 14) { Value = user.UserName });
                        command.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar, 100) { Value = user.Password });
                        command.Parameters.Add(new SqlParameter("@image", SqlDbType.VarBinary) { Value = user.Image ?? SqlBinary.Null });
                        command.Parameters.Add(new SqlParameter("@isActive", SqlDbType.Bit) { Value = user.IsActive });

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
                    () => user.UserId,
                    () => user.FullName,
                    () => user.Phone1,
                    () => user.Phone2,
                    () => user.TelegramId,
                    () => user.CompanyId,
                    () => user.ByUser,
                    () => user.UserName,
                    () => user.Password,
                    () => user.Image,
                    () => user.IsActive
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Users_D", "UpdateCurrentUserAsync", ex.StackTrace,
                    user.CompanyId, "Update Current User", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return rowsAffected > 0;
        }

        //Completed Testing
        public static async Task<bool> DeleteUserAsync(int userId, int companyId, int byUser)
        {
            int rowsAffected = -1;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"[dbo].[USERS_SP_DeleteUser]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // تحديد نوع الاستعلام
                        command.CommandType = CommandType.StoredProcedure;

                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@userId", SqlDbType.Int) { Value = userId });
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });
                        command.Parameters.Add(new SqlParameter("@byUser", SqlDbType.Int) { Value = byUser });

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
                    () => userId,
                    () => companyId,
                    () => byUser
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Users_D", "DeleteUserAsync", ex.StackTrace,
                    companyId, "Delete User", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return rowsAffected > 0;
        }
    }
}
