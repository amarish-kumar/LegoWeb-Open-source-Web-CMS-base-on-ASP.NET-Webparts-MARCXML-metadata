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
    public class SectionDataProvider
    {
        public int RecordCount = 0;
        public int PageCount = 0;
        public int PageNumber = 0;
        public int RecordsPerPage = 10;
        public DataTable Data = null;
        public SectionDataProvider()
        {
            //
            // TODO: Add constructor logic here
            //
        }
       
        public int get_Search_Count(out int outPageCount)
        {
            try
            {
                RecordCount = LegoWeb.BusLogic.Sections.get_Search_Count();
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
                retData = LegoWeb.BusLogic.Sections.get_Search_Page(PageNumber, RecordsPerPage);
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