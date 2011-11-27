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


public partial class Webparts_POLL:WebPartBase
{
    private int _meta_content_id = 0;
    private int _category_id = 0;
    private string _box_css_name = null;//mean no box around
    private Random random = new Random();
     public Webparts_POLL()
    {
        this.Title = "POLL";
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
     [WebDisplayName("2.Poll meta content id:")]
     [WebDescription("Set poll data meta content id to display, if not select top one of category set below.")]
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
     [WebDisplayName("3.Poll category id:")]
     [WebDescription("Select the category to get top 1 poll content record of.")]
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
            btnVote.Text = Resources.strings.Vote;
            btnConfirmVote.Text = Resources.strings.Confirm;

            int pollcontentid = discover_content_id();
            string sQuestion = "";
            int iTotalVoteCount = 0;
            if (pollcontentid <= 0)
            {
                divMessage.InnerHtml = "<b>No suitable data</b>";
                divMessage.Visible = true;
                radioListChoices.Visible = false;                
            }
            try
            {
                DataTable tblPoll = LegoWebSite.Buslgic.Polls.get_PollData(pollcontentid, out sQuestion, out iTotalVoteCount);
                if (tblPoll.Rows.Count > 0)
                {
                    litQuestion.Text = sQuestion;
                    radioListChoices.DataValueField = "ID";
                    radioListChoices.DataTextField = "Choice";
                    radioListChoices.DataSource = tblPoll;
                    radioListChoices.DataBind();
                }
                else
                {
                    divMessage.InnerHtml = "<b>No suitable data</b>";
                    divMessage.Visible = true;
                    radioListChoices.Visible = false;
                }
            }
            catch (Exception ex)
            {
                divMessage.InnerHtml = "Error:" + ex.Message + " " + ex.InnerException;
                divMessage.Visible = true;
                radioListChoices.Visible = false;                
            }
        }
    }
    protected void btnVote_OnClick(object sender, EventArgs e)
    {
        if (radioListChoices.SelectedItem != null)
        {
            divChoices.Visible = true;
            divVoting.Visible = true;
            divResult.Visible = false;
            this.Session["CaptchaImageText"] = GenerateRandomCode();
            txtVoteConfirmNumber.Text = "";
        }
    
    }
    private string GenerateRandomCode()
    {
        string s = "";
        for (int i = 0; i < 6; i++)
            s = String.Concat(s, this.random.Next(10).ToString());
        return s;
    }
    protected void btnConfirmVote_OnClick(object sender, EventArgs e)
    {
        if (this.txtVoteConfirmNumber.Text == this.Session["CaptchaImageText"].ToString())
        {
            divChoices.Visible = false;
            divVoting.Visible = false;
            divResult.Visible = true;
          
            //increase vote count for selected answer
            int iChoiceId =int.Parse(radioListChoices.SelectedValue);
            if (iChoiceId > 0)
            {
                LegoWebSite.Buslgic.Polls.increase_VoteCount(iChoiceId);
                //show result
               divResult.InnerHtml =  getResultHTML();
            }
        }
        else
        {
            this.divMessage.InnerHtml = "(*) Confirmation code is incorrect, please try again.";
            btnVote_OnClick(null, null);
        }
    }

    protected string getResultHTML()
    {
        int ipollcontentid = discover_content_id();
        string sChoice = "";
        int iTotalVoteCount = 0;
        DataTable tblPoll = LegoWebSite.Buslgic.Polls.get_PollData(ipollcontentid, out sChoice, out iTotalVoteCount);

        System.Text.StringBuilder sbResult = new System.Text.StringBuilder();
        foreach (DataRow dr in tblPoll.Rows)
        {
            decimal percentage = 0;
            if (iTotalVoteCount > 0)
                percentage = decimal.Round((decimal.Parse(dr["VoteCount"].ToString()) / decimal.Parse(iTotalVoteCount.ToString())) * 100, MidpointRounding.AwayFromZero);

            string alt = dr["VoteCount"].ToString() + " votes of " + iTotalVoteCount.ToString();

            sbResult.Append("<div class='poll-result'>").Append(dr["Choice"]).Append(" (").Append(dr["VoteCount"]).Append(" votes - ").Append(percentage).Append("%)</div>");
            sbResult.Append("<div class='poll-chart'><img src='images/red-bar.png' width='0%' val='").Append(percentage).Append("%' height='15px' alt='").Append(alt).Append("'title='").Append(alt).Append("' /> ").Append("</div>");
        }        
        sbResult.Append(String.Format("<div class='poll-total'>{0}:{1}</div>",Resources.strings.TotalVoteCount,iTotalVoteCount.ToString()));
        return sbResult.ToString();
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
