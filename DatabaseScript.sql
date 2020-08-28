USE [Interview22August2020_Hemlata]
GO
/****** Object:  StoredProcedure [dbo].[GetStudentById]    Script Date: 27-08-2020 16:58:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Hemlata>
-- Create date: <22 Aug 2020>
-- Description:	<GetStudentById>
-- =============================================
ALTER PROCEDURE [dbo].[GetStudentById] -- [dbo].[GetStudentById] 15
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
