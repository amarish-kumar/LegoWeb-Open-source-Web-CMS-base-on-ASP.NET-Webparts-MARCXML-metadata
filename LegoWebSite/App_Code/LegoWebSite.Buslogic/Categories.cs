using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using MarcXmlParserEx;

namespace LegoWebSite.Buslgic
{
    /// <summary>
    /// Summary description for Categories
    /// </summary>
    public static class Categories
    {
        public static DataSet get_CONTENT_CATEGORY_ID(int iSectionId, int iCategoryId)
        {
            DataSet dataset = new DataSet();
            String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    string strCommandText;
                    SqlCommand objCommand;
                    strCommandText = @" With hierarchy
                                        AS
                                            (
                                                SELECT *
                                                FROM LEGOWEB_CATEGORIES
                                                WHERE SECTION_ID=@_SECTION_ID AND PARENT_CATEGORY_ID=0 and CATEGORY_ID=@_CATEGORY_ID
                                                UNION ALL
                                                SELECT c.CATEGORY_ID,c.PARENT_CATEGORY_ID,c.SECTION_ID,c.CATEGORY_VI_TITLE,c.CATEGORY_EN_TITLE,c.CATEGORY_TEMPLATE_NAME,c.ORDER_NUMBER,c.IS_PUBLIC
                                                FROM LEGOWEB_CATEGORIES AS c INNER JOIN hierarchy AS p ON c.PARENT_CATEGORY_ID = p.CATEGORY_ID
                                            )	
                                        select * from hierarchy";

                    objCommand = new SqlCommand(strCommandText, conn);
                    objCommand.CommandType = CommandType.Text;
                    objCommand.Parameters.Add(new SqlParameter("@_SECTION_ID", SqlDbType.Int));
                    objCommand.Parameters["@_SECTION_ID"].Value = iSectionId;
                    objCommand.Parameters.Add(new SqlParameter("@_CATEGORY_ID", SqlDbType.Int));
                    objCommand.Parameters["@_CATEGORY_ID"].Value = iCategoryId;
                    SqlDataAdapter adap = new SqlDataAdapter(objCommand);
                    conn.Open();
                    adap.Fill(dataset, "Table");
                    conn.Close();
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
            return dataset;
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

                    strCommandText = "SELECT *,(SELECT COUNT(META_CONTENT_ID) FROM LEGOWEB_META_CONTENTS WHERE LEGOWEB_META_CONTENTS.CATEGORY_ID=LEGOWEB_CATEGORIES.CATEGORY_ID AND LANG_CODE=N'" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + "') AS META_CONTENT_COUNT FROM LEGOWEB_CATEGORIES WHERE CATEGORY_ID=" + iCategoryID.ToString();

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
        public static DataSet get_CATEGORY_BY_PARENT_ID(int iParentCatID, int iNumberOfRecord, int iSectionId)
        {
            DataSet retData = new DataSet();
            String ConnString = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(ConnString))
            {
                try
                {
                    String strCommandText; 
                    SqlCommand objCommand;
                    strCommandText = @"
                                        With hierarchy
                                            AS
                                            (
                                                SELECT *
                                                FROM LEGOWEB_CATEGORIES
                                                WHERE SECTION_ID=@_SECTION_ID AND PARENT_CATEGORY_ID = @_PARENT_CATEGORY_ID
                                             )	
                                        select TOP(@_NUMBER_OF_RECORD)* FROM hierarchy";

                    objCommand = new SqlCommand(strCommandText, Conn);
                    objCommand.CommandType = CommandType.Text;

                    objCommand.Parameters.Add(new SqlParameter("@_SECTION_ID", SqlDbType.Int));
                    objCommand.Parameters["@_SECTION_ID"].Value = iSectionId;
                    objCommand.Parameters.Add(new SqlParameter("@_NUMBER_OF_RECORD", SqlDbType.Int));
                    objCommand.Parameters["@_NUMBER_OF_RECORD"].Value = iNumberOfRecord;
                    objCommand.Parameters.Add(new SqlParameter("@_PARENT_CATEGORY_ID", SqlDbType.Int));
                    objCommand.Parameters["@_PARENT_CATEGORY_ID"].Value = iParentCatID;

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
                        objParam.Value = iSectionId;
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
        public static DataSet get_Search_Page(int iCategoryId, int iParentCategoryId, int iSectionId, string sTabChars, int iPage, int iPageSize)
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
        /// <summary>
        /// Get categories with specified menu id
        /// </summary>
        public static int get_CATEGORY_ID_BY_MENU_ID(int iMenuId)
        {
            String connString = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(connString))
            {
                try
                {
                    SqlCommand cmdReader = new SqlCommand("SELECT CATEGORY_ID FROM LEGOWEB_CATEGORIES WHERE MENU_ID=" + iMenuId.ToString(),Conn);
                    Conn.Open();
                    SqlDataReader reader = cmdReader.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        int iCategoryId = int.Parse(reader["CATEGORY_ID"].ToString());
                        Conn.Close();
                        return iCategoryId;
                    }
                    else
                    {
                        Conn.Close();
                        return -1;
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
        /// <summary>
        /// Get direct chilren of one category
        /// </summary>
        public static DataSet get_CATEGORY_CHILREN(int iParentCategoryID)
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
        public static bool is_CATEGORY_EXIST(int iCategoryId)
        { 
            String connString = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(connString))
            {
                try
                {
                    SqlCommand cmdReader = new SqlCommand("SELECT * FROM LEGOWEB_CATEGORIES WHERE CATEGORY_ID=" + iCategoryId.ToString(), Conn);
                    Conn.Open();
                    SqlDataReader reader = cmdReader.ExecuteReader();
                    if (reader.HasRows)
                    {
                        Conn.Close();
                        return true;
                    }
                    else
                    {
                        Conn.Close();
                        return false;
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
        public static string get_CATEGORY_TEMPLATE_NAME(int iCategoryID)
        {
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

        public static string get_CAT_CATEGORY_TREE_XML(int iCategoryID)
        {

            SqlCommand objCommand;
            SqlParameter objParam;
            String outXml = "";

            String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            string strSQL = @"SELECT      
	                            Category_Id AS '@Category_Id',      
	                            Parent_Category_Id AS '@Parent_Category_Id',     
		                        Category_Vi_Title + '(' + cast((Select COUNT(META_CONTENT_ID) From LEGOWEB_META_CONTENTS Where LEGOWEB_META_CONTENTS.IS_PUBLIC=1 AND LEGOWEB_META_CONTENTS.CATEGORY_ID=c.Category_Id) as nvarchar(20)) + ')' as '@Category_Vi_Title',             
		                        Category_En_Title + '(' + cast((Select COUNT(META_CONTENT_ID) From LEGOWEB_META_CONTENTS Where LEGOWEB_META_CONTENTS.IS_PUBLIC=1 AND LEGOWEB_META_CONTENTS.CATEGORY_ID=c.Category_Id) as nvarchar(20)) + ')' as '@Category_En_Title',             
	                            dbo.SelectChildCategoryXml(Category_Id,0)      
                                FROM LEGOWEB_CATEGORIES AS c
                                WHERE Parent_Category_Id=@_ROOT_PARENT_ID AND Is_Public=1 FOR XML PATH ('category')";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    objCommand = new SqlCommand(strSQL, conn);
                    objCommand.CommandType = CommandType.Text;

                    objParam = objCommand.Parameters.Add(new SqlParameter("@_ROOT_PARENT_ID", SqlDbType.Int));
                    objParam.Direction = ParameterDirection.Input;
                    objParam.Value = iCategoryID;

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

        public static string get_NavigatePath(int iCategroryId, string linkUrl)
        {
            string sNaviPath = "";
            string sNaviFormat = "<a href='{0}?catid={1}'>{2}</a>";

            DataTable CatInfo = LegoWebSite.Buslgic.Categories.get_CATEGORY_BY_ID(iCategroryId).Tables[0];
            int icatid = iCategroryId;
            string scatname = CatInfo.Rows[0]["CATEGORY_" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToUpper() + "_TITLE"].ToString();
            int ipcatid = int.Parse(CatInfo.Rows[0]["PARENT_CATEGORY_ID"].ToString());
            sNaviPath = string.Format(sNaviFormat, linkUrl, icatid, scatname);
            while (ipcatid > 0)
            {
                iCategroryId = ipcatid;
                CatInfo = LegoWebSite.Buslgic.Categories.get_CATEGORY_BY_ID(iCategroryId).Tables[0];
                icatid = iCategroryId;
                scatname = CatInfo.Rows[0]["CATEGORY_" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToUpper() + "_TITLE"].ToString();
                sNaviPath = string.Format(sNaviFormat, linkUrl, icatid, scatname) + "<img src='images/navigate.gif' border='0' /> " + sNaviPath;
                ipcatid = int.Parse(CatInfo.Rows[0]["PARENT_CATEGORY_ID"].ToString());
            }
            return sNaviPath;
        }

       
     }

    
    }
