///======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HEALTH       
// CREATION DATE : 22-FEB-2016
// CREATED BY    : MRUNAL SINGH                                      
// MODIFIED DATE :
// MODIFIED DESC :
//====================================================================================== 

using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Health;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Security;
using System.Web.UI.HtmlControls;


public partial class Health_Report_Certificates : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    HealthTransactionController objHelTranTransaction = new HealthTransactionController();
    HealthTransactions objHelTran = new HealthTransactions();
    Health objHel = new Health();
    HelMasterController objHelController = new HelMasterController();
    LabMaster objLab = new LabMaster();
    LabController objLabController = new LabController();

    #region Page Events
    /// <summary>
    /// This Page_Load event checks whether the user has login or not by checking Session["userno"],Session["username"],   
    /// </summary>
    /// 
    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                   // objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS", "IDNO", "PFILENO+ ' - ' +isnull(Title,'')+' '+isnull(FNAME,'')+' '+isnull(MNAME,'')+' '+isnull(LNAME,'')  AS NAME", "", "IDNO");

                    ViewState["action"] = "add";
                    BindlistView();  
                    if (Request.QueryString["id"] != null)
                    {
                        int id = Convert.ToInt32(Request.QueryString["id"]);
                        string Name = Request.QueryString["Name"];
                        if (Request.QueryString["Name"] == "EmployeeCode" || Request.QueryString["Name"] == "EmployeeName")
                        {

                            //objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS", "IDNO", "isnull(Title,'')+' '+isnull(FNAME,'')+' '+isnull(MNAME,'')+' '+isnull(LNAME,'')  AS NAME", "", "IDNO");
                           // ddlEmployee.SelectedValue = id.ToString();
                            hfPatientName.Value = id.ToString();
                            lblPatientCat.Text = "E";
                            FillEmployeeInfo(id);
                        }
                        if (Request.QueryString["Name"] == "StudentName" || Request.QueryString["Name"] == "StudentRegNo")
                        {
                          //  objCommon.FillDropDownList(ddlEmployee, "ACD_STUDENT", "IDNO", "STUDNAME AS NAME", "", "IDNO");
                          //  ddlEmployee.SelectedValue = id.ToString();
                            hfPatientName.Value = id.ToString();
                            lblPatientCat.Text = "S";
                            FillStudentInfo(id);
                        }
                        if (Request.QueryString["Name"] == "Other")
                        {
                           // objCommon.FillDropDownList(ddlEmployee, "HEALTH_PATIENT_DETAILS", "DISTINCT PID AS IDNO", "PATIENT_NAME  AS NAME", "PATIENT_CODE='O'", "");
                           // ddlEmployee.SelectedValue = id.ToString();
                            hfPatientName.Value = id.ToString();
                            lblPatientCat.Text = "O";
                            FillOtherPatientInfo(id);
                        }
                    }
                    //else
                    //{
                    //    if (Convert.ToInt32(Session["usertype"]) != 1)
                    //    {
                    //        imgSearch.Visible = false;
                    //        //ddlEmployee.SelectedValue = Session["idno"].ToString();
                    //        //ddlEmployee.Enabled = false;
                    //        lblPatientCat.Text = "E";
                    //        return;
                    //    }
                    //}      
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
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Report_Certificates.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void FillEmployeeInfo(int idno)
    {
        try
        {
            DataSet ds = objLabController.GetPatientDetailsByPID(idno);
            if (ds.Tables[0].Rows.Count != null)
            {
                txtPatientName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                hfPatientName.Value = ds.Tables[0].Rows[0]["IDNO"].ToString();
            }
            objCommon.FillDropDownList(ddlVisitDate, "HEALTH_PATIENT_DETAILS", "OPDID", "CONVERT(VARCHAR(30),OPDTIME, 113) AS OPDDT", "PATIENT_CODE IN ('E','D') AND ISSUE_CERTIFICATE=1 AND PID=" + idno, "OPDID DESC");
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Report_Certificates.FillEmployeeInfo() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    private void FillStudentInfo(int idno)
    {
        try
        {
            DataSet ds = objLabController.GetStudentInfo(idno);
            if (ds.Tables[0].Rows.Count != null)
            {
                hfPatientName.Value = ds.Tables[0].Rows[0]["IDNO"].ToString();
                txtPatientName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                txtPatientName.Focus();                
            }
            objCommon.FillDropDownList(ddlVisitDate, "HEALTH_PATIENT_DETAILS", "OPDID", "CONVERT(VARCHAR(30),OPDTIME, 113) AS OPDDT", "PATIENT_CODE ='S' AND ISSUE_CERTIFICATE=1 AND PID=" + idno, "OPDID DESC");
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Report_Certificates.FillStudentInfo() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void FillOtherPatientInfo(int idno)
    {
        try
        {
            HelMasterController objHelMaster = new HelMasterController();
            DataSet ds = objHelMaster.GetOtherPatientInfo(idno);
            if (ds.Tables[0].Rows.Count != null)
            {
                txtPatientName.Text = ds.Tables[0].Rows[0]["PATIENT_NAME"].ToString();
                hfPatientName.Value = ds.Tables[0].Rows[0]["PID"].ToString();
                txtPatientName.Focus();            
            }
            objCommon.FillDropDownList(ddlVisitDate, "HEALTH_PATIENT_DETAILS", "OPDID", "CONVERT(VARCHAR(30),OPDTIME, 113) AS OPDDT", "PATIENT_CODE ='O' AND ISSUE_CERTIFICATE=1 AND PID=" + idno, "OPDID DESC");
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Report_Certificates.FillOtherPatientInfo() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    /// <summary>
    /// Page_PreInit event calls SetMasterPage() method.   @P_PATIENT_CODE
    /// </summary>
    /// 
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    private void UnBindList()
    {
        try
        {
            DataTable dt = null;
            lvEmp.DataSource = dt;
            lvEmp.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Report_Certificates.bindlist-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void bindlist(string category, string searchtext)
    {
        try
        {
            ViewState["PCAT"] = category;
            DataTable dt = objHelTranTransaction.RetrievePatientDetails(searchtext, category);
            lvEmp.DataSource = dt;
            lvEmp.DataBind();
            if (category == "StudentName" || category == "StudentRegNo")
            {
                HtmlTableRow trHeader = ((HtmlTableRow)lvEmp.FindControl("trHeader"));
                trHeader.Cells[1].InnerText = "RegNo";
                trHeader.Cells[2].InnerText = "Branch";
                trHeader.Cells[3].InnerText = "Degree";
            }
            else
            {
                HtmlTableRow trHeader = ((HtmlTableRow)lvEmp.FindControl("trHeader"));
                trHeader.Cells[1].InnerText = "Employee Code";
                trHeader.Cells[2].InnerText = " Department";
                trHeader.Cells[3].InnerText = "Designation";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Report_Certificates.bindlist-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
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
        Response.Redirect(url + "&id=" + lnk.CommandArgument + "&Name=" + ViewState["PCAT"].ToString());
    }

    private void BindlistView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("HEALTH_CERTIFICATE_ISSUE CI INNER JOIN HEALTH_PATIENT_DETAILS PD ON (CI.OPDID = PD.OPDID)", "CI.CI_ID, CI.CI_DATE, CI.REMARK, CI.OPDID, PD.PATIENT_NAME, (CASE CI.CERTIFICATENAME WHEN 1 THEN 'MEDICAL CERTIFICATE' WHEN 2 THEN 'REFERENCE CERTIFICATE' WHEN 3 THEN 'FITNESS CERTIFICATE'  END) AS NAME", "CI.CERTIFICATENAME", "", "CI_ID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvCertificate.DataSource = ds;
                lvCertificate.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Report_Certificates.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
    #endregion

    #region Actions
   
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string IP = Request.ServerVariables["REMOTE_HOST"];
        string MAC_ADDRESS = "";
        ShowReport("Prescription", "rpt_Prescription.rpt", IP, MAC_ADDRESS, sender);
    }

    #endregion

    #region Private Methods
    /// <summary>
    /// CheckPageAuthorization() method checks whether the user is authorised to access this Page    
    /// </summary>
    /// 
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CreateOperator.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CreateOperator.aspx");
        }
    }      

    /// <summary>
    /// ShowReport() method displays the report.  
    /// Page title and parameters like Session["userfullname"] are passed to the "CommonReport.aspx" page.
    /// </summary>
    /// 
    private void ShowReport(string reportTitle, string rptFileName, string IPAddress, string MacAddress, object sender)
    {
        try
        {
            ImageButton btnPrint = sender as ImageButton;
            int OPDID = int.Parse(btnPrint.CommandArgument);
            // string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("HEALTH")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Health")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,HEALTH," + rptFileName;
            url += "&param=@P_OPDID=" + OPDID + ",username=" + Session["userfullname"].ToString() + ",IP_ADDRESS=" + IPAddress + ",MAC_ADDRESS=" + MacAddress;
            ScriptManager.RegisterClientScriptBlock(updOpdTransaction, updOpdTransaction.GetType(), "Window", "window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');", true);
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Report_Certificates.ShowReport ->" + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    } 
  
    protected void btnMedCertificate_Click(object sender, EventArgs e)
    {
        
    }
  
    protected void btnRefCertificate_Click(object sender, EventArgs e)
    {
        
    }
    protected void btnFitCertificate_Click(object sender, EventArgs e)
    {
        
    } 
   
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["id"] == null)
            {
                lblPatientCat.Text = "E";       
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Report_Certificates.ddlEmployee_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int opdID = 0;
        try
        {
            objHel.REMARK = txtRemark.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtRemark.Text.Trim());
            objHel.CERTI_NAME_ID = Convert.ToInt32(ddlCertiName.SelectedValue);
            objHel.PID = Convert.ToInt32(hfPatientName.Value);  //Convert.ToInt32(ddlEmployee.SelectedValue);
            objHel.P_CODE = Convert.ToChar(lblPatientCat.Text);
            string OPDID = objCommon.LookUp("HEALTH_PATIENT_DETAILS", "TOP 1 OPDID", "PID=" + Convert.ToInt32(hfPatientName.Value) + " AND PATIENT_CODE='" + Convert.ToChar(lblPatientCat.Text) + "' ORDER BY OPDID DESC");
             if (OPDID != "")
             {
                 opdID = Convert.ToInt32(OPDID);
             }
             else
             {
                 objCommon.DisplayMessage(this.updOpdTransaction, "Visiting details are not available.", this.Page);
                 return;
             }

            objHelTran.OPDID = opdID; //  Convert.ToInt32(ddlVisitDate.SelectedValue);
            objHel.AUDIT_DATE = Convert.ToDateTime(txtIssueDt.Text);

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objHelController.AddCertificateIssue(objHel, objHelTran);
                    if (cs.Equals(CustomStatus.RecordExist))
                    {
                        Clear();
                        objCommon.DisplayMessage(this.updOpdTransaction, "Record Already Exist.", this.Page);
                        return;
                    }
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindlistView();
                        ViewState["action"] = "add";
                        Clear();
                        objCommon.DisplayMessage(this.updOpdTransaction, "Record Save Successfully.", this.Page);
                    }
                }
                else
                {
                    if (ViewState["CI_ID"] != null)
                    {
                        objHel.CI_ID = Convert.ToInt32(ViewState["CI_ID"].ToString());
                        CustomStatus cs = (CustomStatus)objHelController.AddCertificateIssue(objHel, objHelTran);
                        if (cs.Equals(CustomStatus.RecordExist))
                        {
                           Clear();
                            objCommon.DisplayMessage(this.updOpdTransaction, "Record Already Exist.", this.Page);
                            return;
                        }
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            BindlistView();
                            ViewState["action"] = "add";
                            objCommon.DisplayMessage(this.updOpdTransaction, "Record Updated Successfully.", this.Page);
                           Clear();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Report_Certificates.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clear()
    {
        txtPatientName.Text = string.Empty;
        hfPatientName.Value = string.Empty;
        ddlVisitDate.SelectedIndex = 0;
        txtIssueDt.Text = string.Empty;
        txtRemark.Text = string.Empty;
        ddlCertiName.SelectedIndex = 0;
        ViewState["action"] = "add";
        ViewState["CI_ID"] = null;

    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int CI_NO = int.Parse(btnEdit.CommandArgument);
            ViewState["CI_ID"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowDetails(CI_NO);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Report_Certificates.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    private void ShowDetails(int CI_NO)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("HEALTH_CERTIFICATE_ISSUE", "*", "", "CI_ID=" + CI_NO, "CI_ID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
                txtIssueDt.Text = ds.Tables[0].Rows[0]["CI_DATE"].ToString();
                ddlCertiName.SelectedValue = ds.Tables[0].Rows[0]["CERTIFICATENAME"].ToString();
                int opdID = Convert.ToInt32(ds.Tables[0].Rows[0]["OPDID"].ToString());
                objCommon.FillDropDownList(ddlVisitDate, "HEALTH_PATIENT_DETAILS", "OPDID", "CONVERT(VARCHAR(30),OPDTIME, 113) AS OPDDT", "OPDID=" + opdID, "OPDID DESC");
                ddlVisitDate.SelectedValue = opdID.ToString();
                string PATIENT_NAME = objCommon.LookUp("HEALTH_PATIENT_DETAILS", "PATIENT_NAME", "OPDID=" + opdID);
                string pID = objCommon.LookUp("HEALTH_PATIENT_DETAILS", "PID", "OPDID=" + opdID);
                lblPatientCat.Text = objCommon.LookUp("HEALTH_PATIENT_DETAILS", "PATIENT_CODE", "OPDID=" + opdID);
                txtPatientName.Text = PATIENT_NAME;
                hfPatientName.Value = pID.ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Report_Certificates.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();

    }



    protected void btnPrint_Click1(object sender, EventArgs e)
    {
        Button btnPrint = sender as Button;
        int certiNo = int.Parse(btnPrint.CommandArgument);
        ViewState["CERTI_NO"] = int.Parse(btnPrint.CommandArgument);
        ViewState["OPDID"] = int.Parse(btnPrint.CommandName);


        if (certiNo == 1) // Medical certificate
        {
            string IP = Request.ServerVariables["REMOTE_HOST"];
            string MAC_ADDRESS = "";
            ShowMedicalReport("Medical", "MedicalCertificateReport.rpt", IP, MAC_ADDRESS, sender);

        }
        else if (certiNo == 2) // Reference certificate
        {
            string IP = Request.ServerVariables["REMOTE_HOST"];
            string MAC_ADDRESS = "";
            ShowMedicalReport("Referral", "ReferenceCertificateReport.rpt", IP, MAC_ADDRESS, sender);
        }
        else if (certiNo == 3) // Fitness certificate
        {
            string IP = Request.ServerVariables["REMOTE_HOST"];
            string MAC_ADDRESS = "";
            ShowMedicalReport("MedicalFitness", "FitnessCertificateReport.rpt", IP, MAC_ADDRESS, sender);
        }
       

    }

    // This method is used to get the medical certificate report.
    private void ShowMedicalReport(string reportTitle, string rptFileName, string IPAddress, string MacAddress, object sender)
    {
        try
        {                     
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Health")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,HEALTH," + rptFileName;            
            url += "&param=@P_OPDID=" + Convert.ToInt32(ViewState["OPDID"]); 

            ScriptManager.RegisterClientScriptBlock(updOpdTransaction, updOpdTransaction.GetType(), "Window", "window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');", true);
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Report_Certificates.ShowMedicalReport ->" + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    } 
}