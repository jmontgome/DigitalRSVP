create procedure [dbo].[up_RSVP_Get_ById]
(
	@Id uniqueidentifier
)
as
begin
	select *
	from [dbo].[RSVP]
	where  [WorkItemId] = @Id
end