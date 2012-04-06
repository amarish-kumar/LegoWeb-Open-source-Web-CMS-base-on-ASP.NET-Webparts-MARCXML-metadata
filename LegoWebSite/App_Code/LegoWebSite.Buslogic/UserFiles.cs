using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace LegoWebSite.Buslgic
{
    /// <summary>
    /// Summary description for UserFiles
    /// </summary>
    public static class UserFiles
    {
        public static Int32 insert_LEGOWEB_USER_FILES(string sUSER_FILE_NAME, string sPHYSICAL_PATH, int iFORUM_POST_ID, string sUPLOAD_USER)
        {
            Int32 iFILE_ID = 0;

            string connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            SqlConnection connection = new SqlConnection(connStr);
            try
            {
                string strCommandName;
                SqlCommand objCommand;
                SqlParameter objParam;

                strCommandName = "sp_LEGOWEB_USER_FILES_INSERT";
                objCommand = new SqlCommand(strCommandName, connection);
                objCommand.CommandType = CommandType.StoredProcedure;
                //Set the Parameters

                objParam = objCommand.Parameters.Add(new SqlParameter("@_USER_FILE_NAME", SqlDbType.NVarChar, 50));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sUSER_FILE_NAME;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_PHYSICAL_PATH", SqlDbType.NVarChar, 250));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sPHYSICAL_PATH;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_FORUM_POST_ID", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iFORUM_POST_ID;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_UPLOAD_USER", SqlDbType.NVarChar, 30));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sUPLOAD_USER;

                objParam = objCommand.Parameters.Add(new SqlParameter("@_FILE_ID", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Output;

                connection.Open();
                objCommand.ExecuteNonQuery();
                //'Get the Out parameter from the Stored Procedure
                iFILE_ID = Convert.ToInt32((objCommand.Parameters["@_FILE_ID"].Value.ToString()));

                connection.Close();
                return iFILE_ID;
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

        public static DataTable get_LEGOWEB_USER_FILES(int iFileId, int iPostId)
        {
            DataSet retData = new DataSet();
            String ConnString = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(ConnString))
            {
                try
                {
                    String strCommandText;
                    SqlCommand objCommand;
                    bool addWhere=true;
                    strCommandText = "SELECT * FROM LEGOWEB_USER_FILES ";                    
                    if(iFileId>0)
                    {
                          strCommandText +=" WHERE FILE_ID=" + iFileId.ToString();
                    }else
                    {
                        if(iPostId>0)
                        {
                            if(addWhere)
                            {
                                strCommandText += " WHERE FORUM_POST_ID=" + iPostId.ToString();
                                addWhere=false;
                            }else
                            {
                                strCommandText += " AND FORUM_POST_ID=" + iPostId.ToString();
                            }
                        }
                    }
                    strCommandText += " ORDER BY USER_FILE_NAME ASC";                    
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
            return retData.Tables[0];
        }

        public static void delete_LEGOWEB_USER_FILES( int iFILE_ID)
        {
            string connStr = ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString;
            SqlConnection connection = new SqlConnection(connStr);
            try
            {
                string strCommandName;
                SqlCommand objCommand;
                SqlParameter objParam;

                strCommandName = "DELETE FROM LEGOWEB_USER_FILES WHERE FILE_ID=@_FILE_ID";
                objCommand = new SqlCommand(strCommandName, connection);
                objCommand.CommandType = CommandType.Text;
                //Set the Parameters

                objParam = objCommand.Parameters.Add(new SqlParameter("@_FILE_ID", SqlDbType.Int));
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = iFILE_ID;

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
    }
}
