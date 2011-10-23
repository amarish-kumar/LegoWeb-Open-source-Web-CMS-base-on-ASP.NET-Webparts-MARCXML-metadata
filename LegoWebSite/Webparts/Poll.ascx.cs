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


public partial class Webparts_Poll :WebPartBase
{
    private int _meta_content_id=0;
   
    [Personalizable]
    [WebBrowsable]
    /// <summary>
    /// Set Poll Id
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
    private Random random = new Random();
     public Webparts_Poll()
    {
        this.Title = "POLL";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            this.Title = LegoWebSite.Buslgic.CommonParameters.get_COMMON_PARAMETER_VALUE("POLL_TITLE");
            string sLangCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            string SAnswer = "";
            int TotalVotes = 0;
            if (meta_content_id == 0)
            {
                if (CommonUtility.GetInitialValue("contentid", null) != null)
                {
                    meta_content_id = int.Parse(CommonUtility.GetInitialValue("contentid", null).ToString());
                }
            }
            try
            {
                DataTable tblPoll = LegoWebSite.Buslgic.Polls.get_PollData(meta_content_id, out SAnswer, out TotalVotes, sLangCode);
                if (tblPoll.Rows.Count > 0)
                {
                    litQuestion.Text = SAnswer;
                    radioListChoices.DataValueField = "ID";
                    radioListChoices.DataTextField = "Answer";
                    radioListChoices.DataSource = tblPoll;
                    radioListChoices.DataBind();
                }
                else
                {
                    divMsg.InnerHtml = "<b>Không có dữ liệu</b>";
                    divMsg.Visible = true;
                    radioListChoices.Visible = false;
                }
            }
            catch (Exception ex)
            {
                divMsg.InnerHtml = "Lỗi:" + ex.Message + " " + ex.InnerException;
                divMsg.Visible = true;
                radioListChoices.Visible = false;                
            }
        }
    }
    protected void btnVote_OnClick(object sender, EventArgs e)
    {
        if (radioListChoices.SelectedItem != null)
        {
            divQuestion.Visible = false;
            divVote.Visible = true;
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
            divQuestion.Visible = false;
            divVote.Visible = false;
            divResult.Visible = true;
          
            //increase vote count for selected answer
            int iAnswerId =int.Parse(radioListChoices.SelectedValue);
            if (iAnswerId > 0)
            {
                LegoWebSite.Buslgic.Polls.increase_VoteCount(iAnswerId);
                //show result
               divResult.InnerHtml =  getResultHTML();
            }
        }
        else
        {
            this.labelErroMessage.Text = "(*) Mã khẳng định không đúng, mời làm lại.";
            btnVote_OnClick(null, null);
        }
    }

    protected string getResultHTML()
    {
        string sLangCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        string SAnswer = "";
        int TotalVotes = 0;
        DataTable tblPoll = LegoWebSite.Buslgic.Polls.get_PollData(meta_content_id, out SAnswer, out TotalVotes, sLangCode);

        System.Text.StringBuilder sbResult = new System.Text.StringBuilder();
        foreach (DataRow dr in tblPoll.Rows)
        {
            decimal percentage = 0;
            if (TotalVotes > 0)
                percentage = decimal.Round((decimal.Parse(dr["VoteCount"].ToString()) / decimal.Parse(TotalVotes.ToString())) * 100, MidpointRounding.AwayFromZero);

            string alt = dr["VoteCount"].ToString() + " votes out of " + TotalVotes.ToString();

            sbResult.Append("<div class='poll-result'>").Append(dr["Answer"]).Append(" (").Append(dr["VoteCount"]).Append(" votes - ").Append(percentage).Append("%)</div>");
            sbResult.Append("<div class='poll-chart'><img src='images/red-bar.png' width='0%' val='").Append(percentage).Append("%' height='15px' alt='").Append(alt).Append("'title='").Append(alt).Append("' /> ").Append("</div>");
        }
        sbResult.Append("<div class='poll-total'>Tổng số phiếu: ").Append(TotalVotes).Append("</div>");
        return sbResult.ToString();
    }

}
