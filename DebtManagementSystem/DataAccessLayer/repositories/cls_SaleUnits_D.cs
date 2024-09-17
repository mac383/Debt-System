using DataAccessLayer.database;
using DataAccessLayer.models;
using DataAccessLayer.models.SaleUnit_models;
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
    public class cls_SaleUnits_D
    {
        //Completed Testing
        public static async Task<bool> DeleteSaleUnitAsync(int unitId, int companyId)
        {
            int rowsAffected = -1;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"[dbo].[SALESUNITS_SP_DeleteSaleUnit]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // تحديد نوع الاستعلام
                        command.CommandType = CommandType.StoredProcedure;

                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@unitId", SqlDbType.Int) { Value = unitId });
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
                    () => unitId,
                    () => companyId
                );

                // تسجيل الخطا
                md_Errors? error = new md_Errors(ex.Message, ex.Source, "cls_SaleUnits_D", "DeleteSaleUnitAsync", ex.StackTrace,
                    companyId, "Delete Sale Unit", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return rowsAffected > 0;
        }

        //Completed Testing
        public static async Task<int> NewSaleUnitAsync(md_NewSaleUnit saleunit)
        {
            int insertedId = -1;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"[dbo].[SALESUNITS_SP_NewSaleUnit]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // تحديد نوع الاستعلام
                        command.CommandType = CommandType.StoredProcedure;

                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@unitName", SqlDbType.NVarChar, 50) { Value = saleunit.UnitName });
                        command.Parameters.Add(new SqlParameter("@byUser", SqlDbType.Int) { Value = saleunit.ByUser });
                        command.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = saleunit.CompanyId });

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
                    () => saleunit.UnitName,
                    () => saleunit.ByUser,
                    () => saleunit.CompanyId
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_SaleUnits_D", "NewSaleUnitAsync", ex.StackTrace,
                    saleunit.CompanyId, "Insert Sale Unit", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return -1;
            }

            return insertedId;
        }

        //Completed Testing
        public static async Task<bool> UpdateSaleUnitAsync(md_SaleUnit saleUnit)
        {
            int rowsAffected = -1;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"[dbo].[SALESUNITS_SP_UpdateSaleUnit]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // تحديد نوع الاستعلام
                        command.CommandType = CommandType.StoredProcedure;

                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@unitId", SqlDbType.Int) { Value = saleUnit.SaleUnitID });
                        command.Parameters.Add(new SqlParameter("@unitName", SqlDbType.NVarChar, 50) { Value = saleUnit.UnitName });
                        command.Parameters.Add(new SqlParameter("@byUser", SqlDbType.Int) { Value = saleUnit.ByUser });
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = saleUnit.CompanyId });

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
                    () => saleUnit.SaleUnitID,
                    () => saleUnit.UnitName,
                    () => saleUnit.ByUser,
                    () => saleUnit.CompanyId
                  
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_SaleUnits_D", "UpdateSaleUnitAsync", ex.StackTrace,
                 saleUnit.CompanyId, "Update Sale Unit", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return rowsAffected > 0;

        }

        //Completed Testing
        public static async Task<bool> IsSaleUnitExistAsync(string unitName, int companyId)
        {
            bool isExist = false;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT [dbo].[SALESUNITS_FUN_IsSaleUnitExist] (@unitName, @companyId)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@unitName", SqlDbType.NVarChar, 50) { Value = unitName });
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
                    () => unitName,
                    () => companyId
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_SaleUnits_D", "IsSaleUnitExistAsync", ex.StackTrace,
                    companyId, "Is SaleUnit Exist", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return isExist;
        }

        //Completed Testing
        public static async Task<bool> IsSaleUnitExistWithOutCurrentSaleUnitAsync (int unitId, string unitName, int companyId)
        {
            bool isExist = false;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT [dbo].[SALESUNITS_FUN_IsSaleUnitExistWithOutCurrentUnit](@unitId,@unitName, @companyId)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@unitId", SqlDbType.Int) { Value = unitId });
                        command.Parameters.Add(new SqlParameter("@unitName", SqlDbType.NVarChar, 50) { Value = unitName });
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
                    () => unitId,
                    () => unitName,
                    () => companyId
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_SaleUnits_D", "IsSaleUnitExistWithOutCurrentSaleUnitAsync", ex.StackTrace,
                    companyId, "Check SaleUnit Name Without Current SaleUnit ", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return isExist;
        }

        //Completed Testing
        public static async Task<int> GetSaleUnitsCountAsync(int companyId)
        {
            int count = 0;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT [dbo].[SALESUNITS_FUN_GetSalesUnitsCount](@companyId)";

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
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_SaleUnits_D", "GetSaleUnitsCountAsync", ex.StackTrace,
                    companyId, "Get SaleUnits Count ", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return -1;
            }

            return count;
        }

        //Completed Testing
        public static async Task<bool> IsSaleUnitHasRelationsAsync(int unitId, int companyId)
        {
            bool hasRelations = false;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    // Call the scalar function
                    string query = @"SELECT [dbo].[SALESUNITS_FUN_IsSaleUnitHasRelations](@unitId, @companyId)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Adding parameters
                        command.Parameters.Add(new SqlParameter("@unitId", SqlDbType.Int) { Value = unitId });
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
                        () => unitId,
                        () => companyId
                    );

                // Log the error
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_SaleUnits_D", "IsSaleUnitHasRelationsAsync", ex.StackTrace,
                    companyId, "Is SaleUnit Has Relations", parameters);

                // Save the error to the database
                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return hasRelations;

        }

        //Completed Testing
        public static async Task<List<md_SaleUnits>?> GetSaleUnitsAsync(int companyId)
        {
            List<md_SaleUnits> saleUnits = new List<md_SaleUnits>();

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {

                    string query = @"SELECT * FROM [dbo].[SALESUNITS_FUN_GetSalesUnits](@companyId)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });

                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                saleUnits.Add
                                    (
                                        new md_SaleUnits
                                        (
                                            reader.GetInt32(reader.GetOrdinal("UnitId")),
                                            reader.GetString(reader.GetOrdinal("UnitName")),
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
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_SaleUnits_D", "GetSaleUnitsAsync", ex.StackTrace,
                    companyId, "Get All SaleUnits", Parameters);

                await cls_Errors_D.LogErrorAsync(error);
                return null;
            }

            return saleUnits.Count > 0 ? saleUnits : null;
        }

        //Completed Testing
        public static async Task<md_SaleUnit?> GetSaleUnitByIdAsync(int unitId, int companyId)
        {
            md_SaleUnit? saleUnit = null;

            try
            {
                using (SqlConnection connectin = cls_database.Connection())
                {
                    string query = @"SELECT * FROM [dbo].[SALESUNITS_FUN_GetSaleUnitById] (@unitId, @companyId)";

                    using (SqlCommand command = new SqlCommand(query, connectin))
                    {
                        // اضافة المعلمات
                        command.Parameters.Add(new SqlParameter("@unitId", SqlDbType.Int) { Value = unitId });
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });

                        // فتح الاتصال بقاعدة البيانات
                        await connectin.OpenAsync();

                        // تنفيذ الاستعلام
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                saleUnit = new md_SaleUnit
                                (
                                    reader.GetInt32(reader.GetOrdinal("UnitId")),
                                    reader.GetString(reader.GetOrdinal("UnitName")),
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
                    () => unitId,
                    () => companyId
                );

                // تسجيل الخطا
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_SaleUnits_D", "GetSaleUnitByIdAsync", ex.StackTrace,
                    companyId, "Get SaleUnit By Id", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return null;
            }

            return saleUnit;
        }
    }
}
