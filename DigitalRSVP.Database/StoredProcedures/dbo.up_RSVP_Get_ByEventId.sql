create procedure [dbo].[up_RSVP_Get_ByEventId]
(
	@EventId uniqueidentifier
)
as
begin
	select *
	from [dbo].[RSVP]
	where [EventId] = @EventId
end