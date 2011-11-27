using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using MarcXmlParserEx;
using System.Text.RegularExpressions;

namespace LegoWebSite.Buslgic
{
    /// <summary>
    /// Summary description for CommonParameters
    /// </summary>
    public static class CommonParameters
    {
        /// <summary>
        /// Dùng hàm này kiểm tra và tự động lấy common parameter với PARAMETER_NAME nằm trong {} 
        /// </summary>
        /// <param name="inputValue">"Nếu bạn muốn {xxx} hãy gặp {HSA:3269} và có thể là {PATH:hsa04080(3269)}"</param>
        /// <returns></returns>
        public static string asign_COMMON_PARAMETER(string inputValue)
        {
            string outputString = inputValue;
            string pattern = @"\{(.*?)\}";
            MatchCollection matches = Regex.Matches(inputValue, pattern);
            foreach (Match m in matches)
            {
                string sParamName = m.Groups[1].Value;
                string sParamValue = get_COMMON_PARAMETER_VALUE(sParamName);
                outputString = outputString.Replace("{" + sParamName + "}",sParamValue);
            }
            return outputString;
        }

        public static void addunknow_LEGOWEB_COMMON_PARAMETER(string sPARAMETER_NAME)
        {
            String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmdupdateCSParameters = new SqlCommand("INSERT INTO LEGOWEB_COMMON_PARAMETERS(PARAMETER_NAME,PARAMETER_DESCRIPTION) VALUES(@_PARAMETER_NAME, @_PARAMETER_DESCRIPTION)", conn);
                    SqlParameter sqlPara;
                    cmdupdateCSParameters.CommandType = CommandType.Text;

                    sqlPara = cmdupdateCSParameters.Parameters.Add(new SqlParameter("@_PARAMETER_NAME", SqlDbType.NVarChar, 50));
                    sqlPara.Direction = ParameterDirection.Input;
                    sqlPara.Value = sPARAMETER_NAME;

                    sqlPara = cmdupdateCSParameters.Parameters.Add(new SqlParameter("@_PARAMETER_DESCRIPTION", SqlDbType.NVarChar, 255));
                    sqlPara.Direction = ParameterDirection.Input;
                    sqlPara.Value = "Parameter is not set";

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

        public static void update_LEGOWEB_COMMON_PARAMETER(string sPARAMETER_NAME, string sPARAMETER_VALUE)
        {
            String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmdupdateCSParameters = new SqlCommand("UPDATE LEGOWEB_COMMON_PARAMETERS SET PARAMETER_" + System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpper() + "_VALUE = @_PARAMETER_VALUE WHERE PARAMETER_NAME=@_PARAMETER_NAME", conn);
                    SqlParameter sqlPara;
                    cmdupdateCSParameters.CommandType = CommandType.Text;

                    sqlPara = cmdupdateCSParameters.Parameters.Add(new SqlParameter("@_PARAMETER_NAME", SqlDbType.NVarChar, 50));
                    sqlPara.Direction = ParameterDirection.Input;
                    sqlPara.Value = sPARAMETER_NAME;

                    sqlPara = cmdupdateCSParameters.Parameters.Add(new SqlParameter("@_PARAMETER_VALUE", SqlDbType.NVarChar, 255));
                    sqlPara.Direction = ParameterDirection.Input;
                    sqlPara.Value = sPARAMETER_VALUE;

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

        public static String get_COMMON_PARAMETER_VALUE(string sPARAMETER_NAME)
        {
            string outValue = null;
            String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                String strSQL = "SELECT TOP 1 PARAMETER_" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToUpper() + "_VALUE AS PARAM_VALUE FROM LEGOWEB_COMMON_PARAMETERS WHERE PARAMETER_NAME='" + sPARAMETER_NAME + "'";
                try
                {
                    conn.Open();
                    SqlCommand cmdcheckCSParameters = new SqlCommand(strSQL, conn);
                    outValue = Convert.ToString(cmdcheckCSParameters.ExecuteScalar());
                    conn.Close();
                    if (String.IsNullOrEmpty(outValue) && !isExist_PARAMETER_NAME(sPARAMETER_NAME))//not set yet
                    {
                        addunknow_LEGOWEB_COMMON_PARAMETER(sPARAMETER_NAME);
                    }
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


        public static bool isExist_PARAMETER_NAME(string sPARAMETER_NAME)
        {
            String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    SqlCommand cmdReader = new SqlCommand("SELECT PARAMETER_NAME FROM LEGOWEB_COMMON_PARAMETERS WHERE PARAMETER_NAME=N'" + sPARAMETER_NAME + "'", conn);
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
