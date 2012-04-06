using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace LegoWebSite.Buslgic
{
    /// <summary>
    /// Summary description for Sections
    /// </summary>
    public static class Sections
    {
        public static DataSet get_SECTION_BY_ID(int iSECTION_ID)
        {
            DataSet retData = new DataSet();
            String connString = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(connString))
            {
                try
                {
                    String strCommandText;
                    SqlCommand objCommand;

                    strCommandText = "SELECT * FROM LEGOWEB_SECTIONS WHERE SECTION_ID=" + iSECTION_ID.ToString();

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
    }
}