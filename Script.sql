USE [Tutorial]
GO
/****** Object:  User [sad]    Script Date: 23-07-2023 10:14:44 ******/
CREATE USER [sad] FOR LOGIN [sad] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Post]    Script Date: 23-07-2023 10:14:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Post](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](200) NULL,
	[Type] [nvarchar](50) NULL,
	[Datepublished] [datetime] NULL,
	[Author] [nvarchar](50) NULL,
	[status] [bit] NULL,
	[Content] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[ImagePath] [nvarchar](500) NULL,
	[TS] [datetime] NULL,
 CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Post] ON 
GO
INSERT [dbo].[Post] ([Id], [Title], [Type], [Datepublished], [Author], [status], [Content], [Image], [ImagePath], [TS]) VALUES (1, N'Google''s story begins in a humble Stanford University dormitory where Larry Page and Sergey Brin developed a revolutionary', N'News', CAST(N'2024-12-14T00:00:00.000' AS DateTime), N'George Orwell', 1, N'As an AI language model, I don''t have access to real-time information or internet browsing capabilities, including the ability to check the names of authors beyond my training data, which has a cutoff date of September 2021. Therefore, I''m unable to provide the name of the author without specific context or information about the author you are referring to. If you could provide more details or a specific context, I''d be happy to try to help you find the author''s name or provide relevant information.', NULL, N'c7462a90-431d-4ff8-bd8e-f36d5500cc8d_date.png', CAST(N'2023-07-22T11:15:48.983' AS DateTime))
GO
INSERT [dbo].[Post] ([Id], [Title], [Type], [Datepublished], [Author], [status], [Content], [Image], [ImagePath], [TS]) VALUES (2, N'After its official incorporation in 1998, Google rapidly gained popularity, eclipsing other search engines of the time. Its clean and intuitive interface', N'Event', CAST(N'2024-12-13T00:00:00.000' AS DateTime), N'Jhon ', 0, N'Movie Listings Wireframe:
The movie listings wireframe displays a list of available movies, including movie titles, posters, showtimes, and other relevant details. It may also include filtering options to refine the search by genre, language, or date.

Movie Details Wireframe:
The movie details wireframe provides more information about a specific movie, including a larger movie poster, synopsis, cast and crew details, ratings, and reviews. It can also feature a "Book Now" button to initiate the ticket booking process.', NULL, N'7b873183-4a8e-46c2-98e4-0bf7dc7f30d1_date.jpg', CAST(N'2023-07-22T12:05:38.013' AS DateTime))
GO
INSERT [dbo].[Post] ([Id], [Title], [Type], [Datepublished], [Author], [status], [Content], [Image], [ImagePath], [TS]) VALUES (3, N'Google''s impact on the digital landscape has been profound. It revolutionized the way people access information, making it easily accessible and user-friendly', N'News', CAST(N'2024-12-11T00:00:00.000' AS DateTime), N'Dr. Seuss', 0, N'In addition, the discussion highlights the significance of the proposed research in advancing the field of project management. By conducting an in-depth analysis of existing project management practices and evaluating the effectiveness of the developed web application and app, the research aims to contribute valuable insights and recommendations for future project management practices and tool development.

Overall, the discussion emphasizes the importance of the proposed research in addressing the current gaps and challenges in project management, and highlights the potential impact of the project management web application and app in improving project outcomes and team collaboration.', NULL, N'f5f33067-4304-4983-87d4-ef8ea3e61acd_date.jpg', CAST(N'2023-07-22T12:05:38.013' AS DateTime))
GO
INSERT [dbo].[Post] ([Id], [Title], [Type], [Datepublished], [Author], [status], [Content], [Image], [ImagePath], [TS]) VALUES (4, N'These wireframes serve as a blueprint for designing the user interface of a cinema ticket booking system. They help designers and stakeholders visualize the layout', N'Event', CAST(N'2024-12-10T00:00:00.000' AS DateTime), N'J. K. Rowling
', 0, N'One key aspect of the discussion is the need for a comprehensive and user-friendly project management web application and app. The discussion emphasizes that as project complexity increases and teams become more dispersed, there is a growing demand for digital tools that can facilitate effective project planning, collaboration, and communication. The proposed research aims to address this need by developing a web application and app that integrates essential project management features and provides an intuitive user interface.', N'', N'449c54f7-dfda-4b0e-af09-0eb199604cf4_date.jpg', CAST(N'2023-07-22T12:05:38.013' AS DateTime))
GO
INSERT [dbo].[Post] ([Id], [Title], [Type], [Datepublished], [Author], [status], [Content], [Image], [ImagePath], [TS]) VALUES (1003, N'The success page wireframe confirms the successful completion of the booking process. It can display a booking reference number,', N'Comment', CAST(N'2023-07-27T01:01:00.000' AS DateTime), N'Dr Raman', 1, N'The discussion for the dissertation proposal on project management web application and app centers around the significance of the proposed research and its potential contributions to the field of project management.', N'', N'9618af79-c32f-4fb0-abe1-1ce7dcbcc014_date.jpg', CAST(N'2023-07-22T23:33:36.410' AS DateTime))
GO
INSERT [dbo].[Post] ([Id], [Title], [Type], [Datepublished], [Author], [status], [Content], [Image], [ImagePath], [TS]) VALUES (1004, N' the research aims to contribute valuable insights and recommendations for future project management practices and tool development', N'Event', CAST(N'2024-12-12T00:00:00.000' AS DateTime), N'Mr.  Shanmrrion', 1, N'In addition, the discussion highlights the significance of the proposed research in advancing the field of project management. By conducting an in-depth analysis of existing project management practices and evaluating the effectiveness of the developed web application and app, the research aims to contribute valuable insights and recommendations for future project management practices and tool development.

Overall, the discussion emphasizes the importance of the proposed research in addressing the current gaps and challenges in project management, and highlights the potential impact of the project management web application and app in improving project outcomes and team collaboration.', NULL, N'27bfce0a-0c7c-4a86-a94d-1a891ca8277a_date.jpg', CAST(N'2023-07-23T09:43:21.083' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Post] OFF
GO
/****** Object:  StoredProcedure [dbo].[SaveData]    Script Date: 23-07-2023 10:14:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SaveData]
(
@Title varchar(200),
@Type varchar(200),
@Datepublished datetime,
@Author varchar(200),
@status bit,
@Content varchar(max),
@Image varchar(500),
@ImagePath varchar(500)
)
as

insert into Post(Title,Type,Datepublished,Author,status,Content,Image,ImagePath,TS)
values (@Title,@Type,@Datepublished,@Author,@status,@Content,@Image,@ImagePath,GETDATE())
if(@@ROWCOUNT>0)
return 2
else
return 0
	
GO
