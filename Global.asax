<%@ Application Language="C#" %>


<script RunAt="server">

    void Application_BeginRequest(object sender, EventArgs e)
    {
        //HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "http://122.170.117.118");
        //HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
        //HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");

        //if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
        //{
        //    HttpContext.Current.Response.StatusCode = 200;
        //    HttpContext.Current.Response.End();
        //}
        //HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "http://122.170.117.118");
        //HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
        //HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Authorization");
        //HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");

    }

    void Application_Start(object sender, EventArgs e)
    {

        // Code that runs on application startup

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
