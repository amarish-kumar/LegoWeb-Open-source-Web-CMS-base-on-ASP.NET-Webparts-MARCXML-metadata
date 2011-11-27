using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace LegoWebSiteForum.Buslogic
{
    /// <summary>
    /// Summary description for ForumThreadBrowser
    /// </summary>
    public static class ForumThreadBrowser
    {
        public static Int32 get_Threads_Browse_Count(int iForumId)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString);
            SqlCommand cmd = new SqlCommand("LWF_GetThreadCount", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ForumID", SqlDbType.Int, 4);
            cmd.Parameters.Add("@Count", SqlDbType.Int, 4);
            cmd.Parameters[0].Value = iForumId;
            cmd.Parameters[1].Direction = ParameterDirection.Output;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            return (int)cmd.Parameters[1].Value;
        }
        public static DataSet get_Threads_Browse_Page(int iForumId, int iPage, int iPageSize)
        {
            DataSet myPageData = new DataSet();
            iPage--;// giam so voi cach tinh thong thuong: 0 la trang 1

            SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString);
            SqlCommand cmd = new SqlCommand("LWF_GetThreads", Conn);

            try
            {
                // Populate parameters
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ForumID", SqlDbType.Int, 4);
                cmd.Parameters.Add("@PageSize", SqlDbType.Int, 4);
                cmd.Parameters.Add("@PageIndex", SqlDbType.Int, 4);
                cmd.Parameters[0].Value = iForumId;
                cmd.Parameters[1].Value = iPageSize;
                cmd.Parameters[2].Value = iPage;

                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                Conn.Open();
                adap.Fill(myPageData,"Table");
                Conn.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }

            return myPageData;
        }


    }
}