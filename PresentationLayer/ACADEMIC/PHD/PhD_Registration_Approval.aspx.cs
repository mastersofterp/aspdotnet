using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_PHD_PhD_Registration_Approval : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    PhdController objPhd = new PhdController();

    #region Page Action
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }


    #endregion

    #region PageLoad
    private void PopulateDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_PHD_REGISTRATION", "distinct ADMBATCH", "DBO.FN_DESC('ADMBATCH',ADMBATCH)ADMBATCHNAME", "ADMBATCH>0", "ADMBATCH");
        
            objCommon.FillDropDownList(ddlSchool, "ACD_COLLEGE_DEGREE_BRANCH DB INNER JOIN ACD_COLLEGE_MASTER M ON(DB.COLLEGE_ID=M.COLLEGE_ID)", "DISTINCT DB. COLLEGE_ID", "M.COLLEGE_NAME", "UGPGOT=3", "COLLEGE_NAME");  //UGPG=3 added by Nikhil L. on 08/11/2022 hard coded for PhD colleges only.
        }
        catch (Exception ex)
        {
            throw;
        }
    }

 
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
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
                   this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //   lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                    //fill dropdown method
                    PopulateDropDown();

                }

            }
            //divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PhD_Registration_Approval.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PhD_Registration_Approval.aspx");
        }
    }

    #endregion 

    #region pagecode
    protected void BindList()
    {
        try
        {
            DataSet ds;
            int AdmBatch = 0;
            int Department = 0;
            int PhDMode = 0;
            int COLLEGE_ID = 0;
             AdmBatch = Convert.ToInt32(ddlAdmBatch.SelectedValue);
             Department = Convert.ToInt32(ddlDepartment.SelectedValue);
             PhDMode = Convert.ToInt32(ddlPhDMode.SelectedValue);
             COLLEGE_ID = Convert.ToInt32(ddlSchool.SelectedValue);
             ds = objPhd.GetStudentListForPhd(AdmBatch, Department, PhDMode, COLLEGE_ID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Panel3.Visible =true;
                lvPhd.DataSource = ds.Tables[0];
                lvPhd.DataBind();
              //  objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudentRecords);//Set label 
            }
            else
            {
                objCommon.DisplayMessage(this.updSection, "No student data found", this);
                lvPhd.DataSource = null;
                lvPhd.DataBind();
                ddlAdmBatch.SelectedIndex = 0;
                ddlDepartment.SelectedIndex = 0;
                ddlPhDMode.SelectedIndex = 0;
                ddlSchool.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            BindList();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void lnkApproved_Click(object sender, EventArgs e)
    {
        StudentRegist objSR = new StudentRegist();
        int cs = 0;
        LinkButton btnApprove = sender as LinkButton;
        int userno = Convert.ToInt32(btnApprove.CommandArgument);
        int AdmBatch = 0;
        int Department = 0;
        int PhDMode = 0;
        int COLLEGE_ID = 0;
        AdmBatch = Convert.ToInt32(ddlAdmBatch.SelectedValue);
        Department = Convert.ToInt32(ddlDepartment.SelectedValue);
        objSR.IPADDRESS = Session["ipAddress"].ToString();
        objSR.UA_NO = Convert.ToInt32(Session["userno"]);
        PhDMode = Convert.ToInt32(ddlPhDMode.SelectedValue);
        COLLEGE_ID = Convert.ToInt32(ddlSchool.SelectedValue);
        cs = objPhd.InsPhDApproved(objSR, AdmBatch, Department, userno, PhDMode, COLLEGE_ID);
        if (cs ==1)
        {
            objCommon.DisplayMessage(updSection, "Student Approved Successfully ", this.Page);
           // btnApprove.Enabled = false;
            BindList();
        }
        else if (cs ==2 )
        {
            objCommon.DisplayMessage(updSection, " Student is Already Approved", this.Page);
        }
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        
        try
        {
            LinkButton btnview = sender as LinkButton;
            int userno = Convert.ToInt32(btnview.CommandArgument);
            if(Convert.ToInt32(Session["OrgId"]) == 3)
            {
            ShowReport1("ApplicationForm", "rptPhDApplicationForm_cpuk.rpt", userno);
            }
            else 
            {
                 ShowReport1("ApplicationForm", "rptPhDApplicationForm.rpt", userno);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void ShowReport1(string reportTitle, string rptFileName, int userno)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_USERNO=" + Convert.ToInt32(userno) + ",@P_COLLEGE_CODE="+ Session["colcode"].ToString() +"";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updPhdlistview, this.updPhdlistview.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Default2.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlPhDMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        Panel3.Visible = false;
        lvPhd.DataSource = null;
        lvPhd.DataBind();
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        Panel3.Visible = false;
        lvPhd.DataSource = null;
        lvPhd.DataBind();
    }
    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        Panel3.Visible = false;
        lvPhd.DataSource = null;
        lvPhd.DataBind();
    }
    protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Panel3.Visible = false;
            lvPhd.DataSource = null;
            lvPhd.DataBind();
            if (ddlSchool.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlDepartment, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=CDB.BRANCHNO) ", "CDB.BRANCHNO", "B.SHORTNAME", "CDB.UGPGOT=3 AND COLLEGE_ID=" + Convert.ToInt32(ddlSchool.SelectedValue), "CDB.BRANCHNO");
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(`Something went wrong.`)", true);
            return;
        }
    }
    #endregion
}