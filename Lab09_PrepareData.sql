-- ===================================================================
-- LAB 09: ĐỌC, TÌM KIẾM VÀ LỌC DỮ LIỆU BẰNG SQLDATAREADER
-- Học phần: Lập trình .NET (IT3212) - EAUT
-- Sinh viên: Phan Văn Thắng - MSSV: 24002545
-- CSDL: QLSinhVien
-- Bổ sung dữ liệu để đảm bảo ít nhất 3 trang (mỗi trang 10 sinh viên)
-- ===================================================================

USE QLSinhVien;
GO

-- 1. Đảm bảo các khoa cơ bản tồn tại
IF NOT EXISTS (SELECT 1 FROM Khoa WHERE MaKhoa = 'CNTT')
    INSERT INTO Khoa (MaKhoa, TenKhoa) VALUES ('CNTT', N'Công nghệ thông tin');
IF NOT EXISTS (SELECT 1 FROM Khoa WHERE MaKhoa = 'OTO')
    INSERT INTO Khoa (MaKhoa, TenKhoa) VALUES ('OTO', N'Công nghệ kỹ thuật ô tô');
IF NOT EXISTS (SELECT 1 FROM Khoa WHERE MaKhoa = 'DDT')
    INSERT INTO Khoa (MaKhoa, TenKhoa) VALUES ('DDT', N'Điện - Điện tử');
IF NOT EXISTS (SELECT 1 FROM Khoa WHERE MaKhoa = 'KT')
    INSERT INTO Khoa (MaKhoa, TenKhoa) VALUES ('KT', N'Kinh tế');
IF NOT EXISTS (SELECT 1 FROM Khoa WHERE MaKhoa = 'NN')
    INSERT INTO Khoa (MaKhoa, TenKhoa) VALUES ('NN', N'Ngôn ngữ Anh');
GO

-- 2. Đảm bảo sinh viên tác giả Phan Văn Thắng tồn tại
IF NOT EXISTS (SELECT 1 FROM SinhVien WHERE MaSV = '24002545')
BEGIN
    INSERT INTO SinhVien (MaSV, HoTen, NgaySinh, GioiTinh, MaKhoa, DiemTB, Email)
    VALUES ('24002545', N'Phan Văn Thắng', '2006-03-26', 1, 'CNTT', 8.85, '24002545@eaut.edu.vn');
END
ELSE
BEGIN
    UPDATE SinhVien 
    SET HoTen = N'Phan Văn Thắng', NgaySinh = '2006-03-26', GioiTinh = 1, MaKhoa = 'CNTT', DiemTB = 8.85, Email = '24002545@eaut.edu.vn'
    WHERE MaSV = '24002545';
END
GO

-- 3. Bổ sung các sinh viên mẫu với phổ điểm đa dạng (<5.0, 5.0 - 7.9, >=8.0) và độ tuổi đa dạng
DECLARE @Samples TABLE (
    MaSV VARCHAR(20),
    HoTen NVARCHAR(100),
    NgaySinh DATE,
    GioiTinh BIT,
    MaKhoa VARCHAR(10),
    DiemTB DECIMAL(4,2),
    Email VARCHAR(100)
);

INSERT INTO @Samples (MaSV, HoTen, NgaySinh, GioiTinh, MaKhoa, DiemTB, Email) VALUES
('SV001', N'Nguyễn Văn An', '2005-01-15', 1, 'CNTT', 8.20, 'an.nv@eaut.edu.vn'),
('SV002', N'Trần Thị Bình', '2005-05-20', 0, 'CNTT', 7.40, 'binh.tt@eaut.edu.vn'),
('SV003', N'Lê Văn Cường', '2004-11-10', 1, 'OTO', 6.80, 'cuong.lv@eaut.edu.vn'),
('SV004', N'Phạm Hải Đăng', '2005-09-03', 1, 'KT', 8.00, 'dang.ph@eaut.edu.vn'),
('SV005', N'Hoàng Hương Giang', '2006-02-18', 0, 'KT', 7.80, 'giang.hh@eaut.edu.vn'),
('SV006', N'Đặng Minh Hoàng', '2003-07-22', 1, 'OTO', 6.50, 'hoang.dm@eaut.edu.vn'),
('SV007', N'Đỗ Thúy Linh', '2005-12-05', 0, 'NN', 8.80, 'linh.dt@eaut.edu.vn'),
('SV008', N'Vũ Quang Minh', '2004-04-14', 1, 'DDT', 4.50, 'minh.vq@eaut.edu.vn'),
('SV009', N'Ngô Khánh Huyền', '2006-08-30', 0, 'NN', 9.20, 'huyen.nk@eaut.edu.vn'),
('SV010', N'Bùi Tuấn Kiệt', '2005-03-25', 1, 'CNTT', 7.90, 'kiet.bt@eaut.edu.vn'),
('SV011', N'Trịnh Đức Thắng', '2004-06-12', 1, 'CNTT', 4.20, 'thang.td@eaut.edu.vn'),
('SV012', N'Nguyễn Quỳnh Nga', '2005-10-08', 0, 'KT', 8.60, 'nga.nq@eaut.edu.vn'),
('SV013', N'Đinh Hữu Phước', '2003-01-29', 1, 'OTO', 5.20, 'phuoc.dh@eaut.edu.vn'),
('SV014', N'Cao Phương Thảo', '2006-04-17', 0, 'NN', 8.40, 'thao.cp@eaut.edu.vn'),
('SV015', N'Lương Bảo Nam', '2004-08-21', 1, 'DDT', 3.80, 'nam.lb@eaut.edu.vn'),
('SV016', N'Hà Thị Mai', '2005-02-14', 0, 'CNTT', 8.10, 'mai.ht@eaut.edu.vn'),
('SV017', N'Vương Quốc Cường', '2003-12-01', 1, 'OTO', 7.30, 'cuong.vq@eaut.edu.vn'),
('SV018', N'Tạ Bích Diệp', '2006-07-09', 0, 'KT', 9.00, 'diep.tb@eaut.edu.vn'),
('SV019', N'Phan Thanh Tùng', '2004-03-19', 1, 'CNTT', 4.80, 'tung.pt@eaut.edu.vn'),
('SV020', N'Dương Thùy Trang', '2005-11-28', 0, 'NN', 8.70, 'trang.dt@eaut.edu.vn'),
('SV021', N'Lý Hoàng Long', '2004-09-15', 1, 'DDT', 7.50, 'long.lh@eaut.edu.vn'),
('SV022', N'Mai Hồng Nhung', '2005-06-04', 0, 'KT', 6.20, 'nhung.mh@eaut.edu.vn'),
('SV023', N'Chu Đình Trọng', '2003-05-16', 1, 'OTO', 4.00, 'trong.cd@eaut.edu.vn'),
('SV024', N'Nguyễn Thảo My', '2006-10-11', 0, 'CNTT', 8.90, 'my.nt@eaut.edu.vn'),
('SV025', N'Trần Đại Nghĩa', '2004-12-25', 1, 'DDT', 6.90, 'nghia.td@eaut.edu.vn'),
('SV026', N'Võ Hoàng Yến', '2005-07-07', 0, 'NN', 8.30, 'yen.vh@eaut.edu.vn'),
('SV027', N'Bạch Công Khanh', '2003-08-19', 1, 'KT', 5.80, 'khanh.bc@eaut.edu.vn'),
('SV028', N'Tô Kim Ngân', '2006-01-22', 0, 'CNTT', 8.75, 'ngan.tk@eaut.edu.vn'),
('SV029', N'Đoàn Quốc Tuấn', '2004-02-08', 1, 'OTO', 7.10, 'tuan.dq@eaut.edu.vn'),
('SV030', N'Nguyễn Hải Yến', '2005-04-30', 0, 'NN', 9.50, 'yen.nh@eaut.edu.vn');

MERGE INTO SinhVien AS target
USING @Samples AS source
ON target.MaSV = source.MaSV
WHEN MATCHED THEN
    UPDATE SET 
        target.HoTen = source.HoTen,
        target.NgaySinh = source.NgaySinh,
        target.GioiTinh = source.GioiTinh,
        target.MaKhoa = source.MaKhoa,
        target.DiemTB = source.DiemTB,
        target.Email = source.Email
WHEN NOT MATCHED THEN
    INSERT (MaSV, HoTen, NgaySinh, GioiTinh, MaKhoa, DiemTB, Email)
    VALUES (source.MaSV, source.HoTen, source.NgaySinh, source.GioiTinh, source.MaKhoa, source.DiemTB, source.Email);
GO

SELECT COUNT(*) AS TongSoSinhVien FROM SinhVien;
GO
