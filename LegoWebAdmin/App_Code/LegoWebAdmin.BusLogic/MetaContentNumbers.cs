// ----------------------------------------------------------------------
// <copyright file="MetaContentNumbers.cs" package="LEGOWEB">
//     Copyright (C) 2011 LEGOWEB.ORG. All rights reserved.
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
    /// Summary description for MetaContentNumbers
    /// </summary>
    public static class MetaContentNumbers
    {

        public static Int32 insert_META_CONTENT_NUMBERS(int iMETA_CONTENT_ID, int iTAG, int iTAG_INDEX, string sSUBFIELD_CODE, double dSUBFIELD_VALUE, bool bIS_PUBLIC, int iACCESS_LEVEL, string sCREATED_USER)
        {
            string connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            SqlConnection connection = new SqlConnection(connStr);
            try
            {
                string strCommandName;
                SqlCommand objCommand;
                SqlParameter objParam;

                strCommandName = "sp_LEGOWEB_META_CONTENT_NUMBERS_INSERT";
                objCommand = new SqlCommand(strCommandName, connection);
                objCommand.CommandType = CommandType.StoredProcedure;
                //Set the Parameters

                Int32 iMETA_CONTENT_NUMBER_ID = 0;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_META_CONTENT_ID", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iMETA_CONTENT_ID;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_TAG", SqlDbType.SmallInt));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iTAG;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_TAG_INDEX", SqlDbType.SmallInt));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iTAG_INDEX;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_SUBFIELD_CODE", SqlDbType.NVarChar, 1));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sSUBFIELD_CODE;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_SUBFIELD_VALUE", SqlDbType.Real));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = dSUBFIELD_VALUE;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_IS_PUBLIC", SqlDbType.Bit));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = bIS_PUBLIC;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_ACCESS_LEVEL", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iACCESS_LEVEL;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_CREATED_USER", SqlDbType.NVarChar, 30));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sCREATED_USER;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_META_CONTENT_NUMBER_ID", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Output;

                connection.Open();
                objCommand.ExecuteNonQuery();
                //'Get the Out parameter from the Stored Procedure
                iMETA_CONTENT_NUMBER_ID = Convert.ToInt32((objCommand.Parameters["@_META_CONTENT_NUMBER_ID"].Value.ToString()));

                connection.Close();
                return iMETA_CONTENT_NUMBER_ID;
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

        public static void update_META_CONTENT_NUMBERS(int iMETA_CONTENT_NUMBER_ID, int iTAG, int iTAG_INDEX, string sSUBFIELD_CODE,decimal dSUBFIELD_VALUE, bool bIS_PUBLIC, int iACCESS_LEVEL, string sMODIFIED_USER)
        {
            string connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            SqlConnection connection = new SqlConnection(connStr);
            try
            {
                string strCommandName;
                SqlCommand objCommand;
                SqlParameter objParam;

                strCommandName = "sp_LEGOWEB_META_CONTENT_NUMBERS_UPDATE";
                objCommand = new SqlCommand(strCommandName, connection);
                objCommand.CommandType = CommandType.StoredProcedure;
                //Set the Parameters

                objParam = objCommand.Parameters.Add(new SqlParameter("@_META_CONTENT_NUMBER_ID", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iMETA_CONTENT_NUMBER_ID;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_TAG", SqlDbType.SmallInt));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iTAG;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_TAG_INDEX", SqlDbType.SmallInt));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iTAG_INDEX;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_SUBFIELD_CODE", SqlDbType.NVarChar, 1));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sSUBFIELD_CODE;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_SUBFIELD_VALUE", SqlDbType.Real));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = dSUBFIELD_VALUE;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_IS_PUBLIC", SqlDbType.Bit));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = bIS_PUBLIC;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_ACCESS_LEVEL", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iACCESS_LEVEL;

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

        public static bool is_META_CONTENT_NUMBER_EXIST(Int32 iID)
        {
            String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    SqlCommand cmdReader = new SqlCommand("SELECT META_CONTENT_NUMBER_ID FROM LEGOWEB_META_CONTENT_NUMBERS WHERE META_CONTENT_NUMBER_ID=" + iID.ToString(), conn);
                    conn.Open();
                    SqlDataReader reader = cmdReader.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        Int32 retID = (Int32)reader["META_CONTENT_NUMBER_ID"];
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
