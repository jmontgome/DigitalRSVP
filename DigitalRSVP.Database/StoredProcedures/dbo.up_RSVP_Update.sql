/****** Object:  StoredProcedure [dbo].[up_RSVP_Update]    Script Date: 2/23/2025 1:44:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[up_RSVP_Update]
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
		update [dbo].[RSVP]
		set [InviteeId] = @InviteeId,
			[EventId] = @EventId,
			[DateTime] = @DateTime,
			[Note] = @Note
		where [WorkItemId] = @Id

		commit
	end try
	begin catch
		rollback
		declare @excMessage varchar(360) = ERROR_MESSAGE()

		if (NOT EXISTS(select [WorkItemId] from [dbo].[RSVP] where [WorkItemId] = @Id))
		begin
			RAISERROR('[Error:2] - RSVP does not exist. %s', 16, 1, @excMessage)
		end
		else
		begin
			RAISERROR('[Error:99] - RSVP does not exist. %s', 16, 1, @excMessage)
		end
	end catch
end