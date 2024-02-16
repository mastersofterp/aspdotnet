﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.Data;
using System.Data.SqlClient;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Net;
using System.IO;
using System.Collections;
using System.Text;
using System.Web.UI.HtmlControls;
using Newtonsoft.Json;
using System.Text;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Collections.Specialized;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Auth;

public partial class ACADEMIC_ErrorHandling_ErrPageForDefault : System.Web.UI.Page
{
    Common objCommon = new Common();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            showemail();
        }
    }

    protected void lbtnBackToPage_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/default.aspx");
    }

    protected void showemail()
    {
        string email = objCommon.LookUp("Reff", "ERROR_LOG_EMAIL", "");

        if (email != null && email != string.Empty)
        {
            lblemail.Text = email;
        }
        else
        {
            errormsg.Visible = false;
        }
    }
}