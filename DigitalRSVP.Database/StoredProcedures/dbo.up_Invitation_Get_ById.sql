create procedure [dbo].[up_Invitation_Get_ById]
(
	@Id uniqueidentifier
)
as
begin
	select top(1) *
	from [dbo].[Invitation]
	where [WorkItemId] = @Id

	declare @RowsFound int = @@ROWCOUNT;
	if (@RowsFound = 0)
	begin
		RAISERROR('[Error:2] - No invitation found.', 16, 1)
	end
end