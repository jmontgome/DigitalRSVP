/****** Object:  StoredProcedure [dbo].[up_Guest_Get_ByRsvpId]    Script Date: 2/23/2025 1:55:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[up_Guest_Get_ByRsvpId]
(
	@RsvpId uniqueidentifier
)
as
begin
	select *
	from [dbo].[Guest]
	where [RsvpId] = @RsvpId
end