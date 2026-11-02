using System;

namespace Lab09_DataReaderSearch.DTO
{
    public class KhoaDTO
    {
        public string MaKhoa { get; set; }
        public string TenKhoa { get; set; }

        public KhoaDTO() { }

        public KhoaDTO(string maKhoa, string tenKhoa)
        {
            MaKhoa = maKhoa;
            TenKhoa = tenKhoa;
        }

        public override string ToString()
        {
            return TenKhoa;
        }
    }
}
