create table [dbo].[Event]
(
	[Id] [int] identity(1,1) NOT NULL,
	[WorkItemId] [uniqueidentifier] NOT NULL,
	[Name] [varchar](360) NOT NULL,
	[ContactEmail] [varchar](360) NOT NULL,
	[ExpiryDate] [datetime] NOT NULL
)