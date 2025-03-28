/****** Object:  StoredProcedure [dbo].[up_Event_Get_ById]    Script Date: 2/23/2025 11:04:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[up_Event_Get_ById]
(
	@Id uniqueidentifier
)
as
begin
	select
		[Id],
		[WorkItemId],
		[Name],
		[ContactEmail],
		[ExpiryDate]
	from [dbo].[Event]
	where [WorkItemId] = @Id
end