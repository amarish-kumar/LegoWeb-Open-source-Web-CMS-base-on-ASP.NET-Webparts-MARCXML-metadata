using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using MarcXmlParserEx;

/// <summary>
/// Summary description for MetaContents
/// </summary>
namespace LegoWebSite.Buslgic
{
       public static class MetaContents
       {                      
           public static DataTable get_ALL_META_CONTENT_ID_BY_CATEGORY(int iCATEGORY_ID)
           {
               DataSet retData = new DataSet();

               String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
               using (SqlConnection conn = new SqlConnection(connStr))
               {
                   try
                   {
                       string strCommandName;
                       SqlCommand objCommand;
                       SqlParameter objParam;
                       strCommandName = @"
                                WITH hierarchy
                                AS
                                (
                                        SELECT CATEGORY_ID
                                        FROM LEGOWEB_CATEGORIES
                                        WHERE (CATEGORY_ID=@_CATEGORY_ID)
                                        UNION ALL
                                        SELECT c.CATEGORY_ID
                                        FROM LEGOWEB_CATEGORIES AS c INNER JOIN hierarchy AS p ON c.PARENT_CATEGORY_ID = p.CATEGORY_ID
                                 )

	                             SELECT LEGOWEB_META_CONTENTS.META_CONTENT_ID 
		                         FROM LEGOWEB_META_CONTENTS INNER JOIN hierarchy ON LEGOWEB_META_CONTENTS.CATEGORY_ID=hierarchy.CATEGORY_ID
                                 WHERE RECORD_STATUS>0 
                                 ORDER BY MODIFIED_DATE DESC   
                                ";


                       objCommand = new SqlCommand(strCommandName, conn);
                       objCommand.CommandType = CommandType.Text;


                       objParam = objCommand.Parameters.Add(new SqlParameter("@_CATEGORY_ID", SqlDbType.Int));
                       objParam.Direction = ParameterDirection.Input;
                       objParam.Value = iCATEGORY_ID;


                       SqlDataAdapter adap = new SqlDataAdapter(objCommand);
                       conn.Open();
                       adap.Fill(retData, "Table");
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
               return retData.Tables[0];
           }

           public static string get_META_CONTENT_MARCXML_BY_CATEGORY(int iCATEGORY_ID)
           {
               CRecords retRecords = new CRecords();
               CRecord myRec = new CRecord();
               DataTable myTable = get_ALL_META_CONTENT_ID_BY_CATEGORY(iCATEGORY_ID);
               for (int i = 0; i < myTable.Rows.Count; i++)
               {
                   Int32 iID =Int32.Parse( myTable.Rows[i]["META_CONTENT_ID"].ToString());
                   String sXML = get_META_CONTENT_MARCXML(iID, 0);                   
                   myRec.load_Xml(sXML);
                   retRecords.Add(myRec);
               }               
               return retRecords.OuterXml;           
           }

           public static DataTable get_TOP_RELATED_CONTENTS(int iCATEGORY_ID, int iNUMBER_OF_RECORD, string sLANG_CODE, int[] excepted_meta_content_ids)
           {
               DataSet retData = new DataSet();

               String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
               using (SqlConnection conn = new SqlConnection(connStr))
               {
                   try
                   {
                       string strCommandName;
                       SqlCommand objCommand;
                       SqlParameter objParam;
                       strCommandName = @"
                                WITH hierarchy
                                AS
                                (
                                        SELECT CATEGORY_ID
                                        FROM LEGOWEB_CATEGORIES
                                        WHERE CATEGORY_ID=@_CATEGORY_ID
                                        UNION ALL
                                        SELECT c.CATEGORY_ID
                                        FROM LEGOWEB_CATEGORIES AS c INNER JOIN hierarchy AS p ON c.PARENT_CATEGORY_ID = p.CATEGORY_ID
                                 )

	                             SELECT DISTINCT TOP(@_NUMBER_OF_RECORD) LEGOWEB_META_CONTENTS.META_CONTENT_ID,LEGOWEB_META_CONTENTS.MODIFIED_DATE
		                         FROM LEGOWEB_META_CONTENTS INNER JOIN hierarchy ON LEGOWEB_META_CONTENTS.CATEGORY_ID=hierarchy.CATEGORY_ID
                                 WHERE LEGOWEB_META_CONTENTS.LANG_CODE=@_LANG_CODE AND RECORD_STATUS>0 ";
                        
                       if(excepted_meta_content_ids.Length>0)
                       {                       
                        strCommandName +=" AND LEGOWEB_META_CONTENTS.META_CONTENT_ID NOT IN(" + CommonUtility.convert_ArrayToString(excepted_meta_content_ids,",") + ")";
                       }
                       strCommandName +=" ORDER BY MODIFIED_DATE DESC ";   
                       objCommand = new SqlCommand(strCommandName, conn);
                       objCommand.CommandType = CommandType.Text;


                       objParam = objCommand.Parameters.Add(new SqlParameter("@_CATEGORY_ID", SqlDbType.Int));
                       objParam.Direction = ParameterDirection.Input;
                       objParam.Value = iCATEGORY_ID;

                       objParam = objCommand.Parameters.Add(new SqlParameter("@_NUMBER_OF_RECORD", SqlDbType.Int));
                       objParam.Direction = ParameterDirection.Input;
                       objParam.Value = iNUMBER_OF_RECORD;

                       objParam = objCommand.Parameters.Add(new SqlParameter("@_LANG_CODE", SqlDbType.NVarChar, 3));
                       objParam.Direction = ParameterDirection.Input;
                       objParam.Value = sLANG_CODE;
                       
                       SqlDataAdapter adap = new SqlDataAdapter(objCommand);
                       conn.Open();
                       adap.Fill(retData, "Table");
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
               return retData.Tables[0];
           }

           public static DataTable get_TOP_CONTENTS_OF_CATEGORY(int iCATEGORY_ID,int iNUMBER_OF_RECORD, string sLANG_CODE, string sOrderClause)
           {
               DataSet retData = new DataSet();

               String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
               using (SqlConnection conn = new SqlConnection(connStr))
               {
                   try
                   {
                       string strCommandName;
                       SqlCommand objCommand;
                       SqlParameter objParam;
                       strCommandName = @"
                                WITH hierarchy
                                AS
                                (
                                        SELECT CATEGORY_ID
                                        FROM LEGOWEB_CATEGORIES
                                        WHERE CATEGORY_ID=@_CATEGORY_ID
                                        UNION ALL
                                        SELECT c.CATEGORY_ID
                                        FROM LEGOWEB_CATEGORIES AS c INNER JOIN hierarchy AS p ON c.PARENT_CATEGORY_ID = p.CATEGORY_ID
                                 )

	                             SELECT DISTINCT TOP(@_NUMBER_OF_RECORD) LEGOWEB_META_CONTENTS.META_CONTENT_ID,LEGOWEB_META_CONTENTS.MODIFIED_DATE
		                         FROM LEGOWEB_META_CONTENTS INNER JOIN hierarchy ON LEGOWEB_META_CONTENTS.CATEGORY_ID=hierarchy.CATEGORY_ID
                                 WHERE RECORD_STATUS>0                                     
                                ";
                       if (!String.IsNullOrEmpty(sLANG_CODE))
                       {
                           strCommandName += " AND LANG_CODE=N'" + sLANG_CODE + "'" ;
                       }
                       if (String.IsNullOrEmpty(sOrderClause))
                       {
                           strCommandName += " ORDER BY MODIFIED_DATE DESC";
                       }
                       else
                       {
                           strCommandName += " " + sOrderClause;
                       }
                       
                       objCommand = new SqlCommand(strCommandName, conn);
                       objCommand.CommandType = CommandType.Text;

                       objParam = objCommand.Parameters.Add(new SqlParameter("@_CATEGORY_ID", SqlDbType.Int));
                       objParam.Direction = ParameterDirection.Input;
                       objParam.Value = iCATEGORY_ID;

                       objParam = objCommand.Parameters.Add(new SqlParameter("@_NUMBER_OF_RECORD", SqlDbType.Int));
                       objParam.Direction = ParameterDirection.Input;
                       objParam.Value = iNUMBER_OF_RECORD;

                       SqlDataAdapter adap = new SqlDataAdapter(objCommand);
                       conn.Open();
                       adap.Fill(retData, "Table");
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
               return retData.Tables[0];                     
           }

           public static DataTable get_MOST_READ_CONTENTS(int iCATEGORY_ID, int iNUMBER_OF_RECORD, string sLANG_CODE)
           {
               DataSet retData = new DataSet();

               String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
               using (SqlConnection conn = new SqlConnection(connStr))
               {
                   try
                   {
                       string strCommandName;
                       SqlCommand objCommand;
                       SqlParameter objParam;
                       strCommandName = @"
                                WITH hierarchy
                                AS
                                (
                                        SELECT CATEGORY_ID
                                        FROM LEGOWEB_CATEGORIES
                                        WHERE (@_CATEGORY_ID=0 AND PARENT_CATEGORY_ID=0) OR (CATEGORY_ID=@_CATEGORY_ID)
                                        UNION ALL
                                        SELECT c.CATEGORY_ID
                                        FROM LEGOWEB_CATEGORIES AS c INNER JOIN hierarchy AS p ON c.PARENT_CATEGORY_ID = p.CATEGORY_ID
                                 )

	                             SELECT DISTINCT TOP(@_NUMBER_OF_RECORD) LEGOWEB_META_CONTENTS.META_CONTENT_ID,LEGOWEB_META_CONTENTS.READ_COUNT
		                         FROM LEGOWEB_META_CONTENTS INNER JOIN hierarchy ON LEGOWEB_META_CONTENTS.CATEGORY_ID=hierarchy.CATEGORY_ID
                                 WHERE LEGOWEB_META_CONTENTS.LANG_CODE=@_LANG_CODE  AND RECORD_STATUS>0 
                                 ORDER BY READ_COUNT DESC   
                                ";


                       objCommand = new SqlCommand(strCommandName, conn);
                       objCommand.CommandType = CommandType.Text;


                       objParam = objCommand.Parameters.Add(new SqlParameter("@_CATEGORY_ID", SqlDbType.Int));
                       objParam.Direction = ParameterDirection.Input;
                       objParam.Value = iCATEGORY_ID;

                       objParam = objCommand.Parameters.Add(new SqlParameter("@_NUMBER_OF_RECORD", SqlDbType.Int));
                       objParam.Direction = ParameterDirection.Input;
                       objParam.Value = iNUMBER_OF_RECORD;

                       objParam = objCommand.Parameters.Add(new SqlParameter("@_LANG_CODE", SqlDbType.NVarChar, 3));
                       objParam.Direction = ParameterDirection.Input;
                       objParam.Value = sLANG_CODE;

                       SqlDataAdapter adap = new SqlDataAdapter(objCommand);
                       conn.Open();
                       adap.Fill(retData, "Table");
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
               return retData.Tables[0];
           }

           public static DataTable get_TOP_NEWS_BY_CATEGORY(int iCATEGORY_ID, int iNUMBER_OF_RECORD, string sLANG_CODE, int iIMPORTANT_LEVEL)
           {
               DataSet retData = new DataSet();

               String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
               using (SqlConnection conn = new SqlConnection(connStr))
               {
                   try
                   {
                       string strCommandName;
                       SqlCommand objCommand;
                       SqlParameter objParam;
                       strCommandName = @"
                                WITH hierarchy
                                AS
                                (
                                        SELECT CATEGORY_ID
                                        FROM LEGOWEB_CATEGORIES
                                        WHERE (@_CATEGORY_ID=0 AND PARENT_CATEGORY_ID=0) OR (CATEGORY_ID=@_CATEGORY_ID)
                                        UNION ALL
                                        SELECT c.CATEGORY_ID
                                        FROM LEGOWEB_CATEGORIES AS c INNER JOIN hierarchy AS p ON c.PARENT_CATEGORY_ID = p.CATEGORY_ID
                                 )

	                             SELECT DISTINCT TOP(@_NUMBER_OF_RECORD) LEGOWEB_META_CONTENTS.META_CONTENT_ID,LEGOWEB_META_CONTENTS.MODIFIED_DATE 
		                         FROM LEGOWEB_META_CONTENTS INNER JOIN hierarchy ON LEGOWEB_META_CONTENTS.CATEGORY_ID=hierarchy.CATEGORY_ID
                                 WHERE LEGOWEB_META_CONTENTS.LANG_CODE=@_LANG_CODE  AND RECORD_STATUS>0 AND IMPORTANT_LEVEL>=@_IMPORTANT_LEVEL
                                 ORDER BY MODIFIED_DATE DESC   
                                ";


                       objCommand = new SqlCommand(strCommandName, conn);
                       objCommand.CommandType = CommandType.Text;


                       objParam = objCommand.Parameters.Add(new SqlParameter("@_CATEGORY_ID", SqlDbType.Int));
                       objParam.Direction = ParameterDirection.Input;
                       objParam.Value = iCATEGORY_ID;

                       objParam = objCommand.Parameters.Add(new SqlParameter("@_NUMBER_OF_RECORD", SqlDbType.Int));
                       objParam.Direction = ParameterDirection.Input;
                       objParam.Value = iNUMBER_OF_RECORD;

                       objParam = objCommand.Parameters.Add(new SqlParameter("@_LANG_CODE", SqlDbType.NVarChar, 3));
                       objParam.Direction = ParameterDirection.Input;
                       objParam.Value = sLANG_CODE;

                       objParam = objCommand.Parameters.Add(new SqlParameter("@_IMPORTANT_LEVEL", SqlDbType.Int));
                       objParam.Direction = ParameterDirection.Input;
                       objParam.Value = iIMPORTANT_LEVEL;

                       SqlDataAdapter adap = new SqlDataAdapter(objCommand);
                       conn.Open();
                       adap.Fill(retData, "Table");
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
               return retData.Tables[0];
           }

           public static DataTable get_TOP_NEWS_BY_SECTION(int iSECTION_ID, int iNUMBER_OF_RECORD, string sLANG_CODE, int iIMPORTANT_LEVEL)
           {
               DataSet retData = new DataSet();

               String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
               using (SqlConnection conn = new SqlConnection(connStr))
               {
                   try
                   {
                       string strCommandName;
                       SqlCommand objCommand;
                       SqlParameter objParam;
                       strCommandName = @"
                                WITH hierarchy
                                AS
                                (
                                        SELECT CATEGORY_ID
                                        FROM LEGOWEB_CATEGORIES
                                        WHERE PARENT_CATEGORY_ID=0 AND SECTION_ID=@_SECTION_ID
                                        UNION ALL
                                        SELECT c.CATEGORY_ID
                                        FROM LEGOWEB_CATEGORIES AS c INNER JOIN hierarchy AS p ON c.PARENT_CATEGORY_ID = p.CATEGORY_ID
                                 )

	                             SELECT DISTINCT TOP(@_NUMBER_OF_RECORD) LEGOWEB_META_CONTENTS.META_CONTENT_ID,LEGOWEB_META_CONTENTS.MODIFIED_DATE 
		                         FROM LEGOWEB_META_CONTENTS INNER JOIN hierarchy ON LEGOWEB_META_CONTENTS.CATEGORY_ID=hierarchy.CATEGORY_ID
                                 WHERE LEGOWEB_META_CONTENTS.LANG_CODE=@_LANG_CODE  AND RECORD_STATUS>0 AND IMPORTANT_LEVEL>=@_IMPORTANT_LEVEL
                                 ORDER BY MODIFIED_DATE DESC   
                                ";


                       objCommand = new SqlCommand(strCommandName, conn);
                       objCommand.CommandType = CommandType.Text;


                       objParam = objCommand.Parameters.Add(new SqlParameter("@_SECTION_ID", SqlDbType.Int));
                       objParam.Direction = ParameterDirection.Input;
                       objParam.Value = iSECTION_ID;

                       objParam = objCommand.Parameters.Add(new SqlParameter("@_NUMBER_OF_RECORD", SqlDbType.Int));
                       objParam.Direction = ParameterDirection.Input;
                       objParam.Value = iNUMBER_OF_RECORD;

                       objParam = objCommand.Parameters.Add(new SqlParameter("@_LANG_CODE", SqlDbType.NVarChar, 3));
                       objParam.Direction = ParameterDirection.Input;
                       objParam.Value = sLANG_CODE;

                       objParam = objCommand.Parameters.Add(new SqlParameter("@_IMPORTANT_LEVEL", SqlDbType.Int));
                       objParam.Direction = ParameterDirection.Input;
                       objParam.Value = iIMPORTANT_LEVEL;                       

                       SqlDataAdapter adap = new SqlDataAdapter(objCommand);
                       conn.Open();
                       adap.Fill(retData, "Table");
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
               return retData.Tables[0];
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
                           Int32 retID = (Int32)reader["CATEGORY_ID"];
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

           public static String get_META_CONTENT_MARCXML(Int32 iMETA_CONTENT_ID, int  iSCOPE)
           {
               LegoWebSite.DataProvider.MetaContentObject outRec = new LegoWebSite.DataProvider.MetaContentObject();
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

                       objParam = objCommand.Parameters.Add(new SqlParameter("@_SCOPE", SqlDbType.SmallInt));
                       objParam.Direction = ParameterDirection.Input;
                       objParam.Value =iSCOPE;

                       conn.Open();
                       SqlDataReader reader = objCommand.ExecuteReader();
                       int iTag = 0;
                       int iCurrentTagIndex = -1;
                       string sSfValue = null;
                       System.Globalization.NumberFormatInfo info = new System.Globalization.NumberFormatInfo(); 
                       info.NumberDecimalSeparator = "."; 
                       info.NumberGroupSeparator = ","; 
                       while (reader.Read()) //start first row
                       {
                           iTag = int.Parse(reader["TAG"].ToString());
                           if (iTag < 10)//controlfield only one per row
                           {
                               iCurrentTagIndex = int.Parse(reader["TAG_INDEX"].ToString());
                               Cf.ReConstruct();
                               Cf.ID = reader["ID"].ToString();
                               Cf.Tag = iTag.ToString("000");
                               Cf.Type = reader["SUBFIELD_TYPE"].ToString();
                               sSfValue=reader["SUBFIELD_VALUE"].ToString();
                               if (Cf.Type == "NUMBER" && !String.IsNullOrEmpty(sSfValue))
                               {
                                   Cf.Value = String.Format("{0:0.##}", Convert.ToDouble(sSfValue,info));
                               }
                               else
                               {
                                   Cf.Value = sSfValue;
                               }
                               outRec.Controlfields.Add(Cf);
                           }
                           else  //
                           {
                               if (iCurrentTagIndex == int.Parse(reader["TAG_INDEX"].ToString()))
                               {
                                   Sf.ReConstruct();
                                   Sf.ID = reader["ID"].ToString();
                                   Sf.Code = reader["SUBFIELD_CODE"].ToString();
                                   Sf.Type = reader["SUBFIELD_TYPE"].ToString();
                                   sSfValue = reader["SUBFIELD_VALUE"].ToString();
                                   if (Sf.Type == "NUMBER" && !String.IsNullOrEmpty(sSfValue))
                                   {
                                       Sf.Value = String.Format("{0:0.##}", Convert.ToDouble(sSfValue,info));
                                   }
                                   else
                                   {
                                       Sf.Value = sSfValue;
                                   }
                                   Df.Subfields.Add(Sf);
                               }
                               else
                               {
                                   if (Df.Subfields.Count > 0) outRec.Datafields.Add(Df);
                                   Df.ReConstruct();
                                   Df.Tag = int.Parse(reader["TAG"].ToString()).ToString("000");
                                   iCurrentTagIndex = int.Parse(reader["TAG_INDEX"].ToString());
                                   Sf.ReConstruct();
                                   Sf.ID = reader["ID"].ToString();
                                   Sf.Code = reader["SUBFIELD_CODE"].ToString();
                                   Sf.Type = reader["SUBFIELD_TYPE"].ToString();
                                   sSfValue = reader["SUBFIELD_VALUE"].ToString();
                                   if (Sf.Type == "NUMBER" && !String.IsNullOrEmpty(sSfValue))
                                   {
                                       Sf.Value = String.Format("{0:0.##}", Convert.ToDouble(sSfValue,info));
                                   }
                                   else
                                   {
                                       Sf.Value = sSfValue;
                                   }
                                   Df.Subfields.Add(Sf);
                               }
                           }
                       }
                       if (Df.Subfields.Count > 0) outRec.Datafields.Add(Df);
                       Df.ReConstruct();
                       reader.Close();
                       //end read datafield


                       string strCommandName0;
                       SqlCommand objCommand0;
					   //SqlParameter objParam0;

                       strCommandName0 = "SELECT * FROM LEGOWEB_META_CONTENTS WHERE META_CONTENT_ID=" + iMETA_CONTENT_ID.ToString();
                       objCommand0 = new SqlCommand(strCommandName0, conn);
                       objCommand0.CommandType = CommandType.Text;
                       SqlDataReader reader0 = objCommand0.ExecuteReader();
                       reader0.Read();
                       outRec.MetaContentID = iMETA_CONTENT_ID;
                       outRec.Alias = reader0["META_CONTENT_ALIAS"].ToString();
                       outRec.CategoryID = Convert.ToInt16(reader0["CATEGORY_ID"]);
                       outRec.RecordStatus = int.Parse(reader0["RECORD_STATUS"].ToString());
                       outRec.AccessLevel = int.Parse(reader0["ACCESS_LEVEL"].ToString());
                       outRec.LangCode = reader0["LANG_CODE"].ToString();
                       outRec.EntryDate = Convert.ToDateTime(reader0["CREATED_DATE"]).ToString("yyyy-MMM-dd hh:mm:ss");
                       outRec.Creator = reader0["CREATED_USER"].ToString();
                       outRec.ModifyDate = Convert.ToDateTime(reader0["MODIFIED_DATE"]).ToString("dd/MM/yy hh:mm:ss");
                       outRec.Modifier = reader0["MODIFIED_USER"].ToString();
                       reader0.Close();
                       conn.Close();
                       outRec.Datafields.Clean();
                       outRec.Sort();                       
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
               return outRec.OuterXml;
           }

            public static int get_META_CONTENT_ID_BY_CATEGORY_ID(int iCatid, int iNumberOfRecord)
            {
                SqlCommand objCommand;
                SqlParameter objParam;
                String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    try
                    {
                        string strCommand = @"
                                            SELECT TOP(@_NUMBER_OF_RECORD) META_CONTENT_ID 
                                            FROM LEGOWEB_META_CONTENTS
                                            WHERE CATEGORY_ID=" + iCatid.ToString() + "AND LANG_CODE=N'" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + "'  AND RECORD_STATUS>0 ORDER BY MODIFIED_DATE desc";

                        objCommand = new SqlCommand(strCommand, conn);

                        objParam = objCommand.Parameters.Add(new SqlParameter("@_NUMBER_OF_RECORD", SqlDbType.Int));
                        objParam.Direction = ParameterDirection.Input;
                        objParam.Value = iNumberOfRecord;
                        conn.Open();
                        SqlDataReader reader = objCommand.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            int retID = (int)reader["META_CONTENT_ID"];
                            conn.Close();
                            return retID;
                        }
                        else
                        {
                            conn.Close();
                            return 0;
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
            public static int get_READ_COUNT_BY_META_CONTENT_ID(int iMetacontentId)
            {
                String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    String strSQL = "SELECT TOP 1 READ_COUNT AS PARAM_VALUE FROM LEGOWEB_META_CONTENTS WHERE META_CONTENT_ID='" + iMetacontentId + "'";
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(strSQL, conn);
                        int count = int.Parse(cmd.ExecuteScalar().ToString());
                        conn.Close();
                        return count;
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
            public static void increase_READ_COUNT(int iMetaconntentId)
            {
                string connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
                SqlConnection connection = new SqlConnection(connStr);
                try
                {
                    string strCommandName;
                    SqlCommand objCommand;
                    SqlParameter objParam;

                    strCommandName = "UPDATE LEGOWEB_META_CONTENTS SET READ_COUNT = READ_COUNT + 1 WHERE META_CONTENT_ID = " + iMetaconntentId.ToString();
                    objCommand = new SqlCommand(strCommandName, connection);
                    objCommand.CommandType = CommandType.Text;
                    //Set the Parameters

                    objParam = objCommand.Parameters.Add(new SqlParameter("@_MATA_CONTENT_ID", SqlDbType.Int));
                    objParam.Direction = ParameterDirection.Input;
                    objParam.Value = iMetaconntentId;
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
            public static DataSet get_METACONTENT_BY_CATEGORY_ID(int iCatID, int iNumberOfRecord)
            {
                DataSet retData = new DataSet();
                String ConnString = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
                using (SqlConnection Conn = new SqlConnection(ConnString))
                {
                    try
                    {
                        String strCommandText;
                        SqlCommand objCommand;
                        strCommandText = "select TOP(@_NUMBER_OF_RECORD)* from LEGOWEB_META_CONTENTS where CATEGORY_ID = @_CATEGORY_ID AND LANG_CODE=N'" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + "'  AND RECORD_STATUS>0  ORDER BY ORDER_NUMBER asc";

                        objCommand = new SqlCommand(strCommandText, Conn);
                        objCommand.CommandType = CommandType.Text;

                        objCommand.Parameters.Add(new SqlParameter("@_NUMBER_OF_RECORD", SqlDbType.Int));
                        objCommand.Parameters["@_NUMBER_OF_RECORD"].Value = iNumberOfRecord;
                        objCommand.Parameters.Add(new SqlParameter("@_CATEGORY_ID", SqlDbType.Int));
                        objCommand.Parameters["@_CATEGORY_ID"].Value = iCatID;

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
            public static Int32 get_User_Search_Count(int iSearchSectionId, string sSearchField, string sSearchValue)
            {
                String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
                using (SqlConnection Conn = new SqlConnection(connStr))
                {
                    try
                    {
                        string strCommandName;
                        SqlCommand objCommand;
                        SqlParameter objParam;
                        bool bAddWhere = true;
                        strCommandName = @"	
                                       WITH hierarchy
                                        AS
                                        (
                                        SELECT CATEGORY_ID,CATEGORY_VI_TITLE
                                        FROM LEGOWEB_CATEGORIES
                                        WHERE   ((SECTION_ID=@_SECTION_ID AND PARENT_CATEGORY_ID=0))		   	
                                        UNION ALL

                                        SELECT c.CATEGORY_ID,c.CATEGORY_VI_TITLE
                                        FROM LEGOWEB_CATEGORIES AS c INNER JOIN hierarchy AS p ON c.PARENT_CATEGORY_ID = p.CATEGORY_ID
                                        )
                                    SELECT COUNT(DISTINCT LEGOWEB_META_CONTENTS.META_CONTENT_ID) FROM LEGOWEB_META_CONTENTS INNER JOIN hierarchy ON LEGOWEB_META_CONTENTS.CATEGORY_ID=hierarchy.CATEGORY_ID 
                                    ";

                        if (String.IsNullOrEmpty(sSearchField))
                        {
                            sSearchField = "any";
                        }
                        switch (sSearchField.ToLower())
                        {
                            case "any":
                                if (bAddWhere)
                                {
                                    strCommandName += " WHERE LEGOWEB_META_CONTENTS.META_CONTENT_ID IN (SELECT META_CONTENT_ID FROM LEGOWEB_META_CONTENT_TEXTS WHERE SUBFIELD_VALUE LIKE N'%" + sSearchValue + "%')";
                                    bAddWhere = false;
                                }
                                else
                                {
                                    strCommandName += " AND LEGOWEB_META_CONTENTS.META_CONTENT_ID IN (SELECT META_CONTENT_ID FROM LEGOWEB_META_CONTENT_TEXTS WHERE SUBFIELD_VALUE LIKE N'%" + sSearchValue + "%')";
                                }
                                break;
                            case "title":
                                if (bAddWhere)
                                {
                                    strCommandName += " WHERE LEGOWEB_META_CONTENTS.META_CONTENT_ID IN (SELECT META_CONTENT_ID FROM LEGOWEB_META_CONTENT_TEXTS WHERE SUBFIELD_VALUE LIKE N'%" + sSearchValue + "%' AND TAG=245)";
                                    bAddWhere = false;
                                }
                                else
                                {
                                    strCommandName += " AND LEGOWEB_META_CONTENTS.META_CONTENT_ID IN (SELECT META_CONTENT_ID FROM LEGOWEB_META_CONTENT_TEXTS WHERE SUBFIELD_VALUE LIKE N'%" + sSearchValue + "%' TAG=245)";
                                }

                                break;
                            case "ntext":
                                if (bAddWhere)
                                {
                                    strCommandName += " WHERE LEGOWEB_META_CONTENTS.META_CONTENT_ID IN (SELECT META_CONTENT_ID FROM LEGOWEB_META_CONTENT_NTEXTS WHERE SUBFIELD_VALUE LIKE N'%" + sSearchValue + "%')";
                                    bAddWhere = false;
                                }
                                else
                                {
                                    strCommandName += " AND LEGOWEB_META_CONTENTS.META_CONTENT_ID IN (SELECT META_CONTENT_ID FROM LEGOWEB_META_CONTENT_NTEXTS WHERE SUBFIELD_VALUE LIKE N'%" + sSearchValue + "%')";
                                }
                                break;
                        }
                        if (bAddWhere)
                        {
                            strCommandName += " WHERE LEGOWEB_META_CONTENTS.RECORD_STATUS>0 ";
                            bAddWhere = false;
                        }
                        else
                        {
                            strCommandName += " AND LEGOWEB_META_CONTENTS.RECORD_STATUS>0 ";                        
                        }


                        objCommand = new SqlCommand(strCommandName, Conn);
                        objCommand.CommandType = CommandType.Text;
                        //Set the Parameters
                        
                        objParam = objCommand.Parameters.Add(new SqlParameter("@_SECTION_ID", SqlDbType.Int));
                        objParam.Direction = ParameterDirection.Input;
                        objParam.Value = iSearchSectionId > 0 ? iSearchSectionId : System.Data.SqlTypes.SqlInt32.Null;

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
            public static DataSet get_User_Search_Page(int iSearchSectionId, string sSearchField, string sSearchValue, int iPage, int iPageSize)
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
                        bool bAddWhere = true;
                        strCommandName = @"	

                                        WITH hierarchy
	                                    AS
	                                    (
	                                        SELECT CATEGORY_ID,CATEGORY_VI_TITLE
	                                        FROM LEGOWEB_CATEGORIES
	                                        WHERE   ((SECTION_ID=1000 AND PARENT_CATEGORY_ID=0))		   	
	                                        UNION ALL
	                                        SELECT c.CATEGORY_ID,c.CATEGORY_VI_TITLE
	                                        FROM LEGOWEB_CATEGORIES AS c INNER JOIN hierarchy AS p ON c.PARENT_CATEGORY_ID = p.CATEGORY_ID
	                                    )
                                        SELECT DISTINCT TOP(@_SELECT_ROW_NUMBER) LEGOWEB_META_CONTENTS.META_CONTENT_ID
                                        FROM LEGOWEB_META_CONTENTS INNER JOIN hierarchy ON LEGOWEB_META_CONTENTS.CATEGORY_ID=hierarchy.CATEGORY_ID 
                                    ";
                        if (String.IsNullOrEmpty(sSearchField))
                        {
                            sSearchField = "any";
                        }
                        switch (sSearchField.ToLower())
                        {
                            case "any":
                                if (bAddWhere)
                                {
                                    strCommandName += " WHERE LEGOWEB_META_CONTENTS.META_CONTENT_ID IN (SELECT META_CONTENT_ID FROM LEGOWEB_META_CONTENT_TEXTS WHERE SUBFIELD_VALUE LIKE N'%" + sSearchValue + "%')";
                                    bAddWhere = false;
                                }
                                else
                                {
                                    strCommandName += " AND LEGOWEB_META_CONTENTS.META_CONTENT_ID IN (SELECT META_CONTENT_ID FROM LEGOWEB_META_CONTENT_TEXTS WHERE SUBFIELD_VALUE LIKE N'%" + sSearchValue + "%')";
                                }
                                break;
                            case "title":
                                if (bAddWhere)
                                {
                                    strCommandName += " WHERE LEGOWEB_META_CONTENTS.META_CONTENT_ID IN (SELECT META_CONTENT_ID FROM LEGOWEB_META_CONTENT_TEXTS WHERE SUBFIELD_VALUE LIKE N'%" + sSearchValue + "%' AND TAG=245)";
                                    bAddWhere = false;
                                }
                                else
                                {
                                    strCommandName += " AND LEGOWEB_META_CONTENTS.META_CONTENT_ID IN (SELECT META_CONTENT_ID FROM LEGOWEB_META_CONTENT_TEXTS WHERE SUBFIELD_VALUE LIKE N'%" + sSearchValue + "%' TAG=245)";
                                }

                                break;
                            case "ntext":
                                if (bAddWhere)
                                {
                                    strCommandName += " WHERE LEGOWEB_META_CONTENTS.META_CONTENT_ID IN (SELECT META_CONTENT_ID FROM LEGOWEB_META_CONTENT_NTEXTS WHERE SUBFIELD_VALUE LIKE N'%" + sSearchValue + "%')";
                                    bAddWhere = false;
                                }
                                else
                                {
                                    strCommandName += " AND LEGOWEB_META_CONTENTS.META_CONTENT_ID IN (SELECT META_CONTENT_ID FROM LEGOWEB_META_CONTENT_NTEXTS WHERE SUBFIELD_VALUE LIKE N'%" + sSearchValue + "%')";
                                }
                                break;
                        }
                        if (bAddWhere)
                        {
                            strCommandName += " WHERE LEGOWEB_META_CONTENTS.RECORD_STATUS>0 ";
                            bAddWhere = false;
                        }
                        else
                        {
                            strCommandName += " AND LEGOWEB_META_CONTENTS.RECORD_STATUS>0 ";
                        }

                        strCommandName += "ORDER BY META_CONTENT_ID DESC ";
                        objCommand = new SqlCommand(strCommandName, Conn);
                        objCommand.CommandType = CommandType.Text;
                        //Set the Parameters
                        objParam = objCommand.Parameters.Add(new SqlParameter("@_SECTION_ID", SqlDbType.Int));
                        objParam.Direction = ParameterDirection.Input;
                        objParam.Value = iSearchSectionId > 0 ? iSearchSectionId : System.Data.SqlTypes.SqlInt32.Null;

                        objParam = objCommand.Parameters.Add(new SqlParameter("@_SELECT_ROW_NUMBER", SqlDbType.Int));
                        objParam.Direction = ParameterDirection.Input;
                        objParam.Value = iSelectRow;

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

            public static Int32 get_Content_Navigator_Count(int iCATEGORY_ID, string sLANG_CODE)
            {
                String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
                using (SqlConnection Scon = new SqlConnection(connStr))
                {
                    try
                    {
                        string strCommandName;
                        SqlCommand objCommand;
                        SqlParameter objParam;

                        strCommandName = @"	
                                            WITH hierarchy
                                            AS
                                            (
                                                    SELECT CATEGORY_ID
                                                    FROM LEGOWEB_CATEGORIES
                                                    WHERE CATEGORY_ID=@_CATEGORY_ID
                                                    UNION ALL
                                                    SELECT c.CATEGORY_ID
                                                    FROM LEGOWEB_CATEGORIES AS c INNER JOIN hierarchy AS p ON c.PARENT_CATEGORY_ID = p.CATEGORY_ID
                                             )

	                                         SELECT COUNT(DISTINCT LEGOWEB_META_CONTENTS.META_CONTENT_ID)
		                                     FROM LEGOWEB_META_CONTENTS INNER JOIN hierarchy ON LEGOWEB_META_CONTENTS.CATEGORY_ID=hierarchy.CATEGORY_ID
                                             WHERE LEGOWEB_META_CONTENTS.LANG_CODE=@_LANG_CODE AND RECORD_STATUS>0
                                             ";

                        objCommand = new SqlCommand(strCommandName, Scon);
                        objCommand.CommandType = CommandType.Text;


                        objParam = objCommand.Parameters.Add(new SqlParameter("@_CATEGORY_ID", SqlDbType.Int));
                        objParam.Direction = ParameterDirection.Input;
                        objParam.Value = iCATEGORY_ID;

                        objParam = objCommand.Parameters.Add(new SqlParameter("@_LANG_CODE", SqlDbType.NVarChar, 3));
                        objParam.Direction = ParameterDirection.Input;
                        objParam.Value = sLANG_CODE;
                        Scon.Open();
                        Int32 retInt = Convert.ToInt32(objCommand.ExecuteScalar());
                        Scon.Close();
                        return retInt;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (Scon.State == ConnectionState.Open)
                            Scon.Close();
                    }
                }
            }
            public static DataSet get_Content_Navigator_Page(int iCATEGORY_ID, string sLANG_CODE, int iPage, int iPageSize)
            {
                int startPos = (iPage - 1) * iPageSize;
                int iSelectRow = iPage * iPageSize;
                DataSet myPageData = new DataSet();
                String connString = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;

                using (SqlConnection SConn = new SqlConnection(connString))
                {
                    try
                    {
                        string strCommandName;
                        SqlCommand objCommand;
                        SqlParameter objParam;
                        bool bAddWhere = true;
                        strCommandName = @"	
                                            WITH hierarchy
                                            AS
                                            (   
                                                    SELECT CATEGORY_ID
                                                    FROM LEGOWEB_CATEGORIES
                                                    WHERE CATEGORY_ID=@_CATEGORY_ID
                                                    UNION ALL
                                                    SELECT c.CATEGORY_ID
                                                    FROM LEGOWEB_CATEGORIES AS c INNER JOIN hierarchy AS p ON c.PARENT_CATEGORY_ID = p.CATEGORY_ID
                    	                     )
                                         SELECT DISTINCT TOP(@_SELECT_ROW_NUMBER) LEGOWEB_META_CONTENTS.META_CONTENT_ID,LEGOWEB_META_CONTENTS.MODIFIED_DATE
	                                     FROM LEGOWEB_META_CONTENTS INNER JOIN hierarchy ON LEGOWEB_META_CONTENTS.CATEGORY_ID=hierarchy.CATEGORY_ID
                                         WHERE LEGOWEB_META_CONTENTS.LANG_CODE=@_LANG_CODE AND RECORD_STATUS>0
                                         ORDER BY MODIFIED_DATE DESC ";

                        objCommand = new SqlCommand(strCommandName, SConn);
                        objCommand.CommandType = CommandType.Text;

                        objParam = objCommand.Parameters.Add(new SqlParameter("@_CATEGORY_ID", SqlDbType.Int));
                        objParam.Direction = ParameterDirection.Input;
                        objParam.Value = iCATEGORY_ID;

                        objParam = objCommand.Parameters.Add(new SqlParameter("@_LANG_CODE", SqlDbType.NVarChar, 3));
                        objParam.Direction = ParameterDirection.Input;
                        objParam.Value = sLANG_CODE;

                        objParam = objCommand.Parameters.Add(new SqlParameter("@_SELECT_ROW_NUMBER", SqlDbType.Int));
                        objParam.Direction = ParameterDirection.Input;
                        objParam.Value = iSelectRow;

                        SqlDataAdapter adap = new SqlDataAdapter(objCommand);
                        SConn.Open();
                        adap.Fill(myPageData, startPos, iPageSize, "Table");
                        SConn.Close();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (SConn.State == ConnectionState.Open)
                            SConn.Close();
                    }
                }
                return myPageData;
            }

            public static int get_ACCESS_LEVEL(int iMETA_CONTENT_ID)
            {
                String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    try
                    {
                        SqlCommand cmdReader = new SqlCommand("SELECT ACCESS_LEVEL FROM LEGOWEB_META_CONTENTS WHERE META_CONTENT_ID=" + iMETA_CONTENT_ID.ToString(), conn);
                        conn.Open();
                        SqlDataReader reader = cmdReader.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            int iLevel = int.Parse(reader["ACCESS_LEVEL"].ToString());
                            conn.Close();
                            return iLevel;
                        }
                        else
                        {
                            conn.Close();
                            return 0;
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

       }
}