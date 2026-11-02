using System;

namespace Lab09_DataReaderSearch.DTO
{
    public class SinhVienDTO
    {
        public string MaSV { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public bool GioiTinh { get; set; }
        public string MaKhoa { get; set; }
        public string TenKhoa { get; set; }
        public decimal DiemTB { get; set; }
        public string Email { get; set; }

        public string GioiTinhStr => GioiTinh ? "Nam" : "Nữ";

        public int Tuoi
        {
            get
            {
                var today = DateTime.Today;
                int age = today.Year - NgaySinh.Year;
                if (NgaySinh.Date > today.AddYears(-age)) age--;
                return age;
            }
        }
    }
}
