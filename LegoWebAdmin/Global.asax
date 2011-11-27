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
            Membership.CreateUser("admin", "admin" + DateTime.Now.Year.ToString(),"contact@legoweb.org");                  
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
