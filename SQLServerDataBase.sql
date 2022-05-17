
// Changes to the Database

/****** Object:  Table [dbo].[Emails]    Script Date: 5/9/2022 4:19:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Emails]') AND type in (N'U'))
DROP TABLE [dbo].[Emails]
GO
/****** Object:  Table [dbo].[Emails]    Script Date: 5/9/2022 4:19:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Emails](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[EmailAddress] [nvarchar](255) NOT NULL,
	[IsConsumer] [tinyint] NULL,
	[IsBusiness] [tinyint] NULL,
	[IsInvestor] [tinyint] NULL,
	[CreateDateTime] [datetime2](7) NOT NULL,
	[UpdateDateTime] [datetime2](7) NULL,
 CONSTRAINT [PK_EmailsII] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
