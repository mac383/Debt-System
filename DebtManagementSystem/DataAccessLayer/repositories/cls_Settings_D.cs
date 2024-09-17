using DataAccessLayer.database;
using DataAccessLayer.models.Settings_models;
using DataAccessLayer.models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.repositories
{
    public class cls_Settings_D
    {
        // Completed Testing.
        public static async Task<md_Setting?> GetSettingsAsync(int companyId)
        {
            md_Setting? settings = null;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT * FROM [dbo].[SETTINGS_FUN_GetSettings] (@companyId)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // إضافة المعامل
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });

                        // فتح الاتصال
                        await connection.OpenAsync();

                        // تنفيذ الاستعلام
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                // الحصول على النتائج وتحويلها إلى كائن md_Settings
                                settings = new md_Setting
                                (
                                    reader.GetInt32(reader.GetOrdinal("SettingId")),
                                    reader.GetString(reader.GetOrdinal("CompanyName")),
                                    reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                                    reader.IsDBNull(reader.GetOrdinal("Logo")) ? null : (byte[])reader["Logo"],
                                    reader.GetString(reader.GetOrdinal("Currency")),
                                    reader.IsDBNull(reader.GetOrdinal("PaymentRequestMessage")) ? null : reader.GetString(reader.GetOrdinal("PaymentRequestMessage")),
                                    Convert.ToInt32(reader["CompanyId"])
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
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Settings_D", "GetSettingsAsync", ex.StackTrace,
                    companyId, "Get Settings", Parameters);

                await cls_Errors_D.LogErrorAsync(error);
                return null;
            }

            return settings;
        }

        // Completed Testing.
        public static async Task<bool> UpdateSettingsAsync(md_UpdateSetting settings)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"[dbo].[SETTINGS_SP_UpdateSettings]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // إضافة المعاملات
                        command.Parameters.Add(new SqlParameter("@companyName", SqlDbType.NVarChar, 50) { Value = settings.CompanyName });
                        command.Parameters.Add(new SqlParameter("@currency", SqlDbType.NVarChar, 14) { Value = settings.Currency });
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = settings.CompanyId });
                        command.Parameters.Add(new SqlParameter("@description", SqlDbType.NVarChar) { Value = settings.Description ?? (object)DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@logo", SqlDbType.VarBinary) { Value = settings.Logo ?? SqlBinary.Null });
                        command.Parameters.Add(new SqlParameter("@paymentRequestMessage", SqlDbType.NVarChar, 250) { Value = settings.PaymentRequestMessage ?? (object)DBNull.Value });

                        // إضافة المعامل الخاص بقيمة الرجوع
                        SqlParameter returnParameter = command.Parameters.Add("returnValue", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;

                        // فتح الاتصال
                        await connection.OpenAsync();
                        await command.ExecuteScalarAsync();

                        // الحصول على قيمة الرجوع
                        rowsAffected = (int)returnParameter.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                // جمع المعلومات الخاصة بالخطأ
                string Parameters = cls_Errors_D.GetParams
                (
                    () => settings.CompanyName,
                    () => settings.Description,
                    () => settings.Logo,
                    () => settings.Currency,
                    () => settings.PaymentRequestMessage,
                    () => settings.CompanyId
                );

                // تسجيل الخطأ
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_Settings_D", "UpdateSettingsAsync", ex.StackTrace,
                    settings.CompanyId, "Update Setting", Parameters);

                await cls_Errors_D.LogErrorAsync(error);
                return false;
            }

            return rowsAffected > 0;
        }
    }
}
