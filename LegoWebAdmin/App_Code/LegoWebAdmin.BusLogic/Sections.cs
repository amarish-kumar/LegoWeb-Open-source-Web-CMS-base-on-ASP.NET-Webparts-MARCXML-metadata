// ----------------------------------------------------------------------
// <copyright file="Sections.cs" package="LEGOWEB">
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


namespace LegoWebAdmin.BusLogic
{
    /// <summary>
    /// Summary description for Section
    /// </summary>
    public static class Sections
    {
        
        public static void add_Update(int iSECTION_ID, string sSECTION_VI_TITLE, string sSECTION_EN_TITLE)
        {
            string connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            SqlConnection connection = new SqlConnection(connStr);
            try
            {
                string strCommandName;
                SqlCommand objCommand;
                SqlParameter objParam;

                strCommandName = "sp_LEGOWEB_SECTIONS_ADDUPDATE";
                objCommand = new SqlCommand(strCommandName, connection);
                objCommand.CommandType = CommandType.StoredProcedure;
                //Set the Parameters

                objParam = objCommand.Parameters.Add(new SqlParameter("@_SECTION_ID", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iSECTION_ID;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_SECTION_VI_TITLE", SqlDbType.NVarChar, 250));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sSECTION_VI_TITLE;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_SECTION_EN_TITLE", SqlDbType.NVarChar, 250));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sSECTION_EN_TITLE;

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

        public static void remove_Section(int iSECTION_ID)
        {
            string connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            SqlConnection connection = new SqlConnection(connStr);
            try
            {
                string strCommandName;
                SqlCommand objCommand;
                SqlParameter objParam;

                strCommandName = "sp_LEGOWEB_SECTIONS_REMOVE";
                objCommand = new SqlCommand(strCommandName, connection);
                objCommand.CommandType = CommandType.StoredProcedure;
                //Set the Parameters

                objParam = objCommand.Parameters.Add(new SqlParameter("@_SECTION_ID", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iSECTION_ID;

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

        public static DataSet get_Section_By_ID(int iSectionID)
        {
            DataSet retData = new DataSet();
            String connString = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(connString))
            {
                try
                {
                    String strCommandText;
                    SqlCommand objCommand;

                    strCommandText = "SELECT * FROM LEGOWEB_SECTIONS WHERE SECTION_ID=" + iSectionID.ToString();

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

        public static DataSet get_LEGOWEB_SECTIONS()
        {
            DataSet myPageData = new DataSet();
            String connString = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;

            using (SqlConnection Conn = new SqlConnection(connString))
            {
                try
                {
                    String strCommandText = "";
                    SqlCommand objCommand;

                    strCommandText += "SELECT LEGOWEB_SECTIONS.* FROM LEGOWEB_SECTIONS ORDER BY SECTION_ID ASC";

                    objCommand = new SqlCommand(strCommandText, Conn);
                    objCommand.CommandType = CommandType.Text;

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

        public static Int32 get_Search_Count()
        {
            String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(connStr))
            {
                try
                {
                    String strCommandText;
                    SqlCommand objCommand;

                    strCommandText = "SELECT COUNT(SECTION_ID) FROM LEGOWEB_SECTIONS ";
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
                    String strCommandText="";
                    SqlCommand objCommand;

                    strCommandText += "SELECT TOP(" + iSelectRow + ") LEGOWEB_SECTIONS.* FROM LEGOWEB_SECTIONS ORDER BY SECTION_ID ASC";

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
