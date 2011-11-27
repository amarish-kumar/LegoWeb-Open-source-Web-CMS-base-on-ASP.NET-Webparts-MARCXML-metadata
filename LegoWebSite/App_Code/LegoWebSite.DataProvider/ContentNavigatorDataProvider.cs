using System;
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
    /// 
    /// </summary>
    public class ContentNavigatorDataProvider: IDisposable
    {
        public int RecordCount = 0;
        public int PageCount = 0;
        public int PageNumber = 1;
        public int RecordsPerPage = 10;
        public DataTable Data = null;
        public ContentNavigatorDataProvider()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int get_Result_Count(out int outPageCount, int iCategory_id, string sLang_Code)
        {
            try
            {
                RecordCount = LegoWebSite.Buslgic.MetaContents.get_Content_Navigator_Count(iCategory_id, sLang_Code);

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
        public DataTable get_Current_Page(int iCategory_id, string sLang_Code)
        {
            try
            {
                DataSet retData;

                retData = LegoWebSite.Buslgic.MetaContents.get_Content_Navigator_Page(iCategory_id,sLang_Code, PageNumber, RecordsPerPage);
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