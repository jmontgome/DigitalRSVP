create table [dbo].[RSVP]
(
	[Id] int identity(1,1) NOT NULL,
	[WorkItemId] uniqueidentifier NOT NULL,
	[InviteeId] uniqueidentifier NOT NULL,
	[DateTime] datetime NOT NULL,
	[GuestsData] varchar(2000) NOT NULL,
	[AttendingWedding] bit NOT NULL default 0,
	[AttendingReception] bit NOT NULL default 0,
	[Note] varchar(1080) NOT NULL,
	[Created_Date] datetime NOT NULL default getdate(),
	[Updated_Date] datetime NOT NULL default getdate()
)