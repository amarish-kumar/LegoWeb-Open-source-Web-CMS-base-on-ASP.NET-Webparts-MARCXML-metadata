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

public partial class Webparts_PhotoSlider: WebPartBase
{
    private int _meta_content_id;
    public Webparts_PhotoSlider()
    {
        this.Title = "PhotoSlide - Trình diễn tập ảnh";
    }

    [Personalizable]
    [WebBrowsable]
    /// <summary>
    /// Set Content Category
    /// </summary>
    public int meta_content_id
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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
<style type='text/css'> 
body{
	font-family:arial
}
 
.clear {
	clear:both
}
 
#gallery {
	position:relative;
	height:360px
}
	#gallery a {
		float:left;
		position:absolute;
	}
	
	#gallery a img {
		border:none;
	}
	
	#gallery a.show {
		z-index:500
	}
 
	#gallery .caption {
		z-index:600; 
		background-color:#000; 
		color:#ffffff; 
		height:100px; 
		width:100%; 
		position:absolute;
		bottom:0;
	}
 
	#gallery .caption .content {
		margin:5px
	}
	
	#gallery .caption .content h3 {
		margin:0;
		padding:0;
		color:#1DCCEF;
	}
	
 
</style>

";
            int contentid = _meta_content_id;
            if ( contentid== 0 && CommonUtility.GetInitialValue("contentid",null)!=null)
            {
                contentid = int.Parse(CommonUtility.GetInitialValue("contentid", null).ToString());
            }
            if (!LegoWebSite.Buslgic.MetaContents.is_META_CONTENTS_EXIST(contentid))
            {
                divSlideShow.InnerHtml = "<H3>Dữ liệu không tồn tại</H3>";
                return;
            }
            CRecord myRec = new CRecord();
            string sMetaXml = LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_MARCXML(contentid,false);
            myRec.load_Xml(sMetaXml);
            CDatafields Dfs = myRec.Datafields;
            Dfs.Filter("856");
            if (Dfs.Count == 0)
            {
                divSlideShow.InnerHtml = "<H3>Không có dữ liệu ảnh</H3>";
                return;
            }
            else
            {
                string sSlider = "<div id='gallery'>";
                string aFormat="<a href='{0}' {1}> <img src='{2}' alt='{3}' width='{4}' height='{5}' rel='<h3>{3}</h3>{6}'/></a>";
                for (int i = 0; i < Dfs.Count; i++)
                {
                    CDatafield Df=Dfs.Datafield(i);
                    if (i == 0)
                    {
                        sSlider += String.Format(aFormat, String.IsNullOrEmpty(Df.Subfields.Subfield("w").Value) ? "#" : Df.Subfields.Subfield("w").Value, " class='show'", Df.Subfields.Subfield("u").Value, Df.Subfields.Subfield("3").Value, Df.Subfields.Subfield("r").Value, Df.Subfields.Subfield("c").Value, Df.Subfields.Subfield("a").Value);
                    }
                    else
                    {
                        sSlider += String.Format(aFormat, String.IsNullOrEmpty(Df.Subfields.Subfield("w").Value) ? "#" : Df.Subfields.Subfield("w").Value, "", Df.Subfields.Subfield("u").Value, Df.Subfields.Subfield("3").Value, Df.Subfields.Subfield("r").Value, Df.Subfields.Subfield("c").Value, Df.Subfields.Subfield("a").Value);
                    }
                    
                }
                sSlider += "<div class='caption'><div class='content'></div></div>";
                sSlider += "</div";
                divSlideShow.InnerHtml =sliderShowScript + sSlider;
            }

        }
    }

}
