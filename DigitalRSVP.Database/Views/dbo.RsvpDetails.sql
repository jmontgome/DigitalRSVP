go
create view [dbo].[RsvpDetails]
as
	select
		inv.[WorkItemId] as InvitationId,
		rsvp.[WorkItemId] as RsvpId,
		inv.[EventId],
		inv.[Name] as InvitationName,
		rsvp.[Note],
		guest.[Name],
		guest.[Age],
		guest.[AttendingWedding],
		guest.[AttendingReception],
		rsvp.[DateTime] as RSVPDateTime
	from [dbo].[Invitation] inv
	inner join [dbo].[RSVP] rsvp
		on inv.WorkItemId = rsvp.InviteeId
	inner join [dbo].[Guest] guest
		on guest.[RsvpId] = rsvp.WorkItemId