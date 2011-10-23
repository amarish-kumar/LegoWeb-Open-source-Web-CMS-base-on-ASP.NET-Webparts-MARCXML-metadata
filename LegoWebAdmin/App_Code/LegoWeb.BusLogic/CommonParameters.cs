using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace LegoWeb.BusLogic
{
    /// <summary>
    /// Summary description for CommonParameters
    /// </summary>
    public static class CommonParameters
    {
        public static void addudp_LEGOWEB_COMMON_PARAMETER(string sPARAMETER_NAME,int iPARAMETER_TYPE, string sPARAMETER_VI_VALUE, string sPARAMETER_EN_VALUE, string sPARAMETER_DESCRIPTION)
        {
            String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmdupdateCSParameters = new SqlCommand("sp_LEGOWEB_COMMON_PARAMETERS_ADDUDP", conn);
                    SqlParameter sqlPara;
                    cmdupdateCSParameters.CommandType = CommandType.StoredProcedure;
                    
                    sqlPara = cmdupdateCSParameters.Parameters.Add(new SqlParameter("@_PARAMETER_NAME", SqlDbType.NVarChar, 50));
                    sqlPara.Direction = ParameterDirection.Input;
                    sqlPara.Value = sPARAMETER_NAME;

                    sqlPara = cmdupdateCSParameters.Parameters.Add(new SqlParameter("@_PARAMETER_TYPE", SqlDbType.Int));
                    sqlPara.Direction = ParameterDirection.Input;
                    sqlPara.Value = iPARAMETER_TYPE;

                    sqlPara = cmdupdateCSParameters.Parameters.Add(new SqlParameter("@_PARAMETER_VI_VALUE", SqlDbType.NVarChar, 255));
                    sqlPara.Direction = ParameterDirection.Input;
                    sqlPara.Value = sPARAMETER_VI_VALUE;

                    sqlPara = cmdupdateCSParameters.Parameters.Add(new SqlParameter("@_PARAMETER_EN_VALUE", SqlDbType.NVarChar, 255));
                    sqlPara.Direction = ParameterDirection.Input;
                    sqlPara.Value = sPARAMETER_EN_VALUE;

                    sqlPara = cmdupdateCSParameters.Parameters.Add(new SqlParameter("@_PARAMETER_DESCRIPTION", SqlDbType.NVarChar, 255));
                    sqlPara.Direction = ParameterDirection.Input;
                    sqlPara.Value = sPARAMETER_DESCRIPTION;

                    cmdupdateCSParameters.ExecuteNonQuery();
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
        }

        public static DataSet get_LEGOWEB_COMMON_PARAMETER(String sParameterName)
        {
            DataSet retData = new DataSet();
            String connString = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(connString))
            {
                try
                {
                    String strCommandText;
                    SqlCommand objCommand;

                    strCommandText = "SELECT * FROM LEGOWEB_COMMON_PARAMETERS WHERE PARAMETER_NAME='" + sParameterName + "'";

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

        public static String get_COMMON_PARAMETER_VALUE(string sPARAMETER_NAME,string s2ISOLangCode)
        {
            string outValue = null;
            String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                String strSQL = "SELECT TOP 1 PARAMETER_" + s2ISOLangCode + "_VALUE AS PARAM_VALUE FROM LEGOWEB_COMMON_PARAMETERS WHERE PARAMETER_NAME='" + sPARAMETER_NAME + "'";
                try
                {
                    conn.Open();
                    SqlCommand cmdcheckCSParameters = new SqlCommand(strSQL, conn);
                    outValue = Convert.ToString(cmdcheckCSParameters.ExecuteScalar());
                    conn.Close();
                    return outValue;
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

        public static void remove_PARAMETER(String sParaName)
        {
            string connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            SqlConnection connection = new SqlConnection(connStr);
            try
            {
                string strCommandName;
                SqlCommand objCommand;
                SqlParameter objParam;

                strCommandName = "DELETE FROM LEGOWEB_COMMON_PARAMETERS WHERE PARAMETER_NAME=@_PARAMETER_NAME";
                objCommand = new SqlCommand(strCommandName, connection);
                objCommand.CommandType = CommandType.Text;
                //Set the Parameters

                objParam = objCommand.Parameters.Add(new SqlParameter("@_PARAMETER_NAME", SqlDbType.NVarChar,50));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sParaName;

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


        public static Int32 get_Search_Count(int iParamType)
        {
            String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(connStr))
            {
                try
                {
                    string strCommandName;
                    SqlCommand objCommand;
                    strCommandName = "SELECT COUNT(*) FROM LEGOWEB_COMMON_PARAMETERS ";
                    if (iParamType >= 0)
                    {
                        strCommandName += " WHERE PARAMETER_TYPE=" + iParamType.ToString();
                    }                    
                    objCommand = new SqlCommand(strCommandName, Conn);
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

        public static DataSet get_Search_Page(int iParamType, int iPage, int iPageSize)
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

                    strCommandName = "SELECT TOP(" + iSelectRow.ToString() + ") PARAMETER_NAME,PARAMETER_VI_VALUE,PARAMETER_EN_VALUE,PARAMETER_DESCRIPTION FROM LEGOWEB_COMMON_PARAMETERS ";
                    if (iParamType >= 0)
                    {
                        strCommandName += " WHERE PARAMETER_TYPE=" + iParamType.ToString();
                    }    
                    strCommandName += " ORDER BY PARAMETER_NAME ASC";
                    objCommand = new SqlCommand(strCommandName, Conn);
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