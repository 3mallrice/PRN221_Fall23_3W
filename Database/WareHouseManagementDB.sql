CREATE DATABASE PRN221_Fall23_3W_WareHouseManagement
USE PRN221_Fall23_3W_WareHouseManagement

-- Tạo bảng Category
CREATE TABLE Category (
    CategoryId INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    CategoryCode VARCHAR(20) NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Status INT NOT NULL
);

-- Tạo bảng StorageArea
CREATE TABLE StorageArea (
    AreaId INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    AreaCode VARCHAR(20) NOT NULL,
    AreaName NVARCHAR(100) NOT NULL,
    Status INT NOT NULL
);

-- Tạo bảng Account
CREATE TABLE Account (
    AccountId INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    AccountCode VARCHAR(20) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Password VARCHAR(50) NOT NULL,
    Role INT NOT NULL,
    Phone VARCHAR(50),
    Status INT NOT NULL
);

-- Tạo bảng Partner
CREATE TABLE Partner (
    PartnerId INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    PartnerCode VARCHAR(20) NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Status INT NOT NULL
);

-- Tạo bảng Lot
CREATE TABLE Lot (
    LotId INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    AccountId INT NOT NULL,
    PartnerId INT NOT NULL,
    LotCode VARCHAR(20) NOT NULL,
    DateIn DATE NOT NULL,
    Note TEXT,
    Status INT NOT NULL,
    FOREIGN KEY (AccountId) REFERENCES Account(AccountId),
    FOREIGN KEY (PartnerId) REFERENCES Partner(PartnerId)
);

-- Tạo bảng StockOut
CREATE TABLE StockOut (
    StockOutId INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    AccountId INT NOT NULL,
    PartnerId INT NOT NULL,
    DateOut DATE NOT NULL,
    Note TEXT,
    Status INT NOT NULL,
    FOREIGN KEY (AccountId) REFERENCES Account(AccountId),
    FOREIGN KEY (PartnerId) REFERENCES Partner(PartnerId)
);

-- Tạo bảng Product
CREATE TABLE Product (
    ProductId INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    CategoryId INT NOT NULL,
    AreaId INT NOT NULL,
    ProductCode VARCHAR(20) NOT NULL,
    Name NVARCHAR(100),
    Quantity INT NOT NULL,
    Status INT NOT NULL,
    FOREIGN KEY (CategoryId) REFERENCES Category(CategoryId),
    FOREIGN KEY (AreaId) REFERENCES StorageArea(AreaId)
);

-- Tạo bảng StockOutDetail
CREATE TABLE StockOutDetail (
    StockOutDetailId INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    ProductId INT NOT NULL,
    StockOutId INT NOT NULL,
    Quantity INT NOT NULL,
    FOREIGN KEY (ProductId) REFERENCES Product(ProductId),
    FOREIGN KEY (StockOutId) REFERENCES StockOut(StockOutId)
);

-- Tạo bảng LotDetail
CREATE TABLE LotDetail (
    LotDetailId INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    LotId INT NOT NULL,
    ProductId INT NOT NULL,
    PartnerId INT NOT NULL,
    Quantity INT NOT NULL,
    Status INT NOT NULL,
    FOREIGN KEY (LotId) REFERENCES Lot(LotId),
    FOREIGN KEY (ProductId) REFERENCES Product(ProductId),
    FOREIGN KEY (PartnerId) REFERENCES Partner(PartnerId)
);
