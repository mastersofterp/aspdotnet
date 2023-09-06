//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : REPAIR AND MAINTANANCE                                          
// PAGE NAME     : TO ALLOT COMPLAINT TO SELECTED EMPLOYEE                         
// CREATION DATE : 10-SEP-2015                                                  
// CREATED BY    : MRUNAL SINGH    
// DESCRIPTION   : CREATE COMPLAINT CELL FOR MIZO UNIVERSITY. NOW IT WILL BE GIVEN TO DEPT INCHARGE FOR LNMIIT                
// MODIFIED BY   : 
// MODIFIED DATE : 
//=================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Messaging;
using System.Web.Mail;
using System.Xml;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Text.RegularExpressions;


public partial class Complaints_TRANSACTION_ComplaintAllotmentByCell : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    Complaint objCT = new Complaint();
    ComplaintController objCC = new ComplaintController();

    public string Docpath = ConfigurationManager.AppSettings["DirPath"];
    public static string RETPATH = "";

    public string path = string.Empty;
    public string dbPath = string.Empty;

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
            if (Session["userno"] == null && Session["username"] == null &&
                Session["usertype"] == null && Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                CheckPageAuthorization();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                else
                {
                    // lblHelp.Text = "No Help Added";
                }
                if (Convert.ToInt32(Session["idno"]) != 0)
                {
                    DataSet dsCU = null;
                    //dsCU = objCommon.FillDropDown("COMPLAINT_USER", "C_UANO", "C_DEPTNO", "C_UANO =" + Convert.ToInt32(Session["userno"]), "");
                    //if (dsCU.Tables[0].Rows.Count == 0)
                    //{
                    //    pnlCom.Enabled = false;
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('You are not authorized user for complaint cell.');", true);
                    //    return;
                    //}
                    dsCU = objCommon.FillDropDown("COMPLAINT_USER", "C_UANO", "C_DEPTNO", "ISNULL(C_ACTIVE_STATUS,0)=1 AND C_STATUS='A' AND C_UANO =" + Convert.ToInt32(Session["userno"]), "");//Updated by Vijay Andoju on 08-03-2020
                    if (dsCU.Tables[0].Rows.Count > 0)
                    {

                        pnlCom.Enabled = false;
                        divButtons.Visible = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('You are not authorized user for complaint cell.');", true);
                        return;

                    }
                    else
                    {
                        divButtons.Visible = true;
                    }

                }

                BindListView(0);
                FillDepartment();

                txtComplaintDate.Text = Convert.ToString(DateTime.Now);
                txtComplaint.Attributes.Add("readonly", "true");
                ViewState["action"] = "add";
                objCommon.FillDropDownList(ddlCompNature, "COMPLAINT_TYPE", "TYPEID", "TYPENAME", "DEPTID = " + Convert.ToInt32(ddlRMDept.SelectedValue), "");
                //objCommon.FillDropDownList(ddlRMCompTo, "COMPLAINT_STAFF", "STAFFID", "STAFF_NAME", "DEPTNO = " + Convert.ToInt32(ddlRMDept.SelectedValue), "");
                FillComplaintTo(Convert.ToInt32(ddlRMDept.SelectedValue));
                //objCommon.FillDropDownList(ddlPriorityWork, "COMPLAINT_PRIORITY_WORK", "PWID", "PWNAME", "", "");
            }
        }
        else
        {
            if (!(txtComplaintDate.Text == null || txtComplaintDate.Text == "" || txtComplaintDate.Text == string.Empty))
                CalendarExtender1.SelectedDate = Convert.ToDateTime(txtComplaintDate.Text);
        }
    }



    private void BindListView(int StatusId)
    {
        try
        {
            DataSet ds = objCC.GetComplaintsCell(Convert.ToInt16(Session["userno"]), StatusId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvComplaint.DataSource = ds;
                lvComplaint.DataBind();
                ds.Dispose();
            }
            else
            {
                lvComplaint.DataSource = null;
                lvComplaint.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "complaint.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillDepartment()
    {
        try
        {
            objCommon.FillDropDownList(ddlRMDept, "COMPLAINT_DEPARTMENT D INNER JOIN COMPLAINT_USER U ON (D.DEPTID = U.C_DEPTNO)", "D.DEPTID", "D.DEPTNAME", "ISNULL(D.DEL_STATUS,0) = 0 AND U.C_UANO = " + Convert.ToInt16(Session["userno"]), "");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Estate_R_CreateUser.FillEntryFor-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //btnSave.Enabled = false;
            if (txtComplaintDt.Text != string.Empty)
            {
                if (Convert.ToDateTime(txtComplaintDt.Text) < Convert.ToDateTime(System.DateTime.Now.ToString("dd/MM/yyyy")))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Preferable Date Can Not Be Less Than Current Date.');", true);
                    txtComplaintDt.Focus();
                    return;
                }
            }

            objCT.ComplaintId = Convert.ToInt32(ViewState["COMPNO"]);
            objCT.ComplaintDate = Convert.ToDateTime(txtComplaintDate.Text);
            objCT.Deptid = int.Parse(ddlRMDept.SelectedValue);

            objCT.remark = txtDetails.Text.Trim();
            objCT.Ua_No = Int32.Parse(Session["userno"].ToString());
            objCT.CELLALLOTMENTID = 0;


            string[] dNAME = ddlRMCompTo.SelectedItem.Text.Split('-');
            if (dNAME[0].Trim() == "staff".Trim())
            {
                objCT.USER_CATEGORY = 'S';
                objCT.ALLOT_TO_NAME = dNAME[1];
                objCT.Admin_UA_no = Int32.Parse(Session["userno"].ToString());  // in staff case whose login is not possible the complaint will be allotted to Dept Incharge.
            }
            else
            {
                objCT.USER_CATEGORY = 'U';
                objCT.ALLOT_TO_NAME = ddlRMCompTo.SelectedItem.Text;
                objCT.Admin_UA_no = Int32.Parse(ddlRMCompTo.SelectedValue);
            }

           // objCT.PWID = 0; // Convert.ToInt32(ddlPriorityWork.SelectedValue);
            objCT.PWID = Convert.ToInt32(ddlPriorityWork.SelectedValue);


            if (txtComplaintDt.Text == string.Empty)
            {
                objCT.PreferableDate = DateTime.MinValue;
            }
            else
            {
                objCT.PreferableDate = Convert.ToDateTime(txtComplaintDt.Text);
            }

            if (txtPerferTime.Text == string.Empty)
            {
                objCT.PreferableTime = DateTime.MinValue;
            }
            else
            {
                objCT.PreferableTime = Convert.ToDateTime(txtPerferTime.Text);
            }

            if (txtPerferTo.Text == string.Empty)
            {
                objCT.PreferableTimeTo = DateTime.MinValue;
            }
            else
            {
                objCT.PreferableTimeTo = Convert.ToDateTime(txtPerferTo.Text);
            }

            if (rdocomp2.Checked == true)
            {
                objCT.ComplaintStatus = 'C';
            }
            else
            {
               
            }

            if (txtReallotment.Text != string.Empty)
            {
                objCT.SUGGESTION = txtReallotment.Text;
            }
            else
            {
                objCT.SUGGESTION = string.Empty;
            }

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objCC.AddComplaintAllotmentByCell(objCT);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindListView(0);
                        objCommon.DisplayMessage(UpdatePanel1, "Service Allotted Successfully.", this.Page);
                        SendSMSToComplaintee(Convert.ToInt32(ViewState["COMPNO"]));
                        ClearAll();
                    }
                    else
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "Sorry!Try Again.", this.Page);
                    }
                }

                else if (ViewState["action"].ToString().Equals("edit"))
                {

                    objCT.CELLALLOTMENTID = Convert.ToInt32(ViewState["ALLOTMENT_ID"]);
                    CustomStatus cs = (CustomStatus)objCC.AddComplaintAllotmentByCell(objCT);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindListView(0);
                        objCommon.DisplayMessage(UpdatePanel1, "Service Updated Successfully.", this.Page);
                        SendSMSToComplaintee(Convert.ToInt32(ViewState["COMPNO"]));
                        ClearAll();
                    }
                    else
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "Sorry!Try Again.", this.Page);
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "complaint.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // This button is use to send emails to committee members
    private void SendSMSToComplaintee(int ComplaintId)
    {
        try
        {
            string body = string.Empty;
            DataSet ds = objCC.GetAllottedUserData(ComplaintId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string receiver = string.Empty;
                string mobilenum = string.Empty;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["EMAILID"].ToString() != "")
                    {
                        if (receiver == string.Empty)
                        {
                            receiver = ds.Tables[0].Rows[i]["EMAILID"].ToString();
                        }
                        else
                        {
                            receiver = receiver + "," + ds.Tables[0].Rows[i]["EMAILID"].ToString();
                        }

                    }
                }

                string FromEmailID = string.Empty;
                string FromEmailPassword = string.Empty;
                DataSet dsReff = objCommon.FillDropDown("REFF", "IS_COMPLAINT_EMAIL", "EMAILSVCID,EMAILSVCPWD", "", "");
                if (dsReff.Tables[0].Rows.Count > 0)
                {
                    if (dsReff.Tables[0].Rows[0]["IS_COMPLAINT_EMAIL"].ToString() == "1")
                    {
                        FromEmailID = dsReff.Tables[0].Rows[0]["EMAILSVCID"].ToString();
                        FromEmailPassword = dsReff.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();
                        sendmail(FromEmailID, FromEmailPassword, receiver, "Service Request", "Dear Sir");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Estt_complaint.BindListViewFiles-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void sendmail(string fromEmailId, string fromEmailPwd, string toEmailId, string Sub, string body)
    {
        try
        {
            string msg = string.Empty;
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = Sub;

            DataSet ds = objCommon.FillDropDown("COMPLAINT_REGISTER CR INNER JOIN COMPLAINT_TYPE CT ON (CR.TYPEID = CT.TYPEID) INNER JOIN CELL_COMPLAINT_ALLOTMENT CA ON (CR.COMPLAINTID = CA.COMPLAINTID)", "CR.COMPLAINTNO, CR.COMPLAINT,CR.COMPLAINTEE_NAME, CT.TYPENAME", "CA.ALLOT_TO_NAME, CA.REMARK", "CR.COMPLAINTID=" + Convert.ToInt32(ViewState["COMPNO"]) + "", "");

            string MemberEmailId = string.Empty;
            mailMessage.From = new MailAddress(HttpUtility.HtmlEncode(fromEmailId));
            mailMessage.To.Add(toEmailId);

            if (ds.Tables[0].Rows.Count > 0)
            {
                var MailBody = new StringBuilder();
                MailBody.AppendFormat("Allotment of Service Request.{0}\n", " ");
                MailBody.AppendLine(@"<br /> Request No. : " + ds.Tables[0].Rows[0]["COMPLAINTNO"]);
                MailBody.AppendLine(@"<br /> Requester Name : " + ds.Tables[0].Rows[0]["COMPLAINTEE_NAME"]);
                MailBody.AppendLine(@"<br /> Request Type : " + ds.Tables[0].Rows[0]["TYPENAME"]);
                MailBody.AppendLine(@"<br /> Request : " + ds.Tables[0].Rows[0]["COMPLAINT"]);
                MailBody.AppendLine(@"<br /> Job Assigned To : " + ds.Tables[0].Rows[0]["ALLOT_TO_NAME"]);
                MailBody.AppendLine(@"<br /> Department Remarks : " + ds.Tables[0].Rows[0]["REMARK"]);
                MailBody.AppendLine(@"<br /> ");
                MailBody.AppendLine(@"<br /> Thank You.");
                mailMessage.Body = MailBody.ToString();
            }


            mailMessage.IsBodyHtml = true;
            SmtpClient smt = new SmtpClient("smtp.gmail.com");

            smt.UseDefaultCredentials = false;
            smt.Credentials = new NetworkCredential(HttpUtility.HtmlEncode(fromEmailId), HttpUtility.HtmlEncode(fromEmailPwd));
            smt.Port = 587;
            smt.EnableSsl = true;

            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,
            System.Security.Cryptography.X509Certificates.X509Certificate certificate,
            System.Security.Cryptography.X509Certificates.X509Chain chain,
            System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            smt.Send(mailMessage);

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAll();
    }

    private void ClearAll()
    {
        txtComplaintDate.Text = Convert.ToString(DateTime.Now); //DateTime.Today.ToString();    
        txtDetails.Text = string.Empty;
        txtComplaint.Text = string.Empty;
        ddlRMDept.SelectedIndex = -1;
        ddlRMCompTo.SelectedIndex = -1;
        ViewState["COMPNO"] = null;
        ViewState["action"] = "add";
        ViewState["ALLOTMENT_ID"] = null;

        ddlCompNature.SelectedIndex = 0;
        btnDecline.Enabled = true;
         ddlPriorityWork.SelectedIndex = 0;

        pnlFile.Visible = false;
        lvfile.DataSource = null;
        lvfile.DataBind();
        pnlCom.Visible = false;
        txtComplaintDt.Text = string.Empty;
        //txtPerferTime.Text = string.Empty;
        //txtPerferTo.Text = string.Empty;

        // btnSave.Enabled = true;
        txtReallotment.Text = string.Empty;
        divReply.Visible = false;
        divSuggestion.Visible = false;
    }

    protected void ddlRMDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearEmployeeAndFill(Convert.ToInt32(ddlRMDept.SelectedValue));
        ddlRMCompTo.Focus();
    }

    private void FillComplaintTo(int deptid)
    {
        try
        {
            DataSet ds = objCC.GetComplaintToEmployee(deptid);

            ddlRMCompTo.DataSource = ds;
            ddlRMCompTo.DataValueField = ds.Tables[0].Columns["UA_NO"].ToString();
            ddlRMCompTo.DataTextField = ds.Tables[0].Columns["UA_FULLNAME"].ToString();
            ddlRMCompTo.DataBind();
            ddlRMCompTo.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "complaint.FillEmployee-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //Clear Emoloyee DDL and Fill again
    private void ClearEmployeeAndFill(Int32 deptid)
    {
        ddlRMCompTo.Items.Clear();
        ddlRMCompTo.Items.Add("Please Select");
        ddlRMCompTo.SelectedItem.Value = "-1";
        //FillComplaintTo(deptid);
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        //BindListView(Convert.ToInt32(rdbStatus.SelectedValue));
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
        }
    }

    protected void btnDecline_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtDetails.Text != string.Empty)
            {
                if (ViewState["action"].ToString().Equals("edit"))
                {
                    objCT.ComplaintId = Convert.ToInt32(ViewState["COMPNO"]);
                    objCT.remark = txtDetails.Text.Trim();
                    CustomStatus cs = (CustomStatus)objCC.UpdateDeclinedRemark(objCT);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        BindListView(0);
                        ClearAll();
                        objCommon.DisplayMessage(UpdatePanel1, "Service Declined Successfully.", this.Page);
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(UpdatePanel1, "Please Enter Remark.", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "complaint.btnDecline_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            txtComplaintDate.Text = Convert.ToString(DateTime.Now);
            ImageButton btnEdit = sender as ImageButton;
            int COMPNO = int.Parse(btnEdit.CommandArgument);
            string complaintno = btnEdit.ToolTip;
            ViewState["COMPNO"] = int.Parse(btnEdit.CommandArgument);
            ViewState["ALLOTMENT_ID"] = int.Parse(btnEdit.AlternateText);

            ViewState["action"] = "edit";
            ShowDetails(COMPNO);
            ddlRMDept.Focus();

            string file = Docpath + "COMPLAINTS\\ComplaintsFiles\\ComplaintNo_" + complaintno.Replace('/', '-');
            ViewState["FILE_PATH"] = Docpath + "COMPLAINTS\\ComplaintsFiles\\ComplaintNo_" + complaintno.Replace('/', '-');
            BindListViewFiles(file);
            pnlFile.Visible = true;
            pnlCom.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "complaint.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void ShowDetails(int COMPNO)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("COMPLAINT_REGISTER R INNER JOIN COMPLAINT_AREA A ON (R.AREAID = A.AREAID)  LEFT JOIN CELL_COMPLAINT_ALLOTMENT CA ON(R.COMPLAINTID = CA.COMPLAINTID) INNER JOIN  COMPLAINT_DETAIL CD ON (R.COMPLAINTID = CD.COMPLAINTID)",
                "CA.ALLOTMENT_DATE, R.COMPLAINT, R.ALLOTMENTSTATUS, ISNULL(R.DEPTID,0) AS DEPTID, COMPLAINTEE_NAME, COMPLAINTNO",
                "ISNULL(CA.USERNO,0) AS USERNO, ISNULL(CA.REMARK,'') AS REMARK,ISNULL(CA.REALLOTMENTREMARK,'') AS REALLOTMENTREMARK, R.TYPEID,R.COMPLAINTEE_ADDRESS,R.COMPLAINTDATE, A.AREANAME,  Format(PREFERABLE_TIME, 'hh:mm tt') AS PREFERABLE_TIME, Format(PREFERABLE_TIME_TO,'hh:mm tt')AS PREFERABLE_TIME_TO,  PREFERABLE_DATE ,isnull(CA.PWID,0) as PWID , (CD.REMARK) AS RESPONSE,CD.COMPLAINT_STATUS",
                "R.COMPLAINTID=" + COMPNO, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtComplainer.Text = ds.Tables[0].Rows[0]["COMPLAINTEE_NAME"].ToString();
                txtComplaintNo.Text = ds.Tables[0].Rows[0]["COMPLAINTNO"].ToString();
                txtComplaint.Text = ds.Tables[0].Rows[0]["COMPLAINT"].ToString();
                txtLoc.Text = ds.Tables[0].Rows[0]["AREANAME"].ToString();


                txtPerferTime.Text = ds.Tables[0].Rows[0]["PREFERABLE_TIME"].ToString().Trim();
                txtPerferTo.Text = ds.Tables[0].Rows[0]["PREFERABLE_TIME_TO"].ToString().Trim();


                txtComplaintAdd.Text = ds.Tables[0].Rows[0]["COMPLAINTEE_ADDRESS"].ToString();
                txtComplaintDt.Text = ds.Tables[0].Rows[0]["PREFERABLE_DATE"].ToString();
                ddlRMDept.SelectedValue = ds.Tables[0].Rows[0]["DEPTID"].ToString();
                //  FillComplaintTo(Convert.ToInt32(ddlRMDept.SelectedValue));
                //  objCommon.FillDropDownList(ddlRMCompTo, "COMPLAINT_STAFF", "STAFFID", "STAFF_NAME", "DEPTNO = " + Convert.ToInt32(ddlRMDept.SelectedValue), "");
                ddlRMCompTo.SelectedValue = ds.Tables[0].Rows[0]["USERNO"].ToString();
                txtDetails.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
                ddlCompNature.Text = ds.Tables[0].Rows[0]["TYPEID"].ToString();
                ddlPriorityWork.SelectedValue = ds.Tables[0].Rows[0]["PWID"].ToString();
                txtReallotment.Text = ds.Tables[0].Rows[0]["REALLOTMENTREMARK"].ToString();
                if (ds.Tables[0].Rows[0]["ALLOTMENTSTATUS"].ToString() == "A")
                {
                    btnDecline.Enabled = false;
                }
                else
                {
                    btnDecline.Enabled = true;
                }

                if (ds.Tables[0].Rows[0]["COMPLAINT_STATUS"].ToString() == "R")
                {
                    divReply.Visible = true;
                    txtReply.Text = ds.Tables[0].Rows[0]["RESPONSE"].ToString();
                    divSuggestion.Visible = true;
                    //txtReallotment.Visible = true;
                }
                else
                {
                    divReply.Visible = false;
                    txtReply.Visible = false;
                    divSuggestion.Visible = false;
                    //txtReallotment.Visible = false;

                }


            }
            else
            {
                // pnlCom.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "complaint.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #region Attachments

    private void BindListViewFiles(string PATH)
    {
        try
        {
            pnlFile.Visible = true;
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(PATH);
            if (System.IO.Directory.Exists(PATH))
            {
                System.IO.FileInfo[] files = dir.GetFiles();

                if (Convert.ToBoolean(files.Length))
                {
                    lvfile.DataSource = files;
                    lvfile.DataBind();
                    ViewState["FILE"] = files;
                }
                else
                {
                    lvfile.DataSource = null;
                    lvfile.DataBind();
                }
            }
            else
            {
                lvfile.DataSource = null;
                lvfile.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Estt_complaint.BindListViewFiles-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void imgFileDownload_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        DownloadFile(btn.AlternateText);
    }

    public void DownloadFile(string filePath)
    {
        try
        {
            string[] fname1 = filePath.ToString().Split('_');
            string filename = filePath.Substring(filePath.LastIndexOf("_") + 1);

            path = Docpath + "COMPLAINTS\\ComplaintsFiles\\ComplaintNo_" + fname1[1];
            FileStream sourceFile = new FileStream((path + "\\" + filePath), FileMode.Open);
            long fileSize = sourceFile.Length;
            byte[] getContent = new byte[(int)fileSize];
            sourceFile.Read(getContent, 0, (int)sourceFile.Length);
            sourceFile.Close();

            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.BinaryWrite(getContent);
            Response.ContentType = GetResponseType(filePath.Substring(filePath.IndexOf('.')));
            //Response.AddHeader("content-disposition", "attachment; filename=" + filePath);

            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + filename + "\"");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        catch (Exception ex)
        {
            Response.Clear();
            Response.ContentType = "text/html";
            Response.Write("Unable to download the attachment.");
        }
    }

    private string GetResponseType(string fileExtension)
    {
        switch (fileExtension.ToLower())
        {
            case ".doc":
                return "application/vnd.ms-word";
                break;

            case ".docx":
                return "application/vnd.ms-word";
                break;

            case ".xls":
                return "application/ms-excel";
                break;

            case ".xlsx":
                return "application/ms-excel";
                break;

            case ".pdf":
                return "application/pdf";
                break;

            case ".ppt":
                return "application/vnd.ms-powerpoint";
                break;

            case ".txt":
                return "text/plain";
                break;

            case ".jpg":
                return "application/{0}";
                break;
            case ".jpeg":
                return "application/{0}";
                break;
            case "":
                return "";
                break;

            default:
                return "";
                break;
        }
    }

    protected string GetFileName(object obj)
    {
        string f_name = string.Empty;
        string[] fname = obj.ToString().Split('_');

        if (fname[0] == "ComplaintNo")
            f_name = Convert.ToString(fname[2]);

        if (fname[0] == "judDoc")
            f_name = Convert.ToString(fname[3]);

        return f_name;
    }

    protected string GetFileNameCaseNo(object obj)
    {
        string f_name = string.Empty;

        string[] fname = obj.ToString().Split('_');

        if (fname[0] == "ComplaintNo")
            f_name = Convert.ToString(fname[1]);
        f_name = f_name.Replace('-', '/');
        if (fname[0] == "judDoc")
            f_name = Convert.ToString(fname[3]);
        return f_name;
    }

    protected string GetFileDate(object obj)
    {
        string file_path = Convert.ToString(ViewState["FILE_PATH"] + "\\" + obj.ToString());
        FileInfo fileInfo = new FileInfo(file_path);

        DateTime creationTime = DateTime.MinValue;
        creationTime = fileInfo.CreationTime;

        string f_date = string.Empty;
        f_date = creationTime.ToString("dd-MMM-yyyy");
        return f_date;
    }

    protected void rdbStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindListView(Convert.ToInt32(rdbStatus.SelectedValue));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "complaint.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    #endregion





    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            Button btnPrint = sender as Button;
            ViewState["COMPLAINTID"] = int.Parse(btnPrint.CommandArgument);
            ShowReport("ComplaintRegister", "rptServiceRequestRegister.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Estt_complaint.btnPrint_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Complaints")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Complaints," + rptFileName;
            url += "&param=@P_COMPLAINTID=" + ViewState["COMPLAINTID"].ToString() + ",@P_USERNAME=" + Session["userfullname"];

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    protected void btnAddfile_Click(object sender, EventArgs e)
    {

    }
   
}



