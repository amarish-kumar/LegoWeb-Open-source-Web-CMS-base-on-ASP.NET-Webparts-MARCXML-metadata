// ----------------------------------------------------------------------
// <copyright file="MenuTypeDataProvider.cs" package="LEGOWEB">
//     Copyright (C) 2010-2011 HIENDAI SOFTWARE COMPANY. All rights reserved.
//     www.legoweb.org
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
    /// Summary description for MenuTypeDataProvider
    /// </summary>
    public class MenuTypeDataProvider
    {
        public int RecordCount = 0;
        public int PageCount = 0;
        public int PageNumber = 0;
        public int RecordsPerPage = 10;
        public DataTable Data = null;
        public MenuTypeDataProvider()
        {
            //
            // TODO: Add constructor logic here
            //
        }
       
        public int get_Search_Count(out int outPageCount)
        {
            try
            {
                RecordCount = LegoWebAdmin.BusLogic.MenuTypes.get_Search_Count();
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

        public DataTable get_Search_Current_Page()
        {
            try
            {
                DataSet retData;
                int iPos = RecordsPerPage * (PageNumber - 1) + 1;
                retData = LegoWebAdmin.BusLogic.MenuTypes.get_Search_Page(PageNumber, RecordsPerPage);
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