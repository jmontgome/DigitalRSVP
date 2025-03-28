/****** Object:  StoredProcedure [dbo].[up_Guest_Upsert]    Script Date: 2/23/2025 1:59:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[up_Guest_Upsert]
(
	@Id uniqueidentifier,
	@Name nvarchar(360),
	@Age int,
	@AttendingWedding bit,
	@AttendingReception bit,
	@Rsvpid uniqueidentifier
)
as
begin
	begin transaction
	begin try
		if (exists(select [WorkItemId] from [dbo].[Guest] where [WorkItemId] = @Id))
		begin
			update [dbo].[Guest]
			set [RsvpId] = @Rsvpid,
				[Name] = @Name,
				[Age] = @Age,
				[AttendingWedding] = @AttendingWedding,
				[AttendingReception] = @AttendingReception
			where [WorkItemId] = @Id
		end
		else
		begin
			insert into [dbo].[Guest] ( [WorkItemId], [RsvpId], [Name], [Age], [AttendingWedding], [AttendingReception] )
			values ( @Id, @Rsvpid, @Name, @Age, @AttendingWedding, @AttendingReception )
		end
		commit
	end try
	begin catch
		rollback
		declare @excMessage varchar(360) = ERROR_MESSAGE()
		
		RAISERROR(N'[Error:99] - Error upserting Guest. %s', 16, 1, @excMessage)
	end catch
end