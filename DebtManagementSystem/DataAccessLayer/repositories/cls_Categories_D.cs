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
    public class cls_Categories_D
    {

        // Completed Testing.
        public static async Task<bool> IsCategoryExistAsync(string categoryName, int companyId)
        {
            bool isExist = false;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT [dbo].[CATEGORIES_FUN_IsCategoryExist] (@categoryName, @companyId)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@categoryName", SqlDbType.NVarChar, 50) { Value = categoryName });
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
                    () => categoryName,
                    () => companyId
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Categories_D", "IsCategoryExistAsync", ex.StackTrace,
                    companyId, "Check Category Name", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return isExist;
        }

        // Completed Testing.
        public static async Task<bool> IsCategoryExistWithOutCurrentCategoryAsync(int categoryId, string categoryName, int companyId)
        {
            bool isExist = false;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT [dbo].[CATEGORIES_FUN_IsCategoryExistWithOutCurrentCategory](@categoryId,@categoryName, @companyId)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@categoryId", SqlDbType.Int) { Value = categoryId });
                        command.Parameters.Add(new SqlParameter("@categoryName", SqlDbType.NVarChar, 14) { Value = categoryName });
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
                    () => categoryId,
                    () => categoryName,
                    () => companyId
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Categories_D", "IsCategoryExistWithOutCurrentCategoryAsync", ex.StackTrace,
                    companyId, "Check Category Name  Without Current Category ", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return isExist;
        }

        // Completed Testing.
        public static async Task<bool> IsCategoryHasRelationsAsync(int categoryId, int companyId)
        {
            bool hasRelations = false;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    // Call the scalar function
                    string query = @"SELECT dbo.CATEGORIES_FUN_IsCategoryHasRelations(@categoryId, @companyId)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Adding parameters
                        command.Parameters.Add(new SqlParameter("@categoryId", SqlDbType.Int) { Value = categoryId });
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });

                        // Open the connection
                        await connection.OpenAsync();

                        // Execute the query and get the result
                        object? result = await command.ExecuteScalarAsync();

                        // Convert the result to a boolean value
                        if (result != null && result != DBNull.Value)
                            hasRelations = Convert.ToBoolean(result);
                    }
                }
            }
            catch (Exception ex)
            {
                // Collect error information
                string parameters = cls_Errors_D.GetParams
                    (
                        () => categoryId,
                        () => companyId
                    );

                // Log the error
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Categories_D", "IsCategoryHasRelationsAsync", ex.StackTrace, categoryId, "Is Category Has Relations", parameters);

                // Save the error to the database
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return hasRelations;

        }

        // Completed Testing.
        public static async Task<md_Category?> GetCategoryByIdAsync(int categoryId, int companyId)
        {
            md_Category? category = null;

            try
            {
                using (SqlConnection connectin = cls_database.Connection())
                {
                    string query = @"SELECT * FROM [dbo].[CATEGORIES_FUN_GetCategoryById] (@categoryId, @companyId)";

                    using (SqlCommand command = new SqlCommand(query, connectin))
                    {
                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@categoryId", SqlDbType.Int) { Value = categoryId });
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });

                        // فتح الاتصال بقاعدة البيانات
                        await connectin.OpenAsync();

                        // تنفيذ الاستعلام
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                category = new md_Category
                                (
                                            reader.GetInt32(reader.GetOrdinal("CategoryId")),
                                            reader.GetString(reader.GetOrdinal("CategoryName")),
                                            reader.IsDBNull(reader.GetOrdinal("CategoryImage")) ? null : (byte[])reader["Image"],
                                            reader.GetInt32(reader.GetOrdinal("ByUser")),
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
                    () => categoryId,
                    () => companyId
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Categories_D", "GetCategoryByIdAsync", ex.StackTrace,
                    companyId, "Get Category By Id", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return null;
            }

            return category;
        }

        // Completed Testing.
        public static async Task<List<md_Categories>?> GetCategoriesAsync(int companyId)
        {
            List<md_Categories> categories = new List<md_Categories>();

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {

                    string query = @"SELECT * FROM [dbo].[CATEGORIES_FUN_GetCategories](@companyId)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });

                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                categories.Add
                                    (
                                        new md_Categories
                                        (
                                            reader.GetInt32(reader.GetOrdinal("CategoryId")),
                                            reader.GetString(reader.GetOrdinal("CategoryName")),
                                            reader.IsDBNull(reader.GetOrdinal("CategoryImage")) ? null : (byte[])reader["Image"],
                                            reader.GetString(reader.GetOrdinal("ByUser")),
                                            reader.GetInt32(reader.GetOrdinal("Items"))
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
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Categories_D", "GetCategoriesAsync", ex.StackTrace,
                    companyId, "Get All Categories", Parameters);

                await cls_Errors_D.LogErrorAsync(error);
                return null;
            }

            return categories.Count > 0 ? categories : null;
        }

        // Completed Testing.
        public static async Task<bool> DeleteCategoryAsync(int categoryId, int companyId)
        {
            int rowsAffected = -1;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"[dbo].[CATEGORIES_SP_DeleteCategory]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // تحديد نوع الاستعلام
                        command.CommandType = CommandType.StoredProcedure;

                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@categoryId", SqlDbType.Int) { Value = categoryId });
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
                    () => categoryId,
                    () => companyId
                );

                // تسجيل الخطا
                md_Errors? error = new md_Errors(ex.Message, ex.Source, "cls_Categories_D", "DeleteCategoryAsync", ex.StackTrace,
                    categoryId, "Delete Category", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return rowsAffected > 0;
        }

        // Completed Testing.
        public static async Task<int> GetCategoriesCountAsync(int companyId)
        {
            int count = 0;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT [dbo].[CATEGORIES_FUN_GetCategoriesCount](@companyId)";

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
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Categories_D", "GetCategoriesCountAsync", ex.StackTrace,
                    companyId, "Get Categories Count ", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return -1;
            }

            return count;
        }

        // Completed Testing.
        public static async Task<int> NewCategoryAsync(md_Category category)
        {
            int insertedId = -1;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"[dbo].[CATEGORIES_SP_NewCategory]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // تحديد نوع الاستعلام
                        command.CommandType = CommandType.StoredProcedure;

                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@categoryName", SqlDbType.NVarChar, 50) { Value = category.CategoryName });
                        command.Parameters.Add(new SqlParameter("@categoryImage", SqlDbType.VarBinary) { Value = category.Image ?? SqlBinary.Null });
                        command.Parameters.Add(new SqlParameter("@byUser", SqlDbType.Int) { Value = category.ByUser });
                        command.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = category.CompanyId });

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
                    () => category.CategoryName,
                    () => category.Image,
                    () => category.ByUser,
                    () => category.CompanyId
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Categories_D", "NewCategoryAsync", ex.StackTrace,
                    category.CompanyId, "Insert New Category", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return -1;
            }

            return insertedId;
        }

        // Completed Testing.
        public static async Task<bool> UpdateCategoryAsync(md_Category category)
        {
            int rowsAffected = -1;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"[dbo].[CATEGORIES_SP_UpdateCategory]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // تحديد نوع الاستعلام
                        command.CommandType = CommandType.StoredProcedure;

                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@categoryId", SqlDbType.Int) { Value = category.CategoryID });
                        command.Parameters.Add(new SqlParameter("@categoryName", SqlDbType.NVarChar, 50) { Value = category.CategoryName });
                        command.Parameters.Add(new SqlParameter("@categoryImage", SqlDbType.VarBinary) { Value = category.Image ?? SqlBinary.Null });
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = category.CompanyId });
                        command.Parameters.Add(new SqlParameter("@byUser", SqlDbType.Int) { Value = category.ByUser });

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
                    () => category.CategoryID,
                    () => category.CategoryName,
                    () => category.Image,
                    () => category.CompanyId,
                    () => category.ByUser
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Categories_D", "UpdateCategoryAsync", ex.StackTrace,
                   category.CategoryID, "Update Category", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return rowsAffected > 0;
        }

    }
}
