using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace LegoWeb.BusLogic
{
    /// <summary>
    /// Summary description for MenuTypes
    /// </summary>
    public static class MenuTypes
    {

        public static void addUpdate_MenuType(int iMENU_TYPE_ID, string sMENU_TYPE_VI_TITLE,string sMENU_TYPE_EN_TITLE, string sMENU_TYPE_DESCRIPTION)
        {
            string connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            SqlConnection connection = new SqlConnection(connStr);
            try
            {
                string strCommandName;
                SqlCommand objCommand;
                SqlParameter objParam;

                strCommandName = "sp_LEGOWEB_MENU_TYPES_ADDUPDATE";
                objCommand = new SqlCommand(strCommandName, connection);
                objCommand.CommandType = CommandType.StoredProcedure;
                //Set the Parameters

                objParam = objCommand.Parameters.Add(new SqlParameter("@_MENU_TYPE_ID", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iMENU_TYPE_ID;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_MENU_TYPE_VI_TITLE", SqlDbType.NVarChar, 50));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sMENU_TYPE_VI_TITLE;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_MENU_TYPE_EN_TITLE", SqlDbType.NVarChar, 50));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sMENU_TYPE_EN_TITLE;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_MENU_TYPE_DESCRIPTION", SqlDbType.NVarChar, 250));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sMENU_TYPE_DESCRIPTION;

                connection.Open();
                objCommand.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        public static void remove_MenuType(int iMENU_TYPE_ID)
        {
            string connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            SqlConnection connection = new SqlConnection(connStr);
            try
            {
                string strCommandName;
                SqlCommand objCommand;
                SqlParameter objParam;

                strCommandName = "sp_LEGOWEB_MENU_TYPES_REMOVE";
                objCommand = new SqlCommand(strCommandName, connection);
                objCommand.CommandType = CommandType.StoredProcedure;
                //Set the Parameters

                objParam = objCommand.Parameters.Add(new SqlParameter("@_MENU_TYPE_ID", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iMENU_TYPE_ID;

                connection.Open();
                objCommand.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        public static DataSet get_MenuType_By_ID(int iMenuTypeID)
        {
            DataSet retData = new DataSet();
            String connString = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(connString))
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
                        Conn.Close();
                }
            }
            return retData;
        }

        public static bool is_MenuType_Exist(int iMenuTypeId)
        {
            String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    SqlCommand cmdReader = new SqlCommand("SELECT MENU_TYPE_ID FROM LEGOWEB_MENU_TYPES WHERE MENU_TYPE_ID=" + iMenuTypeId.ToString(), conn);
                    conn.Open();
                    SqlDataReader reader = cmdReader.ExecuteReader();
                    if (reader.HasRows)
                    {
                        conn.Close();
                        return true;
                    }
                    else
                    {
                        conn.Close();
                        return false;
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
        public static Int32 get_Search_Count()
        {
            String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(connStr))
            {
                try
                {
                    String strCommandText;
                    SqlCommand objCommand;

                    strCommandText = "SELECT COUNT(MENU_TYPE_ID) FROM LEGOWEB_MENU_TYPES ";
                    objCommand = new SqlCommand(strCommandText, Conn);
                    objCommand.CommandType = CommandType.Text;

                    Conn.Open();
                    Int32 retInt = Convert.ToInt32(objCommand.ExecuteScalar());
                    Conn.Close();
                    return retInt;

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
            }
        }

        public static DataSet get_Search_Page(int iPage, int iPageSize)
        {
            int startPos = (iPage - 1) * iPageSize;
            int iSelectRow = iPage * iPageSize;
            DataSet myPageData = new DataSet();
            String connString = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;

            using (SqlConnection Conn = new SqlConnection(connString))
            {
                try
                {
                    String strCommandText = "";
                    SqlCommand objCommand;

                    strCommandText += "SELECT TOP(" + iSelectRow + ") LEGOWEB_MENU_TYPES.* FROM LEGOWEB_MENU_TYPES ORDER BY MENU_TYPE_ID ASC";

                    objCommand = new SqlCommand(strCommandText, Conn);
                    objCommand.CommandType = CommandType.Text;

                    SqlDataAdapter adap = new SqlDataAdapter(objCommand);
                    Conn.Open();
                    adap.Fill(myPageData, startPos, iPageSize, "Table");
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
            }
            return myPageData;
        }
    
    
    }
}
