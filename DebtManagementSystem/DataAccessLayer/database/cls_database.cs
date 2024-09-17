using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.database
{
    public class cls_database
    {

        private static string? _ConnectionString;

        public static void Initialize(IConfiguration configuration)
        {
            _ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public static SqlConnection Connection()
        {
            return new SqlConnection(_ConnectionString);
        }

    }
}
