//=================================================================================
// PROJECT NAME  :  (RF-CAMPUS)                                                          
// MODULE NAME   : Academic                                     
// CREATION DATE : 25-MAR-2014
// PAGE NAME     : CoAndExtraCurricular_activity
// CREATED BY    : Vipul Tichakule
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_CoAndExtraCurricular_activity : System.Web.UI.Page
{

    #region Page Events

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    
    TeachingPlanController objTeachingPlanController = new TeachingPlanController();

    string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");


    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //Check Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    //Page Authorization
                    // CheckPageAuthorization();


                    Page.Title = Session["coll_name"].ToString();


                }
                string clgcode = objCommon.LookUp("Reff", "College_code", "College_code >0");
                ViewState["college_id"] = clgcode;
            }
        }
        catch (Exception ex)
        {
            throw;
        }


    }
    #endregion;


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string result = string.Empty;
        string programName = txtProgramName.Text;
        string date = txtDate.Text;
        string groupteacher = txtGroupTeacher.Text;
        string principal = txtPrincipal.Text;

       
     
        string orgid =objCommon.LookUp("Reff", "organizationid", "organizationid >0");

        result = objTeachingPlanController.InsertExtraActivityData(programName, date, groupteacher, principal, ViewState["college_id"].ToString(), orgid);
        if (result == "1")
        {
            objCommon.DisplayMessage("Record insert succesfully", this.Page);
            ClearControl();
        }
        else
        {
            objCommon.DisplayMessage("Record not inserted", this.Page);
      
        }

       
    }
   

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_EXT_CUR_ACTIVITY", "PROGRAM_NAME", "PROGRAM_NAME", "", "");

            if (ds != null)
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_COLLEGECODE=" + ViewState["college_id"].ToString(); ;

                divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                divMsg.InnerHtml += " </script>";
            }
            else
            {
                objCommon.DisplayMessage("Record not found", this.Page);
            }
            ////To open new window from Updatepanel
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this, "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }



    //protected void btnCancel_Click(object sender, EventArgs e)
    //{
    //    ClearControl();
        
    //}

    protected void ClearControl()
    {
        try
        {
            txtProgramName.Text = string.Empty;
            txtPrincipal.Text = string.Empty;
            txtGroupTeacher.Text = string.Empty;
            txtDate.Text = string.Empty;
        }
        catch
        {
            throw;
        }
    }

    protected void btnCancel_Click1(object sender, EventArgs e)
    {
         Response.Redirect(Request.Url.ToString());
    }
    protected void btnCoandExtReport_Click(object sender, EventArgs e)
    {
        ShowReport("Co-Carricular And Extra Carricular Activity", "Co_Extra_carricular_actiivty.rpt");
    }
}

