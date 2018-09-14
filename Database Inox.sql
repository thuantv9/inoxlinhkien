-- tạo database
---------------------------------------
create database InoxLinhKien
go
-- tạo bảng
-- tạo bảng sản phẩm
create table Product
(
Id int not null primary key,
Name nvarchar(200),
MadeFrom nvarchar(100),
CategoryId int,
Dimenson ntext,
Image ntext,
Remark ntext,
Status bit
)
go
-- tạo bảng danh mục sản phẩm
create table Category
(
CategoryId int not null primary key,
CategoryName nvarchar(100)
)
go
-- tạo bảng khách hàng (dùng cho phần hiển thị các dự án đã thực hiện)
create table Customer
(
CustomerId	int not null primary key,
CustomerName nvarchar(300),
CustomerImage nvarchar(200),
CustomerDescription ntext,
CustomerRemark ntext
)
go
-- tạo bảng quản lý người dùng
create table Users
(
UserId int not null primary key,
UserName nvarchar(50),
Password ntext
)
go
-- tạo bảng quản lý hình ảnh chạy silde
create table SlideImage
(
SlideId int not null primary key,
SlideImageName nvarchar(50)
)
go
-- tạo bảng đơn hàng
create table Orders
(
OrderId int not null primary key,
CustomerName nvarchar(300),
Creator nvarchar(300),
CreateDate datetime	
)
go
-- tạo bảng chi tiết đơn hàng
create table OrderItem
(
OrderItemId int not null primary key,
OrderId int,
ProductId	int,
Quantity	int	,
)

-- tạo bảng tin tức
create table News
(
NewsId int not null primary key,
NewsImage nvarchar(300),
NewsDescription ntext,
NewsRemark ntext
)
-- hết phần tạo bảng
--------------------------------------------------------------------------------------------------------
-- Tạo dữ liệu giả lập
-- Thêm dữ liệu cho Slide
insert into SlideImage values (1,'/img/slides/nivo/bg-1.jpg');
insert into SlideImage values (2,'/img/slides/nivo/bg-2.jpg'); 
insert into SlideImage values (3,'/img/slides/nivo/bg-3.jpg');
go

-- Thêm dữ liệu cho bảng Category
insert into Category values (1, N'Chậu rửa');
insert into Category values (2, N'Bếp công nghiệp');
insert into Category values (3, N'Tủ');
insert into Category values (4, N'Trạn');
insert into Category values (5, N'Xe đẩy');
go

-- Thêm dữ liệu bảng khách hàng
insert into Customer values(1,N'Bệnh viện đa khoa Lâm Hoa',N'/images/product_1.png',N'Địa chỉ: Phố Lý Bôn, thành phố Thái Bình',N'Lorem....');
insert into Customer values(2,N'Bệnh viện đa khoa Lâm Hoa',N'/images/product_2.png',N'Địa chỉ: Phố Lý Bôn, thành phố Thái Bình',N'Lorem....');
insert into Customer values(3,N'Bệnh viện đa khoa Lâm Hoa',N'/images/product_3.png',N'Địa chỉ: Phố Lý Bôn, thành phố Thái Bình',N'Lorem....');
insert into Customer values(4,N'Bệnh viện đa khoa Lâm Hoa',N'/images/product_4.png',N'Địa chỉ: Phố Lý Bôn, thành phố Thái Bình',N'Lorem....');
insert into Customer values(5,N'Bệnh viện đa khoa Lâm Hoa',N'/images/product_5.png',N'Địa chỉ: Phố Lý Bôn, thành phố Thái Bình',N'Lorem....');
insert into Customer values(6,N'Bệnh viện đa khoa Lâm Hoa',N'/images/product_6.png',N'Địa chỉ: Phố Lý Bôn, thành phố Thái Bình',N'Lorem....');
insert into Customer values(7,N'Bệnh viện đa khoa Lâm Hoa',N'/images/product_7.png',N'Địa chỉ: Phố Lý Bôn, thành phố Thái Bình',N'Lorem....');
go
-- Thêm dữ liệu bảng sản phẩm
insert into Product values (1,N'chậu rửa bát công nghiệp hai hố inox 304',N'inox 304',1,
N'KT:1850x750x800 ; KT chậu: 550x500x280; Vòi nước công nghiệp=2;Chân tăng chỉnh cao thấp =4',
N'/images/product_1.png',
N'Lorem .....',
1);
insert into Product values (2,N'Bếp gas công nghiệp 2 họng đốt( GADO)',
N'inox 304',2,
N'KT: 1250X750X800;Vòi nước công nghiệp;Chân tăng chỉnh cao thấp; Hệ thống làm mát bề mặt',
N'/images/product_2.png',
N'Lorem .....2',
1);
insert into Product values (3,N'tủ để đồ có ngăn kéo cánh mở',
N'inox 304',3,
N'KT : 1500x600x800; KT ngăn kéo :450x200',
N'/images/product_3.png',
N'Lorem .....3',
1);
insert into Product values (4,N'tủ để đồ có ngăn kéo cánh mở',
N'inox 304',3,
N'KT : 1500x600x800; KT ngăn kéo :450x200',
N'/images/product_4.png',
N'Lorem .....3',
1);
insert into Product values (5,N'tủ để đồ có ngăn kéo cánh mở',
N'inox 304',2,
N'KT : 1500x600x800; KT ngăn kéo :450x200',
N'/images/product_5.png',
N'Lorem .....3',
1);
insert into Product values (6,N'tủ để đồ có ngăn kéo cánh mở',
N'inox 304',4,
N'KT : 1500x600x800; KT ngăn kéo :450x200',
N'/images/product_6.png',
N'Lorem .....3',
1);
insert into Product values (7,N'tủ để đồ có ngăn kéo cánh mở',
N'inox 304',2,
N'KT : 1500x600x800; KT ngăn kéo :450x200',
N'/images/product_7.png',
N'Lorem .....3',
1);

go
--------------------------------------------------------------------------------------------------------
-- tạo procedure
-- đối với Product
-- lấy tất cả sản phẩm
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetAllProduct' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.GetAllProduct
go
create procedure dbo.GetAllProduct
as
begin
	select * from Product
end
go

-- lấy sản phẩm theo chủng loại sản phẩm (CategoryId)
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GeProductByCategoryId' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.GeProductByCategoryId
go
create procedure dbo.GeProductByCategoryId
@CategoryId int
as
begin
	select * from Product where CategoryId=@CategoryId
end  
go

-- lấy sản phẩm theo Id sản phẩm
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetProductById' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.GetProductById
go
create procedure dbo.GetProductById
@Id int
as
begin
	select * from Product where Id=@Id
end
go

-- lấy sản phẩm theo tên
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetProductByName' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.GetProductByName
go
create procedure GetProductByName
@Name nvarchar(200)
as
begin
	select * from Product where Name=@Name
end 
go

-- Thêm sản phẩm mới
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'InsertProduct' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.InsertProduct
go
create procedure dbo.InsertProduct
@Id int,
@Name nvarchar(200),
@MadeFrom nvarchar(100),
@CategoryId int,
@Dimenson ntext,
@Image ntext,
@Remark ntext,
@Status bit
as
begin
	insert into Product(Id,Name,MadeFrom,CategoryId,Dimenson,Image,Remark,Status) values (@Id,@Name,@MadeFrom,@CategoryId,@Dimenson,@Image,@Remark,@Status); 
end
go

-- Update sản phẩm mới theo id
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'UpdateProduct' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.UpdateProduct
go
create procedure dbo.UpdateProduct
@Id int,
@Name nvarchar(200),
@MadeFrom nvarchar(100),
@CategoryId int,
@Dimenson ntext,
@Image ntext,
@Remark ntext,
@Status bit
as
begin
	update Product
	set	Name=@Name, MadeFrom=@MadeFrom, CategoryId=@CategoryId, Dimenson=@Dimenson,Image=@Image,Remark=@Remark, Status=@Status
	where Id=@Id
end
go
-- Delete sản phẩm theo id
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'DeleteProduct' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.DeleteProduct
go
create procedure dbo.DeleteProduct
@Id int
as
begin
	delete from Product where Id=@Id
end
go 
-- procedure lấy số tiếp theo của ProdcutId
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetNextProductId' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.GetNextProductId

create procedure dbo.GetNextProductId
as
begin
	 select max(Id)+1 from Product
	
end
go

-- Bảng chủng loại sản phẩm
-- lấy tất cả chủng loại
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetAllCategory' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.GetAllCategory
go
create procedure dbo.GetAllCategory
as
begin 
	select * from Category
end
go
-- lấy chủng loại theo Id
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetCategoryById' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.GetCategoryById
go
create procedure dbo.GetCategoryById
@CategoryId int
as
begin
	select * from Category where CategoryId = @CategoryId
end
go

-- thêm chủng loại
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'InsertCategory' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.InsertCategory
go
create procedure dbo.InsertCategory
@CategoryId int,
@CategoryName nvarchar(100)
as
begin
	insert into Category values (@CategoryId,@CategoryName);
end
-- Update chủng loại
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'UpdateCategory' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.UpdateCategory
go
create procedure dbo.UpdateCategory
@CategoryId int,
@CategoryName nvarchar(100)
as
begin
	update  Category 
	set CategoryName=@CategoryName
	where CategoryId=@CategoryId	
end
-- Delete chủng loại theo id, nếu còn sản phẩm của chủng loại thì không cho xóa
-- Tham khảo từ Online Help
-- procedure lấy số tiếp theo của CategoryIdId
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetNextCategoryId' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.GetNextProductId

create procedure dbo.GetNextCategoryId
as
begin
	 select max(CategoryId)+1 from Category
	
end
go
 
-- Bảng khách hàng
--  lấy tất cả khách hàng
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetAllCustomer' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.GetAllCustomer
go
create procedure dbo.GetAllCustomer
as
begin
	select * from Customer
end
go
-- lấy khách hàng theo Customer id
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetCustomerById' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.GetCustomerById
go
create procedure dbo.GetCustomerById
CustomerId int
as
begin
	select * from Customer where CustomerId=@CustomerId
end
go
-- thêm mới khách hàng
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'InsertCustomer' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.InsertCustomer
go
create procedure dbo.InsertCustomer
@CustomerId int,
@CustomerName nvarchar(300),
@CustomerImage nvarchar(200),
@CustomerDescription ntext,
@CustomerRemark ntext
as
begin
	insert into Customer(CustomerId,CustomerName,CustomerImage,CustomerDescription,CustomerRemark) values (@CustomerId,@CustomerName,@CustomerImage,@CustomerDescription,@CustomerRemark);
end
go

-- update khách hàng
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'UpdateCustomer' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.UpdateCustomer
go
create procedure dbo.UpdateCustomer
@CustomerId int,
@CustomerName nvarchar(300),
@CustomerImage nvarchar(200),
@CustomerDescription ntext,
@CustomerRemark ntext
as
begin
	update Customer
	set CustomerName=@CustomerName,CustomerImage= @CustomerImage, CustomerDescription=@CustomerDescription,  CustomerRemark=@CustomerRemark
	where CustomerId=@CustomerId
end
go
-- delete khách hàng theo id
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'DeleteCustomerById' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.DeleteCustomerById
go
create procedure dbo.DeleteCustomerById
@CustomerId int
as
begin
	delete from Customer where CustomerId=@CustomerId
end
go
-- procedure lấy số tiếp theo của CustomerId
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetNextCategoryId' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.GetNextCustomerId
create procedure dbo.GetNextCustomerId
as
begin
	 select max(CustomerId)+1 from Customer
	
end
go
-- Bảng người dùng
-- lấy tất cả người dùng
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetAllUser' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.GetAllUser
go
create procedure dbo.GetAllUser
as
begin
	select * from Users
end
go
-- lấy người dùng theo UserName
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetUserByUserName' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.GetUserByUserName
go
create procedure dbo.GetUserByUserName
@UserName nvarchar(50)
as
begin
	select * from Users where UserName=@UserName
end
go
-- thêm mới người dùng
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'InsertUser' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.InsertUser
go
create procedure dbo.InsertUser
@UserId int,
@UserName nvarchar(50),
@Password ntext
as
begin
	insert into Users(UserId,UserName,Password) values (@UserId,@UserName,@Password); 
end
go
-- Update người dùng
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'UpdateUser' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.UpdateUser
go
create procedure dbo.UpdateUser
@UserId int,
@UserName nvarchar(50),
@Password ntext
as
begin
	update Users
	set UserName=@UserName, Password=@Password
	where UserId=@UserId
end
go
-- Xóa người dùng theo ID
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'DeleteUser' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.DeleteUser
go
create procedure dbo.DeleteUser
@UserId int
as
begin
	delete from Users where UserId=@UserId
end
go


-- Bảng Slide Image
-- lấy tất cả slide
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetAllSlideImage' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.GetAllSlideImage
go
create procedure dbo.GetAllSlideImage
as
begin
	select * from SlideImage
end
go
-- thêm mới ảnh vào slide
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'InsertSlideImage' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.InsertSlideImage
go
create procedure dbo.InsertSlideImage
@SlideId int,
@SlideImageName nvarchar(50)
as
begin
	insert into SlideImage(SlideId,SlideImageName) values (@SlideId,@SlideImageName); 
end
go
-- xóa ảnh theo id
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'DeleteSlideImage' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.DeleteSlideImage
go
create procedure dbo.DeleteSlideImage
@SlideId int
as
begin
	delete from SlideImage where SlideId=@SlideId
end
go

-- Bảng đơn hàng
-- lấy tất cả đơn hàng
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetAllOrder' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.GetAllOrder
go
create procedure dbo.GetAllOrder
as
begin
	select * from Orders 
end
go
-- thêm mới đơn hàng
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'InsertOrder' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.InsertOrder
go
create procedure dbo.InsertOrder
(
@OrderId int,
@CustomerName nvarchar(300),
@Creator nvarchar(300),
@CreateDate datetime
)
as
begin
	insert into Orders values (@OrderId, @CustomerName, @Creator, @CreateDate);
end
go
--  Update đơn hàng
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'UpdateOrder' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.UpdateOrder
go
create procedure dbo.UpdateOrder
@OrderId int,
@CustomerName nvarchar(300),
@Creator nvarchar(300),
@CreateDate datetime
as
begin
	update Orders 
	set CustomerName=@CustomerName, Creator=@Creator, CreateDate=@CreateDate
	where OrderId=@OrderId
end
go

-- bảng chi tiết đơn hàng
-- lấy tất cả chi tiết đơn hàng 
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetAllOrderItem' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.GetAllOrderItem
go
create procedure dbo.GetAllOrderItem
as
begin
	select * from OrderItem
end
go
-- lấy chi tiết đơn hàng theo mã đơn hàng
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetOrderItemByOrderId' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.GetOrderItemByOrderId
go
create procedure dbo.GetOrderItemByOrderId
@OrderId int
as
begin
	select * from OrderItem where OrderId = @OrderId
end
go
-- Thêm mới chi tiết đơn hàng
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'InsertOrderItem' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.InsertOrderItem
go
create procedure dbo.InsertOrderItem
@OrderItemId int,
@OrderId int,
@ProductId int,
@Quantity int
as
begin
	insert into OrderItem values (@OrderItemId,@OrderId,@ProductId,@Quantity);
end
go
-- Update chi tiết đơn hàng
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'UpdateOrderItem' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.UpdateOrderItem
go
create procedure dbo.UpdateOrderItem
@OrderItemId int,
@OrderId int,
@ProductId int,
@Quantity int
as
begin
	update  OrderItem 
	set OrderId=@OrderId,ProductId=@ProductId,Quantity=@Quantity
	where OrderItemId=@OrderItemId
end
go

-- Thử nghiệm split string trong SQL
CREATE FUNCTION dbo.splitstring ( @stringToSplit VARCHAR(MAX) )
RETURNS
 @returnList TABLE ([Name] [nvarchar] (500))
AS
BEGIN

 DECLARE @name NVARCHAR(255)
 DECLARE @pos INT

 WHILE CHARINDEX(',', @stringToSplit) > 0
 BEGIN
  SELECT @pos  = CHARINDEX(',', @stringToSplit)  
  SELECT @name = SUBSTRING(@stringToSplit, 1, @pos-1)

  INSERT INTO @returnList 
  SELECT @name

  SELECT @stringToSplit = SUBSTRING(@stringToSplit, @pos+1, LEN(@stringToSplit)-@pos)
 END

 INSERT INTO @returnList
 SELECT @stringToSplit

 RETURN
END


