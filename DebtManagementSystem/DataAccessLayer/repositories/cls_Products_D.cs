using DataAccessLayer.database;
using DataAccessLayer.models.User_models;
using DataAccessLayer.models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.models.Products_Models;
using System.Data.SqlTypes;

namespace DataAccessLayer.repositories
{
    public class cls_Products_D
    {
        //Completed Testing
        public static async Task<md_Product?> GetProductByCodeAsync(string productCode, int companyId)
        {
            md_Product? product = null;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT * FROM [dbo].[PRODUCTS_FUN_GetProductByCode] (@productCode, @companyId)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@productCode", SqlDbType.NVarChar, 14) { Value = productCode });
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });

                        // فتح الاتصال بقاعدة البيانات
                        await connection.OpenAsync();

                        // تنفيذ الاستعلام
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                // The record was found
                                // قراءة البيانات وحفضها في product model
                                product = new md_Product
                                (
                                    reader.GetInt32(reader.GetOrdinal("ProductId")),
                                    reader.GetString(reader.GetOrdinal("ProductName")),
                                    reader.GetString(reader.GetOrdinal("ProductCode")),
                                    Convert.ToDouble(reader.GetDecimal(reader.GetOrdinal("ProductPrice"))),
                                    reader.IsDBNull(reader.GetOrdinal("ProductImage")) ? null : (byte[])reader["ProductImage"],
                                    reader.GetInt32(reader.GetOrdinal("CategoryId")),
                                    reader.GetInt32(reader.GetOrdinal("UnitId")),
                                    reader.GetInt32(reader.GetOrdinal("ByUser")),
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
                    () => productCode,
                    () => companyId
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Products_D", "GetProductByCodeAsync", ex.StackTrace,
                   companyId, "Get Product By Code", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return null;
            }

            return product;
        }

        //Completed Testing
        public static async Task<md_Product?> GetProductByByIdAsync(int productId, int companyId)
        {
            md_Product? product = null;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT * FROM [dbo].[PRODUCTS_FUN_GetProductById] (@productId, @companyId)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@productId", SqlDbType.Int) { Value = productId });
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });

                        // فتح الاتصال بقاعدة البيانات
                        await connection.OpenAsync();

                        // تنفيذ الاستعلام
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                // The record was found
                                // قراءة البيانات وحفضها في product model
                                product = new md_Product
                                (
                                    reader.GetInt32(reader.GetOrdinal("ProductId")),
                                    reader.GetString(reader.GetOrdinal("ProductName")),
                                    reader.GetString(reader.GetOrdinal("ProductCode")),
                                    Convert.ToDouble(reader.GetDecimal(reader.GetOrdinal("ProductPrice"))),
                                    reader.IsDBNull(reader.GetOrdinal("ProductImage")) ? null : (byte[])reader["ProductImage"],
                                    reader.GetInt32(reader.GetOrdinal("CategoryId")),
                                    reader.GetInt32(reader.GetOrdinal("UnitId")),
                                    reader.GetInt32(reader.GetOrdinal("ByUser")),
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
                    () => productId,
                    () => companyId
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Products_D", "GetProductByByIdAsync", ex.StackTrace,
                   companyId, "Get Product By ID", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return null;
            }

            return product;
        }

        //Completed Testing
        public static async Task<List<md_Products>?> GetProductsAsync(int companyId)
        {
            List<md_Products> products = new List<md_Products>();

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT * FROM [dbo].[PRODUCTS_FUN_GetProducts] (@companyId)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });

                        // فتح الاتصال بقاعدة البيانات
                        await connection.OpenAsync();

                        // تنفيذ الاستعلام
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                products.Add
                                    (
                                        new md_Products
                                            (
                                                reader.GetInt32(reader.GetOrdinal("ProductId")),
                                                reader.GetString(reader.GetOrdinal("ProductName")),
                                                reader.GetString(reader.GetOrdinal("ProductCode")),
                                                Convert.ToDouble(reader.GetDecimal(reader.GetOrdinal("ProductPrice"))),
                                                reader.IsDBNull(reader.GetOrdinal("ProductImage")) ? null : (byte[])reader["ProductImage"],
                                                reader.GetString(reader.GetOrdinal("CategoryName")),
                                                reader.GetString(reader.GetOrdinal("UnitName")),
                                                reader.GetString(reader.GetOrdinal("UserName"))
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
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Products_D", "GetProductsAsync", ex.StackTrace,
                   companyId, "Get Products", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return null;
            }

            return products.Count > 0 ? products : null;
        }

        //Completed Testing
        public static async Task<List<md_Products>?> GetProductsByCategoryId(int categoryId, int companyId)
        {
            List<md_Products> products = new List<md_Products>();

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT * FROM [dbo].[PRODUCTS_FUN_GetProductsByCategoryId] (@categoryId, @companyId)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@categoryId", SqlDbType.Int) { Value = categoryId });
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });

                        // فتح الاتصال بقاعدة البيانات
                        await connection.OpenAsync();

                        // تنفيذ الاستعلام
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                products.Add
                                    (
                                        new md_Products
                                        (
                                            reader.GetInt32(reader.GetOrdinal("ProductId")),
                                            reader.GetString(reader.GetOrdinal("ProductName")),
                                            reader.GetString(reader.GetOrdinal("ProductCode")),
                                            Convert.ToDouble(reader.GetDecimal(reader.GetOrdinal("ProductPrice"))),
                                            reader.IsDBNull(reader.GetOrdinal("ProductImage")) ? null : (byte[])reader["ProductImage"],
                                            reader.GetString(reader.GetOrdinal("CategoryName")),
                                            reader.GetString(reader.GetOrdinal("UnitName")),
                                            reader.GetString(reader.GetOrdinal("UserName"))
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
                    () => categoryId,
                    () => companyId
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Products_D", "GetProductsByCategoryId", ex.StackTrace,
                   companyId, "Get Product By Category", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return null;
            }

            return products.Count > 0 ? products : null;
        }

        //Completed Testing
        public static async Task<int> GetProductsCountAsync(int companyId)
        {
            int count = 0;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT [dbo].[PRODUCTS_FUN_GetProductsCount](@companyId)";

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
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Products_D", "GetProductsCountAsync", ex.StackTrace,
                    companyId, "Get Products Count ", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return -1;
            }

            return count;
        }

        //Completed Testing
        public static async Task<bool> IsProductCodeExistAsync(string productCode)
        {
            bool isExist = false;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT [dbo].[PRODUCTS_FUN_IsProductCodeExist] (@productCode)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@productCode", SqlDbType.NVarChar, 14) { Value = productCode });
                      
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
                    () => productCode
                   
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Products_D", "IsProductCodeExistAsync", ex.StackTrace,
                    -1, "Check Product Code.(Has not company Id)", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return isExist;
        }

        //Completed Testing
        public static async Task<bool> IsProductHasRelationsAsync(int productId, int companyId)
        {
            bool hasRelations = false;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    // Call the scalar function
                    string query = @"SELECT dbo.PRODUCTS_FUN_IsProductHasRelations(@productId, @companyId)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Adding parameters
                        command.Parameters.Add(new SqlParameter("@productId", SqlDbType.Int) { Value = productId });
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
                        () => productId,
                        () => companyId
                    );

                // Log the error
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Products_D", "IsProductHasRelationsAsync", ex.StackTrace,
                    companyId, "Is Product Has Relations", parameters);

                // Save the error to the database
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return hasRelations;
        }

        //Completed Testing
        public static async Task<int> NewProductAsync(md_NewProduct product)
        {
            int insertedId = -1;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"[dbo].[PRODUCTS_SP_NewProduct]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // تحديد نوع الاستعلام
                        command.CommandType = CommandType.StoredProcedure;

                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@productName", SqlDbType.NVarChar, 100) { Value = product.ProductName });
                        command.Parameters.Add(new SqlParameter("@productCode", SqlDbType.NVarChar, 14) { Value = product.ProductCode });
                        command.Parameters.Add(new SqlParameter("@productPrice", SqlDbType.Money) { Value = product.ProductPrice });
                        command.Parameters.Add(new SqlParameter("@productImage", SqlDbType.VarBinary) { Value = product.ProductImage ?? SqlBinary.Null });
                        command.Parameters.Add(new SqlParameter("@categoryId", SqlDbType.Int) { Value = product.CategoryId });
                        command.Parameters.Add(new SqlParameter("@unitId", SqlDbType.Int) { Value = product.UnitId });
                        command.Parameters.Add(new SqlParameter("@byUser", SqlDbType.Int) { Value = product.ByUser });
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = product.CompanyId });

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
                    () => product.ProductName,
                    () => product.ProductCode,
                    () => product.ProductPrice,
                    () => product.ProductImage,
                    () => product.CategoryId,
                    () => product.UnitId,
                    () => product.ByUser,
                    () => product.CompanyId
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Products_D", "NewProductAsync", ex.StackTrace,
                    product.CompanyId, "Insert New Product", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return -1;
            }

            return insertedId;
        }

        //Completed Testing
        public static async Task<bool> UpdateProductAsync(md_UpdateProduct product)
        {
            int rowsAffected = -1;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"[dbo].[PRODUCTS_SP_UpdateProduct]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // تحديد نوع الاستعلام
                        command.CommandType = CommandType.StoredProcedure;

                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@productId", SqlDbType.Int) { Value = product.ProductId });
                        command.Parameters.Add(new SqlParameter("@productName", SqlDbType.NVarChar, 100) { Value = product.ProductName });
                        command.Parameters.Add(new SqlParameter("@productPrice", SqlDbType.Money) { Value = product.ProductPrice });
                        command.Parameters.Add(new SqlParameter("@productImage", SqlDbType.VarBinary) { Value = product.ProductImage ?? SqlBinary.Null });
                        command.Parameters.Add(new SqlParameter("@categoryId", SqlDbType.Int) { Value = product.CategoryId });
                        command.Parameters.Add(new SqlParameter("@unitId", SqlDbType.Int) { Value = product.UnitId });
                        command.Parameters.Add(new SqlParameter("@byUser", SqlDbType.Int) { Value = product.ByUser });
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = product.CompanyId });

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
                    () => product.ProductId,
                    () => product.ProductName,
                    () => product.ProductPrice,
                    () => product.ProductImage,
                    () => product.CategoryId,
                    () => product.UnitId,
                    () => product.ByUser,
                    () => product.CompanyId
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Products_D", "UpdateProductAsync", ex.StackTrace,
                    product.CompanyId, "Update Product", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return rowsAffected > 0;
        }

        //Completed Testing
        public static async Task<bool> DeleteProductAsync(int productId, int companyId)
        {
            int rowsAffected = -1;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"[dbo].[PRODUCTS_SP_DeleteProduct]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // تحديد نوع الاستعلام
                        command.CommandType = CommandType.StoredProcedure;

                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@productId", SqlDbType.Int) { Value = productId });
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
                    () => productId,
                    () => companyId
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Products_D", "DeleteProductAsync", ex.StackTrace,
                    companyId, "Delete Product", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return rowsAffected > 0;
        }
    }
}
