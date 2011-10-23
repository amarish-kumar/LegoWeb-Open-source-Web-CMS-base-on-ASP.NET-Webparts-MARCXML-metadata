using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using LegoWeb.BusLogic;

namespace LegoWeb.DataProvider
{
    /// <summary>
    /// Summary description for MenuTypeDataProvider
    /// </summary>
    public class MenuDataProvider: IDisposable
    {
        public int RecordCount = 0;
        public int PageCount = 0;
        public int PageNumber = 0;
        public int RecordsPerPage = 10;
        public DataTable Data = null;
        public MenuDataProvider()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int get_Search_Count(out int outPageCount, int iMenuId, int iParentMenuId, int iMenuTypeId)
        {
            try
            {
                RecordCount = LegoWeb.BusLogic.Menus.get_Search_Count(iMenuId,iParentMenuId,iMenuTypeId);
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

        public DataTable get_Search_Current_Page(int iMenuId, int iParentMenuId, int iMenuTypeId, string sTabChars)
        {
            try
            {
                DataSet retData;
                int iPos = RecordsPerPage * (PageNumber - 1) + 1;
                retData = LegoWeb.BusLogic.Menus.get_Search_Page(iMenuId, iParentMenuId, iMenuTypeId, sTabChars, PageNumber, RecordsPerPage);
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