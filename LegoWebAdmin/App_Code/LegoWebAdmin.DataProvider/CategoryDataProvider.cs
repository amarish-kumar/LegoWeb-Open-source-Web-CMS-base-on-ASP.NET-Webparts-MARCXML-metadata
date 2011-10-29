// ----------------------------------------------------------------------
// <copyright file="CategoryDataProvider.cs" company="HIENDAI SOFTWARE COMPANY">
//     Copyright (C) 2010-2011 HIENDAI SOFTWARE COMPANY. All rights reserved.
//     License: GNU/GPL
//     LEGOWEB IS FREE SOFTWARE
// </copyright>
// ------------------------------------------------------------------------
using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using LegoWebAdmin.BusLogic;

namespace LegoWebAdmin.DataProvider
{
    /// <summary>
    /// Summary description for SectionDataProvider
    /// </summary>
    public class CategoryDataProvider: IDisposable
    {
        public int RecordCount = 0;
        public int PageCount = 0;
        public int PageNumber = 0;
        public int RecordsPerPage = 10;
        public DataTable Data = null;
        public CategoryDataProvider()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int get_Search_Count(out int outPageCount, int iCategoryId, int iParentCategoryId, int iSectionId)
        {
            try
            {
                RecordCount = LegoWebAdmin.BusLogic.Categories.get_Search_Count(iCategoryId,iParentCategoryId,iSectionId);
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

        public DataTable get_Search_Current_Page(int iCategoryId, int iParentCategoryId, int iSectionId, string sTabChars)
        {
            try
            {
                DataSet retData;
                int iPos = RecordsPerPage * (PageNumber - 1) + 1;
                retData = LegoWebAdmin.BusLogic.Categories.get_Search_Page(iCategoryId, iParentCategoryId, iSectionId, sTabChars, PageNumber, RecordsPerPage);
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