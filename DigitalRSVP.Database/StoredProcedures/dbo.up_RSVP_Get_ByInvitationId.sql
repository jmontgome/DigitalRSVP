create procedure [dbo].[up_RSVP_Get_ByInvitationId]
(
	@InvId uniqueidentifier
)
as
begin
	select *
	from [dbo].[RSVP]
	where  [InviteeId] = @InvId
end