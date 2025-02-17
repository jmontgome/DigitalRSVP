create procedure [dbo].[up_Event_Get_ById]
(
	@Id uniqueidentifier
)
as
begin
	select * 
	from [dbo].[Event]
	where [WorkItemId] = @Id
end