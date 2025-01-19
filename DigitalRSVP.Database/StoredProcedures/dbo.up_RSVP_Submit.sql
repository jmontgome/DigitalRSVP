create procedure [dbo].[up_RSVP_Submit]
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
		RAISERROR('[Error:1] - RSVP already exists.', 16, 1)
	end
	else
	begin
		insert into [dbo].[RSVP]
		(
			[WorkItemId],
			[InviteeId],
			[DateTime],
			[GuestsData],
			[AttendingWedding],
			[AttendingReception],
			[Note]
		)
		values(
			@Id, @InviteeId, @DateTime, @GuestsData,
			@AttendingWedding, @AttendingReception,
			@Note
		)
	end
end