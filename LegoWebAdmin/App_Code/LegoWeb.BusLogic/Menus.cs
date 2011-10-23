using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using MarcXmlParserEx;

namespace LegoWeb.BusLogic
{
    /// <summary>
    /// Summary description for Menus
    /// </summary>
    public static class Menus
    {

        public static void addUpdate_MENU(int iMENU_ID, int iPARENT_MENU_ID, int iMENU_TYPE_ID, string sMENU_VI_TITLE, string sMENU_EN_TITLE, string sMENU_LINK_URL, string sMENU_IMAGE_URL, int iBROWSER_NAVIGATE, bool bIsPublic)
        {
            string connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            SqlConnection connection = new SqlConnection(connStr);
            try
            {
                string strCommandName;
                SqlCommand objCommand;
                SqlParameter objParam;

                strCommandName = "sp_LEGOWEB_MENUS_ADDUPDATE";
                objCommand = new SqlCommand(strCommandName, connection);
                objCommand.CommandType = CommandType.StoredProcedure;
                //Set the Parameters

                objParam = objCommand.Parameters.Add(new SqlParameter("@_MENU_ID", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iMENU_ID;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_PARENT_MENU_ID", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iPARENT_MENU_ID;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_MENU_TYPE_ID", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iMENU_TYPE_ID;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_MENU_VI_TITLE", SqlDbType.NVarChar, 250));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sMENU_VI_TITLE;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_MENU_EN_TITLE", SqlDbType.NVarChar, 250));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sMENU_EN_TITLE;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_MENU_LINK_URL", SqlDbType.NVarChar, 50));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sMENU_LINK_URL;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_MENU_IMAGE_URL", SqlDbType.NVarChar, 250));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sMENU_IMAGE_URL;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_BROWSER_NAVIGATE", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iBROWSER_NAVIGATE;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_IS_PUBLIC", SqlDbType.Bit));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = bIsPublic;

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

        public static DataSet get_MENU_BY_ID(int iMenuID)
        {
            DataSet retData = new DataSet();
            String connString = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(connString))
            {
                try
                {
                    String strCommandText;
                    SqlCommand objCommand;

                    strCommandText = "SELECT * FROM LEGOWEB_MENUS WHERE MENU_ID=" + iMenuID.ToString();

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

        public static DataSet get_MENU_BY_PARENT_ID(int iParentMenuID, int iMenuTypeId)
        {
            DataSet retData = new DataSet();
            String ConnString = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(ConnString))
            {
                try
                {
                    String strCommandText;
                    SqlCommand objCommand;
                    strCommandText = "SELECT * FROM LEGOWEB_MENUS WHERE PARENT_MENU_ID=" + iParentMenuID.ToString() + (iParentMenuID == 0 ? " AND MENU_TYPE_ID=" + iMenuTypeId.ToString() : "") + " ORDER BY ORDER_NUMBER ASC";
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

        public static DataSet get_MENU_BY_PARENT_ID(int iParentMenuID)
        {
            DataSet retData = new DataSet();
            String ConnString = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(ConnString))
            {
                try
                {
                    String strCommandText;
                    SqlCommand objCommand;
                    strCommandText = "SELECT * FROM LEGOWEB_MENUS WHERE PARENT_MENU_ID=" + iParentMenuID.ToString() + " ORDER BY ORDER_NUMBER ASC";
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
        public static void remove_MENU(int iMENU_ID)
        {
            string connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            SqlConnection connection = new SqlConnection(connStr);
            try
            {
                string strCommandName;
                SqlCommand objCommand;
                SqlParameter objParam;

                strCommandName = "sp_LEGOWEB_MENUS_REMOVE";
                objCommand = new SqlCommand(strCommandName, connection);
                objCommand.CommandType = CommandType.StoredProcedure;
                //Set the Parameters

                objParam = objCommand.Parameters.Add(new SqlParameter("@_MENU_ID", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iMENU_ID;

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

        public static void update_MENU_ORDER(int iMENU_ID, int iORDER_NUMBER)
        {
            string connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            SqlConnection connection = new SqlConnection(connStr);
            try
            {
                string strCommandName;
                SqlCommand objCommand;
                SqlParameter objParam;

                strCommandName = "UPDATE LEGOWEB_MENUS SET ORDER_NUMBER=@_ORDER_NUMBER WHERE MENU_ID=@_MENU_ID";
                objCommand = new SqlCommand(strCommandName, connection);
                objCommand.CommandType = CommandType.Text;
                //Set the Parameters

                objParam = objCommand.Parameters.Add(new SqlParameter("@_MENU_ID", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iMENU_ID;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_ORDER_NUMBER", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iORDER_NUMBER;

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
        public static void publish_MENU(int iMENU_ID, bool bIsPublic)
        {
            string connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            SqlConnection connection = new SqlConnection(connStr);
            try
            {
                string strCommandName;
                SqlCommand objCommand;
                SqlParameter objParam;

                strCommandName = "UPDATE LEGOWEB_MENUS SET IS_PUBLIC=@_IS_PUBLIC WHERE MENU_ID=@_MENU_ID";
                objCommand = new SqlCommand(strCommandName, connection);
                objCommand.CommandType = CommandType.Text;
                //Set the Parameters

                objParam = objCommand.Parameters.Add(new SqlParameter("@_MENU_ID", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iMENU_ID;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_IS_PUBLIC", SqlDbType.Bit));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = bIsPublic;

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

        public static void moveUp_MENU(int iMenuID)
        {
            DataSet mnuData = get_MENU_BY_ID(iMenuID);
            Int32 iParentMenuId = (Int32)mnuData.Tables[0].Rows[0]["PARENT_MENU_ID"];
            int iMenuTypeId = int.Parse(mnuData.Tables[0].Rows[0]["MENU_TYPE_ID"].ToString());
            DataTable catOrder = null;

            if (iParentMenuId == 0)
            {
                catOrder = get_MENU_BY_PARENT_ID(0, iMenuTypeId).Tables[0];
            }
            else
            {
                catOrder = get_MENU_BY_PARENT_ID(iParentMenuId).Tables[0];
            }

            for (int i = 0; i < catOrder.Rows.Count; i++)
            {
                if (i + 1 < catOrder.Rows.Count)
                {
                    if (((Int32)catOrder.Rows[i + 1]["MENU_ID"]) == iMenuID)
                    {
                        update_MENU_ORDER((Int32)catOrder.Rows[i]["MENU_ID"], i + 2);
                        update_MENU_ORDER((Int32)catOrder.Rows[i + 1]["MENU_ID"], i + 1);
                        i++;
                    }
                    else
                    {
                        update_MENU_ORDER((Int32)catOrder.Rows[i]["MENU_ID"], i + 1);
                    }
                }
            }
        }

        public static void moveDown_MENU(int iMenuID)
        {

            DataSet mnuData = get_MENU_BY_ID(iMenuID);
            Int32 iParentMenuId = (Int32)mnuData.Tables[0].Rows[0]["PARENT_MENU_ID"];
            int iMenuTypeId = (int)mnuData.Tables[0].Rows[0]["MENU_TYPE_ID"];
            DataTable catOrder = null;

            if (iParentMenuId == 0)
            {
                catOrder = get_MENU_BY_PARENT_ID(0, iMenuTypeId).Tables[0];
            }
            else
            {
                catOrder = get_MENU_BY_PARENT_ID(iParentMenuId).Tables[0];
            }
            for (int i = 0; i < catOrder.Rows.Count; i++)
            {
                if (((Int32)catOrder.Rows[i]["MENU_ID"]) == iMenuID)
                {
                    if (i + 1 < catOrder.Rows.Count)
                    {
                        update_MENU_ORDER((Int32)catOrder.Rows[i]["MENU_ID"], i + 2);
                        update_MENU_ORDER((Int32)catOrder.Rows[i + 1]["MENU_ID"], i + 1);
                        i++;
                    }
                    else
                    {
                        update_MENU_ORDER((Int32)catOrder.Rows[i]["MENU_ID"], i + 1);
                    }
                }
                else
                {
                    LegoWeb.BusLogic.Menus.update_MENU_ORDER((Int32)catOrder.Rows[i]["MENU_ID"], i + 1);
                }
            }
        }

        public static string get_MENU_LINK_URL(int iMenuID)
        {
            DataSet retData = new DataSet();
            String connString = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(connString))
            {
                try
                {
                    SqlCommand cmdReader = new SqlCommand("SELECT MENU_LINK_URL FROM LEGOWEB_MENUS WHERE MENU_ID=" + iMenuID.ToString(), Conn);
                    Conn.Open();
                    SqlDataReader reader = cmdReader.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        string retStr = reader["MENU_LINK_URL"].ToString();
                        Conn.Close();
                        return retStr;
                    }
                    else
                    {
                        Conn.Close();
                        return null;
                    }
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

        public static Int32 get_Search_Count(int iMenuId, int iParentMenuId, int iMenuTypeId)
        {
            String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(connStr))
            {
                try
                {
                    string strCommandName;
                    SqlCommand objCommand;
                    SqlParameter objParam;

                    strCommandName = "sp_LEGOWEB_MENUS_SEARCH_COUNT";
                    objCommand = new SqlCommand(strCommandName, Conn);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    //Set the Parameters
                    if (iMenuId > 0)
                    {
                        objParam = objCommand.Parameters.Add(new SqlParameter("@_MENU_ID", SqlDbType.Int));
                        objParam.Direction = ParameterDirection.Input;
                        objParam.Value = iMenuId;
                    }
                    if (iParentMenuId > 0)
                    {
                        objParam = objCommand.Parameters.Add(new SqlParameter("@_PARENT_MENU_ID", SqlDbType.Int));
                        objParam.Direction = ParameterDirection.Input;
                        objParam.Value = iParentMenuId;
                    }
                    if (iMenuTypeId > 0)
                    {
                        objParam = objCommand.Parameters.Add(new SqlParameter("@_MENU_TYPE_ID", SqlDbType.Int));
                        objParam.Direction = ParameterDirection.Input;
                        objParam.Value = iMenuTypeId;
                    }
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

        public static DataSet get_Search_Page(int iMenuId, int iParentMenuId, int iMenuTypeId, string sTabChars, int iPage, int iPageSize)
        {
            int startPos = (iPage - 1) * iPageSize;
            int iSelectRow = iPage * iPageSize;
            DataSet myPageData = new DataSet();
            String connString = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;

            using (SqlConnection Conn = new SqlConnection(connString))
            {
                try
                {
                    string strCommandName;
                    SqlCommand objCommand;
                    SqlParameter objParam;


                    strCommandName = "sp_LEGOWEB_MENUS_SEARCH_PAGE";
                    objCommand = new SqlCommand(strCommandName, Conn);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    //Set the Parameters
                    if (iMenuId > 0)
                    {
                        objParam = objCommand.Parameters.Add(new SqlParameter("@_MENU_ID", SqlDbType.Int));
                        objParam.Direction = ParameterDirection.Input;
                        objParam.Value = iMenuId;
                    }
                    if (iParentMenuId > 0)
                    {
                        objParam = objCommand.Parameters.Add(new SqlParameter("@_PARENT_MENU_ID", SqlDbType.Int));
                        objParam.Direction = ParameterDirection.Input;
                        objParam.Value = iParentMenuId;
                    }
                    if (iMenuTypeId > 0)
                    {
                        objParam = objCommand.Parameters.Add(new SqlParameter("@_MENU_TYPE_ID", SqlDbType.Int));
                        objParam.Direction = ParameterDirection.Input;
                        objParam.Value = iMenuTypeId;
                    }
                    if (iSelectRow > 0)
                    {
                        objParam = objCommand.Parameters.Add(new SqlParameter("@_SELECT_ROW_NUMBER", SqlDbType.Int));
                        objParam.Direction = ParameterDirection.Input;
                        objParam.Value = iSelectRow;
                    }
                    if (sTabChars != null)
                    {
                        objParam = objCommand.Parameters.Add(new SqlParameter("@_TAB_CHARS", SqlDbType.NVarChar, 20));
                        objParam.Direction = ParameterDirection.Input;
                        objParam.Value = sTabChars;
                    }
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

        public static bool is_MenuItem_Exist(int iMenuId)
        {
            String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    SqlCommand cmdReader = new SqlCommand("SELECT MENU_ID FROM LEGOWEB_MENUS WHERE MENU_ID=" + iMenuId.ToString(), conn);
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
    }
}
