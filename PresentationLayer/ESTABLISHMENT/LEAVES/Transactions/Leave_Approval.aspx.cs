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
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using BusinessLogicLayer.BusinessLogic;


public partial class ESTABLISHMENT_LEAVES_Transactions_Leave_Approval : System.Web.UI.Page
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
                CheckPageAuthorization();
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
        //divAuthorityList.Visible = false;
        ViewState["IsPayLeave"] = null;
        //pnlButton.Visible = false;
        //btnSave.Enabled = false;
        lvDownload.DataBind();
        lvDownload.DataSource = null;
        //clear_lblvalue();
        clear_textvalue();
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
    private void clear_textvalue()
    {
        // txtLeavename.Text = txtLeavebal.Text = string.Empty;
        txtfrmdt.Text = txttodate.Text = string.Empty;
        txtNodays.Text = txtJoindt.Text = string.Empty;
        //ddlSelectModify.SelectedValue = "0";
        ddlSelectModify.SelectedIndex = 0;
        txtRemarkModify.Text = string.Empty;
        // btnSave.Enabled = false;
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
        ViewState["action"] = null;
        clear_lblvalue();
        clear();
        btnSave.Visible = true;
        btnSave.Enabled = true;
        pnlButton.Visible = false;
        divAuthorityList.Visible = false;
        lnkbut.Visible = true;
        ViewState["LeaveBalance"] = null;
        ViewState["No_of_days"] = null;
    }
    protected void rblleavetype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            btnSave.Enabled = true;
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
                    if (txtfrmdt.Text != txttodate.Text)
                    {
                        MessageBox("Please select single day for Half Day Leave");
                        btnSave.Enabled = false;
                        return;
                    }
                    else
                    {
                        // DataSet ds = objApp.GetNoofdays(Convert.ToDateTime(txtFromdt.Text), Convert.ToDateTime(txtTodt.Text), rblleavetype.SelectedValue, Convert.ToInt32(ddlNoon.SelectedValue), stno, calholy);
                        DataSet ds = objApp.GetNoofdays(Convert.ToDateTime(txtfrmdt.Text), Convert.ToDateTime(txttodate.Text), rblleavetype.SelectedValue, stno, calholy, Convert.ToInt32(ViewState["COLLEGE_NO"]), Convert.ToInt32(ddlLeaveFNAN.SelectedValue));

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            //txttodate,txtTodt,txtfrmdt,txtFromdt
                            double no_of_days = Convert.ToDouble(ds.Tables[0].Rows[0]["TOT_DAYS"]);
                            txtNodays.Text = no_of_days.ToString();
                            txtJoindt.Text = Convert.ToString(ds.Tables[0].Rows[0]["JOINDT"]);
                            btnSave.Enabled = true;
                        }
                    }
                }
            }
            else
            {
                //tdHalfCriteria.Visible = false; tdHalfCriteriaLable.Visible = tdHalfCriteriaLable1.Visible = false;
                //tdHalfCriteriaLable1.Visible = tdHalfCriteriaLable2.Visible = tdHalfCriteriaLable3.Visible = false;
                divHalfCriteria.Visible = false;
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //Leaves objLM = new Leaves();
            int LETRNO = Convert.ToInt32(ViewState["LETRNO"].ToString());
            int UA_NO = Convert.ToInt32(Session["userno"]);
            string Status = ddlSelect.SelectedValue.ToString();
            //if (Status == "Forward To Next Authority(Recommended)".ToString().Trim())
            if (Status == "Approve & Forward To Next Authority(Recommended)".ToString().Trim())
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

            string Remarks ;
            if (txtRemarks.Text != string.Empty)
            {
                Remarks = txtRemarks.Text.ToString();
            }
            else
            {
                Remarks = string.Empty;
            }
            DateTime Aprdate = Convert.ToDateTime(DateTime.Now.Date);
            bool isSMS = Convert.ToBoolean(objCommon.LookUp("payroll_leave_ref", "isnull(isSMS,0)as isSMS", ""));
            bool isEmail = Convert.ToBoolean(objCommon.LookUp("payroll_leave_ref", "isnull(isEmail,0)as isEmail", ""));

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("edit"))
                {
                    if (Convert.ToDouble(ViewState["LeaveBalance"]) >= Convert.ToDouble(ViewState["No_of_days"]) || Status.Equals("R"))
                    {
                        if (ViewState["ModifyLeave"] != null)
                        {
                            if (ViewState["ModifyLeave"].ToString().Equals("edit"))
                            {
                                Status = ddlSelectModify.SelectedValue.ToString();
                                //if (Status == "Forward To Next Authority(Recommended)".ToString().Trim())
                                if (Status == "Approve & Forward To Next Authority(Recommended)".ToString().Trim())
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
                                Remarks = txtRemarkModify.Text.ToString();
                                objLM.APP_REMARKS = Remarks;
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
                                        string statusnew = objCommon.LookUp("Payroll_leave_app_entry", "status", "LETRNO=" + LETRNO);
                                        if (cs.Equals(CustomStatus.RecordUpdated))
                                        {
                                            MessageBox("Record Saved Successfully");
                                            divAuthorityList.Visible = false;
                                            pnlButton.Visible = false;
                                            pnllist.Visible = true;
                                            pnlAdd.Visible = false;
                                            ViewState["action"] = null;
                                            clear_lblvalue();
                                            if (isSMS == true)
                                            {
                                                if (statusnew == "A" || statusnew == "R")
                                                {
                                                    //SendSMS(LETRNO);
                                                }
                                            }
                                            if (isEmail == true)
                                            {
                                                if (statusnew == "A" || statusnew == "R")
                                                {
                                                    SendMail(LETRNO);
                                                }
                                            }
                                            lnkbut.Visible = true;
                                        }
                                    }
                                }
                                else
                                {
                                    objLM.LEAVEFNAN = null;
                                    CustomStatus cs = (CustomStatus)objApp.UpdateAppPassEntry(LETRNO, UA_NO, Status, Remarks, Aprdate, 0);
                                    string statusnew = objCommon.LookUp("Payroll_leave_app_entry", "status", "LETRNO=" + LETRNO);
                                    if (cs.Equals(CustomStatus.RecordUpdated))
                                    {
                                        MessageBox("Record Saved Successfully");
                                        divAuthorityList.Visible = false;
                                        pnlButton.Visible = false;
                                        pnllist.Visible = true;
                                        pnlAdd.Visible = false;
                                        ViewState["action"] = null;
                                        clear_lblvalue();

                                        if (isSMS == true)
                                        {
                                            if (statusnew == "A" || statusnew == "R")
                                            {
                                                //SendSMS(LETRNO);
                                            }

                                        }
                                        if (isEmail == true)
                                        {
                                            if (statusnew == "A" || statusnew == "R" || statusnew == "P")
                                            {
                                                SendMail(LETRNO);
                                            }
                                        }
                                        clear();
                                        lnkbut.Visible = true;
                                    }

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
                                    MessageBox("Record Saved Successfully");
                                    pnlAdd.Visible = false;
                                    divAuthorityList.Visible = false;
                                    pnlButton.Visible = false;
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
                                        SendMail(LETRNO);
                                    }

                                    clear();
                                }
                                ViewState["action"] = null;
                                ViewState["ModifyLeave"] = null;
                                lnkbut.Visible = true;
                            }
                            else
                            {
                                CustomStatus cs = (CustomStatus)objApp.UpdateAppPassEntry(LETRNO, UA_NO, Status, Remarks, Aprdate, 0);
                                string statusnew = objCommon.LookUp("Payroll_leave_app_entry", "status", "LETRNO=" + LETRNO);
                                if (cs.Equals(CustomStatus.RecordUpdated))
                                {
                                    MessageBox("Record Saved Successfully");
                                    divAuthorityList.Visible = false;
                                    pnlButton.Visible = false;
                                    pnllist.Visible = true;
                                    pnlAdd.Visible = false;
                                    ViewState["action"] = null;
                                    clear_lblvalue();
                                    if (isSMS == true)
                                    {
                                        if (statusnew == "A" || statusnew == "R")
                                        {
                                            //SendSMS(LETRNO);
                                        }
                                    }
                                    if (isEmail == true)
                                    {
                                        if (statusnew == "A" || statusnew == "R" || statusnew == "P")
                                        {
                                            SendMail(LETRNO);
                                        }
                                    }
                                    BindLVLeaveApplPendingList();
                                    clear();
                                    lnkbut.Visible = true;
                                }
                            }

                        }
                        else
                        {
                            CustomStatus cs = (CustomStatus)objApp.UpdateAppPassEntry(LETRNO, UA_NO, Status, Remarks, Aprdate, 0);
                            string statusnew = objCommon.LookUp("Payroll_leave_app_entry", "status", "LETRNO=" + LETRNO);
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                pnllist.Visible = true;
                                pnlAdd.Visible = false;
                                divAuthorityList.Visible = false;
                                pnlButton.Visible = false;
                                ViewState["action"] = null;
                                clear_lblvalue();
                                if (isSMS == true)
                                {
                                    if (statusnew == "A" || statusnew == "R")
                                    {
                                        //SendSMS(LETRNO);
                                    }
                                }
                                if (isEmail == true)
                                {
                                    if (statusnew == "A" || statusnew == "R" || statusnew == "P")
                                    {
                                        SendMail(LETRNO);
                                    }
                                }
                                clear();
                                lnkbut.Visible = true;
                            }
                        }
                        ViewState["LeaveBalance"] = null;
                        ViewState["No_of_days"] = null;
                    }
                    else
                    {
                        objCommon.DisplayMessage("Leave can not Approved!! Applicant have Insufficient Leave Balance.", this);
                        return;
                    }
                }
                BindLVLeaveApplPendingList();
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
        //bool isSMS = Convert.ToBoolean(objCommon.LookUp("payroll_leave_ref", "isnull(isSMS,0)as isSMS", ""));
        bool isSMS = true;
        if (isSMS == true)
        {
            objLM.LETRNO = letrno;
            DataSet ds = objApp.GetSMSInformation(objLM);

            if (ds.Tables[0].Rows.Count > 0)
            {


                string employeename = ds.Tables[1].Rows[0]["name"].ToString();

                double tot_days = Convert.ToDouble(ds.Tables[1].Rows[0]["NO_OF_DAYS"].ToString());
                string Joindt = ds.Tables[1].Rows[0]["Joindt"].ToString();
                string PHONENO = ds.Tables[1].Rows[0]["PHONENO"].ToString();
                string FromDate = ds.Tables[1].Rows[0]["From_Date"].ToString();
                string ToDate = ds.Tables[1].Rows[0]["To_Date"].ToString();
                string AUTHORITY_NAME = ds.Tables[1].Rows[0]["AUTHORITY_NAME"].ToString();

                string leavestatus = ds.Tables[1].Rows[0]["LeaveStatus"].ToString();
                string leavename = ds.Tables[1].Rows[0]["Leave_Name"].ToString();
                string status = ds.Tables[1].Rows[0]["STATUS"].ToString();
                string EMPPHONENO = ds.Tables[1].Rows[0]["EMPLOYEEPHONENO"].ToString();
                //string EMPPHONENO = "9665884963";
                string Balance = ds.Tables[1].Rows[0]["Bal"].ToString();
                string leavenamestatus = string.Empty;

                if (leavestatus.Trim() != string.Empty || leavestatus != "")
                {
                    leavenamestatus = leavestatus + ' ' + leavename;
                }
                else
                {
                    leavenamestatus = leavename;
                }
                if (status == "A")
                {
                    message = "Dear " + employeename + "\nWe are happy to grant you a " + leavename + " request starting from " + FromDate + " to " + ToDate + "."
                              + "\nYour " + leavename + " balance is " + Balance + "." + "\nMAKAUT, WB";


                    SendSMSTEMPLATE(EMPPHONENO, message, "1007362942717297845");


                }
                else if (status == "R")
                {
                    string Remark = "";
                    if (txtRemarks.Text != string.Empty)
                    {
                        Remark = txtRemarks.Text;
                    }
                    else
                    {
                        Remark = "";
                    }
                    message = "Dear " + employeename + ",\nYour " + leavename + "application is rejected." + "\nReason- " + Remark + ".\nMAKAUT, WB";

                    SendSMSTEMPLATE(EMPPHONENO, message, "1007484436363016512");

                }
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
                    string department = ds.Tables[1].Rows[0]["department"].ToString();
                    string APPDT = ds.Tables[1].Rows[0]["APPDT"].ToString();
                    double tot_days = Convert.ToDouble(ds.Tables[1].Rows[0]["NO_OF_DAYS"].ToString());
                    string Joindt = ds.Tables[1].Rows[0]["Joindt"].ToString();
                    string toEmailId = ds.Tables[1].Rows[0]["EMAILID"].ToString();//Approve_Status
                    string Approve_Status = ds.Tables[1].Rows[0]["Approve_Status"].ToString();//Approve_Status
                    string Sub = "Leave Application Notification";
                    //Full day/half day [casual] Leave Application Submitted By ['ABC']. Date from 1/1/2018 to 1/12018 & Joining Date is [Joindt]
                    //body = leavestatus + leavename + " Application Submitted By " + name + " For " + tot_days + " days & Joining Date is " + Joindt;

                    //body = leavestatus + leavename + " Application Submitted By " + name + ". Date From " + Convert.ToDateTime(FromDate).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(ToDate).ToString("dd-MMM-yyyy") + " & Joining Date is " + Joindt;

                    string leavestatus = ds.Tables[1].Rows[0]["LeaveStatus"].ToString();
                    string leavename = ds.Tables[1].Rows[0]["Leave_Name"].ToString();
                    string leavenamestatus = string.Empty;

                    string Remark = ds.Tables[1].Rows[0]["APR_REMARKS"].ToString();

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
                        successNote = " successfully ";
                    }
                    if (STATUS == "P")
                    {
                        //body = "Hi " + AUTHORITY_NAME + "," + Environment.NewLine + employeename + " " + "has applied " + leavenamestatus + " from " + FromDate
                        //           + " " + "to " + ToDate + " , " + Environment.NewLine + ""
                        //           + "Joining date will be " + Joindt;

                        body = "Hi " + AUTHORITY_NAME + "," + Environment.NewLine + Environment.NewLine + employeename + " of department " + department + " " + "has applied " + leavenamestatus + " for " + tot_days + " days from " + FromDate
                              + " " + "to " + ToDate + " on " + APPDT + " , " + Environment.NewLine + ""
                              + "Joining date will be " + Joindt + "." + Environment.NewLine + Environment.NewLine + "Thanks and Regards" + Environment.NewLine + employeename;

                        ToEmail = ds.Tables[1].Rows[0]["EmailId"].ToString();
                    }
                    else if (STATUS == "A")
                    {
                        body = "Hi " + employeename + "," + Environment.NewLine + " Your applied " + leavenamestatus + " from " + FromDate
                                   + " " + "to " + ToDate + " , " + Environment.NewLine + ""
                                   + " has been " + Approve_Status + " " + successNote + "with remark " + Remark+" .";
                        ToEmail = ds.Tables[1].Rows[0]["EMPLOYEEMAIL"].ToString();
                    }
                    else if (STATUS == "R")
                    {
                        body = "Hi " + employeename + "," + Environment.NewLine + " Your applied " + leavenamestatus + " from " + FromDate
                                   + " " + "to " + ToDate + " , " + Environment.NewLine + ""
                                   + " has been " + Approve_Status + "with remark " + Remark+" .";
                        ToEmail = ds.Tables[1].Rows[0]["EMPLOYEEMAIL"].ToString();
                    }

                    //SmtpClient smtpClient = new SmtpClient();
                    //MailMessage mailMessage = new MailMessage(fromEmailId, toEmailId, Sub, body);
                    //// MailMessage mailMessage = new MailMessage(fromEmailId, "vidisha.kamatkar@mastersofterp.co.in", Sub, body);
                    //smtpClient.Credentials = new System.Net.NetworkCredential(fromEmailId, fromEmailPwd);

                    //smtpClient.EnableSsl = true;
                    //smtpClient.Port = 587; // 587
                    //smtpClient.Host = "smtp.gmail.com";

                    //ServicePointManager.ServerCertificateValidationCallback =
                    //delegate(object s, X509Certificate certificate,
                    //X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    //{ return true; };

                    //smtpClient.Send(mailMessage);// .SendAsync(mailMessage, null);
                    // lblMsg.Text = "Email has been successfully sent..!!";
                    /*
                       text = "Hi " + multipleName + "," + Environment.NewLine + employeename + " " + "has applied a leave from " + FromDate
                                + " " + "to " + ToDate + " " + Environment.NewLine + "with reason " + "'" + txtReason.Text.Trim() + "'" + ","
                                + "Joining date will be " + JoiningDate;
                     * */

                    //string email_type, Link;
                    //DataSet dsMailconfig = getModuleConfig();
                    //if (dsMailconfig != null && dsMailconfig.Tables.Count > 0 && dsMailconfig.Tables[0].Rows.Count > 0)
                    //{
                    //    email_type = dsMailconfig.Tables[0].Rows[0]["EMAIL_TYPE"].ToString();
                    //    Link = dsMailconfig.Tables[0].Rows[0]["LINK"].ToString();

                    //}

                    SendEmailCommon objSendEmail = new SendEmailCommon();
                    int status = 0;
                    try
                    {
                        //status = SendMailBYSendgrid(message, emailid, subject);
                        //status = sendEmail(message, emailid, subject);
                        //Task<int> task = Execute(body, ToEmail, Sub);
                        //status = task.Result;


                        status = objSendEmail.SendEmail(ToEmail, body, Sub);

                    }
                    catch (Exception ex3)
                    {
                        //lbl3.Text = "lbl3  " + "email status " + status + " " + ex3.ToString();
                    }
                }
            }


        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

    }
    protected void btnApproval_Click(object sender, EventArgs e)
    {
        try
        {
            Button btnApproval = sender as Button;
            int LETRNO = int.Parse(btnApproval.CommandArgument);
            Session["LETRNO"] = LETRNO;

            ShowDetails(LETRNO);
            pnllist.Visible = false;
            pnlAdd.Visible = true;
            pnlButton.Visible = true;
            lnkbut.Visible = false;
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.btnApproval_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        try
        {
            Button btnApproval = sender as Button;
            int LETRNO = int.Parse(btnApproval.CommandArgument);
            Session["LETRNO"] = LETRNO;

            ShowDetails(LETRNO);

            pnllist.Visible = false;
            // pnlAdd.Visible = true;
            pnlvedit.Visible = true;
            pnlButton.Visible = true;

            ViewState["action"] = "edit";
            ViewState["ModifyLeave"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.btnApproval_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }

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
                ViewState["No_of_days"] = Convert.ToDouble(ds.Tables[0].Rows[0]["NO_OF_DAYS"].ToString());
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
                    divNoon.Visible = true;
                }
                else
                {
                    divHalfCriteria.Visible = false; rblleavetype.SelectedValue = "0";//FULL DAY
                    divNoon.Visible = false;
                }

               

                if (ds.Tables[4].Rows.Count > 0)
                {
                    lvCLSArrangment.DataSource = ds.Tables[4];
                    lvCLSArrangment.DataBind();
                }
                else
                {
                    lvCLSArrangment.DataSource = null;
                    lvCLSArrangment.DataBind();
                }

                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["IsClassArrangeRequired"].ToString().ToString()) == true)
                {
                    panelArrangement.Visible = true;
                    lvCLSArrangment.DataSource = ds.Tables[4];
                    lvCLSArrangment.DataBind();
                }
                else
                {
                    panelArrangement.Visible = false;
                }

                //string CHARGE_ACCEPT_STATUS = ds.Tables[0].Rows[0]["CHARGE_ACCEPT_STATUS"].ToString();

                //ViewState["CHARGE_ACCEPT_STATUS"] = CHARGE_ACCEPT_STATUS;
                //if (CHARGE_ACCEPT_STATUS == "N".ToString().Trim())
                //{
                //    MessageBox("Sorry ! Reliever Not Yet Accepted the Given Charge");

                //    btnSave.Enabled = false;

                //    pnllist.Visible = true;
                //    pnlAdd.Visible = false;
                //    pnlButton.Visible = false;
                //    return;
                //}
                //else
                //{
                //    btnSave.Enabled = true;
                //    pnllist.Visible = false;
                //    pnlAdd.Visible = true;
                //    pnlButton.Visible = true;
                //}

                lblJoindt.Text = ds.Tables[0].Rows[0]["JOINDT"].ToString();
                lblNoon.Text = ds.Tables[0].Rows[0]["LEAVE_FNAN"].ToString();
                lblReason.Text = ds.Tables[0].Rows[0]["REASON"].ToString();

                //---------------------Newly added  By Shrikant B on 07/09/2021------------------//
                lblDepartment.Text = ds.Tables[0].Rows[0]["SUBDEPT"].ToString();
                lblDesignation.Text = ds.Tables[0].Rows[0]["SUBDESIG"].ToString();
                lblApplydt.Text = ds.Tables[0].Rows[0]["ApplyDate"].ToString();


                lblmodifyDepartment.Text = ds.Tables[0].Rows[0]["SUBDEPT"].ToString();
                lblmodifyDesignation.Text = ds.Tables[0].Rows[0]["SUBDESIG"].ToString();
                //-------------------------------------------------------------------------------//
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
                int RNO = 0;
                RNO = Convert.ToInt32(ds.Tables[0].Rows[0]["RNO"].ToString());
                // int YR = DateTime.Now.Year;
                // DataSet ds1 = objApp.GetLeavesStatus(idno, YR, leaveno);//Session["idno"]
                int YR = 0;// DateTime.Now.Year;
                if (RNO == 0)
                {
                    YR = Convert.ToInt32(ds.Tables[0].Rows[0]["YEAR"].ToString());
                }
                else
                {
                    YR = Convert.ToInt32(objCommon.LookUp("Payroll_leavetran", "isnull(YEAR,0)", "st=2 and Rno=" + RNO));
                }                
                // DataSet ds1 = objApp.GetLeavesStatus(idno, YR, lno);//Session["idno"]
                DataSet ds1 = objApp.GetLeavesStatus(idno, YR, lno);//Session["idno"]
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    double bal = Convert.ToDouble(ds1.Tables[0].Rows[0]["BAL"]);
                    ViewState["LeaveBalance"] = bal;
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
                    //removeItem = ddlSelect.Items.FindByValue("Forward To Next Authority(Recommended)");
                    removeItem = ddlSelect.Items.FindByValue("Approve & Forward To Next Authority(Recommended)");
                    ddlSelect.Items.Remove(removeItem);

                    removeItem = ddlSelectModify.Items.FindByValue("F");
                    ddlSelectModify.Items.Remove(removeItem);
                    //removeItem = ddlSelectModify.Items.FindByValue("Forward To Next Authority(Recommended)");
                    removeItem = ddlSelectModify.Items.FindByValue("Approve & Forward To Next Authority(Recommended)");
                    ddlSelectModify.Items.Remove(removeItem);

                }
                else
                {
                    ddlSelect.Items.Clear();
                    //ddlSelect.Items.Add("Forward To Next Authority(Recommended)");
                    ddlSelect.Items.Add("Approve & Forward To Next Authority(Recommended)");
                    ddlSelect.Items.Add("Approve & Final Submit");
                    ddlSelect.Items.Add("Reject");



                    ddlSelectModify.Items.Clear();
                    //ddlSelectModify.Items.Add("Forward To Next Authority(Recommended)");
                    ddlSelectModify.Items.Add("Approve & Forward To Next Authority(Recommended)");
                    ddlSelectModify.Items.Add("Approve & Final Submit");
                    ddlSelectModify.Items.Add("Reject");

                }

            }

            //// Added By SHrikant Bharne.
            int UA_No = 0;
            if (ds.Tables[1].Rows.Count > 0)
            {
                for (int i = 0; i <= ds.Tables[1].Rows.Count - 1; i++)
                {
                    UA_No = Convert.ToInt32(ds.Tables[1].Rows[i]["UA_No"].ToString());
                }
            }
            int stno = Convert.ToInt32(ViewState["STNO"]);


            if (Convert.ToInt32(Session["userno"]) == UA_No)     //// if (Convert.ToInt32(Session["userno"]) == UA_No)
            {
                ListItem removeItem = ddlSelect.Items.FindByValue("F");
                ddlSelect.Items.Remove(removeItem);
                //removeItem = ddlSelect.Items.FindByValue("Forward To Next Authority(Recommended)");
                removeItem = ddlSelect.Items.FindByValue("Approve & Forward To Next Authority(Recommended)");
                ddlSelect.Items.Remove(removeItem);
            }
            else
            {
                ListItem removeItem = ddlSelect.Items.FindByValue("A");
                ddlSelect.Items.Remove(removeItem);
                removeItem = ddlSelect.Items.FindByValue("Approve & Final Submit");
                ddlSelect.Items.Remove(removeItem);
            }

            if (ds.Tables[3].Rows.Count > 0)
            {
                char CLASS_ARRAN_STATUS = Convert.ToChar(ds.Tables[3].Rows[0]["CLASS_ARRAN_STATUS"].ToString());
                ViewState["CLASS_ARRAN_STATUS"] = CLASS_ARRAN_STATUS;
                if (CLASS_ARRAN_STATUS == 'P')
                {
                    MessageBox("Sorry ! Class Reliver Not Yet Accepted the Given Charge");
                    btnSave.Enabled = false;

                    pnllist.Visible = true;
                    pnlAdd.Visible = false;
                    pnlButton.Visible = false;
                    return;
                }
                else if (CLASS_ARRAN_STATUS == 'R')
                {
                    MessageBox("Sorry ! Class Reliver Not Yet Accepted the Given Charge");
                    btnSave.Enabled = false;

                    pnllist.Visible = true;
                    pnlAdd.Visible = false;
                    pnlButton.Visible = false;
                    return;
                }
                else
                {
                    btnSave.Enabled = true;
                    pnllist.Visible = false;
                    pnlAdd.Visible = true;
                    pnlButton.Visible = true;
                }
            }
            else
            {

            }

           

            ////

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
        pnlButton.Visible = false;

        lnkbut.Visible = true;
    }

    protected void lnkbut_Click(object sender, EventArgs e)
    {
        pnlODStatus.Visible = true;
        pnlAdd.Visible = false;
        pnllist.Visible = false;
        trfrmto.Visible = true;
        trbutshow.Visible = true;
        this.BindLVLeaveApprStatusAll();
        pnlButton.Visible = false;
        lnkbut.Visible = false;
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
                //MessageBox("Not Allowed ,Please check To Date. Todate is not more than before " + days + " day of today");
                //btnSave.Enabled = false;

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
                    if (totdays > 0.50)
                    {
                        rblleavetype.SelectedValue = "0";
                    }
                    else
                    {
                        rblleavetype.SelectedValue = "1";
                    }
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

    protected void txtfrmdt_TextChanged(object sender, EventArgs e)
    {
        if (txttodate.Text.ToString() != string.Empty && txttodate.Text.ToString() != "__/__/____" && txtfrmdt.Text.ToString() != string.Empty && txtfrmdt.Text.ToString() != "__/__/____")
        {
            DateTime fdt = Convert.ToDateTime(txtfrmdt.Text);
            DateTime tdt = Convert.ToDateTime(txttodate.Text);
            if (tdt < fdt)
            {
                //MessageBox("Todate must be greater than Fromdate");
                txttodate.Text = string.Empty;
            }
        }
        else
        {

        }
    }
    protected void btnRPT_Click(object sender, EventArgs e)
    {
        try
        {

            Button btnApply = sender as Button;
            int LNO = int.Parse(btnApply.CommandArgument);
            int letrno = int.Parse(btnApply.ToolTip.ToString());
            ViewState["letrno"] = letrno;
            ViewState["LNO"] = LNO;
            DataSet ds1 = objCommon.FillDropDown("PAYROLL_EMPMAS EMP INNER JOIN PAYROLL_LEAVE_APP_ENTRY LA ON (LA.EMPNO=EMP.IDNO)", "EMP.COLLEGE_NO,EMP.SUBDEPTNO,EMP.STNO,EMP.STAFFNO", "EMP.IDNO", "LA.LETRNO=" + letrno + "", "");
            if (ds1.Tables[0].Rows.Count > 0)
            {

                ViewState["STNO"] = ds1.Tables[0].Rows[0]["STNO"].ToString();
                ViewState["IDNO"] = ds1.Tables[0].Rows[0]["IDNO"].ToString();

            }

            ShowReport("Application_form", "leave_application.rpt");

        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Application.btnApply_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");

        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {

            //int idno = Convert.ToInt32(Session["idno"]);
            //int staffno =Convert.ToInt32(objCommon .LookUp ("PAYROLL_EMPMAS","STAFFNO","IDNO="+idno));
            //GetStudentIDs();
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;

            //int idnorpt = Convert.ToInt32(Session["idno"]);
            //int stnorpt = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS","STNO","IDNO="+idnorpt));
            int stnorpt = Convert.ToInt32(ViewState["STNO"]);
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + GetStudentIDs() + ",UserName=" + Session["username"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(Session["currentsession"]);@P_IDNO
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_PREVSTATUS=" + Convert.ToInt32(ddlExamType.SelectedValue);
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_EMPNO=" + Convert.ToInt32(ViewState["IDNO"]) + "," + "@P_LNO=" + Convert.ToInt32(ViewState["LNO"].ToString()) + "," + "@P_LETRNO=" + Convert.ToInt32(ViewState["letrno"].ToString()) + "," + "@username=" + Session["userfullname"].ToString() + "," + "@P_STNO=" + stnorpt;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_EMPNO=" + Convert.ToInt32(Session["idno"]) + "," + "@P_LNO=" + Convert.ToInt32(ViewState["LNO"].ToString()) + "," + "@P_LETRNO=" + Convert.ToInt32(ViewState["LETRNO"].ToString()) + "," + "@username=" + Session["userfullname"].ToString() + "," + "@P_STNO=" + stnorpt + " ," + "@P_TRANNO=1" +", @P_IDNO="+ Convert.ToInt32(Session["idno"])  ;
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            //Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Comparative.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
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


    public void SendSMSTEMPLATE(string mobno, string message, string TemplateID = "")
    {
        try
        {
            string url = string.Empty;
            string uid = string.Empty;
            string pass = string.Empty;
            DataSet ds = objCommon.FillDropDown("Reff", "SMSProvider", "SMSSVCID,SMSSVCPWD", "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                url = string.Format("http://" + ds.Tables[0].Rows[0]["SMSProvider"].ToString() + "?");
                uid = ds.Tables[0].Rows[0]["SMSSVCID"].ToString();
                pass = ds.Tables[0].Rows[0]["SMSSVCPWD"].ToString();
                WebRequest request = HttpWebRequest.Create("" + url + "ID=" + uid + "&PWD=" + pass + "&PHNO=" + mobno + "&TEXT=" + message + "&TemplateID=" + TemplateID + "");
                WebResponse response = request.GetResponse();
                System.IO.StreamReader reader = new StreamReader(response.GetResponseStream());
                string urlText = reader.ReadToEnd(); // it takes the response from your url. now you can use as your need      
                //return urlText;  
                Session["result"] = 1;
            }
        }
        catch (Exception)
        {
        }
    }

    static async Task<int> Execute(string Message, string toEmailId, string sub)
    {
        int ret = 0;

        try
        {

            Common objCommon = new Common();
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY,SUBJECT_OTP", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);
            var fromAddress = new System.Net.Mail.MailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), "SBU");
            var toAddress = new System.Net.Mail.MailAddress(toEmailId, "");

            var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString());
            var subject = sub;
            var to = new EmailAddress(toEmailId, "");
            var plainTextContent = "";
            var htmlContent = Message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            string res = Convert.ToString(response.StatusCode);
            if (res == "Accepted")
            {
                ret = 1;
            }
            else
            {
                ret = 0;
            }


        }
        catch (Exception ex)
        {
            ret = 0;
        }
        return ret;
    }

    
   
   


}
