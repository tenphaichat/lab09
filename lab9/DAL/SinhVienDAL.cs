using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Lab09_DataReaderSearch.DAL
{
    public class SinhVienDAL
    {
        // -------------------------------------------------------------
        // BÀI 1: Nạp SqlDataReader vào DataTable
        // -------------------------------------------------------------
        public DataTable LoadStudents()
        {
            string sql = @"SELECT sv.MaSV, sv.HoTen, sv.NgaySinh, 
                                  CASE sv.GioiTinh WHEN 1 THEN N'Nam' ELSE N'Nữ' END AS GioiTinh, 
                                  k.TenKhoa, sv.DiemTB, sv.Email, sv.MaKhoa
                           FROM SinhVien sv 
                           JOIN Khoa k ON sv.MaKhoa = k.MaKhoa
                           ORDER BY sv.MaSV";

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
            return table;
        }

        // -------------------------------------------------------------
        // BÀI 2: Tìm kiếm gần đúng theo họ tên
        // -------------------------------------------------------------
        public DataTable SearchByName(string keyword)
        {
            string sql = @"SELECT sv.MaSV, sv.HoTen, sv.NgaySinh,
                                  CASE sv.GioiTinh WHEN 1 THEN N'Nam' ELSE N'Nữ' END AS GioiTinh,
                                  k.TenKhoa, sv.DiemTB, sv.Email, sv.MaKhoa
                           FROM SinhVien sv
                           JOIN Khoa k ON sv.MaKhoa = k.MaKhoa
                           WHERE sv.HoTen LIKE @Keyword 
                           ORDER BY sv.HoTen";

            DataTable table = new DataTable();
            using (SqlConnection conn = DbConnectionFactory.Create())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@Keyword", SqlDbType.NVarChar, 100).Value = "%" + (keyword ?? "").Trim() + "%";
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    table.Load(reader);
                }
            }
            return table;
        }

        // -------------------------------------------------------------
        // BÀI 3: Lọc theo khoa và khoảng điểm
        // -------------------------------------------------------------
        public DataTable FilterByKhoaAndScore(string maKhoa, decimal minDiem, decimal maxDiem)
        {
            string sql = @"SELECT sv.MaSV, sv.HoTen, sv.NgaySinh,
                                  CASE sv.GioiTinh WHEN 1 THEN N'Nam' ELSE N'Nữ' END AS GioiTinh,
                                  k.TenKhoa, sv.DiemTB, sv.Email, sv.MaKhoa
                           FROM SinhVien sv
                           JOIN Khoa k ON sv.MaKhoa = k.MaKhoa
                           WHERE sv.DiemTB BETWEEN @Min AND @Max";

            if (!string.IsNullOrEmpty(maKhoa) && maKhoa != "ALL")
            {
                sql += " AND sv.MaKhoa = @MaKhoa";
            }
            sql += " ORDER BY sv.DiemTB DESC, sv.MaSV ASC";

            DataTable table = new DataTable();
            using (SqlConnection conn = DbConnectionFactory.Create())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@Min", SqlDbType.Decimal).Value = minDiem;
                cmd.Parameters.Add("@Max", SqlDbType.Decimal).Value = maxDiem;
                if (!string.IsNullOrEmpty(maKhoa) && maKhoa != "ALL")
                {
                    cmd.Parameters.Add("@MaKhoa", SqlDbType.VarChar, 10).Value = maKhoa;
                }

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    table.Load(reader);
                }
            }
            return table;
        }

        // -------------------------------------------------------------
        // BÀI 4: Phân trang bằng OFFSET-FETCH
        // -------------------------------------------------------------
        public DataTable GetStudentsPaged(int pageIndex, int pageSize, out int totalRecords)
        {
            totalRecords = 0;
            string countSql = "SELECT COUNT(*) FROM SinhVien";
            string pagedSql = @"SELECT sv.MaSV, sv.HoTen, sv.NgaySinh,
                                       CASE sv.GioiTinh WHEN 1 THEN N'Nam' ELSE N'Nữ' END AS GioiTinh,
                                       k.TenKhoa, sv.DiemTB, sv.Email, sv.MaKhoa
                                FROM SinhVien sv
                                JOIN Khoa k ON sv.MaKhoa = k.MaKhoa
                                ORDER BY sv.MaSV
                                OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

            DataTable table = new DataTable();
            using (SqlConnection conn = DbConnectionFactory.Create())
            {
                conn.Open();

                // Đếm tổng số bản ghi
                using (SqlCommand countCmd = new SqlCommand(countSql, conn))
                {
                    totalRecords = Convert.ToInt32(countCmd.ExecuteScalar());
                }

                // Đọc trang dữ liệu
                using (SqlCommand cmd = new SqlCommand(pagedSql, conn))
                {
                    int offset = Math.Max(0, (pageIndex - 1) * pageSize);
                    cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = offset;
                    cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        table.Load(reader);
                    }
                }
            }
            return table;
        }

        private static SqlParameter CloneParam(SqlParameter p)
        {
            return new SqlParameter(p.ParameterName, p.SqlDbType)
            {
                Value = p.Value,
                Size = p.Size,
                Precision = p.Precision,
                Scale = p.Scale
            };
        }

        // -------------------------------------------------------------
        // BÀI 6, 7, 8: Tìm kiếm đa tiêu chí, Sắp xếp an toàn & Lọc theo tuổi
        // -------------------------------------------------------------
        public DataTable SearchAdvanced(
            string maSV,
            string hoTen,
            string maKhoa,
            int? gioiTinh,         // null: Tất cả, 1: Nam, 0: Nữ
            decimal? minDiem,
            decimal? maxDiem,
            int? minTuoi,
            int? maxTuoi,
            string sortColumn,     // Tên cột cần sắp xếp
            string sortDirection,  // ASC hoặc DESC
            int pageIndex,
            int pageSize,
            out int totalRecords)
        {
            totalRecords = 0;

            // Whitelist cột sắp xếp để ngăn chặn SQL Injection (Bài 7)
            Dictionary<string, string> validSortColumns = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "MaSV", "sv.MaSV" },
                { "HoTen", "sv.HoTen" },
                { "NgaySinh", "sv.NgaySinh" },
                { "GioiTinh", "sv.GioiTinh" },
                { "TenKhoa", "k.TenKhoa" },
                { "DiemTB", "sv.DiemTB" },
                { "Email", "sv.Email" },
                { "Tuoi", "(DATEDIFF(YEAR, sv.NgaySinh, GETDATE()) - CASE WHEN DATEADD(YEAR, DATEDIFF(YEAR, sv.NgaySinh, GETDATE()), sv.NgaySinh) > GETDATE() THEN 1 ELSE 0 END)" }
            };

            string safeSortCol = "sv.MaSV";
            if (!string.IsNullOrEmpty(sortColumn) && validSortColumns.ContainsKey(sortColumn))
            {
                safeSortCol = validSortColumns[sortColumn];
            }

            string safeSortDir = "ASC";
            if (!string.IsNullOrEmpty(sortDirection) && sortDirection.Trim().ToUpper() == "DESC")
            {
                safeSortDir = "DESC";
            }

            // Xây dựng điều kiện WHERE động với tham số hóa an toàn
            List<string> whereClauses = new List<string>();
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(maSV))
            {
                whereClauses.Add("sv.MaSV LIKE @MaSV");
                parameters.Add(new SqlParameter("@MaSV", SqlDbType.VarChar, 20) { Value = "%" + maSV.Trim() + "%" });
            }

            if (!string.IsNullOrWhiteSpace(hoTen))
            {
                whereClauses.Add("sv.HoTen LIKE @HoTen");
                parameters.Add(new SqlParameter("@HoTen", SqlDbType.NVarChar, 100) { Value = "%" + hoTen.Trim() + "%" });
            }

            if (!string.IsNullOrWhiteSpace(maKhoa) && maKhoa != "ALL")
            {
                whereClauses.Add("sv.MaKhoa = @MaKhoa");
                parameters.Add(new SqlParameter("@MaKhoa", SqlDbType.VarChar, 10) { Value = maKhoa.Trim() });
            }

            if (gioiTinh.HasValue && (gioiTinh.Value == 0 || gioiTinh.Value == 1))
            {
                whereClauses.Add("sv.GioiTinh = @GioiTinh");
                parameters.Add(new SqlParameter("@GioiTinh", SqlDbType.Bit) { Value = (gioiTinh.Value == 1) });
            }

            if (minDiem.HasValue)
            {
                whereClauses.Add("sv.DiemTB >= @MinDiem");
                parameters.Add(new SqlParameter("@MinDiem", SqlDbType.Decimal) { Value = minDiem.Value });
            }

            if (maxDiem.HasValue)
            {
                whereClauses.Add("sv.DiemTB <= @MaxDiem");
                parameters.Add(new SqlParameter("@MaxDiem", SqlDbType.Decimal) { Value = maxDiem.Value });
            }

            // Bài 8: Lọc theo tuổi chính xác
            string ageExpr = "(DATEDIFF(YEAR, sv.NgaySinh, GETDATE()) - CASE WHEN DATEADD(YEAR, DATEDIFF(YEAR, sv.NgaySinh, GETDATE()), sv.NgaySinh) > GETDATE() THEN 1 ELSE 0 END)";
            if (minTuoi.HasValue)
            {
                whereClauses.Add($"{ageExpr} >= @MinTuoi");
                parameters.Add(new SqlParameter("@MinTuoi", SqlDbType.Int) { Value = minTuoi.Value });
            }

            if (maxTuoi.HasValue)
            {
                whereClauses.Add($"{ageExpr} <= @MaxTuoi");
                parameters.Add(new SqlParameter("@MaxTuoi", SqlDbType.Int) { Value = maxTuoi.Value });
            }

            string whereSql = whereClauses.Count > 0 ? " WHERE " + string.Join(" AND ", whereClauses) : "";

            string baseFrom = @"FROM SinhVien sv 
                                JOIN Khoa k ON sv.MaKhoa = k.MaKhoa" + whereSql;

            string countSql = "SELECT COUNT(*) " + baseFrom;

            string dataSql = $@"SELECT sv.MaSV, sv.HoTen, sv.NgaySinh,
                                       {ageExpr} AS Tuoi,
                                       CASE sv.GioiTinh WHEN 1 THEN N'Nam' ELSE N'Nữ' END AS GioiTinh,
                                       k.TenKhoa, sv.DiemTB, sv.Email, sv.MaKhoa
                                {baseFrom}
                                ORDER BY {safeSortCol} {safeSortDir}
                                OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

            DataTable table = new DataTable();
            using (SqlConnection conn = DbConnectionFactory.Create())
            {
                conn.Open();

                // 1. Đếm tổng số bản ghi thỏa điều kiện
                using (SqlCommand countCmd = new SqlCommand(countSql, conn))
                {
                    foreach (var p in parameters)
                    {
                        countCmd.Parameters.Add(CloneParam(p));
                    }
                    totalRecords = Convert.ToInt32(countCmd.ExecuteScalar());
                }

                // 2. Lấy dữ liệu phân trang
                using (SqlCommand cmd = new SqlCommand(dataSql, conn))
                {
                    foreach (var p in parameters)
                    {
                        cmd.Parameters.Add(CloneParam(p));
                    }

                    int offset = Math.Max(0, (pageIndex - 1) * pageSize);
                    cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = offset;
                    cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        table.Load(reader);
                    }
                }
            }

            return table;
        }
    }
}
