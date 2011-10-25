--LEGOWEB_SECTIONS

if exists (select * from sysobjects where id = object_id('dbo.sp_LEGOWEB_SECTIONS_ADDUPDATE') and sysstat & 0xf = 4)   drop procedure dbo.sp_LEGOWEB_SECTIONS_ADDUPDATE    
GO
CREATE procedure sp_LEGOWEB_SECTIONS_ADDUPDATE
		@_SECTION_ID int,
	    @_SECTION_VI_TITLE nvarchar(250),	
	    @_SECTION_EN_TITLE nvarchar(250)
as
BEGIN
	  begin    
	     set nocount on    	     
	     if exists (select * from LEGOWEB_SECTIONS WHERE SECTION_ID =@_SECTION_ID)    
	     begin    
			update LEGOWEB_SECTIONS
			set
				SECTION_VI_TITLE=@_SECTION_VI_TITLE,	
				SECTION_EN_TITLE=@_SECTION_EN_TITLE
			where
				SECTION_ID=@_SECTION_ID
		 end    
	     else    
	     begin    
			insert into LEGOWEB_SECTIONS
			(
				SECTION_ID,
				SECTION_VI_TITLE,	
				SECTION_EN_TITLE
			)
			values
			(
				@_SECTION_ID,
				@_SECTION_VI_TITLE,	
				@_SECTION_EN_TITLE
			)	
	     end
	  end
END
GO

if exists (select * from sysobjects where id = object_id('dbo.sp_LEGOWEB_SECTIONS_REMOVE') and sysstat & 0xf = 4)   drop procedure dbo.sp_LEGOWEB_SECTIONS_REMOVE    
GO
CREATE procedure sp_LEGOWEB_SECTIONS_REMOVE
		@_SECTION_ID int
as
BEGIN
	  begin    
	     set nocount on
		 DELETE FROM LEGOWEB_SECTIONS WHERE SECTION_ID=@_SECTION_ID
		 UPDATE LEGOWEB_CATEGORIES SET SECTION_ID=0 WHERE SECTION_ID=@_SECTION_ID   	    	     
	  end
END
GO

-- LEGOWEB_CATEGORIES

if exists (select * from sysobjects where id = object_id('dbo.sp_LEGOWEB_CATEGORIES_ADDUPDATE') and sysstat & 0xf = 4)   drop procedure dbo.sp_LEGOWEB_CATEGORIES_ADDUPDATE    
GO
CREATE procedure sp_LEGOWEB_CATEGORIES_ADDUPDATE
		@_CATEGORY_ID int,
		@_PARENT_CATEGORY_ID int = 0,
		@_SECTION_ID int=0,
	    @_CATEGORY_VI_TITLE nvarchar(250),	
	    @_CATEGORY_EN_TITLE nvarchar(250),	
	    @_CATEGORY_IMAGE_URL nvarchar(250) = NULL,	
		@_CATEGORY_TEMPLATE_NAME nvarchar(50),
		@_MENU_ID int=0,
		@_IS_PUBLIC bit = 1,
		@_ADMIN_LEVEL int = 0,
		@_ADMIN_ROLES nvarchar(250) = NULL
as
BEGIN
	  begin    
	     set nocount on    	     
	     if exists (select * from LEGOWEB_CATEGORIES WHERE CATEGORY_ID =@_CATEGORY_ID)    
	     begin    
			update LEGOWEB_CATEGORIES
			set
				PARENT_CATEGORY_ID=@_PARENT_CATEGORY_ID,
				SECTION_ID=@_SECTION_ID,
				CATEGORY_VI_TITLE=@_CATEGORY_VI_TITLE,
				CATEGORY_EN_TITLE=@_CATEGORY_EN_TITLE,	
				CATEGORY_IMAGE_URL=@_CATEGORY_IMAGE_URL,
				CATEGORY_TEMPLATE_NAME=@_CATEGORY_TEMPLATE_NAME,
				MENU_ID=@_MENU_ID,
				IS_PUBLIC=@_IS_PUBLIC,
				ADMIN_LEVEL=@_ADMIN_LEVEL,
				ADMIN_ROLES=@_ADMIN_ROLES				
			where
				CATEGORY_ID=@_CATEGORY_ID
		 end    
	     else    
	     begin    
			insert into LEGOWEB_CATEGORIES
			(
				CATEGORY_ID,
				PARENT_CATEGORY_ID,
				SECTION_ID,
				CATEGORY_VI_TITLE,
				CATEGORY_EN_TITLE,
				CATEGORY_IMAGE_URL,
				CATEGORY_TEMPLATE_NAME,
				MENU_ID,
				IS_PUBLIC,
				ADMIN_LEVEL,
				ADMIN_ROLES
			)
			values
			(
				@_CATEGORY_ID,
				@_PARENT_CATEGORY_ID,
				@_SECTION_ID,
				@_CATEGORY_VI_TITLE,
				@_CATEGORY_EN_TITLE,
				@_CATEGORY_IMAGE_URL,
				@_CATEGORY_TEMPLATE_NAME,
				@_MENU_ID,
				@_IS_PUBLIC,
				@_ADMIN_LEVEL,
				@_ADMIN_ROLES
			)	
	     end
	  end
END
GO

if exists (select * from sysobjects where id = object_id('dbo.sp_LEGOWEB_CATEGORIES_REMOVE') and sysstat & 0xf = 4)   drop procedure dbo.sp_LEGOWEB_CATEGORIES_REMOVE    
GO
CREATE procedure sp_LEGOWEB_CATEGORIES_REMOVE
		@_CATEGORY_ID int
as
BEGIN
	  begin    
	     set nocount on
		 DELETE FROM LEGOWEB_CATEGORIES WHERE CATEGORY_ID=@_CATEGORY_ID
		 UPDATE LEGOWEB_CATEGORIES SET PARENT_CATEGORY_ID=0 WHERE PARENT_CATEGORY_ID=@_CATEGORY_ID   	    	     
	  end
END
GO


/****** Object:  UserDefinedFunction [dbo].[SelectChildCategoryXml]    Script Date: 06/01/2011 11:49:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SelectChildCategoryXml]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[SelectChildCategoryXml]

GO

CREATE function SelectChildCategoryXml(@Category_Id as int, @AdminMode as bit=1) -- if @AdminMode select all children otherwise select only public chilren 
returns xml 
begin     
return 
(         
	select              
		Category_Id as "@Category_Id",              
		Parent_Category_Id as "@Parent_Category_Id",             
		Category_Vi_Title + '(' + cast((Select COUNT(META_CONTENT_ID) From LEGOWEB_META_CONTENTS Where LEGOWEB_META_CONTENTS.IS_PUBLIC=1 AND LEGOWEB_META_CONTENTS.CATEGORY_ID=c.Category_Id) as nvarchar(20)) + ')' as "@Category_Vi_Title",             
		Category_En_Title + '(' + cast((Select COUNT(META_CONTENT_ID) From LEGOWEB_META_CONTENTS Where LEGOWEB_META_CONTENTS.IS_PUBLIC=1 AND LEGOWEB_META_CONTENTS.CATEGORY_ID=c.Category_Id) as nvarchar(20)) + ')' as "@Category_En_Title",             
		dbo.SelectChildCategoryXml(Category_Id,@AdminMode)         
	from LEGOWEB_CATEGORIES AS c        
	where Parent_Category_Id = @Category_Id AND ((@AdminMode=1) OR ((@AdminMode=0) AND (Is_Public=1)))
	order by Order_Number ASC    
	for xml path('category'), type     
	) 
end 

GO


if exists (select * from sysobjects where id = object_id('dbo.sp_LEGOWEB_CATEGORIES_SEARCH_COUNT') and sysstat & 0xf = 4)   drop procedure dbo.sp_LEGOWEB_CATEGORIES_SEARCH_COUNT    
GO
CREATE procedure sp_LEGOWEB_CATEGORIES_SEARCH_COUNT
		@_CATEGORY_ID int = NULL,
		@_PARENT_CATEGORY_ID int =NULL,
		@_SECTION_ID int=NULL
as
BEGIN
	WITH hierarchy
	AS
	(
	SELECT CATEGORY_ID
	FROM LEGOWEB_CATEGORIES
	WHERE ((@_CATEGORY_ID IS NULL AND ((@_PARENT_CATEGORY_ID IS NULL AND ((@_SECTION_ID IS NULL) OR (SECTION_ID=@_SECTION_ID AND PARENT_CATEGORY_ID=0))) OR (PARENT_CATEGORY_ID=@_PARENT_CATEGORY_ID))) OR (CATEGORY_ID=@_CATEGORY_ID))	  		  			   	
	UNION ALL
	SELECT c.CATEGORY_ID
	FROM LEGOWEB_CATEGORIES AS c INNER JOIN hierarchy AS p ON c.PARENT_CATEGORY_ID = p.CATEGORY_ID
	)
	SELECT COUNT( DISTINCT CATEGORY_ID)
	FROM hierarchy
END
GO

if exists (select * from sysobjects where id = object_id('dbo.sp_LEGOWEB_CATEGORIES_SEARCH_PAGE') and sysstat & 0xf = 4)   drop procedure dbo.sp_LEGOWEB_CATEGORIES_SEARCH_PAGE    
GO
CREATE procedure sp_LEGOWEB_CATEGORIES_SEARCH_PAGE
		@_CATEGORY_ID int = NULL,
		@_PARENT_CATEGORY_ID int =NULL,
		@_SECTION_ID int=NULL,
		@_SELECT_ROW_NUMBER int=10,
		@_TAB_CHARS nvarchar(20)=N' '
as
BEGIN
	WITH hierarchy
	AS
	(
	SELECT CATEGORY_ID, SECTION_ID, PARENT_CATEGORY_ID, CATEGORY_VI_TITLE,CATEGORY_EN_TITLE, MENU_ID,IS_PUBLIC,CATEGORY_TEMPLATE_NAME, 0 AS Lv, CAST('\' + LTRIM(CATEGORY_ID) + '\' AS VARCHAR(MAX)) AS DIRECTION_PATH, CAST('\' + right(replicate('0', 2) + convert(varchar(10), ORDER_NUMBER), 2) + '\' AS VARCHAR(MAX)) AS ORDER_PATH,ORDER_NUMBER, ADMIN_LEVEL, ADMIN_ROLES
	FROM LEGOWEB_CATEGORIES
	WHERE ((@_CATEGORY_ID IS NULL AND ((@_PARENT_CATEGORY_ID IS NULL AND ((@_SECTION_ID IS NULL) OR (SECTION_ID=@_SECTION_ID AND PARENT_CATEGORY_ID=0))) OR (PARENT_CATEGORY_ID=@_PARENT_CATEGORY_ID))) OR (CATEGORY_ID=@_CATEGORY_ID))	  		  	
	UNION ALL
	SELECT c.CATEGORY_ID,c.SECTION_ID, c.PARENT_CATEGORY_ID, c.CATEGORY_VI_TITLE,c.CATEGORY_EN_TITLE,c.MENU_ID,c.IS_PUBLIC,c.CATEGORY_TEMPLATE_NAME,p.Lv + 1 AS Lv, p.DIRECTION_PATH + CAST(LTRIM(c.CATEGORY_ID) + '\' AS VARCHAR(MAX)) AS DIRECTION_PATH,p.ORDER_PATH + CAST(right(replicate('0', 2) + convert(varchar(10), c.ORDER_NUMBER), 2) + '\' AS VARCHAR(MAX)) AS ORDER_PATH,c.ORDER_NUMBER,  c.ADMIN_LEVEL, c.ADMIN_ROLES
	FROM LEGOWEB_CATEGORIES AS c INNER JOIN hierarchy AS p ON c.PARENT_CATEGORY_ID = p.CATEGORY_ID
	)
	SELECT DISTINCT TOP(@_SELECT_ROW_NUMBER) hierarchy.CATEGORY_ID,hierarchy.PARENT_CATEGORY_ID,hierarchy.SECTION_ID,((REPLICATE(@_TAB_CHARS, Lv * 1)) + hierarchy.CATEGORY_VI_TITLE) AS CATEGORY_VI_TITLE,((REPLICATE(@_TAB_CHARS, Lv * 1)) + hierarchy.CATEGORY_EN_TITLE) AS CATEGORY_EN_TITLE,hierarchy.IS_PUBLIC, hierarchy.CATEGORY_TEMPLATE_NAME,hierarchy.DIRECTION_PATH,hierarchy.ORDER_PATH,hierarchy.ORDER_NUMBER, hierarchy.ADMIN_LEVEL, hierarchy.ADMIN_ROLES,LEGOWEB_SECTIONS.SECTION_VI_TITLE,LEGOWEB_SECTIONS.SECTION_EN_TITLE,LEGOWEB_MENUS.MENU_VI_TITLE,LEGOWEB_MENUS.MENU_EN_TITLE
	FROM (hierarchy INNER JOIN LEGOWEB_SECTIONS ON hierarchy.SECTION_ID= LEGOWEB_SECTIONS.SECTION_ID) LEFT JOIN LEGOWEB_MENUS ON hierarchy.MENU_ID=LEGOWEB_MENUS.MENU_ID ORDER BY ORDER_PATH ASC
	
END
GO

--LEGOWEB_META_CONTENTS

/****** Object:  StoredProcedure [dbo].[sp_LEGOWEB_META_CONTENTS_INSERT]    Script Date: 01/15/2010 10:59:33 ******/
if exists (select * from sysobjects where id = object_id('dbo.sp_LEGOWEB_META_CONTENTS_INSERT') and sysstat & 0xf = 4)   drop procedure dbo.sp_LEGOWEB_META_CONTENTS_INSERT    
GO
CREATE procedure [dbo].[sp_LEGOWEB_META_CONTENTS_INSERT]
	@_CATEGORY_ID int,
	@_LEADER nvarchar(24)=N'                        ', --reserve 24 charaters space compatible with MARC 21 Bib Leader
	@_LANG_CODE nvarchar (3)='vie',	
	@_META_CONTENT_TITLE nvarchar (250),
	@_CREATED_USER nvarchar (30),
	@_IS_PUBLIC bit,
	@_ACCESS_LEVEL smallint=0, 
	@_META_CONTENT_ID int OUTPUT
as
BEGIN
	insert into LEGOWEB_META_CONTENTS
	(
		CATEGORY_ID,
		LEADER,
		LANG_CODE,
		META_CONTENT_TITLE,
		IS_PUBLIC,
		ACCESS_LEVEL,
		CREATED_USER		
	)
	values
	(
		@_CATEGORY_ID,
		@_LEADER,
		@_LANG_CODE,
		@_META_CONTENT_TITLE,
		@_IS_PUBLIC,
		@_ACCESS_LEVEL,
		@_CREATED_USER				
	)	
	set @_META_CONTENT_ID = @@identity 
END

GO

/****** Object:  StoredProcedure [dbo].[sp_LEGOWEB_META_CONTENTS_UPDATE]    Script Date: 01/15/2010 11:01:14 ******/
if exists (select * from sysobjects where id = object_id('dbo.sp_LEGOWEB_META_CONTENTS_UPDATE') and sysstat & 0xf = 4)   drop procedure dbo.sp_LEGOWEB_META_CONTENTS_UPDATE    
GO
CREATE procedure [dbo].[sp_LEGOWEB_META_CONTENTS_UPDATE]
	@_META_CONTENT_ID int,
	@_CATEGORY_ID smallint,
	@_LEADER nvarchar (24)='                        ',
	@_LANG_CODE nvarchar (3)='vie',
	@_META_CONTENT_TITLE nvarchar (250),	
	@_MODIFIED_USER nvarchar (30),
	@_IS_PUBLIC bit,
	@_ACCESS_LEVEL smallint=0
as
BEGIN
	UPDATE LEGOWEB_META_CONTENTS
	SET 	
	CATEGORY_ID=@_CATEGORY_ID,
	LEADER=@_LEADER,
	LANG_CODE=@_LANG_CODE,
	META_CONTENT_TITLE=@_META_CONTENT_TITLE,
	MODIFIED_USER=@_MODIFIED_USER,	
	IS_PUBLIC=@_IS_PUBLIC,
	MODIFIED_DATE=getdate(),
	ACCESS_LEVEL=@_ACCESS_LEVEL
	WHERE META_CONTENT_ID=@_META_CONTENT_ID	
END
GO


if exists (select * from sysobjects where id = object_id('dbo.sp_LEGOWEB_META_CONTENTS_ADMIN_SEARCH_COUNT') and sysstat & 0xf = 4)   drop procedure dbo.sp_LEGOWEB_META_CONTENTS_ADMIN_SEARCH_COUNT    
GO
CREATE procedure sp_LEGOWEB_META_CONTENTS_ADMIN_SEARCH_COUNT
		@_SECTION_ID int=NULL,
		@_ROOT_CATEGORY_ID int = NULL
as

BEGIN

	WITH hierarchy
	AS
	(
	SELECT CATEGORY_ID,CATEGORY_VI_TITLE
	FROM LEGOWEB_CATEGORIES
	WHERE (((@_ROOT_CATEGORY_ID IS NULL) AND ((@_SECTION_ID IS NULL) OR (SECTION_ID=@_SECTION_ID AND PARENT_CATEGORY_ID=0))) OR (CATEGORY_ID=@_ROOT_CATEGORY_ID))
		  		   	
	UNION ALL

	SELECT c.CATEGORY_ID,c.CATEGORY_VI_TITLE
	FROM LEGOWEB_CATEGORIES AS c INNER JOIN hierarchy AS p ON c.PARENT_CATEGORY_ID = p.CATEGORY_ID
	)
	SELECT COUNT(DISTINCT LEGOWEB_META_CONTENTS.META_CONTENT_ID) FROM LEGOWEB_META_CONTENTS INNER JOIN hierarchy ON LEGOWEB_META_CONTENTS.CATEGORY_ID=hierarchy.CATEGORY_ID 
END
GO


if exists (select * from sysobjects where id = object_id('dbo.sp_LEGOWEB_META_CONTENTS_ADMIN_SEARCH_PAGE') and sysstat & 0xf = 4)   drop procedure dbo.sp_LEGOWEB_META_CONTENTS_ADMIN_SEARCH_PAGE    
GO
CREATE procedure sp_LEGOWEB_META_CONTENTS_ADMIN_SEARCH_PAGE
		@_SECTION_ID int=NULL,
		@_ROOT_CATEGORY_ID int = NULL,
		@_SELECT_ROW_NUMBER int=10
as
BEGIN
	WITH hierarchy
	AS
	(
		SELECT CATEGORY_ID,CATEGORY_VI_TITLE
		FROM LEGOWEB_CATEGORIES
		WHERE (((@_ROOT_CATEGORY_ID IS NULL) AND ((@_SECTION_ID IS NULL) OR (SECTION_ID=@_SECTION_ID AND PARENT_CATEGORY_ID=0))) OR (CATEGORY_ID=@_ROOT_CATEGORY_ID))
		UNION ALL
		SELECT c.CATEGORY_ID,c.CATEGORY_VI_TITLE
		FROM LEGOWEB_CATEGORIES AS c INNER JOIN hierarchy AS p ON c.PARENT_CATEGORY_ID = p.CATEGORY_ID
	)
	SELECT DISTINCT TOP(@_SELECT_ROW_NUMBER) LEGOWEB_META_CONTENTS.*,hierarchy.CATEGORY_VI_TITLE 
	FROM LEGOWEB_META_CONTENTS INNER JOIN hierarchy ON LEGOWEB_META_CONTENTS.CATEGORY_ID=hierarchy.CATEGORY_ID 
	ORDER BY LEGOWEB_META_CONTENTS.MODIFIED_DATE DESC	
END
GO

--LEGOWEB_META_CONTENT_NUMBERS

/****** Object:  StoredProcedure [dbo].[sp_LEGOWEB_META_CONTENT_NUMBERS_INSERT]    Script Date: 01/15/2010 10:59:33 ******/
if exists (select * from sysobjects where id = object_id('dbo.sp_LEGOWEB_META_CONTENT_NUMBERS_INSERT') and sysstat & 0xf = 4)   drop procedure dbo.sp_LEGOWEB_META_CONTENT_NUMBERS_INSERT    
GO
CREATE procedure [dbo].[sp_LEGOWEB_META_CONTENT_NUMBERS_INSERT]
	@_META_CONTENT_ID int,
	@_TAG smallint,
	@_TAG_INDEX smallint,	
	@_SUBFIELD_CODE [nchar](1)='a',
	@_SUBFIELD_VALUE [numeric](18, 2) =0,
	@_IS_PUBLIC bit,
	@_ACCESS_LEVEL smallint=0, 
	@_CREATED_USER nvarchar (30),
	@_META_CONTENT_NUMBER_ID int OUTPUT
as
BEGIN
	insert into LEGOWEB_META_CONTENT_NUMBERS
	(
		META_CONTENT_ID,
		TAG,
		TAG_INDEX,
		SUBFIELD_CODE,
		SUBFIELD_VALUE,
		IS_PUBLIC,
		ACCESS_LEVEL,
		CREATED_USER		
	)
	values
	(
		@_META_CONTENT_ID,
		@_TAG,
		@_TAG_INDEX,
		@_SUBFIELD_CODE,
		@_SUBFIELD_VALUE,
		@_IS_PUBLIC,
		@_ACCESS_LEVEL,
		@_CREATED_USER				
	)	
	set @_META_CONTENT_NUMBER_ID = @@identity 
END

GO

/****** Object:  StoredProcedure [dbo].[sp_LEGOWEB_META_CONTENT_NUMBERS_UPDATE]    Script Date: 01/15/2010 11:01:14 ******/
if exists (select * from sysobjects where id = object_id('dbo.sp_LEGOWEB_META_CONTENT_NUMBERS_UPDATE') and sysstat & 0xf = 4)   drop procedure dbo.sp_LEGOWEB_META_CONTENT_NUMBERS_UPDATE    
GO
CREATE procedure [dbo].[sp_LEGOWEB_META_CONTENT_NUMBERS_UPDATE]
	@_META_CONTENT_NUMBER_ID int,
	@_TAG smallint=0,
	@_TAG_INDEX smallint=0,
	@_SUBFIELD_CODE nvarchar(1)='a',
	@_SUBFIELD_VALUE [numeric](18, 2) =0,	
	@_IS_PUBLIC bit,
	@_ACCESS_LEVEL smallint=0, 
	@_MODIFIED_USER nvarchar (30)
as
BEGIN
	UPDATE LEGOWEB_META_CONTENT_NUMBERS
	SET
	TAG=@_TAG,
	TAG_INDEX=@_TAG_INDEX,
	SUBFIELD_CODE=@_SUBFIELD_CODE, 	
	SUBFIELD_VALUE=@_SUBFIELD_VALUE,
	MODIFIED_USER=@_MODIFIED_USER,	
	IS_PUBLIC=@_IS_PUBLIC,
	MODIFIED_DATE=getdate(),
	ACCESS_LEVEL=@_ACCESS_LEVEL
	WHERE META_CONTENT_NUMBER_ID=@_META_CONTENT_NUMBER_ID	
END
GO

--LEGOWEB_META_CONTENT_BOOLEANS

/****** Object:  StoredProcedure [dbo].[sp_LEGOWEB_META_CONTENT_BOOLEANS_INSERT]    Script Date: 01/15/2010 10:59:33 ******/
if exists (select * from sysobjects where id = object_id('dbo.sp_LEGOWEB_META_CONTENT_BOOLEANS_INSERT') and sysstat & 0xf = 4)   drop procedure dbo.sp_LEGOWEB_META_CONTENT_BOOLEANS_INSERT    
GO
CREATE procedure [dbo].[sp_LEGOWEB_META_CONTENT_BOOLEANS_INSERT]
	@_META_CONTENT_ID int,
	@_TAG smallint,
	@_TAG_INDEX smallint,	
	@_SUBFIELD_CODE [nchar](1)='a',
	@_SUBFIELD_VALUE [bit] =0,
	@_IS_PUBLIC bit,
	@_ACCESS_LEVEL smallint=0, 
	@_CREATED_USER nvarchar (30),
	@_META_CONTENT_BOOLEAN_ID int OUTPUT
as
BEGIN
	insert into LEGOWEB_META_CONTENT_BOOLEANS
	(
		META_CONTENT_ID,
		TAG,
		TAG_INDEX,
		SUBFIELD_CODE,
		SUBFIELD_VALUE,
		IS_PUBLIC,
		ACCESS_LEVEL,
		CREATED_USER		
	)
	values
	(
		@_META_CONTENT_ID,
		@_TAG,
		@_TAG_INDEX,
		@_SUBFIELD_CODE,
		@_SUBFIELD_VALUE,
		@_IS_PUBLIC,
		@_ACCESS_LEVEL,
		@_CREATED_USER				
	)	
	set @_META_CONTENT_BOOLEAN_ID = @@identity 
END

GO

/****** Object:  StoredProcedure [dbo].[sp_LEGOWEB_META_CONTENT_BOOLEANS_UPDATE]    Script Date: 01/15/2010 11:01:14 ******/
if exists (select * from sysobjects where id = object_id('dbo.sp_LEGOWEB_META_CONTENT_BOOLEANS_UPDATE') and sysstat & 0xf = 4)   drop procedure dbo.sp_LEGOWEB_META_CONTENT_BOOLEANS_UPDATE    
GO
CREATE procedure [dbo].[sp_LEGOWEB_META_CONTENT_BOOLEANS_UPDATE]
	@_META_CONTENT_BOOLEAN_ID int,
	@_TAG smallint=0,
	@_TAG_INDEX smallint=0,
	@_SUBFIELD_CODE nvarchar(1)='a',
	@_SUBFIELD_VALUE [bit] =0,	
	@_IS_PUBLIC bit,
	@_ACCESS_LEVEL smallint=0, 
	@_MODIFIED_USER nvarchar (30)
as
BEGIN
	UPDATE LEGOWEB_META_CONTENT_BOOLEANS
	SET
	TAG=@_TAG,
	TAG_INDEX=@_TAG_INDEX,
	SUBFIELD_CODE=@_SUBFIELD_CODE, 	
	SUBFIELD_VALUE=@_SUBFIELD_VALUE,
	MODIFIED_USER=@_MODIFIED_USER,	
	IS_PUBLIC=@_IS_PUBLIC,
	MODIFIED_DATE=getdate(),
	ACCESS_LEVEL=@_ACCESS_LEVEL
	WHERE META_CONTENT_BOOLEAN_ID=@_META_CONTENT_BOOLEAN_ID	
END
GO

--LEGOWEB_META_CONTENT_DATES

/****** Object:  StoredProcedure [dbo].[sp_LEGOWEB_META_CONTENT_DATES_INSERT]    Script Date: 01/15/2010 10:59:33 ******/
if exists (select * from sysobjects where id = object_id('dbo.sp_LEGOWEB_META_CONTENT_DATES_INSERT') and sysstat & 0xf = 4)   drop procedure dbo.sp_LEGOWEB_META_CONTENT_DATES_INSERT    
GO
CREATE procedure [dbo].[sp_LEGOWEB_META_CONTENT_DATES_INSERT]
	@_META_CONTENT_ID int,
	@_TAG smallint,
	@_TAG_INDEX smallint,	
	@_SUBFIELD_CODE [nchar](1)='a',
	@_SUBFIELD_VALUE [nvarchar](30),
	@_IS_PUBLIC bit,
	@_ACCESS_LEVEL smallint=0, 
	@_CREATED_USER nvarchar (30),
	@_META_CONTENT_DATE_ID int OUTPUT
as
BEGIN
	insert into LEGOWEB_META_CONTENT_DATES
	(
		META_CONTENT_ID,
		TAG,
		TAG_INDEX,
		SUBFIELD_CODE,
		SUBFIELD_VALUE,
		IS_PUBLIC,
		ACCESS_LEVEL,
		CREATED_USER		
	)
	values
	(
		@_META_CONTENT_ID,
		@_TAG,
		@_TAG_INDEX,
		@_SUBFIELD_CODE,
		@_SUBFIELD_VALUE,
		@_IS_PUBLIC,
		@_ACCESS_LEVEL,
		@_CREATED_USER				
	)	
	set @_META_CONTENT_DATE_ID = @@identity 
END

GO

/****** Object:  StoredProcedure [dbo].[sp_LEGOWEB_META_CONTENT_DATES_UPDATE]    Script Date: 01/15/2010 11:01:14 ******/
if exists (select * from sysobjects where id = object_id('dbo.sp_LEGOWEB_META_CONTENT_DATES_UPDATE') and sysstat & 0xf = 4)   drop procedure dbo.sp_LEGOWEB_META_CONTENT_DATES_UPDATE    
GO
CREATE procedure [dbo].[sp_LEGOWEB_META_CONTENT_DATES_UPDATE]
	@_META_CONTENT_DATE_ID int,
	@_TAG smallint=0,
	@_TAG_INDEX smallint=0,
	@_SUBFIELD_CODE nvarchar(1)='a',
	@_SUBFIELD_VALUE [nvarchar](30),
	@_IS_PUBLIC bit,
	@_ACCESS_LEVEL smallint=0, 
	@_MODIFIED_USER nvarchar (30)
as
BEGIN
	UPDATE LEGOWEB_META_CONTENT_DATES
	SET 	
	TAG=@_TAG,
	TAG_INDEX=@_TAG_INDEX,
	SUBFIELD_CODE=@_SUBFIELD_CODE,
	SUBFIELD_VALUE=@_SUBFIELD_VALUE,
	MODIFIED_USER=@_MODIFIED_USER,	
	IS_PUBLIC=@_IS_PUBLIC,
	MODIFIED_DATE=getdate(),
	ACCESS_LEVEL=@_ACCESS_LEVEL
	WHERE META_CONTENT_DATE_ID=@_META_CONTENT_DATE_ID	
END
GO

--LEGOWEB_META_CONTENT_TEXTS

/****** Object:  StoredProcedure [dbo].[sp_LEGOWEB_META_CONTENT_TEXTS_INSERT]    Script Date: 01/15/2010 10:59:33 ******/
if exists (select * from sysobjects where id = object_id('dbo.sp_LEGOWEB_META_CONTENT_TEXTS_INSERT') and sysstat & 0xf = 4)   drop procedure dbo.sp_LEGOWEB_META_CONTENT_TEXTS_INSERT    
GO
CREATE procedure [dbo].[sp_LEGOWEB_META_CONTENT_TEXTS_INSERT]
	@_META_CONTENT_ID int,
	@_TAG smallint,
	@_TAG_INDEX smallint,	
	@_SUBFIELD_CODE [nchar](1)='a',
	@_SUBFIELD_VALUE [nvarchar](400),
	@_IS_PUBLIC bit,
	@_ACCESS_LEVEL smallint=0, 
	@_CREATED_USER nvarchar (30),
	@_META_CONTENT_TEXT_ID int OUTPUT
as
BEGIN
	insert into LEGOWEB_META_CONTENT_TEXTS
	(
		META_CONTENT_ID,
		TAG,
		TAG_INDEX,
		SUBFIELD_CODE,
		SUBFIELD_VALUE,
		IS_PUBLIC,
		ACCESS_LEVEL,
		CREATED_USER		
	)
	values
	(
		@_META_CONTENT_ID,
		@_TAG,
		@_TAG_INDEX,
		@_SUBFIELD_CODE,
		@_SUBFIELD_VALUE,
		@_IS_PUBLIC,
		@_ACCESS_LEVEL,
		@_CREATED_USER				
	)	
	set @_META_CONTENT_TEXT_ID = @@identity 
END

GO

/****** Object:  StoredProcedure [dbo].[sp_LEGOWEB_META_CONTENT_TEXTS_UPDATE]    Script Date: 01/15/2010 11:01:14 ******/
if exists (select * from sysobjects where id = object_id('dbo.sp_LEGOWEB_META_CONTENT_TEXTS_UPDATE') and sysstat & 0xf = 4)   drop procedure dbo.sp_LEGOWEB_META_CONTENT_TEXTS_UPDATE    
GO
CREATE procedure [dbo].[sp_LEGOWEB_META_CONTENT_TEXTS_UPDATE]
	@_META_CONTENT_TEXT_ID int,
	@_TAG smallint=0,
	@_TAG_INDEX smallint=0,
	@_SUBFIELD_CODE nvarchar(1)='a',
	@_SUBFIELD_VALUE [nvarchar](400),
	@_IS_PUBLIC bit,
	@_ACCESS_LEVEL smallint=0, 
	@_MODIFIED_USER nvarchar (30)
as
BEGIN
	UPDATE LEGOWEB_META_CONTENT_TEXTS
	SET 	
	TAG=@_TAG,
	TAG_INDEX=@_TAG_INDEX,
	SUBFIELD_CODE=@_SUBFIELD_CODE,
	SUBFIELD_VALUE=@_SUBFIELD_VALUE,
	MODIFIED_USER=@_MODIFIED_USER,	
	IS_PUBLIC=@_IS_PUBLIC,
	MODIFIED_DATE=getdate(),
	ACCESS_LEVEL=@_ACCESS_LEVEL
	WHERE META_CONTENT_TEXT_ID=@_META_CONTENT_TEXT_ID	
END
GO


--LEGOWEB_META_CONTENT_NTEXTS

/****** Object:  StoredProcedure [dbo].[sp_LEGOWEB_META_CONTENT_NTEXTS_INSERT]    Script Date: 01/15/2010 10:59:33 ******/
if exists (select * from sysobjects where id = object_id('dbo.sp_LEGOWEB_META_CONTENT_NTEXTS_INSERT') and sysstat & 0xf = 4)   drop procedure dbo.sp_LEGOWEB_META_CONTENT_NTEXTS_INSERT    
GO
CREATE procedure [dbo].[sp_LEGOWEB_META_CONTENT_NTEXTS_INSERT]
	@_META_CONTENT_ID int,
	@_TAG smallint,
	@_TAG_INDEX smallint,	
	@_SUBFIELD_CODE [nchar](1)='a',
	@_SUBFIELD_VALUE [nvarchar](max),
	@_IS_PUBLIC bit,
	@_ACCESS_LEVEL smallint=0, 
	@_CREATED_USER nvarchar (30),
	@_META_CONTENT_NTEXT_ID int OUTPUT
as
BEGIN
	insert into LEGOWEB_META_CONTENT_NTEXTS
	(
		META_CONTENT_ID,
		TAG,
		TAG_INDEX,
		SUBFIELD_CODE,
		SUBFIELD_VALUE,
		IS_PUBLIC,
		ACCESS_LEVEL,
		CREATED_USER		
	)
	values
	(
		@_META_CONTENT_ID,
		@_TAG,
		@_TAG_INDEX,
		@_SUBFIELD_CODE,
		@_SUBFIELD_VALUE,
		@_IS_PUBLIC,
		@_ACCESS_LEVEL,
		@_CREATED_USER				
	)	
	set @_META_CONTENT_NTEXT_ID = @@identity 
END

GO

/****** Object:  StoredProcedure [dbo].[sp_LEGOWEB_META_CONTENT_NTEXTS_UPDATE]    Script Date: 01/15/2010 11:01:14 ******/
if exists (select * from sysobjects where id = object_id('dbo.sp_LEGOWEB_META_CONTENT_NTEXTS_UPDATE') and sysstat & 0xf = 4)   drop procedure dbo.sp_LEGOWEB_META_CONTENT_NTEXTS_UPDATE    
GO
CREATE procedure [dbo].[sp_LEGOWEB_META_CONTENT_NTEXTS_UPDATE]
	@_META_CONTENT_NTEXT_ID int,
	@_TAG smallint=0,
	@_TAG_INDEX smallint=0,
	@_SUBFIELD_CODE nvarchar(1)='a',
	@_SUBFIELD_VALUE [nvarchar](max),
	@_IS_PUBLIC bit,
	@_ACCESS_LEVEL smallint=0, 
	@_MODIFIED_USER nvarchar (30)
as
BEGIN
	UPDATE LEGOWEB_META_CONTENT_NTEXTS
	SET 	
	TAG=@_TAG,
	TAG_INDEX=@_TAG_INDEX,
	SUBFIELD_CODE=@_SUBFIELD_CODE,
	SUBFIELD_VALUE=@_SUBFIELD_VALUE,
	MODIFIED_USER=@_MODIFIED_USER,	
	IS_PUBLIC=@_IS_PUBLIC,
	MODIFIED_DATE=getdate(),
	ACCESS_LEVEL=@_ACCESS_LEVEL
	WHERE META_CONTENT_NTEXT_ID=@_META_CONTENT_NTEXT_ID	
END
GO







/****** Object:  StoredProcedure [dbo].[sp_LEGOWEB_META_CONTENTS_GET_FOR_XML]    Script Date: 01/15/2010 11:01:14 ******/
if exists (select * from sysobjects where id = object_id('dbo.sp_LEGOWEB_META_CONTENTS_GET_FOR_XML') and sysstat & 0xf = 4)   drop procedure dbo.sp_LEGOWEB_META_CONTENTS_GET_FOR_XML    
GO
CREATE procedure [dbo].[sp_LEGOWEB_META_CONTENTS_GET_FOR_XML]
	@_META_CONTENT_ID int,
	@_SCOPE bit=0
as
BEGIN
if(@_SCOPE=0)
	BEGIN
	select META_CONTENT_DATE_ID AS ID,TAG,TAG_INDEX,SUBFIELD_CODE,'DATE' AS SUBFIELD_TYPE,CONVERT(nvarchar,SUBFIELD_VALUE,103) AS SUBFIELD_VALUE from LEGOWEB_META_CONTENT_DATES WHERE META_CONTENT_ID=@_META_CONTENT_ID
	UNION ALL
	select META_CONTENT_BOOLEAN_ID AS ID,TAG,TAG_INDEX,SUBFIELD_CODE,'BOOLEAN' AS SUBFIELD_TYPE,CONVERT(nvarchar,SUBFIELD_VALUE) AS SUBFIELD_VALUE from LEGOWEB_META_CONTENT_BOOLEANS WHERE META_CONTENT_ID=@_META_CONTENT_ID
	UNION ALL
	select META_CONTENT_NUMBER_ID AS ID,TAG,TAG_INDEX,SUBFIELD_CODE,'NUMBER' AS SUBFIELD_TYPE,CONVERT(nvarchar,SUBFIELD_VALUE) AS SUBFIELD_VALUE from LEGOWEB_META_CONTENT_NUMBERS WHERE META_CONTENT_ID=@_META_CONTENT_ID
	UNION ALL
	select META_CONTENT_TEXT_ID AS ID,TAG,TAG_INDEX,SUBFIELD_CODE,'TEXT' AS SUBFIELD_TYPE,SUBFIELD_VALUE from LEGOWEB_META_CONTENT_TEXTS WHERE META_CONTENT_ID=@_META_CONTENT_ID
	ORDER BY TAG, TAG_INDEX, SUBFIELD_CODE ASC
	END
ELSE IF(@_SCOPE=1)
	BEGIN
	select META_CONTENT_DATE_ID AS ID,TAG,TAG_INDEX,SUBFIELD_CODE,'DATE' AS SUBFIELD_TYPE,CONVERT(nvarchar,SUBFIELD_VALUE,103) AS SUBFIELD_VALUE from LEGOWEB_META_CONTENT_DATES WHERE META_CONTENT_ID=@_META_CONTENT_ID
	UNION ALL
	select META_CONTENT_BOOLEAN_ID AS ID,TAG,TAG_INDEX,SUBFIELD_CODE,'BOOLEAN' AS SUBFIELD_TYPE,CONVERT(nvarchar,SUBFIELD_VALUE) AS SUBFIELD_VALUE from LEGOWEB_META_CONTENT_BOOLEANS WHERE META_CONTENT_ID=@_META_CONTENT_ID
	UNION ALL
	select META_CONTENT_NUMBER_ID AS ID,TAG,TAG_INDEX,SUBFIELD_CODE,'NUMBER' AS SUBFIELD_TYPE,CONVERT(nvarchar,SUBFIELD_VALUE) AS SUBFIELD_VALUE from LEGOWEB_META_CONTENT_NUMBERS WHERE META_CONTENT_ID=@_META_CONTENT_ID
	UNION ALL
	select META_CONTENT_TEXT_ID AS ID,TAG,TAG_INDEX,SUBFIELD_CODE,'TEXT' AS SUBFIELD_TYPE,SUBFIELD_VALUE from LEGOWEB_META_CONTENT_TEXTS WHERE META_CONTENT_ID=@_META_CONTENT_ID
	UNION ALL
	select META_CONTENT_NTEXT_ID AS ID,TAG,TAG_INDEX,SUBFIELD_CODE,'NTEXT' AS SUBFIELD_TYPE,SUBFIELD_VALUE from LEGOWEB_META_CONTENT_NTEXTS WHERE META_CONTENT_ID=@_META_CONTENT_ID
	ORDER BY TAG, TAG_INDEX, SUBFIELD_CODE ASC
	END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_LEGOWEB_META_CONTENTS_DELETE]    Script Date: 01/15/2010 11:01:14 ******/
if exists (select * from sysobjects where id = object_id('dbo.sp_LEGOWEB_META_CONTENTS_DELETE') and sysstat & 0xf = 4)   drop procedure dbo.sp_LEGOWEB_META_CONTENTS_DELETE    
GO
CREATE procedure [dbo].[sp_LEGOWEB_META_CONTENTS_DELETE]
	@_META_CONTENT_ID int
as
BEGIN

DELETE FROM LEGOWEB_META_CONTENT_DATES WHERE META_CONTENT_ID=@_META_CONTENT_ID
DELETE FROM LEGOWEB_META_CONTENT_NUMBERS WHERE META_CONTENT_ID=@_META_CONTENT_ID
DELETE FROM LEGOWEB_META_CONTENT_TEXTS WHERE META_CONTENT_ID=@_META_CONTENT_ID
DELETE FROM LEGOWEB_META_CONTENT_NTEXTS WHERE META_CONTENT_ID=@_META_CONTENT_ID
DELETE FROM LEGOWEB_META_CONTENTS WHERE META_CONTENT_ID=@_META_CONTENT_ID

END
GO

/****** Object:  StoredProcedure [dbo].[sp_LEGOWEB_META_CONTENTS_SUBFIELD_REMOVE]    Script Date: 01/15/2010 11:01:14 ******/
if exists (select * from sysobjects where id = object_id('dbo.sp_LEGOWEB_META_CONTENTS_SUBFIELD_REMOVE') and sysstat & 0xf = 4)   drop procedure dbo.sp_LEGOWEB_META_CONTENTS_SUBFIELD_REMOVE    
GO
CREATE procedure [dbo].[sp_LEGOWEB_META_CONTENTS_SUBFIELD_REMOVE]
	@_SUBFIELD_ID int
as
BEGIN

DELETE FROM LEGOWEB_META_CONTENT_DATES WHERE META_CONTENT_DATE_ID=@_SUBFIELD_ID
DELETE FROM LEGOWEB_META_CONTENT_BOOLEANS WHERE META_CONTENT_BOOLEAN_ID=@_SUBFIELD_ID
DELETE FROM LEGOWEB_META_CONTENT_NUMBERS WHERE META_CONTENT_NUMBER_ID=@_SUBFIELD_ID
DELETE FROM LEGOWEB_META_CONTENT_TEXTS WHERE META_CONTENT_TEXT_ID=@_SUBFIELD_ID
DELETE FROM LEGOWEB_META_CONTENT_NTEXTS WHERE META_CONTENT_NTEXT_ID=@_SUBFIELD_ID

END
GO


--LEGOWEB_MENU_TYPES

if exists (select * from sysobjects where id = object_id('dbo.sp_LEGOWEB_MENU_TYPES_ADDUPDATE') and sysstat & 0xf = 4)   drop procedure dbo.sp_LEGOWEB_MENU_TYPES_ADDUPDATE    
GO
CREATE procedure sp_LEGOWEB_MENU_TYPES_ADDUPDATE
		@_MENU_TYPE_ID int,
	    @_MENU_TYPE_VI_TITLE nvarchar(250),	
	    @_MENU_TYPE_EN_TITLE nvarchar(250),	
		@_MENU_TYPE_DESCRIPTION nvarchar(50)
as
BEGIN
	  begin    
	     set nocount on    	     
	     if exists (select * from LEGOWEB_MENU_TYPES WHERE MENU_TYPE_ID =@_MENU_TYPE_ID)    
	     begin    
			update LEGOWEB_MENU_TYPES
			set
				MENU_TYPE_VI_TITLE=@_MENU_TYPE_VI_TITLE,	
				MENU_TYPE_EN_TITLE=@_MENU_TYPE_EN_TITLE,	
				MENU_TYPE_DESCRIPTION=@_MENU_TYPE_DESCRIPTION	
			where
				MENU_TYPE_ID=@_MENU_TYPE_ID
		 end    
	     else    
	     begin    
			insert into LEGOWEB_MENU_TYPES
			(
				MENU_TYPE_ID,
				MENU_TYPE_VI_TITLE,	
				MENU_TYPE_EN_TITLE,	
				MENU_TYPE_DESCRIPTION	
			)
			values
			(
				@_MENU_TYPE_ID,
				@_MENU_TYPE_VI_TITLE,
				@_MENU_TYPE_EN_TITLE,	
				@_MENU_TYPE_DESCRIPTION
			)	
	     end
	  end
END
GO

if exists (select * from sysobjects where id = object_id('dbo.sp_LEGOWEB_MENU_TYPES_REMOVE') and sysstat & 0xf = 4)   drop procedure dbo.sp_LEGOWEB_MENU_TYPES_REMOVE    
GO
CREATE procedure sp_LEGOWEB_MENU_TYPES_REMOVE
		@_MENU_TYPE_ID int
as
BEGIN
	  begin    
	     set nocount on
		 DELETE FROM LEGOWEB_MENU_TYPES WHERE MENU_TYPE_ID=@_MENU_TYPE_ID
		 UPDATE LEGOWEB_MENUS SET MENU_TYPE_ID=0 WHERE MENU_TYPE_ID=@_MENU_TYPE_ID   	    	     
	  end
END
GO

--LEGOWEB_MENU

if exists (select * from sysobjects where id = object_id('dbo.sp_LEGOWEB_MENUS_ADDUPDATE') and sysstat & 0xf = 4)   drop procedure dbo.sp_LEGOWEB_MENUS_ADDUPDATE    
GO
CREATE procedure sp_LEGOWEB_MENUS_ADDUPDATE
		@_MENU_ID int,
		@_PARENT_MENU_ID int = 0,
		@_MENU_TYPE_ID int=0,
	    @_MENU_VI_TITLE nvarchar(250),	
	    @_MENU_EN_TITLE nvarchar(250)=NULL,	
		@_MENU_LINK_URL nvarchar(50)=NULL,
		@_MENU_IMAGE_URL nvarchar(250)=null,
		@_BROWSER_NAVIGATE tinyint=0,
		@_IS_PUBLIC bit = 1,
		@_ACCESS_LEVEL tinyint=0		
as
BEGIN
	  begin    
	     set nocount on    	     
	     if exists (select * from LEGOWEB_MENUS WHERE MENU_ID =@_MENU_ID)    
	     begin    
			update LEGOWEB_MENUS
			set
				PARENT_MENU_ID=@_PARENT_MENU_ID,
				MENU_TYPE_ID=@_MENU_TYPE_ID,
				MENU_VI_TITLE=@_MENU_VI_TITLE,
				MENU_EN_TITLE=@_MENU_EN_TITLE,	
				MENU_LINK_URL=@_MENU_LINK_URL,
				MENU_IMAGE_URL=@_MENU_IMAGE_URL,
				BROWSER_NAVIGATE=@_BROWSER_NAVIGATE,
				IS_PUBLIC=@_IS_PUBLIC,
				ACCESS_LEVEL=@_ACCESS_LEVEL		
			where
				MENU_ID=@_MENU_ID
		 end    
	     else    
	     begin    
			insert into LEGOWEB_MENUS
			(
				MENU_ID,
				PARENT_MENU_ID,
				MENU_TYPE_ID,
				MENU_VI_TITLE,
				MENU_EN_TITLE,
				MENU_LINK_URL,
				MENU_IMAGE_URL,
				BROWSER_NAVIGATE,
				IS_PUBLIC,
				ACCESS_LEVEL
			)
			values
			(
				@_MENU_ID,
				@_PARENT_MENU_ID,
				@_MENU_TYPE_ID,
				@_MENU_VI_TITLE,
				@_MENU_EN_TITLE,
				@_MENU_LINK_URL,
				@_MENU_IMAGE_URL,
				@_BROWSER_NAVIGATE,
				@_IS_PUBLIC,
				@_ACCESS_LEVEL
			)	
	     end
	  end
END
GO

if exists (select * from sysobjects where id = object_id('dbo.sp_LEGOWEB_MENUS_REMOVE') and sysstat & 0xf = 4)   drop procedure dbo.sp_LEGOWEB_MENUS_REMOVE    
GO
CREATE procedure sp_LEGOWEB_MENUS_REMOVE
		@_MENU_ID int
as
BEGIN
	  begin    
	     set nocount on
		 DELETE FROM LEGOWEB_MENUS WHERE MENU_ID=@_MENU_ID
		 UPDATE LEGOWEB_MENUS SET PARENT_MENU_ID=0 WHERE PARENT_MENU_ID=@_MENU_ID   	    	     
	  end
END
GO

if exists (select * from sysobjects where id = object_id('dbo.sp_LEGOWEB_MENUS_SEARCH_COUNT') and sysstat & 0xf = 4)   drop procedure dbo.sp_LEGOWEB_MENUS_SEARCH_COUNT    
GO
CREATE procedure sp_LEGOWEB_MENUS_SEARCH_COUNT
		@_MENU_ID int = NULL,
		@_PARENT_MENU_ID int = NULL,
		@_MENU_TYPE_ID int=NULL
as
BEGIN
	WITH hierarchy
	AS
	(
	SELECT MENU_ID
	FROM LEGOWEB_MENUS
	WHERE ((@_MENU_ID IS NULL AND ((@_PARENT_MENU_ID IS NULL AND ((@_MENU_TYPE_ID IS NULL) OR (MENU_TYPE_ID=@_MENU_TYPE_ID AND PARENT_MENU_ID=0))) OR (PARENT_MENU_ID=@_PARENT_MENU_ID))) OR (MENU_ID=@_MENU_ID))		 		  		   	
	UNION ALL
	SELECT c.MENU_ID
	FROM LEGOWEB_MENUS AS c INNER JOIN hierarchy AS p ON c.PARENT_MENU_ID = p.MENU_ID
	)
	SELECT COUNT( DISTINCT MENU_ID)
	FROM hierarchy
END
GO

if exists (select * from sysobjects where id = object_id('dbo.sp_LEGOWEB_MENUS_SEARCH_PAGE') and sysstat & 0xf = 4)   drop procedure dbo.sp_LEGOWEB_MENUS_SEARCH_PAGE    
GO
CREATE procedure sp_LEGOWEB_MENUS_SEARCH_PAGE
		@_MENU_ID int = NULL,
		@_PARENT_MENU_ID int =NULL,
		@_MENU_TYPE_ID int=NULL,
		@_SELECT_ROW_NUMBER int=10,
		@_TAB_CHARS nvarchar(20)=N' '
as
BEGIN
	WITH hierarchy
	AS
	(
	SELECT MENU_ID, MENU_TYPE_ID, PARENT_MENU_ID, MENU_VI_TITLE,MENU_EN_TITLE,IS_PUBLIC,MENU_LINK_URL, 0 AS Lv, CAST('\' + LTRIM(MENU_ID) + '\' AS VARCHAR(MAX)) AS DIRECTION_PATH, CAST('\' + right(replicate('0', 2) + convert(varchar(10), ORDER_NUMBER), 2) + '\' AS VARCHAR(MAX)) AS ORDER_PATH,ORDER_NUMBER
	FROM LEGOWEB_MENUS
	WHERE ((@_MENU_ID IS NULL AND ((@_PARENT_MENU_ID IS NULL AND ((@_MENU_TYPE_ID IS NULL) OR (MENU_TYPE_ID=@_MENU_TYPE_ID AND PARENT_MENU_ID NOT IN(SELECT MENU_ID FROM LEGOWEB_MENUS WHERE MENU_TYPE_ID=@_MENU_TYPE_ID)))) OR (PARENT_MENU_ID=@_PARENT_MENU_ID))) OR (MENU_ID=@_MENU_ID))
	UNION ALL
	SELECT c.MENU_ID,c.MENU_TYPE_ID, c.PARENT_MENU_ID, c.MENU_VI_TITLE,c.MENU_EN_TITLE,c.IS_PUBLIC,c.MENU_LINK_URL,p.Lv + 1 AS Lv, p.DIRECTION_PATH + CAST(LTRIM(c.MENU_ID) + '\' AS VARCHAR(MAX)) AS DIRECTION_PATH,p.ORDER_PATH + CAST(right(replicate('0', 2) + convert(varchar(10), c.ORDER_NUMBER), 2) + '\' AS VARCHAR(MAX)) AS ORDER_PATH,c.ORDER_NUMBER
	FROM LEGOWEB_MENUS AS c INNER JOIN hierarchy AS p ON c.PARENT_MENU_ID = p.MENU_ID AND c.MENU_TYPE_ID=p.MENU_TYPE_ID
	)
	SELECT DISTINCT TOP(@_SELECT_ROW_NUMBER) hierarchy.MENU_ID,hierarchy.PARENT_MENU_ID,hierarchy.MENU_TYPE_ID,((REPLICATE(@_TAB_CHARS, Lv * 1)) + hierarchy.MENU_VI_TITLE) AS MENU_VI_TITLE,((REPLICATE(@_TAB_CHARS, Lv * 1)) + hierarchy.MENU_EN_TITLE) AS MENU_EN_TITLE,hierarchy.IS_PUBLIC, hierarchy.MENU_LINK_URL,hierarchy.DIRECTION_PATH,hierarchy.ORDER_PATH,hierarchy.ORDER_NUMBER,LEGOWEB_MENU_TYPES.MENU_TYPE_VI_TITLE
	FROM hierarchy INNER JOIN LEGOWEB_MENU_TYPES ON hierarchy.MENU_TYPE_ID= LEGOWEB_MENU_TYPES.MENU_TYPE_ID ORDER BY ORDER_PATH ASC
	
END
GO

--LEGOWEB_COMMON_PARAMETERS

if exists (select * from sysobjects where id = object_id('dbo.sp_LEGOWEB_COMMON_PARAMETERS_ADDUDP') and sysstat & 0xf = 4)  drop procedure dbo.sp_LEGOWEB_COMMON_PARAMETERS_ADDUDP

GO

CREATE PROCEDURE sp_LEGOWEB_COMMON_PARAMETERS_ADDUDP
		(
			@_PARAMETER_NAME nvarchar(50),
			@_PARAMETER_TYPE smallint = 3,
			@_PARAMETER_VI_VALUE nvarchar(255)=NULL,
			@_PARAMETER_EN_VALUE nvarchar(255)=NULL,
			@_PARAMETER_DESCRIPTION nvarchar(255)=NULL
		)    
	  AS    
	  begin    
	     set nocount on    	     
	     if exists (select * from LEGOWEB_COMMON_PARAMETERS WHERE PARAMETER_NAME=@_PARAMETER_NAME)    
	     begin    
			update LEGOWEB_COMMON_PARAMETERS 
			set 
				PARAMETER_TYPE	= @_PARAMETER_TYPE,
				PARAMETER_VI_VALUE = @_PARAMETER_VI_VALUE, 
				PARAMETER_EN_VALUE = @_PARAMETER_EN_VALUE, 
				PARAMETER_DESCRIPTION = @_PARAMETER_DESCRIPTION
			WHERE PARAMETER_NAME = @_PARAMETER_NAME
	     end    
	     else    
	     begin    
			insert 
			LEGOWEB_COMMON_PARAMETERS (PARAMETER_NAME,PARAMETER_TYPE,PARAMETER_VI_VALUE,PARAMETER_EN_VALUE,PARAMETER_DESCRIPTION) 
			VALUES(@_PARAMETER_NAME,@_PARAMETER_TYPE,@_PARAMETER_VI_VALUE,@_PARAMETER_EN_VALUE,@_PARAMETER_DESCRIPTION)
	     end
	  end
GO


--USE LegoWebDb
--EXEC sp_fulltext_database 'enable'
--CREATE UNIQUE INDEX PK_FulltextKey ON LEGOWEB_META_CONTENT_NTEXTS(META_CONTENT_NTEXT_ID);
----GO
----CREATE FULLTEXT CATALOG metacontentntexts_catalog;
--GO
--CREATE FULLTEXT INDEX ON LEGOWEB_META_CONTENT_NTEXTS
--  ( 
--	  SUBFIELD_VALUE	
--  )
--  KEY INDEX PK_FulltextKey
--      ON metacontentntexts_catalog
--      WITH CHANGE_TRACKING AUTO;
--   GO





