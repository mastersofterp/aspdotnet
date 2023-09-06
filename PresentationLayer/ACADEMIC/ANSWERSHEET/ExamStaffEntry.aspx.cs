//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : EVALUATOR ORDER                                                     
// CREATION DATE : 17-JUNE-2017                                                          
// CREATED BY    : ROHIT KUMAR TIWARI                               
// MODIFIED by   :                                                    
// MODIFIED DESC :                                                                      
//======================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_ANSWERSHEET_ExamStaffEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    Student objs = new Student();
    UAIMS_Common objUCommon = new UAIMS_Common();
    CheckBox chk;
    TextBox txtChk;
    DataSet dsForChk;

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

        if (!IsPostBack)
        {
            dsForChk = objCommon.FillDropDown("ACD_STAFF_TYPE", "STAFFTYPE_NO", "STAFFTYPE_NAME", "STAFFTYPE_NO>0", "STAFFTYPE_NO");
            this.GetStaffType();
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                ////Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
                objCommon.FillDropDownList(ddlStaffType, "ACD_STAFF_TYPE", "STAFFTYPE_NO", "STAFFTYPE_NAME", "STAFFTYPE_NO>0", "STAFFTYPE_NAME ASC");  //Added on 07112022
                //objCommon.FillDropDownList(ddlBranch, "ACD_DEPARTMENT", "DEPTNO", "DISTINCT DEPTNAME", "DEPTNO > 0", "DEPTNAME ASC");
                objCommon.FillDropDownList(ddlBranch, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME+' - '+DEPTCODE", "DEPTNO > 0", "DEPTNAME ASC");
                BindListView();
            }

        }
        else
        {

        }


    }
    private void GetStaffType()
    {
        Session["Forchk"] = (DataSet)dsForChk;
        DataSet ds = (DataSet)Session["Forchk"];

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlStaffType.DataSource = ds;
            ddlStaffType.DataTextField = "STAFFTYPE_NAME";
            ddlStaffType.DataValueField = "STAFFTYPE_NO";
            ddlStaffType.DataBind();


        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=SessionCreate.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=SessionCreate.aspx");
        }
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            DataSet ds = objCommon.FillDropDown("ACD_EXAM_STAFF", "EXAM_STAFF_NO", "STAFF_NAME, COLLEGE_ADDRESS, (case STAFF_TYPE when '1' then 'Internal' when '2' then 'External' end)STAFF_TYPE, ACTIVE", "EXAM_STAFF_NO > 0 AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue), "STAFF_TYPE,STAFF_NAME");
            lvSession.DataSource = ds;
            lvSession.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_SessionCreate.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        int N = ddlName.SelectedIndex;
        if(ddlName.Visible == true)
        {
        if (ddlName.SelectedIndex <= 0)
        {
            objCommon.DisplayMessage(this.updSession, "Please Select Faculty Name", this.Page);
            return;
        }
            }

        string s = string.Empty;
        int status = 0;
        foreach (ListItem li in ddlStaffType.Items)
        {

            if (li.Selected)
            {
                s = s + li.Value + ',';
            }
            
        }

        if (s == null || s == "")
        {
            objCommon.DisplayMessage(this.updSession, "Please Select atleast one CheckBox", this.Page);
            return;
        }
        int editRec = Convert.ToInt32(hdnEditedRecord.Value);
        CustomStatus retSt = 0;
        //if (hdfStatus.Value == "true")
        //{
        //    status = 1;
        //}


        //int ActiveStatus;
        //if (hdFeeApplicable.Value == "true")
        if (rblStatus.Checked == true)
        {
            status = 1;
        }
        else
        {
            status = 0;
        }

        try
        {
            AnswerSheetController objAns = new AnswerSheetController();

            if (editRec <= 0)
            {
                        //if (chkOther.Checked == true)

                        if (ddlStaffType.SelectedValue == "2")
                        {
                            retSt = (CustomStatus)objAns.AddSaveExam(txtName.Text.Trim(), txtAddress.Text, txtMobile.Text.Trim(), txtEmail.Text.Trim(), s, status, Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32((chkOther.Checked ? 1 : 0)), Convert.ToInt32(ddlName.SelectedValue));

                        }
                        else
                        {
                            retSt = (CustomStatus)objAns.AddSaveExam(ddlName.SelectedItem.Text, txtAddress.Text, txtMobile.Text.Trim(), txtEmail.Text.Trim(), s, status, Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32((chkOther.Checked ? 1 : 0)), Convert.ToInt32(ddlName.SelectedValue));
                        }
                        if (retSt.Equals(CustomStatus.RecordSaved) )
                        {
                            ClearControls();
                            BindListView();
                            objCommon.DisplayMessage(this.updSession, "Record Saved Successfully", this.Page);
                        }
                        else if(retSt.Equals(CustomStatus.DuplicateRecord))
                        {
                            
                            objCommon.DisplayMessage(this.updSession, " Record Already Exists", this.Page);
                        }
            }

            else
            {                    
                    int staffno = Convert.ToInt32(ViewState["StaffNo"]);
                    if (ddlStaffType.SelectedValue == "2")
                    {
                        retSt = (CustomStatus)objAns.UpdateExamStaff(txtName.Text.Trim(), txtAddress.Text.Trim(), txtMobile.Text.Trim(), txtEmail.Text.Trim(), s, status, Convert.ToInt32(ddlBranch.SelectedValue), staffno, Convert.ToInt32((chkOther.Checked ? 1 : 0)), Convert.ToInt32(ddlName.SelectedValue));

                    }
                    else
                    {
                        retSt = (CustomStatus)objAns.UpdateExamStaff(ddlName.SelectedItem.Text, txtAddress.Text.Trim(), txtMobile.Text.Trim(), txtEmail.Text.Trim(), s, status, Convert.ToInt32(ddlBranch.SelectedValue), staffno, Convert.ToInt32((chkOther.Checked ? 1 : 0)), Convert.ToInt32(ddlName.SelectedValue));

                    }
                    if (retSt.Equals(CustomStatus.DuplicateRecord))
                    {
                        objCommon.DisplayMessage(this.updSession, " Faculty Already Exists", this.Page);
                    }
                    else if (retSt.Equals(CustomStatus.Error))
                    {
                        objCommon.DisplayMessage(this.updSession, " Something Went Wrong", this.Page);
                    }
                    else if (retSt.Equals(CustomStatus.RecordUpdated))
                    {

                        ClearControls();
                        BindListView();
                        objCommon.DisplayMessage(this.updSession, "Record Updated Successfully", this.Page);
                    }
            }



        }


        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.updSession, "Record Failed", this.Page);
            //return;
        }
    }


    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            hdnEditedRecord.Value = btnEdit.CommandArgument;
            ViewState["StaffNo"] = int.Parse(btnEdit.CommandArgument);
            this.ShowDetails(int.Parse(btnEdit.CommandArgument));
            //btnUpdate.Visible = true;
            //btnSubmit.Visible = false;
            btnSubmit.Text = "UPDATE";
            ddlStaffType.Enabled = false;






        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_SessionCreate.btnEdit_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void ShowDetails(int EStaff_No)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_EXAM_STAFF", "EXAM_STAFF_NO", "STAFF_NAME,UA_NO,MOBILE_NO,EMAIL_ID,ISNULL(SESSIONNO,0)SESSIONNO,ISNULL(DEPTNO,0)DEPTNO, COLLEGE_ADDRESS, STAFF_TYPE, ACTIVE,ISOTHER_FACULTY", "EXAM_STAFF_NO = " + EStaff_No, "EXAM_STAFF_NO");


            //string otherFaculty = objCommon.LookUp("ACD_EXAM_STAFF", "ISOTHER_FACULTY", "EXAM_STAFF_NO='" + EStaff_No + "'");
            string External = objCommon.LookUp("ACD_EXAM_STAFF", "STAFF_TYPE", "EXAM_STAFF_NO='" + EStaff_No + "'");
            if (ds != null || ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    if (External == "2")
                    {
                        //chkOther.Checked = true;
                        txtName.Visible = true;
                        ddlName.Visible = false;
                        txtName.Text = ds.Tables[0].Rows[0]["STAFF_NAME"].ToString();

                    }
                    else
                    {

                        //chkOther.Checked = false;
                        ddlName.Visible = true;
                        txtName.Visible = false;
                        //objCommon.FillDropDownList(ddlName, "USER_ACC U INNER JOIN ACD_DEPARTMENT D ON U.UA_DEPTNO=D.DEPTNO", "U.UA_FULLNAME", "U.UA_FULLNAME", "UA_TYPE=" + 3 + "AND UA_DEPTNO=" + Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"].ToString()) + "", "U.UA_FULLNAME");


                        //objCommon.FillDropDownList(ddlName, "USER_ACC U CROSS APPLY STRING_SPLIT(U.UA_DEPTNO, ',') [GR]  INNER JOIN ACD_DEPARTMENT D ON [GR].value=D.DEPTNO", "U.UA_NO", "U.UA_FULLNAME", "UA_TYPE=3 AND [GR].[VALUE] IN (SELECT [VALUE] FROM DBO.SPLIT('" + Convert.ToInt32(ds.Tables[0].Rows[0]["DEPTNO"].ToString()) + "', ','))" + "", "U.UA_FULLNAME");

                        ddlSession.SelectedValue = ds.Tables[0].Rows[0]["SESSIONNO"].ToString();
                        ddlBranch.SelectedValue = ds.Tables[0].Rows[0]["DEPTNO"].ToString();

                        objCommon.FillDropDownList(ddlName, "USER_ACC U CROSS APPLY STRING_SPLIT(U.UA_DEPTNO, ',') [GR]  INNER JOIN ACD_DEPARTMENT D ON [GR].value=D.DEPTNO", "U.UA_NO", "U.UA_FULLNAME", "UA_TYPE=3 AND [GR].[VALUE] IN (SELECT [VALUE] FROM DBO.SPLIT('" + ddlBranch.SelectedValue.ToString() + "', ','))" + "", "U.UA_FULLNAME");
                        ddlName.SelectedValue = ds.Tables[0].Rows[0]["UA_NO"].ToString();
                       // ddlName.SelectedItem.Text = ds.Tables[0].Rows[0]["STAFF_NAME"].ToString();


                    }
                    ddlBranch.SelectedValue = ds.Tables[0].Rows[0]["DEPTNO"].ToString();
                    txtAddress.Text = ds.Tables[0].Rows[0]["COLLEGE_ADDRESS"].ToString();
                    txtMobile.Text = ds.Tables[0].Rows[0]["MOBILE_NO"].ToString();
                    txtEmail.Text = ds.Tables[0].Rows[0]["EMAIL_ID"].ToString();

                    string[] chkGet = null;
                    chkGet = ds.Tables[0].Rows[0]["STAFF_TYPE"].ToString().Split(',');

                    //Clear already selected checkbox
                    foreach (ListItem li in ddlStaffType.Items)
                    {
                        if (li.Selected)
                            li.Selected = false;
                    }
                    //

                    //Bind new selected checkbox through DB
                    foreach (string item in chkGet)
                    {
                        foreach (ListItem li in ddlStaffType.Items)
                        {
                            if (li.Value == item)
                            {
                                li.Selected = true;
                            }
                        }
                    }
                    //

                    
                   // rblStatus.SelectedValue = ds.Tables[0].Rows[0]["ACTIVE"].ToString().ToLower() == "true" ? "Active" : "De-Active";


                    if (ds.Tables[0].Rows[0]["ACTIVE"].ToString().ToLower() == "true")
                    {

                        rblStatus.Checked = true;
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "onoff(true);", true);


                    }
                    else
                    {
                        rblStatus.Checked = false;
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "onoff(false);", true);

                    }

                }
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        Page.Response.Redirect(Page.Request.Url.ToString(), true);
    }
    private void ClearControls()
    {
        ddlSession.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        txtName.Text = string.Empty;
        txtAddress.Text = string.Empty;
        hdnEditedRecord.Value = "0";
        txtEmail.Text = string.Empty;
        txtMobile.Text = string.Empty;
        ddlName.SelectedIndex = 0;
       // rblStatus.SelectedIndex = -1;
        ddlStaffType.SelectedIndex = 0;
        ddlStaffType.Enabled = true;
        //btnUpdate.Visible = false;
        //btnSubmit.Visible = true;
        btnSubmit.Text = "SUBMIT";
        //lvSession.DataSource = null;

       

    }
    private void BindListView()
    {
        try
        {

            DataSet ds = objCommon.FillDropDown("ACD_EXAM_STAFF S inner join ACD_DEPARTMENT D on S.DEPTNO=D.DEPTNO", "EXAM_STAFF_NO", "STAFF_NAME,MOBILE_NO,EMAIL_ID, (case STAFF_TYPE when '1' then 'INTERNAL' when '2' then 'EXTERNAL' end)STAFF_TYPE, (case ACTIVE when '1' then 'Active' when '0' then 'De-Active' end)ACTIVE,DEPTNAME", "EXAM_STAFF_NO > 0", "EXAM_STAFF_NO DESC");

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvSession.DataSource = ds;
                lvSession.DataBind();
                lvSession.Visible = true;
            }
            else
            {
                lvSession.DataSource = null;

                lvSession.DataBind();
                lvSession.Visible = false;

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_SessionCreate.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }



    }



    protected void ddlBranch_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_EXAM_STAFF S  INNER JOIN ACD_DEPARTMENT D ON S.DEPTNO=D.DEPTNO ", "EXAM_STAFF_NO", "STAFF_NAME, COLLEGE_ADDRESS,MOBILE_NO,EMAIL_ID, (case STAFF_TYPE when '1' then 'Internal' when '2' then 'External' when '3' then 'Paper Setter' when '4' then 'Moderator' when '4' then 'Valuer' end)STAFF_TYPE,(case ACTIVE when   '1' then 'Active' when '0' then 'De-Active' end)as ACTIVE,DEPTNAME", "EXAM_STAFF_NO > 0 AND D.DEPTNO=" + ddlBranch.SelectedValue, "STAFF_NAME ASC");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvSession.DataSource = ds;
                lvSession.DataBind();
                lvSession.Visible = true;
            }
            else
            {
                lvSession.DataSource = null;

                lvSession.DataBind();
                lvSession.Visible = false;

            }

            //objCommon.FillDropDownList(ddlName, "USER_ACC U INNER JOIN ACD_DEPARTMENT D ON U.UA_DEPTNO=D.DEPTNO", "U.UA_FULLNAME", "U.UA_FULLNAME", "UA_TYPE=" + 3 + "AND UA_DEPTNO=" + ddlBranch.SelectedValue.ToString() + "", "U.UA_FULLNAME");

            objCommon.FillDropDownList(ddlName, "USER_ACC U CROSS APPLY STRING_SPLIT(U.UA_DEPTNO, ',') [GR]  INNER JOIN ACD_DEPARTMENT D ON [GR].value=D.DEPTNO", "U.UA_NO", "U.UA_FULLNAME", "UA_TYPE=3 AND [GR].[VALUE] IN (SELECT [VALUE] FROM DBO.SPLIT('"+ddlBranch.SelectedValue.ToString()+"', ','))" + "", "U.UA_FULLNAME");

            //objCommon.FillDropDownList(ddlName, "USER_ACC U INNER JOIN ACD_DEPARTMENT D ON U.UA_DEPTNO=D.DEPTNO", "U.UA_FULLNAME", "U.UA_FULLNAME", "UA_TYPE=" + 3 + "AND UA_DEPTNO=" + ddlBranch.SelectedValue.ToString() + "", "U.UA_FULLNAME");
       
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_SessionCreate.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }

    }
    protected void ddlName_SelectedIndexChanged(object sender, EventArgs e)
    {

        string txtMobile1 = objCommon.LookUp("USER_ACC", "UA_MOBILE", "UA_TYPE=3 and UA_NO=" + ddlName.SelectedValue);
        txtMobile.Text = txtMobile1;

        string email1 = objCommon.LookUp("USER_ACC", "UA_EMAIL", "UA_TYPE=3 and UA_NO=" + ddlName.SelectedValue);
        txtEmail.Text = email1;

        //txtAddress.Text = "Government College of Engineering Aurangabad, Railway Station Road Aurangabad Maharashtra India – 431005";
    }
    protected void chkOther_CheckedChanged(object sender, EventArgs e)
    {
        if (chkOther.Checked == true)
        {
            txtName.Visible = true;
            ddlName.Visible = false;
            txtAddress.Text = string.Empty;
        }
        else
        {
            txtName.Visible = false;
            ddlName.Visible = true;
            ddlName.SelectedIndex = 0;

        }
    }
    protected void btnStaffAdd_Click(object sender, EventArgs e)
    {
        ShowReport("Staff_Address", "StaffLocalAddress.rpt");       
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue);
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updSession, this.updSession.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_ReceiptTypeDefinition.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnAquitancerpt_Click(object sender, EventArgs e)
    {
        ShowReportAquitance("Aquitance_Report", "rptAquittanceReport.rpt");  
    }

    private void ShowReportAquitance(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue);
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updSession, this.updSession.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_ReceiptTypeDefinition.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }




    protected void ddlStaffType_SelectedIndexChanged(object sender, EventArgs e)
    {


        ddlSession.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        txtName.Text = string.Empty;
        txtAddress.Text = string.Empty;
        hdnEditedRecord.Value = "0";
        txtEmail.Text = string.Empty;
        txtMobile.Text = string.Empty;
        ddlName.SelectedIndex = 0;
        //rblStatus.SelectedIndex = -1;
        //ddlStaffType.SelectedIndex = 0;



        if (ddlStaffType.SelectedValue == "0")
        {
            ddlName.Visible = true;
            txtName.Visible = false;
        }
        else if (ddlStaffType.SelectedValue == "1" || ddlStaffType.SelectedValue == "3")
        {
            ddlName.Visible = true;
            txtName.Visible = false;
           // txtAddress.Text = "Government College of Engineering Aurangabad, Railway Station Road Aurangabad Maharashtra India – 431005";
            txtAddress.Text = objCommon.LookUp("REFF", "CollegeName", "");
          

        }
        else if (ddlStaffType.SelectedValue == "2")
        {
            {
                txtName.Visible = true;
                ddlName.Visible = false;
                txtMobile.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtAddress.Text = string.Empty;
            }
        }
    }
    
}