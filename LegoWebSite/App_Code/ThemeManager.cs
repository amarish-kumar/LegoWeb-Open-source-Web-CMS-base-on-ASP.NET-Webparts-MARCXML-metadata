using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Collections.Generic;
/// <summary>
/// Summary description for ThemeManager
/// </summary>
public class ThemeManager
{
    #region Theme-Related Method
    public static List<Theme> GetThemes()
    {
        DirectoryInfo dInfo = new DirectoryInfo(
          System.Web.HttpContext.Current.Server.MapPath("App_Themes"));
        DirectoryInfo[] dArrInfo = dInfo.GetDirectories();
        List<Theme> list = new List<Theme>();
        foreach (DirectoryInfo sDirectory in dArrInfo)
        {
            Theme temp = new Theme(sDirectory.Name);
            list.Add(temp);
        }
        return list;
    }
    #endregion

}
