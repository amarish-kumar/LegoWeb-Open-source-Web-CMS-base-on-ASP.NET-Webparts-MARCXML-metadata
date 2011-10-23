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
        public static string asign_WEBPART_PERSONALIZE_PARAMETER(string inputValue)
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

        public static void update_LEGOWEB_COMMON_PARAMETER(string sPARAMETER_NAME, string sPARAMETER_VALUE)
        {
            String connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmdupdateCSParameters = new SqlCommand("UPDATE LEGOWEB_COMMON_PARAMETERS SET PARAMETER_VI_VALUE = @_PARAMETER_VALUE , PARAMETER_EN_VALUE = @_PARAMETER_VALUE WHERE PARAMETER_NAME=@_PARAMETER_NAME", conn);
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
    }
}
