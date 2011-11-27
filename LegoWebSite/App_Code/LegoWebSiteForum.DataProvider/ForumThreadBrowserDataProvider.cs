using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace LegoWebSiteForum.DataProvider
{
    /// <summary>
    /// Summary description for SectionDataProvider
    /// </summary>
    public class ForumThreadBrowserDataProvider : IDisposable
    {
        public int RecordCount = 0;
        public int PageCount = 0;
        public int PageNumber = 1;
        public int RecordsPerPage = 10;
        public DataTable Data = null;
        public ForumThreadBrowserDataProvider()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int get_Threads_Browse_Page_Count(out int outPageCount, int iForumId)
        {
            try
            {
                RecordCount = LegoWebSiteForum.Buslogic.ForumThreadBrowser.get_Threads_Browse_Count(iForumId);
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
        public DataTable get_Threads_Browse_Page(int iForumId)
        {
            try
            {
                DataSet retData;

                retData = LegoWebSiteForum.Buslogic.ForumThreadBrowser.get_Threads_Browse_Page(iForumId, PageNumber, RecordsPerPage);
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