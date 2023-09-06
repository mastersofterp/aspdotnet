//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : APPLY FOR BONAFIDE CERTIFICATE
// CREATION DATE : 06-DECEMBER-2021                                                    
// CREATED BY    : PRANITA A. HIRADKAR                                                        
// MODIFIED DATE : 
// MODIFIED DESC : 
//=======================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_Stud_Apply_bonafide_certificate : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
    #region pageload
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
                CheckPageAuthorization();
                string IPADDRESS = string.Empty;
                IPADDRESS = Request.ServerVariables["REMOTE_ADDR"];
                ViewState["ipAddress"] = IPADDRESS;
                //CHECK THE STUDENT LOGIN
                if (Convert.ToInt32(Session["usertype"]) == 2)
                {
                    pnlAdmin.Visible = false;
                    divStudentInfo.Visible = true;
                    divregno.Visible = false;
                    divbutton.Visible = true;
                    ShowDetails();
                    SHOW();
                    objCommon.FillDropDownList(ddlCertificate, "ACD_CERTIFICATE_MASTER", "CERT_NO", "CERT_NAME", "CERT_NO >0", "CERT_NO ");
                    btnApply.Enabled = true;
                    // btnReport.Enabled = true;
                }
                else if (Convert.ToInt32(Session["usertype"]) == 1)
                {
                    // divbutton.Visible = false;
                    pnlAdmin.Visible = true;
                    Panel2.Visible = true;
                    btnApply.Enabled = true;
                }
                else
                {
                    objCommon.DisplayMessage(updStudBonafide, "you are not authorized to view this page.!!", this.Page);
                    return;
                }
            }
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Stud_Apply_bonafide_certificate.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Stud_Apply_bonafide_certificate.aspx");
        }
    }

    #endregion

    #region show
    private void ShowData()
    {
        int idno = 0;
        string regno = txtRegNo.Text;
        try
        {
            DataTableReader dtr = objSC.GetStudentInfo_bonafide(idno, regno);
            if (dtr != null)
            {
                if (dtr.Read())
                {
                    lblAdmBatch.Text = dtr["BATCHNAME"] == null ? string.Empty : dtr["BATCHNAME"].ToString();
                    lblBranch.Text = dtr["LONGNAME"] == null ? string.Empty : dtr["LONGNAME"].ToString();
                    lblbranchno.Text = dtr["BRANCHNO"] == null ? string.Empty : dtr["BRANCHNO"].ToString();
                    ViewState["BRANCHNO"] = lblbranchno.Text;

                    lblStudename.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                    lblDOB.Text = dtr["DOB"] == DBNull.Value ? "" : Convert.ToDateTime(dtr["DOB"]).ToString("dd/MM/yyyy");
                    lblDegree.Text = dtr["DEGREENAME"] == null ? string.Empty : dtr["DEGREENAME"].ToString();
                    lbldegreeno.Text = dtr["DEGREENO"] == null ? string.Empty : dtr["DEGREENO"].ToString();
                    ViewState["DEGREENO"] = lbldegreeno.Text;

                    lblMobNo.Text = dtr["STUDENTMOBILE"] == null ? string.Empty : dtr["STUDENTMOBILE"].ToString();
                    lblRegNo.Text = dtr["REGNO"] == null ? string.Empty : dtr["REGNO"].ToString();
                    lblSemester.Text = dtr["SEMESTERNAME"] == null ? string.Empty : dtr["SEMESTERNAME"].ToString();
                    lblsemesterno.Text = dtr["SEMESTERNO"] == null ? string.Empty : dtr["SEMESTERNO"].ToString();
                    ViewState["SEMESTERNO"] = lblsemesterno.Text;
                    cert_no.Text = dtr["CERT_NO"] == null ? string.Empty : dtr["CERT_NO"].ToString();
                    ViewState["CERT_NO"] = cert_no.Text;

                    lblSchool.Text = dtr["COLLEGE_NAME"] == null ? string.Empty : dtr["COLLEGE_NAME"].ToString();
                    lblEmailID.Text = dtr["EMAILID"] == null ? string.Empty : dtr["EMAILID"].ToString();
                    lblbatch.Text = dtr["BATCHNO"] == null ? string.Empty : dtr["BATCHNO"].ToString();
                    ViewState["ADMBATCH"] = lblbatch.Text;
                    lblcollege_id.Text = dtr["COLLEGE_ID"] == null ? string.Empty : dtr["COLLEGE_ID"].ToString();
                    ViewState["COLLEGE_ID"] = lblcollege_id.Text;
                }
            } divStudentInfo.Visible = true;
            divbutton.Visible = true;
            pnlAdmin.Visible = false;
            Panel2.Visible = true;
            SHOW();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stud_Apply_bonafide_certificate.ShowDetails()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void SHOW()
    {
        try
        {
            int idno = 0;
            if (Convert.ToInt32(Session["usertype"]) == 2)
            {
                idno = Convert.ToInt32(Session["idno"]);
            }
            else if (Convert.ToInt32(Session["usertype"]) == 1)
            {
                string regno = txtRegNo.Text;
                idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO=" + regno));
            }

            string SP_Name2 = "PKG_GET_STUDENT_CERTIFICATE";
            string SP_Parameters2 = "@P_IDNO";
            string Call_Values2 = "" + idno + "";
            DataSet dsStudList = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            if (dsStudList.Tables[0].Rows.Count > 0)
            {
                lvIssueCert.DataSource = dsStudList;
                lvIssueCert.DataBind();
            }
            else
            {
                // objCommon.DisplayMessage(this.updStudBonafide, "No Record Found", this.Page);
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void ShowDetails()
    {
        int idno = Convert.ToInt32(Session["idno"]);
        string regno = "0";
        try
        {
            DataTableReader dtr = objSC.GetStudentInfo_bonafide(idno, regno);
            if (dtr != null)
            {
                if (dtr.Read())
                {
                    lblAdmBatch.Text = dtr["BATCHNAME"] == null ? string.Empty : dtr["BATCHNAME"].ToString();
                    lblBranch.Text = dtr["LONGNAME"] == null ? string.Empty : dtr["LONGNAME"].ToString();
                    lblbranchno.Text = dtr["BRANCHNO"] == null ? string.Empty : dtr["BRANCHNO"].ToString();
                    ViewState["BRANCHNO"] = lblbranchno.Text;

                    lblStudename.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                    lblDOB.Text = dtr["DOB"] == DBNull.Value ? "" : Convert.ToDateTime(dtr["DOB"]).ToString("dd/MM/yyyy");
                    lblDegree.Text = dtr["DEGREENAME"] == null ? string.Empty : dtr["DEGREENAME"].ToString();
                    lbldegreeno.Text = dtr["DEGREENO"] == null ? string.Empty : dtr["DEGREENO"].ToString();
                    ViewState["DEGREENO"] = lbldegreeno.Text;

                    lblMobNo.Text = dtr["STUDENTMOBILE"] == null ? string.Empty : dtr["STUDENTMOBILE"].ToString();
                    lblRegNo.Text = dtr["REGNO"] == null ? string.Empty : dtr["REGNO"].ToString();
                    lblSemester.Text = dtr["SEMESTERNAME"] == null ? string.Empty : dtr["SEMESTERNAME"].ToString();
                    lblsemesterno.Text = dtr["SEMESTERNO"] == null ? string.Empty : dtr["SEMESTERNO"].ToString();
                    ViewState["SEMESTERNO"] = lblsemesterno.Text;
                    cert_no.Text = dtr["CERT_NO"] == null ? string.Empty : dtr["CERT_NO"].ToString();
                    ViewState["CERT_NO"] = cert_no.Text;

                    lblSchool.Text = dtr["COLLEGE_NAME"] == null ? string.Empty : dtr["COLLEGE_NAME"].ToString();
                    lblEmailID.Text = dtr["EMAILID"] == null ? string.Empty : dtr["EMAILID"].ToString();
                    lblbatch.Text = dtr["BATCHNO"] == null ? string.Empty : dtr["BATCHNO"].ToString();
                    ViewState["ADMBATCH"] = lblbatch.Text;
                    lblcollege_id.Text = dtr["COLLEGE_ID"] == null ? string.Empty : dtr["COLLEGE_ID"].ToString();
                    ViewState["COLLEGE_ID"] = lblcollege_id.Text;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stud_Apply_bonafide_certificate.ShowDetails()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (txtRegNo.Text == string.Empty)
        {
            objCommon.DisplayMessage(updStudBonafide, "Please Enter Registration No...!!", this.Page);
            return;
        }
        string idno = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "IDNO", "REGNO = '" + txtRegNo.Text.Trim() + "'");
        if (idno == "")
        {
            objCommon.DisplayMessage(updStudBonafide, "Student Not Found for This Registration No...!!", this.Page);
            return;
        }
        ShowData();
        objCommon.FillDropDownList(ddlCertificate, "ACD_CERTIFICATE_MASTER", "CERT_NO", "CERT_NAME", "CERT_NO >0", "CERT_NO ");
    }

    #endregion

    #region apply

    protected void btnApply_Click(object sender, EventArgs e)
    {
        try
        {
            int ApproveStatus = 0;
            int idno = 0;
            if (Convert.ToInt32(Session["usertype"]) == 2)
            {
                idno = Convert.ToInt32(Session["idno"]);
            }
            else if (Convert.ToInt32(Session["usertype"]) == 1)
            {
                string regno = txtRegNo.Text;
                idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO=" + regno));
            }
            // int id = Convert.ToInt32(Session["idno"]);
            int batch = Convert.ToInt32(ViewState["ADMBATCH"]);
            int certno = Convert.ToInt32(ddlCertificate.SelectedValue);
            string applyipaddress = ViewState["ipAddress"].ToString();
            int applyby = Convert.ToInt32(Session["userno"]);
            string COUNT = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(*)", "IDNO=" + idno);
            if (COUNT == "0")
            {
                objCommon.DisplayMessage(this.updStudBonafide, "Course Registration not yet done for any semester.So, you are not authorized to apply any certificate", this.Page);
                ShowDetails();
                SHOW();
            }
            else
            {

                CustomStatus cs = (CustomStatus)objSC.Insert_ApproveBonafideCerti(idno, batch, certno, applyby, applyipaddress, ApproveStatus);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.updStudBonafide, "Applied Successfully for Certificate", this.Page);
                    ShowDetails();
                    SHOW();
                }
                else if (cs.Equals(CustomStatus.RecordExist))
                {
                    objCommon.DisplayMessage(this.updStudBonafide, "Alerady Applied for Certificate", this.Page);
                    ShowDetails();
                    SHOW();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Stud_Apply_bonafide_certificate.btnApply_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable");
        }
    }

    #endregion

    #region report

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("Bonafide_Certificate_Report", "bonafide_certificate_student.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Session["idno"].ToString() + ",@P_ADMBATCHNO=" + Convert.ToInt32(ViewState["ADMBATCH"]) + ",@P_COLLEGEID=" + Convert.ToInt32(ViewState["COLLEGE_ID"]) + ",@P_DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"]) + ",@P_BRANCHNO=" + Convert.ToInt32(ViewState["BRANCHNO"]) + ",@P_SEMESTERNO=" + Convert.ToInt32(ViewState["SEMESTERNO"]) + ",@P_CERT_NO=" + Convert.ToInt32(ViewState["CERT_NO"]);

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MarksEntryNotDone.aspx.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #endregion

    #region printcertificate
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtRegNo.Text = string.Empty;
        divStudentInfo.Visible = false;
    }

    protected void btnPrint_Click(object sender, ImageClickEventArgs e)
    {

        string CERT_SHORT_NAME = ((System.Web.UI.WebControls.ImageButton)(sender)).CommandArgument.ToString();
        int certno = Convert.ToInt32(objCommon.LookUp("ACD_CERTIFICATE_MASTER", "CERT_NO", "CERT_SHORT_NAME='" + CERT_SHORT_NAME + "'"));
        string idno = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();

        if (CERT_SHORT_NAME == "TC")
        {
            if (Session["OrgId"].ToString() == "2")
            {
                string tc = objCommon.LookUp("ACD_CERT_TRAN", "TC_CERTIFICATE", "CERT_NO=" + certno + "and idno=" + Convert.ToInt32(idno) + "");
                if (tc == "1" || tc == "2")
                {
                    int shift = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "distinct SHIFT", "IDNO IN ( SELECT * FROM fn_Split(REPLACE('" + idno + "', '$',','),','))"));
                    if (shift == 1)
                    {

                        ShowReport_TC_CERT(idno, CERT_SHORT_NAME, "Leaving_Certificate", "rptFullTimeTC_Crescent.rpt");
                    }
                    else if (shift == 2)
                    {
                        ShowReport_TC_CERT(idno, CERT_SHORT_NAME, "Leaving_Certificate", "rptPartTimeTC.rpt");
                    }
                }
                else
                {
                    ShowReport_TC_CERT(idno, CERT_SHORT_NAME, "Leaving_Certificate", "rptDiscontinue_TC_Crescent.rpt");
                }
            }
            else if (Session["OrgId"].ToString() == "6")
            {

                ShowReport_TC_CERT(idno, CERT_SHORT_NAME, "Leaving_Certificate", "RCPiperReport.rpt");

            }
            else if (Session["OrgId"].ToString() == "1")
            {
                ShowReport_TC_CERT(idno, CERT_SHORT_NAME, "Transfer_Certificate", "CrystalReport_LC_RCPIT.rpt");
            }
        }
        else if (CERT_SHORT_NAME == "BC")
        {
            if (Session["OrgId"].ToString() == "1")
            {
                ShowReport_BC(idno, CERT_SHORT_NAME, "Bonafide_Certificate", "rptBonafideCertificate_RCPTI.rpt");
            }
            else if (Session["OrgId"].ToString() == "6")
            {
                ShowReport_BC(idno, CERT_SHORT_NAME, "Bonafide_Certificate", "rptBonafideCertificate_RCPIEPER.rpt");

            }
        }
    }

    private void ShowReport_TC_CERT(string param, string CERT_SHORT_NAME, string reportTitle, string rptFileName)
    {
        string studentIds = string.Empty;
        int bonafiedOption = 0;
        int semesterOption = 0;
        int idno = Convert.ToInt32(param);
        int certno = Convert.ToInt32(objCommon.LookUp("ACD_CERTIFICATE_MASTER", "CERT_NO", "CERT_SHORT_NAME='" + CERT_SHORT_NAME + "'"));
        int admbatch = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "admbatch", "IDNO=" + idno));
        int DEGREENO = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + idno));
        int BRANCHNO = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO=" + idno));
        int Semester = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "IDNO=" + idno));
        int ACADEMIC_YEAR = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "isnull(ACADEMIC_YEAR_ID,0)", "IDNO=" + idno));
        int SESSION = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT A INNER JOIN ACD_STUDENT S ON(S.IDNO=A.IDNO AND A.SEMESTERNO=S.SEMESTERNO)", "DISTINCT SESSIONNO", "A.IDNO=" + idno));


        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        if (Session["OrgId"].ToString() == "2")
        {
            url += "&param=@P_IDNO=" + param + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_CERT_NO=" + 2;
        }
        else if (Session["OrgId"].ToString() == "6")
        {
            //url += "&param=@P_IDNO=" + param + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + " @P_ACADEMIC_YEAR=" + Convert.ToInt32(ddlAcademicYear.SelectedValue) + ""; 
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_ADMBATCHNO=" + admbatch + ",@P_SESSIONNO=" + SESSION + ",@P_DEGREENO=" + DEGREENO + ",@P_BRANCHNO=" + BRANCHNO + ",@P_SEMESTERNO=" + Semester + ",@P_CERT_NO=" + certno + ",@P_ACADEMIC_YEAR=" + ACADEMIC_YEAR + "";

            //url += "&param=@P_IDNO=" + param + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + ""; ;
        }

        else
        {
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_ADMBATCHNO=" + admbatch + ",@P_SESSIONNO=" + SESSION + ",@P_DEGREENO=" + DEGREENO + ",@P_BRANCHNO=" + BRANCHNO + ",@P_SEMESTERNO=" + Semester + ",@P_CERT_NO=" + certno + ",@P_ACADEMIC_YEAR=" + ACADEMIC_YEAR + "";
        }
        //+",@P_LEAVE_DATE=" + Convert.ToDateTime(txtleaving.Text); ;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");
        ScriptManager.RegisterClientScriptBlock(this.updStudBonafide, this.updStudBonafide.GetType(), "controlJSScript", sb.ToString(), true);
        //}
    }

    private void ShowReport_BC(string param, string CERT_SHORT_NAME, string reportTitle, string rptFileName)
    {
        string studentIds = string.Empty;
        int idno = Convert.ToInt32(param);
        int certno = Convert.ToInt32(objCommon.LookUp("ACD_CERTIFICATE_MASTER", "CERT_NO", "CERT_SHORT_NAME='" + CERT_SHORT_NAME + "'"));
        int admbatch = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "admbatch", "IDNO=" + idno));
        int DEGREENO = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + idno));
        int BRANCHNO = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO=" + idno));
        int Semester = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "IDNO=" + idno));
        int ACADEMIC_YEAR = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "ACADEMIC_YEAR_ID", "IDNO=" + idno));
        int SESSION = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT A INNER JOIN ACD_STUDENT S ON(S.IDNO=A.IDNO AND A.SEMESTERNO=S.SEMESTERNO)", "DISTINCT SESSIONNO", "A.IDNO=" + idno));
        int idno_tran = Convert.ToInt32(objCommon.LookUp("ACD_CERT_TRAN", "COUNT(IDNO)", "IDNO=" + idno + "AND CERT_NO=" + certno + "AND SEMESTERNO=" + Semester));
        if (idno_tran == 0)
        {

        }
        else
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            if (CERT_SHORT_NAME == "BC")
            {
                if (Session["OrgId"].ToString() == "6")
                {
                    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_ADMBATCHNO=" + admbatch + ",@P_SESSIONNO=" + SESSION + ",@P_DEGREENO=" + DEGREENO + ",@P_BRANCHNO=" + BRANCHNO + ",@P_SEMESTERNO=" + Semester + ",@P_CERT_NO=" + certno + ",@P_ACADEMIC_YEAR=" + ACADEMIC_YEAR + "";
                }
                else
                {
                    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_ADMBATCHNO=" + admbatch + ",@P_SESSIONNO=" + SESSION + ",@P_DEGREENO=" + DEGREENO + ",@P_BRANCHNO=" + BRANCHNO + ",@P_SEMESTERNO=" + Semester + ",@P_CERT_NO=" + certno + "";
                }
            }
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updStudBonafide, this.updStudBonafide.GetType(), "controlJSScript", sb.ToString(), true);
        }
    }

    //private void ShowReport_BC1(string param, string CERT_SHORT_NAME, string reportTitle, string rptFileName)
    //{
    //    string studentIds = string.Empty;
    //    int idno = Convert.ToInt32(param);
    //    int certno = Convert.ToInt32(objCommon.LookUp("ACD_CERTIFICATE_MASTER", "CERT_NO", "CERT_SHORT_NAME='" + CERT_SHORT_NAME + "'"));
    //    int admbatch = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "admbatch", "IDNO=" + idno));
    //    int DEGREENO = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + idno));
    //    int BRANCHNO = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO=" + idno));
    //    int Semester = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "IDNO=" + idno));
    //    int ACADEMIC_YEAR = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "ACADEMIC_YEAR_ID", "IDNO=" + idno));
    //    int SESSION = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT A INNER JOIN ACD_STUDENT S ON(S.IDNO=A.IDNO AND A.SEMESTERNO=S.SEMESTERNO)", "DISTINCT SESSIONNO", "A.IDNO=" + idno));
    //    int idno_tran = Convert.ToInt32(objCommon.LookUp("ACD_CERT_TRAN", "COUNT(IDNO)", "IDNO=" + idno + "AND CERT_NO=" + certno + "AND SEMESTERNO=" + Semester));
    //    if (idno_tran == 0)
    //    {

    //    }
    //    else
    //    {
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,Academic," + rptFileName;

    //        if (CERT_SHORT_NAME == "BC")
    //        {
    //            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + ",@P_ACADEMIC_YEAR=" + Convert.ToInt32(ddlAcademicYear.SelectedValue) + "";
    //        }
    //        else
    //            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue);
    //            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "";

    //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //        sb.Append(@"window.open('" + url + "','','" + features + "');");
    //        ScriptManager.RegisterClientScriptBlock(this.updStudBonafide, this.updStudBonafide.GetType(), "controlJSScript", sb.ToString(), true);
    //    }
    //}


    #endregion
}