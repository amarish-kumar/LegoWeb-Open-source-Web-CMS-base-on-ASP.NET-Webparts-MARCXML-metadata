// ----------------------------------------------------------------------
// <copyright file="MetaContents.cs" package="LEGOWEB">
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
    /// Summary description for MetaContents
    /// </summary>
    public static class MetaContents
    {
        public static Int32 save_META_CONTENTS_XML(string sMARCXML, string sUSER)
        {
            Int32 retId = 0;
            LegoWebAdmin.DataProvider.ContentEditorDataHelper contentObj = new LegoWebAdmin.DataProvider.ContentEditorDataHelper();
            contentObj.load_Xml(sMARCXML);
            int iCategoryId=contentObj.CategoryID;
            string sLangCode=contentObj.LangCode;
            string sTitle=contentObj.Datafields.Datafield("245").Subfields.Subfield("a").Value;
            string sAlias = contentObj.Alias;
            int iRecordStatus=contentObj.RecordStatus;
            int iAccessLevel=contentObj.AccessLevel;
            int iImportantLevel = contentObj.ImportantLevel;
            retId = contentObj.MetaContentID;
            string sLeader = contentObj.Leader;

            contentObj.Sort();
            bool isUpdate = false;
            if (is_META_CONTENTS_EXIST(retId))
            {
                if (string.IsNullOrEmpty(sAlias))
                {
                    sAlias = CommonUtility.convert_TitleToAlias(sTitle);
                    string orgAlias = sAlias;
                    int iversion = 1;
                    while (is_META_CONTENT_ALIAS_EXIST(sAlias, retId))
                    {
                        sAlias = String.Format("{0}_{1}", orgAlias, iversion);
                        iversion++;
                    }
                }
                //update meta content process
                update_META_CONTENT(retId, iCategoryId, sLeader, sLangCode, sTitle,sAlias, iRecordStatus, iAccessLevel,iImportantLevel, sUSER);
                isUpdate = true;
            }
            else
            {
                if (string.IsNullOrEmpty(sAlias))
                {
                    sAlias = CommonUtility.convert_TitleToAlias(sTitle);
                    string orgAlias = sAlias;
                    int iversion = 1;
                    while (is_META_CONTENT_ALIAS_EXIST(sAlias, 0))
                    {
                        sAlias = String.Format("{0}_{1}", orgAlias, iversion);
                        iversion++;
                    }
                }
                //insert meta content process
                retId = insert_META_CONTENTS(iCategoryId, sLeader, sLangCode, sTitle,sAlias, iRecordStatus, iAccessLevel,iImportantLevel, sUSER);
                isUpdate = false;
            }

            CDatafields Dfs = contentObj.Datafields;
            for (int i = 0; i < Dfs.Count; i++)
            {
                CDatafield Df = Dfs.Datafield(i);
                CSubfields Sfs = Df.Subfields;

                for (int j = 0; j < Sfs.Count; j++)
                {
                    CSubfield Sf = Sfs.Subfield(j);
                    int iID = int.Parse("0" + Sf.ID);
                    switch (Sf.Type)
                    {
                        case "DATE":
                            if (Sf.Value != string.Empty)
                            {
                                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("vi-VN");
                                DateTime myDate = Convert.ToDateTime(Sf.Value, culture);
                                if (isUpdate && MetaContentDates.is_META_CONTENT_DATE_EXIST(iID))
                                {
                                    MetaContentDates.update_META_CONTENT_DATES(iID, int.Parse(Df.Tag), i, Sf.Code, myDate.ToString("yyyy-MMM-dd"), true, 0, sUSER);
                                }
                                else
                                {

                                    MetaContentDates.insert_META_CONTENT_DATES(retId, int.Parse(Df.Tag), i, Sf.Code,myDate.ToString("yyyy-MMM-dd"), true, 0, sUSER);
                                }
                            }
                            break;
                        case "BOOLEAN":
                            if (isUpdate && MetaContentBooleans.is_META_CONTENT_BOOLEAN_EXIST(iID))
                            {
                                MetaContentBooleans.update_META_CONTENT_BOOLEANS(iID, int.Parse(Df.Tag), i, Sf.Code, Convert.ToBoolean(Sf.Value), true, 0, sUSER);
                            }
                            else
                            {
                                MetaContentBooleans.insert_META_CONTENT_BOOLEANS(retId, int.Parse(Df.Tag), i, Sf.Code, Convert.ToBoolean(Sf.Value), true, 0, sUSER);
                            }
                            break;

                        case "NUMBER":
                            if (isUpdate && MetaContentNumbers.is_META_CONTENT_NUMBER_EXIST(iID))
                            {
                                MetaContentNumbers.update_META_CONTENT_NUMBERS(iID, int.Parse(Df.Tag), i, Sf.Code,(String.IsNullOrEmpty(Sf.Value)?0:Convert.ToDecimal(Sf.Value,new System.Globalization.CultureInfo("en-US"))), true, 0, sUSER);
                            }
                            else
                            {
                                MetaContentNumbers.insert_META_CONTENT_NUMBERS(retId, int.Parse(Df.Tag), i, Sf.Code, double.Parse("0" + Sf.Value), true, 0, sUSER);
                            }
                            break;
                        case "TEXT":
                            if (isUpdate && MetaContentTexts.is_META_CONTENT_TEXT_EXIST(iID))
                            {
                                MetaContentTexts.update_META_CONTENT_TEXTS(iID, int.Parse(Df.Tag), i, Sf.Code, Sf.Value, true, 0, sUSER);
                            }
                            else
                            {
                                MetaContentTexts.insert_META_CONTENT_TEXTS(retId, int.Parse(Df.Tag), i, Sf.Code, Sf.Value, true, 0, sUSER);
                            }
                            break;
                        case "NTEXT":
                            if (isUpdate && MetaContentNTexts.is_META_CONTENT_NTEXT_EXIST(iID))
                            {
                                MetaContentNTexts.update_META_CONTENT_NTEXTS(iID, int.Parse(Df.Tag), i, Sf.Code, Sf.Value, true, 0, sUSER);
                            }
                            else
                            {
                                MetaContentNTexts.insert_META_CONTENT_NTEXTS(retId, int.Parse(Df.Tag), i, Sf.Code, Sf.Value, true, 0, sUSER);
                            }
                            break;
                    }
                }
            }
           return retId;
        }


        public static String get_META_CONTENT_MARCXML(Int32 iMETA_CONTENT_ID, int iSCOPE)
        {
            LegoWebAdmin.DataProvider.ContentEditorDataHelper ContentObject = new LegoWebAdmin.DataProvider.ContentEditorDataHelper();
            CControlfield Cf = new CControlfield();
            CDatafield Df = new CDatafield();
            CSubfield Sf = new CSubfield();

            String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    string strCommandName;
                    SqlCommand objCommand;
                    SqlParameter objParam;

                    strCommandName = "sp_LEGOWEB_META_CONTENTS_GET_FOR_XML";
                    objCommand = new SqlCommand(strCommandName, conn);
                    objCommand.CommandType = CommandType.StoredProcedure;

                    objParam = objCommand.Parameters.Add(new SqlParameter("@_META_CONTENT_ID", SqlDbType.Int));
                    objParam.Direction = ParameterDirection.Input;
                    objParam.Value = iMETA_CONTENT_ID;

                    objParam = objCommand.Parameters.Add(new SqlParameter("@_SCOPE", SqlDbType.Int));
                    objParam.Direction = ParameterDirection.Input;
                    objParam.Value = iSCOPE;

                    conn.Open();
                    SqlDataReader reader = objCommand.ExecuteReader();
                    int iTag=0;
                    int iCurrentTagIndex=-1;                    
                    while (reader.Read()) //start first row
                    {
                            iTag=int.Parse(reader["TAG"].ToString());
                            if (iTag < 10)//controlfield only one per row
                            {
                                iCurrentTagIndex = int.Parse(reader["TAG_INDEX"].ToString());
                                Cf.ReConstruct();
                                Cf.ID = reader["ID"].ToString();
                                Cf.Tag = iTag.ToString("000");
                                Cf.Type = reader["SUBFIELD_TYPE"].ToString();
                                if (Cf.Type == "NUMBER")
                                {
                                    Decimal dvalue = Convert.ToDecimal(reader["SUBFIELD_VALUE"].ToString(), new System.Globalization.CultureInfo("en-US"));
                                    if (dvalue != 0)
                                    {
                                        Cf.Value = String.Format("{0:0.##}", dvalue);
                                    }
                                }
                                else
                                {
                                    Cf.Value = reader["SUBFIELD_VALUE"].ToString();
                                }
                                ContentObject.Controlfields.Add(Cf);
                            }
                            else  //
                            {
                                if (iCurrentTagIndex == int.Parse(reader["TAG_INDEX"].ToString()))
                                {
                                    Sf.ReConstruct();
                                    Sf.ID = reader["ID"].ToString();
                                    Sf.Code = reader["SUBFIELD_CODE"].ToString();
                                    Sf.Type = reader["SUBFIELD_TYPE"].ToString();
                                    if (Sf.Type == "NUMBER")
                                    {
                                        Decimal dvalue = Convert.ToDecimal(reader["SUBFIELD_VALUE"].ToString(), new System.Globalization.CultureInfo("en-US"));
                                        if (dvalue != 0)
                                        {
                                            Sf.Value = String.Format("{0:0.##}", dvalue);
                                        }
                                    }
                                    else
                                    {
                                        Sf.Value = reader["SUBFIELD_VALUE"].ToString();
                                    }
                                    Df.Subfields.Add(Sf);
                                }
                                else
                                {
                                    if (Df.Subfields.Count > 0) ContentObject.Datafields.Add(Df);
                                    Df.ReConstruct();
                                    Df.Tag = int.Parse(reader["TAG"].ToString()).ToString("000");
                                    iCurrentTagIndex = int.Parse(reader["TAG_INDEX"].ToString());
                                    Sf.ReConstruct();
                                    Sf.ID = reader["ID"].ToString();
                                    Sf.Code = reader["SUBFIELD_CODE"].ToString();
                                    Sf.Type = reader["SUBFIELD_TYPE"].ToString();
                                    if (Sf.Type == "NUMBER")
                                    {
                                        Decimal dvalue = Convert.ToDecimal(reader["SUBFIELD_VALUE"].ToString(), new System.Globalization.CultureInfo("en-US"));
                                        if (dvalue != 0)
                                        {
                                            Sf.Value = String.Format("{0:0.##}", dvalue);
                                        }
                                    }
                                    else
                                    {
                                        Sf.Value = reader["SUBFIELD_VALUE"].ToString();
                                    }
                                    Df.Subfields.Add(Sf);                                
                                }                                
                            }
                    }
                    if (Df.Subfields.Count > 0) ContentObject.Datafields.Add(Df);
                    Df.ReConstruct();
                    reader.Close();  
                    //end read datafield


                    string strCommandName0;
                    SqlCommand objCommand0;
                    SqlParameter objParam0;

                    strCommandName0 = "SELECT * FROM LEGOWEB_META_CONTENTS WHERE META_CONTENT_ID=" + iMETA_CONTENT_ID.ToString();
                    objCommand0 = new SqlCommand(strCommandName0, conn);
                    objCommand0.CommandType = CommandType.Text;
                    SqlDataReader reader0 = objCommand0.ExecuteReader();
                    reader0.Read();
                    ContentObject.MetaContentID = iMETA_CONTENT_ID;
                    ContentObject.CategoryID =Convert.ToInt16(reader0["CATEGORY_ID"]);
                    ContentObject.Alias = reader0["META_CONTENT_ALIAS"].ToString();
                    ContentObject.Leader = reader0["LEADER"].ToString();
                    ContentObject.RecordStatus =int.Parse(reader0["RECORD_STATUS"].ToString());
                    ContentObject.AccessLevel =Convert.ToInt16(reader0["ACCESS_LEVEL"]);
                    ContentObject.ImportantLevel = Convert.ToInt16(reader0["IMPORTANT_LEVEL"]);
                    ContentObject.LangCode = reader0["LANG_CODE"].ToString();
                    ContentObject.EntryDate =Convert.ToDateTime(reader0["CREATED_DATE"]).ToString("yyyy-MMM-dd hh:mm:ss");
                    ContentObject.Creator = reader0["CREATED_USER"].ToString();
                    ContentObject.ModifyDate = Convert.ToDateTime(reader0["MODIFIED_DATE"]).ToString("yyyy-MMM-dd hh:mm:ss");
                    ContentObject.Modifier = reader0["MODIFIED_USER"].ToString();
                    reader0.Close();
                    conn.Close();                   
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
            ContentObject.Sort();
            return ContentObject.OuterXml;
        }
        public static bool is_META_CONTENTS_EXIST(Int32 iID)
        {
            String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    SqlCommand cmdReader = new SqlCommand("SELECT META_CONTENT_ID FROM LEGOWEB_META_CONTENTS WHERE META_CONTENT_ID=" + iID.ToString(), conn);
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

        public static bool is_META_CONTENT_ALIAS_EXIST(string sAlias,Int32 iID)
        {
            String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    SqlCommand cmdReader = new SqlCommand("SELECT META_CONTENT_ID FROM LEGOWEB_META_CONTENTS WHERE META_CONTENT_ALIAS=N'" + sAlias + "' AND META_CONTENT_ID<>" + iID.ToString(), conn);
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



        public static Int32 insert_META_CONTENTS(int iCATEGORY_ID,string sLEADER, string sLANG_CODE,string sMETA_CONTENT_TITLE, string sMETA_CONTENT_ALIAS,int iRECORD_STATUS, int iACCESS_LEVEL,int iIMPORTANT_LEVEL, string sCREATED_USER)
        {
            string connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            SqlConnection connection = new SqlConnection(connStr);
            try
            {
                string strCommandName;
                SqlCommand objCommand;
                SqlParameter objParam;

                strCommandName = "sp_LEGOWEB_META_CONTENTS_INSERT";
                objCommand = new SqlCommand(strCommandName, connection);
                objCommand.CommandType = CommandType.StoredProcedure;
                //Set the Parameters

                Int32 iMETA_CONTENT_ID = 0;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_CATEGORY_ID", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iCATEGORY_ID;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_LEADER", SqlDbType.NVarChar, 24));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value =sLEADER;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_LANG_CODE", SqlDbType.NVarChar, 3));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sLANG_CODE;                

                objParam = objCommand.Parameters.Add(new SqlParameter("@_META_CONTENT_TITLE", SqlDbType.NVarChar, 250));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sMETA_CONTENT_TITLE;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_META_CONTENT_ALIAS", SqlDbType.NVarChar, 250));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sMETA_CONTENT_ALIAS;    

                objParam = objCommand.Parameters.Add(new SqlParameter("@_RECORD_STATUS", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iRECORD_STATUS;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_ACCESS_LEVEL", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iACCESS_LEVEL;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_IMPORTANT_LEVEL", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iIMPORTANT_LEVEL;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_CREATED_USER", SqlDbType.NVarChar, 30));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sCREATED_USER;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_META_CONTENT_ID", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Output;

                connection.Open();
                objCommand.ExecuteNonQuery();
                //'Get the Out parameter from the Stored Procedure
                iMETA_CONTENT_ID = Convert.ToInt32((objCommand.Parameters["@_META_CONTENT_ID"].Value.ToString()));

                connection.Close();
                return iMETA_CONTENT_ID;
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

        public static void update_META_CONTENT(int iMETA_CONTENT_ID,int iCATEGORY_ID, string sLEADER, string sLANG_CODE,string sMETA_CONTENT_TITLE,string sMETA_CONTENT_ALIAS, int iRECORD_STATUS, int iACCESS_LEVEL,int iIMPORTANT_LEVEL, string sMODIFIED_USER)
        {
            string connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            SqlConnection connection = new SqlConnection(connStr);
            try
            {
                string strCommandName;
                SqlCommand objCommand;
                SqlParameter objParam;

                strCommandName = "sp_LEGOWEB_META_CONTENTS_UPDATE";
                objCommand = new SqlCommand(strCommandName, connection);
                objCommand.CommandType = CommandType.StoredProcedure;
                //Set the Parameters

                objParam = objCommand.Parameters.Add(new SqlParameter("@_META_CONTENT_ID", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iMETA_CONTENT_ID;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_CATEGORY_ID", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iCATEGORY_ID;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_LEADER", SqlDbType.NVarChar, 24));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sLEADER;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_LANG_CODE", SqlDbType.NVarChar, 3));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sLANG_CODE;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_META_CONTENT_TITLE", SqlDbType.NVarChar, 250));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sMETA_CONTENT_TITLE;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_META_CONTENT_ALIAS", SqlDbType.NVarChar, 250));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sMETA_CONTENT_ALIAS;                

                objParam = objCommand.Parameters.Add(new SqlParameter("@_RECORD_STATUS", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iRECORD_STATUS;
                
                objParam = objCommand.Parameters.Add(new SqlParameter("@_ACCESS_LEVEL", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iACCESS_LEVEL;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_IMPORTANT_LEVEL", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iIMPORTANT_LEVEL;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_MODIFIED_USER", SqlDbType.NVarChar, 30));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sMODIFIED_USER;

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

        public static void Increase_ReadCount(int iMetaContentID)
        {
            String connString = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(connString))
            {
                try
                {
                    String strCommandText;
                    SqlCommand objCommand;

                    strCommandText = "UPDATE LEGOWEB_META_CONTENTS SET READ_COUNT=READ_COUNT + 1 WHERE META_CONTENT_ID=" + iMetaContentID.ToString();

                    objCommand = new SqlCommand(strCommandText, Conn);
                    objCommand.CommandType = CommandType.Text;
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

        public static Int32 get_Admin_Search_Count(int iSectionId,int iRootCategoryId)
        {
            if (iRootCategoryId > 0) iSectionId = 0;// no need to pass sectionid if have categoryid

            String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(connStr))
            {
                try
                {
                    string strCommandName;
                    SqlCommand objCommand;
                    SqlParameter objParam;

                    strCommandName = "sp_LEGOWEB_META_CONTENTS_ADMIN_SEARCH_COUNT";
                    objCommand = new SqlCommand(strCommandName, Conn);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    //Set the Parameters
                    if (iSectionId > 0)
                    {
                        objParam = objCommand.Parameters.Add(new SqlParameter("@_SECTION_ID", SqlDbType.Int));
                        objParam.Direction = ParameterDirection.Input;
                        objParam.Value = iSectionId;
                    }
                    if (iRootCategoryId > 0)
                    {
                        objParam = objCommand.Parameters.Add(new SqlParameter("@_ROOT_CATEGORY_ID", SqlDbType.Int));
                        objParam.Direction = ParameterDirection.Input;
                        objParam.Value = iRootCategoryId;
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

        public static DataSet get_Admin_Search_Page(int iSectionId, int iRootCategoryId, int iPage, int iPageSize)
        {
            if (iRootCategoryId > 0) iSectionId = 0;// no need to pass sectionid if have categoryid

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

                    strCommandName = "sp_LEGOWEB_META_CONTENTS_ADMIN_SEARCH_PAGE";
                    objCommand = new SqlCommand(strCommandName, Conn);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    //Set the Parameters
                    if (iSectionId > 0)
                    {
                        objParam = objCommand.Parameters.Add(new SqlParameter("@_SECTION_ID", SqlDbType.Int));
                        objParam.Direction = ParameterDirection.Input;
                        objParam.Value = iSectionId;
                    }
                    if (iRootCategoryId > 0)
                    {
                        objParam = objCommand.Parameters.Add(new SqlParameter("@_ROOT_CATEGORY_ID", SqlDbType.Int));
                        objParam.Direction = ParameterDirection.Input;
                        objParam.Value = iRootCategoryId;
                    }
                    if (iSelectRow > 0)
                    {
                        objParam = objCommand.Parameters.Add(new SqlParameter("@_SELECT_ROW_NUMBER", SqlDbType.Int));
                        objParam.Direction = ParameterDirection.Input;
                        objParam.Value = iSelectRow;
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

        public static DataSet get_Trash_META_CONTENTS()
        {
            DataSet myTrashData = new DataSet();
            String connString = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;

            using (SqlConnection Conn = new SqlConnection(connString))
            {
                try
                {
                    string strCommandName;
                    SqlCommand objCommand;
                    SqlParameter objParam;

                    strCommandName = "sp_LEGOWEB_META_CONTENTS_TRASH";
                    objCommand = new SqlCommand(strCommandName, Conn);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    //Set the Parameters
                    SqlDataAdapter adap = new SqlDataAdapter(objCommand);
                    Conn.Open();
                    adap.Fill(myTrashData, "Trash");
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
            return myTrashData;
        }


        public static Int32 get_META_CONTENT_CATEGORY_ID(int iMetaContentId)
        {
            String connString = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(connString))
            {
                try
                {
                    SqlCommand cmdReader = new SqlCommand("SELECT CATEGORY_ID FROM LEGOWEB_META_CONTENTS WHERE META_CONTENT_ID=" + iMetaContentId.ToString(), Conn);
                    Conn.Open();
                    SqlDataReader reader = cmdReader.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        Int32 retID =(Int32)reader["CATEGORY_ID"];
                        Conn.Close();
                        return retID;
                    }
                    else
                    {
                        Conn.Close();
                        return 0;
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

        public static void movetrash_META_CONTENTS(int iMETA_CONTENT_ID)
        {
            string connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            SqlConnection connection = new SqlConnection(connStr);
            try
            {
                string strCommandName;
                SqlCommand objCommand;
                SqlParameter objParam;

                strCommandName = "UPDATE LEGOWEB_META_CONTENTS SET RECORD_STATUS=-1 WHERE META_CONTENT_ID=@_META_CONTENT_ID";
                objCommand = new SqlCommand(strCommandName, connection);
                objCommand.CommandType = CommandType.Text;
                //Set the Parameters

                objParam = objCommand.Parameters.Add(new SqlParameter("@_META_CONTENT_ID", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iMETA_CONTENT_ID;

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

        public static void delete_META_CONTENTS(int iMETA_CONTENT_ID)
        {
            string connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            SqlConnection connection = new SqlConnection(connStr);
            try
            {
                string strCommandName;
                SqlCommand objCommand;
                SqlParameter objParam;

                strCommandName = "sp_LEGOWEB_META_CONTENTS_DELETE";
                objCommand = new SqlCommand(strCommandName, connection);
                objCommand.CommandType = CommandType.StoredProcedure;
                //Set the Parameters

                objParam = objCommand.Parameters.Add(new SqlParameter("@_META_CONTENT_ID", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iMETA_CONTENT_ID;

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
      
        public static void restore_META_CONTENTS(int iMETA_CONTENT_ID)
        {
            string connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            SqlConnection connection = new SqlConnection(connStr);
            try
            {
                string strCommandName;
                SqlCommand objCommand;
                SqlParameter objParam;

                strCommandName = "UPDATE LEGOWEB_META_CONTENTS SET RECORD_STATUS=0 WHERE META_CONTENT_ID=@_META_CONTENT_ID";
                objCommand = new SqlCommand(strCommandName, connection);
                objCommand.CommandType = CommandType.Text;
                //Set the Parameters

                objParam = objCommand.Parameters.Add(new SqlParameter("@_META_CONTENT_ID", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iMETA_CONTENT_ID;

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

        public static void remove_META_CONTENT_SUBFIELD(int iSUBFIELD_ID)
        {
            string connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            SqlConnection connection = new SqlConnection(connStr);
            try
            {
                string strCommandName;
                SqlCommand objCommand;
                SqlParameter objParam;

                strCommandName = "sp_LEGOWEB_META_CONTENTS_SUBFIELD_REMOVE";
                objCommand = new SqlCommand(strCommandName, connection);
                objCommand.CommandType = CommandType.StoredProcedure;
                //Set the Parameters

                objParam = objCommand.Parameters.Add(new SqlParameter("@_SUBFIELD_ID", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iSUBFIELD_ID;

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

        public static DataSet get_META_CONTENT_BY_CATEGORY_ID(int iCategoryId)
        {
            DataSet retData = new DataSet();
            String ConnString = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(ConnString))
            {
                try
                {
                    String strCommandText;
                    SqlCommand objCommand;
                    strCommandText = "SELECT * FROM LEGOWEB_META_CONTENTS WHERE CATEGORY_ID=" + iCategoryId.ToString() + " ORDER BY ORDER_NUMBER ASC";
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

        public static void update_META_CONTENT_ORDER(int iMETA_CONTENT_ID, int iORDER_NUMBER)
        {
            string connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            SqlConnection connection = new SqlConnection(connStr);
            try
            {
                string strCommandName;
                SqlCommand objCommand;
                SqlParameter objParam;

                strCommandName = "UPDATE LEGOWEB_META_CONTENTS SET ORDER_NUMBER=@_ORDER_NUMBER WHERE META_CONTENT_ID=@_META_CONTENT_ID";
                objCommand = new SqlCommand(strCommandName, connection);
                objCommand.CommandType = CommandType.Text;
                //Set the Parameters

                objParam = objCommand.Parameters.Add(new SqlParameter("@_META_CONTENT_ID", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iMETA_CONTENT_ID;

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

        public static void moveUp_META_CONTENT(int iMetaContentID)
        {
            int iCategoryId = get_META_CONTENT_CATEGORY_ID(iMetaContentID);
            DataTable contentOrder = get_META_CONTENT_BY_CATEGORY_ID(iCategoryId).Tables[0];

            for (int i = 0; i < contentOrder.Rows.Count; i++)
            {
                if (i + 1 < contentOrder.Rows.Count)
                {
                    if (((Int32)contentOrder.Rows[i + 1]["META_CONTENT_ID"]) == iMetaContentID)
                    {
                        update_META_CONTENT_ORDER((Int32)contentOrder.Rows[i]["META_CONTENT_ID"], i + 2);
                        update_META_CONTENT_ORDER((Int32)contentOrder.Rows[i + 1]["META_CONTENT_ID"], i + 1);
                        i++;
                    }
                    else
                    {
                        update_META_CONTENT_ORDER((Int32)contentOrder.Rows[i]["META_CONTENT_ID"], i + 1);
                    }
                }
            }
        }

        public static void moveDown_META_CONTENT(int iMetaContentID)
        {
            int iCategoryId = get_META_CONTENT_CATEGORY_ID(iMetaContentID);
            DataTable contentOrder = get_META_CONTENT_BY_CATEGORY_ID(iCategoryId).Tables[0];

            for (int i = 0; i < contentOrder.Rows.Count; i++)
            {
                if (((Int32)contentOrder.Rows[i]["META_CONTENT_ID"]) == iMetaContentID)
                {
                    if (i + 1 < contentOrder.Rows.Count)
                    {
                        update_META_CONTENT_ORDER((Int32)contentOrder.Rows[i]["META_CONTENT_ID"], i + 2);
                        update_META_CONTENT_ORDER((Int32)contentOrder.Rows[i + 1]["META_CONTENT_ID"], i + 1);
                        i++;
                    }
                    else
                    {
                        update_META_CONTENT_ORDER((Int32)contentOrder.Rows[i]["META_CONTENT_ID"], i + 1);
                    }
                }
                else
                {
                    LegoWebAdmin.BusLogic.MetaContents.update_META_CONTENT_ORDER((Int32)contentOrder.Rows[i]["META_CONTENT_ID"], i + 1);
                }
            }
        }

        public static void publish_META_CONTENTS(int iMETA_CONTENT_ID, bool bIsPublic)
        {
            string connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            SqlConnection connection = new SqlConnection(connStr);
            try
            {
                string strCommandName;
                SqlCommand objCommand;
                SqlParameter objParam;

                strCommandName = "UPDATE LEGOWEB_META_CONTENTS SET RECORD_STATUS=@_RECORD_STATUS WHERE META_CONTENT_ID=@_META_CONTENT_ID";
                objCommand = new SqlCommand(strCommandName, connection);
                objCommand.CommandType = CommandType.Text;
                //Set the Parameters

                objParam = objCommand.Parameters.Add(new SqlParameter("@_META_CONTENT_ID", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iMETA_CONTENT_ID;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_RECORD_STATUS", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = bIsPublic?1:0;

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
        /// <summary>
        /// Get and Set allow access User Roles
        /// </summary>
        /// <param name="iMETA_CONTENT_ID"></param>
        /// <returns></returns>
        public static string[] get_ACCESS_ROLES(int iMETA_CONTENT_ID)
        {
            string[] allowRoles;
            String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    SqlCommand cmdReader = new SqlCommand("SELECT ACCESS_ROLES FROM LEGOWEB_META_CONTENTS WHERE META_CONTENT_ID=" + iMETA_CONTENT_ID.ToString(), conn);
                    conn.Open();
                    SqlDataReader reader = cmdReader.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        string sRoles = reader["ACCESS_ROLES"].ToString();
                        conn.Close();
                        char[] splitter  = {';',','};
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

        public static void set_ACCESS_ROLES(int iMETA_CONTENT_ID, string sACCESS_ROLES)
        {
            String connString = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(connString))
            {
                try
                {
                    String strCommandText;
                    SqlCommand objCommand;
                    SqlParameter objParam;

                    strCommandText = "UPDATE LEGOWEB_META_CONTENTS SET ACCESS_ROLES=@_ACCESS_ROLES WHERE META_CONTENT_ID=@_META_CONTENT_ID";
                    objCommand = new SqlCommand(strCommandText, Conn);
                    objCommand.CommandType = CommandType.Text;

                    objParam = objCommand.Parameters.Add(new SqlParameter("@_ACCESS_ROLES", SqlDbType.NVarChar,100));
                    objParam.Direction = ParameterDirection.Input;
                    objParam.Value = sACCESS_ROLES;

                    objParam = objCommand.Parameters.Add(new SqlParameter("@_META_CONTENT_ID", SqlDbType.Int));
                    objParam.Direction = ParameterDirection.Input;
                    objParam.Value = iMETA_CONTENT_ID;


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

        public static DataSet get_META_CONTENT_BY_ID(int iFromID, int iToID)
        {
         
            DataSet myPageData = new DataSet();
            String connString = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(connString))
            {
                try
                {
                    string strCommandName;
                    SqlCommand objCommand;
                    SqlParameter objParam;

                    strCommandName = @"	SELECT DISTINCT LEGOWEB_META_CONTENTS.*,LEGOWEB_CATEGORIES.CATEGORY_VI_TITLE 
	                                    FROM LEGOWEB_META_CONTENTS INNER JOIN LEGOWEB_CATEGORIES ON LEGOWEB_META_CONTENTS.CATEGORY_ID=LEGOWEB_CATEGORIES.CATEGORY_ID 
	                                    WHERE LEGOWEB_META_CONTENTS.META_CONTENT_ID BETWEEN @_FROM_ID AND @_TO_ID
	                                    ORDER BY META_CONTENT_ID ASC";
                    objCommand = new SqlCommand(strCommandName, Conn);
                    objCommand.CommandType = CommandType.Text;
                    //Set the Parameters
                    objParam = objCommand.Parameters.Add(new SqlParameter("@_FROM_ID", SqlDbType.Int));
                    objParam.Direction = ParameterDirection.Input;
                    objParam.Value = iFromID;

                    objParam = objCommand.Parameters.Add(new SqlParameter("@_TO_ID", SqlDbType.Int));
                    objParam.Direction = ParameterDirection.Input;
                    objParam.Value = iToID;


                    SqlDataAdapter adap = new SqlDataAdapter(objCommand);
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
            }
            return myPageData;
        }

        public static DataSet get_META_CONTENT_BY_CATEGORY_ID(int iCATEGORY_ID, int iSECTION_ID)
        {

            DataSet myPageData = new DataSet();
            String connString = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(connString))
            {
                try
                {
                    string strCommandName;
                    SqlCommand objCommand;
                    SqlParameter objParam;

                    strCommandName = @"		WITH hierarchy
	                                        AS
	                                        (
		                                        SELECT CATEGORY_ID,CATEGORY_VI_TITLE
		                                        FROM LEGOWEB_CATEGORIES
		                                        WHERE (((@_ROOT_CATEGORY_ID=0) AND ((@_SECTION_ID =0 ) OR (SECTION_ID=@_SECTION_ID AND PARENT_CATEGORY_ID=0))) OR (CATEGORY_ID=@_ROOT_CATEGORY_ID))
		                                        UNION ALL
		                                        SELECT c.CATEGORY_ID,c.CATEGORY_VI_TITLE
		                                        FROM LEGOWEB_CATEGORIES AS c INNER JOIN hierarchy AS p ON c.PARENT_CATEGORY_ID = p.CATEGORY_ID
	                                        )
	                                        SELECT DISTINCT LEGOWEB_META_CONTENTS.*,hierarchy.CATEGORY_VI_TITLE 
	                                        FROM LEGOWEB_META_CONTENTS INNER JOIN hierarchy ON LEGOWEB_META_CONTENTS.CATEGORY_ID=hierarchy.CATEGORY_ID 
	                                        ORDER BY LEGOWEB_META_CONTENTS.META_CONTENT_ID ASC	
                                            ";
                    objCommand = new SqlCommand(strCommandName, Conn);
                    objCommand.CommandType = CommandType.Text;
                    //Set the Parameters
                    objParam = objCommand.Parameters.Add(new SqlParameter("@_ROOT_CATEGORY_ID", SqlDbType.Int));
                    objParam.Direction = ParameterDirection.Input;
                    objParam.Value = iCATEGORY_ID;

                    objParam = objCommand.Parameters.Add(new SqlParameter("@_SECTION_ID", SqlDbType.Int));
                    objParam.Direction = ParameterDirection.Input;
                    objParam.Value = iSECTION_ID;


                    SqlDataAdapter adap = new SqlDataAdapter(objCommand);
                    Conn.Open();
                    adap.Fill(myPageData, "Table");
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
