<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {        
        if (Roles.RoleExists("ADMINISTRATORS") == false)
        {
            Roles.CreateRole("ADMINISTRATORS");
        }        
        if (Roles.RoleExists("WEBEDITORS") == false)
        {
            Roles.CreateRole("WEBEDITORS");
        }
        if (Roles.RoleExists("WEBMASTERS") == false)
        {
            Roles.CreateRole("WEBMASTERS");
        }                
        if (Membership.GetUser("admin") == null)
        {
            Membership.CreateUser("admin", "admin" + DateTime.Now.Year.ToString(),"contact@hiendai.com.vn");                  
        }        
        if (!Roles.IsUserInRole("admin", "ADMINISTRATORS"))
        {
            Roles.AddUserToRole("admin", "ADMINISTRATORS");
        }
        if (!Roles.IsUserInRole("admin", "WEBEDITORS"))
        {
            Roles.AddUserToRole("admin", "WEBEDITORS");
        }
        if (!Roles.IsUserInRole("admin", "WEBMASTERS"))
        {
            Roles.AddUserToRole("admin", "WEBMASTERS");
        }
        
        //if (Membership.GetUser("administrator") == null)
        //{
        //    Membership.CreateUser("administrator", "administrator" + DateTime.Now.Year.ToString(), "hiendaisoftware@gmail.com");
        //}
        //if (!Roles.IsUserInRole("administrator", "ADMINISTRATORS"))
        //{
        //    Roles.AddUserToRole("administrator", "ADMINISTRATORS");
        //}
        //if (!Roles.IsUserInRole("administrator", "WEBEDITORS"))
        //{
        //    Roles.AddUserToRole("administrator", "WEBEDITORS");
        //}
        //if (!Roles.IsUserInRole("administrator", "WEBMASTERS"))
        //{
        //    Roles.AddUserToRole("administrator", "WEBMASTERS");
        //}                
        //FCKEditor
        Application["FCKeditor:UserFilesPhysicalPath"] = System.Configuration.ConfigurationManager.AppSettings["LegoWebFilesPhysicalPath"].ToString();
        Application["FCKeditor:UserFilesVirtuaPath"] = System.Configuration.ConfigurationManager.AppSettings["LegoWebFilesVirtuaPath"].ToString();

        Application["_locales"] = System.Configuration.ConfigurationManager.GetSection("locales");
        HttpContext.Current.Cache.Insert("__InvalidateAllPages", DateTime.Now, null,
                                          System.DateTime.MaxValue, System.TimeSpan.Zero,
                                          System.Web.Caching.CacheItemPriority.NotRemovable,
                                          null);
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started
    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
