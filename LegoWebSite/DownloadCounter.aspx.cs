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

using LegoWebSite.DataProvider;
using System.IO;

public partial class DownloadCounter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (CommonUtility.GetInitialValue("urlid", null) != null)//get subfield u id
        {
            int iSfId = int.Parse(CommonUtility.GetInitialValue("urlid", null).ToString());
            if (iSfId > 0)
            {
                DataTable UrlInfo = LegoWebSite.Buslgic.MetaContentTexts.get_META_CONTENT_TEXT(iSfId);
                if (UrlInfo.Rows.Count > 0)
                {
                    DataTable CountInfo = LegoWebSite.Buslgic.MetaContentNumbers.get_META_CONTENT_NUMBER(int.Parse(UrlInfo.Rows[0]["META_CONTENT_ID"].ToString()), int.Parse(UrlInfo.Rows[0]["TAG"].ToString()), int.Parse(UrlInfo.Rows[0]["TAG_INDEX"].ToString()), "n");
                    if (CountInfo.Rows.Count > 0)
                    {
                        Decimal count=CountInfo.Rows[0]["SUBFIELD_VALUE"]!=DBNull.Value?(Decimal)CountInfo.Rows[0]["SUBFIELD_VALUE"]:0;
                        int iCount=Convert.ToInt16(count)+1;
                        //call update function
                        LegoWebSite.Buslgic.MetaContentNumbers.update_META_CONTENT_NUMBER_VALUE(int.Parse(CountInfo.Rows[0]["META_CONTENT_NUMBER_ID"].ToString()),iCount);
                    }
                }            
            }
        }
        if (CommonUtility.GetInitialValue("url", null) != null)
        {
            String sUrl =CommonUtility.GetInitialValue("url", null).ToString();
            if (!string.IsNullOrEmpty(sUrl))
            {
                sUrl = System.Web.HttpUtility.HtmlDecode(sUrl);
                Response.Redirect(sUrl);
                Response.End();
            }                
        }
    }
    protected override void OnInit(EventArgs e)
    {
        CultureUtility.SetThreadCulture();
        base.OnInit(e);
    }
}
