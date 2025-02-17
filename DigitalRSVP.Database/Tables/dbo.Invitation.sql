/****** Object:  Table [dbo].[Invitation]    Script Date: 2/13/2025 9:11:53 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Invitation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WorkItemId] [uniqueidentifier] NOT NULL,
	[EventId] [uniqueidentifier] NOT NULL,
	[Name] [varchar](240) NOT NULL,
	[WeddingParty] [bit] NOT NULL,
	[DesignatedSeating] [bit] NOT NULL,
	[NoteToInvitee] [varchar](1080) NOT NULL,
	[Created_Date] [datetime] NOT NULL,
	[Updated_Date] [datetime] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Invitation] ADD  DEFAULT ((0)) FOR [WeddingParty]
GO

ALTER TABLE [dbo].[Invitation] ADD  DEFAULT ((0)) FOR [DesignatedSeating]
GO

ALTER TABLE [dbo].[Invitation] ADD  DEFAULT (getdate()) FOR [Created_Date]
GO

ALTER TABLE [dbo].[Invitation] ADD  DEFAULT (getdate()) FOR [Updated_Date]
GO