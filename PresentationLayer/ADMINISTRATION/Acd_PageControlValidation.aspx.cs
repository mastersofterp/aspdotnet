using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using System.Data;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.Academic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Academic;
using System.Web.Services;
using Newtonsoft.Json;
using mastersofterp_MAKAUAT;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System.Data.SqlClient;
using System.IO;
using Microsoft.WindowsAzure.Storage.Auth;


/*
---------------------------------------------------------------------------------------------------------------------------                                                                      
Created By : Ashutosh Dhobe                                                                  
Created On : 04-04-2024                                                
Purpose    : To manage student information pages controls                                        
Version    : 1.0.0                                                                 
---------------------------------------------------------------------------------------------------------------------------                                                                        
Version      Modified On       Modified By          Purpose                                                                        
---------------------------------------------------------------------------------------------------------------------------                                                                        
                      
------------------------------------------- -------------------------------------------------------------------------------                                                                                                                     
*/

public partial class Acd_PageControlValidation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    PageControlValidationController objMConfig = new PageControlValidationController();
    ModuleConfig objMod = new ModuleConfig();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
              Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                this.CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
            }
        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Acd_PageControlValidation.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Acd_PageControlValidation.aspx");
        }
    }

    [WebMethod]
    public static string ACD_Getpagename()
    {
        try
        {
            string finaljson;
            Common objCommon = new Common();
            PageControlValidationController objMConfig = new PageControlValidationController();
            DataSet ds = objCommon.FillDropDown("ACD_STUDENT_CONFIG", "DISTINCT MIN(ORGANIZATION_ID) AS ORGANIZATION_ID", "DISPLAYPAGENAME", "DISPLAYPAGENAME IS NOT NULL GROUP BY DISPLAYPAGENAME", string.Empty);
            finaljson = JsonConvert.SerializeObject(ds.Tables[0]);
            return finaljson;
        }
        catch
        {
            return JsonConvert.SerializeObject("");
        }
    }

    [WebMethod]
    public static string GetStudentConfigData(int OrgID, string PageNo, string PageName, string SectionName)
    {
        try
        {
            PageControlValidationController objMConfig = new PageControlValidationController();
            DataSet ds = objMConfig.GetStudentConfigData(OrgID, PageNo, PageName, SectionName);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
        catch
        {
            return JsonConvert.SerializeObject("");
        }
    }

    [WebMethod]
    public static string SaveUpdateStudentconfig(List<StudentModuleConfig> StudentConfig)
    {
        string status = string.Empty;
        try
        {          
            Common objCommon = new Common();
            PageControlValidationController objMConfig = new PageControlValidationController();
            CustomStatus cs = (CustomStatus)objMConfig.SaveUpdateStudentConfig(StudentConfig);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                status = "Record Saved Successfully!";
            }
            else if (cs.Equals(CustomStatus.RecordUpdated))
            {
                status = "Record Updated Successfully!";
            }
            else
            {
                status = "Record Already exist";
            }
        }
        catch 
        {
            return JsonConvert.SerializeObject("");
        }
        return status;
    }

    [WebMethod(EnableSession = true)]
    public static string Getsection(string PageName)
    {
        string finaljson;
        try
        {
            Common objCommon = new Common();
            PageControlValidationController objMConfig = new PageControlValidationController();
            HttpContext.Current.Session["PageName"] = PageName;
            DataSet ds = objCommon.FillDropDown("ACD_STUDENT_CONFIG", "DISTINCT ORGANIZATION_ID", "SECTIONNAME", "PAGENAME='" + PageName + "'AND SECTIONNAME IS NOT NULL", "SECTIONNAME");
            finaljson = JsonConvert.SerializeObject(ds.Tables[0]);
            return finaljson;
        }
        catch
        {
            return JsonConvert.SerializeObject("");
        }
    }
}