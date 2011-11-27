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
public partial class Webparts_GGVNEXPWEATHER : WebPartBase
{

    private string _box_css_name = null;//mean no box around

    public Webparts_GGVNEXPWEATHER()
    {
        this.Title = "GADGET VNEXPRESS WEATHER";
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
            } 
            
            try
            {
                //Load dữ liệu từ file hanoi.xml
                LoadData("http://vnexpress.net/ListFile/Weather/hanoi.xml");

            }
            catch { }
        }
    }
    // Hàm bind dữ liệu xml ra DataTable
    private DataTable GetTable(string FileName)
    {
        DataTable dtb = new DataTable();
        DataSet authorsDataSet;
        string filePath = FileName;
        authorsDataSet = new DataSet();
        authorsDataSet.ReadXml(filePath);
        dtb = authorsDataSet.Tables[0];
        return dtb;
    }
    // Load dữ liệu theo filepath
    private void LoadData(string xmlFilePath)
    {
        DataTable dtb = GetTable(xmlFilePath);
        if (dtb.Rows.Count > 0)
        {
            litTemperature.Text = "&nbsp;<img src='http://vnexpress.net/Images/Weather/" + dtb.Rows[0][0].ToString().Trim() + "'>";
            litTemperature.Text += "<img src='http://vnexpress.net/Images/Weather/" + dtb.Rows[0][1].ToString().Trim() + "'>";
            litTemperature.Text += "<img src='http://vnexpress.net/Images/Weather/" + dtb.Rows[0][2].ToString().Trim() + "'>";
            litTemperature.Text += "<img src='http://vnexpress.net/Images/Weather/c.gif'>";
            litTemperature.Text += "<br>" + dtb.Rows[0][6].ToString().Trim();
        }
        dtb.Dispose();
    }
    protected void dropCities_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData("http://vnexpress.net/ListFile/Weather/" + dropCities.SelectedValue.ToString());
    }
}
