/****** Object:  StoredProcedure [dbo].[up_Invitation_Get_ById]    Script Date: 3/3/2025 9:35:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[up_Invitation_Get_ById]
(
	@Id uniqueidentifier
)
as
begin
	select top(1) *
	from [dbo].[Invitation]
	where [WorkItemId] = @Id
end