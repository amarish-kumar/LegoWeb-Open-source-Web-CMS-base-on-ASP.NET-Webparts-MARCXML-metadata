// ----------------------------------------------------------------------
// <copyright file="Categories.cs" package="LEGOWEB">
//     Copyright (C) 2010-2011 HIENDAI SOFTWARE COMPANY. All rights reserved.
//     www.legoweb.org
//     License: GNU/GPL
//     LEGOWEB IS FREE SOFTWARE
// </copyright>
// ------------------------------------------------------------------------



using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using MarcXmlParserEx;

namespace LegoWebAdmin.BusLogic
{
    /// <summary>
    /// Summary description for Categories
    /// </summary>
    public static class Categories
    {


        public static string[] get_ADMIN_ROLES(int iCATEGORY_ID)
        {
            string[] allowRoles;
            String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    SqlCommand cmdReader = new SqlCommand("SELECT ADMIN_ROLES FROM LEGOWEB_CATEGORIES WHERE CATEGORY_ID=" + iCATEGORY_ID.ToString(), conn);
                    conn.Open();
                    SqlDataReader reader = cmdReader.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        string sRoles = reader["ADMIN_ROLES"].ToString();
                        conn.Close();
                        char[] splitter = { ';', ',' };
                        allowRoles = sRoles.Split(splitter);
                        return allowRoles;
                    }
                    else
                    {
                        conn.Close();
                        return null;
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

        public static string get_CATEGORY_TREE_XML(int iPARENT_CATEGORY_ID, int section_id)
        {
            SqlCommand objCommand;
            SqlParameter objParam;
            String outXml = "";

            String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            string strSQL = @"SELECT
                                CATEGORY_ID AS '@CATEGORY_ID',      
                                PARENT_CATEGORY_ID AS '@PARENT_CATEGORY_ID',     
                                CATEGORY_VI_TITLE + '(' + cast((Select COUNT(META_CONTENT_ID) From LEGOWEB_META_CONTENTS Where LEGOWEB_META_CONTENTS.CATEGORY_ID=LEGOWEB_CATEGORIES.CATEGORY_ID) as nvarchar(20)) + ')' as '@CATEGORY_VI_TITLE',             
                                CATEGORY_EN_TITLE + '(' + cast((Select COUNT(META_CONTENT_ID) From LEGOWEB_META_CONTENTS Where LEGOWEB_META_CONTENTS.CATEGORY_ID=LEGOWEB_CATEGORIES.CATEGORY_ID) as nvarchar(20)) + ')' as '@CATEGORY_EN_TITLE',
                                dbo.SelectChildCategoryXml(CATEGORY_ID,1)      
                                FROM LEGOWEB_CATEGORIES
                                WHERE PARENT_CATEGORY_ID=@_ROOT_PARENT_ID AND SECTION_ID=@SECTION_ID FOR XML PATH ('category')";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    objCommand = new SqlCommand(strSQL, conn);
                    objCommand.CommandType = CommandType.Text;

                    objParam = objCommand.Parameters.Add(new SqlParameter("@_ROOT_PARENT_ID", SqlDbType.Int));
                    objParam.Direction = ParameterDirection.Input;
                    objParam.Value = iPARENT_CATEGORY_ID;
                    objParam = objCommand.Parameters.Add(new SqlParameter("@SECTION_ID", SqlDbType.Int));
                    objParam.Direction = ParameterDirection.Input;
                    objParam.Value = section_id;
                    conn.Open();

                    System.Xml.XmlReader xmlr = objCommand.ExecuteXmlReader();

                    xmlr.Read();

                    while (xmlr.ReadState != System.Xml.ReadState.EndOfFile)
                    {

                        outXml += xmlr.ReadOuterXml();

                    }

                    conn.Close();
                    return outXml;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
        }
        public static DataSet get_MENUS_BY_SECTION_ID(int SECTION_ID)
        {
            DataSet retData = new DataSet();
            String ConnString = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(ConnString))
            {
                try
                {
                    String strCommandText;
                    SqlCommand objCommand;
                    strCommandText = "SELECT * FROM LEGOWEB_CATEGORIES WHERE SECTION_ID=" + SECTION_ID.ToString() + "  AND PARENT_CATEGORY_ID=0 ORDER BY ORDER_NUMBER ASC";
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
        public static void apply_ADMIN_ROLES_TO_CHILREN(int iPARENT_CATEGORY_ID, int iADMIN_LEVEL, string sADMIN_ROLES)
        {
            //không đệ qui chỉ được 1 cấp con
            String connString = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(connString))
            {
                try
                {
                    String strCommandText;
                    SqlCommand objCommand;
                    SqlParameter objParam;

                    strCommandText = "UPDATE LEGOWEB_CATEGORIES SET ADMIN_LEVEL=@_ADMIN_LEVEL, ADMIN_ROLES=@_ADMIN_ROLES WHERE PARENT_CATEGORY_ID=@_PARENT_CATEGORY_ID";
                    objCommand = new SqlCommand(strCommandText, Conn);
                    objCommand.CommandType = CommandType.Text;

                    objParam = objCommand.Parameters.Add(new SqlParameter("@_PARENT_CATEGORY_ID", SqlDbType.Int));
                    objParam.Direction = ParameterDirection.Input;
                    objParam.Value = iPARENT_CATEGORY_ID;

                    objParam = objCommand.Parameters.Add(new SqlParameter("@_ADMIN_LEVEL", SqlDbType.Int));
                    objParam.Direction = ParameterDirection.Input;
                    objParam.Value = iADMIN_LEVEL;

                    objParam = objCommand.Parameters.Add(new SqlParameter("@_ADMIN_ROLES", SqlDbType.NVarChar, 100));
                    objParam.Direction = ParameterDirection.Input;
                    objParam.Value = sADMIN_ROLES;

                    Conn.Open();
                    objCommand.ExecuteNonQuery();
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
        }
        public static void set_ADMIN_ROLES(int iCATEGORY_ID, string sADMIN_ROLES)
        {
            String connString = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(connString))
            {
                try
                {
                    String strCommandText;
                    SqlCommand objCommand;
                    SqlParameter objParam;

                    strCommandText = "UPDATE LEGOWEB_CATEGORIES SET ADMIN_ROLES=@_ADMIN_ROLES WHERE CATEGORY_ID=@_CATEGORY_ID";
                    objCommand = new SqlCommand(strCommandText, Conn);
                    objCommand.CommandType = CommandType.Text;

                    objParam = objCommand.Parameters.Add(new SqlParameter("@_ADMIN_ROLES", SqlDbType.NVarChar, 100));
                    objParam.Direction = ParameterDirection.Input;
                    objParam.Value = sADMIN_ROLES;

                    objParam = objCommand.Parameters.Add(new SqlParameter("@_CATEGORY_ID", SqlDbType.Int));
                    objParam.Direction = ParameterDirection.Input;
                    objParam.Value = iCATEGORY_ID;


                    Conn.Open();
                    objCommand.ExecuteNonQuery();
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
        }

        public static void addUpdate_CATEGORY(int iCATEGORY_ID, int iPARENT_CATEGORY_ID, int iSECTION_ID, string sCATEGORY_VI_TITLE, string sCATEGORY_EN_TITLE, string sCATEGORY_ALIAS, string sCATEGORY_TEMPLATE_NAME, string sCATEGORY_IMAGE_URL, int iMENU_ID, bool bIsPublic, int iAdminLevel, string sAdminRoles, string sSeoTitle, string sSeoDescription, string sSeoKeywords)
        {
            string connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            SqlConnection connection = new SqlConnection(connStr);
            try
            {
                string strCommandName;
                SqlCommand objCommand;
                SqlParameter objParam;

                strCommandName = "sp_LEGOWEB_CATEGORIES_ADDUPDATE";
                objCommand = new SqlCommand(strCommandName, connection);
                objCommand.CommandType = CommandType.StoredProcedure;
                //Set the Parameters

                objParam = objCommand.Parameters.Add(new SqlParameter("@_CATEGORY_ID", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iCATEGORY_ID;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_PARENT_CATEGORY_ID", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iPARENT_CATEGORY_ID;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_SECTION_ID", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iSECTION_ID;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_CATEGORY_VI_TITLE", SqlDbType.NVarChar, 250));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sCATEGORY_VI_TITLE;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_CATEGORY_EN_TITLE", SqlDbType.NVarChar, 250));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sCATEGORY_EN_TITLE;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_CATEGORY_ALIAS", SqlDbType.NVarChar, 250));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sCATEGORY_ALIAS;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_CATEGORY_TEMPLATE_NAME", SqlDbType.NVarChar, 50));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sCATEGORY_TEMPLATE_NAME;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_CATEGORY_IMAGE_URL", SqlDbType.NVarChar, 250));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sCATEGORY_IMAGE_URL;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_MENU_ID", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iMENU_ID;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_IS_PUBLIC", SqlDbType.Bit));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = bIsPublic;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_ADMIN_LEVEL", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iAdminLevel;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_ADMIN_ROLES", SqlDbType.NVarChar, 250));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sAdminRoles;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_SEO_TITLE", SqlDbType.NVarChar, 100));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sSeoTitle;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_SEO_DESCRIPTION", SqlDbType.NVarChar, 255));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sSeoDescription;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_SEO_KEYWORDS", SqlDbType.NVarChar, 255));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sSeoKeywords;

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

        public static DataSet get_CATEGORY_BY_ID(int iCategoryID)
        {
            DataSet retData = new DataSet();
            String connString = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(connString))
            {
                try
                {
                    String strCommandText;
                    SqlCommand objCommand;

                    strCommandText = "SELECT * FROM LEGOWEB_CATEGORIES WHERE CATEGORY_ID=" + iCategoryID.ToString();

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

        public static bool is_CATEGORY_ID_EXIST(int iCategoryId)
        {
            String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    SqlCommand cmdReader = new SqlCommand("SELECT CATEGORY_ID FROM LEGOWEB_CATEGORIES WHERE CATEGORY_ID=" + iCategoryId.ToString(), conn);
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
        public static DataSet get_CATEGORY_BY_PARENT_ID(int iParentCategoryID, int iSectionId)
        {
            DataSet retData = new DataSet();
            String ConnString = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(ConnString))
            {
                try
                {
                    String strCommandText;
                    SqlCommand objCommand;
                    strCommandText = "SELECT * FROM LEGOWEB_CATEGORIES WHERE PARENT_CATEGORY_ID=" + iParentCategoryID.ToString() + (iParentCategoryID == 0 ? " AND SECTION_ID=" + iSectionId.ToString() : "") + " ORDER BY ORDER_NUMBER ASC";
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

        public static DataSet get_CATEGORY_BY_PARENT_ID(int iParentCategoryID)
        {
            DataSet retData = new DataSet();
            String ConnString = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(ConnString))
            {
                try
                {
                    String strCommandText;
                    SqlCommand objCommand;
                    strCommandText = "SELECT * FROM LEGOWEB_CATEGORIES WHERE PARENT_CATEGORY_ID=" + iParentCategoryID.ToString() + " ORDER BY ORDER_NUMBER ASC";
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
        public static void remove_CATEGORY(int iCATEGORY_ID)
        {
            string connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            SqlConnection connection = new SqlConnection(connStr);
            try
            {
                string strCommandName;
                SqlCommand objCommand;
                SqlParameter objParam;

                strCommandName = "sp_LEGOWEB_CATEGORIES_REMOVE";
                objCommand = new SqlCommand(strCommandName, connection);
                objCommand.CommandType = CommandType.StoredProcedure;
                //Set the Parameters

                objParam = objCommand.Parameters.Add(new SqlParameter("@_CATEGORY_ID", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iCATEGORY_ID;

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

        public static void update_CATEGORY_ORDER(int iCATEGORY_ID, int iORDER_NUMBER)
        {
            string connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            SqlConnection connection = new SqlConnection(connStr);
            try
            {
                string strCommandName;
                SqlCommand objCommand;
                SqlParameter objParam;

                strCommandName = "UPDATE LEGOWEB_CATEGORIES SET ORDER_NUMBER=@_ORDER_NUMBER WHERE CATEGORY_ID=@_CATEGORY_ID";
                objCommand = new SqlCommand(strCommandName, connection);
                objCommand.CommandType = CommandType.Text;
                //Set the Parameters

                objParam = objCommand.Parameters.Add(new SqlParameter("@_CATEGORY_ID", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iCATEGORY_ID;

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
        public static void publish_CATEGORY(int iCATEGORY_ID, bool bIsPublic)
        {
            string connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            SqlConnection connection = new SqlConnection(connStr);
            try
            {
                string strCommandName;
                SqlCommand objCommand;
                SqlParameter objParam;

                strCommandName = "UPDATE LEGOWEB_CATEGORIES SET IS_PUBLIC=@_IS_PUBLIC WHERE CATEGORY_ID=@_CATEGORY_ID";
                objCommand = new SqlCommand(strCommandName, connection);
                objCommand.CommandType = CommandType.Text;
                //Set the Parameters

                objParam = objCommand.Parameters.Add(new SqlParameter("@_CATEGORY_ID", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iCATEGORY_ID;

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

        public static void moveUp_CATEGORY(int iCategoryID)
        {
            DataSet catData = get_CATEGORY_BY_ID(iCategoryID);
            Int32 iParentCategoryId = (Int32)catData.Tables[0].Rows[0]["PARENT_CATEGORY_ID"];
            int iSectionId = int.Parse(catData.Tables[0].Rows[0]["SECTION_ID"].ToString());
            DataTable catOrder = null;

            if (iParentCategoryId == 0)
            {
                catOrder = get_CATEGORY_BY_PARENT_ID(0, iSectionId).Tables[0];
            }
            else
            {
                catOrder = get_CATEGORY_BY_PARENT_ID(iParentCategoryId).Tables[0];
            }

            for (int i = 0; i < catOrder.Rows.Count; i++)
            {
                if (i + 1 < catOrder.Rows.Count)
                {
                    if (((Int32)catOrder.Rows[i + 1]["CATEGORY_ID"]) == iCategoryID)
                    {
                        update_CATEGORY_ORDER((Int32)catOrder.Rows[i]["CATEGORY_ID"], i + 2);
                        update_CATEGORY_ORDER((Int32)catOrder.Rows[i + 1]["CATEGORY_ID"], i + 1);
                        i++;
                    }
                    else
                    {
                        update_CATEGORY_ORDER((Int32)catOrder.Rows[i]["CATEGORY_ID"], i + 1);
                    }
                }
            }
        }

        public static void moveDown_CATEGORY(int iCategoryID)
        {

            DataSet catData = get_CATEGORY_BY_ID(iCategoryID);
            Int32 iParentCategoryId = (Int32)catData.Tables[0].Rows[0]["PARENT_CATEGORY_ID"];
            int iSectionId = (int)catData.Tables[0].Rows[0]["SECTION_ID"];
            DataTable catOrder = null;

            if (iParentCategoryId == 0)
            {
                catOrder = get_CATEGORY_BY_PARENT_ID(0, iSectionId).Tables[0];
            }
            else
            {
                catOrder = get_CATEGORY_BY_PARENT_ID(iParentCategoryId).Tables[0];
            }
            for (int i = 0; i < catOrder.Rows.Count; i++)
            {
                if (((Int32)catOrder.Rows[i]["CATEGORY_ID"]) == iCategoryID)
                {
                    if (i + 1 < catOrder.Rows.Count)
                    {
                        update_CATEGORY_ORDER((Int32)catOrder.Rows[i]["CATEGORY_ID"], i + 2);
                        update_CATEGORY_ORDER((Int32)catOrder.Rows[i + 1]["CATEGORY_ID"], i + 1);
                        i++;
                    }
                    else
                    {
                        update_CATEGORY_ORDER((Int32)catOrder.Rows[i]["CATEGORY_ID"], i + 1);
                    }
                }
                else
                {
                    LegoWebAdmin.BusLogic.Categories.update_CATEGORY_ORDER((Int32)catOrder.Rows[i]["CATEGORY_ID"], i + 1);
                }
            }
        }

        public static string get_CATEGORY_TEMPLATE_NAME(int iCategoryID)
        {
            DataSet retData = new DataSet();
            String connString = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(connString))
            {
                try
                {
                    SqlCommand cmdReader = new SqlCommand("SELECT CATEGORY_TEMPLATE_NAME FROM LEGOWEB_CATEGORIES WHERE CATEGORY_ID=" + iCategoryID.ToString(), Conn);
                    Conn.Open();
                    SqlDataReader reader = cmdReader.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        string retStr = reader["CATEGORY_TEMPLATE_NAME"].ToString();
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

        public static Int32 get_Search_Count(int iCategoryId, int iParentCategoryId, int iSectionId)
        {
            String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(connStr))
            {
                try
                {
                    string strCommandName;
                    SqlCommand objCommand;
                    SqlParameter objParam;

                    strCommandName = "sp_LEGOWEB_CATEGORIES_SEARCH_COUNT";
                    objCommand = new SqlCommand(strCommandName, Conn);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    //Set the Parameters
                    if (iCategoryId > 0)
                    {
                        objParam = objCommand.Parameters.Add(new SqlParameter("@_CATEGORY_ID", SqlDbType.Int));
                        objParam.Direction = ParameterDirection.Input;
                        objParam.Value = iCategoryId;
                    }
                    if (iParentCategoryId > 0)
                    {
                        objParam = objCommand.Parameters.Add(new SqlParameter("@_PARENT_CATEGORY_ID", SqlDbType.Int));
                        objParam.Direction = ParameterDirection.Input;
                        objParam.Value = iParentCategoryId;
                    }
                    if (iSectionId > 0)
                    {
                        objParam = objCommand.Parameters.Add(new SqlParameter("@_SECTION_ID", SqlDbType.Int));
                        objParam.Direction = ParameterDirection.Input;
                        objParam.Value =iSectionId;
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

        public static DataSet get_Search_Page(int iCategoryId, int iParentCategoryId, int iSectionId, string sTabChars,int iPage, int iPageSize)
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


                    strCommandName = "sp_LEGOWEB_CATEGORIES_SEARCH_PAGE";
                    objCommand = new SqlCommand(strCommandName,Conn);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    //Set the Parameters
                    if (iCategoryId > 0)
                    {
                        objParam = objCommand.Parameters.Add(new SqlParameter("@_CATEGORY_ID", SqlDbType.Int));
                        objParam.Direction = ParameterDirection.Input;
                        objParam.Value = iCategoryId;
                    }
                    if (iParentCategoryId > 0)
                    {
                        objParam = objCommand.Parameters.Add(new SqlParameter("@_PARENT_CATEGORY_ID", SqlDbType.Int));
                        objParam.Direction = ParameterDirection.Input;
                        objParam.Value = iParentCategoryId;
                    }
                    if (iSectionId > 0)
                    {
                        objParam = objCommand.Parameters.Add(new SqlParameter("@_SECTION_ID", SqlDbType.Int));
                        objParam.Direction = ParameterDirection.Input;
                        objParam.Value = iSectionId;
                    }
                    if (iSelectRow > 0)
                    {
                        objParam = objCommand.Parameters.Add(new SqlParameter("@_SELECT_ROW_NUMBER", SqlDbType.Int));
                        objParam.Direction = ParameterDirection.Input;
                        objParam.Value = iSelectRow;                   
                    }
                    if (sTabChars != null)
                    {
                        objParam = objCommand.Parameters.Add(new SqlParameter("@_TAB_CHARS", SqlDbType.NVarChar,20));
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
    
    }
}
