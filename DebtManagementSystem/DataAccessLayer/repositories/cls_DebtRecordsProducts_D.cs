using DataAccessLayer.database;
using DataAccessLayer.models;
using DataAccessLayer.models.DebtRecordsProducts_models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.repositories
{
    public class cls_DebtRecordsProducts_D
    {
        // Completed Testing.
        public static async Task<List<md_DebtRecordsProducts>?> GetDebtRecordsProductsAsync(int debtRecordId, int companyId)
        {
            List<md_DebtRecordsProducts> debtProducts = new List<md_DebtRecordsProducts>();

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"SELECT * FROM [dbo].[DEBTRECORDS_PRODUCTS_FUN_GetByDebtRecordsId] (@debtRecordsId, @companyId)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // إضافة المعامل
                        command.Parameters.Add(new SqlParameter("@debtRecordsId", SqlDbType.Int) { Value = debtRecordId });
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId });

                        // فتح الاتصال
                        await connection.OpenAsync();

                        // تنفيذ الاستعلام
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                debtProducts.Add
                                    (
                                        new md_DebtRecordsProducts
                                        (
                                            reader.GetInt32(reader.GetOrdinal("Debt_Product_Id")),
                                            reader.GetString(reader.GetOrdinal("FullName")),
                                            reader.GetString(reader.GetOrdinal("ProductName")),
                                            Convert.ToDouble(reader.GetDecimal(reader.GetOrdinal("ProductPrice"))),
                                            reader.GetString(reader.GetOrdinal("UnitName")),
                                            reader.GetInt32(reader.GetOrdinal("Quantity")),
                                            Convert.ToDouble(reader.GetDecimal(reader.GetOrdinal("TotalPrice"))),
                                            reader.GetString(reader.GetOrdinal("Currency")),
                                            reader.GetString(reader.GetOrdinal("ByUser")),
                                            reader.GetString(reader.GetOrdinal("IsPaid")),
                                            reader.GetDateTime(reader.GetOrdinal("RegistrationDate"))
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
                md_Errors error = new md_Errors(ex.Message, ex.Source, "cls_DebtRecordsProducts_D", "GetDebtRecordsProducts", ex.StackTrace,
                    companyId, "Get DebtRecordsProducts", Parameters);

                // حفظ الخطا في قاعدة البيانات
                await cls_Errors_D.LogErrorAsync(error);
                return null;
            }

            return debtProducts.Count > 0 ? debtProducts : null;
        }

    }
}
