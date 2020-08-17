Use FUH_COMPANY
Go
Create proc sp_Insert_tblDepartment
@depNum int,
@depName nvarchar(50),
@mgrSSN decimal,
@mgrAssDate datetime
As
Insert into tblDepartment values (@depNum,@depName,@mgrSSN,@mgrAssDate)
Go



Create proc sp_Update_tblDepartment
@depNum int,
@depName nvarchar(50),
@mgrSSN decimal,
@mgrAssDate datetime
As
Update tblDepartment Set depName=@depName, mgrSSN=@mgrSSN, mgrAssDate=@mgrAssDate Where depNum=@depNum
Go




Use FUH_COMPANY
Go
Create proc sp_Delete_tblDepartment
@depNum int
As
Delete From tblDepLocation Where depNum=@depNum
Delete From tblProject Where depNum=@depNum
Delete From tblEmployee Where depNum=@depNum
Delete From tblDepartment Where depNum=@depNum
Go

sp_Delete_tblDepartment
5

Create proc sp_Search_tblDepartment_byDepName
@depName nvarchar(50)
As
select *
from tblDepartment
where depName like N'%'+@depName+'%'


