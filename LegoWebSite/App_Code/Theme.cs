using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


/// <summary>
/// Summary description for Theme
/// </summary>
public class Theme
{
    private string _name;
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
    public Theme(string name)
    {
        Name = name;
    }

}
