<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

        //FCKEditor
        Application["FCKeditor:UserFilesPhysicalPath"] = System.Configuration.ConfigurationSettings.AppSettings["LegoWebFilesPhysicalPath"].ToString();
        Application["FCKeditor:UserFilesVirtuaPath"] = System.Configuration.ConfigurationSettings.AppSettings["LegoWebFilesVirtuaPath"].ToString();
        
        Application["_locales"] = System.Configuration.ConfigurationManager.GetSection("locales");
        HttpContext.Current.Cache.Insert("__InvalidateAllPages", DateTime.Now, null,
                                          System.DateTime.MaxValue, System.TimeSpan.Zero,
                                          System.Web.Caching.CacheItemPriority.NotRemovable,
                                          null);
        //FCKEditor
		Application["OnlineUsers"] = 0;
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
        Session["lang"] = "en";
        // Code that runs when a new session is started
        Application.Lock();
        Application["OnlineUsers"] = (int)Application["OnlineUsers"] + 1;
        Application.UnLock();

        int Visited_Count = 0;
        string sCount = LegoWebSite.Buslgic.CommonParameters.get_COMMON_PARAMETER_VALUE("NUMBER_OF_VISITED");
        if (string.IsNullOrEmpty(sCount))
        {
            Visited_Count = 1;
        }
        else
        {
            if (int.TryParse(sCount, out Visited_Count))
            {
                Visited_Count++;
            }
            else
            {
                Visited_Count = 1;
            }
        }
        LegoWebSite.Buslgic.CommonParameters.update_LEGOWEB_COMMON_PARAMETER("NUMBER_OF_VISITED", Visited_Count.ToString());
        Application["Visited_Count"] = Visited_Count;


           
    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
        Application.Lock();
        Application["OnlineUsers"] = (int)Application["OnlineUsers"] - 1;
        Application.UnLock();
    }
       
</script>
