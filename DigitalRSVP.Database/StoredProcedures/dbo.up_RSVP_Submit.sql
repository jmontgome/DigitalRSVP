/****** Object:  StoredProcedure [dbo].[up_RSVP_Submit]    Script Date: 2/23/2025 1:43:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[up_RSVP_Submit]
(
	@Id uniqueidentifier,
	@EventId uniqueidentifier,
	@InviteeId uniqueidentifier,
	@DateTime datetime,
	@Note varchar(1000)
)
as
begin
	begin try
		begin transaction
		insert into [dbo].[RSVP]
		(
			[WorkItemId],
			[EventId],
			[InviteeId],
			[DateTime],
			[Note]
		)
		values(
			@Id, @EventId, @InviteeId, @DateTime, @Note
		)

		commit
	end try
	begin catch
		rollback
		declare @excMessage varchar(360) = ERROR_MESSAGE()

		if (exists(select [WorkItemId] from [dbo].[RSVP] where [WorkItemId] = @Id))
		begin
			RAISERROR(N'[ERROR:1] - Error submitting RSVP. %s', 16, 1, @excMessage)
		end
		else
		begin
			RAISERROR(N'[ERROR:99] - Error submitting RSVP. %s', 16, 1, @excMessage)
		end
	end catch
end