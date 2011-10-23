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
/// Summary description for RSSFeedWebPart
/// </summary>

namespace LegoWebSite.Webparts
{
    public class RSSFeedWebPart :WebPart
    {
        private string _RSSFeedID = "RssFeed1";
        // RSS URL
        private string _RSSFeedURL = "http://vnexpress.net/rss/gl/vi-tinh.rss";
        // Record Number
        private string _NumberOfRecord = "5";
        //DisplayOptions
        private string _DisplayOptions = "datetime";

        private string _viTitle = "RSS News Feed";
        private string _enTitle = "RSS News Feed";

        public RSSFeedWebPart()
        {
            this.Title = "RSS Feed";
        }
        protected override void OnPreRender(EventArgs e)
        {
            if (HttpContext.Current.Session["lang"] != null)
            {
                switch (HttpContext.Current.Session["lang"].ToString())
                {
                    case "vi":
                        this.Title = _viTitle;
                        break;
                    case "en":
                        this.Title = _enTitle;
                        break;
                }
            }
            base.OnPreRender(e);
        }

        [Personalizable]
        [WebBrowsable]
        /// <summary>
        /// Set Content Category
        /// </summary>
        public string viTitle
        {
            get
            {
                return _viTitle;
            }
            set
            {
                _viTitle = value;
            }
        }
        [Personalizable]
        [WebBrowsable]
        /// <summary>
        /// Set Content Category
        /// </summary>
        public string enTitle
        {
            get
            {
                return _enTitle;
            }
            set
            {
                _enTitle = value;
            }
        }

        [Personalizable]
        [WebBrowsable]
        public string RSSFeedID
        {
            get { return _RSSFeedID; }
            set { _RSSFeedID = value; }
        }
        [Personalizable]
        [WebBrowsable]
        public string RSSFeedURL
        {
            get { return _RSSFeedURL; }
            set { _RSSFeedURL = value; }
        }
        [Personalizable]
        [WebBrowsable]
        public string NumberOfRecord
        {
            get { return _NumberOfRecord; }
            set { _NumberOfRecord = value; }
        }
        [Personalizable]
        [WebBrowsable]
        /// <summary>
        /// Set Record Position first, last and number
        /// </summary>
        public string DisplayOptions
        {
            get { return _DisplayOptions; }
            set { _DisplayOptions = value; }
        }

        // ************************************************************************

        // ************************************************************************
        // Gets and sets the Web part collection of connection points
        protected override void RenderContents(HtmlTextWriter writer)
        {
            string sTopBound = @"
		<div id='silver-gradient-box'>
			<div class='t'>
		 		<div class='t'>
					<div class='t'></div>
		 		</div>
			</div>
			<div class='m'>";
               
                
            string sBottomBound= @"
			        <div class='clr'></div>
			</div>
			<div class='b'>
				<div class='b'>
					<div class='b'></div>
				</div>
			</div>
   		</div>";
      
            writer.Write("<script type='text/javascript' src='http://www.google.com/jsapi?key=ABQIAAAA8stszlEBh61D_FFCx-qyyRScfF93HYE83NPQPh9y0A68FYuPPBT45OUjYf9IAp8YI-j5DTeWgfItPg'></script> ");
            writer.Write("<script type=\"text/javascript\" src=\"gfeedfetcher_vn.js\"></script>");

            string RssFeedScripts= @"
                                <div style='width:98%;height:98%;'>
                                <script type='text/javascript'>
                                var socialfeed=new gfeedfetcher('{0}', 'example2class', '_new')
                                socialfeed.addFeed('Slashdot','{1}') // 'http://vnexpress.net/rss/gl/vi-tinh.rss'
                                socialfeed.displayoptions('{2}') //show the specified additional fields
                                socialfeed.setentrycontainer('div') //Display each entry as a div
                                socialfeed.filterfeed('{3}', 'label') //Show 6 entries, sort by label
                                socialfeed.init() //Always call this last
                                </script>
                                </div>";
            writer.Write(sTopBound);
            //writer.Write("<b>củ chuối quá đi mất cứ chèn ra ngoài làm hỏng cả border</b><br>Cộng hòa xã hội chủ nghĩa Việt Nam độc lập tự do hạnh phúc muôn năm tự do muôn năm vân vân và vân vân");
            writer.Write(string.Format(RssFeedScripts,_RSSFeedID, _RSSFeedURL,_DisplayOptions, _NumberOfRecord));
            writer.Write(sBottomBound);
        }
    }
}