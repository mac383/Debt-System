using DataAccessLayer.database;
using DataAccessLayer.models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.repositories
{
    public class cls_Errors_D
    {
        public static async Task<bool> LogErrorAsync(md_Errors error)
        {
            int insertedId = 0;

            try
            {
                using (SqlConnection connection = cls_database.Connection())
                {
                    string query = @"[dbo].[ERRORS_SP_NewError]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@errorMessage", SqlDbType.NVarChar, 250) { Value = error.ErrorMessage });
                        command.Parameters.Add(new SqlParameter("@source", SqlDbType.NVarChar, 50) { Value = error.Source });
                        command.Parameters.Add(new SqlParameter("@class", SqlDbType.NVarChar, 50) { Value = error.Class });
                        command.Parameters.Add(new SqlParameter("@method", SqlDbType.NVarChar, 50) { Value = error.Method });
                        command.Parameters.Add(new SqlParameter("@stackTrace", SqlDbType.NVarChar, 250) { Value = error.StackTrace });
                        command.Parameters.Add(new SqlParameter("@companyId", SqlDbType.Int) { Value = error.CompanyId });
                        command.Parameters.Add(new SqlParameter("@action", SqlDbType.NVarChar, 50) { Value = error.Action });
                        command.Parameters.Add(new SqlParameter("@params", SqlDbType.NVarChar, 500) { Value = error.Parameters });

                        SqlParameter returnParameter = command.Parameters.Add("returnValue", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;

                        await connection.OpenAsync();
                        await command.ExecuteScalarAsync();

                        insertedId = (int)returnParameter.Value;
                    }
                }
            }
            catch
            {
                return false;
            }

            return insertedId > 0;
        }

        public static string GetParams(params Expression<Func<object?>>[] expressions)
        {
            var results = new List<string>();

            foreach (var expression in expressions)
            {
                MemberExpression? memberExpression = null;

                if (expression.Body is MemberExpression)
                {
                    memberExpression = (MemberExpression)expression.Body;
                }
                else if (expression.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression)
                {
                    memberExpression = (MemberExpression)unaryExpression.Operand;
                }

                if (memberExpression != null)
                {
                    string parameterName = memberExpression.Member.Name;
                    object parameterValue = expression.Compile().Invoke();
                    string? valueString = parameterValue == null ? "null" : parameterValue.ToString();
                    results.Add($"name: {parameterName}, value: {valueString}");
                }
            }

            return string.Join(Environment.NewLine, results);

            /*
                INVOKE
                MyParams = GetParams
                (
                    () => var1,
                    () => var2,
                    () => var3
                );
             */

        }

    }
}
