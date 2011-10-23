using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using LegoWeb.BusLogic;

namespace LegoWeb.DataProvider
{
    /// <summary>
    /// Summary description for SectionDataProvider
    /// </summary>
    public class MetaContentDataProvider: IDisposable
    {
        public int RecordCount = 0;
        public int PageCount = 0;
        public int PageNumber = 0;
        public int RecordsPerPage = 10;
        public DataTable Data = null;
        public MetaContentDataProvider()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int get_Admin_Search_Count(out int outPageCount,int iSectionId,int iRootCategoryId)
        {
            try
            {
                RecordCount = LegoWeb.BusLogic.MetaContents.get_Admin_Search_Count(iSectionId,iRootCategoryId);
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

        public DataTable get_Admin_Search_Current_Page(int iSectionId, int iRootCategoryId)
        {
            try
            {
                DataSet retData;
                int iPos = RecordsPerPage * (PageNumber - 1) + 1;
                retData = LegoWeb.BusLogic.MetaContents.get_Admin_Search_Page(iSectionId,iRootCategoryId, PageNumber, RecordsPerPage);
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