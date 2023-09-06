<%@ Application Language="C#" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="Microsoft.AspNet.SignalR" %>
<%@ Import Namespace="DynamicAL_v2" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="IITMS.UAIMS" %>
<script runat="server">
    string conStr = ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString; //added on 06-04-2020 by Vaishali for Notification

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
        //Application["TextBackColor"] = "#FF99FF";
        //ASPNETChatControl.ChatControl.DataProvider = new ASPNETChatControl.DAL.SqlServer.SqlDataProvider();     //commented on 06-04-2020 by Vaishali for Notification
        //ASPNETChatControl.ChatControl.ContactListProvider = new SampleCSharpApp.SampleContactListProvider.SampleContactListProvider();
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        //added on 06-04-2020 by Vaishali for Notification
        RouteTable.Routes.MapHubs();
        System.Data.SqlClient.SqlDependency.Start(conStr);

        Application["userno"] = 1;
        Application["usertype"] = 1;
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //ASPNETChatControl.ChatControl.StopSession();
        //  Code that runs on application shutdown

        System.Data.SqlClient.SqlDependency.Stop(conStr); //added on 06-04-2020 by Vaishali for Notification
    }
    protected void Application_PreSendRequestHeaders()
    {
        if (HttpContext.Current != null)
        {
            //HttpContext.Current.Response.Headers.Remove("X-Powered-By");
            HttpContext.Current.Response.Headers.Remove("X-AspNet-Version");
            HttpContext.Current.Response.Headers.Remove("X-AspNetMvc-Version");
            HttpContext.Current.Response.Headers.Remove("Server");
        }
    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs
        // Code that runs when an unhandled error occurs
        //Added by Deepali on 14/08/2020 (to check internet connectivity)
        //System.Net.WebClient client = new System.Net.WebClient();
        //byte[] datasize = null;
        //try
        //{
        //    datasize = client.DownloadData("http://www.google.com");
        //}
        //catch (Exception ex)
        //{
        //}
        //if (datasize == null)
        //{
        //    Session.Abandon();
        //    //Response.Flush();
        //    //Response.Clear();
        //    //this.Response.Redirect("https://sbutest.mastersofterp.in/");
        //    this.Response.Redirect("~/default.aspx");
        //}
        string MachineType = "", BrowserName = "", BrowserVersion = "", Page_Name = "", Message = "", ErrNo = "0", Stack = "", Err_Level = "", LineNo = "0", MethodName = "", PC_Name = "", WinVersion = "", Page_Path = "", pageName = "", UserNo = "", UserType = "", PageUrl = "";

        UserNo = Application["userno"].ToString();
        UserType = Application["usertype"].ToString();

        DynamicAL_v2.DynamicControllerAL AL = new DynamicAL_v2.DynamicControllerAL();
        string SP_Name = "PKG_CRUD_RUNTIME_ERR_LOG";

        try
        {
            Exception exception = Server.GetLastError().GetBaseException();

            Page_Name = HttpContext.Current.Request.Url.Segments[HttpContext.Current.Request.Url.Segments.Length - 1];
            pageName = Page_Name.Split('.')[0];
            PageUrl = HttpContext.Current.Request.Url.ToString();


            if (Context.Request.Url.ToString().Contains(".aspx"))
            {
                if (Context.Request.Url.ToString().Contains(pageName))
                {
                    if (System.Web.HttpContext.Current.Session != null)
                    {
                        Session["BackToPageUrl"] = Context.Request.Url.ToString();
                    }
                }

                var mn = new StackTrace(exception);
                for (int i = mn.FrameCount - 1; i >= 0; i--)
                {
                    if (((System.Reflection.MemberInfo)(mn.GetFrame(i).GetMethod())).DeclaringType.ToString().Contains(pageName))
                    {
                        MethodName += mn.GetFrame(i).GetMethod().Name + "-";
                    }
                }

                if (MethodName.Length != 0)
                    MethodName = MethodName.Remove(MethodName.Length - 1, 1);
                else
                    MethodName = "NA";
            }


            PC_Name = Context.Request.ServerVariables["REMOTE_USER"];
            string strx = Context.Request.ServerVariables["HTTP_USER_AGENT"];
            string str = strx.Split('(')[1];
            WinVersion = str.Split(')')[0];

            if (str.Contains("Windows"))
            {
                MachineType = "Windows";
            }
            else if (str.Contains("Android"))
            {
                MachineType = "Android";
            }
            else if (str.Contains("iPhone "))
            {
                MachineType = "iPhone";
            }
            else
            {
                MachineType = "NA";
            }

            if (strx.Contains("Chrome"))
            {
                BrowserName = "Chrome";
                BrowserVersion = strx.Split(new string[] { "Chrome/" }, StringSplitOptions.None)[1];
            }
            else if (strx.Contains("Firefox"))
            {
                BrowserName = "Firefox";
                BrowserVersion = strx.Split(new string[] { "Firefox/" }, StringSplitOptions.None)[1];
            }
            else
            {
                BrowserName = "NA";
                BrowserVersion = strx;
            }

            Message = Regex.Replace(exception.Message, @"\t|\n|\r", "");
            if (Message.Contains("Error Number"))
            {
                ErrNo = Message.Split(new string[] { "Error Number:" }, StringSplitOptions.None)[1].Split(',')[0];
                Message = Message.Split(new string[] { "->" }, StringSplitOptions.None)[2];
                Message = Message.Split(new string[] { " at" }, StringSplitOptions.None)[0];
                Err_Level = "DB Level Error";
            }
            else
            {
                ErrNo = "0";
                Err_Level = "Code Level Error";
            }

            Stack = Regex.Replace(exception.StackTrace, @"\t|\n|\r", "");
            string[] StackTrace = Stack.Split(new string[] { " at" }, StringSplitOptions.None);

            for (int i = 0; i < StackTrace.Length; i++)
            {
                if (StackTrace[i].ToString().Contains("line"))
                {
                    if (StackTrace[i].ToString().Contains(pageName))
                    {
                        Stack = StackTrace[i].ToString();
                    }
                }
            }

            Page_Path = Stack.Replace(',', ' ');
            if (Page_Path.Contains(" in"))
            {
                Page_Path = Page_Path.Split(new string[] { " in" }, StringSplitOptions.None)[1];
                if (Page_Path.Contains(":line"))
                {
                    Page_Path = Page_Path.Split(new string[] { ":line" }, StringSplitOptions.None)[0];
                }
            }


            if (Stack.Contains("line "))
            {
                LineNo = Stack.Split(new string[] { "line " }, StringSplitOptions.None)[1].Split(' ')[0];
            }

            string SP_Parameters = " @P_UA_NO ,@P_USER_TYPE ,@P_PAGE_NAME ,@P_PAGE_PATH ,@P_PAGE_NO ,@P_METHOD_NAME ,@P_LINE_NO ,@P_ERR_MESSAGE ,@P_ERR_NO ,@P_IP_ADDRESS ,@P_PC_NAME ,@P_MACHINE_TYPE ,@P_OS_VERSION ,@P_BROWSER_NAME ,@P_BROWSER_VERSION,@P_ERR_LEVEL";
            string Call_Values = "" + UserNo + "," + UserType + "," + Page_Name + "," + Page_Path + ",0," + MethodName + "," + LineNo + "," + Message + "," + ErrNo + ",NA," + PC_Name + "," + MachineType + "," + WinVersion + "," + BrowserName + "," + BrowserVersion.Split('.')[0] + "," + Err_Level + "";

            string que_out = AL.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true, 2);
        }
        catch (Exception ex)
        {
            string SP_Parameters = " @P_UA_NO ,@P_USER_TYPE ,@P_PAGE_NAME ,@P_PAGE_PATH ,@P_PAGE_NO ,@P_METHOD_NAME ,@P_LINE_NO ,@P_ERR_MESSAGE ,@P_ERR_NO ,@P_IP_ADDRESS ,@P_PC_NAME ,@P_MACHINE_TYPE ,@P_OS_VERSION ,@P_BROWSER_NAME ,@P_BROWSER_VERSION,@P_ERR_LEVEL";
            //string Call_Values = "6600,99,Global.asax,NA,0,NA,0," + ex.Message + ",0,NA," + PC_Name + "," + MachineType + "," + WinVersion + "," + BrowserName + "," + BrowserVersion.Split('.')[0] + ",Global Level Error";
            string Call_Values = "" + UserNo + ",99,Global.asax," + pageName + ",0,NA,0,ERR,0,NA,NA,NA,NA,NA,NA,Global Level Error";

            string que_out = AL.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true, 2);
        }
    }

    void Session_Start(object sender, EventArgs e) 
    {
       // ASPNETChatControl.ChatControl.DataProvider = new ASPNETChatControl.DAL.SqlServer.SqlDataProvider();
       // ASPNETChatControl.ChatControl.ContactListProvider = new SampleCSharpApp.SampleContactListProvider.SampleContactListProvider();
        // Code that runs when a new session is started

        //added on 06-04-2020 by Vaishali for Notification
        NotificationComponent NC = new NotificationComponent();
        var currentTime = DateTime.Now;
        HttpContext.Current.Session["LastUpdated"] = currentTime;

        NC.RegisterNotification(currentTime);
    }

    void Session_End(object sender, EventArgs e) 
    {
        // ASPNETChatControl.ChatControl.StopSession();
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
    
       
</script>