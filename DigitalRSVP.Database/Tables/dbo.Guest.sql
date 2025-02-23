/****** Object:  Table [dbo].[RSVP]    Script Date: 2/23/2025 11:41:35 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RSVP](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WorkItemId] [uniqueidentifier] NOT NULL,
	[InviteeId] [uniqueidentifier] NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[Note] [varchar](1080) NOT NULL,
	[Created_Date] [datetime] NOT NULL,
	[Updated_Date] [datetime] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[RSVP] ADD  DEFAULT (getdate()) FOR [Created_Date]
GO

ALTER TABLE [dbo].[RSVP] ADD  DEFAULT (getdate()) FOR [Updated_Date]
GO


