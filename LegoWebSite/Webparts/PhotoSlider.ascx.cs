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
using MarcXmlParserEx;

public partial class Webparts_PHOTOSLIDER: WebPartBase
{
    private int _meta_content_id = 0;
    private int _category_id = 0;    
    private string _box_css_name = null;//mean no box around

    public Webparts_PHOTOSLIDER()
    {
        this.Title = "PHOTO SLIDE SHOW";
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

    [Personalizable]
    [WebBrowsable]
    [WebDisplayName("2.Meta content id:")]
    [WebDescription("Set meta content id to display, if not select top one of category set below.")]
    /// <summary>
    /// meta_content_id: id of metat content record, if set display specified record
    /// </summary>
    public int p2_meta_content_id
    {
        get
        {
            return _meta_content_id;
        }
        set
        {
            _meta_content_id = value;
        }
    }

    [Personalizable]
    [WebBrowsable]
    [WebDisplayName("3.Category id:")]
    [WebDescription("Select the category to get top 1 content record of.")]
    /// <summary>
    /// category_id: if meta_content_id not set, auto detech 1 last update meta content record in category_id
    /// </summary>
    public int p3_category_id
    {
        get
        {
            return _category_id;
        }
        set
        {
            _category_id = value;
        }
    }

    #endregion webparts properties

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

            string sliderShowScript = @"
                    <script type='text/javascript'> 
                     
                    $(document).ready(function() {		
                    	
	                    //Execute the slideShow
	                    slideShow();
                     
                    });
                     
                    function slideShow() {
                     
	                    //Set the opacity of all images to 0
	                    $('#gallery a').css({opacity: 0.0});
                    	
	                    //Get the first image and display it (set it to full opacity)
	                    $('#gallery a:first').css({opacity: 1.0});
                    	
	                    //Set the caption background to semi-transparent
	                    $('#gallery .caption').css({opacity: 0.7});
                     
	                    //Resize the width of the caption according to the image width
	                    $('#gallery .caption').css({width: $('#gallery a').find('img').css('width')});
                    	
	                    //Get the caption of the first image from REL attribute and display it
	                    $('#gallery .content').html($('#gallery a:first').find('img').attr('rel'))
	                    .animate({opacity: 0.7}, 400);
                    	
	                    //Call the gallery function to run the slideshow, 6000 = change to next image after 6 seconds
	                    setInterval('gallery()',6000);
                    	
                    }
                     
                    function gallery() {
                    	
	                    //if no IMGs have the show class, grab the first image
	                    var current = ($('#gallery a.show')?  $('#gallery a.show') : $('#gallery a:first'));
                     
	                    //Get next image, if it reached the end of the slideshow, rotate it back to the first image
	                    var next = ((current.next().length) ? ((current.next().hasClass('caption'))? $('#gallery a:first') :current.next()) : $('#gallery a:first'));	
                    	
	                    //Get next image caption
	                    var caption = next.find('img').attr('rel');	
                    	
	                    //Set the fade in effect for the next image, show class has higher z-index
	                    next.css({opacity: 0.0})
	                    .addClass('show')
	                    .animate({opacity: 1.0}, 1000);
                     
	                    //Hide the current image
	                    current.animate({opacity: 0.0}, 1000)
	                    .removeClass('show');
                    	
	                    //Set the opacity to 0 and height to 1px
	                    $('#gallery .caption').animate({opacity: 0.0}, { queue:false, duration:0 }).animate({height: '1px'}, { queue:true, duration:300 });	
                    	
	                    //Animate the caption, opacity to 0.7 and heigth to 100px, a slide up effect
	                    $('#gallery .caption').animate({opacity: 0.7},100 ).animate({height: '100px'},500 );
                    	
	                    //Display the content
	                    $('#gallery .content').html(caption);                    	                    	
                    }
                     
                    </script>
               ";

            int contentid=discover_content_id();

            if ((contentid<=0) || (contentid> 0 && !LegoWebSite.Buslgic.MetaContents.is_META_CONTENTS_EXIST(contentid)))
            {
                this.litContent.Text = "<H3>No suitable data!</H3>";
                return;
            }      

            CRecord myRec = new CRecord();
            string sMetaXml = LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_MARCXML(contentid,0);
            myRec.load_Xml(sMetaXml);

            string sdefaultHeight = myRec.Datafields.Datafield("300").Subfields.Subfield("h").Value;
            string sdefaultWidth = myRec.Datafields.Datafield("300").Subfields.Subfield("w").Value;

            CDatafields Dfs = myRec.Datafields;
            Dfs.Filter("856");
            if (Dfs.Count == 0)
            {

                this.litContent.Text = "<H3>No image info</H3>";
                return;
            }
            else
            {
                string sSliderHTML = "<div id='gallery'>";
                string aFormat="<a href='{0}' {1}> <img src='{2}' alt='{3}' width='{4}' height='{5}' rel='<h3>{3}</h3>{6}'/></a>";
                for (int i = 0; i < Dfs.Count; i++)
                {
                    CDatafield Df=Dfs.Datafield(i);
                    if (i == 0)
                    {
                        sSliderHTML += String.Format(aFormat, String.IsNullOrEmpty(Df.Subfields.Subfield("l").Value) ? "#" : Df.Subfields.Subfield("l").Value, " class='show'", Df.Subfields.Subfield("u").Value, Df.Subfields.Subfield("3").Value, String.IsNullOrEmpty(Df.Subfields.Subfield("w").Value) == true ? sdefaultWidth : Df.Subfields.Subfield("w").Value, String.IsNullOrEmpty(Df.Subfields.Subfield("h").Value) == true ? sdefaultHeight: Df.Subfields.Subfield("h").Value, Df.Subfields.Subfield("a").Value);
                    }
                    else
                    {
                        sSliderHTML += String.Format(aFormat, String.IsNullOrEmpty(Df.Subfields.Subfield("l").Value) ? "#" : Df.Subfields.Subfield("l").Value, "", Df.Subfields.Subfield("u").Value, Df.Subfields.Subfield("3").Value, String.IsNullOrEmpty(Df.Subfields.Subfield("w").Value) == true ? sdefaultWidth : Df.Subfields.Subfield("w").Value, String.IsNullOrEmpty(Df.Subfields.Subfield("h").Value) == true ? sdefaultHeight: Df.Subfields.Subfield("h").Value, Df.Subfields.Subfield("a").Value);
                    }
                    
                }
                sSliderHTML += "<div class='caption'><div class='content'></div></div>";
                sSliderHTML += "</div";
                this.litContent.Text =sSliderHTML;
            }
            Page.RegisterStartupScript("slidershowscript", sliderShowScript);
        }
    }

    private int discover_content_id()
    {
        int contentid = 0;
        int categoryid = 0;
        int menuid = 0;
        if (_meta_content_id == 0)
        {
            if (_category_id == 0)
            {
                if (CommonUtility.GetInitialValue("catid", null) != null)
                {
                    categoryid = int.Parse(CommonUtility.GetInitialValue("catid", null).ToString());
                }
                else if (CommonUtility.GetInitialValue("mnuid", null) != null)
                {
                    menuid = int.Parse(CommonUtility.GetInitialValue("mnuid", 0).ToString());
                    categoryid = LegoWebSite.Buslgic.Categories.get_CATEGORY_ID_BY_MENU_ID(menuid);
                }
            }
            else
            {
                categoryid = _category_id;
            }

            //try to discover contentid            
            if (categoryid > 0)
            {
                DataTable top1Data = LegoWebSite.Buslgic.MetaContents.get_TOP_CONTENTS_OF_CATEGORY(categoryid, 1,null, null);
                if (top1Data.Rows.Count > 0)
                {
                    contentid = (int)top1Data.Rows[0]["META_CONTENT_ID"];
                }
            }
        }
        else
        {
            contentid = _meta_content_id;
        }
        return contentid;
    }

}
