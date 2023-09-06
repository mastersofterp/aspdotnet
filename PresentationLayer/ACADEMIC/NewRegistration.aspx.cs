using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_NewRegistration : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    StudentController objSC = new StudentController();

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
        //Page.ClientScript.RegisterClientScriptInclude("selective", ResolveUrl(@"..\includes\prototype.js"));
        //Page.ClientScript.RegisterClientScriptInclude("selective1", ResolveUrl(@"..\includes\scriptaculous.js"));
        //Page.ClientScript.RegisterClientScriptInclude("selective2", ResolveUrl(@"..\includes\modalbox.js"));

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
                CheckPageAuthorization();
               

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();


                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
                objCommon.FillDropDownList(ddlAdmibatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
                objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID>0", "COLLEGE_ID");
                objCommon.FillDropDownList(ddlNationality, "ACD_NATIONALITY", "NATIONALITYNO", "NATIONALITY", "NATIONALITYNO > 0", "NATIONALITYNO");
            }

            if (Request.QueryString["id"] != null)
            {
              
                ShowStudentDetails();
                //ShowSignDetails();
                ViewState["idno"] = Convert.ToInt32(Request.QueryString["id"].ToString());
            }
            

        }
        else
        {

            if (Page.Request.Params["__EVENTTARGET"] != null)
            {
                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnsearch"))
                {
                    ddlAdmibatch.Focus();
                    string[] arg = Page.Request.Params["__EVENTARGUMENT"].ToString().Split(',');
                    bindlist(arg[0], arg[1], arg[2]);
                }

                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btncancelmodal"))
                {
                    txtSearch.Text = string.Empty;
                    lvStudent.DataSource = null;

                    lvStudent.DataBind();
                    lblNoRecords.Text = string.Empty;
                    //ddlAdmibatch.SelectedValue = "0";
                }
            }
        }


        //Cancel();
    }


    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=studentinfoentry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=studentinfoentry.aspx");
        }
    }

    private void bindlist(string category, string searchtext, string val)
    {
        StudentController objSC = new StudentController();
        DataSet ds = objSC.RetrieveStudentDetailsForRegistration(searchtext, category, val);

        if (ds.Tables[0].Rows.Count > 0)
        {
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
            ddlAdmibatch.SelectedValue = val;
            txtSearch.Text = searchtext;
        }
        else
        {
            lblNoRecords.Text = "Total Records : 0";
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            ddlAdmibatch.SelectedValue = val;
            txtSearch.Text = searchtext;
        }
    }

    protected void lnkId_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        string url = string.Empty;
        if (Request.Url.ToString().IndexOf("&id=") > 0)
            url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
        else
            url = Request.Url.ToString();

        Response.Redirect(url + "&id=" + lnk.CommandArgument);
       
    }

    private void ShowStudentDetails()
    {
        try
        {
            //int admbatch = Convert.ToInt32(ddlAdmbatch.SelectedValue);
            //string IDNO = objCommon.LookUp("ACD_STUDENT", "IDNO", "STUDNAME='" + txtStudent.Text + "' AND ADMBATCH=" + ddlAdmbatch.SelectedValue);
            //int idno = Convert.ToInt32(IDNO);
            DataSet ds = null;
            ds = objSC.GetStudentDetailsForRegistration(Convert.ToInt32(Request.QueryString["id"].ToString()));
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlAdmbatch.SelectedValue = (ds.Tables[0].Rows[0]["ADMBATCH"].ToString());
                        txtStudent.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                        txtName.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                        txtFatherName.Text = ds.Tables[0].Rows[0]["FATHERNAME"].ToString();
                        txtMotherName.Text = ds.Tables[0].Rows[0]["MOTHERNAME"].ToString();
                        if (ds.Tables[0].Rows[0]["SEX"].ToString().Trim().Equals("M"))
                        {
                            rdoMale.Checked = true;
                            rdoFemale.Checked = false;

                        }
                        else
                        {
                            rdoFemale.Checked = true;
                            rdoMale.Checked = false;
                        }
                        txtDateOfBirth.Text = ds.Tables[0].Rows[0]["DOB"].ToString() == string.Empty ? string.Empty : ds.Tables[0].Rows[0]["DOB"].ToString();
                        txtadmitted.Text = ds.Tables[0].Rows[0]["YEAR"].ToString();
                        ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                        //txtBranch.Text = ds.Tables[0].Rows[0]["BRANCHNAME"].ToString();
                         txtRollNo.Text = ds.Tables[0].Rows[0]["SSC_ROLLNO"].ToString();
                        //txtXYear.Text = ds.Tables[0].Rows[0]["SSC_YEAR"].ToString();
                        txtBoard.Text = ds.Tables[0].Rows[0]["BOARD"].ToString();
                        txtlastSchool.Text = ds.Tables[0].Rows[0]["SCHOOL_COLLEGE_NAME"].ToString();
                        txtLastRoll.Text = ds.Tables[0].Rows[0]["QEXMROLLNO"].ToString();
                        txtlastExmYear.Text = ds.Tables[0].Rows[0]["YEAR_OF_EXAMHSSC"].ToString();
                        txtMobile.Text = ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                        txtEmail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                        txtDomecial.Text = ds.Tables[0].Rows[0]["STATEOFDOMECIAL"].ToString();
                        ddlNationality.SelectedValue = (ds.Tables[0].Rows[0]["NATIONALITYNO"].ToString());
                        txtRegistrationNo.Text = ds.Tables[0].Rows[0]["ENROLLNO"].ToString();
                        rdoMigration.SelectedValue = ds.Tables[0].Rows[0]["MIGRATIONSTATUS"].ToString();
                        rdoRegStatus.SelectedValue = ds.Tables[0].Rows[0]["REGSTATUS"].ToString();
                        txtCardSerialNo.Text = ds.Tables[0].Rows[0]["CARDNO"].ToString();
                        //if (ds.Tables[0].Rows[0]["MIGRATIONSTATUS"].ToString().Trim().Equals(1))
                        //{
                        //    rdoMigration.SelectedValue = "1";
                        //}
                        //else
                        //{
                        //    rdoMigration.SelectedValue = "2";
                        //}
                        txtadmitted.Enabled = false;
                        ddlCollege.Enabled = false;
                        txtRegistrationNo.Enabled = false;
                        txtCardSerialNo.Enabled = false;
                        ddlAdmbatch.Enabled = false;
                       

                    }
                }
            }
            else
            {
                objCommon.DisplayMessage("Student not Available for this Selection", this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_NewRegistration.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    //protected void btnShow_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int admbatch = Convert.ToInt32(ddlAdmbatch.SelectedValue);
    //        string IDNO = objCommon.LookUp("ACD_STUDENT", "IDNO", "STUDNAME='" + txtStudent.Text + "' AND ADMBATCH=" + ddlAdmbatch.SelectedValue);
    //        int idno = Convert.ToInt32(IDNO);
    //        DataSet ds = null;
    //        ds = objSC.GetStudentDetailsForRegistration(idno, admbatch);
    //        if (ds != null)
    //        {
    //            if (ds.Tables.Count > 0)
    //            {
    //                if (ds.Tables[0].Rows.Count > 0)
    //                {
    //                    txtName.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
    //                    txtFatherName.Text = ds.Tables[0].Rows[0]["FATHERNAME"].ToString();
    //                    txtMotherName.Text = ds.Tables[0].Rows[0]["MOTHERNAME"].ToString();
    //                    if (ds.Tables[0].Rows[0]["SEX"].ToString().Trim().Equals("M"))
    //                    {
    //                        rdoMale.Checked = true;
    //                        rdoFemale.Checked = false;

    //                    }
    //                    else
    //                    {
    //                        rdoFemale.Checked = true;
    //                        rdoMale.Checked = false;
    //                    }
    //                    txtDateOfBirth.Text = ds.Tables[0].Rows[0]["DOB"].ToString() == string.Empty ? string.Empty : ds.Tables[0].Rows[0]["DOB"].ToString();
    //                    txtCollege.Text = ds.Tables[0].Rows[0]["COLLEGENAME"].ToString();
    //                    txtDepartment.Text = ds.Tables[0].Rows[0]["DEPTNAME"].ToString();
    //                    txtBoard.Text = ds.Tables[0].Rows[0]["BOARD"].ToString();
    //                    txtlastSchool.Text = ds.Tables[0].Rows[0]["SCHOOL_COLLEGE_NAME"].ToString();
    //                    txtLastRoll.Text = ds.Tables[0].Rows[0]["QEXMROLLNO"].ToString();
    //                    txtlastExmYear.Text = ds.Tables[0].Rows[0]["YEAR_OF_EXAMHSSC"].ToString();
    //                    txtMobile.Text = ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
    //                    txtEmail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
    //                    //txtDomecial.Text = ds.Tables[0].Rows[0]["COUNTRYDOMICILE"].ToString();
    //                    ddlNationality.SelectedValue = (ds.Tables[0].Rows[0]["NATIONALITYNO"].ToString());
    //                    txtRegistrationNo.Text = ds.Tables[0].Rows[0]["ENROLLNO"].ToString();
    //                }
    //            }
    //        }
    //        else
    //        {
    //            objCommon.DisplayMessage("Student not Available for this Selection", this.Page);
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ACADEMIC_NewRegistration.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }

    //}
    protected void btnGenerateRegNo_Click(object sender, EventArgs e)
    {
        Student objS = new Student();
        StudentQualExm objSQualExam = new StudentQualExm();
        CertificateMaster objCert = new CertificateMaster();
        
  
        try
        {
            string rollNo = string.Empty;
            rollNo = objCommon.LookUp("ACD_STU_LAST_QUALEXM", "SSC_ROLLNO", "SSC_ROLLNO='" + txtRollNo.Text + "'");
            if (!string.IsNullOrEmpty(rollNo))
            {
                objCommon.DisplayMessage(updSelection, "Entered X Roll No. is already registered for this University.Can Not apply", this.Page);
                return;
            }
            DateTime dtmin = new DateTime(1753, 1, 1);
            DateTime dtmax = new DateTime(9999, 12, 31);
            if (!string.IsNullOrEmpty(txtDateOfBirth.Text.Trim()))
            {
                if ((DateTime.Compare(Convert.ToDateTime(txtDateOfBirth.Text.Trim()), dtmin) <= 0) ||
                    (DateTime.Compare(Convert.ToDateTime(txtDateOfBirth.Text.Trim()), dtmax) >= 0))
                {
                    //1/1/1753 12:00:00 AM and 12/31/9999
                    objCommon.DisplayMessage("Please Enter Valid Date!!", this.Page);
                    return;
                }
            }

            //if (!txtName.Text.Trim().Equals(string.Empty)) objS.StudName = txtName.Text.Trim();
            //if (!txtFatherName.Text.Trim().Equals(string.Empty)) objS.FatherName = txtFatherName.Text.Trim();


            //string IDNO = objCommon.LookUp("ACD_STUDENT", "IDNO", "STUDNAME='" + txtName.Text + "' AND ADMBATCH=" + ddlAdmbatch.SelectedValue);
            //int idno = Convert.ToInt32(IDNO);
           
            objS.IdNo = Convert.ToInt32(ViewState["idno"]);
             objS.AdmBatch = Convert.ToInt32(ddlAdmbatch.SelectedValue);
            objS.StudName = txtName.Text.Trim().Equals(string.Empty) ? string.Empty : txtName.Text.Trim();
            objS.FatherName = txtFatherName.Text.Trim().Equals(string.Empty) ? string.Empty : txtFatherName.Text.Trim();
            objS.MotherName = txtMotherName.Text.Trim().Equals(string.Empty) ? string.Empty : txtMotherName.Text.Trim();

            if (rdoMale.Checked)
                objS.Sex = 'M';
            else
                objS.Sex = 'F';

            //if (!txtDateOfBirth.Text.Trim().Equals(string.Empty)) objS.Dob =Convert.ToDateTime(txtDateOfBirth.Text.Trim().ToString("yyyy/MM/dd"));
            if (!txtDateOfBirth.Text.Trim().Equals(string.Empty)) objS.Dob = Convert.ToDateTime(txtDateOfBirth.Text.Trim());

            //objS.Class_admited = txtadmitted.Text.Trim().Equals(string.Empty) ? string.Empty : txtadmitted.Text.Trim();

            objS.Collegeid = Convert.ToInt32(ddlCollege.SelectedValue); ;

            // string branch = objCommon.LookUp("ACD_BRANCH","BRANCHNO","LONGNAME='"+ txtBranch.Text +"'");
            //int branchno = Convert.ToInt32(branch);

            //objS.BranchNo = branchno;

            objSQualExam.SSCRollNo = txtRollNo.Text.Trim().Equals(string.Empty) ? string.Empty : txtRollNo.Text.Trim();

            //objSQualExam.SSCYear = txtXYear.Text.Trim().Equals(string.Empty) ? string.Empty : txtXYear.Text.Trim();   
   
            objSQualExam.BOARD = txtBoard.Text.Trim().Equals(string.Empty) ? string.Empty : txtBoard.Text.Trim();
            objSQualExam.SCHOOL_COLLEGE_NAME = txtlastSchool.Text.Trim().Equals(string.Empty) ? string.Empty : txtlastSchool.Text.Trim();
            objSQualExam.QEXMROLLNO = txtLastRoll.Text.Trim().Equals(string.Empty) ? string.Empty : txtLastRoll.Text.Trim();
            objSQualExam.YEAR_OF_EXAMHSSC = txtlastExmYear.Text.Trim().Equals(string.Empty) ? string.Empty : txtlastExmYear.Text.Trim();
            objS.StudentMobile = txtMobile.Text.Trim().Equals(string.Empty) ? string.Empty : txtMobile.Text.Trim();
            objS.EmailID = txtEmail.Text.Trim().Equals(string.Empty) ? string.Empty : txtEmail.Text.Trim();
            objS.Stateof_domecial = txtDomecial.Text.Trim().Equals(string.Empty) ? string.Empty : txtDomecial.Text.Trim();
            objS.NationalityNo = Convert.ToInt32(ddlNationality.SelectedValue);
            if (rdoMigration.SelectedValue == "1")
                objCert.MigrationStatus = 1;
            else
                objCert.MigrationStatus = 2;

            objS.EnrollNo = txtRegistrationNo.Text.Trim().Equals(string.Empty) ? string.Empty : txtRegistrationNo.Text.Trim();

            if (rdoRegStatus.SelectedValue == "1")
                objCert.RegStatus = 1;
            else
                objCert.RegStatus = 2;
            objCert.CardNo = txtCardSerialNo.Text.Trim().Equals(string.Empty) ? string.Empty : txtCardSerialNo.Text.Trim();



            string output = objSC.UpdateNewRegistrationStudentDetails(objS, objSQualExam,objCert);
            if (output != "-99")
            {
                objCommon.DisplayMessage(updSelection,"Student Registration No. Generated and Saved Succesfully.", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(updSelection,"Server Error...", this.Page);
            }
            ShowStudentDetails();
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.ToString();
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_NewRegistration.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
     
        string url = string.Empty;

        if (Request.Url.ToString().IndexOf("&id=") > 0)
            url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
        Response.Redirect(url);
    
    }

    //private void Cancel()
    //{
    //    txtName.Text = string.Empty;
    //    txtFatherName.Text = string.Empty;
    //    txtMotherName.Text = string.Empty;
    //    txtDateOfBirth.Text = string.Empty;
    //    txtadmitted.Text = string.Empty;
    //    txtCollege.Text = string.Empty;
    //    txtBranch.Text = string.Empty;
    //    txtRollNo.Text = string.Empty;
    //    txtlastExmYear.Text = string.Empty;
    //    txtLastRoll.Text = string.Empty;
    //    txtMobile.Text = string.Empty;
    //    txtEmail.Text = string.Empty;
    //    txtlastSchool.Text = string.Empty;
    //    txtRegistrationNo.Text = string.Empty;
    //    txtXYear.Text = string.Empty;
    //    txtDomecial.Text = string.Empty;
    //    txtCardSerialNo.Text = string.Empty;
    //    txtBoard.Text = string.Empty;
    //    ddlNationality.SelectedValue = "0";
        
    //}
   
    protected void btnPrintCard_Click(object sender, EventArgs e)
    {
        ShowPrintCard("Registration_Report", "rptRegistrationCardReport.rpt");
    }

    private void ShowPrintCard(string reportTitle, string rptFileName)
    {
       
        //string IDNO = objCommon.LookUp("ACD_STUDENT", "IDNO", "STUDNAME='" + txtName.Text + "' AND ADMBATCH=" + ddlAdmbatch.SelectedValue);
        //int idno = Convert.ToInt32(IDNO);
        
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + ViewState["idno"] +"";
            //,@P_TYPE=" + Convert.ToInt32(rblReport.SelectedValue);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updSelection, this.updSelection.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
   
}