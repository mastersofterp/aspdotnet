//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : REPAIT AND MAINTAINANCE                                         
// PAGE NAME     : TO ALLOT COMPLAINT TO SELECTED EMPLOYEE                         
// CREATION DATE : 16-April-2009                                                   
// CREATED BY    : G.V.S KIRAN KUMAR                                               
// MODIFIED BY   : 
// MODIFIED DESC : 
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
using System.Web.UI.WebControls.WebParts;

using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Messaging;
using System.Web.Mail;
using System.Xml;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Text.RegularExpressions;

public partial class Estate_Complaint_Allotment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ComplaintController objCC = new ComplaintController();
    Complaint objWorkedOut = new Complaint();
    DataTable DtItemList = new DataTable("DtItemList");
    public string Docpath = ConfigurationManager.AppSettings["DirPath"];
    public static string RETPATH = "";
    public string path = string.Empty;
    public string dbPath = string.Empty;


    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, System.EventArgs e)
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

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Check browser and set table width

                //if (Request.Browser.Browser.ToLower().Equals("opera"))
                //    tblMain.Width = "100%";
                //else if (Request.Browser.Browser.ToLower().Equals("ie"))
                //    tblMain.Width = "100%";
                //else
                //    tblMain.Width = "100%";

                //Load Page Helpb
                if (Request.QueryString["pageno"] != null)
                {
                    // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                //Bind the ListView with Domain
                BindListViewComplaintAllotment(Convert.ToInt32(Session["userno"].ToString()));

                //Filling complaint nature by user number
                FillComplaintNature(Convert.ToInt32(Session["userno"].ToString()));
                //txtWorkoutDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                txtWorkoutDate.Text = Convert.ToString(DateTime.Now);
                objCommon.FillDropDownList(ddlitemlist, "COMPLAINT_ITEMMASTER", "ITEMID", "ITEMNAME", "", "ITEMID");
                Session["DtItemList"] = null;
                ViewState["CWID"] = null;
                ViewState["complaintno"] = null;
            }
        }
    }


    private void BindListViewComplaintAllotment(int userno)
    {
        try
        {
            DataSet ds = objCC.GetAllComplaintsUser(userno);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                PnlCompAllot.Visible = false;
            }
            else
            {
                PnlCompAllot.Visible = true;
                lvComplaint.DataSource = ds;
                lvComplaint.DataBind();
                ds.Dispose();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Estate_Complaint_Allotment.BindListViewComplaintAllotment-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Clear();
            ImageButton btnEdit = sender as ImageButton;
            int complaintno = int.Parse(btnEdit.CommandArgument);

            ViewState["complaintno"] = int.Parse(btnEdit.CommandArgument);
            ViewState["compl"] = objCommon.LookUp("COMPLAINT_REGISTER", "COMPLAINTNO", "COMPLAINTID=" + ViewState["complaintno"].ToString());//ADDED  BY VIJAY ON 24-02-2020

            FillComplaintNature(Convert.ToInt32(Session["userno"].ToString()));

            //added by Sonal Banode
            //string compstatus = objCommon.LookUp("COMPLAINT_REGISTER", "COMPLAINTSTATUS", "COMPLAINTID=" + complaintno);
            
            //if (compstatus == "C")
            //{
            //    MessageBox("Record cannot be edited as status is completed.");
            //    return;
            //}
            ShowDetails(complaintno);

            DataSet ds = objCommon.FillDropDown("COMPLAINT_REGISTER R INNER JOIN CELL_COMPLAINT_ALLOTMENT CA ON (R.COMPLAINTID = CA.COMPLAINTID) INNER JOIN COMPLAINT_AREA A ON (R.AREAID = A.AREAID)", "R.*", "CA.ALLOT_TO_NAME , A.AREANAME,cast(PREFERABLE_TIME as time) PREF_TIME_FROM,cast(PREFERABLE_TIME_TO as time)PREF_TIME_TO,PREFERABLE_DATE as PREF_DATE, CA.REMARK, isnull(CA.REALLOTMENTREMARK,'')AS REALLOTMENTREMARK", "R.COMPLAINTID=" + complaintno, "");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtcomplaintsdt.Text = ds.Tables[0].Rows[0]["COMPLAINT"].ToString().Trim();
                txtcomplaintpwd.Text = ds.Tables[0].Rows[0]["PWID"].ToString().Trim();

                lblComplainteeName.Text = ds.Tables[0].Rows[0]["COMPLAINTEE_NAME"].ToString().Trim();
                lblCompNo.Text = ds.Tables[0].Rows[0]["COMPLAINTNO"].ToString().Trim();
                lblLocation.Text = ds.Tables[0].Rows[0]["COMPLAINTEE_ADDRESS"].ToString() + " --> " + ds.Tables[0].Rows[0]["AREANAME"].ToString(); ;
                lblContactNo.Text = ds.Tables[0].Rows[0]["COMPLAINTEE_PHONENO"].ToString();
                txtWorkAllotedTo.Text = ds.Tables[0].Rows[0]["ALLOT_TO_NAME"].ToString();
                ddlRMCompnature.SelectedValue = ds.Tables[0].Rows[0]["TYPEID"].ToString();
                //txtComplaintDt.Text = ds.Tables[0].Rows[0]["PREF_DATE"].ToString().Trim();
                //txtPerferTime.Text = ds.Tables[0].Rows[0]["PREF_TIME_FROM"].ToString().Trim();
                //txtPerferTo.Text = ds.Tables[0].Rows[0]["PREF_TIME_TO"].ToString().Trim();
                //////txtPerferTime.Text = "10:00 AM";
                //////txtPerferTo.Text = "06:00 PM";
                txtWorkoutDate.Text = ds.Tables[0].Rows[0]["COMPLAINTDATE"].ToString().Trim();
                lblRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString().Trim();
                txtReallotment.Text = ds.Tables[0].Rows[0]["REALLOTMENTREMARK"].ToString();

            }
            ds = objCommon.FillDropDown("COMPLAINT_WORKOUT", "*", "", "COMPLAINTID =" + complaintno, "COMPLAINTID");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlRMCompnature.SelectedValue = ds.Tables[0].Rows[0]["COMPNATURE_ID"].ToString();
                txtDetails.Text = ds.Tables[0].Rows[0]["WORKOUT"].ToString().Trim();
                txtWorkAllotedTo.Text = ds.Tables[0].Rows[0]["ALLOTED_TO"].ToString().Trim();
            }
            DataSet dss = objCommon.FillDropDown("COMPLAINT_WORKOUTDETAILS W INNER JOIN COMPLAINT_ITEMMASTER I ON (W.ITEMID=I.ITEMID)", "W.CWID, I.ITEMNAME, W.ITEMID", "W.ITEMUNIT,W.QTYISSUED, ISNULL(W.IS_AVAILABLE,0) IS_AVAILABLE ", "W.COMPLAINTID =" + complaintno, "W.COMPLAINTID");
            lvitems.DataSource = dss;
            lvitems.DataBind();
            Session["DtItemList"] = dss.Tables[0];
            //Added Nancy Sharma to fetch time in AM/PM format
            DataSet dsT = objCommon.FillDropDown("COMPLAINT_REGISTER", "Format(PREFERABLE_TIME, 'hh:mm tt') AS PREFERABLE_TIME,FORMAT(PREFERABLE_DATE,'dd-MM-yy') as PREFERABLE_DATE ", "Format(PREFERABLE_TIME_TO,'hh:mm tt')AS PREFERABLE_TIME_TO", "COMPLAINTID=" + complaintno, "");
            {
                txtPerferTime.Text = dsT.Tables[0].Rows[0]["PREFERABLE_TIME"].ToString().Trim();
                txtPerferTo.Text = dsT.Tables[0].Rows[0]["PREFERABLE_TIME_TO"].ToString().Trim();
                txtComplaintDt.Text = dsT.Tables[0].Rows[0]["PREFERABLE_DATE"].ToString().Trim();
            }

            //empid = Convert.ToInt32(objCommon.LookUp("COMPLAINT_ALLOTMENT", "EMPID", "COMPLAINTID=" + complaintno));
            //lblEmployee.Text = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_NO=" + empid);
            //  ds = objCommon.FillDropDown("COMPLAINT_REGISTER CR", "CR.COMPLAINTID, CR.COMPLAINTEE_ADDRESS, CR.COMPLAINTEE_PHONENO", "", "CR.COMPLAINTID=" + complaintno, "");
            // lblAllotterName.Text = ds.Tables[0].Rows[0]["UA_FULLNAME"].ToString();

            // BindListViewWorkoutDetails(empid, complaintno);
            //pnlworkout.Visible = true;
            ////Pnlbutton.Visible = true;
            //pnlworkoutdetails.Visible = true;
            //pnlList.Visible = false;
            //chkAddItem.Checked = false;
            //rdocomp2.Checked = true;

            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Estate_Complaint_Workout.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int compno)
    {
        DataSet ds = objCommon.FillDropDown("COMPLAINT_REGISTER R INNER JOIN CELL_COMPLAINT_ALLOTMENT CA ON (R.COMPLAINTID = CA.COMPLAINTID) INNER JOIN COMPLAINT_AREA A ON (R.AREAID = A.AREAID) INNER JOIN  COMPLAINT_DETAIL CD ON (R.COMPLAINTID = CD.COMPLAINTID)",   
            "R.*", "CA.ALLOT_TO_NAME , A.AREANAME,cast(PREFERABLE_TIME as time) PREF_TIME_FROM,cast(PREFERABLE_TIME_TO as time)PREF_TIME_TO,PREFERABLE_DATE as PREF_DATE, CA.REMARK,isnull(CA.REALLOTMENTREMARK,'')AS REALLOTMENTREMARK, (CD.REMARK) AS RESPONSE,CD.COMPLAINT_STATUS", "R.COMPLAINTID=" + compno +"  AND CD.EMPLOYEE_UA_NO IS NOT NULL"   , "");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtcomplaintsdt.Text = ds.Tables[0].Rows[0]["COMPLAINT"].ToString().Trim();
                txtcomplaintpwd.Text = ds.Tables[0].Rows[0]["PWID"].ToString().Trim();

                lblComplainteeName.Text = ds.Tables[0].Rows[0]["COMPLAINTEE_NAME"].ToString().Trim();
                lblCompNo.Text = ds.Tables[0].Rows[0]["COMPLAINTNO"].ToString().Trim();
                lblLocation.Text = ds.Tables[0].Rows[0]["COMPLAINTEE_ADDRESS"].ToString() + " --> " + ds.Tables[0].Rows[0]["AREANAME"].ToString(); ;
                lblContactNo.Text = ds.Tables[0].Rows[0]["COMPLAINTEE_PHONENO"].ToString();
                txtWorkAllotedTo.Text = ds.Tables[0].Rows[0]["ALLOT_TO_NAME"].ToString();
                ddlRMCompnature.SelectedValue = ds.Tables[0].Rows[0]["TYPEID"].ToString();
                //txtComplaintDt.Text = ds.Tables[0].Rows[0]["PREF_DATE"].ToString().Trim();
                //txtPerferTime.Text = ds.Tables[0].Rows[0]["PREF_TIME_FROM"].ToString().Trim();
                //txtPerferTo.Text = ds.Tables[0].Rows[0]["PREF_TIME_TO"].ToString().Trim();
                //////txtPerferTime.Text = "10:00 AM";
                //////txtPerferTo.Text = "06:00 PM";
                txtWorkoutDate.Text = ds.Tables[0].Rows[0]["COMPLAINTDATE"].ToString().Trim();
                lblRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString().Trim();
                txtReallotment.Text = ds.Tables[0].Rows[0]["REALLOTMENTREMARK"].ToString();

                if (ds.Tables[0].Rows[0]["COMPLAINT_STATUS"].ToString() == "R")
                {
                    divReply.Visible = true;
                    txtReply.Text = ds.Tables[0].Rows[0]["RESPONSE"].ToString();
                    divAction.Visible = true;
                    txtRework.Visible = true;
                    divReallotment.Visible = true;
                }
                else
                {
                    divReply.Visible = false;
                    txtReply.Visible = false;
                    divAction.Visible = false;
                    txtRework.Visible = false;
                    divReallotment.Visible = false;
                }

            }
        SqlDataReader dr = objCC.Getworkout(compno);

        //to show created user details     
        if (dr != null)
        {
            DivAttch.Visible = true;
            listfile.Visible = true;
            if (dr.Read())
            {

                //CalendarExtender1.SelectedDate = Convert.ToDateTime(dr["WORKDATE"].ToString());
                txtDetails.Text = dr["WORKOUT"].ToString();
                string compstatus = objCommon.LookUp("COMPLAINT_REGISTER", "COMPLAINTSTATUS", "COMPLAINTID=" + compno);

              
          

                    if (compstatus == "I")
                    {
                        rdocomp1.Checked = true;
                        rdocomp2.Checked = false;
                    }
                    else if (compstatus == "C")
                    {
                        rdocomp2.Checked = true;
                    }
                
               
            }
            path = Docpath + "COMPLAINTS\\ComplaintsFiles\\ComplaintNo_" + ViewState["compl"].ToString().Replace("/", "-") + "\\RequestWorkoutDetails";//ADDED  BY VIJAY ON 24-02-2020
            BindListViewFiles(path);//ADDED  BY VIJAY ON 24-02-2020
        }

        if (dr != null) dr.Close();
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("Workout Report", "rptWorkoutReport.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Complaints")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Complaints," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_COMPLAINTID=" + ViewState["complaintno"].ToString().Trim();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            foreach (ListViewItem i in lvitems.Items)
            {
                CheckBox chkItem = (CheckBox)i.FindControl("chkItem");
                if (chkItem.Checked == false)
                {
                    count = 1;
                }
            }


            //btnSave.Enabled = false;
            objWorkedOut.Deptid = Convert.ToInt32(objCommon.LookUp("CELL_COMPLAINT_ALLOTMENT", "DEPTID", "COMPLAINTID=" + Convert.ToInt32(ViewState["complaintno"])));
            objWorkedOut.ComplaintId = Convert.ToInt32(ViewState["complaintno"]);
           // objWorkedOut.WorkDate = Convert.ToDateTime(txtWorkoutDate.Text);
            objWorkedOut.WorkDate = Convert.ToDateTime(Convert.ToString(DateTime.Now));
            objWorkedOut.WorkOut = txtDetails.Text.Trim();
            objWorkedOut.EmpId = Convert.ToInt32(Session["userno"].ToString().Trim());
            objWorkedOut.CompNatureId = Convert.ToInt32(ddlRMCompnature.SelectedValue);
            objWorkedOut.AllotedTo = txtWorkAllotedTo.Text.Trim();

            if (rdocomp1.Checked == true)
            {
                objWorkedOut.C_Status = "I";
            }
            else
            {
                if (txtDetails.Text.Trim().Length == 0)
                {
                    MessageBox("Please Enter Workout Details."); 
                    //objCommon.DisplayMessage(UpdatePanel1, "Please Enter Workout Details.", this.Page);
                    return;
                }
                else
                {
                    objWorkedOut.C_Status = "C";
                }
            }
            DataSet ds = objCommon.FillDropDown("COMPLAINT_WORKOUT", "CWID", "COMPLAINTID", "COMPLAINTID=" + Convert.ToInt32(ViewState["complaintno"]), "");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                objWorkedOut.CwId = Convert.ToInt32(ds.Tables[0].Rows[0]["CWID"].ToString().Trim());
            }
            else
            {
                objWorkedOut.CwId = 0;
            }


            if ((DataTable)Session["DtItemList"] != null)
            {
                DataTable dtItem;
                dtItem = (DataTable)Session["DtItemList"];
                objWorkedOut.ITEMLIST_DT = dtItem;
            }

            if (txtRework.Text != string.Empty)
            {
                objWorkedOut.REWORK = txtRework.Text;
            }
            else
            {
                objWorkedOut.REWORK = string.Empty;
            }

            CustomStatus cs = (CustomStatus)objCC.InsertComplaintWorkOut(objWorkedOut);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                string ReceiverEmialID = string.Empty;
                string messBody = string.Empty;
                string FromEmailID = string.Empty;
                string FromEmailPassword = string.Empty;

                if (objWorkedOut.C_Status.Equals("C"))
                {
                    DataSet dsReff = objCommon.FillDropDown("REFF", "IS_COMPLAINT_EMAIL", "EMAILSVCID, EMAILSVCPWD", "", "");
                    if (dsReff.Tables[0].Rows.Count > 0)
                    {
                        if (dsReff.Tables[0].Rows[0]["IS_COMPLAINT_EMAIL"].ToString() == "1")
                        {
                            FromEmailID = dsReff.Tables[0].Rows[0]["EMAILSVCID"].ToString();
                            FromEmailPassword = dsReff.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();

                            ds = objCommon.FillDropDown("COMPLAINT_REGISTER CR inner join user_acc UA on(cr.UA_NO =ua.UA_NO)", "ua.UA_IDNO,cr.COMPLAINT", "cr.COMPLAINTNO, UA_EMAIL", "CR.COMPLAINTID=" + Convert.ToInt16(ViewState["complaintno"].ToString()), "CR.COMPLAINTID");

                            if (ds != null && ds.Tables[0].Rows.Count > 0)
                            {
                                ReceiverEmialID = ds.Tables[0].Rows[0]["UA_EMAIL"].ToString();
                                messBody = "Your" + " " + "Service Request No. :-" + "  " + ds.Tables[0].Rows[0]["COMPLAINTNO"].ToString() + " " + "has been completed ,";
                                messBody += (@"<br /> ");
                                messBody += (@"<br /> ");
                                messBody += " Request Detail -" + ds.Tables[0].Rows[0]["complaint"].ToString() + "  ";
                                sendmail(FromEmailID, FromEmailPassword, ReceiverEmialID, "Service Request", messBody);
                            }
                        }
                    }
                }
                else if (rdocomp1.Checked == true && count != 0)
                {
                    objWorkedOut.C_Status = "I";
                    DataSet dsReff = objCommon.FillDropDown("REFF", "IS_COMPLAINT_EMAIL", "EMAILSVCID, EMAILSVCPWD", "", "");
                    if (dsReff.Tables[0].Rows.Count > 0)
                    {
                        if (dsReff.Tables[0].Rows[0]["IS_COMPLAINT_EMAIL"].ToString() == "1")
                        {
                            FromEmailID = dsReff.Tables[0].Rows[0]["EMAILSVCID"].ToString();
                            FromEmailPassword = dsReff.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();

                            ds = objCommon.FillDropDown("COMPLAINT_REGISTER CR inner join user_acc UA on(cr.UA_NO =ua.UA_NO)", "ua.UA_IDNO,cr.COMPLAINT", "cr.COMPLAINTNO, UA_EMAIL", "CR.COMPLAINTID=" + Convert.ToInt16(ViewState["complaintno"].ToString()), "CR.COMPLAINTID");
                            if (ds != null && ds.Tables[0].Rows.Count > 0)
                            {
                                ReceiverEmialID = ds.Tables[0].Rows[0]["UA_EMAIL"].ToString();
                                messBody = "Your" + " " + "Service No :-" + "  " + ds.Tables[0].Rows[0]["COMPLAINTNO"].ToString() + "  " + " And Service:-" + ds.Tables[0].Rows[0]["complaint"].ToString() + "  " + "has not completed";
                                sendmailToComplaintee(FromEmailID, FromEmailPassword, ReceiverEmialID, "Related To Service Request", messBody);
                            }
                        }
                    }
                }
                MessageBox("Record Saved Successfully."); 
                Clear();
                BindListViewComplaintAllotment(Convert.ToInt32(Session["userno"].ToString()));
            }
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Estate_Complaint_Workout.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
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

            //DataSet ds = objCommon.FillDropDown("FILE_FILEMASTER F INNER JOIN USER_ACC U ON (F.USERNO = U.UA_NO)", "F.FILE_ID, F.FILE_CODE, F.FILE_NAME, DESCRIPTION", "U.UA_FULLNAME, F.CREATION_DATE", "FILE_ID=" + Convert.ToInt32(ViewState["FILE_ID"]) + "", "");
            // DataSet ds = objCommon.FillDropDown("COMPLAINT_REGISTER CR INNER JOIN COMPLAINT_DEPARTMENT CD ON (CR.DEPTID = CD.DEPTID)", "CR.COMPLAINTNO, CR.COMPLAINTDATE, CD.DEPTNAME", "CR.PREFERABLE_DATE, CR.PREFERABLE_TIME", "CR.COMPLAINTID=" + Convert.ToInt32(ViewState["ComplaintId"]) + "", "");

            string MemberEmailId = string.Empty;
            mailMessage.From = new MailAddress(HttpUtility.HtmlEncode(fromEmailId));
            mailMessage.To.Add(toEmailId);

            var MailBody = new StringBuilder();
            MailBody.AppendFormat("Dear Sir, {0}\n", " ");
            MailBody.AppendLine(@"<br />" + body);
            MailBody.AppendLine(@"<br /> ");
            MailBody.AppendLine(@"<br /> ");
            MailBody.AppendLine(@"<br /> ");
            MailBody.AppendLine(@"<br />Thanks And Regards");
            MailBody.AppendLine(@"<br />" + Session["userfullname"].ToString());

            mailMessage.Body = MailBody.ToString();
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

    public void sendmailToComplaintee(string fromEmailId, string fromEmailPwd, string toEmailId, string Sub, string body)
    {
        try
        {
            string msg = string.Empty;
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = Sub;

            //DataSet ds = objCommon.FillDropDown("FILE_FILEMASTER F INNER JOIN USER_ACC U ON (F.USERNO = U.UA_NO)", "F.FILE_ID, F.FILE_CODE, F.FILE_NAME, DESCRIPTION", "U.UA_FULLNAME, F.CREATION_DATE", "FILE_ID=" + Convert.ToInt32(ViewState["FILE_ID"]) + "", "");
            DataSet ds = objCommon.FillDropDown("COMPLAINT_REGISTER CR INNER JOIN COMPLAINT_DEPARTMENT CD ON (CR.DEPTID = CD.DEPTID)", "CR.COMPLAINTNO, CR.COMPLAINTDATE, CD.DEPTNAME", "CR.PREFERABLE_DATE, CR.PREFERABLE_TIME", "CR.COMPLAINTID=" + Convert.ToInt32(ViewState["complaintno"]) + "", "");
            DataSet dsR = objCommon.FillDropDown("COMPLAINT_WORKOUT W INNER JOIN COMPLAINT_WORKOUTDETAILS WD ON (W.COMPLAINTID = WD.COMPLAINTID) INNER JOIN COMPLAINT_ITEMMASTER I ON (WD.ITEMID = I.ITEMID)", "I.ITEMNAME, WD.ITEMUNIT ", "WD.QTYISSUED ", "WD.IS_AVAILABLE = 0 AND WD.COMPLAINTID=" + Convert.ToInt32(ViewState["complaintno"]) + "", "");

            string MemberEmailId = string.Empty;
            mailMessage.From = new MailAddress(HttpUtility.HtmlEncode(fromEmailId));
            mailMessage.To.Add(toEmailId);

            var MailBody = new StringBuilder();
            MailBody.AppendFormat("Dear Sir, As per your Service Request.no.{0}\n", " ");
            MailBody.AppendLine(@"<br />" + ds.Tables[0].Rows[0]["COMPLAINTNO"]);
            MailBody.AppendLine(@"<br /> on date : " + Convert.ToDateTime(ds.Tables[0].Rows[0]["COMPLAINTDATE"]).ToString("dd/MM/yyyy"));
            MailBody.AppendLine(@"<br /> Department : " + ds.Tables[0].Rows[0]["DEPTNAME"]);
            MailBody.AppendLine(@"<br /> ");
            MailBody.AppendLine(@"<br /> ");
            MailBody.AppendLine(@"<br /> The following items are not available at present.");
            MailBody.AppendLine(@"<br /> Thank You. ");


            mailMessage.Body = MailBody.ToString();
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

    private void FillComplaintNature(Int32 userno)
    {
        try
        {
            DataSet ds = objCC.GetComplaintNature(userno);
            ddlRMCompnature.DataSource = ds;
            ddlRMCompnature.DataValueField = ds.Tables[0].Columns["TYPEID"].ToString();
            ddlRMCompnature.DataTextField = ds.Tables[0].Columns["TYPENAME"].ToString();
            ddlRMCompnature.DataBind();
            ddlRMCompnature.SelectedIndex = -1;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Estate_Complaint_Allotment.FillComplaintNature-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();

    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    // clear method
    private void Clear()
    {

        //  btnSave.Enabled = true;
        lblCompNo.Text = string.Empty;
        lblComplainteeName.Text = string.Empty;
        lblContactNo.Text = string.Empty;
        lblLocation.Text = string.Empty;
        lblRemark.Text = string.Empty;
        txtDetails.Text = string.Empty;
        txtWorkAllotedTo.Text = string.Empty;
        txtcomplaintsdt.Text = string.Empty;
        txtcomplaintpwd.Text = string.Empty;
        txtWorkAllotedTo.Text = string.Empty;
        ddlRMCompnature.SelectedIndex = 0;
        lvitems.DataSource = null;
        lvitems.DataBind();
        //ddlRMAllotedto.SelectedIndex = 0;
        //ddlRMCompnature.SelectedIndex = 0;   
        lblItemUnit.Text = string.Empty;

        chkAvailableItem.Checked = false;
        txtWorkoutDate.Text = string.Empty;
        txtComplaintDt.Text = string.Empty;
        txtPerferTime.Text = string.Empty;
        listfile.Visible = false;
        lvfile.DataSource = null;
        lvfile.DataBind();
        DivAttch.Visible = false;
        txtPerferTo.Text = string.Empty;
        divAction.Visible = false;
        txtRework.Text = string.Empty;
        divReply.Visible = false;
        txtReply.Text = string.Empty;

    }

    //protected void dpPager_PreRender(object sender, EventArgs e)
    //{
    //    //Bind the ListView with Domain
    //    BindListViewComplaintAllotment(Convert.ToInt32(Session["userno"].ToString()));
    //}

    public void Showmessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
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

    #region  This is for Add Items in DataTable list
    private DataTable CreateTabel()
    {
        DataTable dtItem = new DataTable();
        dtItem.Columns.Add(new DataColumn("CWID", typeof(decimal)));
        dtItem.Columns.Add(new DataColumn("ITEMNAME", typeof(string)));
        dtItem.Columns.Add(new DataColumn("ITEMID", typeof(decimal)));
        dtItem.Columns.Add(new DataColumn("ITEMUNIT", typeof(string)));
        dtItem.Columns.Add(new DataColumn("QTYISSUED", typeof(decimal)));
        dtItem.Columns.Add(new DataColumn("IS_AVAILABLE", typeof(int)));
        return dtItem;
    }


    protected void btnAdd_Click1(object sender, EventArgs e)
    {
        try
        {
            lvitems.Visible = true;
            if (Session["DtItemList"] != null && ((DataTable)Session["DtItemList"]) != null)
            {
                DataTable dtItem = (DataTable)Session["DtItemList"];
                DataRow dr = dtItem.NewRow();
                if (CheckDuplicateSectionName(dtItem, ddlitemlist.SelectedItem.Text))
                {
                    ClearRec();
                    Showmessage("This Item Name Already Exist.");
                    return;
                }
                dr["CWID"] = Convert.ToInt32(ViewState["CWID"]) + 1;
                dr["ITEMNAME"] = ddlitemlist.SelectedItem.Text == null ? string.Empty : ddlitemlist.SelectedItem.Text;
                dr["ITEMID"] = Convert.ToDecimal(ddlitemlist.SelectedValue == null ? string.Empty : ddlitemlist.SelectedValue);
                dr["ITEMUNIT"] = lblItemUnit.Text;
                dr["QTYISSUED"] = txtItemQuantity.Text.Trim();
                dr["IS_AVAILABLE"] = chkItem.Checked == true ? 1 : 0;



                dtItem.Rows.Add(dr);
                Session["DtItemList"] = dtItem;
                lvitems.DataSource = dtItem;
                lvitems.DataBind();
                ClearRec();
                ViewState["CWID"] = Convert.ToInt32(ViewState["CWID"]) + 1;
            }
            else
            {

                DataTable dtItem = this.CreateTabel();
                DataRow dr = dtItem.NewRow();

                dr["CWID"] = Convert.ToInt32(ViewState["CWID"]) + 1;
                dr["ITEMNAME"] = ddlitemlist.SelectedItem.Text == null ? string.Empty : ddlitemlist.SelectedItem.Text;
                dr["ITEMID"] = Convert.ToDecimal(ddlitemlist.SelectedValue == null ? string.Empty : ddlitemlist.SelectedValue);
                dr["ITEMUNIT"] = lblItemUnit.Text;
                dr["QTYISSUED"] = txtItemQuantity.Text.Trim();
                dr["IS_AVAILABLE"] = chkItem.Checked == true ? 1 : 0;

                ViewState["CWID"] = Convert.ToInt32(ViewState["CWID"]) + 1;
                dtItem.Rows.Add(dr);
                ClearRec();
                Session["DtItemList"] = dtItem;
                lvitems.DataSource = dtItem;
                lvitems.DataBind();
            }
            ShowDetails(Convert.ToInt32(ViewState["complaintno"]));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Estate_Complaint_Allotment .btnAdd_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private bool CheckDuplicateSectionName(DataTable dtItem, string value)
    {
        bool retVal = false;
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dtItem.Rows)
            {
                if (dr["ITEMID"].ToString() == value)
                {
                    datRow = dr;
                    retVal = true;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Estate_Complaint_Allotment.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return retVal;
    }

    protected void ClearRec()
    {
        ddlitemlist.SelectedIndex = 0;
        txtItemQuantity.Text = string.Empty;

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;

            //if (Session["DtItemList"] != null && ((DataTable)Session["DtItemList"]) != null)
            //{
            //  CustomStatus cs=(CustomStatus)objCC.DeleteComplaintWorkoutDetails(Convert.ToInt32(btnDelete.CommandArgument),1);
            //  if (cs.Equals(CustomStatus.RecordDeleted))
            //  {
            //      DataTable dtItem = (DataTable)Session["DtItemList"];
            //      dtItem.Rows.Remove(this.GetEditableDatarow(dtItem, btnDelete.CommandArgument));
            //      Session["DtItemList"] = dtItem;
            //      lvitems.DataSource = dtItem;
            //      lvitems.DataBind();
            //      lvitems.Visible = true;
            //  }
            //  else
            //  {
            //      DataTable dtItem = (DataTable)Session["DtItemList"];
            //      Session["DtItemList"] = dtItem;
            //      lvitems.DataSource = dtItem;
            //      lvitems.DataBind();
            //      lvitems.Visible = true;
            //  }
                
            //}


            if (Session["DtItemList"] != null && ((DataTable)Session["DtItemList"]) != null)
            {
                DataTable dtItem = (DataTable)Session["DtItemList"];
                dtItem.Rows.Remove(this.GetEditableDatarow(dtItem, btnDelete.CommandArgument));
                Session["DtItemList"] = dtItem;
                if (dtItem.Rows.Count > 0)
                {
                    lvitems.DataSource = dtItem;
                    lvitems.DataBind();
                }
                else
                {
                    lvitems.DataSource = null;
                    lvitems.DataBind();
                    lvitems.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["errror"]) == true)
                objCommon.ShowError(Page, "Estate_Complaint_Allotment.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private DataRow GetEditableDatarow(DataTable dtItem, string value)
    {

        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dtItem.Rows)
            {
                if (dr["CWID"].ToString() == value)
                {
                    datRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Estate_Complaint_Allotment.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return datRow;
    }

    protected void ddlitemlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ShowDetails(Convert.ToInt32(ViewState["complaintno"]));
            DataSet ds = objCommon.FillDropDown("COMPLAINT_ITEMMASTER", "ITEMID", "ITEMUNIT", "ITEMID=" + Convert.ToInt32(ddlitemlist.SelectedValue), "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblItemUnit.Text = ds.Tables[0].Rows[0]["ITEMUNIT"].ToString();
                ddlitemlist.Focus();
            }
            ddlitemlist.Focus();
            

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Estate_Complaint_Allotment.ddlitemlist_SelectedIndexChanged -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #endregion

    protected void btnEditItem_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEditRec = sender as ImageButton;
            DataTable dt;
            if (Session["DtItemList"] != null && ((DataTable)Session["DtItemList"]) != null)
            {
                dt = ((DataTable)Session["DtItemList"]);
                ViewState["EDIT_SRNO"] = btnEditRec.CommandArgument;
                DataRow dr = this.GetEditableDatarow(dt, btnEditRec.CommandArgument);
                ddlitemlist.SelectedValue = dr["ITEMID"].ToString();
                lblItemUnit.Text = dr["ITEMUNIT"].ToString();
                txtItemQuantity.Text = dr["QTYISSUED"].ToString();
                if (dr["QTYISSUED"].ToString() == "0")
                {
                    chkItem.Checked = false;
                }
                else
                {
                    chkItem.Checked = true;
                }
                dt.Rows.Remove(dr);
                Session["DtItemList"] = dt;
                lvitems.DataSource = dt;
                lvitems.DataBind();
                ViewState["actionContent"] = "edit";
               
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["erreor"]) == true)
                objCommon.ShowError(Page, "Health_LaboratoryTest_TestContent.btnEditRec_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }



    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            Button btnPrint = sender as Button;
            ViewState["COMPLAINTID"] = int.Parse(btnPrint.CommandArgument);
            ShowServiceReport("ComplaintRegister", "rptServiceRequestRegister.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Estt_complaint.btnPrint_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowServiceReport(string reportTitle, string rptFileName)
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
           // ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }



    public bool FileTypeValid(string FileExtention)
    {
        bool retVal = false;
        string[] Ext = { ".jpg", ".JPG", ".bmp", ".BMP", ".gif", ".GIF", ".png", ".PNG", ".pdf", ".PDF", ".xls", ".XLS", ".doc", ".DOC", ".zip", ".ZIP", ".txt", ".TXT", ".docx", ".DOCX", ".XLSX", ".xlsx", ".rar", ".RAR" };
        foreach (string ValidExt in Ext)
        {
            if (FileExtention == ValidExt)
            {
                retVal = true;
            }
        }
        return retVal;
    }


    protected void btnAddfile_Click(object sender, EventArgs e)//ADDED  BY VIJAY ON 24-02-2020
    {

        try
        {
            if (FileUpload1.HasFile)
            {

                if (ViewState["compl"] != string.Empty)
                {
                    if (FileTypeValid(System.IO.Path.GetExtension(FileUpload1.FileName)))
                    {
                        string file = Docpath + "COMPLAINTS\\ComplaintsFiles\\ComplaintNo_" + ViewState["compl"].ToString().Replace("/", "-") + "\\RequestWorkoutDetails";

                        if (!System.IO.Directory.Exists(file))
                        {
                            System.IO.Directory.CreateDirectory(file);
                        }
                        ViewState["FILENAME"] = file;

                        path = file + "\\RequestWorkoutDetails_" + ViewState["compl"].ToString().Replace("/", "-") + "_" + FileUpload1.FileName;

                        dbPath = file;
                        string filename = FileUpload1.FileName;
                        ViewState["FILE_NAME"] = filename;

                        //CHECKING FOLDER EXISTS OR NOT file
                        HttpPostedFile chkFileSize = FileUpload1.PostedFile;
                        if (chkFileSize.ContentLength <= 102400) // For Allowing 100 Kb Size Files only 
                        {
                            if (ViewState["compl"] != "")
                            {
                                if (!System.IO.Directory.Exists(path))
                                {
                                    FileUpload1.PostedFile.SaveAs(path);
                                }
                            }
                            BindListViewFiles(file);
                            ShowDetails(Convert.ToInt32(ViewState["complaintno"]));
                        }
                        else
                        {
                            objCommon.DisplayMessage(this, "File size should not exceed 100 Kb.", this.Page);
                        }
                    }

                    else
                    {
                        MessageBox("Please Upload Valid Files[.jpg,.pdf,.xls,.doc,.txt,.zip]");
                        //objCommon.DisplayMessage(UpdatePanel1, "Please Upload Valid Files[.jpg,.pdf,.xls,.doc,.txt,.zip]", this.Page);
                        FileUpload1.Focus();
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this, "Plese enter complaint no.", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(this, "Please Select File", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Estt_complaint.btAdd_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindListViewFiles(string PATH)//ADDED  BY VIJAY ON 24-02-2020
    {
        try
        {
            DivAttch.Visible = true;
            listfile.Visible = true;
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
    protected string GetFileName(object obj)//ADDED  BY VIJAY ON 24-02-2020
    {
        string f_name = string.Empty;
        string[] fname = obj.ToString().Split('_');

        if (fname[0] == "RequestWorkoutDetails")
            f_name = Convert.ToString(fname[2]);

        if (fname[0] == "judDoc")
            f_name = Convert.ToString(fname[3]);

        return f_name;
    }
    protected string GetFileNameCaseNo(object obj)//ADDED  BY VIJAY ON 24-02-2020
    {
        string f_name = string.Empty;
        ViewState["compl"].ToString().Replace('/', '-');
        string[] fname = obj.ToString().Split('_');

        if (fname[1] == ViewState["compl"].ToString().Replace('/', '-'))
            f_name = Convert.ToString(fname[1]);
        f_name = f_name.Replace('-', '/');
        if (fname[0] == "judDoc")
            f_name = Convert.ToString(fname[3]);
        return f_name;
    }
    protected string GetFileDate(object obj)//ADDED  BY VIJAY ON 24-02-2020
    {
        string file_path = Convert.ToString(ViewState["FILE_PATH"] + "\\" + obj.ToString());
        FileInfo fileInfo = new FileInfo(file_path);

        DateTime creationTime = DateTime.MinValue;
        creationTime = fileInfo.CreationTime;

        string f_date = string.Empty;
        //f_date = creationTime.ToString("dd-MMM-yyyy");
        f_date = DateTime.Today.ToString("dd-MMM-yyyy");
        return f_date;
    }
    protected void btnAttachDelete_Click(object sender, ImageClickEventArgs e)//ADDED  BY VIJAY ON 24-02-2020
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            string case_entry_no = btnDelete.CommandArgument;
            string[] fname1 = case_entry_no.ToString().Split('_');

            path = Docpath + "COMPLAINTS\\ComplaintsFiles\\ComplaintNo_" + ViewState["compl"].ToString().Replace("/", "-") + "\\RequestWorkoutDetails";

            string fname = btnDelete.CommandArgument;


            if (File.Exists(path + "\\" + fname))
            {
                //DELETING THE FILE
                File.Delete(path + "\\" + fname);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('File Deleted  !!');", true);
            }
            BindListViewFiles(path);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Estt_complaint.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }



    }
    protected void imgFileDownload_Click(object sender, ImageClickEventArgs e)//ADDED  BY VIJAY ON 24-02-2020
    {
        ImageButton btn = sender as ImageButton;
        DownloadFile(btn.AlternateText);
    }
    public void DownloadFile(string filePath)//ADDED  BY VIJAY ON 24-02-2020
    {
        try
        {
            string[] fname1 = filePath.ToString().Split('_');
            string filename = filePath.Substring(filePath.LastIndexOf("_") + 1);
            path = Docpath + "COMPLAINTS\\ComplaintsFiles\\ComplaintNo_" + ViewState["compl"].ToString().Replace("/", "-") + "\\RequestWorkoutDetails";
            FileStream sourceFile = new FileStream((path + "\\" + filePath), FileMode.Open);
            long fileSize = sourceFile.Length;
            byte[] getContent = new byte[(int)fileSize];
            sourceFile.Read(getContent, 0, (int)sourceFile.Length);
            sourceFile.Close();

            Response.Clear();
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
    private string GetResponseType(string fileExtension)//ADDED  BY VIJAY ON 24-02-2020
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
    protected void btnCancel_Click(object sender, EventArgs e)
    
    {
        Response.Redirect(Request.Url.ToString());
    }
}
