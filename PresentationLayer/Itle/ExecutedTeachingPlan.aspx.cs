using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.NITPRM;

public partial class Itle_TeachingPlan : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ITeachingPlanController myController = new ITeachingPlanController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Check Session
        if (Session["userno"] == null || Session["username"] == null ||
            Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        else
        {
            //Check CourseNo in Session variable,if null then redirect to SelectCourse page. 
            if (Session["ICourseNo"] == null)
            {
                Response.Redirect("~/Itle/selectCourse.aspx?pageno=1445");
            }

            if (!Page.IsPostBack)
            {

                //Page Authorization
                // CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                if (ViewState["action"] == null)
                    ViewState["action"] = "add";

                // temprary provision for current session using session variable [by defaullt value set 1 in db]
                lblSession.Text = Session["SESSION_NAME"].ToString();
                lblSession.ToolTip = Session["SessionNo"].ToString();
                lblCourseName.Text = Session["ICourseName"].ToString();

                objCommon.FillDropDownList(ddlTeachingPlan, "ACD_ITEACHINGPLAN", "PLAN_NO", "CONVERT(NVARCHAR(10),START_DATE,103)+' - '+CONVERT(NVARCHAR(10),START_DATE,103)+', '+SUBJECT ", "SESSIONNO=" + Session["SessionNo"].ToString() + " AND UA_NO=" + Session["userno"].ToString() + " AND COURSENO=" + Session["ICourseNo"].ToString(), "START_DATE DESC");
            }
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(),int.Parse(Session["loginid"].ToString()),0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Itle_TeachingPlan.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Itle_TeachingPlan.aspx");
        }
    }

    protected void ddlTeachingPlan_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlTeachingPlan.SelectedIndex > 0)
            {
                Fill_ListView();
                DataTableReader dtr = myController.GetSinglePlanByPlanNo(Convert.ToInt32(ddlTeachingPlan.SelectedValue));
                if (dtr != null)
                {
                    if (dtr.Read())
                    {
                        lblStartDate.Text = dtr["START_DATE"] == null ? "" : Convert.ToDateTime(dtr["START_DATE"]).ToString("dddd, dd MMMM yyyy hh:mm tt");
                        lblEndDate.Text = dtr["END_DATE"] == null ? "" : Convert.ToDateTime(dtr["END_DATE"]).ToString("dddd, dd MMMM yyyy hh:mm tt");
                        lblSubject.Text = dtr["SUBJECT"] == null ? "" : dtr["SUBJECT"].ToString();
                        divDesc.InnerHtml = dtr["DESCRIPTION"] == null ? "" : dtr["DESCRIPTION"].ToString();
                    }
                    dtr.Close();
                }
                divDetails.Visible = true;
            }
            else
                divDetails.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_TeachingPlan.btnDelete_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            #region Server Side Validation

            #endregion

            // bind data to object
            ExecutedTPlan execPlan = new ExecutedTPlan();
            execPlan.TPlanNo = Convert.ToInt32(ddlTeachingPlan.SelectedValue);
            execPlan.CoveredTopics = txtTopicsCovered.Text.Trim();
            execPlan.Deviation = txtDeviation.Text.Trim();
            execPlan.ReasonForDeviation = txtDevtReason.Text.Trim();
            execPlan.ResourcesUsed = txtResources.Text.Trim();

            if (ViewState["action"] != null)
            {
                CustomStatus cs = CustomStatus.Others;

                if (ViewState["action"].ToString().Equals("add"))
                    cs = (CustomStatus)myController.AddExecutedTPlan(execPlan);
                else
                {
                    execPlan.ExecutedTPlanNo = Convert.ToInt32(ViewState["RecordNo"]);
                    cs = (CustomStatus)myController.UpdateExecutedTPlan(execPlan);
                }

                if (cs == CustomStatus.RecordSaved)
                {
                    objCommon.DisplayMessage("Record saved successfully.", this);
                    ClearControls();
                    Fill_ListView();
                }
                else if (cs == CustomStatus.Error)
                    objCommon.DisplayMessage("Unable to save record.", this);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Itle_TeachingPlan.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int recordNo = int.Parse(btnEdit.CommandArgument);
            ViewState["RecordNo"] = recordNo;
            ViewState["action"] = "edit";

            DataSet ds = myController.GetExecutedTPlanDetails(recordNo);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtTopicsCovered.Text = ds.Tables[0].Rows[0]["COVERED_TOPIC"].ToString();
                txtDeviation.Text = ds.Tables[0].Rows[0]["DEVIATION"].ToString();
                txtDevtReason.Text = ds.Tables[0].Rows[0]["DEVIATION_REASON"].ToString();
                txtResources.Text = ds.Tables[0].Rows[0]["RESOURCES"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_TeachingPlan.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private void Fill_ListView()
    {
        DataSet ds = myController.GetAllExecutedTPlan(Convert.ToInt32(ddlTeachingPlan.SelectedValue));
        if (ds != null && ds.Tables.Count > 0)
        {
            // show all executed teaching plan by selected teaching plan no.
            lvExecTPlan.DataSource = ds;
            lvExecTPlan.DataBind();
        }
    }

    private void ClearControls()
    {
        txtTopicsCovered.Text = "";
        txtDeviation.Text = "";
        txtDevtReason.Text = "";
        txtResources.Text = "";
        ViewState["action"] = "add";
        ViewState["RecordNo"] = null;
    }

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("itle")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Executed_Teaching_Plan";
            url += "&path=~,Reports,Itle,ExecutedTeachingPlanReport.rpt";
            url += "&param=@P_TPLAN_NO=0,@P_SESSION_NO=" + Session["SessionNo"].ToString() + ",@P_USER_NO=" + Session["userno"].ToString() + ",@P_COURSE_NO=" + Session["ICourseNo"].ToString() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            string script = " <script type='text/javascript' language='javascript'> try{";
            script += " window.open('" + url + "','Executed_Teaching_Plan','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            script += " }catch(e){ alert('Error: ' + e.description); } </script>";
            RegisterClientScriptBlock("ShowPopup", script);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_TeachingPlan.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}