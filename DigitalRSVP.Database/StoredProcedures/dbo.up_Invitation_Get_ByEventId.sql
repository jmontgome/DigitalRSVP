create procedure [dbo].[up_Invitation_Get_ByEventId]
(
	@EventId uniqueidentifier
)
as
begin
	select *
	from [dbo].[Invitation]
	where [EventId] = @EventId
end