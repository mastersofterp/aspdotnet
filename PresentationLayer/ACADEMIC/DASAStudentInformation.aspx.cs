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

public partial class ACADEMIC_DASAStudentInformation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
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
                //  CheckPageAuthorization();
                ViewState["usertype"] = Session["usertype"];

                if (ViewState["usertype"].ToString() == "2")
                {
                    divadmissiondetails.Visible = false;
                   // btnGoHome.Visible = false;
                    divhome.Visible = false;
                }
                else
                {
                    divadmissiondetails.Visible = true;
                   // btnGoHome.Visible = true;
                    divhome.Visible = true;
                }
               this.ShowStudentDetails();

            }

        }
    }



    private void ShowStudentDetails()
    {
        Student objS = new Student();
        StudentController objSC = new StudentController();
        DataTableReader dtr = null;
        if (ViewState["usertype"].ToString() == "2")
        {
            dtr = objSC.GetStudentDetails(Convert.ToInt32(Session["idno"]));
        }
        else
        {
            dtr = objSC.GetStudentDetails(Convert.ToInt32(Session["stuinfoidno"]));

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


                txtVisaExpiry.Text = dtr["VISA_EXPIRY_DATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dtr["VISA_EXPIRY_DATE"]).ToString();
                txtPassportExpiry.Text = dtr["PASSPORT_EXPIRY_DATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dtr["PASSPORT_EXPIRY_DATE"]).ToString();
                txtPassportIssueDate.Text = dtr["PASSPORT_ISSUE_DATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dtr["PASSPORT_ISSUE_DATE"]).ToString();
                txtStayPermit.Text = dtr["STAY_PERMIT_VALIDUP"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dtr["STAY_PERMIT_VALIDUP"]).ToString();
                if (Convert.ToBoolean(dtr["INDIAN_ORIGIN"]) == true)
                {
                    rdobtn_Indian.SelectedValue = "Y";
                }
                else
                {
                    rdobtn_Indian.SelectedValue = "N";
                }
                txtScholarshipScheme.Text = dtr["SCHOLARSHIP_SCHEME"] == null ? string.Empty : dtr["SCHOLARSHIP_SCHEME"].ToString();
                txtAgency.Text = dtr["AGENCY"] == null ? string.Empty : dtr["AGENCY"].ToString();
                txtPassportPlace.Text = dtr["PLACE_ISSUE_PASSPORT"] == null ? string.Empty : dtr["PLACE_ISSUE_PASSPORT"].ToString();                         
                txtCitizenship.Text = dtr["CITIZENSHIP"] == null ? string.Empty : dtr["CITIZENSHIP"].ToString();

            }
        }
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
            if (ViewState["usertype"].ToString() == "2" || ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || ViewState["usertype"].ToString() == "7" || ViewState["usertype"].ToString() == "5")
            {
                if (ViewState["usertype"].ToString() == "2")
                {
                    objS.IdNo = Convert.ToInt32(Session["idno"]);
                }
                else
                {
                    objS.IdNo = Convert.ToInt32(Session["stuinfoidno"]);
                }
                if (!txtVisaExpiry.Text.Trim().Equals(string.Empty)) objS.VisaExpiryDate = Convert.ToDateTime(txtVisaExpiry.Text.Trim());
                if (!txtPassportExpiry.Text.Trim().Equals(string.Empty)) objS.PassportExpiryDate = Convert.ToDateTime(txtPassportExpiry.Text.Trim());
                if (!txtPassportIssueDate.Text.Trim().Equals(string.Empty)) objS.PassportIssueDate = Convert.ToDateTime(txtPassportIssueDate.Text.Trim());
                if (!txtStayPermit.Text.Trim().Equals(string.Empty)) objS.StayPermitDate = Convert.ToDateTime(txtStayPermit.Text.Trim());
                if (rdobtn_Indian.SelectedValue == "Y")
                    objS.IndianOrigin = true;
                else
                    objS.IndianOrigin = false;
                if (!txtScholarshipScheme.Text.Trim().Equals(string.Empty)) objS.ScholarshipScheme = txtScholarshipScheme.Text.Trim();
                if (!txtAgency.Text.Trim().Equals(string.Empty)) objS.Agency = txtAgency.Text.Trim();
                if (!txtPassportPlace.Text.Trim().Equals(string.Empty)) objS.PassportIssuePlace = txtPassportPlace.Text.Trim();
                if (!txtCitizenship.Text.Trim().Equals(string.Empty)) objS.Citizenship = txtCitizenship.Text.Trim();

                CustomStatus cs = (CustomStatus)objSC.UpdateStudentDASAInformation(objS, Convert.ToInt32(Session["usertype"]));
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    ShowStudentDetails();
                   // objCommon.DisplayMessage(upddasainformation, "DASA Student Information Updated Successfully!!", this.Page);

                    divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('DASA Student Information Updated Successfully!!'); </script>";

                    //string strScript = "<SCRIPT language='javascript'>window.location='QualificationDetails.aspx';</SCRIPT>";
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "strScript", strScript);

                   Response.Redirect("~/academic/QualificationDetails.aspx");
                }
                else
                {
                    objCommon.DisplayMessage(upddasainformation, "Error Occured While Updating DASA Student Information!!", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage("You Are Not Authorised Person For This Form.Contact To Administrator.", this.Page);
            }
        }
        catch (Exception Ex)
        { 
        
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
                objCommon.DisplayMessage(this.upddasainformation, "Please Search Enrollment No!!", this.Page);
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
            ScriptManager.RegisterClientScriptBlock(this.upddasainformation, this.upddasainformation.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
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
}