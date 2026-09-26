-- 1. Tạo Database Nhà Thuốc Hồng Phúc
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'NhaThuocHongPhuc')
BEGIN
    CREATE DATABASE NhaThuocHongPhuc;
END
GO

USE NhaThuocHongPhuc;
GO

-- 2. Xóa bảng cũ nếu đã tồn tại (để reset dữ liệu sạch)
IF OBJECT_ID('dbo.ChiTietHoaDon', 'U') IS NOT NULL DROP TABLE dbo.ChiTietHoaDon;
IF OBJECT_ID('dbo.HoaDon', 'U') IS NOT NULL DROP TABLE dbo.HoaDon;
IF OBJECT_ID('dbo.LoThuoc', 'U') IS NOT NULL DROP TABLE dbo.LoThuoc;
IF OBJECT_ID('dbo.DanhMucThuoc', 'U') IS NOT NULL DROP TABLE dbo.DanhMucThuoc;
IF OBJECT_ID('dbo.NhaCungCap', 'U') IS NOT NULL DROP TABLE dbo.NhaCungCap;
IF OBJECT_ID('dbo.NhanVien', 'U') IS NOT NULL DROP TABLE dbo.NhanVien;
IF OBJECT_ID('dbo.KhachHang', 'U') IS NOT NULL DROP TABLE dbo.KhachHang;
GO

-- 3. Bảng Nhà Cung Cấp
CREATE TABLE dbo.NhaCungCap (
    MaNCC INT IDENTITY(1,1) PRIMARY KEY,
    TenNCC NVARCHAR(150) NOT NULL,
    SoDienThoai VARCHAR(15),
    DiaChi NVARCHAR(255)
);

-- 4. Bảng Danh Mục Thuốc
CREATE TABLE dbo.DanhMucThuoc (
    MaThuoc INT IDENTITY(1,1) PRIMARY KEY,
    TenThuoc NVARCHAR(150) NOT NULL,
    HoatChat NVARCHAR(255),
    DonViTinh NVARCHAR(50) NOT NULL,
    GiaNhap DECIMAL(12, 2) NOT NULL CHECK (GiaNhap >= 0),
    GiaBan DECIMAL(12, 2) NOT NULL CHECK (GiaBan >= 0),
    KeThuoc NVARCHAR(50)
);

-- 5. Bảng Quản Lý Lô Thuốc & Hạn Sử Dụng
CREATE TABLE dbo.LoThuoc (
    MaLo INT IDENTITY(1,1) PRIMARY KEY,
    MaThuoc INT NOT NULL,
    SoLo VARCHAR(50) NOT NULL,
    NgaySanXuat DATE,
    HanSuDung DATE NOT NULL,
    SoLuongTon INT NOT NULL DEFAULT 0 CHECK (SoLuongTon >= 0),
    CONSTRAINT FK_LoThuoc_Thuoc FOREIGN KEY (MaThuoc) REFERENCES dbo.DanhMucThuoc(MaThuoc) ON DELETE CASCADE
);

-- 6. Bảng Nhân Viên
CREATE TABLE dbo.NhanVien (
    MaNV INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    ChucVu NVARCHAR(50) DEFAULT N'Dược sĩ bán thuốc',
    SoDienThoai VARCHAR(15) UNIQUE
);

-- 7. Bảng Khách Hàng
CREATE TABLE dbo.KhachHang (
    MaKH INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL DEFAULT N'Khách lẻ',
    SoDienThoai VARCHAR(15),
    DiemTichLuy INT DEFAULT 0
);

-- 8. Bảng Hóa Đơn Bán Hàng
CREATE TABLE dbo.HoaDon (
    MaHD INT IDENTITY(1,1) PRIMARY KEY,
    MaNV INT,
    MaKH INT,
    NgayLap DATETIME2 DEFAULT GETDATE(),
    TongTien DECIMAL(12, 2) DEFAULT 0,
    CONSTRAINT FK_HoaDon_NhanVien FOREIGN KEY (MaNV) REFERENCES dbo.NhanVien(MaNV) ON DELETE SET NULL,
    CONSTRAINT FK_HoaDon_KhachHang FOREIGN KEY (MaKH) REFERENCES dbo.KhachHang(MaKH) ON DELETE SET NULL
);

-- 9. Bảng Chi Tiết Hóa Đơn
CREATE TABLE dbo.ChiTietHoaDon (
    MaHD INT,
    MaLo INT,
    SoLuong INT NOT NULL CHECK (SoLuong > 0),
    DonGia DECIMAL(12, 2) NOT NULL,
    PRIMARY KEY (MaHD, MaLo),
    CONSTRAINT FK_CTHD_HoaDon FOREIGN KEY (MaHD) REFERENCES dbo.HoaDon(MaHD) ON DELETE CASCADE,
    CONSTRAINT FK_CTHD_LoThuoc FOREIGN KEY (MaLo) REFERENCES dbo.LoThuoc(MaLo)
);
GO

-- =============================================
-- CHÈN DỮ LIỆU MẪU (DATA SEEDING)
-- =============================================

INSERT INTO dbo.NhaCungCap (TenNCC, SoDienThoai, DiaChi) VALUES
(N'Dược Hậu Giang (DHG)', '02923891433', N'Cần Thơ'),
(N'Pharmacity Supplier', '18006821', N'TP. Hồ Chí Minh');

INSERT INTO dbo.DanhMucThuoc (TenThuoc, HoatChat, DonViTinh, GiaNhap, GiaBan, KeThuoc) VALUES
(N'Hapacol 500', 'Paracetamol 500mg', N'Hộp', 35000, 45000, N'Kệ A1'),
(N'Berberin 100mg', 'Berberin', N'Lọ', 15000, 22000, N'Kệ B2'),
(N'Vitamin C 500mg', 'Acid Ascorbic', N'Vỉ', 8000, 12000, N'Kệ C1');

INSERT INTO dbo.LoThuoc (MaThuoc, SoLo, NgaySanXuat, HanSuDung, SoLuongTon) VALUES
(1, 'LOT202601', '2026-01-10', '2028-01-10', 100),
(2, 'LOT202602', '2026-02-15', '2027-02-15', 50),
(3, 'LOT202603', '2026-03-01', '2027-09-01', 200);

INSERT INTO dbo.NhanVien (HoTen, ChucVu, SoDienThoai) VALUES
(N'Dược sĩ Nguyễn Văn Phúc', N'Quản lý', '0909123456'),
(N'Trần Thị Hồng', N'Dược sĩ bán thuốc', '0908654321');

INSERT INTO dbo.KhachHang (HoTen, SoDienThoai, DiemTichLuy) VALUES
(N'Khách Lẻ', NULL, 0),
(N'Lê Văn Nam', '0912333444', 15);

INSERT INTO dbo.HoaDon (MaNV, MaKH, TongTien) VALUES 
(2, 2, 69000);

INSERT INTO dbo.ChiTietHoaDon (MaHD, MaLo, SoLuong, DonGia) VALUES 
(1, 1, 1, 45000),
(1, 2, 1, 24000);
GO

-- Kiểm tra dữ liệu vừa chèn
SELECT * FROM dbo.DanhMucThuoc;
SELECT * FROM dbo.LoThuoc;