USE PRN221_Fall23_3W_WareHouseManagement

--- Bảng Category ---
INSERT INTO Category (CategoryCode, Name, Status)
VALUES
('CAT001', N'Đồ điện tử', 1),
('CAT002', N'Điện thoại di động', 1),
('CAT003', N'Điện gia dụng', 1);

--- Bảng StorageArea ---
INSERT INTO StorageArea (AreaCode, AreaName, Status)
VALUES
('AREA001', N'Kho A', 1),
('AREA002', N'Kho B', 1),
('AREA003', N'Kho C', 1);

--- Bảng Account ---
INSERT INTO Account (AccountCode, Email, Name, Password, Role, Phone, Status)
VALUES
('ACC001', 'nguyenvananh@gmail.com', N'Nguyễn Văn Anh', 'Password1!', 1, '0987654321', 1),
('ACC002', 'lethithanh@gmail.com', N'Lê Thị Thanh', 'Password1!', 2, '0912834756', 1),
('ACC003', 'tranvanbinh@gmail.com', N'Trần Văn Bình', 'Password1!', 1, '0912345678', 1);

--- Bảng Partner ---
INSERT INTO Partner (PartnerCode, Name, Status)
VALUES
('PART001', N'Công ty TNHH Thương mại và Xuất nhập khẩu Đại Đồng', 1),
('PART002', N'Công ty Cổ phần Thương mại và Xuất nhập khẩu Bách Hợp', 1),
('PART003', N'Công ty TNHH Thương mại và Xuất nhập khẩu Điện tử - Gia dụng Minh Quang', 1);

--- Bảng Lot ---
INSERT INTO Lot (AccountId, PartnerId, LotCode, DateIn, Note, Status)
VALUES
(1, 1, 'LOT001', '2023-01-01', null, 1),
(2, 2, 'LOT002', '2023-02-01', null, 1),
(3, 3, 'LOT003', '2023-03-01', null, 1);

--- Bảng StockOut ---
INSERT INTO StockOut (AccountId, PartnerId, DateOut, Note, Status)
VALUES
(1, 1, '2023-01-15', null, 1),
(2, 2, '2023-02-15', null, 1),
(3, 3, '2023-03-15', null, 1);

--- Bảng StockOutDetail ---
INSERT INTO StockOutDetail (ProductId, StockOutId, Quantity)
VALUES
(1, 1, 10),
(2, 1, 5),
(3, 2, 8);

--- Bảng Product ---
INSERT INTO Product (CategoryId, AreaId, ProductCode, Name, Quantity, Status)
VALUES
(1, 1, 'PROD001', N'Tivi LED Samsung 50 inch', 20, 1),
(2, 2, 'PROD002', N'Điện thoại iPhone 12', 15, 1),
(3, 3, 'PROD003', N'Máy giặt Electrolux', 30, 1);

--- Bảng LotDetail ---
INSERT INTO LotDetail (LotId, ProductId, PartnerId, Quantity, Status)
VALUES
(1, 1, 1, 15, 1),
(2, 2, 2, 10, 1),
(3, 3, 3, 20, 1);