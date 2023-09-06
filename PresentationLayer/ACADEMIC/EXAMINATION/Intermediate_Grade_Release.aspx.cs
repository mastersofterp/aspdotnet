using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;

public partial class ACADEMIC_EXAMINATION_Intermediate_Grade_Release : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ConfigController objconfig = new ConfigController();
    ExamController objexam = new ExamController();
    string th_pr = string.Empty;
    int subid;
    string schemeno = string.Empty;
    string regnos;
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

                Page.Title = Session["coll_name"].ToString();
                
            }
           //get College ID
            ViewState["College_ID"] = objCommon.LookUp("User_Acc", "UA_COLLEGE_NOS", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()));
            //get College and schema
            this.PopulateDropDown();
           
            ViewState["edit"] = null;
            //Session["action"] = null;


        }
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
    }
    private void PopulateDropDown()
    {
        try
        {
            if (Session["usertype"].ToString().Equals("1"))
            {

                objCommon.FillDropDownList(ddlSchool, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + ViewState["College_ID"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID ASC");
            }
            else
            {
                objCommon.FillDropDownList(ddlSchool, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + ViewState["College_ID"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID ASC");
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Intermediate_Grade_Release.PopulateDropDown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GetDetails();
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", " DISTINCT SESSIONNO ", "SESSION_NAME", "COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + "", "SESSIONNO DESC");
          
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Intermediate_Grade_Release.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void GetDetails()
    {
        try
        {
            ViewState["SchemeNo"] = string.Empty;
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlSchool.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["SchemeNo"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Intermediate_Grade_Release.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void GetUGPG()
    {
        try
        {

            ViewState["UGPGOT"] = string.Empty;
            GetDetails();
            DataSet ds = objCommon.GetUGPG(Convert.ToInt32(ViewState["degreeno"]),Convert.ToInt32(ddlSchool.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["UGPGOT"] = Convert.ToInt32(ds.Tables[0].Rows[0]["UGPGOT"]).ToString();
              
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Intermediate_Grade_Release.GetUGPG-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlCourse.Items.Clear();
            objCommon.FillDropDownList(ddlCourse,"ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE SR ON C.COURSENO = SR.COURSENO", "C.COURSENO", "(C.CCODE + ' - ' + C.COURSE_NAME) COURSE_NAME ", "SR.SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue), "C.COURSE_NAME DESC");
            ddlCourse.Items.Add(new ListItem("Please Select", "0"));
            ddlCourse.Focus();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Intermediate_Grade_Release.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ExamController objexam = new ExamController();
             DataSet ds = null;

             ds = objexam.GetGradeRelease(Convert.ToInt32(ddlCourse.SelectedValue));
             if (ds.Tables[0].Rows.Count > 0)
             {
                 ddlGradeRelease.DataSource = ds;
                 ddlGradeRelease.DataTextField = "GRADEREALSE";
                 ddlGradeRelease.DataValueField = "COURSENO";
                 ddlGradeRelease.DataBind();
             }
             ddlGradeRelease.Focus();
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Intermediate_Grade_Release.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }


    #region List Bind For Exam Component
    private void BinGridAssesment()
    {
        try
        {
          // GetUGPG();
           DataSet ds = null;
           ds = objexam.GetGradeTable(Convert.ToInt32(ddlSession.SelectedValue),Convert.ToInt32(ddlCourse.SelectedValue));
           lvGrade.DataSource = ds;
           lvGrade.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Intermediate_Grade_Release.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion  List Bind View


    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            GetDetails();

            if (ddlSchool.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(updGradeRelease, "Please Select College, Session and Course/Subject", this.Page);
            }
            else if (ddlSession.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(updGradeRelease, "Please Select  Session ", this.Page);
            }
            else if (ddlCourse.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(updGradeRelease, "Please Select Course/Subject", this.Page);
            }
           else
            {
                BinGridAssesment();
                DataSet ds = objexam.Show_Grade_Release(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue),Convert.ToInt32(ViewState["SchemeNo"]));
                ViewState["SEMESTERNO"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SEMESTERNO"].ToString());
                ViewState["PREV_STATUS"] = Convert.ToInt32(ds.Tables[0].Rows[0]["PREV_STATUS"].ToString());
                lvGradeList.DataSource = ds;
                lvGradeList.DataBind();
                Gradelist();
            }
        }
        catch(Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Intermediate_Grade_Release.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    public void Gradelist()
    {
        try
        {
            string COMP = objCommon.LookUp("ACD_INTERMEDIATE_GRADE_CONFIGURATION", "SUBMENUEXAM", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND COURSENO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["SchemeNo"]) + "AND SUBMENUEXAM=" + Convert.ToString(ddlGradeRelease.SelectedValue));

            objexam.GradeAllotment123(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ViewState["SchemeNo"]), Convert.ToInt32(ViewState["SEMESTERNO"]), Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(ViewState["PREV_STATUS"]), Convert.ToString(COMP));
        }
        catch(Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Intermediate_Grade_Release.Gradelist-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void Clear()
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    public void getSemesterandpreviousStstus()
    {

     
    }
    private void Getgradename()
    {
        try
        {

            // ViewState["UGPGOT"] = string.Empty;
            //  GetDetails();
            DataSet ds = objexam.GetGradename(Convert.ToString(ddlGradeRelease.SelectedItem.Text));

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["GRADE_COLUMN_NAME"] = Convert.ToString(ds.Tables[0].Rows[0]["GRADE_COLUMN_NAME"]).ToString();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Intermediate_Grade_Release.GetUGPG-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnAllotGrade_Click(object sender, EventArgs e)
    {
        
        try
        {
            CustomStatus cs = 0;
            GetDetails();
            Gradelist();
            Getgradename();
             
             foreach (ListViewDataItem item in lvGradeList.Items)
           {
                 CheckBox chk = item.FindControl("chkAction") as CheckBox;
                 HiddenField sturegno = item.FindControl("hdfregno") as HiddenField;

                //string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((item.FindControl("lblStudname")) as Label).ToolTip) + ""));
                if (chk.Checked)
                {
                   
                    regnos +=sturegno.Value + ",";

                    //GenerateQrCode((((item.FindControl("lblStudname")) as Label).ToolTip), RegNo, (((item.FindControl("lblStudname")) as Label).Text));
                }
           }

           objexam.GradeAllotmentbyuser(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ViewState["SchemeNo"]), Convert.ToInt32(ViewState["SEMESTERNO"]), regnos, Convert.ToString(ViewState["GRADE_COLUMN_NAME"]));
              
              

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Intermediate_Grade_Release.btnAllotGrade-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnPublish_Click(object sender, EventArgs e)
    {
        try
        {
           // Getgradename();

            objexam.GradeAllotmentbyuser(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ViewState["SchemeNo"]), Convert.ToInt32(ViewState["SEMESTERNO"]), regnos, Convert.ToString(ViewState["GRADE_COLUMN_NAME"]));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Intermediate_Grade_Release.btnAllotGrade-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
   
  }
    protected void btnUnPublish_Click(object sender, EventArgs e)
    {
        try
        {
            //Getgradename();
            objexam.GradeAllotmentbyuser(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ViewState["SchemeNo"]), Convert.ToInt32(ViewState["SEMESTERNO"]), regnos, Convert.ToString(ViewState["GRADE_COLUMN_NAME"]));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Intermediate_Grade_Release.btnAllotGrade-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
  
}