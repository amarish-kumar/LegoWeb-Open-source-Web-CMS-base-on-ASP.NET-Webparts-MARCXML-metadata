using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using MarcXmlParserEx;

namespace LegoWebSite.Buslgic
{
    /// <summary>
    /// Summary description for Menus
    /// </summary>
    public static class Menus
    {
        public static DataSet get_MENUS_BY_MENU_ID(int iMenuID)
        {
            DataSet retData = new DataSet();
            String ConnString = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(ConnString))
            {
                try
                {
                    String strCommandText;
                    SqlCommand objCommand;
                    strCommandText = "SELECT * FROM LEGOWEB_MENUS WHERE MENU_ID=" + iMenuID.ToString() +" AND IS_PUBLIC=1 ORDER BY ORDER_NUMBER ASC";
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

        public static DataSet get_MENUS_BY_PARENT_ID(int iParentMenuID)
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
        public static DataSet get_MENUS_BY_PARENT_ID(int iParentMenuID, int iMenuType)
        {
            DataSet retData = new DataSet();
            String ConnString = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(ConnString))
            {
                try
                {
                    String strCommandText;
                    SqlCommand objCommand;
                    SqlParameter objParam;

                    strCommandText = "SELECT * FROM LEGOWEB_MENUS WHERE ";
                    if(iParentMenuID<=0)//mean get all root menu item of a menu type = get all menu type with parent id not in this menu type
                    {
                        strCommandText += " PARENT_MENU_ID NOT IN(SELECT MENU_ID FROM LEGOWEB_MENUS WHERE MENU_TYPE_ID=@_MENU_TYPE_ID) AND MENU_TYPE_ID=@_MENU_TYPE_ID ";    
                    }else
                    {
                        strCommandText += " PARENT_MENU_ID =@_PARENT_MENU_ID AND MENU_TYPE_ID=@_MENU_TYPE_ID ";    
                    }
                    strCommandText += " AND LEGOWEB_MENUS.IS_PUBLIC=1 ORDER BY ORDER_NUMBER ASC";                    
                    
                    objCommand = new SqlCommand(strCommandText, Conn);
                    objCommand.CommandType = CommandType.Text;


                    if (iParentMenuID > 0)
                    {
                        objParam = objCommand.Parameters.Add(new SqlParameter("@_PARENT_MENU_ID", SqlDbType.Int));
                        objParam.Direction = ParameterDirection.Input;
                        objParam.Value = iParentMenuID;
                    }

                    objParam = objCommand.Parameters.Add(new SqlParameter("@_MENU_TYPE_ID", SqlDbType.Int));
                    objParam.Direction = ParameterDirection.Input;
                    objParam.Value = iMenuType;

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

        public static int get_PARENT_MENU_ID(int iMenuID)
        {
            String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    SqlCommand cmdReader = new SqlCommand("SELECT PARENT_MENU_ID FROM LEGOWEB_MENUS WHERE MENU_ID=" + iMenuID.ToString(), conn);
                    conn.Open();
                    SqlDataReader reader = cmdReader.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        int retID = (int)reader["PARENT_MENU_ID"];
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

        public static int get_PARENT_MENU_ID(int iMenuID, int iMenuTypeID)
        {
            String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    SqlParameter objParam;
                    SqlCommand cmdReader = new SqlCommand("SELECT PARENT_MENU_ID FROM LEGOWEB_MENUS WHERE MENU_ID=@_MENU_ID AND (PARENT_MENU_ID IN (SELECT MENU_ID FROM LEGOWEB_MENUS WHERE MENU_TYPE_ID=@_MENU_TYPE_ID))", conn);
                    conn.Open();

                    objParam = cmdReader.Parameters.Add(new SqlParameter("@_MENU_ID", SqlDbType.Int));
                    objParam.Direction = ParameterDirection.Input;
                    objParam.Value = iMenuID;

                    objParam = cmdReader.Parameters.Add(new SqlParameter("@_MENU_TYPE_ID", SqlDbType.Int));
                    objParam.Direction = ParameterDirection.Input;
                    objParam.Value = iMenuTypeID;

                    SqlDataReader reader = cmdReader.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        int retID = (int)reader["PARENT_MENU_ID"];
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

    }
}
