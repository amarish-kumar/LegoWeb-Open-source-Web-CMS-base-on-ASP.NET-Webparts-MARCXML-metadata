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
/// Summary description for RSSFeed
/// </summary>

namespace LegoWebSite.Webparts
{
    public class GGRSSNEWSFEED : WebPart
    {
        private string _box_css_name = null;//mean no box around
        private string _rss_news_feed_id = "RssNewsFeed1";
        private string _rss_news_feed_url = "http://vnexpress.net/rss/gl/vi-tinh.rss";
        private int _number_of_record = 5;
        private string _display_option = "datetime";

        public GGRSSNEWSFEED()
        {
            this.Title = "GADGET RSS NEWS FEED";
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
        [WebDisplayName("2.Rss id:")]
        [WebDescription("Set id for javascript.")]
        /// <summary>
        /// Set id for javascript avoid conflict if have multi webparts in one page
        /// </summary>
        public string p1_rss_news_feed_id
        {
            get
            {
                return _rss_news_feed_id;
            }
            set
            {
                _rss_news_feed_id = value;
            }
        }
        [Personalizable]
        [WebBrowsable]
        [WebDisplayName("3.Rss source url:")]
        [WebDescription("Set id for javascript.")]
        /// <summary>
        /// Set id for javascript avoid conflict if have multi webparts in one page
        /// </summary>
        public string p3_rss_news_feed_url
        {
            get
            {
                return _rss_news_feed_url;
            }
            set
            {
                _rss_news_feed_url = value;
            }
        }

        [Personalizable]
        [WebBrowsable]
        [WebDisplayName("4.Number of records:")]
        [WebDescription("Set number of records to get")]
        /// <summary>
        /// Set number of all records to get and display
        /// </summary>
        public int p4_number_of_record
        {
            get
            {
                return _number_of_record;
            }
            set
            {
                _number_of_record = value;
            }
        }

        [Personalizable]
        [WebBrowsable]
        [WebDisplayName("5.Display option:")]
        [WebDescription("Set display option")]
        /// <summary>
        /// Set display option 
        /// </summary>
        public string p5_display_option
        {
            get
            {
                return _display_option;
            }
            set
            {
                _display_option = value;
            }
        }
        #endregion webparts properties

        // ************************************************************************

        // ************************************************************************
        // Gets and sets the Web part collection of connection points
        protected override void RenderContents(HtmlTextWriter writer)
        {
            string sBoxTop = "";
            string sBoxBottom = "";
            if (!String.IsNullOrEmpty(_box_css_name))
            {
                if (_box_css_name.IndexOf("-title-") > 0)
                {
                    sBoxTop = String.Format("<div id=\"{0}\"><div class=\"t\"><div class=\"t\"><div class=\"t\"></div></div></div><div class=\"title\">{1}</div><div class=\"m\"><div class=\"clearfix\">", _box_css_name, LegoWebSite.Buslgic.CommonParameters.asign_COMMON_PARAMETER(this.Title));
                    sBoxBottom = "</div><div class=\"clr\"></div></div><div class=\"b\"><div class=\"b\"><div class=\"b\"></div></div></div></div>";
                }
                else
                {
                    sBoxTop = String.Format("<div id=\"{0}\"><div class=\"t\"><div class=\"t\"><div class=\"t\"></div></div></div><div class=\"m\"><div class=\"clearfix\">", _box_css_name);
                    sBoxBottom = "</div><div class=\"clr\"></div></div><div class=\"b\"><div class=\"b\"><div class=\"b\"></div></div></div></div>";
                }
            }
      
            writer.Write("<script type='text/javascript' src='http://www.google.com/jsapi?key=ABQIAAAA8stszlEBh61D_FFCx-qyyRScfF93HYE83NPQPh9y0A68FYuPPBT45OUjYf9IAp8YI-j5DTeWgfItPg'></script> ");
            writer.Write("<script type=\"text/javascript\" src=\"js/gfeedfetcher_vn.js\"></script>");

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
            writer.Write(sBoxTop);
            writer.Write(string.Format(RssFeedScripts, _rss_news_feed_id, _rss_news_feed_url, _display_option, _number_of_record));
            writer.Write(sBoxBottom);
        }
    }
}