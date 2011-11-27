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
/// <summary>
/// Để chạy được webpart thời tiết chứng khoán này ta cần phải tham chiếu tới một số file sau
/// - File weather.css để styles cho webpart
/// - File imagesweather chứa các ảnh
/// - Cop file AjaxControlToolkit.dll vào trong thư mục Bin mục đích là để khi selectedindexchange dropdowlist
///   tỉnh thành phố thì sẽ ko bị load lại trang
/// </summary>
public partial class Webparts_GGVNEXPSECURITIES : WebPartBase
{
    private string _box_css_name = null;//mean no box around

    public Webparts_GGVNEXPSECURITIES()
    {
        this.Title = "GADGET VNEXPRESS SECURITIES";
    }
    
    #region webparts properties
    [Personalizable]
    [WebBrowsable]
    [WebDisplayName("1.Box css class name:")]
    [WebDescription("Set css class name of container box.")]
    /// <summary>
    /// set box css name to set box container if contains -title- then title of box will auto set
    /// </summary>
    public string p1_box_css_name
    {
        get
        {
            return _box_css_name;
        }
        set
        {
            _box_css_name = value;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(_box_css_name))
            {
                if (_box_css_name.IndexOf("-title-") > 0)
                {
                    string sBoxTop = String.Format("<div id=\"{0}\"><div class=\"t\"><div class=\"t\"><div class=\"t\"></div></div></div><div class=\"title\">{1}</div><div class=\"m\"><div class=\"clearfix\">", _box_css_name, LegoWebSite.Buslgic.CommonParameters.asign_COMMON_PARAMETER(this.Title));
                    string sBoxBottom = "</div><div class=\"clr\"></div></div><div class=\"b\"><div class=\"b\"><div class=\"b\"></div></div></div></div>";
                    this.litBoxTop.Text = sBoxTop;
                    this.litBoxBottom.Text = sBoxBottom;
                }
                else
                {
                    string sBoxTop = String.Format("<div id=\"{0}\"><div class=\"t\"><div class=\"t\"><div class=\"t\"></div></div></div><div class=\"m\"><div class=\"clearfix\">", _box_css_name);
                    string sBoxBottom = "</div><div class=\"clr\"></div></div><div class=\"b\"><div class=\"b\"><div class=\"b\"></div></div></div></div>";
                    this.litBoxTop.Text = sBoxTop;
                    this.litBoxBottom.Text = sBoxBottom;
                }

                string scriptLoadStockData = @"<script>
                                        document.onload = loadStockIframes();
                                     </script>";
                Page.RegisterStartupScript("scriptLoadStockData", scriptLoadStockData);

            }

        }
    }
}
