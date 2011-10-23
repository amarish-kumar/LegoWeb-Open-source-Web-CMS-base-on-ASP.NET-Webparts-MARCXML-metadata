using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;



/// <summary>
/// Summary description for UserDataProvider
/// </summary>
namespace LegoWeb.DataProvider
{
    public class UserDataProvider
{
    public int RecordCount = 0;
    public int PageCount = 0;
    public int PageNumber = 0;
    public int RecordsPerPage = 10;
    public DataTable Data;

    public UserDataProvider()
    {
        /*
         Comment: Gets or sets application-specific information for the membership user. 
         CreationDate: Gets the date and time when the user was added to the membership data store. 
         Email: Gets or sets the e-mail address for the membership user. 
         IsApproved: Gets or sets whether the membership user can be authenticated. 
         IsLockedOut: Gets a value indicating whether the membership user is locked out and unable to be validated. 
         IsOnline: Gets whether the user is currently online. 
         LastActivityDate: Gets or sets the date and time when the membership user was last authenticated or accessed the application. 
         LastLockoutDate: Gets the most recent date and time that the membership user was locked out. 
         LastLoginDate: Gets or sets the date and time when the user was last authenticated. 
         LastPasswordChangedDate: Gets the date and time when the membership user's password was last updated. 
         PasswordQuestion: Gets the password question for the membership user. 
         ProviderName: Gets the name of the membership provider that stores and retrieves user information for the membership user. 
         ProviderUserKey: Gets the user identifier from the membership data source for the user. 
         UserName: Gets the logon name of the membership user.          
         */
        Data = new DataTable();
        Data.TableName = "USERS";
        Data.Columns.Add(new DataColumn("UserName", typeof(string)));
        Data.Columns.Add(new DataColumn("Email", typeof(string)));
        Data.Columns.Add(new DataColumn("Comment", typeof(string)));
        Data.Columns.Add(new DataColumn("IsApproved", typeof(bool)));
        Data.Columns.Add(new DataColumn("IsLockedOut", typeof(bool)));
        Data.Columns.Add(new DataColumn("CreationDate", typeof(string)));
        Data.Columns.Add(new DataColumn("LastActivityDate", typeof(string)));
        Data.Columns.Add(new DataColumn("IsOnline", typeof(bool)));
    }

    public DataTable findAllUsers(out int outPageCount)
    {
        try
        {
            MembershipUserCollection myUsers=Membership.GetAllUsers(PageNumber-1, RecordsPerPage, out RecordCount);
            PageCount = RecordCount / RecordsPerPage;
            if (RecordCount % RecordsPerPage > 0)
            {
                PageCount++;
            }
            outPageCount = PageCount;
            //load data                
            if (RecordCount > 0)
            {
                int iPos=(PageNumber-1)*RecordsPerPage+1;
                foreach (MembershipUser user in myUsers)
                {
                    DataRow row;
                    row = Data.NewRow();
                    row["UserName"] = user.UserName;
                    row["Email"] = user.Email;
                    row["Comment"] = user.Comment;
                    row["IsApproved"] = user.IsApproved;
                    row["IsLockedOut"] = user.IsLockedOut;
                    row["CreationDate"] = user.CreationDate.ToShortDateString();
                    row["LastActivityDate"] = user.LastActivityDate.ToShortDateString();
                    row["IsOnline"] = user.IsOnline;
                    Data.Rows.Add(row);
                }
            }
            return Data;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable findUsersByName(out int outPageCount, string sUserName)
    {
        try
        {
            MembershipUserCollection myUsers = Membership.FindUsersByName(sUserName,PageNumber - 1, RecordsPerPage, out RecordCount);
            PageCount = RecordCount / RecordsPerPage;
            if (RecordCount % RecordsPerPage > 0)
            {
                PageCount++;
            }
            outPageCount = PageCount;
            //load data                
            if (RecordCount > 0)
            {
                int iPos = (PageNumber - 1) * RecordsPerPage + 1;
                foreach (MembershipUser user in myUsers)
                {
                    DataRow row;
                    row = Data.NewRow();
                    row["UserName"] = user.UserName;
                    row["Email"] = user.Email;
                    row["Comment"] = user.Comment;
                    row["IsApproved"] = user.IsApproved;
                    row["IsLockedOut"] = user.IsLockedOut;
                    row["CreationDate"] = user.CreationDate.ToShortDateString();
                    row["LastActivityDate"] = user.LastActivityDate.ToShortDateString();
                    row["IsOnline"] = user.IsOnline;
                    Data.Rows.Add(row);
                }
            }
            return Data;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable findUsersByEmail(out int outPageCount, string sEmail)
    {
        try
        {
            MembershipUserCollection myUsers = Membership.FindUsersByEmail(sEmail, PageNumber - 1, RecordsPerPage, out RecordCount);
            PageCount = RecordCount / RecordsPerPage;
            if (RecordCount % RecordsPerPage > 0)
            {
                PageCount++;
            }
            outPageCount = PageCount;
            //load data                
            if (RecordCount > 0)
            {
                int iPos = (PageNumber - 1) * RecordsPerPage + 1;
                foreach (MembershipUser user in myUsers)
                {
                    DataRow row;
                    row = Data.NewRow();
                    row["UserName"] = user.UserName;
                    row["Email"] = user.Email;
                    row["Comment"] = user.Comment;
                    row["IsApproved"] = user.IsApproved;
                    row["IsLockedOut"] = user.IsLockedOut;
                    row["CreationDate"] = user.CreationDate.ToShortDateString();
                    row["LastActivityDate"] = user.LastActivityDate.ToShortDateString();
                    row["IsOnline"] = user.IsOnline;
                    Data.Rows.Add(row);
                }
            }
            return Data;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable findUsersByRole(out int outPageCount, string sRole)
    {
        try
        {
            string[] usersInRole = Roles.GetUsersInRole(sRole);
            RecordCount = usersInRole.Length;

            PageCount = RecordCount / RecordsPerPage;
            if (RecordCount % RecordsPerPage > 0)
            {
                PageCount++;
            }
            outPageCount = PageCount;

            if (RecordCount == 0) return Data;
            int istartPos = RecordsPerPage * (PageNumber-1);
            int iendPos = PageNumber * RecordsPerPage;
            if (istartPos > RecordCount) return Data;
            iendPos = RecordCount > iendPos ? iendPos : RecordCount;


            int iPos = (PageNumber - 1) * RecordsPerPage + 1;

            for (int i = istartPos; i < iendPos; i++)
            {
                MembershipUser user = Membership.GetUser(usersInRole[i]);
                if (user != null)
                {
                    DataRow row;
                    row = Data.NewRow();
                    row["UserName"] = user.UserName;
                    row["Email"] = user.Email;
                    row["Comment"] = user.Comment;
                    row["IsApproved"] = user.IsApproved;
                    row["IsLockedOut"] = user.IsLockedOut;
                    row["CreationDate"] = user.CreationDate.ToShortDateString();
                    row["LastActivityDate"] = user.LastActivityDate.ToShortDateString();
                    row["IsOnline"] = user.IsOnline;
                    Data.Rows.Add(row);
                }
            }
            return Data;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
}
