﻿<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        FmriCommon.setLogPath(Server.MapPath("App_Data\\fmri.log"));
        FmriCommon.LogToFile("Application starts...");
        FmriCommon.getMatlabRunner(Application, Server);
        FmriCommon.LogToFile("MatlabRunner created.");
        
        Application["ID"] = new Random().Next(10000);
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown
        FmriCommon.LogToFile("Application ends.");
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
