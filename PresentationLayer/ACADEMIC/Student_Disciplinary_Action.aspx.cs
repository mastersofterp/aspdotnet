using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_Student_Disciplinary_Action : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
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
                //Page Authorization
                this.CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "FLOCK=1 AND SESSIONNO>0", "SESSIONNO DESC");
                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "", "SESSIONNO DESC");
                objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");
                //BindListView();
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            }
        }
        divMsg.InnerHtml = string.Empty;
        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlSession.SelectedIndex == 0 || txtStudentRollNo.Text == "" || txtFromDate.Text == "" || txtToDate.Text == "" || txtRemark.Text == "")
            {
                string message = "";
                if (ddlSession.SelectedIndex == 0)
                {
                    message = "Please select Session \\n";
                }
                if (txtStudentRollNo.Text == "")
                {
                    message = message + "Please Enter Enrollment No \\n";
                }
                if (txtFromDate.Text == "")
                {
                    message = message + "Please select From Date \\n";
                }
                if (txtToDate.Text == "")
                {
                    message = message + "Please select To Date \\n";
                }
                if (txtRemark.Text == "")
                {
                    message = message + "Please Enter Remark \\n";
                }
                objCommon.DisplayMessage(this.updDiscipline, message, this.Page);
                return;
            }

            if (txtToDate.Text != "" || txtFromDate.Text != "")
            {
                if (Convert.ToDateTime(txtToDate.Text) < Convert.ToDateTime(txtFromDate.Text))
                {
                    objCommon.DisplayMessage(this.updDiscipline, "To Date should be greater than From Date", this.Page);
                    return;
                }
                else
                {
                    if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
                    {
                        int idno = Convert.ToInt32(ViewState["DISC_STUD_IDNO"]);
                        ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                        Disciplinary_ActionController objDiscpline = new Disciplinary_ActionController();
                        int result = objDiscpline.UpdateDisciplinaryAction(Convert.ToInt32(ViewState["DISCIPLINE_ID"]), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text), txtRemark.Text, Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(),Convert.ToInt32(Session["OrgId"]));
                        if (result == 1)
                        {
                            objCommon.DisplayMessage(updDiscipline, "Record Updated Successfully!", this.Page);
                            BindListView();
                            ClearFields();
                        }
                        else
                        {
                            objCommon.DisplayMessage(updDiscipline, "Something went wrong!", this.Page);
                        }
                    }
                    else
                    {
                        int idno = Convert.ToInt32(ViewState["DISC_STUD_IDNO"]);
                        ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                        Disciplinary_ActionController objDiscpline = new Disciplinary_ActionController();
                        int result = objDiscpline.InsertDisciplinaryAction(Convert.ToInt32(ddlSession.SelectedValue), idno, txtStudentRollNo.Text, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text), txtRemark.Text, Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(),Convert.ToInt32(Session["OrgId"]));
                        if (result == 1)
                        {
                            objCommon.DisplayMessage(updDiscipline, "Record Saved Successfully!", this.Page);
                            BindListView();
                            ClearFields();
                            btnSubmit.Enabled = false;
                        }
                        else
                        {
                            objCommon.DisplayMessage(updDiscipline, "Something went wrong!", this.Page);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearFields();
        pnlStudents.Visible = false;
        divStudentDetails.Visible = false;
        ddlSession.Items.Clear();
        ddlSession.Items.Add(new ListItem("Please Select", "0"));
        ddlCollege.SelectedIndex = 0;
    }

    public void ClearFields()
    {
        ddlSession.SelectedIndex = 0;
        txtStudentRollNo.Text = "";
        txtFromDate.Text = "";
        txtToDate.Text = "";
        txtRemark.Text = "";
        ViewState["action"] = null;
        btnSubmit.Text = "Submit";
        btnSubmit.Enabled = false;

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Student_Disciplinary_Action.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Student_Disciplinary_Action.aspx");
        }
    }

    public void BindListView()
    {
        DataSet ds = new DataSet();
        ds = objCommon.FillDropDown("ACD_DISCIPLINARY_ACTION", "DISCIPLINE_ID", "SESSIONNO, IDNO   ,REGNO   ,FROMDATE   ,TODATE   ,REMARK   ,UA_NO   ,CREATED_DATE ,IP_ADDRESS", "REGNO = '" + txtStudentRollNo.Text + "'", "DISCIPLINE_ID DESC");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            pnlStudents.Visible = true;
            divStudentDetails.Visible = false;
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label -
        }
        else
        {
            pnlStudents.Visible = false;
            pnlStudents.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int DISCIPLINE_ID = int.Parse(btnEdit.CommandArgument);
            ViewState["DISCIPLINE_ID"] = int.Parse(btnEdit.CommandArgument);
            ViewState["edit"] = "edit";
            btnSubmit.Text = "Update";
            btnSubmit.Enabled = true;
            this.ShowDetails(DISCIPLINE_ID);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowDetails(int DISCIPLINE_ID)
    {
        try
        {
            DataSet ds = new DataSet();
            ds = objCommon.FillDropDown("ACD_DISCIPLINARY_ACTION", "DISCIPLINE_ID", "SESSIONNO,IDNO   ,REGNO   ,FROMDATE   ,TODATE   ,REMARK   ,UA_NO   ,CREATED_DATE ,IP_ADDRESS", "DISCIPLINE_ID =" + DISCIPLINE_ID, "DISCIPLINE_ID DESC");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];

                ddlSession.SelectedValue = dt.Rows[0]["SESSIONNO"].ToString();
                txtStudentRollNo.Text = dt.Rows[0]["REGNO"].ToString();
                txtFromDate.Text = dt.Rows[0]["FROMDATE"].ToString();
                txtToDate.Text = dt.Rows[0]["TODATE"].ToString();
                txtRemark.Text = dt.Rows[0]["REMARK"].ToString();

                DataSet dsStudentDetails = objCommon.FillDropDown("ACD_STUDENT A LEFT JOIN ACD_SEMESTER B ON A.SEMESTERNO = B.SEMESTERNO LEFT JOIN ACD_DEGREE C ON A.DEGREENO = C.DEGREENO LEFT JOIN ACD_BRANCH D ON D.BRANCHNO = A.BRANCHNO", "IDNO", "STUDNAME,REGNO,SEMESTERNAME SEMESTER,CODE DEGREE,D.SHORTNAME BRANCH", "REGNO = '" + txtStudentRollNo.Text + "'", "");
                if (dsStudentDetails != null && dsStudentDetails.Tables[0].Rows.Count > 0)
                {
                    lblStudentName.Text = dsStudentDetails.Tables[0].Rows[0]["STUDNAME"].ToString();
                    lblEnrollmentNo.Text = dsStudentDetails.Tables[0].Rows[0]["REGNO"].ToString();
                    lblSemester.Text = dsStudentDetails.Tables[0].Rows[0]["SEMESTER"].ToString();
                    lblDegree.Text = dsStudentDetails.Tables[0].Rows[0]["DEGREE"].ToString();
                    lblBranch.Text = dsStudentDetails.Tables[0].Rows[0]["BRANCH"].ToString();
                    ViewState["DISC_STUD_IDNO"] = dsStudentDetails.Tables[0].Rows[0]["IDNO"].ToString();
                    divStudentDetails.Visible = true;

                }


            }
            else
            {
                divStudentDetails.Visible = false;
            }
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex == 0 && txtStudentRollNo.Text == "")
        {
            objCommon.DisplayMessage(updDiscipline, "Please Select Session. \\n Enter Enrollment No.", this.Page);
            return;
        }
        else if (ddlSession.SelectedIndex > 0 && String.IsNullOrEmpty(txtStudentRollNo.Text))
        {
            objCommon.DisplayMessage(updDiscipline, "Please Enter Enrollment No", this.Page);
            return;
        }
        else if (ddlSession.SelectedIndex == 0 && txtStudentRollNo.Text != "")
        {
            objCommon.DisplayMessage(updDiscipline, "Please Select Session", this.Page);
            return;
        }
        DataSet ds = objCommon.FillDropDown("ACD_DISCIPLINARY_ACTION", "DISCIPLINE_ID", "SESSIONNO,IDNO,REGNO,FROMDATE,TODATE,REMARK", "REGNO = '" + txtStudentRollNo.Text + "' AND SESSIONNO = " + ddlSession.SelectedValue, "");

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["DISC_STUD_IDNO"] = ds.Tables[0].Rows[0]["IDNO"].ToString();
            pnlStudents.Visible = true;
            divStudentDetails.Visible = false;
            btnSubmit.Enabled = false;
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label -

        }
        else
        {
            divStudentDetails.Visible = true;
            btnSubmit.Enabled = true;
            DataSet dsStudentDetails = objCommon.FillDropDown("ACD_STUDENT A LEFT JOIN ACD_SEMESTER B ON A.SEMESTERNO = B.SEMESTERNO LEFT JOIN ACD_DEGREE C ON A.DEGREENO = C.DEGREENO LEFT JOIN ACD_BRANCH D ON D.BRANCHNO = A.BRANCHNO", "IDNO", "STUDNAME,REGNO,SEMESTERNAME SEMESTER,CODE DEGREE,D.SHORTNAME BRANCH", "REGNO = '" + txtStudentRollNo.Text + "'", "");
            if (dsStudentDetails != null && dsStudentDetails.Tables[0].Rows.Count > 0)
            {
                lblStudentName.Text = dsStudentDetails.Tables[0].Rows[0]["STUDNAME"].ToString();
                lblEnrollmentNo.Text = dsStudentDetails.Tables[0].Rows[0]["REGNO"].ToString();
                lblSemester.Text = dsStudentDetails.Tables[0].Rows[0]["SEMESTER"].ToString();
                lblDegree.Text = dsStudentDetails.Tables[0].Rows[0]["DEGREE"].ToString();
                lblBranch.Text = dsStudentDetails.Tables[0].Rows[0]["BRANCH"].ToString();
                ViewState["DISC_STUD_IDNO"] = dsStudentDetails.Tables[0].Rows[0]["IDNO"].ToString();
            }
            pnlStudents.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex == 0)
        {
            objCommon.DisplayMessage(updDiscipline, "Please Select Session.", this.Page);
            return;
        }

        try
        {
            ShowReport("StudentHostelIDCard", "rptDisciplinaryAction.rpt");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue;

            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]) 
                + ",@P_SESSIONNO=" + ddlSession.SelectedValue;
            
            
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updDiscipline, this.updDiscipline.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex == 0)
        {
            objCommon.DisplayMessage(updDiscipline, "Please Select Session.", this.Page);
            return;
        }
        try
        {
            GridView GV = new GridView();
            string ContentType = string.Empty;

            DataSet ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_DISCIPLINARY_ACTION A ON S.IDNO = A.IDNO INNER JOIN ACD_SESSION_MASTER B ON A.SESSIONNO = B.SESSIONNO", "SESSION_NAME SESSION", "S.STUDNAME [STUDENT NAME],A.REGNO [ENROLLMENT NO],CONVERT(VARCHAR(10), A.FROMDATE, 103) AS [FROM DATE] ,CONVERT(VARCHAR(10), A.TODATE, 103) AS [TO DATE],A.REMARK", "A.SESSIONNO=" + ddlSession.SelectedValue, "");

            if (ds.Tables[0].Rows.Count > 0)
            {
                GV.DataSource = ds;
                GV.DataBind();
                string filename = ddlSession.SelectedItem.Text.Trim();
                filename = filename.Replace(" ", "");
                string attachment = "attachment; filename=disciplinaryaction-" + filename + ".xls";
                //string attachment = "attachment; filename=disciplinaryaction-EVEN201920.xls";
                //string attachment = "attachment; filename=AdmissionRegisterStudents.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GV.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayUserMessage(updDiscipline, "No Record Found.", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0)
        {
            if (ddlCollege.SelectedValue != "0")
            {
                ViewState["college_id"] = Convert.ToInt32(ddlCollege.SelectedValue).ToString();
            }
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER SM WITH (NOLOCK) INNER JOIN ACD_COLLEGE_MASTER CM ON (SM.COLLEGE_ID = CM.COLLEGE_ID)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND SM.COLLEGE_ID = " + Convert.ToInt32(ddlCollege.SelectedValue) + " AND SM.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER M INNER JOIN ACD_STUDENT_RESULT R ON(M.SESSIONNO=R.SESSIONNO)", "DISTINCT R.SESSIONNO", "SESSION_NAME", "M.SESSIONNO >0 AND IS_ACTIVE=1 AND COLLEGE_ID = " + Convert.ToInt32(ddlCollege.SelectedValue) + " AND M.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "R.SESSIONNO DESC");
            ddlSession.Focus();
        }
        else
        {
            ddlSession.Items.Clear();
            ddlSession.Items.Add(new ListItem("Please Select", "0"));
        }
    }
}