-----------------------------------------------------------------------------
-- Tạo bảng
IF OBJECT_ID('dbo.Categories', 'U') IS NOT NULL 
BEGIN
	DELETE FROM dbo.Categories
	DROP TABLE dbo.Categories
END

CREATE TABLE [dbo].[Categories](  
			[CategoryID] [int] PRIMARY KEY,
			[Level] [int] not null,
    		[CategoryName] [nvarchar](60) not null,  
			[ParentCategoryID] [int] not null,
			[EditDate] [datetime],
			[Editor] [nvarchar](50),
			[Description] [nvarchar](200),  
    		[Remarks] [nvarchar](max),  
		)  
GO		
--------------------------------------------------------------------------------------------------
-- Insert Data
INSERT INTO [dbo].[Categories] VALUES (1,1,N'Siganature',0,'',N'',N'Phân hệ Signature',N'');
INSERT INTO [dbo].[Categories] VALUES (2,1,N'TS4',0,'',N'',N'Phân hệ TS4',N'');
INSERT INTO [dbo].[Categories] VALUES (3,2,N'Overview ',1,'',N'',N'',N'');
INSERT INTO [dbo].[Categories] VALUES (4,2,N'“Work with…” function',1,'',N'',N'',N'');
INSERT INTO [dbo].[Categories] VALUES (5,2,N'Creat new…',1,'',N'',N'',N'');
INSERT INTO [dbo].[Categories] VALUES (6,2,N'Maintain',1,'',N'',N'',N'');
INSERT INTO [dbo].[Categories] VALUES (7,2,N'Report ',1,'',N'',N'',N'');
INSERT INTO [dbo].[Categories] VALUES (8,2,N'T4S Client',2,'',N'',N'TS4 Client',N'');
INSERT INTO [dbo].[Categories] VALUES (9,2,N'TS4 Plugin',2,'',N'',N'TS4 Plugin',N'');
INSERT INTO [dbo].[Categories] VALUES (10,3,N'Tổng quan',9,'',N'',N'Mục đích, Phạm vi ….',N'');
INSERT INTO [dbo].[Categories] VALUES (11,3,N'Giới thiệu chung',9,'',N'',N'Tổng quan chương trình, và các nội dung khác …',N'');
INSERT INTO [dbo].[Categories] VALUES (12,3,N'Giới thiệu các chức năng',9,'',N'',N'Giới thiệu chức năng hệ thống',N'');
INSERT INTO [dbo].[Categories] VALUES (13,4,N'Chức năng trong phân hệ TransMaster',12,'',N'',N'',N'');
INSERT INTO [dbo].[Categories] VALUES (14,4,N'Chức năng trong phân hệ JournalScan',12,'',N'',N'',N'');
INSERT INTO [dbo].[Categories] VALUES (15,4,N'Phím chức năng',12,'',N'',N'',N'');
INSERT INTO [dbo].[Categories] VALUES (16,4,N'Hướng dẫn sử dụng các chức năng hệ thống ',12,'',N'',N'',N'');
INSERT INTO [dbo].[Categories] VALUES (17,5,N'Đăng nhập ',16,'',N'',N'',N'');
INSERT INTO [dbo].[Categories] VALUES (18,5,N'Phím chức năng',16,'',N'',N'',N'');
INSERT INTO [dbo].[Categories] VALUES (19,5,N'Chuyển tiền trong nước đến',16,'',N'',N'',N'');
INSERT INTO [dbo].[Categories] VALUES (20,5,N'Chuyển tiền trong nước ngoài hệ thống',16,'',N'',N'',N'');
INSERT INTO [dbo].[Categories] VALUES (21,5,N'Money Gram',16,'',N'',N'',N'');
INSERT INTO [dbo].[Categories] VALUES (22,5,N'Host to host',16,'',N'',N'',N'');
INSERT INTO [dbo].[Categories] VALUES (23,5,N'Chuyển tiền đi nước ngoài',16,'',N'',N'',N'');
INSERT INTO [dbo].[Categories] VALUES (24,5,N'Phát hành hối phiếu Bank Draft',16,'',N'',N'',N'');
INSERT INTO [dbo].[Categories] VALUES (25,5,N'Nhờ thu',16,'',N'',N'',N'');
INSERT INTO [dbo].[Categories] VALUES (26,5,N'Kho bạc nhà nước',16,'',N'',N'',N'');
INSERT INTO [dbo].[Categories] VALUES (27,5,N'Thanh toán hóa đơn',16,'',N'',N'',N'');
INSERT INTO [dbo].[Categories] VALUES (28,5,N'Kết hối ngoại tệ',16,'',N'',N'',N'');
INSERT INTO [dbo].[Categories] VALUES (29,5,N'TransMaster',16,'',N'',N'',N'');
INSERT INTO [dbo].[Categories] VALUES (30,5,N'JounalScan',16,'',N'',N'',N'');
INSERT INTO [dbo].[Categories] VALUES (31,5,N'TransError',16,'',N'',N'',N'');
INSERT INTO [dbo].[Categories] VALUES (32,5,N'SystemAdmin',16,'',N'',N'',N'');

-- SELECT TOÀN BỘ BẢNG
-------------------------------------------------------------------
-- TÌM KIẾM TÊN NHỮNG MENU MÀ CÓ MENU CON
-- TỨC LÀ CÓ ID XUẤT HIỆN TRONG PARENTCATEGORYID
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'SP_ListAllChildCategory' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.SP_ListAllChildCategory

create procedure dbo.SP_ListAllChildCategory
as
begin
select CategoryName from Categories
where CategoryID in (select distinct ParentCategoryId from Categories)
end
GO
------------------------------------------------------------------------
-- STORE PROCEDURE LIST DANH SÁCH MÀ THAM SỐ TRUYỀN VÀO LÀ TÊN MENU CHA
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'SP_LISTCHILDNODE1' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.SP_LISTCHILDNODE1

CREATE PROCEDURE dbo.SP_LISTCHILDNODE1
@CATEGORYNAME NVARCHAR(100)
AS
BEGIN
	SELECT CATEGORYNAME 
	FROM Categories
	WHERE ParentCategoryId in (select CategoryID from Categories where CategoryName=@CATEGORYNAME)
END
GO
-----------------------------------------------------------
--- Create procedure InsertUpdateCategory
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'InsertUpdateCategory' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.InsertUpdateCategory

create procedure dbo.InsertUpdateCategory
(
@CategoryID integer,
@Level integer,
@CategoryName nvarchar(50),
@ParentCategoryID integer,
@EditDate datetime,
@Editor nvarchar(60),
@Description nvarchar(200),
@Remarks nvarchar(max),
@Action varchar(10)
)
as
begin
if @Action='Insert'
	begin
	insert into Categories(CategoryID,Level,CategoryName,ParentCategoryID,EditDate,Editor,Description,Remarks) values (@CategoryID,@Level,@CategoryName,@ParentCategoryID,@EditDate,@Editor,@Description,@Remarks);
	end
if @Action='Update'
	begin
	update Categories set Level=@Level,CategoryName=@CategoryName,ParentCategoryId=@ParentCategoryID,EditDate=@EditDate,Editor=@Editor, 
						Description=@Description,Remarks=@Remarks
						where CategoryID=@CategoryID;
	end
end
GO
-------------------------------------------------------------------------------
---- procedure delete các bản ghi DeleteCategory2: CÂU LỆNH ĐÃ CHẠY OKE : Tạm thời không dùng do chuyển thuật toán
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'DeleteCategory2' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.DeleteCategory2

create procedure dbo.DeleteCategory2
(
@CategoryID integer
)
as
begin
	declare @in int	
	set @in=@CategoryID
	if exists (select * from Categories where ParentCategoryId=@in)
	begin
			declare mycursor Cursor for
			select CategoryID from Categories where ParentCategoryId=@in
			open mycursor
			fetch next  from mycursor into @in
			while @@fetch_status = 0
			begin
			exec DeleteCategory @in
			fetch next from mycursor into @in
			end
			close mycursor
			deallocate mycursor
	end	
	delete from Categories where CategoryID=@CategoryID
end
go
--------------------------------------------------------------------------------
--  procedure delete bản ghi: nếu không có con thì xóa, có con thì không xóa
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'DeleteCategory' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.DeleteCategory

create procedure dbo.DeleteCategory
(
@CategoryID integer
)
as
begin
if exists (select * from Categories where ParentCategoryId=@CategoryID)
begin
return
end
else
begin
delete from Categories where CategoryID=@CategoryID
end
end
GO
----------------------------------------------------------------------------
-- procedure lấy tất cả các bản ghi
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ShowAllCategory' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.ShowAllCategory

create procedure dbo.ShowAllCategory
as
begin
select * from Categories
end
GO
------------------------------------------------------------------------------
-- procedure lấy số tiếp theo của CategoryID
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetNextCategoryID' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.GetNextCategoryID

create procedure dbo.GetNextCategoryID
as
begin
	 select max(CategoryID)+1 from Categories
	
end
go
----------------------------------------------------------------------------------
--- procedure lấy danh sách category
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetListCategoryForSelectParent' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.GetListCategoryForSelectParent

create procedure dbo.GetListCategoryForSelectParent
as
begin
	select CategoryID, CategoryName from Categories
end
GO	
------------------------------------------------------------------------------------
--- procedure lấy level
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetListLevel' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.GetListLevel

create procedure dbo.GetListLevel
as
begin
	select distinct Level from Categories
end
GO
-- procedure lấy những Category id và categoryname mà có level trước level truyền vào. Ví dụ truyền level 3 thì lấy những category có level=2.
-- Việc này phục vụ cho thêm mới category khi chọn level cho category thì ô ParentCategoryID list lên danh sách những level cha
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetCategoryByLevelParent' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.GetCategoryByLevelParent

create procedure dbo.GetCategoryByLevelParent
@Level integer
as
begin
select CategoryID, CategoryName from Categories where Level = (@Level -1)
end
GO
------------------------------------------------
--- procedure lấy data theo level
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetCategoryByLevel' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.GetCategoryByLevel

create procedure dbo.GetCategoryByLevel
@Level integer
as
begin
if @Level=0
begin
	select * from Categories
end
else
begin
select * from Categories where Level = @Level
end
end
GO
--------------------------------------------------------------------
-- procedure lấy nextlevl
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetNextLevel' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.GetNextLevel

create procedure dbo.GetNextLevel
as
begin
	select max(Level)+1 from Categories
end
GO
--------------------------------------------------------------
-- procedure lấy nhưng Category có level trước level truyền và
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'Getparentcategorybylevelofchild' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.Getparentcategorybylevelofchild

create procedure dbo.Getparentcategorybylevelofchild
@Level int
as
begin
	select *
	from Categories
	where Level = @Level -1
end
GO
------------------------------------------------------------------
--- procedure lấy category theo id
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetCategoryByID' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.GetCategoryByID

create procedure dbo.GetCategoryByID
@CategoryID int
as
begin
select * from Categories where CategoryID=@CategoryID
end
GO
------------------------------------------------------------------
-- procedure lấy category theo Name
IF EXISTS (SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetContentFromCategoryName' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.GetContentFromCategoryName

create procedure dbo.GetContentFromCategoryName
@CategoryName nvarchar(80)
as
begin
	select * from Categories where CategoryName=N'@CATEGORYNAME'
end
GO
	
