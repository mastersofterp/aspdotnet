using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EASendMail;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.IO;

public partial class ACADEMIC_Condonation_Approval : System.Web.UI.Page
{
    Common objCommon = new Common();
    Student objS = new Student();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
    Student objstudent = new Student();
    ConfigController Confi = new ConfigController();
    protected void Page_PreInit(object sender, EventArgs e)
    {
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
                DataSet dsCheck = objSC.CheckCautionApprovalAuthority(Convert.ToInt32(Session["userno"]));
                if (dsCheck.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToInt32(dsCheck.Tables[0].Rows[0]["APP_NO"].ToString()) == 0)
                    {
                        Response.Redirect("~/notauthorized.aspx?page=NoDues_Approval.aspx");
                    }
                }
                PopulateDropDownList();
                if (Session["usertype"].ToString() != "1")
                {
                   // objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "DISTINCT SCHEMENO", "SCHEMENAME", "SCHEMENO>0 AND DEPTNO IN(" + Session["userdeptno"] + ")", "SCHEMENO");

                }
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER WITH (NOLOCK)", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
            }
        }
    }
    protected void PopulateDropDownList()
    {
        //try
        //{
        //    AcademinDashboardController objADEController = new AcademinDashboardController();
        //    DataSet ds = objADEController.Get_College_Session(1, Session["college_nos"].ToString());
        //    ddlCollegesession.Items.Clear();
        //    ddlCollegesession.Items.Add("Please Select");
        //    ddlCollegesession.SelectedItem.Value = "0";

        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        ddlCollegesession.DataSource = ds;
        //        ViewState["College_id"] = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
        //        ddlCollegesession.DataValueField = ds.Tables[0].Columns[0].ToString();
        //        ddlCollegesession.DataTextField = ds.Tables[0].Columns[4].ToString();
        //        ddlCollegesession.DataBind();
        //        ddlCollegesession.SelectedIndex = 0;
        //    }

        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_MarkEntryDashboardWireFrame.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server UnAvailable");
        //}
        string deptnos = (Session["userdeptno"].ToString() == "" || Session["userdeptno"].ToString() == string.Empty) ? "0" : Session["userdeptno"].ToString();

        if (Session["usertype"].ToString() != "1")
        {
            objCommon.FillDropDownList(ddlScheme, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "DISTINCT COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (DB.DEPTNO IN (" + deptnos + ") OR ('" + deptnos + "')='0')", "");
        }
        else
        {
            objCommon.FillDropDownList(ddlScheme, "ACD_COLLEGE_SCHEME_MAPPING", "DISTINCT COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=NoDues_Approval.aspx");
            }
        }
        else
        {
            Response.Redirect("~/notauthorized.aspx?page=NoDues_Approval.aspx");
        }
    }

    protected void btnShowapprovedstud_Click(object sender, EventArgs e)
    {
        BindListView();
    }
    protected void btnsavestatus_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            CustomStatus cs = 0;
            string IDNO = "", courseno = "", semesterno = "",reason="";
            int userNo = Convert.ToInt32(Session["userno"].ToString());
            int userType = Convert.ToInt32(Session["usertype"].ToString());
            DataSet dsColleges = objSC.GetCollegesByUser_For_NoDues(Convert.ToInt32(Session["userno"]));
            string college = string.Empty;
            string actual_College = string.Empty;
            if (dsColleges.Tables.Count > 0)
            {
                if (dsColleges.Tables[1].Rows.Count > 0)
                {
                    for (int i = 0; i < dsColleges.Tables[1].Rows.Count; i++)
                    {
                        college = Convert.ToString(dsColleges.Tables[1].Rows[i]["COLLEGE_NODUES"].ToString());
                        if (college != "" && college != string.Empty)
                        {
                            actual_College += college + ',';

                        }
                    }
                    ViewState["ACTUAL_COLLEGE"] = actual_College;

                }
                actual_College = actual_College.TrimEnd(',');
            }
            string Collegeno = objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", "COLLEGE_ID", "COSCHNO=" + ddlScheme.SelectedValue);
            if (ddlStatus.SelectedValue == "0")
            {
                objCommon.DisplayMessage(this, "Please Select Status !!", this.Page);
                return;
            }
            foreach (ListViewDataItem dataitem in lvAAPath.Items)
            {
                CheckBox chkBoxX = dataitem.FindControl("chktransfer") as CheckBox;
                HiddenField hdfcourseno = dataitem.FindControl("hdfcourseno") as HiddenField;
                HiddenField hdfsemesterno = dataitem.FindControl("hdfsemesterno") as HiddenField;
                TextBox txtreason = dataitem.FindControl("txtreason") as TextBox;
                if (chkBoxX.Checked == true && chkBoxX.Enabled == true)
                {
                    count++;
                    if (ddlStatus.SelectedValue == "2")
                    {
                        if (txtreason.Text == "")
                        {
                            objCommon.DisplayMessage(this, "Please Enter Reason !!", this.Page);
                            return;
                        }
                    }
                    IDNO += chkBoxX.ToolTip + ',';
                    courseno+=hdfcourseno.Value +',';
                    semesterno += hdfsemesterno.Value + ',';
                    reason += txtreason.Text + ',';

                    
                }
            }
            if (count == 0)
            {
                objCommon.DisplayMessage(this, "Please Select At Least One Student !!", this.Page);
                return;
            }
            IDNO = IDNO.TrimEnd(',');
            semesterno = semesterno.TrimEnd(',');
            courseno = courseno.TrimEnd(',');
            reason = reason.TrimEnd(',');
            cs = (CustomStatus)Confi.AddedCondonation(Convert.ToString(Collegeno), Convert.ToInt32(userType), Convert.ToInt32(userNo), IDNO, Convert.ToInt32(ddlStatus.SelectedValue), semesterno, courseno, Convert.ToInt32(ddlCollegesession.SelectedValue), reason);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this, "Record Saved Successfully !!", this.Page);
                ddlStatus.SelectedIndex = 0;
                BindListView();
            }
            else
            {
                objCommon.DisplayMessage(this, "Error in Saving", this.Page);
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlCourse, "ACD_OFFERED_COURSE O INNER JOIN ACD_COURSE C ON C.COURSENO=O.COURSENO", "DISTINCT O.COURSENO", "COURSE_NAME", "O.SESSIONNO=" + Convert.ToInt32(ddlCollegesession.SelectedValue) + "AND O.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + "AND O.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue), "");
    }
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSemester.SelectedIndex = 0;
        ddlCourse.SelectedIndex = 0;
        if (ddlScheme.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlScheme.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_idOVER"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

                objCommon.FillDropDownList(ddlCollegesession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID = " + Convert.ToInt32(ViewState["college_idOVER"]) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");

            }
        }
        else
        {
            ddlCollegesession.Items.Clear();
        }
    }
    protected void ddlCollegesession_SelectedIndexChanged(object sender, EventArgs e)
    {
        //objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME S INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON CDB.DEGREENO=S.DEGREENO AND S.BRANCHNO=CDB.BRANCHNO", "DISTINCT SCHEMENO", "SCHEMENAME", "SCHEMENO>0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["College_id"]), "SCHEMENO");

        ddlSemester.SelectedIndex = 0;
        //ddlScheme.SelectedIndex = 0;
        ddlCourse.SelectedIndex = 0;
    }
    private void BindListView()
    {
        try
        {
            int userNo = Convert.ToInt32(Session["userno"].ToString());
            int userType = Convert.ToInt32(Session["usertype"].ToString());
            DataSet dsColleges = objSC.GetCollegesByUser_For_NoDues(Convert.ToInt32(Session["userno"]));
            string college = string.Empty;
            string actual_College = string.Empty;
            if (dsColleges.Tables.Count > 0)
            {
                if (dsColleges.Tables[1].Rows.Count > 0)
                {
                    for (int i = 0; i < dsColleges.Tables[1].Rows.Count; i++)
                    {
                        college = Convert.ToString(dsColleges.Tables[1].Rows[i]["COLLEGE_NODUES"].ToString());
                        if (college != "" && college != string.Empty)
                        {
                            actual_College += college + '$';
                        }
                    }
                    ViewState["ACTUAL_COLLEGE"] = actual_College;

                }
                actual_College = actual_College.TrimEnd('$'); 
            }
            string Collegeno = objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", "COLLEGE_ID", "COSCHNO=" + ddlScheme.SelectedValue);
            string SP_Name2 = "PKG_ACD_GET_STUDENTS_FOR_CONDOLANCE_APPROVAL";
            string SP_Parameters2 = "@P_COLLEGE_NO,@P_UA_TYPE,@P_UANO,@P_SESSIONNO,@P_SCHEMNO,@P_SEMESTERNO,@P_COURSENO";
            string Call_Values2 = "" + Collegeno + "," + userType + "," + userNo + "," + Convert.ToInt32(ddlCollegesession.SelectedValue) + "," + Convert.ToInt32(ViewState["schemeno"]) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," + Convert.ToInt32(ddlCourse.SelectedValue) + "";
            DataSet dsStudList = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            if (dsStudList.Tables.Count != 0)
            {
                if (dsStudList.Tables[0].Rows.Count > 0)
                {
                    btnsavestatus.Visible = true;
                    pnlAAPaList.Visible = true;
                    lvAAPath.DataSource = dsStudList;
                    lvAAPath.DataBind();
                }
                else
                {
                    btnsavestatus.Visible = false;
                    pnlAAPaList.Visible = false;
                    lvAAPath.DataSource = null;
                    lvAAPath.DataBind();
                    objCommon.DisplayMessage(this.UpdApproved, "Record Not Found!", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(this.UpdApproved, "Record Not Found!", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlCourse.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlCollegesession.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        btnsavestatus.Visible = false;
        pnlAAPaList.Visible = false;
        lvAAPath.DataSource = null;
        lvAAPath.DataBind();

    }
    //protected void btnExcel_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int userNo = Convert.ToInt32(Session["userno"].ToString());
    //        int userType = Convert.ToInt32(Session["usertype"].ToString());
    //        DataSet dsColleges = objSC.GetCollegesByUser_For_NoDues(Convert.ToInt32(Session["userno"]));
    //        string college = string.Empty;
    //        string actual_College = string.Empty;
    //        if (dsColleges.Tables.Count > 0)
    //        {
    //            if (dsColleges.Tables[1].Rows.Count > 0)
    //            {
    //                for (int i = 0; i < dsColleges.Tables[1].Rows.Count; i++)
    //                {
    //                    college = Convert.ToString(dsColleges.Tables[1].Rows[i]["COLLEGE_NODUES"].ToString());
    //                    if (college != "" && college != string.Empty)
    //                    {
    //                        actual_College += college + ',';

    //                    }
    //                }
    //                ViewState["ACTUAL_COLLEGE"] = actual_College;

    //            }
    //            actual_College = actual_College.TrimEnd(',');
    //        }
    //        string SP_Name2 = "PKG_ACD_GET_STUDENTS_FOR_CONDOLANCE_APPROVAL_EXCEL";
    //        string SP_Parameters2 = "@P_COLLEGE_NO,@P_UA_TYPE,@P_UANO,@P_SESSIONNO,@P_SCHEMNO,@P_SEMESTERNO,@P_COURSENO";
    //        string Call_Values2 = "" + actual_College + "," + userType + "," + userNo + "," + Convert.ToInt32(ddlCollegesession.SelectedValue) + "," + Convert.ToInt32(ddlScheme.SelectedValue) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," + Convert.ToInt32(ddlCourse.SelectedValue) + "";
    //        DataSet dsStudList = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
    //        if (dsStudList.Tables[0].Rows.Count > 0)
    //        {
    //            GridView gvStudData = new GridView();
    //            gvStudData.DataSource = dsStudList;
    //            gvStudData.DataBind();
    //            string FinalHead = @"<style>.FinalHead { font-weight:bold; }</style>";
    //            string attachment = "attachment; filename=CondonationStatusList.xls";
    //            Response.ClearContent();
    //            Response.AddHeader("content-disposition", attachment);
    //            Response.ContentType = "application/vnd.MS-excel";
    //            StringWriter sw = new StringWriter();
    //            HtmlTextWriter htw = new HtmlTextWriter(sw);
    //            Response.Write(FinalHead);
    //            gvStudData.RenderControl(htw);
    //            Response.Write(sw.ToString());
    //            Response.End();
    //        }
    //        else
    //        {
    //            objCommon.DisplayMessage(this.UpdApproved, "Record Not Found!", this.Page);
    //            return;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "ACADEMIC_Condonation_Approval.GetStudentID() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}
}