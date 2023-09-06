using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Linq;

public partial class ACADEMIC_AdmissionDetails : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ddlBatch.Attributes.Add("disabled", "disabled");
            ddlBranch.Attributes.Add("disabled", "disabled");
            ddlSemester.Attributes.Add("disabled", "disabled");
            ddlDegree.Attributes.Add("disabled", "disabled");
            ddlYear.Attributes.Add("disabled", "disabled");
            ddlAcademicYear.Attributes.Add("disabled", "disabled");
            ddlSchoolCollege.Attributes.Add("disabled", "disabled");
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                //  CheckPageAuthorization();
                ViewState["usertype"] = Session["usertype"];


                if (ViewState["usertype"].ToString() == "2")
                {
                    divadmissiondetailstreeview.Visible = false;
                    divAdmissionApprove.Visible = false;
                    AdmDetails.Visible = false;
                    divhome.Visible = false;
                    //divPrintReport.Visible = true;
                    int FinalSubmit = 0;
                    if (objCommon.LookUp("ACD_ADM_STUD_INFO_SUBMIT_LOG", "FINAL_SUBMIT", "IDNO=" + Convert.ToInt32(Session["idno"])) != String.Empty)
                    {
                        FinalSubmit = Convert.ToInt32(objCommon.LookUp("ACD_ADM_STUD_INFO_SUBMIT_LOG", "FINAL_SUBMIT", "IDNO=" + Convert.ToInt32(Session["idno"])));
                    }
                    if (FinalSubmit == 1)
                    { divPrintReport.Visible = true; }
                    else
                    { divPrintReport.Visible = false; }
                   // btnGohome.Visible = false;
                }
                else if (ViewState["usertype"].ToString() == "8") //HOD
                {
                    divadmissiondetailstreeview.Visible = false;
                    divAdmissionApprove.Visible = true;
                    AdmDetails.Visible = false;
                   
                    ddlPaymentType.Enabled = false;
                    ddlclaim.Enabled = false;
                    divhome.Visible = true;
                    btnSubmit.Visible = false;
                }
                else
                {
                    divadmissiondetailstreeview.Visible = true;
                    divAdmissionApprove.Visible = true;
                    AdmDetails.Visible = true;
                    //txtDateOfAdmission.Enabled = true;
                    ////ddlSchoolCollege.Enabled = true;
                    ////ddlDegree.Enabled = true;
                    //ddlBranch.Enabled = true;
                    //ddlBatch.Enabled = true;
                    //ddlStateOfEligibility.Enabled = true;
                    //ddlYear.Enabled = true;
                    //ddlSemester.Enabled = true;
                    ddlPaymentType.Enabled = true;
                   // ddlclaim.Enabled = true;
                    divhome.Visible = true;
                   // btnGohome.Visible = true;
                }
                
                this.FillDropDown();
                ShowStudentDetails();

            }

        }
    }


    private void DocumentRequaired(int payType)
    {
        StudentController objSC = new StudentController();
        //DataSet ds = objSC.GetStudentDocListConfirm(5389, 1, payType);
        //DataSet ds = objSC.GetStudentDocListConfirm(Convert.ToInt32(txtIDNo.Text), Convert.ToInt32(ddlDegree.SelectedValue),payType);
        DataSet ds = objSC.GetStudentDocListConfirm(Convert.ToInt32(Session["stuinfoidno"]), Convert.ToInt16(ddlDegree.SelectedValue), payType); //FOR BE & BE PTDP SAM DOC LIST
        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                chkDoc.DataTextField = "DOC_NAME";
                chkDoc.DataValueField = "DOCUMENTNO";
                chkDoc.DataSource = ds.Tables[0];
                chkDoc.DataBind();
                for (int i = 0; i < chkDoc.Items.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["STATUS"].ToString() == "1")
                    {
                        chkDoc.Items[i].Selected = true;
                    }
                }
            }
        }
        //lblDocsList.Visible = true;
        chkDoc.Visible = true;
    }

    private void ShowStudentDetails()
    {
        StudentController objSC = new StudentController();
        DataTableReader dtr = null;
        Student objS = new Student();
        if (ViewState["usertype"].ToString() == "2")
        {
            dtr = objSC.GetStudentDetails(Convert.ToInt32(Session["idno"]));
            divAdmissionDetails.Visible = false;


        }
        else
        {
            dtr = objSC.GetStudentDetails(Convert.ToInt32(Session["stuinfoidno"]));
            divAdmissionDetails.Visible = true;
            trAdmission.Visible = true;

        }
        if (dtr != null)
        {
            if (dtr.Read())
            {
                if (ViewState["usertype"].ToString() == "2")
                {
                    objS.IdNo = Convert.ToInt32(Session["idno"]);
                }
                else
                {
                    objS.IdNo = Convert.ToInt32(Session["stuinfoidno"]);
                }
                txtDateOfAdmission.Text = dtr["ADMDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dtr["ADMDATE"]).ToString("dd/MM/yyyy");

                this.objCommon.FillDropDownList(ddlSchoolCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "", "COLLEGE_NAME");
                ddlSchoolCollege.SelectedValue = dtr["COLLEGE_ID"] == null ? "0" : dtr["COLLEGE_ID"].ToString();

                this.objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID=" + ddlSchoolCollege.SelectedValue, "DEGREENAME");
                ddlDegree.SelectedValue = dtr["DEGREENO"] == null ? "0" : dtr["DEGREENO"].ToString();

                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", string.Empty, "LONGNAME");
                ddlBranch.SelectedValue = dtr["BRANCHNO"] == null ? "0" : dtr["BRANCHNO"].ToString();

                ddlBatch.SelectedValue = dtr["ADMBATCH"] == null ? "0" : dtr["ADMBATCH"].ToString();

                ddlYear.SelectedValue = dtr["YEAR"] == null ? "0" : dtr["YEAR"].ToString();
                ddlSemester.SelectedValue = dtr["SEMESTERNO"] == null ? "0" : dtr["SEMESTERNO"].ToString();

                ddlclaim.SelectedValue = dtr["CLAIMID"] == null ? "0" : dtr["CLAIMID"].ToString();

                this.objCommon.FillDropDownList(ddlAcademicYear, "ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "ACADEMIC_YEAR_ID>0 AND ISNULL(ACTIVE_STATUS,0) = 1 AND ISNULL(IS_CURRENT_FY,0)=1", "ACADEMIC_YEAR_NAME");
              
               ddlAcademicYear.SelectedValue = dtr["ACADEMIC_YEAR_ID"] == null ? "0" : dtr["ACADEMIC_YEAR_ID"].ToString();
               // ddlAcademicYear.SelectedValue = string.IsNullOrEmpty(dtr["ACADEMIC_YEAR_ID"].ToString()) ? "0" : dtr["ACADEMIC_YEAR_ID"].ToString();              

                ddlPaymentType.SelectedValue = dtr["ADMCATEGORYNO"] == null ? "0" : dtr["ADMCATEGORYNO"].ToString();

                if ((ddlDegree.SelectedValue) == "1")
                {
                    if (Convert.ToInt32(ddlPaymentType.SelectedValue) == 3)
                    {
                       // dasaPanel.Visible = true;
                    }
                }

                if ((ddlPaymentType.SelectedValue) == "1" || (ddlPaymentType.SelectedValue) == "2")
                {
                    this.DocumentRequaired(4);
                }
                else
                {
                    this.DocumentRequaired(3);
                }
            }
        }
       

    }

    private void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0 AND ISNULL(ACTIVESTATUS,0) = 1", "DEGREENAME");
            objCommon.FillDropDownList(ddlBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0 AND ISNULL(ACTIVESTATUS,0) = 1", "BATCHNAME DESC");
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0 AND ISNULL(ACTIVESTATUS,0) = 1", "SEMESTERNAME");
            objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0 AND ISNULL(ACTIVESTATUS,0) = 1", "YEAR");
            objCommon.FillDropDownList(ddlStateOfEligibility, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0 AND ISNULL(ACTIVESTATUS,0) = 1", "STATENAME");
            objCommon.FillDropDownList(ddlPaymentType, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO>0 AND ISNULL(ACTIVESTATUS,0) = 1", "CATEGORY");
            //objCommon.FillDropDownList(ddlAdmCaste, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO > 0 AND ISNULL(ACTIVESTATUS,0) = 1", "CATEGORYNO");
            objCommon.FillDropDownList(ddlclaim, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO>0 AND ISNULL(ACTIVESTATUS,0) = 1", "CATEGORY");
            objCommon.FillDropDownList(ddlAcademicYear, "ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "ACADEMIC_YEAR_ID>0 AND ISNULL(ACTIVE_STATUS,0) = 1 AND ISNULL(IS_CURRENT_FY,0)=1 ", "ACADEMIC_YEAR_NAME");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSchoolCollege.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID=" + ddlSchoolCollege.SelectedValue, "D.DEGREENAME");
            ddlDegree.Focus();
        }
        else
        {
            ddlSchoolCollege.SelectedIndex = 0;
        }

    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CD.BRANCHNO > 0 AND CD.COLLEGE_ID=" + Convert.ToInt32(ddlSchoolCollege.SelectedValue), "B.LONGNAME");
        ddlBranch.Focus();
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMFULLNAME", "SEMESTERNO>0 and yearno=" + ddlYear.SelectedValue, "SEMESTERNO");
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        StudentController objSC = new StudentController();
        Student objS = new Student();
        StudentPhoto objSPhoto = new StudentPhoto();
        StudentAddress objSAddress = new StudentAddress();
        StudentQualExm objSQualExam = new StudentQualExm();
        try
        {
            if (ViewState["usertype"].ToString() == "2" || ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || ViewState["usertype"].ToString() == "7" || ViewState["usertype"].ToString() == "5" || ViewState["usertype"].ToString() == "8")
            {
                if (ViewState["usertype"].ToString() == "2")
                {
                    objS.IdNo = Convert.ToInt32(Session["idno"]);
                }
                else
                {
                    objS.IdNo = Convert.ToInt32(Session["stuinfoidno"]);
                }
                if (!txtDateOfAdmission.Text.Trim().Equals(string.Empty)) objS.AdmDate = Convert.ToDateTime(txtDateOfAdmission.Text.Trim());
                objS.College_ID = Convert.ToInt32(ddlSchoolCollege.SelectedValue);
                objS.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
                objS.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
                objS.BatchNo = Convert.ToInt32(ddlBatch.SelectedValue);
                objS.Year = Convert.ToInt32(ddlYear.SelectedValue);
                objS.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
                objS.PType = Convert.ToInt32(ddlPaymentType.SelectedValue);
                objS.ClaimType = Convert.ToInt32(ddlclaim.SelectedValue);
                CustomStatus cs = (CustomStatus)objSC.UpdateStudentAdmissionDetails(objS,Convert.ToInt32(Session["usertype"]));
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                   // ShowStudentDetails();
                  //  objCommon.DisplayMessage(updAdmissionDetails, "Admission Details Updated Successfully!!", this.Page);

                   // divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('Admission Details Updated Successfully!!'); </script>";

                    //string strScript = "<SCRIPT language='javascript'>window.location='DASAStudentInformation.aspx';</SCRIPT>";
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "strScript", strScript);

                    Response.Redirect("~/academic/UploadDocument.aspx");
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "redirect script", "alert('Admission Details Updated Successfully!!'); location.href='UploadDocument.aspx';", true);
                }
                else
                {
                    objCommon.DisplayMessage(updAdmissionDetails, "Error Occured While Updating Admission Details!!", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage("You Are Not Authorised Person For This Form.Contact To Administrator.", this.Page);
            }

        }
        catch (Exception Ex)
        {
            throw;
        }

       


    }
    protected void btnGohome_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/StudentInfoEntryNew.aspx?pageno=2219");
    }

    protected void lnkPersonalDetail_Click(object sender, EventArgs e)
    {
        //Server.Transfer("~/academic/PersonalDetails.aspx", false);

        Response.Redirect("~/academic/PersonalDetails.aspx");

        // HttpContext.Current.RewritePath("PersonalDetails.aspx");
    }
    protected void lnkAddressDetail_Click(object sender, EventArgs e)
    {
        //Server.Transfer("~/academic/AddressDetails.aspx", false);

        Response.Redirect("~/academic/AddressDetails.aspx");
    }
    protected void lnkAdmissionDetail_Click(object sender, EventArgs e)
    {
        //Server.Transfer("~/academic/AdmissionDetails.aspx", false);
        Response.Redirect("~/academic/AdmissionDetails.aspx");

    }

    protected void lnkDasaStudentInfo_Click(object sender, EventArgs e)
    {

        //Server.Transfer("~/academic/DASAStudentInformation.aspx", false);
        Response.Redirect("~/academic/DASAStudentInformation.aspx");
    }
    protected void lnkUploadDocument_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/UploadDocument.aspx");
    }
    protected void lnkQualificationDetail_Click(object sender, EventArgs e)
    {

        //Server.Transfer("~/academic/QualificationDetails.aspx", false);
        Response.Redirect("~/academic/QualificationDetails.aspx");
    }
    protected void lnkotherinfo_Click(object sender, EventArgs e)
    {
        //Server.Transfer("~/academic/OtherInformation.aspx", false);
        Response.Redirect("~/academic/OtherInformation.aspx");
    }
    protected void lnkApproveAdm_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/ApproveAdmission.aspx");
    }
    protected void lnkprintapp_Click(object sender, EventArgs e)
    {
        GEC_Student objGecStud = new GEC_Student();
        if (ViewState["usertype"].ToString() == "2")
        {
            objGecStud.RegNo = Session["idno"].ToString();
            string output = objGecStud.RegNo;
            ShowReport("Admission_Form_Report_M.TECH", "Admission_Slip_Confirm_PHD_General.rpt", output);
        }
        else
        {
            if (Session["stuinfoidno"] != null)
            {
                objGecStud.RegNo = Session["stuinfoidno"].ToString();
                string output = objGecStud.RegNo;
                ShowReport("Admission_Form_Report_M.TECH", "Admission_Slip_Confirm_PHD_General.rpt", output);
            }
            else
            {
                objCommon.DisplayMessage(this.updAdmissionDetails, "Please Search Enrollment No!!", this.Page);
            }
        }
    }

    private void ShowReport(string reportTitle, string rptFileName, string regno)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            //url += "pagetitle=" + reportTitle;
            url += "pagetitle=Admission Form Report " + Session["stuinfoenrollno"].ToString();
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + regno + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_ADMBATCH=" + Convert.ToInt32(ddlBatch.SelectedValue) + ",@PTYPE=" + ((rbDDPayment.Checked) ? Convert.ToInt32("0") : Convert.ToInt32("1")) + ",@Year=" + ddlYear.SelectedValue; 
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(regno) + "";
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";


            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updAdmissionDetails, this.updAdmissionDetails.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void lnkGoHome_Click(object sender, EventArgs e)
    {
        if (ViewState["usertype"].ToString() == "1")
        {
            Session["stuinfoidno"] = null;
            Session["stuinfoenrollno"] = null;
            Session["stuinfofullname"] = null;
            Response.Redirect("~/academic/StudentInfoEntry.aspx?pageno=74");
        }
        else
        {
            Response.Redirect("~/academic/StudentInfoEntry.aspx?pageno=74");
        }
    }

    protected void lnkCovid_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/CovidVaccinationDetails.aspx");
    }
}