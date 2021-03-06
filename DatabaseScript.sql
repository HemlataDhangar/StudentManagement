USE [Interview22August2020_Hemlata]
GO
/****** Object:  UserDefinedFunction [dbo].[SplitString]    Script Date: 28-08-2020 12:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[SplitString]
(    
      @Input NVARCHAR(MAX),
      @Character CHAR(1)
)
RETURNS @Output TABLE (
      Item NVARCHAR(1000)
)
AS
BEGIN
      DECLARE @StartIndex INT, @EndIndex INT
 
      SET @StartIndex = 1
      IF SUBSTRING(@Input, LEN(@Input) - 1, LEN(@Input)) <> @Character
      BEGIN
            SET @Input = @Input + @Character
      END
 
      WHILE CHARINDEX(@Character, @Input) > 0
      BEGIN
            SET @EndIndex = CHARINDEX(@Character, @Input)
           
            INSERT INTO @Output(Item)
            SELECT SUBSTRING(@Input, @StartIndex, @EndIndex - 1)
           
            SET @Input = SUBSTRING(@Input, @EndIndex + 1, LEN(@Input))
      END
 
      RETURN
END
GO
/****** Object:  Table [dbo].[Course]    Script Date: 28-08-2020 12:32:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course](
	[CourseId] [int] IDENTITY(1,1) NOT NULL,
	[CourseName] [varchar](250) NULL,
	[IsActive] [bit] NULL,
	[CreatedOn] [datetime] NULL,
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 28-08-2020 12:32:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[StudentId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[DateOfBirth] [date] NULL,
	[Gender] [varchar](10) NULL,
	[IsActive] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentCourse]    Script Date: 28-08-2020 12:32:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentCourse](
	[StudentCourseId] [int] IDENTITY(1,1) NOT NULL,
	[CourseId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_StudentCourse] PRIMARY KEY CLUSTERED 
(
	[StudentCourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[StudentCourse]  WITH CHECK ADD  CONSTRAINT [FK_StudentCourse_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([StudentId])
GO
ALTER TABLE [dbo].[StudentCourse] CHECK CONSTRAINT [FK_StudentCourse_Student]
GO
ALTER TABLE [dbo].[StudentCourse]  WITH CHECK ADD  CONSTRAINT [FK_StudentCourse_StudentCourse] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([CourseId])
GO
ALTER TABLE [dbo].[StudentCourse] CHECK CONSTRAINT [FK_StudentCourse_StudentCourse]
GO
/****** Object:  StoredProcedure [dbo].[DeleteStudent]    Script Date: 28-08-2020 12:32:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Hemlata>
-- Create date: <22 August>
-- Description:	<Delete>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteStudent]
	@id int
AS
BEGIN
    Delete from [dbo].[StudentCourse] where [StudentId] = @id
	Delete from  [dbo].[Student] where [StudentId] = @id
	
END
GO
/****** Object:  StoredProcedure [dbo].[GetStudentById]    Script Date: 28-08-2020 12:32:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Hemlata>
-- Create date: <22 Aug 2020>
-- Description:	<GetStudentById>
-- =============================================
CREATE PROCEDURE [dbo].[GetStudentById] -- [dbo].[GetStudentById] 15
	@id int = 0
AS
BEGIN
Declare @lst varchar(max) = 0
select @lst = isnull(@lst+',','')+ CAST([CourseId] as varchar(10)) from [dbo].[StudentCourse] where [StudentId] = @id

SELECT 
[StudentId],
[Name],
[Gender] AS GenderType,
CAST([DateOfBirth] as varchar(20)) AS DateOfBirth,
 @lst AS CourseId

FROM [dbo].[Student] where StudentId = @id
END
GO
/****** Object:  StoredProcedure [dbo].[ManageStudent]    Script Date: 28-08-2020 12:32:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Hemlata>
-- Create date: <22 August 2020>
-- Description:	<Add Update Student>
-- =============================================
CREATE PROCEDURE [dbo].[ManageStudent] -- EXEC ManageStudent 1,'Hi'
	@StudentId int,
	@Name varchar(100),
	@DateofBirth datetime = null,
	@Gender varchar(10) = 0

AS
BEGIN
	MERGE [dbo].[Student] as T  USING (select @StudentId as StudentId ,@Name as [Name], @DateofBirth as DateOfBirth ,@Gender  as Gender ) As S
	
ON (S.StudentId = T.StudentId)
WHEN MATCHED Then
    Update  Set 
	T.Name = S.Name,
	T.DateOfBirth = S.DateOfBirth,
	T.Gender = S.Gender,
	T.ModifiedDate = getdate()
WHEN NOT MATCHED BY Target Then
    Insert ([Name],[DateOfBirth],[Gender], [IsActive] ,[CreatedDate],[ModifiedDate]  ) Values (S.[Name], S.DateOfBirth, S.Gender,1,getdate(),getdate());
END
GO
/****** Object:  StoredProcedure [dbo].[ManageStudentCourse]    Script Date: 28-08-2020 12:32:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Hemlata>
-- Create date: <22 Aug>
-- Description:	<Manage Course>
-- =============================================
CREATE PROCEDURE [dbo].[ManageStudentCourse]-- 18,''
		@StudentId int,
		@CourseIds varchar(MAX)
AS
BEGIN


    Delete from [dbo].[StudentCourse] where [StudentId] = @StudentId
	if(len(@CourseIds)> 0)
	Begin
		INSERT INTO [dbo].[StudentCourse] ([CourseId],[StudentId],[CreatedDate],[ModifiedDate] )
	    SELECT Item, @StudentId,getdate(),getdate() FROM dbo.SplitString(@CourseIds, ',')
	End

END
GO
/****** Object:  StoredProcedure [dbo].[ManageStudentWithCourse]    Script Date: 28-08-2020 12:32:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Hemlata>
-- Create date: <22 August 2020>
-- Description:	<Add Update Student>
-- =============================================
CREATE PROCEDURE [dbo].[ManageStudentWithCourse] -- EXEC ManageStudent 1,'Hi'
	@StudentId int,
	@Name varchar(100),
	@DateofBirth datetime = null ,
	@Gender varchar(10) = 0,
	@CourseIds varchar(MAX) = null

AS
BEGIN


-- Update
if(@StudentId > 0)
Begin 
    Update [dbo].[Student]  Set 
	Name = @Name,
	DateOfBirth = @DateofBirth,
	Gender =  @Gender,
	ModifiedDate = getdate()
	where [StudentId] = @StudentId
	Exec ManageStudentCourse  @StudentId,@CourseIds
End
else
-- Insert
Begin
Insert into [dbo].[Student] ([Name],[DateOfBirth],[Gender], [IsActive] ,[CreatedDate],[ModifiedDate]  ) Values (@Name, @DateofBirth, @Gender,1,getdate(),getdate())
Declare @tempId int
Set  @tempId = SCOPE_IDENTITY()
Exec ManageStudentCourse  @tempId,@CourseIds
End




--Declare @tempId int
----select  @tempId = @StudentId
--	MERGE [dbo].[Student] as T  USING (select @StudentId as StudentId ,@Name as [Name], @DateofBirth as DateOfBirth ,@Gender  as Gender ) As S
--ON (S.StudentId = T.StudentId)
--WHEN MATCHED Then
--    Update  Set 
--	T.Name = S.Name,
--	T.DateOfBirth = S.DateOfBirth,
--	T.Gender = S.Gender,
--	T.ModifiedDate = getdate()
		
--WHEN NOT MATCHED BY Target Then
--    Insert ([Name],[DateOfBirth],[Gender], [IsActive] ,[CreatedDate],[ModifiedDate]  ) Values (S.[Name], S.DateOfBirth, S.Gender,1,getdate(),getdate());
--	Set  @tempId = IDENT_CURRENT('Student')

--	if(len(@StudentId)> 0)
--	  Set  @tempId = @StudentId
	--Exec ManageStudentCourse  @tempId,@CourseIds
END





GO
