using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace LegoWebSiteForum.Buslogic
{
    /// <summary>
    /// Summary description for ForumUsers
    /// </summary>
    public class User
    {
        private int _postCount;
        private int _userID;
        private int _webID;
        private string _alias;
        private string _avatar;
        private string _email;
        private string _roles;

        public User()
        {
        }

        public int PostCount
        {
            get
            {
                return _postCount;
            }
            set
            {
                _postCount = value;
            }
        }

        public int UserID
        {
            get
            {
                return _userID;
            }
            set
            {
                _userID = value;
            }
        }


        public string Alias
        {
            get
            {
                return _alias;
            }
            set
            {
                _alias = value;
            }
        }

        public string Avatar
        {
            get
            {
                return _avatar;
            }
            set
            {
                _avatar = value;
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }

        public string Roles
        {
            get
            {
                return _roles;
            }
            set
            {
                _roles = value;
            }
        }
    }

    public static class ForumUsers
    {
        public static User GetUser(int userID)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString);
            SqlCommand cmd = new SqlCommand("LWF_GetUser", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID", SqlDbType.Int, 4);
            cmd.Parameters[0].Value = userID;

            conn.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            // Should throw an exception here if dr.Read returns false
            dr.Read();
            User user = PopulateUser(dr);
            user.UserID = userID;

            dr.Close();
            conn.Close();

            return user;
        }

        private static User PopulateUser(SqlDataReader dr)
        {
            User user = new User();

            user.Alias = Convert.ToString(dr["Alias"]);
            user.Email = Convert.ToString(dr["Email"]);
            user.PostCount = Convert.ToInt32(dr["PostCount"]);
            user.Roles = Convert.IsDBNull(dr["Roles"]) ? string.Empty : Convert.ToString(dr["Roles"]);
            user.Avatar = Convert.IsDBNull(dr["Avatar"]) ? string.Empty : Convert.ToString(dr["Avatar"]);

            return user;
        }

        public static int GetUserIDFromEmail(string email)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString);
            SqlCommand cmd = new SqlCommand("LWF_GetUserIDFromEmail", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@UserID", SqlDbType.Int, 4);
            cmd.Parameters[0].Value = email;
            cmd.Parameters[1].Direction = ParameterDirection.Output;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            if (Convert.IsDBNull(cmd.Parameters[1].Value))
                return 0;

            return (int)cmd.Parameters[1].Value;
        }

        public static void UpdateUser(User user)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString);
            SqlCommand cmd = new SqlCommand("LWF_UpdateUser", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID", SqlDbType.Int, 4);
            cmd.Parameters.Add("@Alias", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Avatar", SqlDbType.NVarChar, 250);
            cmd.Parameters[0].Value = user.UserID;
            cmd.Parameters[1].Value = user.Alias;
            cmd.Parameters[2].Value = user.Email;
            if (user.Avatar == string.Empty)
                cmd.Parameters[3].Value = System.DBNull.Value;
            else
                cmd.Parameters[3].Value = user.Avatar;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static int AddUser(User user)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString);
            SqlCommand cmd = new SqlCommand("LWF_AddUser", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID", SqlDbType.Int, 4);
            cmd.Parameters.Add("@Alias", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters[0].Direction = ParameterDirection.Output;
            cmd.Parameters[1].Value = user.Alias;
            cmd.Parameters[2].Value = user.Email;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            user.UserID = (int)cmd.Parameters[0].Value;

            return user.UserID;
        }

        public static int GetLoggedOnUser(string identityName)
        {
            // The user variable corresponds to a string which is the number of
            // the currently logged on user.  Generally, this function should be
            // called with user set to "Page.User.Identity.Name".
            int userID = 0;

            try
            {
                userID = Convert.ToInt32(identityName);
                if (userID > 0)
                {
                    User user = GetUser(userID);
                }
            }
            catch
            {
                userID = 0;
            }

            return userID;
        }

        public static bool AliasExists(string alias, int webID)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString);
            SqlCommand cmd = new SqlCommand("LWF_AliasExists", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Alias", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Exists", SqlDbType.Bit, 1);
            cmd.Parameters[0].Value = alias;
            cmd.Parameters[1].Value = webID;
            cmd.Parameters[2].Direction = ParameterDirection.Output;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            return (bool)cmd.Parameters[2].Value;
        }

        public static bool EmailExists(string email, int webID)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LEGOWEBDB"].ConnectionString);
            SqlCommand cmd = new SqlCommand("LWF_EmailExists", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Exists", SqlDbType.Bit, 1);
            cmd.Parameters[0].Value = email;
            cmd.Parameters[1].Value = webID;
            cmd.Parameters[2].Direction = ParameterDirection.Output;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            return (bool)cmd.Parameters[2].Value;
        }

    }
}