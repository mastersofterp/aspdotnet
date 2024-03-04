using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Net;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;

public partial class ESTABLISHMENT_LEAVES_Transactions_BulkLeaveApproval : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objApp = new LeavesController();
    Leaves objLM = new Leaves();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                pnlAdd.Visible = false;
                pnlvedit.Visible = false;
                pnllist.Visible = true;
                int usernock = Convert.ToInt32(Session["userno"]);
                BindLVLeaveApplPendingList();
                btnHidePanel.Visible = false;
                trfrmto.Visible = false;
                trbutshow.Visible = false;
                txtFromdt.Text = System.DateTime.Now.ToString();
                txtTodt.Text = System.DateTime.Now.ToString();
                ViewState["ModifyLeave"] = "add";


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
                Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
        }
    }
    protected void BindLVLeaveApplPendingList()
    {
        try
        {
            DataSet ds = objApp.GetPendListforLeaveApproval(Convert.ToInt32(Session["userno"]));
            if (ds.Tables[0].Rows.Count <= 0)
            {
                //dpPager.Visible = false;
            }
            else
            {
                //dpPager.Visible = true;
            }
            lvPendingList.DataSource = ds;
            lvPendingList.DataBind();
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.BindLVLeaveApplPendingList ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }

    private void SendSMSOLD(int letrno)
    {

        ////========================
        //  int pano = Convert.ToInt32(objCommon.LookUp("payroll_leave_app_pass_entry", "pano", "letrno=" + LETRNO + " And status='P'"));
        //  int uano = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_PASSING_AUTHORITY", "ua_no", "pano=" + pano));
        //  string mob_no = objCommon.LookUp("user_acc", "ua_mobile", "ua_no=" + uano);

        //  int idno = Convert.ToInt32(objCommon.LookUp("payroll_leave_app_entry", "EMPNO", "LETRNO=" + LETRNO));
        //  string name = objCommon.LookUp("payroll_empmas", "fname+' '+mname+' '+ lname as name", "idno=" + idno);
        //  string url = "http://smsnmms.co.in/sms.aspx";
        //  string uid = string.Empty; string pass = string.Empty; string mobno = string.Empty;
        //  string message = "Leave has been apply by " + name;
        //  DataSet dsReff = objCommon.FillDropDown("REFF", "SMSSVCID", "SMSSVCPWD", "", "");
        //  if (dsReff.Tables[0].Rows.Count > 0)
        //  {
        //      uid = dsReff.Tables[0].Rows[0]["SMSSVCID"].ToString();
        //      pass = dsReff.Tables[0].Rows[0]["SMSSVCPWD"].ToString();
        //  }
        //  mobno = mob_no;
        //  WebRequest request = HttpWebRequest.Create("" + url + "?ID=" + uid + "&PWD=" + pass + "&PHNO=" + mobno + "&TEXT=" + message + "");
        //  WebResponse response = request.GetResponse();
        //  StreamReader reader = new StreamReader(response.GetResponseStream());
        //  string urlText = reader.ReadToEnd();
        //  //==================================
        string url = "http://smsnmms.co.in/sms.aspx";
        string uid = string.Empty; string pass = string.Empty; string mobno = string.Empty; string message, Mobile_no = string.Empty;
        bool isSMS = Convert.ToBoolean(objCommon.LookUp("payroll_leave_ref", "isnull(isSMS,0)as isSMS", ""));
        if (isSMS == true)
        {
            DataSet dsReff = objCommon.FillDropDown("REFF", "SMSSVCID", "SMSSVCPWD", "", "");
            if (dsReff.Tables[0].Rows.Count > 0)
            {
                uid = dsReff.Tables[0].Rows[0]["SMSSVCID"].ToString();
                pass = dsReff.Tables[0].Rows[0]["SMSSVCPWD"].ToString();
            }
            objLM.LETRNO = letrno;
            DataSet ds = objApp.GetSMSInformation(objLM);

            if (ds.Tables[0].Rows.Count > 0)
            {

                string leavestatus = ds.Tables[0].Rows[0]["LeaveStatus"].ToString();
                string name = ds.Tables[0].Rows[0]["name"].ToString();
                string leavename = ds.Tables[0].Rows[0]["Leave_Name"].ToString();
                double tot_days = Convert.ToDouble(ds.Tables[0].Rows[0]["NO_OF_DAYS"].ToString());
                string Joindt = ds.Tables[0].Rows[0]["Joindt"].ToString();
                string PHONENO = ds.Tables[0].Rows[0]["PHONENO"].ToString();
                //Full day/half day [casual] Leave Application Submitted By ['ABC'] for [2] days & Joining Date is [Joindt]
                message = leavestatus + leavename + " Application Submitted By " + name + " For " + tot_days + " days & Joining Date is " + Joindt;
                WebRequest request = HttpWebRequest.Create("" + url + "?ID=" + uid + "&PWD=" + pass + "&PHNO=" + PHONENO + "&TEXT=" + message + "");
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string urlText = reader.ReadToEnd();
            }
        }
    }
    //protected void dpPager_PreRender(object sender, EventArgs e)
    //{

    //    BindLVLeaveApplPendingList();
    //}
    private void clear()
    {
        txtRemarks.Text = string.Empty;
        ddlSelect.SelectedIndex = 0;
        ViewState["EMPNO"] = ViewState["COLLEGE_NO"] = ViewState["STNO"] = null;
        ViewState["LNO"] = null;
        //ViewState["action"] = null;
        //ViewState["ModifyLeave"] = null;
        divAuthorityList.Visible = false;
        ViewState["IsPayLeave"] = null;
        pnlButton.Visible = false;


        lvDownload.DataBind();
        lvDownload.DataSource = null;
    }
    private void clear_lblvalue()
    {
        lblEmpName.Text = string.Empty;
        lblLeaveName.Text = string.Empty;
        lblFromdt.Text = string.Empty;
        lblTodt.Text = string.Empty;
        lblNodays.Text = string.Empty;
        lblJoindt.Text = string.Empty;
        lblReason.Text = string.Empty;
        lblNoon.Text = string.Empty;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        pnlAdd.Visible = false;
        pnlvedit.Visible = false;
        pnllist.Visible = true;
        Tr1.Visible = true;
        lnkbut.Visible = true;
        ViewState["action"] = null;
        clear_lblvalue();
        clear();
    }
    protected void rblleavetype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rblleavetype.SelectedValue == "1")//half day
            {
                //ddlLeaveFNAN.Visible = true;
                //tdHalfCriteria.Visible = true; tdHalfCriteriaLable.Visible = tdHalfCriteriaLable1.Visible = true;
                //tdHalfCriteriaLable1.Visible = tdHalfCriteriaLable2.Visible = tdHalfCriteriaLable3.Visible = true;
                divHalfCriteria.Visible = true;
                int calholy = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE", "CAL_HOLIDAYS", "LNO=" + Convert.ToInt32(ViewState["LNO"])));
                int stno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "STNO", "IDNO=" + Convert.ToInt32(Session["idno"])));
                if (txttodate.Text.ToString() != string.Empty && txttodate.Text.ToString() != "__/__/____" && txtfrmdt.Text.ToString() != string.Empty && txtfrmdt.Text.ToString() != "__/__/____")
                {
                    // DataSet ds = objApp.GetNoofdays(Convert.ToDateTime(txtFromdt.Text), Convert.ToDateTime(txtTodt.Text), rblleavetype.SelectedValue, Convert.ToInt32(ddlNoon.SelectedValue), stno, calholy);
                    DataSet ds = objApp.GetNoofdays(Convert.ToDateTime(txtfrmdt.Text), Convert.ToDateTime(txttodate.Text), rblleavetype.SelectedValue, stno, calholy, Convert.ToInt32(ViewState["COLLEGE_NO"]), Convert.ToInt32(ddlLeaveFNAN.SelectedValue));

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //txttodate,txtTodt,txtfrmdt,txtFromdt
                        double no_of_days = Convert.ToDouble(ds.Tables[0].Rows[0]["TOT_DAYS"]);
                        txtNodays.Text = no_of_days.ToString();
                        txtJoindt.Text = Convert.ToString(ds.Tables[0].Rows[0]["JOINDT"]);
                    }
                }
            }
            else
            {
                //tdHalfCriteria.Visible = false; tdHalfCriteriaLable.Visible = tdHalfCriteriaLable1.Visible = false;
                //tdHalfCriteriaLable1.Visible = tdHalfCriteriaLable2.Visible = tdHalfCriteriaLable3.Visible = false;
                divHalfCriteria.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Application.txtTodt_TextChanged->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }
    protected void ddlLeaveFNAN_SelectedIndexChanged(object sender, EventArgs e)
    {
        int calholy = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE", "CAL_HOLIDAYS", "LNO=" + Convert.ToInt32(ViewState["LNO"])));
        //int stno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "STNO", "IDNO=" + Convert.ToInt32(Session["idno"])));
        int stno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "STNO", "IDNO=" + Convert.ToInt32(ViewState["EMPNO"])));

        if (txttodate.Text.ToString() != string.Empty && txttodate.Text.ToString() != "__/__/____" && txtfrmdt.Text.ToString() != string.Empty && txtfrmdt.Text.ToString() != "__/__/____")
        {
            // DataSet ds = objApp.GetNoofdays(Convert.ToDateTime(txtFromdt.Text), Convert.ToDateTime(txtTodt.Text), rblleavetype.SelectedValue, Convert.ToInt32(ddlNoon.SelectedValue), stno, calholy);
            DataSet ds = objApp.GetNoofdays(Convert.ToDateTime(txtfrmdt.Text), Convert.ToDateTime(txttodate.Text), rblleavetype.SelectedValue, stno, calholy, Convert.ToInt32(ViewState["COLLEGE_NO"]), Convert.ToInt32(ddlLeaveFNAN.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0)
            {
                //txttodate,txtTodt,txtfrmdt,txtFromdt
                double no_of_days = Convert.ToDouble(ds.Tables[0].Rows[0]["TOT_DAYS"]);
                txtNodays.Text = no_of_days.ToString();
                txtJoindt.Text = Convert.ToString(ds.Tables[0].Rows[0]["JOINDT"]);
            }
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //Leaves objLM = new Leaves();
            int LETRNO = Convert.ToInt32(ViewState["LETRNO"].ToString());
            int UA_NO = Convert.ToInt32(Session["userno"]);
            string Status = ddlSelect.SelectedValue.ToString();
            if (Status == "Forward To Next Authority(Recommended)".ToString().Trim())
            {
                Status = "F".ToString().Trim();
            }
            else if (Status == "Approve & Final Submit".ToString().Trim())
            {
                Status = "A".ToString().Trim();
            }
            else if (Status == "Reject".ToString().Trim())
            {
                Status = "R".ToString().Trim();
            }
            objLM.STATUS = Status;
            string Remarks = txtRemarks.Text.ToString();
            DateTime Aprdate = Convert.ToDateTime(DateTime.Now.Date);
            bool isSMS = Convert.ToBoolean(objCommon.LookUp("payroll_leave_ref", "isnull(isSMS,0)as isSMS", ""));
            bool isEmail = Convert.ToBoolean(objCommon.LookUp("payroll_leave_ref", "isnull(isEmail,0)as isEmail", ""));

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("edit"))
                {
                    if (ViewState["ModifyLeave"] != null)
                    {
                        if (ViewState["ModifyLeave"].ToString().Equals("edit"))
                        {
                            Status = ddlSelectModify.SelectedValue.ToString();
                            if (Status == "Forward To Next Authority(Recommended)".ToString().Trim())
                            {
                                Status = "F".ToString().Trim();
                            }
                            else if (Status == "Approve & Final Submit".ToString().Trim())
                            {
                                Status = "A".ToString().Trim();
                            }
                            else if (Status == "Reject".ToString().Trim())
                            {
                                Status = "R".ToString().Trim();
                            }
                            objLM.STATUS = Status;
                            objLM.LETRNO = Convert.ToInt32(Session["LETRNO"]);
                            objLM.FROMDT = Convert.ToDateTime(txtfrmdt.Text);
                            objLM.TODT = Convert.ToDateTime(txttodate.Text);
                            objLM.NO_DAYS = Convert.ToDouble(txtNodays.Text);
                            objLM.JOINDT = Convert.ToDateTime(txtJoindt.Text);
                            if (rblleavetype.SelectedValue == "1")//half day
                            {
                                if (Convert.ToInt32(ddlLeaveFNAN.SelectedValue) == 1)
                                {
                                    objLM.LEAVEFNAN = true;
                                }
                                else
                                {
                                    objLM.LEAVEFNAN = false;
                                }
                                //
                                if (objLM.FROMDT != objLM.TODT)
                                {
                                    MessageBox("Please select Single date for Half Day");
                                    return;
                                }
                                else
                                {
                                    CustomStatus cs = (CustomStatus)objApp.UpdateAppPassEntry(LETRNO, UA_NO, Status, Remarks, Aprdate, 0);
                                    // SendMail(LETRNO);

                                }
                            }
                            else
                            {
                                objLM.LEAVEFNAN = null;
                                CustomStatus cs = (CustomStatus)objApp.UpdateAppPassEntry(LETRNO, UA_NO, Status, Remarks, Aprdate, 0);
                                SendMail(LETRNO);
                            }
                            if (divml.Visible == true && trType.Visible == false)
                            {
                                if (Convert.ToInt32(rdbml.SelectedValue) == 0)
                                {
                                    //full pay
                                    objLM.MLHPL = 1;
                                }
                                else
                                {
                                    //half pay
                                    objLM.MLHPL = 2;
                                }
                            }
                            else
                            {
                                objLM.MLHPL = 0;
                            }


                            //TO UPDATE LEAVE APPLICATION ENTRY

                            //decimal balleave = Convert.ToDecimal(lblbal.Text);



                            CustomStatus lcs = (CustomStatus)objApp.UpdateLeaveAppEntry(objLM);
                            if (lcs.Equals(CustomStatus.RecordUpdated))
                            {
                                MessageBox("Record Updated Successfully");
                                pnlAdd.Visible = false;
                                pnlvedit.Visible = false;
                                pnllist.Visible = true;
                                ViewState["action"] = null;
                                clear_lblvalue();
                                if (isSMS == true)
                                {
                                    //SendSMS(LETRNO);
                                }
                                else if (isEmail == true)
                                {
                                    if (objLM.STATUS == "A" || objLM.STATUS == "R")
                                    {
                                        // SendMail(LETRNO);
                                    }
                                }

                                clear();
                            }
                            ViewState["action"] = null;
                            ViewState["ModifyLeave"] = null;
                        }
                        else
                        {
                            CustomStatus cs = (CustomStatus)objApp.UpdateAppPassEntry(LETRNO, UA_NO, Status, Remarks, Aprdate, 0);
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                MessageBox("Record Updated Successfully");
                                pnllist.Visible = true;
                                pnlAdd.Visible = false;
                                ViewState["action"] = null;
                                clear_lblvalue();
                                if (isSMS == true)
                                {
                                    SendSMS(LETRNO);
                                }
                                else if (isEmail == true)
                                {
                                    if (objLM.STATUS == "A" || objLM.STATUS == "R")
                                    {
                                        //SendMail(LETRNO);
                                    }
                                }
                                clear();
                            }
                        }

                    }
                    else
                    {
                        CustomStatus cs = (CustomStatus)objApp.UpdateAppPassEntry(LETRNO, UA_NO, Status, Remarks, Aprdate, 0);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            pnllist.Visible = true;
                            pnlAdd.Visible = false;
                            ViewState["action"] = null;
                            clear_lblvalue();
                            clear();
                        }
                    }


                }
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }
    private void SendSMS(int letrno)
    {
        //int pano = Convert.ToInt32(objCommon.LookUp("payroll_leave_app_pass_entry", "pano", "STATUS='P' AND letrno=" + letrno));
        //int ua_no = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_PASSING_AUTHORITY", "ua_no", "PANO=" + pano));
        //string Mobile_no = objCommon.LookUp("user_acc", "ua_mobile", "ua_no=" + ua_no);
        //string name = objCommon.LookUp("payroll_empmas", "fname+' '+mname+' '+ lname as name", "idno=" + Convert.ToInt32(Session["idno"]));
        string url = "http://smsnmms.co.in/sms.aspx";
        string uid = string.Empty; string pass = string.Empty; string mobno = string.Empty; string message, Mobile_no = string.Empty;
        //select issms,* from payroll_leave_ref
        bool isSMS = Convert.ToBoolean(objCommon.LookUp("payroll_leave_ref", "isnull(isSMS,0)as isSMS", ""));
        if (isSMS == true)
        {
            DataSet dsReff = objCommon.FillDropDown("REFF", "SMSSVCID", "SMSSVCPWD", "", "");
            if (dsReff.Tables[0].Rows.Count > 0)
            {
                uid = dsReff.Tables[0].Rows[0]["SMSSVCID"].ToString();
                pass = dsReff.Tables[0].Rows[0]["SMSSVCPWD"].ToString();
            }
            objLM.LETRNO = letrno;
            DataSet ds = objApp.GetSMSInformation(objLM);

            if (ds.Tables[0].Rows.Count > 0)
            {


                string employeename = ds.Tables[1].Rows[0]["name"].ToString();

                double tot_days = Convert.ToDouble(ds.Tables[0].Rows[0]["NO_OF_DAYS"].ToString());
                string Joindt = ds.Tables[0].Rows[0]["Joindt"].ToString();
                string PHONENO = ds.Tables[0].Rows[0]["PHONENO"].ToString();
                string FromDate = ds.Tables[1].Rows[0]["From_Date"].ToString();
                string ToDate = ds.Tables[1].Rows[0]["To_Date"].ToString();
                string AUTHORITY_NAME = ds.Tables[1].Rows[0]["AUTHORITY_NAME"].ToString();

                string leavestatus = ds.Tables[1].Rows[0]["LeaveStatus"].ToString();
                string leavename = ds.Tables[1].Rows[0]["Leave_Name"].ToString();
                string leavenamestatus = string.Empty;

                if (leavestatus.Trim() != string.Empty || leavestatus != "")
                {
                    leavenamestatus = leavestatus + ' ' + leavename;
                }
                else
                {
                    leavenamestatus = leavename;
                }
                //Full day/half day [casual] Leave Application Submitted By ['ABC'] for [2] days & Joining Date is [Joindt]
                //   message = leavestatus + leavename + " Application Submitted By " + name + " For " + tot_days + " days & Joining Date is " + Joindt;
                // message = leavestatus + leavename + " Application Submitted By " + name + ". Date From " + Convert.ToDateTime(FromDate).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(ToDate).ToString("dd-MMM-yyyy") + " & Joining Date is " + Joindt;
                message = "Hi " + AUTHORITY_NAME + "," + Environment.NewLine + employeename + " " + "has applied " + leavenamestatus + " from " + FromDate
                             + " " + "to " + ToDate + " , " + Environment.NewLine + ""
                             + "Joining date will be " + Joindt;

                WebRequest request = HttpWebRequest.Create("" + url + "?ID=" + uid + "&PWD=" + pass + "&PHNO=" + PHONENO + "&TEXT=" + message + "");
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string urlText = reader.ReadToEnd();
            }
        }
    }
    public void SendMail(int letrno)
    {
        try
        {
            string body = string.Empty;
            objLM.LETRNO = letrno;
            DataSet ds = objApp.GetSMSInformation(objLM);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string fromEmailId = ds.Tables[0].Rows[0]["SenderEmailId"].ToString();
                string fromEmailPwd = ds.Tables[0].Rows[0]["SenderEmailPassword"].ToString();
                if (ds.Tables[1].Rows.Count > 0)
                {
                    string STATUS = ds.Tables[1].Rows[0]["STATUS"].ToString();
                    string Name = ds.Tables[1].Rows[0]["NAME"].ToString();
                    string AUTHORITY_NAME = ds.Tables[1].Rows[0]["AUTHORITY_NAME"].ToString();
                    //AUTHORITY_NAME
                    string FromDate = ds.Tables[1].Rows[0]["From_Date"].ToString();
                    string ToDate = ds.Tables[1].Rows[0]["To_Date"].ToString();

                    string employeename = ds.Tables[1].Rows[0]["name"].ToString();
                    double tot_days = Convert.ToDouble(ds.Tables[1].Rows[0]["NO_OF_DAYS"].ToString());
                    string Joindt = ds.Tables[1].Rows[0]["Joindt"].ToString();
                    string toEmailId = ds.Tables[1].Rows[0]["EMAILID"].ToString();//Approve_Status
                    string Approve_Status = ds.Tables[1].Rows[0]["Approve_Status"].ToString();//Approve_Status
                    string Employeemail = ds.Tables[1].Rows[0]["Employeemail"].ToString();
                    string APR_REMARKS = ds.Tables[1].Rows[0]["APR_REMARKS"].ToString();
                    string Sub = "Leave Application Notification";
                    //Full day/half day [casual] Leave Application Submitted By ['ABC']. Date from 1/1/2018 to 1/12018 & Joining Date is [Joindt]
                    //body = leavestatus + leavename + " Application Submitted By " + name + " For " + tot_days + " days & Joining Date is " + Joindt;

                    //body = leavestatus + leavename + " Application Submitted By " + name + ". Date From " + Convert.ToDateTime(FromDate).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(ToDate).ToString("dd-MMM-yyyy") + " & Joining Date is " + Joindt;

                    string leavestatus = ds.Tables[1].Rows[0]["LeaveStatus"].ToString();
                    string leavename = ds.Tables[1].Rows[0]["Leave_Name"].ToString();
                    string leavenamestatus = string.Empty;

                    if (leavestatus.Trim() != string.Empty || leavestatus != "")
                    {
                        leavenamestatus = leavestatus + ' ' + leavename;
                    }
                    else
                    {
                        leavenamestatus = leavename;
                    }
                    string ToEmail = string.Empty;//Approve_Status
                    string successNote = string.Empty;
                    if (STATUS == "A")
                    {
                        successNote = " successufully !";
                    }
                    //if (STATUS == "P")
                    //{


                    //    body = "Hi " + AUTHORITY_NAME + "," + Environment.NewLine + employeename + " " + "has applied " + leavenamestatus + " from " + FromDate
                    //               + " " + "to " + ToDate + " , " + Environment.NewLine + ""
                    //               + "Joining date will be " + Joindt;
                    //    ToEmail = ds.Tables[1].Rows[0]["EmailId"].ToString();
                    //}
                    if (STATUS == "A")
                    {
                        body = "Dear " + employeename + "," + Environment.NewLine +
                            "Your Leave application dated From " + FromDate + " To " + ToDate + " is " + Approve_Status + "." + Environment.NewLine + Environment.NewLine
                            + "HR-Department" + Environment.NewLine + " Sri Venkateswara College of Engineering";


                        //body = "Greeti " + employeename + "," + Environment.NewLine + " Your applied " + leavenamestatus + " from " + FromDate
                        //           + " " + "to " + ToDate + " , " + Environment.NewLine + ""
                        //           + " has been " + Approve_Status+" "+ successNote;
                        ToEmail = ds.Tables[1].Rows[0]["EmailId"].ToString();
                    }

                    else if (STATUS == "R")
                    {
                        body = "Dear " + employeename + "," + Environment.NewLine +
                            "Your Leave application dated From " + FromDate + " To " + ToDate + " is " + Approve_Status + " due to the following reason . " + Environment.NewLine +
                            "Reason : " + APR_REMARKS + Environment.NewLine + Environment.NewLine + "HR-Department" + Environment.NewLine + " Sri Venkateswara College of Engineering ";
                    }
                    SmtpClient smtpClient = new SmtpClient();
                    // MailMessage mailMessage = new MailMessage(fromEmailId, toEmailId, Sub, body);
                    MailMessage mailMessage = new MailMessage(fromEmailId, Employeemail, Sub, body);
                    smtpClient.Credentials = new System.Net.NetworkCredential(fromEmailId, fromEmailPwd);

                    smtpClient.EnableSsl = true;
                    smtpClient.Port = 587; // 587
                    smtpClient.Host = "smtp.gmail.com";

                    ServicePointManager.ServerCertificateValidationCallback =
                    delegate(object s, X509Certificate certificate,
                    X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    { return true; };

                    smtpClient.Send(mailMessage);// .SendAsync(mailMessage, null);
                    // lblMsg.Text = "Email has been successfully sent..!!";
                    /*
                       text = "Hi " + multipleName + "," + Environment.NewLine + employeename + " " + "has applied a leave from " + FromDate
                                + " " + "to " + ToDate + " " + Environment.NewLine + "with reason " + "'" + txtReason.Text.Trim() + "'" + ","
                                + "Joining date will be " + JoiningDate;
                     * */
                }
            }


        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

    }
    //protected void btnApproval_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Button btnApproval = sender as Button;
    //        int LETRNO = int.Parse(btnApproval.CommandArgument);
    //        Session["LETRNO"] = LETRNO;

    //        ShowDetails(LETRNO);
    //        pnllist.Visible = false;
    //        pnlAdd.Visible = true;
    //        pnlButton.Visible = true;

    //        ViewState["action"] = "edit";
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.btnApproval_Click ->" + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server.UnAvailable");
    //    }
    //}

    //protected void btnApproval_Click(object sender, EventArgs e)
    //{
    //    try
    //    {

    //        Button btnApproval = sender as Button;
    //        int LETRNO = int.Parse(btnApproval.CommandArgument);
    //        Session["LETRNO"] = LETRNO;
    //        ShowDetails(LETRNO);
    //        // int LETRNO = Convert.ToInt32(ViewState["LETRNO"].ToString());
    //        int UA_NO = Convert.ToInt32(Session["userno"]);
    //        string Status;
    //        objLM.STATUS = Status = "A".ToString().Trim(); //txtRemarks.Text.ToString();
    //        //lvPendingList.FindControl 
    //        DateTime Aprdate = Convert.ToDateTime(DateTime.Now.Date);
    //        bool isSMS = Convert.ToBoolean(objCommon.LookUp("payroll_leave_ref", "isnull(isSMS,0)as isSMS", ""));
    //        bool isEmail = Convert.ToBoolean(objCommon.LookUp("payroll_leave_ref", "isnull(isEmail,0)as isEmail", ""));
    //        string Remarks;

    //        var control = (Control)sender;
    //        var container = control.NamingContainer;
    //        var textBox = container.FindControl("txtxremarknew") as TextBox;

    //        // foreach (ListViewDataItem dataItem in lvPendingList.Items)
    //        //{

    //        // TextBox PrNo = dataItem.FindControl("txtxremarknew") as TextBox;
    //        //TextBox PrNo = lvPendingList.FindControl("txtxremarknew") as TextBox;
    //        // Remarks = PrNo.Text;

    //        Remarks = "Approved";

    //        CustomStatus cs = (CustomStatus)objApp.UpdateAppPassEntry(LETRNO, UA_NO, Status, Remarks, Aprdate, 0);
    //        if (cs.Equals(CustomStatus.RecordUpdated))
    //        {
    //            MessageBox("Record Updated Successfully");
    //            pnllist.Visible = true;
    //            pnlAdd.Visible = false;
    //            ViewState["action"] = null;
    //            clear_lblvalue();
    //            if (isSMS == true)
    //            {
    //                //SendSMS(LETRNO);
    //            }
    //            else if (isEmail == true)
    //            {
    //                if (objLM.STATUS == "A" || objLM.STATUS == "R")
    //                {
    //                    // SendMail(LETRNO);
    //                }
    //            }
    //            clear();
    //        }

    //        // }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.btnApproval_Click ->" + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server.UnAvailable");
    //    }
    //}

    //protected void btnModify_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Button btnApproval = sender as Button;
    //        int LETRNO = int.Parse(btnApproval.CommandArgument);
    //        Session["LETRNO"] = LETRNO;
    //        int UA_NO = Convert.ToInt32(Session["userno"]);
    //        string Status;
    //        objLM.STATUS = Status = "R".ToString().Trim(); //txtRemarks.Text.ToString();
    //        //lvPendingList.FindControl 

    //        DateTime Aprdate = Convert.ToDateTime(DateTime.Now.Date);
    //        bool isSMS = Convert.ToBoolean(objCommon.LookUp("payroll_leave_ref", "isnull(isSMS,0)as isSMS", ""));
    //        bool isEmail = Convert.ToBoolean(objCommon.LookUp("payroll_leave_ref", "isnull(isEmail,0)as isEmail", ""));
    //        string Remarks;
    //        var control = (Control)sender;
    //        var container = control.NamingContainer;
    //        var textBox = container.FindControl("txtxremarknew") as TextBox;
    //        Remarks = textBox.Text;
    //        CustomStatus cs = (CustomStatus)objApp.UpdateAppPassEntry(LETRNO, UA_NO, Status, Remarks, Aprdate, 0);
    //        if (cs.Equals(CustomStatus.RecordUpdated))
    //        {
    //            MessageBox("Record Updated Successfully");
    //            pnllist.Visible = true;
    //            pnlAdd.Visible = false;
    //            ViewState["action"] = null;
    //            clear_lblvalue();
    //            if (isSMS == true)
    //            {
    //                //SendSMS(LETRNO);
    //            }
    //            else if (isEmail == true)
    //            {
    //                if (objLM.STATUS == "A" || objLM.STATUS == "R")
    //                {
    //                    SendMail(LETRNO);
    //                }
    //            }
    //            clear();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.btnApproval_Click ->" + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server.UnAvailable");
    //    }

    //}



    //protected void btnModify_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Button btnApproval = sender as Button;
    //        int LETRNO = int.Parse(btnApproval.CommandArgument);
    //        Session["LETRNO"] = LETRNO;

    //        ShowDetails(LETRNO);

    //        pnllist.Visible = false;
    //        // pnlAdd.Visible = true;
    //        pnlvedit.Visible = true;
    //        pnlButton.Visible = true;

    //        ViewState["action"] = "edit";
    //        ViewState["ModifyLeave"] = "edit";
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.btnApproval_Click ->" + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server.UnAvailable");
    //    }

    //}

    private void ShowDetails(Int32 LETRNO)
    {
        DataSet ds = new DataSet();

        try
        {
            ds = objApp.GetLeaveApplDetail(LETRNO);

            //int YR = DateTime.Now.Year;
            //DataSet ds1 = objApp.GetLeavesStatus(Convert.ToInt32(Session["idno"].ToString()), YR, 0);//Session["idno"]
            //ds.Tables[1] = objApp.GetLeaveApplStatus(LETRNO);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["LETRNO"] = LETRNO;
                lblEmpName.Text = ds.Tables[0].Rows[0]["EmpName"].ToString();
                lblLeaveName.Text = ds.Tables[0].Rows[0]["LName"].ToString();
                lblFromdt.Text = ds.Tables[0].Rows[0]["From_date"].ToString();
                lblTodt.Text = ds.Tables[0].Rows[0]["TO_DATE"].ToString();
                lblNodays.Text = ds.Tables[0].Rows[0]["NO_OF_DAYS"].ToString();
                double NO_OF_DAYS = Convert.ToDouble(ds.Tables[0].Rows[0]["NO_OF_DAYS"]);  // ds.Tables[0].Rows[0]["NO_OF_DAYS"];
                if (NO_OF_DAYS == 0.5)
                {

                    divHalfCriteria.Visible = true;
                    rblleavetype.SelectedValue = "1";//HALF DAY
                    //bool LEAVE_FNAN =Convert.ToBoolean( ds.Tables[0].Rows[0]["LEAVE_FNAN"].ToString());
                    //if(LEAVE_FNAN==1)
                    if (ds.Tables[0].Rows[0]["LEAVE_FNAN"].ToString() == "FN")
                    {
                        ddlLeaveFNAN.SelectedValue = "1";
                    }
                    else if (ds.Tables[0].Rows[0]["LEAVE_FNAN"].ToString() == "AN")
                    {
                        ddlLeaveFNAN.SelectedValue = "0";
                    }
                }
                else
                {
                    divHalfCriteria.Visible = false; rblleavetype.SelectedValue = "0";//FULL DAY
                }
                lblJoindt.Text = ds.Tables[0].Rows[0]["JOINDT"].ToString();
                lblNoon.Text = ds.Tables[0].Rows[0]["LEAVE_FNAN"].ToString();
                lblReason.Text = ds.Tables[0].Rows[0]["REASON"].ToString();
                int lno = Convert.ToInt32(ds.Tables[0].Rows[0]["LNO"]);
                int leaveno = Convert.ToInt32(ds.Tables[0].Rows[0]["LEAVENO"]);
                ViewState["LNO"] = lno.ToString();


                string filename = ds.Tables[0].Rows[0]["filename"].ToString();
                ViewState["filename"] = filename.ToString();



                int idno = Convert.ToInt32(ds.Tables[0].Rows[0]["EMPNO"]);


                if (filename != string.Empty)
                {
                    lvDownload.DataSource = ds;
                    lvDownload.DataBind();
                    GetFileNamePath(filename, LETRNO, idno);//Screen shot-swati.png
                    divDocument.Visible = true;
                }
                else
                {
                    divDocument.Visible = false;
                }

                //Used To show details in Leave Modify Panel 
                txtLeavename.Text = ds.Tables[0].Rows[0]["LName"].ToString();
                txtfrmdt.Text = ds.Tables[0].Rows[0]["From_date"].ToString();
                txttodate.Text = ds.Tables[0].Rows[0]["TO_DATE"].ToString();
                txtNodays.Text = ds.Tables[0].Rows[0]["NO_OF_DAYS"].ToString();
                txtJoindt.Text = ds.Tables[0].Rows[0]["JOINDT"].ToString();

                ViewState["EMPNO"] = ds.Tables[0].Rows[0]["EMPNO"].ToString();
                ViewState["COLLEGE_NO"] = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString();
                ViewState["STNO"] = ds.Tables[0].Rows[0]["STNO"].ToString();
                // int YR = DateTime.Now.Year;
                // DataSet ds1 = objApp.GetLeavesStatus(idno, YR, leaveno);//Session["idno"]
                int YR = 0;// DateTime.Now.Year;
                YR = Convert.ToInt32(ds.Tables[0].Rows[0]["YEAR"].ToString());
                // DataSet ds1 = objApp.GetLeavesStatus(idno, YR, lno);//Session["idno"]
                DataSet ds1 = objApp.GetLeavesStatus(idno, YR, lno);//Session["idno"]
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    double bal = Convert.ToDouble(ds1.Tables[0].Rows[0]["BAL"]);
                    txtLeavebal.Text = bal.ToString();
                    lblbal.Text = bal.ToString();
                    double total = Convert.ToDouble(ds1.Tables[0].Rows[0]["TOTAL"]);
                    // DataSet dsbal = objCommon.FillDropDown("PAYROLL_LEAVE_APP_ENTRY", "SUM(NO_OF_DAYS) as LEAVES", "", "STATUS IN('A','T') and EMPNO=" + idno + "AND LNO=" + lno, "");
                    double leaves = Convert.ToDouble(objCommon.LookUp("PAYROLL_LEAVE_APP_ENTRY", "ISNULL(SUM(NO_OF_DAYS),0) as LEAVES", "STATUS IN('A','T') and EMPNO=" + idno + "AND LNO=" + lno));
                    // double leaves = Convert.ToDouble(dsbal.Tables [0].Rows [0]["LEAVES"]);
                    //lbltot.Text = total.ToString();
                    //lblbal.Text = (total - leaves).ToString();
                    //added by suraj
                    // lblbal.Text=ds1.Tables[0].Rows[0]["bal"].ToString();
                    // //-----------------------------------------------------
                    // //18-may-2015
                    //string leavename = Convert.ToString(objCommon.LookUp("PAYROLL_LEAVE_NAME", "LEAVE_SHORTNAME", "LVNO=" + lno));

                    // if (leavename == "COMPL")
                    // {

                    //     lblbal.Text = "1.00";
                    // }
                    // else
                    // {
                    //     lblbal.Text = ds1.Tables[0].Rows[0]["bal"].ToString();
                    // }


                    // //------------------------------------------------------
                    // decimal lblbalchk = Convert.ToDecimal(lblbal.Text);
                    // if (lblbalchk == 0)
                    //     lblBalZeroMsg.Visible = true;

                    // else
                    //     lblBalZeroMsg.Visible = false;

                    //txtLeavebal.Text = (total - leaves).ToString();
                }

                ViewState["IsPayLeave"] = ds.Tables[0].Rows[0]["IsPayLeave"].ToString();
                if (Convert.ToBoolean(ViewState["IsPayLeave"]) == true)
                {
                    divml.Visible = true;
                    trType.Visible = false;
                    int ml_hpl = Convert.ToInt32(ds.Tables[0].Rows[0]["ML_HPL"].ToString());
                    if (ml_hpl == 1)
                    {
                        //full pay leave
                        rdbml.SelectedValue = "0";
                    }
                    else if (ml_hpl == 2)
                    {
                        //full pay leave
                        rdbml.SelectedValue = "1";
                    }
                }
                else
                {
                    divml.Visible = false;
                    trType.Visible = true;
                }
            }
            lvStatus.DataSource = ds.Tables[1];
            lvStatus.DataBind();
            divAuthorityList.Visible = true;
            if (ds.Tables[2].Rows.Count > 0)
            {
                //FORWARD_APPROVAL_STATUS
                string FORWARD_APPROVAL_STATUS = ds.Tables[2].Rows[0]["FORWARD_APPROVAL_STATUS"].ToString();

                //string status = Convert.ToString(objCommon.LookUp("PAYROLL_LEAVE_APP_PASS_ENTRY", "status", "LETRNO=" + LETRNO + " and status='P'"));
                //if (status == "P")
                //{
                //    //ddlSelect.Items.Remove("Forward To Next Authority(Recommended)");
                //    // ddlSelect.SelectedValue.Remove="F";
                //    ListItem removeItem = ddlSelect.Items.FindByValue("F");
                //    ddlSelect.Items.Remove(removeItem);
                //}
                if (FORWARD_APPROVAL_STATUS != "F".ToString().Trim())
                {
                    ListItem removeItem = ddlSelect.Items.FindByValue("F");
                    ddlSelect.Items.Remove(removeItem);
                    removeItem = ddlSelect.Items.FindByValue("Forward To Next Authority(Recommended)");
                    ddlSelect.Items.Remove(removeItem);

                    removeItem = ddlSelectModify.Items.FindByValue("F");
                    ddlSelectModify.Items.Remove(removeItem);
                    removeItem = ddlSelectModify.Items.FindByValue("Forward To Next Authority(Recommended)");
                    ddlSelectModify.Items.Remove(removeItem);

                }
                else
                {
                    ddlSelect.Items.Clear();
                    ddlSelect.Items.Add("Forward To Next Authority(Recommended)");
                    ddlSelect.Items.Add("Approve & Final Submit");
                    ddlSelect.Items.Add("Reject");



                    ddlSelectModify.Items.Clear();
                    ddlSelectModify.Items.Add("Forward To Next Authority(Recommended)");
                    ddlSelectModify.Items.Add("Approve & Final Submit");
                    ddlSelectModify.Items.Add("Reject");

                }

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.ShowDetails ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindLVLeaveApprStatusParticular();
    }

    protected void btnHidePanel_Click(object sender, EventArgs e)
    {
        pnlODStatus.Visible = false;

        pnlAdd.Visible = false;
        pnllist.Visible = true;
        trfrmto.Visible = false;
        trbutshow.Visible = false;
        Tr1.Visible = true;
        lnkbut.Visible = true;
    }

    protected void lnkbut_Click(object sender, EventArgs e)
    {
        pnlODStatus.Visible = true;
        pnlAdd.Visible = false;
        pnllist.Visible = false;
        trfrmto.Visible = true;
        trbutshow.Visible = true;
        Tr1.Visible = false;
        lnkbut.Visible = false;
        this.BindLVLeaveApprStatusAll();
    }

    protected void BindLVLeaveApprStatusAll()
    {
        try
        {
            DataSet ds = objApp.GetPendListforLVApprovalStatusALL(Convert.ToInt32(Session["userno"]));
            if (ds.Tables[0].Rows.Count <= 0)
            {
                //dpPager.Visible = false;
            }
            else
            {
               // dpPager.Visible = true;
            }
            lvApprStatus.DataSource = ds;
            lvApprStatus.DataBind();
            btnHidePanel.Visible = true;
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.BindLVLeaveApplPendingList ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }

    protected void BindLVLeaveApprStatusParticular()
    {
        try
        {
            //DateTime DT = Convert.ToDateTime (txtFromdt.Text);
            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromdt.Text)));
            Fdate = Fdate.Substring(0, 10);
            string Tdate = (String.Format("{0:u}", Convert.ToDateTime(txtTodt.Text)));
            Tdate = Tdate.Substring(0, 10);


            DataSet ds = objApp.GetPendListforLVApprovalStatusParticular(Convert.ToInt32(Session["userno"]), Fdate, Tdate);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                //dpPager.Visible = false;
            }
            else
            {
                //dpPager.Visible = true;
            }
            lvApprStatus.DataSource = ds;
            lvApprStatus.DataBind();
            btnHidePanel.Visible = true;
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.BindLVLeaveApplPendingList ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }
    protected void txttodate_TextChanged(object sender, EventArgs e)
    {
        DataSet ds = null;
        try
        {

            int letrno = Convert.ToInt32(Session["LETRNO"]);
            int aplno = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_APP_ENTRY", "LNO", "LETRNO=" + letrno));


            int days = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE", "ALLOWED_DAYS", "LNO=" + aplno));
            int calholy = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE", "CAL_HOLIDAYS", "LNO=" + aplno));

            DateTime dt = Convert.ToDateTime(txttodate.Text);
            string todt = dt.ToShortDateString();

            DateTime today = System.DateTime.Now.Date;
            string todate = today.ToShortDateString();
            DateTime dt2 = Convert.ToDateTime(todate);
            DateTime sundaydt = dt2.AddDays(-1);


            DateTime sub = today.AddDays(-days);
            string validdate = sub.ToShortDateString();
            DateTime frmdt = Convert.ToDateTime(txtfrmdt.Text);

            string fromdt = frmdt.ToShortDateString();
            int frm = Convert.ToInt32(fromdt.Substring(0, 2));
            int todaydt = Convert.ToInt32(todate.Substring(0, 2));


            btnSave.Enabled = true;
            if (frmdt < sub)
            {
                MessageBox("Not Allowed ,Please check To Date Todate is not more than before" + days + "day of today");
                btnSave.Enabled = false;

            }
            else
            {
                btnSave.Enabled = true;
            }

            if (txttodate.Text.ToString() != string.Empty && txttodate.Text.ToString() != "__/__/____" && txtfrmdt.Text.ToString() != string.Empty && txtfrmdt.Text.ToString() != "__/__/____")
            {
                int stno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "STNO", "IDNO=" + Convert.ToInt32(Session["idno"])));
                int collegeno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "COLLEGE_NO", "IDNO=" + Convert.ToInt32(Session["idno"])));
                ds = objApp.GetNoofdays(Convert.ToDateTime(txtfrmdt.Text), Convert.ToDateTime(txttodate.Text), "0", stno, calholy, collegeno, Convert.ToInt32(ddlLeaveFNAN.SelectedValue));

                if (ds.Tables[0].Rows.Count > 0)
                {

                    double totdays = Convert.ToDouble(ds.Tables[0].Rows[0]["TOT_DAYS"].ToString());
                    txtNodays.Text = totdays.ToString();

                    txtJoindt.Text = ds.Tables[0].Rows[0]["JOINDT"].ToString();
                    txtNodays.Text = Convert.ToString(ds.Tables[0].Rows[0][0]);
                    if (totdays == 0)
                    {
                        btnSave.Enabled = false;
                        MessageBox("Sorry! Total Days are Zero. Not Allow");
                        return;
                    }
                    if (Convert.ToBoolean(ViewState["IsPayLeave"]) == true)
                    {
                        if (rdbml.SelectedValue == "0")
                        {
                            totdays = totdays * 2;
                            txtNodays.Text = totdays.ToString();
                        }
                    }
                    else
                    {
                        btnSave.Enabled = true;
                    }
                    Chkvalid();


                }
            }
            lblFromdt.Text = txtfrmdt.Text;
            lblTodt.Text = txttodate.Text;
            lblNodays.Text = txtNodays.Text;
            btnSave.Enabled = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Application.txtTodt_TextChanged->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }
    protected void Chkvalid()
    {
        if (txtLeavebal.Text != string.Empty && txtNodays.Text != string.Empty)
        {
            if (Convert.ToDouble(txtLeavebal.Text) <= 0)
            {
                objCommon.DisplayMessage("Leave Days Can not be 0 or leass than 0", this);

                txttodate.Text = string.Empty;
                txtNodays.Text = string.Empty;
                txttodate.Focus();
            }

            if (Convert.ToDouble(txtLeavebal.Text) < Convert.ToDouble(txtNodays.Text))
            {
                objCommon.DisplayMessage("Leave Days Can not be greater than Balance Days", this);

                txttodate.Text = string.Empty;
                txtNodays.Text = string.Empty;
                txttodate.Focus();
            }
            else
            {
                txtNodays.Focus();
            }
        }

    }
    //function to popup the message box
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    public string GetFileNamePath(object filename, object letrno, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/LEAVE_CERTIFICATE_DOCUMENT/" + idno.ToString() + "/LETRNO_" + letrno + "." + extension[1].ToString().Trim());
        else
            return "";
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        int LETRNO = 0;
        string Remarks;
        int cs = 0;
        int checkcount = 0;
        try
        {

            foreach (ListViewDataItem items in lvPendingList.Items)
            {
                CheckBox chkSelect = items.FindControl("chkSelect") as CheckBox;
                HiddenField idno = items.FindControl("hdnEmpno") as HiddenField;
                HiddenField leaveno = items.FindControl("hdnLeaveno") as HiddenField;
                Label NoOfDays = items.FindControl("noOfDays") as Label;

                int IDNO = Convert.ToInt32(idno.Value);
                int LeaveNo = Convert.ToInt32(leaveno.Value);
                double noOfDays = Convert.ToDouble(NoOfDays.Text);

                int UA_NO = Convert.ToInt32(Session["userno"]);
                string Status;
                objLM.STATUS = Status = "A".ToString().Trim(); //txtRemarks.Text.ToString();
                //lvPendingList.FindControl 
                DateTime Aprdate = Convert.ToDateTime(DateTime.Now.Date);

                Remarks = "Approved";

                if (chkSelect.Checked && chkSelect != null)
                {
                    checkcount = checkcount + 1;
                    LETRNO = Convert.ToInt32(chkSelect.ToolTip);
                    DataSet ds = objApp.GetBalanceforDirectLeaveApproval(LETRNO, IDNO, LeaveNo);
                    double bal = Convert.ToDouble(ds.Tables[0].Rows[0]["CLBal"].ToString());

                    if (bal >= noOfDays || Status.Equals("R"))
                    {
                        cs = Convert.ToInt32(objApp.UpdateAppPassEntry(LETRNO, UA_NO, Status, Remarks, Aprdate, 0));
                    }

                }
            }
            if (checkcount == 0)
            {
                MessageBox("Please Select Atleast One Leave for Approval");
                return;
            }
            if (checkcount > 0)
            {
                MessageBox("Record Saved Successfully");

            }

            BindLVLeaveApplPendingList();
        }
        catch (Exception ex)
        {
        }

    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        int LETRNO = 0;
        string Remarks;
        int cs = 0;
        int checkcount = 0;
        try
        {
            foreach (ListViewDataItem items in lvPendingList.Items)
            {
                CheckBox chkSelect = items.FindControl("chkSelect") as CheckBox;

                int UA_NO = Convert.ToInt32(Session["userno"]);
                string Status;
                objLM.STATUS = Status = "R".ToString().Trim(); //txtRemarks.Text.ToString();
                //lvPendingList.FindControl 
                DateTime Aprdate = Convert.ToDateTime(DateTime.Now.Date);

                Remarks = "Rejected";

                if (chkSelect.Checked && chkSelect != null)
                {
                    checkcount = checkcount + 1;
                    LETRNO = Convert.ToInt32(chkSelect.ToolTip);
                    cs = Convert.ToInt32(objApp.UpdateAppPassEntry(LETRNO, UA_NO, Status, Remarks, Aprdate, 0));

                }
            }
            if (checkcount == 0)
            {
                MessageBox("Please Select Atleast One Leave for Reject");
                return;
            }
            if (checkcount > 0)
            {
                MessageBox("Record Saved Successfully");
            }

            BindLVLeaveApplPendingList();
        }
        catch (Exception ex)
        {

        }
    }
    protected void txtTodt_TextChanged(object sender, EventArgs e)
    {
        DateTime DtFrom, DtTo, Test;
        if (DateTime.TryParseExact(txtTodt.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out Test) == true)
        {
            if (txtTodt.Text != string.Empty && txtTodt.Text != "__/__/____" && txtFromdt.Text != string.Empty && txtFromdt.Text != "__/__/____")
            {
                DtFrom = Convert.ToDateTime(txtFromdt.Text.ToString());

                DtTo = Convert.ToDateTime(txtTodt.Text.ToString());

                if (DtFrom > DtTo)
                {
                    MessageBox("To Date Should Be Larger Than Or Equals To From Date");
                    //txtTodt.Text = string.Empty;
                    txtTodt.Text = string.Empty;
                    return;
                }
            }
        }
        else
        {
            txtTodt.Text = string.Empty;
        }
    }
}