﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using LegoWebSite.Buslgic;
using MarcXmlParserEx;

namespace LegoWebSite.DataProvider
{
    /// <summary>
    /// Summary description for SectionDataProvider
    /// </summary>
    public class ContentSearchDataProvider: IDisposable
    {
        public int RecordCount = 0;
        public int PageCount = 0;
        public int PageNumber = 1;
        public int RecordsPerPage = 10;
        public DataTable Data = null;
        public ContentSearchDataProvider()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int get_User_Search_Count(out int outPageCount, int iSearchSectionId, string sSearchField, string sSearchValue)
        {
            try
            {
                RecordCount = LegoWebSite.Buslgic.MetaContents.get_User_Search_Count(iSearchSectionId, sSearchField, sSearchValue);

                PageCount = RecordCount / RecordsPerPage;
                if (RecordCount % RecordsPerPage > 0)
                {
                    PageCount++;
                }
                outPageCount = PageCount;
                return RecordCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable get_User_Search_Current_Page(int iSearchSectionId, string sSearchField, string sSearchValue)
        {
            try
            {
                DataSet retData;
            
                retData = LegoWebSite.Buslgic.MetaContents.get_User_Search_Page(iSearchSectionId,sSearchField,sSearchValue, PageNumber, RecordsPerPage);
                Data = retData.Tables[0];
              
                return Data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Dispose()
        {
            // Use SupressFinalize in case a subclass
            // of this type implements a finalizer.
            GC.SuppressFinalize(this);
        }
    }
}