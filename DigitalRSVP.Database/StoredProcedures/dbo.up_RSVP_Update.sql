create procedure [dbo].[up_RSVP_Update]
(
	@Id uniqueidentifier,
	@InviteeId uniqueidentifier,
	@DateTime datetime,
	@GuestsData varchar(2000),
	@AttendingWedding bit,
	@AttendingReception bit,
	@Note varchar(1000)
)
as
begin
	if (exists(select [WorkItemId] from [dbo].[RSVP] where [WorkItemId] = @Id))
	begin
		update [dbo].[RSVP]
		set [InviteeId] = @InviteeId,
			[DateTime] = @DateTime,
			[GuestsData] = @GuestsData,
			[AttendingWedding] = @AttendingWedding,
			[AttendingReception] = @AttendingReception,
			[Note] = @Note
		where [WorkItemId] = @Id
	end
	else
	begin
		RAISERROR('[Error:2] - RSVP does not exist.', 16, 1)
	end
end