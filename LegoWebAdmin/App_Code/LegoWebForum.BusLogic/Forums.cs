using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace LegoWebForum.BusLogic
{
    /// <summary>
    /// Summary description for Forums
    /// </summary>
    public static class Forums
    {
        public static DataTable get_LEGOWEB_FORUMS(int iForumID)
        {
            DataSet fData = new DataSet();

            String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                String strSQL = "SELECT * FROM LEGOWEB_FORUMS ";
                if (iForumID>0)
                {
                    strSQL += " WHERE ForumID=" + iForumID.ToString();
                }

                strSQL += "ORDER BY OrderNumber ASC ";
                try
                {
                    SqlCommand objCommand;
                    SqlDataAdapter adap;

                    objCommand = new SqlCommand(strSQL, conn);
                    objCommand.CommandType = CommandType.Text;

                    adap = new SqlDataAdapter(objCommand);
                    conn.Open();
                    adap.Fill(fData, "Table");
                    conn.Close();
                    return fData.Tables[0];
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn != null)
                        conn.Close();
                }
            }
        }

        public static Int32 add_LEGOWEB_FORUMS(string sTitle, string sDescription, string sAdminRoles, bool bIsPublic, int iOrderNumber, string sImageURL)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString);
            SqlCommand cmd = new SqlCommand("LWF_AddForum", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ForumID", SqlDbType.Int, 4);
            cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@AdminRoles", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@IsPublic", SqlDbType.Bit, 1);
            cmd.Parameters.Add("@OrderNumber", SqlDbType.Int, 4);
            cmd.Parameters.Add("@ImageURL", SqlDbType.NVarChar, 250);

            cmd.Parameters[0].Direction = ParameterDirection.Output;
            cmd.Parameters[1].Value = sTitle;
            cmd.Parameters[2].Value = sDescription;
            cmd.Parameters[3].Value = sAdminRoles;
            cmd.Parameters[4].Value = bIsPublic;
            cmd.Parameters[5].Value = iOrderNumber;
            cmd.Parameters[6].Value = sImageURL;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            int iForumID = (int)cmd.Parameters[0].Value;

            return iForumID;
        }


        public static void update_LEGOWEB_FORUMS(int iForumID,string sTitle, string sDescription, string sAdminRoles, bool bIsPublic, int iOrderNumber,string sImageURL)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString);
            SqlCommand cmd = new SqlCommand("LWF_UpdateForum", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ForumID", SqlDbType.Int, 4);
            cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@AdminRoles", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@IsPublic", SqlDbType.Bit, 1);
            cmd.Parameters.Add("@OrderNumber", SqlDbType.Int, 4);
            cmd.Parameters.Add("@ImageURL", SqlDbType.NVarChar, 250);

            cmd.Parameters[0].Value = iForumID;
            cmd.Parameters[1].Value = sTitle;
            cmd.Parameters[2].Value = sDescription;
            cmd.Parameters[3].Value = sAdminRoles;
            cmd.Parameters[4].Value = bIsPublic;
            cmd.Parameters[5].Value = iOrderNumber;
            cmd.Parameters[6].Value = sImageURL;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void delete_LEGOWEB_FORUMS(int iForumID)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString);
            SqlCommand cmd = new SqlCommand("LWF_DeleteForum", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ForumID", SqlDbType.Int, 4);

            cmd.Parameters[0].Value = iForumID;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
