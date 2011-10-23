using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security; 

public partial class VerifyNewUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if the id is in the query string
        if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
        {    
            //store the user id    
            Guid userId = new Guid(Request.QueryString["ID"]);     
            //attempt to get the user's information    
            MembershipUser user = Membership.GetUser(userId);     
            //check if the user exists    
            if (user != null)    
            {        
                //check to make sure the user is not approved yet        
                if (!user.IsApproved)        
                {            
                    //approve the user            
                    user.IsApproved = true;            
                    //update the account in the database            
                    Membership.UpdateUser(user);            
                    //display a success message            
                    label1.Text = LegoWebSite.Buslgic.CommonParameters.get_COMMON_PARAMETER_VALUE("REGISTRATION_SUCCESS_GREETING");
                }
            }
        } 
    }
    protected override void OnInit(EventArgs e)
    {
        CultureUtility.SetThreadCulture();
        base.OnInit(e);
    }
}
