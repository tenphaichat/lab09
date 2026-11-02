using System;
using System.Data;
using System.Data.SqlClient;

namespace Lab09_DataReaderSearch.DAL
{
    public class KhoaDAL
    {
        public DataTable GetAllKhoa(bool includeAllOption = false)
        {
            string sql = "SELECT MaKhoa, TenKhoa FROM Khoa ORDER BY TenKhoa";
            DataTable table = new DataTable();

            using (SqlConnection conn = DbConnectionFactory.Create())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    table.Load(reader);
                }
            }

            if (includeAllOption)
            {
                DataRow allRow = table.NewRow();
                allRow["MaKhoa"] = "";
                allRow["TenKhoa"] = "-- Tất cả các khoa --";
                table.Rows.InsertAt(allRow, 0);
            }

            return table;
        }
    }
}
