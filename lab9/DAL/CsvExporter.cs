using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Lab09_DataReaderSearch.DAL
{
    public static class CsvExporter
    {
        public static string ExportToCsv(DataGridView dgv, string defaultFileName = "DanhSachSinhVien.csv")
        {
            if (dgv == null || dgv.Rows.Count == 0)
            {
                throw new InvalidOperationException("Không có dữ liệu trên bảng để xuất CSV.");
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                sfd.FileName = defaultFileName;
                sfd.Title = "Lưu kết quả tìm kiếm ra tệp CSV (UTF-8)";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ExportToCsvFile(dgv, sfd.FileName);
                    return sfd.FileName;
                }
            }
            return null;
        }

        public static void ExportToCsvFile(DataGridView dgv, string filePath)
        {
            var sb = new StringBuilder();

            // Header columns
            var headers = new System.Collections.Generic.List<string>();
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (col.Visible)
                {
                    headers.Add(EscapeCsvField(col.HeaderText));
                }
            }
            sb.AppendLine(string.Join(",", headers));

            // Rows
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;

                var rowFields = new System.Collections.Generic.List<string>();
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (col.Visible)
                    {
                        object val = row.Cells[col.Index].Value;
                        string strVal = "";

                        if (val != null && val != DBNull.Value)
                        {
                            if (val is DateTime dt)
                            {
                                strVal = dt.ToString("dd/MM/yyyy");
                            }
                            else if (val is decimal dec)
                            {
                                strVal = dec.ToString("0.00");
                            }
                            else
                            {
                                strVal = val.ToString();
                            }
                        }
                        rowFields.Add(EscapeCsvField(strVal));
                    }
                }
                sb.AppendLine(string.Join(",", rowFields));
            }

            // UTF-8 with BOM (\uFEFF) ensures Excel opens Vietnamese accents perfectly
            File.WriteAllText(filePath, sb.ToString(), new UTF8Encoding(true));
        }

        private static string EscapeCsvField(string field)
        {
            if (string.IsNullOrEmpty(field)) return "";
            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r"))
            {
                return "\"" + field.Replace("\"", "\"\"") + "\"";
            }
            return field;
        }
    }
}
