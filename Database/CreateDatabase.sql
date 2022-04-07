USE [master]
GO

CREATE DATABASE [TranslateApp]
GO

USE [TranslateApp]
GO

CREATE TABLE [dbo].[Words](
	[Word_Id] [int] IDENTITY(1,1) NOT NULL,
	[OriginalText] [nvarchar](100) NULL,
	[TranslatedText] [nvarchar](500) NULL,
	[FromLanguage] [nvarchar](50) NULL,
	[ToLanguage] [nvarchar](50) NULL,
	[PhoneticSymbols] [nvarchar](max) NULL,
	[Notes] [nvarchar](max) NULL,
	[IsFavorite] [bit] NOT NULL,
 CONSTRAINT [PK_Words] PRIMARY KEY CLUSTERED 
(
	[Word_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

