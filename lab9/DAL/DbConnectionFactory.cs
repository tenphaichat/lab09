using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Lab09_DataReaderSearch.DAL
{
    public static class DbConnectionFactory
    {
        private const string DefaultConnectionString =
            @"Server=(localdb)\MSSQLLocalDB;Initial Catalog=QLSinhVien;Integrated Security=True;TrustServerCertificate=True;";

        public static string GetConnectionString(string name = "QLSinhVienConnection")
        {
            try
            {
                var setting = ConfigurationManager.ConnectionStrings[name];
                if (setting != null && !string.IsNullOrWhiteSpace(setting.ConnectionString))
                {
                    return setting.ConnectionString;
                }
            }
            catch
            {
                // Fallback nếu không đọc được config
            }
            return DefaultConnectionString;
        }

        public static SqlConnection Create(string name = "QLSinhVienConnection")
        {
            string connStr = GetConnectionString(name);
            return new SqlConnection(connStr);
        }
    }
}
