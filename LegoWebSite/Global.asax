<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
        //FCKEditor
        Application["FCKeditor:UserFilesPhysicalPath"] = System.Configuration.ConfigurationSettings.AppSettings["LegoWebFilesPhysicalPath"].ToString();
        Application["FCKeditor:UserFilesVirtuaPath"] = System.Configuration.ConfigurationSettings.AppSettings["LegoWebFilesVirtuaPath"].ToString();
        //FCKEditor
        
        Application["_locales"] = System.Configuration.ConfigurationManager.GetSection("locales");
        HttpContext.Current.Cache.Insert("__InvalidateAllPages", DateTime.Now, null,
                                          System.DateTime.MaxValue, System.TimeSpan.Zero,
                                          System.Web.Caching.CacheItemPriority.NotRemovable,
                                          null);        
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
        // Code that runs when a new session is started
        Application.Lock();
        Application["OnlineUsers"] = (int)Application["OnlineUsers"] + 1;
        Application.UnLock();

        int Total_Visited_Count = 0;
        int Month_Visited_Count = 0;        
        string sTotalVisitedCount = LegoWebSite.Buslgic.CommonParameters.get_COMMON_PARAMETER_VALUE("TOTAL_VISITED_COUNT");
        if (string.IsNullOrEmpty(sTotalVisitedCount))
        {
            Total_Visited_Count = 1;
        }
        else
        {
            if (int.TryParse(sTotalVisitedCount, out Total_Visited_Count))
            {
                Total_Visited_Count++;
            }
            else
            {
                Total_Visited_Count = 1;
            }
        }
        LegoWebSite.Buslgic.CommonParameters.update_LEGOWEB_COMMON_PARAMETER("TOTAL_VISITED_COUNT", Total_Visited_Count.ToString());
        Application["TOTAL_VISITED_COUNT"] = Total_Visited_Count.ToString();

                
        string sMonthVisitedParam = String.Format("VISITED_IN_{0}", DateTime.Now.Year.ToString("00") + DateTime.Now.Month.ToString("00"));
        string sMonthVisitedCount = LegoWebSite.Buslgic.CommonParameters.get_COMMON_PARAMETER_VALUE(sMonthVisitedParam);
        if (string.IsNullOrEmpty(sMonthVisitedCount))
        {
            Month_Visited_Count = 1;
        }
        else
        {
            if (int.TryParse(sMonthVisitedCount, out Month_Visited_Count))
            {
                Month_Visited_Count++;
            }
            else
            {
                Month_Visited_Count = 1;
            }
        }
        LegoWebSite.Buslgic.CommonParameters.update_LEGOWEB_COMMON_PARAMETER(sMonthVisitedParam, Month_Visited_Count.ToString());
        Application[sMonthVisitedParam] = Month_Visited_Count;           
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
