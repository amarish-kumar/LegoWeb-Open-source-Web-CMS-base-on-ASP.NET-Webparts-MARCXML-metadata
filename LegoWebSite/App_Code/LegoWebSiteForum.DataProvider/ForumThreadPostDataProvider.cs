using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace LegoWebSiteForum.DataProvider
{
    /// <summary>
    /// Summary description for SectionDataProvider
    /// </summary>
    public class ForumThreadPostDataProvider : IDisposable
    {
        public int RecordCount = 0;
        public int PageCount = 0;
        public int PageNumber = 1;
        public int RecordsPerPage = 10;
        public DataTable Data = null;
        public ForumThreadPostDataProvider()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int get_Thread_Posts_Page_Count(out int outPageCount, int iThreadId)
        {
            try
            {
                RecordCount = LegoWebSiteForum.Buslogic.Forum.GetThreadPostsCount(iThreadId);
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
        public DataTable get_Thread_Posts_Page(int iThreadId, ForumUtils.ForumView forumView)
        {
            try
            {
                DataTable retData;
                retData = LegoWebSiteForum.Buslogic.Forum.GetThread(iThreadId, PageNumber - 1, RecordsPerPage, forumView);
                return retData;
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