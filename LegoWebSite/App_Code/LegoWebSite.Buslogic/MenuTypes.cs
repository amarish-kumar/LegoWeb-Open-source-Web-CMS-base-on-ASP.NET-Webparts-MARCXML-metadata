using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace LegoWebSite.Buslgic
{
    /// <summary>
    /// Summary description for Menus
    /// </summary>
    public static class MenuTypes
    {

        public static int get_TOP_MENU_TYPE_ID()
        {
            String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    SqlCommand cmdReader = new SqlCommand("SELECT TOP(1) MENU_TYPE_ID FROM LEGOWEB_MENU_TYPES ORDER BY MENU_TYPE_ID ASC ", conn);
                    conn.Open();
                    SqlDataReader reader = cmdReader.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        int retID =int.Parse(reader["MENU_TYPE_ID"].ToString());
                        conn.Close();
                        return retID;
                    }
                    else
                    {
                        conn.Close();
                        return -1;
                    }
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

        public static DataSet get_MENU_TYPES_BY_ID(int iMenuTypeID)
        {
            DataSet retData = new DataSet();
            String ConnString = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(ConnString))
            {
                try
                {
                    String strCommandText;
                    SqlCommand objCommand;
                    strCommandText = "SELECT * FROM LEGOWEB_MENU_TYPES WHERE MENU_TYPE_ID=" + iMenuTypeID.ToString();
                    objCommand = new SqlCommand(strCommandText, Conn);
                    objCommand.CommandType = CommandType.Text;
                    SqlDataAdapter adap = new SqlDataAdapter(objCommand);
                    Conn.Open();
                    adap.Fill(retData, "Table");
                    Conn.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (Conn.State == ConnectionState.Open)
                    {
                        Conn.Close();
                    }
                }
            }
            return retData;
        }

    }
}
