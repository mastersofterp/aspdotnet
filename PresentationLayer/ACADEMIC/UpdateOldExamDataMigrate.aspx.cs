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

public partial class ACADEMIC_UpdateOldExamDataMigrate : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();

    #region Page Event

    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                    this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        int UPDATE_OLDEXAM_DATA_MIGRATION = Convert.ToInt32(objCommon.LookUp("REFF", "isnull(Update_OldExam_Data_Migration,0)", ""));

                        if (Convert.ToInt32(Session["userno"]) == UPDATE_OLDEXAM_DATA_MIGRATION) //Check UPDATE OLD MOGRATE DATA RIGHTS FROM REFF.
                        {
                            this.PopulateDropDownList();
                         
                        }
                        else
                        {
                            CheckPageAuthorization();
                        }
                    }
                    else
                    {
                        CheckPageAuthorization();
                    }

                  

                    //txtToDate.Text = DateTime.Now.ToShortDateString();
                    //Focus on From Date
                    //txtFromDate.Focus();
                }
            }

            //Blank Div
            //divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_StudentHorizontalReport.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=UpdateOldExamDataMigrate.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=UpdateOldExamDataMigrate.aspx");
        }
    }

    #endregion

    protected void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO < 56 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_StudentHorizontalReport.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void txtAdmissionNo_TextChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0 && txtAdmissionNo.Text!=string.Empty)
        {
            objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT R INNER JOIN ACD_SEMESTER S ON(R.SEMESTERNO = S.SEMESTERNO)", "DISTINCT R.SEMESTERNO", "S.SEMESTERNAME", "R.SESSIONNO ='" + ddlSession.SelectedValue + "' AND R.REGNO = '" + txtAdmissionNo.Text + "'", "R.SEMESTERNO");
            txtAdmissionNo.Focus();
        }
        else
        {
            ddlSemester.Focus();
            ddlSession.SelectedIndex = 0;
            txtAdmissionNo.Text = string.Empty;
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        try
        {
            if (ddlSemester.SelectedIndex == 0 || ddlSession.SelectedIndex == 0 || txtAdmissionNo.Text.Trim() == string.Empty)
            {
                objCommon.DisplayMessage(updStudent, "Please Enter Registration No", this.Page);
            }
            else
            {
                int idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "ENROLLNO = '" + txtAdmissionNo.Text + "' OR REGNO='" + txtAdmissionNo.Text + "'"));
                ViewState["idno"] = idno;
                if (idno > 0)
                {
                    ds = objSC.GetStudentResultDetailsByRegNo(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), txtAdmissionNo.Text.Trim());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        pnlResult.Visible = true;
                       // btnRemove.Visible = true;
                        DataRow dr = ds.Tables[0].Rows[0];
                        this.PopulateStudentInfoSection(dr);

                    }
                }
                else
                {
                    pnlResult.Visible = false;
                }
            }
        }

        catch (Exception ex)
        {
        }
    }

    private void PopulateStudentInfoSection(DataRow dr)
    {
        try
        {
            txtRegCredits.Text = dr["REGD_CREDITS"].ToString();
            txtEarnCredits.Text = dr["EARN_CREDITS"].ToString();

            if (dr["RESULT"] == null || dr["RESULT"] == string.Empty || dr["RESULT"] == "")
            {

                ddlResult.SelectedIndex = 0;
            }
            else if (dr["RESULT"].ToString()=="PASS")
            {

                ddlResult.SelectedValue = "1";
            }
            else if (dr["RESULT"].ToString() == "FAIL")
            {
                ddlResult.SelectedValue = "2";
            }
            //ddlResult.SelectedItem.Text = dr["RESULT"].ToString();
            //lblDegree.ToolTip = dr["DEGREENO"].ToString();
            txtCommCredits.Text = dr["CUMMULATIVE_CREDITS"].ToString();
            txtOutOfmarks.Text = dr["OUTOFMARKS"].ToString();
            txtObtainMarks.Text = dr["TOTAL_OBTD_MARKS"].ToString();
            txtSGPA.Text = dr["SGPA"].ToString();
            txtYGPA.Text = dr["YGPA"].ToString();
            txtDGPA.Text = dr["DGPA"].ToString();
            txtCommEGP.Text = dr["CUMMULATIVE_EGP"].ToString();
            if (dr["PROMOTED"] == null || dr["PROMOTED"] == string.Empty || dr["PROMOTED"] == "")
            {

                ddlPromoStatus.SelectedIndex = 0;
            }
            else if (dr["PROMOTED"].ToString() == "PASS")
            {

                ddlPromoStatus.SelectedValue = "1";
            }
            else if (dr["PROMOTED"].ToString() == "PROMOTED")
            {
                ddlPromoStatus.SelectedValue = "2";
            }
            else if (dr["PROMOTED"].ToString() == "NOT PROMOTED")
            {
                ddlPromoStatus.SelectedValue = "3";
            }
            //ddlPromoStatus.SelectedItem.Text = dr["PROMOTED"].ToString();

            BindCourseList();
            btnSubmit.Enabled = true;
        }
        catch (Exception ex)
        {
        }
    }

    private void BindCourseList()
    {
        try
        {
            DataSet dslist = objSC.GetStudentCourseList(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), txtAdmissionNo.Text.Trim());
          
            if (dslist != null && dslist.Tables.Count > 0 && dslist.Tables[0].Rows.Count > 0)
            {
                DataTable dt = dslist.Tables[0];

                pvlCourse.Visible = true;
                divCourseList.Visible = true;
                //btnRemove.Visible = true;
                //SetInitialRow();
                //SetBindPreviousData();
                lvCourse.DataSource = dslist;
                lvCourse.DataBind();


            }
            else
            {

                pvlCourse.Visible = true;
                divCourseList.Visible = true;
                lvCourse.DataSource = null;
                lvCourse.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "User_Status_Report.DisplayAllCount() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
           
            string notSelected_studName = string.Empty;
            string notSelected_studName_remark = string.Empty;
            bool atleastOneCourse = false;
            decimal RegCredits = 0.0m;
            decimal EarnCredits=0.0m;
            decimal CommCredits=0.0m;
            decimal OutOfmarks = 0.0m;
            decimal ObtainMarks = 0.0m;
            decimal SGPA = 0.0m;
            decimal YGPA = 0.0m;
            decimal DGPA = 0.0m;
            decimal CommEGP = 0.0m;
           
            int IDNO = Convert.ToInt32(ViewState["idno"]);
            int SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
            int SEMESTERNO = Convert.ToInt32(ddlSemester.SelectedValue);
          
            if (txtRegCredits.Text.Trim().Equals(string.Empty) || txtRegCredits.Text.Trim()=="")
            {
                RegCredits = 0;//Convert.ToDecimal(DBNull.Value);
                
            }
            else
            {
                RegCredits = Convert.ToDecimal(txtRegCredits.Text.Trim());
            }

            if (txtEarnCredits.Text.Trim().Equals(string.Empty) || txtEarnCredits.Text.Trim() == "")
            {
                EarnCredits = 0;//Convert.ToDecimal(DBNull.Value);

            }
            else
            {
                EarnCredits = Convert.ToDecimal(txtEarnCredits.Text.Trim());
            }
            if (txtCommCredits.Text.Trim().Equals(string.Empty) || txtCommCredits.Text.Trim() == "")
            {
                CommCredits = 0;//Convert.ToDecimal(DBNull.Value);

            }
            else
            {
                CommCredits = Convert.ToDecimal(txtCommCredits.Text.Trim());
            }
            if (txtOutOfmarks.Text.Trim().Equals(string.Empty) || txtOutOfmarks.Text.Trim() == "")
            {
                OutOfmarks = 0;//Convert.ToDecimal(DBNull.Value);

            }
            else
            {
                OutOfmarks = Convert.ToDecimal(txtOutOfmarks.Text.Trim());
            }
            if (txtObtainMarks.Text.Trim().Equals(string.Empty) || txtObtainMarks.Text.Trim() == "")
            {
                ObtainMarks = 0;//Convert.ToDecimal(DBNull.Value);

            }
            else
            {
                ObtainMarks = Convert.ToDecimal(txtObtainMarks.Text.Trim());
            }
            if (txtSGPA.Text.Trim().Equals(string.Empty) || txtSGPA.Text.Trim() == "")
            {
                SGPA = 0;//Convert.ToDecimal(DBNull.Value);

            }
            else
            {
                SGPA = Convert.ToDecimal(txtSGPA.Text.Trim());
            }
            if (txtYGPA.Text.Trim().Equals(string.Empty) || txtYGPA.Text.Trim() == "")
            {
                YGPA = 0;//Convert.ToDecimal(DBNull.Value);

            }
            else
            {
                YGPA = Convert.ToDecimal(txtYGPA.Text.Trim());
            }
             if (txtDGPA.Text.Trim().Equals(string.Empty) || txtDGPA.Text.Trim() == "")
            {
                DGPA = 0;//Convert.ToDecimal(DBNull.Value);

            }
            else
            {
                DGPA = Convert.ToDecimal(txtDGPA.Text.Trim());
            }
             if (txtCommEGP.Text.Trim().Equals(string.Empty) || txtCommEGP.Text.Trim() == "")
             {
                 CommEGP = 0;//Convert.ToDecimal(DBNull.Value);

             }
             else
             {
                 CommEGP = Convert.ToDecimal(txtCommEGP.Text.Trim());
             }
            string Result = ddlResult.SelectedItem.Text;
            
            string PromoStatus = ddlPromoStatus.SelectedItem.Text;
            int Uano = Convert.ToInt32(Session["userno"]);
            string IPADDRESS = Request.ServerVariables["REMOTE_ADDR"];

            CustomStatus cs1 = (CustomStatus)objSC.UpdateMigrationResultDetails(IDNO, SESSIONNO, SEMESTERNO, RegCredits, EarnCredits, CommCredits, Result, OutOfmarks, ObtainMarks, SGPA, YGPA, DGPA, CommEGP, PromoStatus,Uano,IPADDRESS);

            if (cs1.Equals(CustomStatus.RecordSaved))
            {
                dt.Columns.AddRange(new DataColumn[7] 
                        { 
                            new DataColumn("IDNO", typeof(int)),
                            new DataColumn("SESSIONNO", typeof(int)),
                            new DataColumn("SEMESTERNO",typeof(int)),
                            new DataColumn("COURSENO",typeof(int)),
                            new DataColumn("GRADE",typeof(string)),
                            new DataColumn("GDPOINT",typeof(decimal)),
                            //new DataColumn("CA1",typeof(decimal)),
                            //new DataColumn("CA2",typeof(decimal)),
                            //new DataColumn("CA3",typeof(decimal)),
                            //new DataColumn("CA4",typeof(decimal)),
                            //new DataColumn("PCA1",typeof(decimal)),
                            //new DataColumn("PCA2",typeof(decimal)),
                            new DataColumn("ENDSEM",typeof(decimal)),
                        });

                foreach (ListViewDataItem item in lvCourse.Items)
                {
                    int StudentType = 0;
                    if ((item.FindControl("cbRow") as CheckBox).Checked)
                    {
                        string grade = (item.FindControl("txtGrade") as TextBox).Text.Trim();
                        string endsem = (item.FindControl("txtEndSem") as TextBox).Text.Trim();
                        string gdpoint = (item.FindControl("txtGradePt") as TextBox).Text.Trim();
                        if (grade == string.Empty || grade == "" || endsem == string.Empty || endsem == "" || gdpoint == string.Empty || gdpoint == "")
                        {
                            objCommon.DisplayMessage(updStudent, "Please Enter Grade, Grade Point And End Sem Marks.", this.Page);
                            return;
                        }
                        atleastOneCourse = true;
                        if ((item.FindControl("lblCourseName") as Label).Text.Trim() != string.Empty)
                        {
                            int COURSENO = Convert.ToInt32((item.FindControl("cbRow") as CheckBox).ToolTip);
                            string GRADE = (item.FindControl("txtGrade") as TextBox).Text.Trim();
                            decimal GDPOINT = Convert.ToDecimal((item.FindControl("txtGradePt") as TextBox).Text.Trim());
                            //decimal CA1 = Convert.ToDecimal((item.FindControl("txtCAI") as TextBox).Text.Trim());
                            //decimal CA2 = Convert.ToDecimal((item.FindControl("txtCAII") as TextBox).Text.Trim());
                            //decimal CA3 = Convert.ToDecimal((item.FindControl("txtCAIII") as TextBox).Text.Trim());
                            //decimal CA4 = Convert.ToDecimal((item.FindControl("txtCAVI") as TextBox).Text.Trim());
                            //decimal PCA1 = Convert.ToDecimal((item.FindControl("txtPCAI") as TextBox).Text.Trim());
                            //decimal PCA2 = Convert.ToDecimal((item.FindControl("txtPCAII") as TextBox).Text.Trim());
                            decimal ENDSEM = Convert.ToDecimal((item.FindControl("txtEndSem") as TextBox).Text.Trim());

                            dt.Rows.Add(IDNO, SESSIONNO, SEMESTERNO, COURSENO, GRADE, GDPOINT, ENDSEM);
                        }
                       
                    }
                }

                int count = 0;
                if (dt.Rows.Count > 0)
                {
                    btnSubmit.Enabled = false;
                    CustomStatus cs = new CustomStatus();
                    foreach (DataRow dr in dt.Rows)
                    {
                        //INSERT QUERY
                        cs = (CustomStatus)objSC.UpdateMigrationCourseDetails(Convert.ToInt32(dr["IDNO"]), Convert.ToInt32(dr["SESSIONNO"]), Convert.ToInt32(dr["SEMESTERNO"]),
                                                                            Convert.ToInt32(dr["COURSENO"]), dr["GRADE"].ToString(), Convert.ToDecimal(dr["GDPOINT"]),
                                                                            Convert.ToDecimal(dr["ENDSEM"]), Uano, IPADDRESS);

                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            count++;
                        }
                    }

                    //if (count == dt.Rows.Count)
                    //{
                    //    objCommon.DisplayMessage(updStudent, "Record Updated Successfully.", this.Page);
                    //    //show();
                    //    //clear();
                    //    return;
                    //}

                }
                objCommon.DisplayMessage(updStudent, "Record Updated Successfully.", this.Page);
                pnlResult.Visible = false;
                pvlCourse.Visible = false;
                Clear();
            }
            else
            {
                objCommon.DisplayMessage(updStudent, "Error Occured while Updating the Record.", this.Page);
                
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_StudentDuesNoDuesFine.btnSubmit_Click() -> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clear()
    {
        txtAdmissionNo.Text = string.Empty;
        ddlSemester.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
    }
}