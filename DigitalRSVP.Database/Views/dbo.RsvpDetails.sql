/****** Object:  View [dbo].[RsvpDetails]    Script Date: 3/18/2025 10:57:15 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


alter view [dbo].[RsvpDetails]
as
	select
		inv.[WorkItemId] as InvitationId,
		rsvp.[WorkItemId] as RsvpId,
		inv.[EventId],
		inv.[Name] as InvitationName,
		inv.[NoteToInvitee],
		inv.[WeddingParty],
		inv.[DesignatedSeating],
		rsvp.[Note],
		guest.[Name],
		guest.[Age],
		guest.[AttendingWedding],
		guest.[AttendingReception],
		rsvp.[DateTime] as RSVPDateTime
	from [dbo].[Invitation] inv
	left join [dbo].[RSVP] rsvp
		on inv.WorkItemId = rsvp.InviteeId
	left join [dbo].[Guest] guest
		on guest.[RsvpId] = rsvp.WorkItemId
GO


