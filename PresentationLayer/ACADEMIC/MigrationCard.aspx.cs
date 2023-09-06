using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_MigrationCard : System.Web.UI.Page
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
                   // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
               
            }

            if (Request.QueryString["id"] != null)
            {
                
                ShowStudentDetails();
                
            }

            
        }
        else
        {

            if (Page.Request.Params["__EVENTTARGET"] != null)
            {
                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnsearch"))
                {
                    string[] arg = Page.Request.Params["__EVENTARGUMENT"].ToString().Split(',');
                    bindlist(arg[0], arg[1]);
                }

                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btncancelmodal"))
                {
                    txtSearch.Text = string.Empty;
                    lvStudent.DataSource = null;

                    lvStudent.DataBind();
                    lblNoRecords.Text = string.Empty;
                }
            }
        }
    }

    private void ShowStudentDetails()
    {
        try
        {
         
           
            DataSet ds = null;
            ds = objSC.GetStudentDetailsForMigration(Convert.ToInt32(Request.QueryString["id"].ToString()));
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlAdmbatch.SelectedValue = ds.Tables[0].Rows[0]["ADMBATCH"].ToString();
                        txtStudent.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                        txtName.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                        txtFatherName.Text = ds.Tables[0].Rows[0]["FATHERNAME"].ToString();
                        txtLastDegree.Text = ds.Tables[0].Rows[0]["QUALIEXMNAME"].ToString();
                        txtlastSchool.Text = ds.Tables[0].Rows[0]["SCHOOL_COLLEGE_NAME"].ToString();
                        txtLastRoll.Text = ds.Tables[0].Rows[0]["QEXMROLLNO"].ToString();
                        txtlastExmYear.Text = ds.Tables[0].Rows[0]["YEAR_OF_EXAMHSSC"].ToString();
                        txtAddress.Text = ds.Tables[0].Rows[0]["PADDRESS"].ToString();
                        txtMobile.Text = ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                        txtEmail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                        txtRegistrationNo.Text = ds.Tables[0].Rows[0]["ENROLLNO"].ToString();
                        rdoMigration.SelectedValue = ds.Tables[0].Rows[0]["MIGRATION_ORG_DUPLCT"].ToString();
                        txtCardSerialNo.Text = ds.Tables[0].Rows[0]["CARDNO"].ToString();


                        txtRegistrationNo.Enabled = false;
                        txtCardSerialNo.Enabled = false;
                        ddlAdmbatch.Enabled = false;

                        ViewState["idno"] = Convert.ToInt32(Request.QueryString["id"].ToString());
                        //string IDNO = objCommon.LookUp("ACD_STUDENT", "IDNO", "STUDNAME='" + txtName.Text + "'");
                        //int idNo = Convert.ToInt32(IDNO);
                        string migdate = objCommon.LookUp("ACD_CERT_TRAN", "Convert(varchar(15),max(ISSUE_DATE),103)", "IDNO=" + ViewState["idno"] + "");
                        string idno = objCommon.LookUp("ACD_CERT_TRAN", "max(IDNO)", "IDNO=" + ViewState["idno"] + "");
                        if (!string.IsNullOrEmpty(migdate))
                        {
                            objCommon.DisplayMessage(updSelection, "Already certificate issued on date:" + migdate, this.Page);
                        }

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

    private void bindlist(string category, string searchtext)
    {
        StudentController objSC = new StudentController();
        DataSet ds = objSC.RetrieveStudentDetailsRegistrationNoAllotted(searchtext, category);

        if (ds.Tables[0].Rows.Count > 0)
        {
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
            txtSearch.Text = searchtext;
        }
        else
            lblNoRecords.Text = "Total Records : 0";
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string url = string.Empty;

        if (Request.Url.ToString().IndexOf("&id=") > 0)
            url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
        Response.Redirect(url);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Student objS = new Student();
        StudentQualExm objSQualExam = new StudentQualExm();
        CertificateMaster objCert = new CertificateMaster();


        try
        {
      
   
            //string IDNO = objCommon.LookUp("ACD_STUDENT", "IDNO", "STUDNAME='" + txtName.Text + "' AND ADMBATCH=" + ddlAdmbatch.SelectedValue);
            //int idno = Convert.ToInt32(IDNO);

            if (rdoMigration.SelectedValue == "1")
            {
                string migOrgDuplct = objCommon.LookUp("ACD_CERT_TRAN", "MIGRATION_ORG_DUPLCT", "IDNO=" + ViewState["idno"] + " and MIGRATION_ORG_DUPLCT= 1");
                if (!string.IsNullOrEmpty(migOrgDuplct) && migOrgDuplct == "1")
                {
                    objCommon.DisplayMessage(updSelection, "Already applied for Original Certificate.", this.Page);
                    rdoMigration.SelectedValue = "2";
                    return;
                }
            }


            objS.IdNo = Convert.ToInt32(ViewState["idno"]);
            objS.AdmBatch = Convert.ToInt32(ddlAdmbatch.SelectedValue);
            objS.StudName = txtName.Text.Trim().Equals(string.Empty) ? string.Empty : txtName.Text.Trim();
            objS.FatherName = txtFatherName.Text.Trim().Equals(string.Empty) ? string.Empty : txtFatherName.Text.Trim();
            objSQualExam.SCHOOL_COLLEGE_NAME = txtlastSchool.Text.Trim().Equals(string.Empty) ? string.Empty : txtlastSchool.Text.Trim();
            objSQualExam.QEXMROLLNO = txtLastRoll.Text.Trim().Equals(string.Empty) ? string.Empty : txtLastRoll.Text.Trim();
            objSQualExam.YEAR_OF_EXAMHSSC = txtlastExmYear.Text.Trim().Equals(string.Empty) ? string.Empty : txtlastExmYear.Text.Trim();
            objS.PAddress = txtAddress.Text.Trim().Equals(string.Empty) ? string.Empty : txtAddress.Text.Trim();
            objS.StudentMobile = txtMobile.Text.Trim().Equals(string.Empty) ? string.Empty : txtMobile.Text.Trim();
            objS.EmailID = txtEmail.Text.Trim().Equals(string.Empty) ? string.Empty : txtEmail.Text.Trim();
            if (rdoMigration.SelectedValue == "1")
                objCert.Migration_org_duplct = 1;
            else
                objCert.Migration_org_duplct = 2;
            objCert.CardNo = txtCardSerialNo.Text.Trim().Equals(string.Empty) ? string.Empty : txtCardSerialNo.Text.Trim();



            string output = objSC.UpdateMigrationStudentDetails(objS, objSQualExam, objCert);
            if (output != "-99")
            {
                objCommon.DisplayMessage(updSelection, "Student Registration No. Generated and Saved Successfully.", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(updSelection, "Server Error...", this.Page);
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
    protected void btnPrintCard_Click(object sender, EventArgs e)
    {
        ShowPrintCard("Migration_Card", "rptMigrationCertificate.rpt");
    }

    private void ShowPrintCard(string reportTitle, string rptFileName)
    {

        string IDNO = objCommon.LookUp("ACD_STUDENT", "IDNO", "STUDNAME='" + txtName.Text + "' AND ADMBATCH=" + ddlAdmbatch.SelectedValue);
        int idno = Convert.ToInt32(IDNO);

        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno;
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