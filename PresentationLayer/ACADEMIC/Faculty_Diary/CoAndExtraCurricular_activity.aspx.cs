//=================================================================================
// PROJECT NAME  :  (RF-CAMPUS)                                                          
// MODULE NAME   : Academic                                     
// CREATION DATE : 25-MAR-2014
// PAGE NAME     : CoAndExtraCurricular_activity
// CREATED BY    : Vipul Tichakule
// MODIFIED BY   : GUNESH MOHANE
// MODIFIED DATE : 30/03/2024 
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
                PopulateCollege();
                //bindList();
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

    #region DropDown Events
    protected void PopulateCollege()
    {
        try
        {
            if (Session["usertype"].ToString() == "3")
            {
                if (Session["usertype"].ToString() != "1")
                {
                    //objCommon.FillDropDownList(ddlClgname, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COLLEGE_SCHEME_MAPPING SC ON (SC.SCHEMENO = CT.SCHEMENO)", "DISTINCT SC.COSCHNO", "SC.COL_SCHEME_NAME", "(CT.UA_NO=" + Convert.ToInt32(Session["userno"]) + "OR CT.ADTEACHER = " + Convert.ToInt32(Session["userno"]) + ")", "SC.COSCHNO");
                    objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (CASE WHEN '" + Session["userdeptno"] + "' ='0'  THEN '0' ELSE DB.DEPTNO END) IN (" + Session["userdeptno"] + ")", "");
                }
                else
                {
                    objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CoAndExtraCurricular_activity.PopulateCollege-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void PoupulateSession()
    {
        try
        {
            if (ddlClgname.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));
                //ViewState["degreeno"]

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                {
                    ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                    ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                    ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                    ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

                    objCommon.FillDropDownList(ddlSession, "ACD_COURSE_TEACHER CT INNER JOIN ACD_SESSION_MASTER C ON (CT.SESSIONNO=C.SESSIONNO)", "DISTINCT C.SESSIONNO", "C.SESSION_NAME", "C.SESSIONNO > 0 AND ISNULL(C.IS_ACTIVE,0)=1 AND C.COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]) + " AND C.OrganizationId=" + Convert.ToInt32(Session["OrgId"]) + "AND CT.UA_NO=" + Convert.ToInt32(Session["userno"]), "C.SESSIONNO DESC");
                    ddlSession.Focus();
                    bindList(0, Convert.ToInt32(ddlClgname.SelectedValue));
                    
                }
            }
            else
            {
                lvExtraActivity.DataSource = null;
                lvExtraActivity.DataBind();
                Panel1.Visible = false;
                ddlSession.Items.Clear();
                ddlSession.Items.Add(new ListItem("Please Select", "0"));
                ddlClgname.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CoAndExtraCurricular_activity.PoupulateSession-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            PoupulateSession();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CoAndExtraCurricular_activity.ddlClgname_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindList(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlClgname.SelectedValue));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CoAndExtraCurricular_activity.ddlSession_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    #region Button Events
    protected void bindList(int sessionno, int schemeno) 
    {
        string clgcode = ViewState["college_id"].ToString();
        string uano = Session["userno"].ToString();
        try
        {
            DataSet ds = objTeachingPlanController.BindCoAndExtraCurricular_activity(clgcode, uano,sessionno,schemeno);
            if (ds != null && ds.Tables.Count > 0)
            {
                lvExtraActivity.DataSource = ds;
                lvExtraActivity.DataBind();
                Panel1.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CoAndExtraCurricular_activity.bindList-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int schemeno = Convert.ToInt32(ddlClgname.SelectedValue);
            int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            string result = string.Empty;
            string programName = txtProgramName.Text;
            string date = txtDate.Text;
            string groupteacher = txtGroupTeacher.Text;
            string principal = txtPrincipal.Text;

            string orgid = objCommon.LookUp("Reff", "organizationid", "organizationid >0");
            string uano = Session["userno"].ToString();

            if (hfid.Value == string.Empty)
            {
                result = objTeachingPlanController.InsertExtraActivityData(programName, date, groupteacher, principal, ViewState["college_id"].ToString(), orgid, uano, schemeno, sessionno);
                if (result == "1")
                {
                    objCommon.DisplayMessage("Record Inserted Succesfully", this.Page);
                    ClearControl();
                }
                if (result == "-1001") 
                {
                    objCommon.DisplayMessage("Record Already Exists.", this.Page);
                    ClearControl();
                }
                else
                    objCommon.DisplayMessage("Record not Inserted", this.Page);
            }
            else
            {
                result = objTeachingPlanController.UpdateCoAndExtraCurricular_activityByID(Convert.ToInt32(hfid.Value), programName, date, groupteacher, principal, ViewState["college_id"].ToString(), orgid, uano, schemeno, sessionno);
                if (result == "2")
                {
                    objCommon.DisplayMessage("Record Updated Succesfully", this.Page);
                    ClearControl();
                }
                else
                    objCommon.DisplayMessage("Record not Updated", this.Page);
            }

            bindList(sessionno, schemeno);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CoAndExtraCurricular_activity.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
   
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string clgcode = ViewState["college_id"].ToString();
            string uano = Session["userno"].ToString();
            int schemeno = Convert.ToInt32(ddlClgname.SelectedValue);
            int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            DataSet ds = objTeachingPlanController.CheckDatainReportCo_Extra(Convert.ToInt32(clgcode),Convert.ToInt32(uano),schemeno,sessionno);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_COLLEGECODE=" + ViewState["college_id"].ToString() + ",@P_UANO=" + Convert.ToInt32(Session["userno"].ToString()) + ",@P_SCHEMENO=" + ddlClgname.SelectedValue + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGE_CODE=" + ViewState["college_id"].ToString(); // ViewState["college_id"].ToString(); ;

                //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                //divMsg.InnerHtml += " </script>";

                ////To open new window from Updatepanel
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "controlJSScript", sb.ToString(), true);
            }
            else
            {
                objCommon.DisplayMessage("Record not found", this.Page);
            }
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CoAndExtraCurricular_activity.PopulateCollege-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
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
            hfid.Value = string.Empty;
            ddlClgname.SelectedIndex = 0;
            ddlSession.ClearSelection();
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
        try
        {
            if (ddlClgname.SelectedIndex != 0)
                if (ddlSession.SelectedIndex != 0)
                    ShowReport("Co-Carricular And Extra Carricular Activity", "Co_Extra_carricular_actiivty.rpt");
                else
                    objCommon.DisplayMessage("Please Select Session", this.Page);
            else
                objCommon.DisplayMessage("Please Select College & Scheme", this.Page);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CoAndExtraCurricular_activity.PopulateCollege-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnRemove_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnRemoveRecord = sender as ImageButton;
            int id = int.Parse(btnRemoveRecord.CommandArgument);
            string result = objTeachingPlanController.DeleteCoAndExtraCurricular_activity(id);
            int schemeno = Convert.ToInt32(ddlClgname.SelectedValue);
            int sessionno = Convert.ToInt32(ddlSession.SelectedValue);

            if (result == "3")
            {
                objCommon.DisplayMessage("Record Deleted Succesfully", this.Page);
            }
            else
                objCommon.DisplayMessage("Record Not Deleted", this.Page);
            bindList(schemeno, sessionno);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CoAndExtraCurricular_activity.PopulateCollege-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnRemoveRecord = sender as ImageButton;
            int id = int.Parse(btnRemoveRecord.CommandArgument);
            ViewState["edit"] = "edit";
            DataSet ds = objTeachingPlanController.GetCoAndExtraCurricular_activityByID(id);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                hfid.Value = id.ToString();
                PopulateCollege();
                ddlClgname.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["SCHEMENO"]);
                PoupulateSession();
                ddlSession.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["SESSIONNO"]);
                txtProgramName.Text = Convert.ToString(ds.Tables[0].Rows[0]["PROGRAM_NAME"]);
                txtDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["DATE"]).ToString("dd/MM/yyyy");
                txtPrincipal.Text = Convert.ToString(ds.Tables[0].Rows[0]["PRINCIPAL_DETAILS"]);
                txtGroupTeacher.Text = Convert.ToString(ds.Tables[0].Rows[0]["GROUP_TEACHER"]);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CoAndExtraCurricular_activity.PopulateCollege-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion
}

