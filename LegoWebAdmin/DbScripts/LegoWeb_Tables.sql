/*
// ----------------------------------------------------------------------
// <copyright file="LegoWeb_Tables.sql" package="LEGOWEB">
//     Copyright (C) 2010-2011 HIENDAI SOFTWARE COMPANY. All rights reserved.
//     www.legoweb.org
//     License: GNU/GPL
//     LEGOWEB IS FREE SOFTWARE
// </copyright>
// ------------------------------------------------------------------------
*/
--Modified 28-10-2011 add some new ALIAS fields
--Modified 22-09-2011 add some new fields LEGOWEB_CATEGORIES.ADMIN_LEVEL, LEGOWEB_CATEGORIES.ADMIN_ROLES, LEGOWEB_META_CONTENTS.LEADER

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  Table [dbo].[LEGOWEB_SECTIONS]    Script Date: 01/12/2010 10:31:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LEGOWEB_SECTIONS]') AND type in (N'U'))
DROP TABLE [dbo].[LEGOWEB_SECTIONS]
GO
CREATE TABLE [dbo].[LEGOWEB_SECTIONS](
	[SECTION_ID] [int] NOT NULL,
	[SECTION_VI_TITLE] [nvarchar](250) NOT NULL,	
	[SECTION_EN_TITLE] [nvarchar](250) NULL DEFAULT ('Section Title'),	
	CONSTRAINT [PK_SECTION_ID] PRIMARY KEY NONCLUSTERED  ( SECTION_ID )
	)
GO

INSERT LEGOWEB_SECTIONS VALUES(1,N'Nội dung thông thường',N'Normal Contents')
INSERT LEGOWEB_SECTIONS VALUES(2,N'Nội dung đặc thù','Special Contents')

/****** Object:  Table [dbo].[LEGOWEB_CATEGORIES]    Script Date: 01/12/2010 10:31:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LEGOWEB_CATEGORIES]') AND type in (N'U'))
DROP TABLE [dbo].[LEGOWEB_CATEGORIES]
GO
CREATE TABLE [dbo].[LEGOWEB_CATEGORIES](
	[CATEGORY_ID] [int] NOT NULL,
	[PARENT_CATEGORY_ID] [int] NULL DEFAULT 0,
	[SECTION_ID] [int] NOT NULL DEFAULT 0,
	[CATEGORY_VI_TITLE] [nvarchar](250) NOT NULL DEFAULT ('Tên Chuyên Mục'),	
	[CATEGORY_EN_TITLE] [nvarchar](250) NULL DEFAULT ('Category Title'),	
	[CATEGORY_ALIAS] [nvarchar](250) NULL,
	[CATEGORY_TEMPLATE_NAME] [nvarchar](50) NULL DEFAULT ('data_article'),--tintuc.kinhte
	[CATEGORY_IMAGE_URL] [nvarchar](250) NULL,
	[MENU_ID] [int] NOT NULL DEFAULT 0,
	[ORDER_NUMBER] int DEFAULT 0,
	[IS_PUBLIC] bit DEFAULT 1,
	[ADMIN_LEVEL] smallint DEFAULT 0, -- qui dinh cach phan quyen cap nhat noi dung cho category 0 moi nguoi dang nhap thanh cong vao quan tri, 1 nhung nguoi nam trong admin_roles
	[ADMIN_ROLES] nvarchar(250) DEFAULT NULL, -- cac nhom quyen cap nhat noi dung thuoc ve chuyen muc nay	
	CONSTRAINT [PK_CATEGORY_ID] PRIMARY KEY NONCLUSTERED  ( CATEGORY_ID )
	)
GO



/****** Object:  Table [dbo].[LEGOWEB_META_CONTENTS]    Script Date: 01/12/2010 10:31:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LEGOWEB_META_CONTENTS]') AND type in (N'U'))
DROP TABLE [dbo].[LEGOWEB_META_CONTENTS]
GO
CREATE TABLE [dbo].[LEGOWEB_META_CONTENTS](
	[META_CONTENT_ID] [int] IDENTITY(1,1) NOT NULL,
	[CATEGORY_ID] [int] NULL DEFAULT ((1)),
	[LEADER] [nvarchar] (24) DEFAULT ('                        '),-- 24 ky tu theo tieu chuan MARC phuc vu mo rong cac thuoc tinh quan tri cua META_CONTENT
	[LANG_CODE]   [nvarchar](3) NULL DEFAULT ('vie'),
	[META_CONTENT_TITLE] [nvarchar](250) NULL,
	[META_CONTENT_ALIAS] [nvarchar](250) NULL,
	[ORDER_NUMBER] int DEFAULT (0),	
	[READ_COUNT] [int] DEFAULT 0,
	[RECORD_STATUS] smallint DEFAULT ((0)),	
	[ACCESS_LEVEL] smallint DEFAULT 0,
	[ACCESS_ROLES] [nvarchar](100) NULL,
	[CREATED_DATE] [datetime] NULL DEFAULT (getdate()),
	[CREATED_USER] [nvarchar](30) NULL DEFAULT (NULL),
	[MODIFIED_DATE] [datetime] NULL DEFAULT (getdate()),
	[MODIFIED_USER] [nvarchar](30) NULL DEFAULT (NULL),
 CONSTRAINT [PK_META_CONTENT_ID] PRIMARY KEY NONCLUSTERED 
(
	[META_CONTENT_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [dbo].[LEGOWEB_META_CONTENT_NUMBERS]    Script Date: 01/12/2010 10:31:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LEGOWEB_META_CONTENT_NUMBERS]') AND type in (N'U'))
DROP TABLE [dbo].[LEGOWEB_META_CONTENT_NUMBERS]
GO
CREATE TABLE [dbo].[LEGOWEB_META_CONTENT_NUMBERS](
	[META_CONTENT_NUMBER_ID] [int] IDENTITY(1,1) NOT NULL,
	[META_CONTENT_ID] [int] NOT NULL,
	[TAG] [smallint] NOT NULL DEFAULT ((0)),
	[TAG_INDEX] [smallint] NOT NULL  DEFAULT ((0)),
	[SUBFIELD_CODE] [nchar](1) NOT NULL DEFAULT ('a'),
	[SUBFIELD_VALUE] [numeric](18, 2) NULL,
	[IS_PUBLIC]   [bit] NULL DEFAULT ((0)),	
	[ACCESS_LEVEL] smallint DEFAULT 0,
	[CREATED_DATE] [datetime] NULL DEFAULT (getdate()),
	[CREATED_USER] [nvarchar](30) NULL DEFAULT (NULL),
	[MODIFIED_DATE] [datetime] NULL DEFAULT (getdate()),
	[MODIFIED_USER] [nvarchar](30) NULL DEFAULT (NULL),
 CONSTRAINT [PK_META_CONTENT_NUMBER_ID] PRIMARY KEY NONCLUSTERED 
(
	[META_CONTENT_NUMBER_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



/****** Object:  Table [dbo].[LEGOWEB_META_CONTENT_BOOLEANS]    Script Date: 01/12/2010 10:31:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LEGOWEB_META_CONTENT_BOOLEANS]') AND type in (N'U'))
DROP TABLE [dbo].[LEGOWEB_META_CONTENT_BOOLEANS]
GO
CREATE TABLE [dbo].[LEGOWEB_META_CONTENT_BOOLEANS](
	[META_CONTENT_BOOLEAN_ID] [int] IDENTITY(1,1) NOT NULL,
	[META_CONTENT_ID] [int] NOT NULL,
	[TAG] [smallint] NOT NULL DEFAULT ((0)),
	[TAG_INDEX] [smallint] NOT NULL  DEFAULT ((0)),
	[SUBFIELD_CODE] [nchar](1) NOT NULL DEFAULT ('a'),
	[SUBFIELD_VALUE] [bit] NULL,
	[IS_PUBLIC]   [bit] NULL DEFAULT ((0)),	
	[ACCESS_LEVEL] smallint DEFAULT 0,
	[CREATED_DATE] [datetime] NULL DEFAULT (getdate()),
	[CREATED_USER] [nvarchar](30) NULL DEFAULT (NULL),
	[MODIFIED_DATE] [datetime] NULL DEFAULT (getdate()),
	[MODIFIED_USER] [nvarchar](30) NULL DEFAULT (NULL),
 CONSTRAINT [PK_META_CONTENT_BOOLEAN_ID] PRIMARY KEY NONCLUSTERED 
(
	[META_CONTENT_BOOLEAN_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO




IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LEGOWEB_META_CONTENT_DATES]') AND type in (N'U'))
DROP TABLE [dbo].[LEGOWEB_META_CONTENT_DATES]
GO
CREATE TABLE [dbo].[LEGOWEB_META_CONTENT_DATES](
	[META_CONTENT_DATE_ID] [int] IDENTITY(1,1) NOT NULL,
	[META_CONTENT_ID] [int] NOT NULL,
	[TAG] [smallint] NOT NULL DEFAULT ((0)),
	[TAG_INDEX] [smallint] NOT NULL  DEFAULT ((0)),
	[SUBFIELD_CODE] [nchar](1) NOT NULL DEFAULT ('a'),
	[SUBFIELD_VALUE] [datetime] NULL,
	[IS_PUBLIC]   [bit] NULL DEFAULT ((0)),	
	[ACCESS_LEVEL] smallint DEFAULT 0,
	[CREATED_DATE] [datetime] NULL DEFAULT (getdate()),
	[CREATED_USER] [nvarchar](30) NULL DEFAULT (NULL),
	[MODIFIED_DATE] [datetime] NULL DEFAULT (getdate()),
	[MODIFIED_USER] [nvarchar](30) NULL DEFAULT (NULL),
 CONSTRAINT [PK_META_CONTENT_DATE_ID] PRIMARY KEY NONCLUSTERED 
(
	[META_CONTENT_DATE_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO




IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LEGOWEB_META_CONTENT_TEXTS]') AND type in (N'U'))
DROP TABLE [dbo].[LEGOWEB_META_CONTENT_TEXTS]
GO
CREATE TABLE [dbo].[LEGOWEB_META_CONTENT_TEXTS](
	[META_CONTENT_TEXT_ID] [int] IDENTITY(1,1) NOT NULL,
	[META_CONTENT_ID] [int] NOT NULL,
	[TAG] [smallint] NOT NULL DEFAULT ((0)),
	[TAG_INDEX] [smallint] NOT NULL  DEFAULT ((0)),
	[SUBFIELD_CODE] [nchar](1) NOT NULL DEFAULT ('a'),
	[SUBFIELD_VALUE] [nvarchar](400) NULL,
	[IS_PUBLIC]   [bit] NULL DEFAULT ((0)),	
	[ACCESS_LEVEL] smallint DEFAULT 0,
	[CREATED_DATE] [datetime] NULL DEFAULT (getdate()),
	[CREATED_USER] [nvarchar](30) NULL DEFAULT (NULL),
	[MODIFIED_DATE] [datetime] NULL DEFAULT (getdate()),
	[MODIFIED_USER] [nvarchar](30) NULL DEFAULT (NULL),
 CONSTRAINT [PK_META_CONTENT_TEXT_ID] PRIMARY KEY NONCLUSTERED 
(
	[META_CONTENT_TEXT_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LEGOWEB_META_CONTENT_NTEXTS]') AND type in (N'U'))
DROP TABLE [dbo].[LEGOWEB_META_CONTENT_NTEXTS]
GO
CREATE TABLE [dbo].[LEGOWEB_META_CONTENT_NTEXTS](
	[META_CONTENT_NTEXT_ID] [int] IDENTITY(1,1) NOT NULL,
	[META_CONTENT_ID] [int] NOT NULL,
	[TAG] [smallint] NOT NULL DEFAULT ((0)),
	[TAG_INDEX] [smallint] NOT NULL  DEFAULT ((0)),
	[SUBFIELD_CODE] [nchar](1) NOT NULL DEFAULT ('a'),
	[SUBFIELD_VALUE] [nvarchar](max) NULL,
	[IS_PUBLIC]   [bit] NULL DEFAULT ((0)),	
	[ACCESS_LEVEL] smallint DEFAULT 0,
	[CREATED_DATE] [datetime] NULL DEFAULT (getdate()),
	[CREATED_USER] [nvarchar](30) NULL DEFAULT (NULL),
	[MODIFIED_DATE] [datetime] NULL DEFAULT (getdate()),
	[MODIFIED_USER] [nvarchar](30) NULL DEFAULT (NULL),
 CONSTRAINT [PK_META_CONTENT_NTEXT_ID] PRIMARY KEY NONCLUSTERED 
(
	[META_CONTENT_NTEXT_ID] ASC
)WITH ( PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



--LEGOWEB Menus Defination
if exists (select * from sysobjects where id = object_id('dbo.LEGOWEB_MENU_TYPES') and sysstat & 0xf = 3) drop table dbo.LEGOWEB_MENU_TYPES
GO
CREATE TABLE [dbo].[LEGOWEB_MENU_TYPES](
	[MENU_TYPE_ID] [smallint] NOT NULL,
	[MENU_TYPE_VI_TITLE] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[MENU_TYPE_EN_TITLE] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[MENU_TYPE_DESCRIPTION] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_LEGOWEB_MENU_TYPES] PRIMARY KEY CLUSTERED 
(
	[MENU_TYPE_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

INSERT LEGOWEB_MENU_TYPES VALUES(1,N'Trinh Don Ngang Chinh','Main Menubar','')

GO

if exists (select * from sysobjects where id = object_id('dbo.LEGOWEB_MENUS') and sysstat & 0xf = 3) drop table dbo.LEGOWEB_MENUS
GO
CREATE TABLE [dbo].[LEGOWEB_MENUS](
	[MENU_ID] [int] NOT NULL,
	[PARENT_MENU_ID] [int] NOT NULL DEFAULT ((0)),
	[MENU_TYPE_ID] [int] NOT NULL,
	[MENU_VI_TITLE] [nvarchar](50) NULL,
	[MENU_EN_TITLE] [nvarchar](50) NULL,
	[MENU_IMAGE_URL] [nvarchar](250) NULL,
	[MENU_LINK_URL] [nvarchar](50) NULL,	
	[BROWSER_NAVIGATE] [tinyint] NOT NULL DEFAULT ((0)),
	[ORDER_NUMBER] [smallint] NOT NULL DEFAULT ((0)),
	[IS_PUBLIC] [bit] NOT NULL DEFAULT ((1)),
	[ACCESS_LEVEL] [smallint] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_LEGOWEB_MENUS] PRIMARY KEY CLUSTERED 
(
	[MENU_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


--LEGOWEB_COMMON_PARAMETERS for System Parameters

if exists (select * from sysobjects where id = object_id('dbo.LEGOWEB_COMMON_PARAMETERS') and sysstat & 0xf = 3) drop table dbo.LEGOWEB_COMMON_PARAMETERS
GO
CREATE TABLE LEGOWEB_COMMON_PARAMETERS
(   
    PARAMETER_NAME                  NVARCHAR(50)  NOT NULL, 
	PARAMETER_TYPE					smallint default(0), --1 company registration parameters, 2 system parameters, 3 dynamic dictionary         
 	PARAMETER_VI_VALUE              NVARCHAR(255),
	PARAMETER_EN_VALUE              NVARCHAR(255),
 	PARAMETER_DESCRIPTION			NVARCHAR(255)
 	PRIMARY KEY(PARAMETER_NAME)      
)
GO

INSERT LEGOWEB_COMMON_PARAMETERS VALUES('WEBSITE_TITLE',1,N'tiêu đề website của bạn','your website title',N'Website title ')
INSERT LEGOWEB_COMMON_PARAMETERS VALUES('WEBSITE_META_DESCRIPTION',1,N'dữ liệu mô tả hỗ trợ máy tìm kiếm','your meta description for SEO',N'Website title ')
INSERT LEGOWEB_COMMON_PARAMETERS VALUES('WEBSITE_META_KEYWORDS',1,N'các từ khóa hỗ trợ máy tìm kiếm','your meta keywords for SEO',N'Website title ')
INSERT LEGOWEB_COMMON_PARAMETERS VALUES('ORGANIZATION_NAME',1,N'tên tổ chức của bạn','your organization name',N'Tên công ty trình bày dưới footer')
INSERT LEGOWEB_COMMON_PARAMETERS VALUES('ORGANIZATION_ADDRESS',1,N'địa chỉ','your organization address',N'Địa chỉ công ty trình bày dưới footer')
INSERT LEGOWEB_COMMON_PARAMETERS VALUES('ORGANIZATION_PHONE',1,N'điện thoại','your organization phone number',N'Điện thoại công ty trình bày dưới footer')
INSERT LEGOWEB_COMMON_PARAMETERS VALUES('ORGANIZATION_FAX',1,N'số fax','your organization fax number',N'Fax công ty trình bày dưới footer')
INSERT LEGOWEB_COMMON_PARAMETERS VALUES('ORGANIZATION_EMAIL',1,N'địa chỉ email','your organization email address',N'Email công ty trình bày dưới footer')
INSERT LEGOWEB_COMMON_PARAMETERS VALUES('NUMBER_OF_VISITED',2,'0','0',N'number of visted')
INSERT LEGOWEB_COMMON_PARAMETERS VALUES('REGISTRATION_EMAIL_SUBJECT',2,N'Đăng ký người sử dụng','User registration',N'Tiêu đề thư kích hoạt tài khoản đăng ký')
INSERT LEGOWEB_COMMON_PARAMETERS VALUES('REGISTRATION_EMAIL_BODY',2,N'Chào {0}, cảm ơn bạn đã đăng ký sử dụng dịch vụ của chúng tôi!.<br/> Để kích hoạt tài khoản người dùng <a href={1}> Nhấn vào đây</a>',N'Hello {0}, Welcome to LIBRARY OF NHATRANG UNIVERSITY. <p/> To activate your account <a href={1}> Click here </a>',N'Nội dung thư kích hoạt tài khoản đăng ký 2 tham số {0} tên user, {1} url kích hoạt')
INSERT LEGOWEB_COMMON_PARAMETERS VALUES('REGISTRATION_SUCCESS_GREETING',2,N'Chúc mừng, bạn đã đăng ký thành công, hãy đăng nhập để khai thác dịch vụ của chúng tôi!.','Your registration is successfull!',N'Lời chúc mừng đăng ký thành công')

GO



