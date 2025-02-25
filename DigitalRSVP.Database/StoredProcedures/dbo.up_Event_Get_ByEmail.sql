create procedure [dbo].[up_Event_Get_ByEmail]
(
	@email varchar(360)
)
as
begin
	select *
	from [dbo].[Event]
	where [ContactEmail] = @email
end