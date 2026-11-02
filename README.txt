================================================================================
BỘ MÔN CÔNG NGHỆ PHẦN MỀM - KHOA CÔNG NGHỆ THÔNG TIN - ĐẠI HỌC CÔNG NGHỆ ĐÔNG Á
BÁO CÁO BÀI TẬP THỰC HÀNH LAB 9: ĐỌC, TÌM KIẾM VÀ LỌC DỮ LIỆU BẰNG SQLDATAREADER
Học phần: Lập trình .NET (IT3212)
Sinh viên thực hiện: Phan Văn Thắng
Mã số sinh viên: 24002545
Lớp: IT3212
================================================================================

1. TỔNG QUAN VÀ HƯỚNG DẪN CẤU HÌNH MÔI TRƯỜNG
- Yêu cầu môi trường: Visual Studio 2022 hoặc .NET SDK (>= .NET 8 / .NET 9 hỗ trợ Windows Forms Desktop).
- Máy chủ CSDL: Microsoft SQL Server 2019/2022 hoặc SQL Server LocalDB: (localdb)\MSSQLLocalDB.
- CSDL sử dụng: QLSinhVien (Bảng SinhVien, Khoa).
- Cấu hình chuỗi kết nối tại tệp: lab9/App.config
  + Kết nối mặc định Windows Authentication:
      <add name="QLSinhVienConnection" 
           connectionString="Server=(localdb)\MSSQLLocalDB;Initial Catalog=QLSinhVien;Integrated Security=True;TrustServerCertificate=True;" />
  + Kết nối SQL Server Authentication (nếu cần):
      <add name="QLSinhVienSqlAuth" 
           connectionString="Server=(localdb)\MSSQLLocalDB;Initial Catalog=QLSinhVien;User Id=AppUser;Password=P@ssword123!;TrustServerCertificate=True;" />

2. HƯỚNG DẪN BIÊN DỊCH VÀ CHẠY DỰ ÁN
- Bước 1: Khởi tạo và nạp dữ liệu phong phú cho CSDL QLSinhVien
  + Mở tệp Lab09_PrepareData.sql trong SSMS hoặc Azure Data Studio trên CSDL QLSinhVien.
  + Nhấn F5 (Execute) để thực thi. Kịch bản sẽ nạp 31 bản ghi sinh viên với đầy đủ độ tuổi (18 - 26+), phổ điểm đa dạng (<5.0, 5.0-7.9, >=8.0) phục vụ kiểm thử phân trang ít nhất 3 trang và tô màu cảnh báo học tập.
- Bước 2: Biên dịch và chạy ứng dụng WinForms
  + Mở tệp solution: IT3212_Labs.sln bằng Visual Studio 2022 -> Nhấn F5 hoặc Ctrl+F5 để khởi chạy.
  + Hoặc chạy bằng dòng lệnh .NET CLI:
      cd lab9
      dotnet run --project lab9/Lab09_DataReaderSearch.csproj

3. CẤU TRÚC VÀ TỔ CHỨC MÃ NGUỒN
- Solution: IT3212_Labs.sln
- Project: lab9/Lab09_DataReaderSearch.csproj (.NET 9 Windows Forms)
- Tệp kịch bản CSDL: Lab09_PrepareData.sql
- Tầng DTO (Data Transfer Object) - Namespace: Lab09_DataReaderSearch.DTO:
  + KhoaDTO.cs: Thực thể Khoa (MaKhoa, TenKhoa).
  + SinhVienDTO.cs: Thực thể SinhVien (MaSV, HoTen, NgaySinh, GioiTinh, MaKhoa, TenKhoa, DiemTB, Email, Tuoi).
- Tầng DAL (Data Access Layer) - Namespace: Lab09_DataReaderSearch.DAL:
  + DbConnectionFactory.cs: Nhà máy khởi tạo kết nối SqlConnection, đọc an toàn từ App.config.
  + KhoaDAL.cs: Truy vấn danh mục khoa bằng SqlDataReader, nạp DataTable hỗ trợ mục "-- Tất cả các khoa --".
  + SinhVienDAL.cs: Triển khai 100% các nghiệp vụ đọc dữ liệu mô hình kết nối:
      * LoadStudents(): Nạp toàn bộ sinh viên kèm tên khoa bằng SqlDataReader và table.Load(reader) (Bài 1).
      * SearchByName(): Tìm kiếm gần đúng họ tên bằng SqlParameter an toàn (Bài 2).
      * FilterByKhoaAndScore(): Lọc theo Khoa và khoảng điểm TB (Bài 3).
      * GetStudentsPaged(): Phân trang dữ liệu bằng cú pháp OFFSET-FETCH kết hợp ORDER BY (Bài 4).
      * SearchAdvanced(): Tìm kiếm đa tiêu chí, lọc độ tuổi chính xác, sắp xếp danh sách cột an toàn chống SQL Injection (Bài 6, 7, 8).
  + CsvExporter.cs: Tiện ích xuất dữ liệu trên DataGridView ra tệp CSV hỗ trợ tiếng Việt Unicode UTF-8 kèm BOM (\uFEFF) mở hoàn hảo trên Microsoft Excel (Bài 10).
- Tầng GUI (Presentation Layer) - Namespace: Lab09_DataReaderSearch.GUI:
  + frmMain.cs & frmMain.Designer.cs: Giao diện chuẩn mực Windows Forms, phân vùng rõ ràng:
      * Vùng tiêu đề thương hiệu Trường Đại học Công nghệ Đông Á (EAUT).
      * Vùng bộ lọc và tiêu chí tra cứu đa dạng (Mã, Họ tên, Khoa, Giới tính, Điểm, Tuổi, Sắp xếp).
      * Vùng hiển thị DataGridView với thanh điều hướng phân trang (|<, <, Trang X/Y, >, >|, Chọn số dòng).
      * Vùng hiển thị thông tin chi tiết sinh viên được chọn, tự động nạp ảnh đại diện theo mã/giới tính.
      * Thanh trạng thái StatusStrip phản hồi thời gian thực: "Đang chọn: [MaSV] - [HoTen] | Trang X/Y | Tổng số: Z".
      * Hộp chọn kịch bản mẫu (Demo Mode) giúp giảng viên/người dùng kiểm thử nhanh từng bài tập chỉ với 1 cú click chuột.
  + ScreenshotCapture.cs: Tiện ích chụp màn hình tự động kết xuất ảnh giao diện sắc nét có khung cửa sổ chuẩn phục vụ làm báo cáo thực hành.

4. DANH SÁCH BÀI TẬP ĐÃ HOÀN THÀNH (10/10 BÀI)
* PHẦN 1: BÀI TẬP CÓ HƯỚNG DẪN (BÀI 1 - 5)
- Bài 1: Nạp SqlDataReader vào DataTable:
  + Truy vấn JOIN giữa bảng SinhVien và Khoa, đọc bằng ExecuteReader().
  + Sử dụng phương thức DataTable.Load(reader) chuyển toàn bộ dữ liệu luồng vào bảng nhớ và gán DataSource cho DataGridView.
- Bài 2: Tìm kiếm gần đúng theo họ tên:
  + Tìm kiếm theo điều kiện: WHERE HoTen LIKE @Keyword.
  + Tham số hóa an toàn bằng SqlParameter: cmd.Parameters.Add("@Keyword", SqlDbType.NVarChar, 100).Value = "%" + keyword.Trim() + "%";
  + Tuyệt đối không nối chuỗi SQL, phòng chống triệt để tấn công SQL Injection.
- Bài 3: Lọc theo khoa và khoảng điểm:
  + Cho phép chọn một khoa cụ thể hoặc "-- Tất cả các khoa --".
  + Nhập điểm tối thiểu và tối đa (NumericUpDown).
  + Mọi giá trị lọc đều được truyền qua tham số @Min, @Max, @MaKhoa.
- Bài 4: Phân trang bằng OFFSET-FETCH:
  + Áp dụng cú pháp chuẩn của SQL Server 2012+:
      ORDER BY MaSV OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY
  + Cho phép chọn số lượng sinh viên mỗi trang (5, 10, 15, 20 dòng).
  + Cung cấp đầy đủ các nút điều hướng: Trang đầu (|<), Trang trước (<), Trang sau (>), Trang cuối (>|).
- Bài 5: Hiển thị chi tiết dòng được chọn:
  + Bắt sự kiện SelectionChanged và CellClick của DataGridView.
  + Trích xuất dữ liệu dòng hiện tại (CurrentRow), kiểm tra giá trị null/DBNull.
  + Đồng bộ lên các điều khiển chi tiết: Mã SV, Họ tên, Ngày sinh, Tuổi, Giới tính (Radio), Khoa, Điểm TB (kèm xếp loại), Email và Ảnh chân dung sinh viên.
  + Cập nhật thông báo thanh trạng thái StatusStrip: "Đang chọn: [MaSV] - [HoTen]...".

* PHẦN 2: BÀI TẬP TỰ THỰC HIỆN (BÀI 6 - 10)
- Bài 6: Tìm kiếm đa tiêu chí:
  + Kết hợp đồng thời nhiều tiêu chí: Mã SV, Họ tên, Khoa, Giới tính (Tất cả / Nam / Nữ) và Khoảng điểm TB.
  + Cho phép bỏ trống bất kỳ tiêu chí nào, hệ thống tự động sinh mệnh đề WHERE tối ưu với tham số hóa an toàn.
- Bài 7: Sắp xếp theo lựa chọn:
  + Cho phép người dùng chọn cột sắp xếp (Mã SV, Họ tên, Điểm TB, Ngày sinh, Khoa, Tuổi) và chiều sắp xếp (Tăng dần ASC / Giảm dần DESC).
  + Kiểm tra danh sách cột hợp lệ (Whitelist Validation) trong mã C# trước khi đưa vào câu lệnh SQL, ngăn ngừa nguy cơ SQL Injection do tên cột không thể bind bằng SqlParameter.
- Bài 8: Lọc theo tuổi:
  + Tìm sinh viên trong khoảng độ tuổi từ TuoiMin đến TuoiMax.
  + Tính toán tuổi chính xác theo ngày tháng năm sinh thực tế:
      (DATEDIFF(YEAR, sv.NgaySinh, GETDATE()) - CASE WHEN DATEADD(YEAR, DATEDIFF(YEAR, sv.NgaySinh, GETDATE()), sv.NgaySinh) > GETDATE() THEN 1 ELSE 0 END)
- Bài 9: Định dạng DataGridView và tô màu điểm số:
  + Định dạng ngày sinh hiển thị theo chuẩn Việt Nam: dd/MM/yyyy.
  + Định dạng điểm trung bình hiển thị số thực 2 chữ số thập phân: 0.00.
  + Căn lề trực quan: Mã SV, Giới tính, Ngày sinh, Tuổi căn giữa; Họ tên, Khoa, Email căn trái; Điểm TB căn phải.
  + Xử lý sự kiện CellFormatting tô màu trực quan:
      * Sinh viên có DiemTB < 5.0: Nền màu hồng đỏ nhạt, chữ đỏ đậm cảnh báo học tập.
      * Sinh viên có DiemTB >= 8.0: Nền màu xanh lá nhạt, chữ xanh đậm in đậm biểu dương sinh viên khá giỏi.
- Bài 10: Xuất kết quả tìm kiếm ra tệp CSV:
  + Cho phép xuất đúng tập dữ liệu đang hiển thị trên DataGridView ra tệp CSV.
  + Hỗ trợ bộ mã tiếng Việt Unicode UTF-8 kèm Byte Order Mark (\uFEFF) giúp mở trực tiếp bằng Microsoft Excel mà không bị lỗi font tiếng Việt.
  + Xử lý đóng mở ngoặc kép an toàn cho các trường chứa dấu phẩy, dấu nháy kép hoặc ký tự xuống dòng.
  + Hộp thoại SaveFileDialog cho phép người dùng chủ động chọn nơi lưu tệp.

5. DỮ LIỆU KIỂM THỬ MẪU (TEST CASES)
- Trường hợp 1 (Bài 1): Nạp toàn bộ danh sách -> DataGridView hiển thị 10 dòng đầu tiên của trang 1 trên tổng số 31 sinh viên.
- Trường hợp 2 (Bài 2): Nhập "Thắng" vào ô Họ tên -> Nhấn Tìm kiếm -> Hiển thị sinh viên "Phan Văn Thắng" (24002545) và "Trịnh Đức Thắng" (SV011).
- Trường hợp 3 (Bài 3): Chọn Khoa "Công nghệ thông tin", Điểm TB từ 7.0 đến 9.0 -> Hiển thị danh sách các sinh viên CNTT có điểm thỏa mãn.
- Trường hợp 4 (Bài 4): Nhấn nút "Sau >" -> DataGridView chuyển sang Trang 2 / 4, hiển thị từ sinh viên SV011 đến SV020.
- Trường hợp 5 (Bài 5): Click chọn dòng sinh viên "24002545 - Phan Văn Thắng" -> Vùng chi tiết bên phải lập tức hiển thị đầy đủ thông tin cá nhân kèm ảnh chân dung đại diện; thanh trạng thái cập nhật: "Đang chọn: 24002545 - Phan Văn Thắng...".
- Trường hợp 6 (Bài 6): Chọn Giới tính "Nam", Điểm TB từ 7.5 đến 10.0 -> Hệ thống lọc chính xác các nam sinh viên có kết quả học tập khá giỏi.
- Trường hợp 7 (Bài 7): Chọn cột "Điểm TB", thứ tự "Giảm dần (DESC)" -> Nhấn Tìm kiếm -> Danh sách sinh viên được sắp xếp với điểm cao nhất đứng đầu (SV030: 9.50, SV009: 9.20, SV018: 9.00, 24002545: 8.85...).
- Trường hợp 8 (Bài 8): Chọn tuổi từ 20 đến 22 tuổi -> Hệ thống lọc danh sách sinh viên sinh từ năm 2004 đến 2006.
- Trường hợp 9 (Bài 9): Quan sát DataGridView -> Sinh viên SV008 (4.50), SV011 (4.20), SV015 (3.80), SV019 (4.80), SV023 (4.00) được tô nền đỏ nhạt; sinh viên 24002545 (8.85), SV001 (8.20), SV004 (8.00), SV007 (8.80) được tô nền xanh lá nhạt.
- Trường hợp 10 (Bài 10): Nhấn nút "Xuất CSV" -> Lưu tệp "DanhSachSinhVien_Exported.csv" -> Mở tệp bằng Excel -> Hiển thị tiếng Việt có dấu chuẩn xác 100%.
================================================================================
