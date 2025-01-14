create table [dbo].[Invitation]
(
	[Id] int identity(1,1) NOT NULL,
	[WorkItemId] uniqueidentifier NOT NULL,
	[Name] varchar(240) NOT NULL,
	[WeddingPart] bit NOT NULL default 0,
	[DesignatedSeating] bit NOT NULL default 0,
	[NoteToInvitee] varchar(1080) NOT NULL,
	[Created_Date] datetime NOT NULL default getdate(),
	[Updated_Date] datetime NOT NULL default getdate()
) on [PRIMARY]