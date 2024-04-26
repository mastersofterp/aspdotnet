using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data.SqlClient;
using SendGrid;
using SendGrid.Helpers.Mail;
using EASendMail;
using System.Net;
using System.Net.Mail;
using BusinessLogicLayer.BusinessLogic;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;

public partial class HOSTEL_GATEPASS_HostelGatePassRequest : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    GatePassRequest objGatePass = new GatePassRequest();
    GatePassRequestController objGPR = new GatePassRequestController();
    SendEmailCommon objSendEmail = new SendEmailCommon();

    //below code added by Himanshu Tamrakar 05042024
    DateTime Fromdate = DateTime.Now.AddDays(-1);
    DateTime Todate = DateTime.Now.AddDays(7);

    string gatepass_no;
    #region Page Events
    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                    this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                }
                if (Convert.ToInt16(Session["usertype"]) == 1)
                {
                    adminsearch.Visible = true;
                    pnlStudentHGPRequestDetails.Visible = false;
                    pnlbuttons.Visible = false;
                }
                //objCommon.FillDropDownList(ddlSearch, "ACD_STUDENT", "IDNO", "STUDNAME", "HOSTELER=1 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "STUDNAME");
                objCommon.FillDropDownList(ddlSearch, " ACD_STUDENT S INNER JOIN ACD_HOSTEL_ROOM_ALLOTMENT R ON S.IDNO=R.RESIDENT_NO ", "DISTINCT S.IDNO", "S.STUDNAME", " S.HOSTELER=1 AND S.OrganizationId=" + Convert.ToInt32(Session["OrgId"]) + " AND  R.HOSTEL_SESSION_NO IN (SELECT MAX(HOSTEL_SESSION_NO) FROM  ACD_HOSTEL_SESSION WHERE FLOCK=1) AND R.CAN=0", "STUDNAME"); //Added By Himanshu tamrakar on date 01/04/2024
                string HostelNo = objCommon.LookUp("ACD_HOSTEL_ROOM_ALLOTMENT", "HOSTEL_NO", "RESIDENT_NO=" + Convert.ToInt32(Session["idno"]) + " and  CAN=0 AND HOSTEL_SESSION_NO=" + Convert.ToInt32(Session["hostel_session"]));
                // objCommon.FillDropDownList(ddlPurposeSearch, "ACD_HOSTEL_PURPOSE_MASTER", "PURPOSE_NO", "PURPOSE_NAME", "ISACTIVE=1", "PURPOSE_NO");


                objCommon.FillDropDownList(ddlStuType, "ACD_HOSTEL_STUDENT_TYPE", "STUDENT_TYPE_ID", "STUDENT_TYPE", "STUDENT_TYPE_ID>0", "STUDENT_TYPE_ID");

                if (Session["usertype"].ToString().Equals("2"))
                {
                    string HostelNum = objCommon.LookUp("ACD_HOSTEL_ROOM_ALLOTMENT", "HOSTEL_NO", "RESIDENT_NO=" + Convert.ToInt32(Session["idno"]) + " and  CAN=0 AND HOSTEL_SESSION_NO=" + Convert.ToInt32(Session["hostel_session"]));

                    if (HostelNum == null || HostelNum == "")
                    {
                        objCommon.DisplayMessage("Room Alloted not found in current Hostel Session. Due to that reason You are not eligible for Hostel Gate Pass.", this.Page);
                        btnSubmit.Enabled = false;
                    }
                    else
                    {
                        objCommon.FillDropDownList(ddlHostel, "ACD_HOSTEL", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO = " + Convert.ToInt32(HostelNum) + " AND HOSTEL_NO>0", "HOSTEL_NO");
                        ddlHostel.SelectedValue = HostelNum;
                        ddlHostel.Enabled = false;
                    }

                }
                else
                {
                    objCommon.FillDropDownList(ddlHostel, "ACD_HOSTEL", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO>0", "HOSTEL_NO");
                }


                PopulateDropDownList();

                //commented and added by Himanshu Tamrakar
                //BindListView():
                BindListView(null, 0, Convert.ToString(DateTime.Parse(Convert.ToString(Todate)).ToString("yyyy-MM-dd")), Convert.ToString(DateTime.Parse(Convert.ToString(Fromdate)).ToString("yyyy-MM-dd")), "0");

                ViewState["action"] = "add";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Master_GatePassRequest.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion Page Events

    #region Action


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objGatePass.StudType = Convert.ToInt32(ddlStuType.SelectedValue);
            //objGatePass.ApprPath = path.InnerText;
            objGatePass.OutDate = DateTime.Parse(txtoutDate.Text.Trim());
            objGatePass.OutHourFrom = Convert.ToInt32(ddloutHourFrom.SelectedValue);
            objGatePass.OutMinFrom = Convert.ToInt32(ddloutMinFrom.SelectedValue);
            objGatePass.OutAMPM = ddlAM_PM1.SelectedValue;
            objGatePass.InDate = DateTime.Parse(txtinDate.Text.Trim());
            objGatePass.InHourFrom = Convert.ToInt32(ddlinHourFrom.SelectedValue);
            objGatePass.InMinFrom = Convert.ToInt32(ddlinMinFrom.SelectedValue);
            objGatePass.InAMPM = ddlAM_PM2.SelectedValue;
            objGatePass.PurposeID = Convert.ToInt32(ddlPurpose.SelectedValue);
            objGatePass.PurposeOther = txtOther.Text.Trim();
            objGatePass.Remarks = txtRemark.Text;
            objGatePass.IDNO = Convert.ToInt32(Session["idno"]);
            objGatePass.CollegeCode = Session["colcode"].ToString();
            objGatePass.organizationid = Session["OrgId"].ToString();

            if (ddlinHourFrom.SelectedValue == "0")
            {
                objCommon.DisplayMessage("Please Select In Hour From", this.Page);
                return;
            }
            if (ddloutHourFrom.SelectedValue == "0")
            {
                objCommon.DisplayMessage("Please Select Out Hour From", this.Page);
                return;
            }
            if (Convert.ToInt32(Session["usertype"]) == 1)
            {
                objGatePass.Admin_UANO = Convert.ToInt32(Session["userno"]);
            }

            if (ViewState["action"].ToString().Equals("add"))
            {
                if (CheckDuplicateEntry() == true)
                {
                    objCommon.DisplayMessage("Entry for this Selection Already Done.", this.Page);
                    return;
                }

                int cs = objGPR.Insert_Update_GatePassRequest(objGatePass, Convert.ToInt32(ddlHostel.SelectedValue));
                if (cs == 1)
                {
                    objCommon.DisplayMessage("Record Saved Successfully.", this.Page);
                    ViewState["action"] = "add";
                    Clear();
                    Sendmail();
                }
                //if (cs == 4)
                //{
                //    objCommon.DisplayMessage("Parent Login Not Found. Contact to Administrator.", this.Page);
                //    ViewState["action"] = "add";
                //}
                else if (cs == 7)
                {
                    objCommon.DisplayMessage("You Already Applied Gate Pass For Selected Date.", this.Page); //Added By himanshu tamrakar 07-03-2024
                    ViewState["action"] = "add";
                    Clear();
                }
                else if (cs == -99)
                {
                    objCommon.DisplayMessage("Passing path Not found. Contact to Administrator.", this.Page);
                    ViewState["action"] = "add";
                    Clear();
                }
            }
            else
            {
                //Edit
                if (ViewState["gatepass_no"] != null)
                {
                    objGatePass.GatePassNo = Convert.ToInt32(ViewState["gatepass_no"].ToString());

                    if (CheckDuplicateEntry() == true)
                    {
                        objCommon.DisplayMessage("Entry for this Selection Already Done.", this.Page);
                        return;
                    }

                    int cs = objGPR.Insert_Update_GatePassRequest(objGatePass, Convert.ToInt32(ddlHostel.SelectedValue));
                    if (cs == 2)
                    {
                        objCommon.DisplayMessage("Record Updated Successfully.", this.Page);
                        ViewState["action"] = "add";
                        Clear();
                    }
                    else if (cs == 7)
                    {
                        objCommon.DisplayMessage("You Already Applied Gate Pass For Selected Date.", this.Page);  //Added By himanshu tamrakar 07-03-2024
                        ViewState["action"] = "add";
                        Clear();
                    }
                    else if (cs == -99)
                    {
                        objCommon.DisplayMessage("Passing path Not found.Contact to Administrator.", this.Page);
                        ViewState["action"] = "add";
                        Clear();
                    }
                }
            }

            //commented and added by Himanshu Tamrakar 05042024
            //BindListView();
            BindListView(null, 0, Convert.ToString(DateTime.Parse(Convert.ToString(Todate)).ToString("yyyy-MM-dd")), Convert.ToString(DateTime.Parse(Convert.ToString(Fromdate)).ToString("yyyy-MM-dd")), "0");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Masters_Asset.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Clear();
            if (Session["usertype"].ToString().Equals("1"))
            {
                pnlStudentHGPRequestDetails.Visible = false;
            }
            btnSubmit.Text = "Update";
            ImageButton btnEdit = sender as ImageButton;
            int gatepass_no = int.Parse(btnEdit.CommandArgument);

            string IsApprove = objCommon.LookUp("ACD_HOSTEL_GATEPASS_DETAILS", "HOSTEL_GATE_PASS_NO", "FINAL_STATUS = 'A' AND HGP_ID=" + gatepass_no);

            if (!string.IsNullOrEmpty(IsApprove))
            {
                objCommon.DisplayMessage("You can not modify gatepass request after approval.", this.Page);
                Clear();
                return;

            }
            else
            {
                ShowDetail(gatepass_no);
                pnlStudentHGPRequestDetails.Visible = true;
                ViewState["action"] = "edit";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "GatePass.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void ddlPurpose_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPurpose.SelectedItem.Text.ToUpper() == "OTHERS")
        {
            txtOther.Visible = true;
        }
        else
        {
            txtOther.Visible = false;
        }
    }

    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearch.SelectedValue == "0")
        {
            objCommon.DisplayMessage("Please Select valid Student", this);
            return;
        }
        Session["idno"] = ddlSearch.SelectedValue;

        if (ddlSearch.SelectedValue != null && ddlSearch.SelectedValue != "")
        {
            pnlStudentHGPRequestDetails.Visible = true;
            pnlbuttons.Visible = true;

            //Below code Added By Himanshu tamrakar 01/04/2024
            string HostelNo = objCommon.LookUp("ACD_HOSTEL_ROOM_ALLOTMENT", "HOSTEL_NO", "RESIDENT_NO=" + Convert.ToInt32(Session["idno"]) + " and  CAN=0 AND HOSTEL_SESSION_NO=" + Convert.ToInt32(Session["hostel_session"]));
            objCommon.FillDropDownList(ddlHostel, "ACD_HOSTEL", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO = " + Convert.ToInt32(HostelNo) + " AND HOSTEL_NO>0", "HOSTEL_NO");
            ddlHostel.SelectedValue = HostelNo;
            ddlHostel.Enabled = false;

        }
        if (ddlSearch.SelectedIndex == 0)
        {
            pnlStudentHGPRequestDetails.Visible = false;
            pnlbuttons.Visible = false;
        }
    }

    protected void txtoutDate_TextChanged(object sender, EventArgs e)
    {
        if (txtoutDate.Text != "" && txtoutDate.Text != string.Empty)
        {
            if (Convert.ToDateTime(txtoutDate.Text) < System.DateTime.Now.AddDays(-1))
            {
                string message = "Please Select 'Out Date' as Current Date or Greater than Current Date.";
                string encodedMessage = HttpUtility.JavaScriptStringEncode(message);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + encodedMessage + "');", true);
                txtoutDate.Text = string.Empty;
                txtoutDate.Focus();
                return;
            }
        }

        if (txtoutDate.Text != "" && txtoutDate.Text != string.Empty && txtinDate.Text != "" && txtinDate.Text != string.Empty)
        {
            if (Convert.ToDateTime(txtoutDate.Text) > Convert.ToDateTime(txtinDate.Text))
            {
                objCommon.DisplayMessage("Out Date should be less than In Date.", this.Page);
                txtoutDate.Text = string.Empty;
                txtoutDate.Focus();
                return;
            }
        }


    }

    protected void txtinDate_TextChanged(object sender, EventArgs e)
    {
        if (txtinDate.Text != "" && txtinDate.Text != string.Empty)
        {
            if (Convert.ToDateTime(txtinDate.Text) < System.DateTime.Now.AddDays(-1))
            {
                string message = "Please select 'In Date' as Current Date or Greater than current Date.";
                string encodedMessage = HttpUtility.JavaScriptStringEncode(message);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + encodedMessage + "');", true);
                txtinDate.Text = string.Empty;
                txtinDate.Focus();
                return;
            }
        }

        if (txtoutDate.Text != "" && txtoutDate.Text != string.Empty && txtinDate.Text != "" && txtinDate.Text != string.Empty)
        {
            DateTime OutDate = Convert.ToDateTime(txtoutDate.Text);
            DateTime InDate = Convert.ToDateTime(txtinDate.Text);

            bool res = DateTime.Equals(OutDate, InDate);
            if (res)
            {
                if (ddlAM_PM1.SelectedValue == ddlAM_PM2.SelectedValue && ddlAM_PM1.SelectedIndex != 0 && ddlAM_PM2.SelectedIndex != 0)
                {
                    if (Convert.ToInt32(ddloutHourFrom.SelectedValue) > Convert.ToInt32(ddlinHourFrom.SelectedValue))
                    {
                        objCommon.DisplayMessage("Hour From should not be greater than Hour To.", this.Page);
                        ddloutHourFrom.Focus();
                        ddloutHourFrom.SelectedValue = "0";
                        txtinDate.Text = string.Empty;
                        return;
                    }
                }
            }
            else
            {
                if (Convert.ToDateTime(txtoutDate.Text) > Convert.ToDateTime(txtinDate.Text))
                {
                    objCommon.DisplayMessage("In Date should be greater than Out Date.", this.Page);
                    txtinDate.Text = string.Empty;
                    txtinDate.Focus();
                    return;
                }
            }
        }
    }
    #endregion Action

    #region Private Methods
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=HostelGatePassRequest.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=HostelGatePassRequest.aspx");
        }
    }

    protected void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlPurpose, "ACD_HOSTEL_PURPOSE_MASTER", "PURPOSE_NO", "PURPOSE_NAME", "ISACTIVE=1", "");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "HOSTEL_RoomAllotmentStatus.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private bool CheckDuplicateEntry()
    {
        bool flag = false;
        try
        {
            string gpr = objCommon.LookUp("ACD_HOSTEL_GATEPASS_DETAILS", "HGP_ID", "OUTDATE=" + txtoutDate.Text + " and  IDNO ='" + Convert.ToInt32(Session["idno"]) + "'");
            if (gpr != null && gpr != string.Empty)
            {
                flag = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "GatePassRequest.CheckDuplicateEntry() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return flag;
    }

    private void Clear()
    {
        btnSubmit.Text = "Submit";

        txtoutDate.Text = string.Empty;
        ddloutHourFrom.SelectedIndex = 0;
        ddloutMinFrom.SelectedIndex = 0;
        txtinDate.Text = string.Empty;
        ddlinHourFrom.SelectedIndex = 0;
        ddlinMinFrom.SelectedIndex = 0;
        ddlAM_PM1.SelectedIndex = 0;
        ddlAM_PM2.SelectedIndex = 0;
        ddlPurpose.SelectedIndex = 0;
        txtOther.Text = string.Empty;
        txtRemark.Text = string.Empty;
        ddlStuType.SelectedIndex = 0;
        ddlHostel.SelectedIndex = 0;
        ddlSearch.SelectedIndex = 0;

        if (Session["usertype"].ToString().Equals("2"))
        {
            string HostelNum = objCommon.LookUp("ACD_HOSTEL_ROOM_ALLOTMENT", "HOSTEL_NO", "RESIDENT_NO=" + Convert.ToInt32(Session["idno"]) + " and  CAN=0 AND HOSTEL_SESSION_NO=" + Convert.ToInt32(Session["hostel_session"]));

            if (HostelNum == null || HostelNum == "")
            {
                objCommon.DisplayMessage("Room Alloted not found in current Hostel Session. Due to that reason You are not eligible for Hostel Gate Pass.", this.Page);
                btnSubmit.Enabled = false;
            }
            else
            {
                objCommon.FillDropDownList(ddlHostel, "ACD_HOSTEL", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO = " + Convert.ToInt32(HostelNum) + " AND HOSTEL_NO>0", "HOSTEL_NO");
                ddlHostel.SelectedValue = HostelNum;
                ddlHostel.Enabled = false;
            }

        }
    }

    //private void BindListView()
    //{
    //    try
    //    {
    //        DataSet ds = objGPR.GetAllGatePass();
    //        lvGatePass.DataSource = ds;
    //        lvGatePass.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUaimsCommon.ShowError(Page, "Hostel_Masters_HostelPurpose.BindListView --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUaimsCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}
    //below code added by Himanshu tamrakar 05042024
    private void BindListView(string Applydate, int Purpose, string Todate, string Fromdate, string Status)
    {
        try
        {
            DataSet ds = objGPR.GetAllGatePass(Applydate, Purpose, Todate, Fromdate, Status);
            lvGatePass.DataSource = ds;
            lvGatePass.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Masters_HostelPurpose.BindListView --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetail(int GatePassRequestNo)
    {
        DataSet ds = objGPR.GetGatePass(GatePassRequestNo);

        DataRow dr = ds.Tables[0].Rows[0];

        //Show Detail
        if (dr != null)
        {
            ViewState["gatepass_no"] = GatePassRequestNo.ToString();
            ddlStuType.SelectedValue = dr["STUDENT_TYPE"] == null ? string.Empty : dr["STUDENT_TYPE"].ToString();
            ddlHostel.SelectedValue = dr["HOSTEL_NO"] == null ? string.Empty : dr["HOSTEL_NO"].ToString();
            txtoutDate.Text = dr["OUTDATE"] == null ? string.Empty : dr["OUTDATE"].ToString();
            ddloutHourFrom.SelectedValue = dr["OUT_HOUR_FROM"] == null ? string.Empty : dr["OUT_HOUR_FROM"].ToString();
            ddloutMinFrom.SelectedValue = dr["OUT_MIN_FROM"] == null ? string.Empty : dr["OUT_MIN_FROM"].ToString();
            ddlAM_PM1.SelectedValue = dr["OUT_AM_PM"] == null ? string.Empty : dr["OUT_AM_PM"].ToString();
            txtinDate.Text = dr["INDATE"] == null ? string.Empty : dr["INDATE"].ToString();
            ddlinHourFrom.SelectedValue = dr["IN_HOUR_FROM"] == null ? string.Empty : dr["IN_HOUR_FROM"].ToString();
            ddlinMinFrom.SelectedValue = dr["IN_MIN_FROM"] == null ? string.Empty : dr["IN_MIN_FROM"].ToString();
            ddlAM_PM2.SelectedValue = dr["IN_AM_PM"] == null ? string.Empty : dr["IN_AM_PM"].ToString();
            ddlPurpose.SelectedValue = dr["PURPOSE_ID"] == null ? string.Empty : dr["PURPOSE_ID"].ToString();
            txtOther.Text = dr["PURPOSE_OTHER"] == null ? string.Empty : dr["PURPOSE_OTHER"].ToString();
            txtRemark.Text = dr["REMARKS"] == null ? string.Empty : dr["REMARKS"].ToString();
            //ddlSearch.SelectedValue = dr["STUDNAME"] == null ? string.Empty : dr["STUDNAME"].ToString();
            ddlSearch.SelectedItem.Text = dr["STUDNAME"] == null ? string.Empty : dr["STUDNAME"].ToString();
        }
    }

    private DataSet getModuleConfig()
    {
        DataSet ds = objCommon.GetModuleConfig(Convert.ToInt32(Session["OrgId"]));
        return ds;
    }

    public void Sendmail()
    {
        string email_type = string.Empty;
        string Link = string.Empty;
        int sendmail = 0;
        string subject = string.Empty;
        string srnno = string.Empty;
        string pwd = string.Empty;
        int status = 0;
        string IDNO = Session["IDNO"].ToString();

        string MISLink = objCommon.LookUp("ACD_MODULE_CONFIG", "ONLINE_ADM_LINK", "OrganizationId=" + Session["OrgId"]);

        string Username = string.Empty;
        string Password = string.Empty;

        string Name = objCommon.LookUp("ACD_STUDENT", "STUDNAME", "IDNO=" + Convert.ToInt32(Session["IDNO"]));
        string Father_Name = objCommon.LookUp("ACD_STUDENT", "FATHERFIRSTNAME", "IDNO=" + Convert.ToInt32(Session["IDNO"]));
        string Branchname = objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_DEGREE D ON (S.DEGREENO=D.DEGREENO) INNER JOIN ACD_BRANCH B ON (B.BRANCHNO=S.BRANCHNO)", "CONCAT(D.DEGREENAME, ' in ',B.LONGNAME)", "IDNO=" + Session["IDNO"].ToString());

        string REGNO = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + Convert.ToInt32(Session["IDNO"]));
        string EmailID = objCommon.LookUp("ACD_STUDENT", "EMAILID", "IDNO=" + Convert.ToInt32(Session["IDNO"]));
        string FatherID = objCommon.LookUp("ACD_STUDENT", "FATHER_EMAIL", "IDNO=" + Convert.ToInt32(Session["IDNO"]));
        string MotherID = objCommon.LookUp("ACD_STUDENT", "MOTHER_EMAIL", "IDNO=" + Convert.ToInt32(Session["IDNO"]));
        string college = objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_COLLEGE_MASTER M ON(S.COLLEGE_ID=M.COLLEGE_ID)", "M.COLLEGE_NAME", "IDNO=" + Convert.ToInt32(Session["IDNO"]));

        string Reason = objCommon.LookUp("ACD_HOSTEL_GATEPASS_DETAILS HGD INNER JOIN ACD_HOSTEL_PURPOSE_MASTER HPM ON(HGD.PURPOSE_ID=HPM.PURPOSE_NO)", "HPM.PURPOSE_NAME", "IDNO=" + Convert.ToInt32(Session["IDNO"]));
        string OutDateTime = objCommon.LookUp("ACD_HOSTEL_GATEPASS_DETAILS", "CONCAT(CONVERT(varchar, CAST(OUTDATE AS date), 103), '  ', OUT_HOUR_FROM, ' : ', OUT_MIN_FROM, '  ', OUT_AM_PM)", "IDNO=" + Convert.ToInt32(Session["IDNO"]));
        string InDateTime = objCommon.LookUp("ACD_HOSTEL_GATEPASS_DETAILS", "CONCAT(CONVERT(varchar, CAST(INDATE AS date), 103), '  ', IN_HOUR_FROM, ' : ', IN_MIN_FROM, '  ', IN_AM_PM)", "IDNO=" + Convert.ToInt32(Session["IDNO"]));

        Username = REGNO;
        Password = REGNO;

        Session["Enrollno"] = srnno;
        DataSet ds = getModuleConfig();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            email_type = ds.Tables[0].Rows[0]["EMAIL_TYPE"].ToString();
            Link = ds.Tables[0].Rows[0]["LINK"].ToString();
            sendmail = Convert.ToInt32(ds.Tables[0].Rows[0]["THIRDPARTY_PAYLINK_MAIL_SEND"].ToString());

            if (sendmail == 0)
            {
                subject = "Gate Pass Request - " + Name;

                string message = "";
                message += "<p>Dear :<b>" + Father_Name + "</b> </p>";
                message += "<p>We hope this message finds you well.</p>";
                message += "<p>We wanted to inform you that your son, [" + Name + "], who is a resident at our hostel, has submitted a request for a gate pass to temporarily leave the hostel premises.</p>";
                message += "<p>The reason for his request: [" + Reason + "].</p>";
                message += "<p>Date and Time of Departure: [" + OutDateTime + "].</p>";
                message += "<p>Date and Time of Return: [" + InDateTime + "].</p>";
                message += "<p>If you have any questions, concerns, or if you need any further information regarding this request, please do not hesitate to reach out to us.</p>";
                message += "<p>We appreciate your understanding and cooperation in this matter.</p>";
                message += "<p>Thank you.</p>";

                status = objSendEmail.SendEmail(FatherID, message, subject);

                // status = objCommon.sendEmail(message, FatherID, subject);
            }
        }

        if (status == 1)
        {
            objCommon.DisplayMessage(this.Page, "Email sent Successfully.", this.Page);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "functionConfirm", "confirmmsg();", true);
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Failed to send mail.", this.Page);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "functionConfirm", "confirmmsg();", true);
        }

    }

    #endregion Private Methods
    protected void ddlAM_PM2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtoutDate.Text != "" && txtoutDate.Text != string.Empty && txtinDate.Text != "" && txtinDate.Text != string.Empty)
        {
            DateTime OutDate = Convert.ToDateTime(txtoutDate.Text);
            DateTime InDate = Convert.ToDateTime(txtinDate.Text);
            if (ddlAM_PM2.SelectedValue == "PM")  //Added By Himanshu Tamrakar 06-03-2024
            {
                ddlinHourFrom.SelectedValue = "11";
                ddlinMinFrom.SelectedValue = "59";
            }

            bool res = DateTime.Equals(OutDate, InDate);
            if (res)
            {
                //Commented By Himanshu tamrakar 29/11/2023
                //if (ddlAM_PM1.SelectedValue == ddlAM_PM2.SelectedValue)
                //{
                //    if (ddloutHourFrom.SelectedValue=="0")
                //    {

                //    }
                //    else if (Convert.ToInt32(ddloutHourFrom.SelectedValue) >= Convert.ToInt32(ddlinHourFrom.SelectedValue))
                //    {
                //        objCommon.DisplayMessage("Hour To should be greater than Hour From.", this.Page);
                //        ddlAM_PM2.SelectedValue = "0";
                //        ddlinHourFrom.Focus();
                //        return;
                //    }
                //}
                if (ddlAM_PM1.SelectedValue == "PM")
                {
                    if (ddlAM_PM2.SelectedValue == "AM")
                    {
                        objCommon.DisplayMessage("For Current Date, If you select Out Date PM then you can not select AM time of In Date.", this.Page);
                        ddlAM_PM2.Focus();
                        ddlAM_PM2.SelectedIndex = 0;
                        return;
                    }
                }
                if (ddlAM_PM2.SelectedValue == "AM") //Added By himanshu tamrakar
                {
                    if (ddlAM_PM1.SelectedValue == "AM")
                    {
                        if (Convert.ToInt32(ddlinHourFrom.SelectedValue) <= Convert.ToInt32(ddloutHourFrom.SelectedValue))
                        {
                            objCommon.DisplayMessage("Hour To should be greater than Hour From", this.Page);
                            ddloutHourFrom.SelectedValue = "11";
                            ddloutMinFrom.SelectedValue = "59";
                            ddlAM_PM2.Focus();
                        }
                    }
                    else
                    {

                    }
                }
            }

        }
        else
        {
            if (txtinDate.Text == "" && txtinDate.Text == string.Empty)
            {
                objCommon.DisplayMessage("Please Select In Date First.", this.Page);
                return;
            }
        }
    }
    protected void ddlinHourFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtoutDate.Text != "" && txtoutDate.Text != string.Empty && txtinDate.Text != "" && txtinDate.Text != string.Empty)
        {
            string currentDate = System.DateTime.Now.ToString("dd/MM/yyyy");

            DateTime OutDate = Convert.ToDateTime(txtoutDate.Text);
            DateTime InDate = Convert.ToDateTime(txtinDate.Text);
            if (ddlAM_PM1.SelectedValue == "PM")
            {
                if (ddlinHourFrom.SelectedValue == "0")
                {
                    objCommon.DisplayMessage("For PM you can not select 00 for In Hour From.", this.Page);
                    int hour;
                    if (Convert.ToInt32(ddloutHourFrom.SelectedValue) >= 0)
                    {
                        if (Convert.ToInt32(ddloutHourFrom.SelectedValue) > 12)
                        {
                            hour = Convert.ToInt32(ddloutHourFrom.SelectedValue);
                            hour = hour - 12;
                        }
                        else
                        {
                            hour = Convert.ToInt32(ddloutHourFrom.SelectedValue);
                        }
                    }
                    else
                    {
                        hour = DateTime.Now.Hour;
                        if (hour > 12)
                        {
                            hour = hour - 12;
                        }
                    }
                    ddlinHourFrom.SelectedValue = Convert.ToString(hour + 1);
                    ddlinHourFrom.Focus();
                    return;
                }
            }
            if (InDate == Convert.ToDateTime(currentDate))
            {
                if (ddlAM_PM2.SelectedValue == "0")
                {
                    string message = "For current date entry.Please select AM/PM before entering Hour To.";
                    string encodedMessage = HttpUtility.JavaScriptStringEncode(message);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + encodedMessage + "');", true);
                    ddlinHourFrom.SelectedIndex = 0;
                    ddlAM_PM2.Focus();
                    return;
                }
            }


            bool res = DateTime.Equals(OutDate, InDate);
            if (res)
            {
                if (ddlAM_PM1.SelectedValue == ddlAM_PM2.SelectedValue)
                {
                    if (ddloutHourFrom.SelectedValue == "12")
                    {
                        if (ddlinHourFrom.SelectedValue == "12")
                        {
                            objCommon.DisplayMessage("Hour To should be greater than Hour From.", this.Page);
                            ddlinHourFrom.SelectedIndex = 0;
                            ddlinHourFrom.Focus();
                            return;
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(ddloutHourFrom.SelectedValue) >= Convert.ToInt32(ddlinHourFrom.SelectedValue))
                        {
                            objCommon.DisplayMessage("Hour To should be greater than Hour From.", this.Page);
                            ddlinHourFrom.SelectedIndex = 0;
                            ddlinHourFrom.Focus();
                            return;
                        }
                    }
                }
                else
                {
                    if (ddlAM_PM1.SelectedValue == "PM")
                    {
                        if (ddlAM_PM2.SelectedValue == "AM")
                        {
                            objCommon.DisplayMessage("For Current Date, If you select Out Date PM then you can not select AM time of In Date.", this.Page);
                            ddlAM_PM2.Focus();
                            ddlAM_PM2.SelectedIndex = 0;
                            return;
                        }
                    }
                }
            }

        }
    }
    protected void ddloutHourFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtoutDate.Text != "" && txtoutDate.Text != string.Empty)
        {
            string currentDate = System.DateTime.Now.ToString("dd/MM/yyyy");

            if (ddlAM_PM1.SelectedValue == "PM") //Added By Himanshu Tamrakar 06-03-2024
            {
                if (ddloutHourFrom.SelectedValue == "0")
                {
                    objCommon.DisplayMessage("For PM  you Can not Select 0 for in Hour from", this.Page);
                    int hour = DateTime.Now.Hour;
                    if (hour > 12)
                    {
                        hour = hour - 12;
                    }
                    ddloutHourFrom.SelectedValue = Convert.ToString(hour);
                    ddloutHourFrom.Focus();
                    return;
                }
            }
            if (Convert.ToDateTime(txtoutDate.Text) == Convert.ToDateTime(currentDate))
            {
                if (ddlAM_PM1.SelectedValue == "0")
                {
                    string message = "For current date entry.Please select AM/PM before entering Hour From.";
                    string encodedMessage = HttpUtility.JavaScriptStringEncode(message);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + encodedMessage + "');", true);
                    ddloutHourFrom.SelectedIndex = 0;
                    ddlAM_PM1.Focus();
                    return;
                }

                DateTime current = Convert.ToDateTime(txtoutDate.Text);

                current = current.AddHours(Convert.ToDouble(ddloutHourFrom.SelectedValue));

                if (ddlAM_PM1.SelectedValue == "PM")
                {
                    current = current.AddHours(12);
                }

                if (current > System.DateTime.Now)
                {
                }
                else
                {
                    objCommon.DisplayMessage("Hour From should be greater than Current Hour.", this.Page);
                    ddloutHourFrom.Focus();
                    ddloutHourFrom.SelectedIndex = 0;
                    return;
                }
            }
        }
    }
    protected void printGatepass_Click(object sender, EventArgs e)  //Added By Himanshu tamrakar for ticket : 51837 on 19/12/2023
    {
        Button btnEdit = sender as Button;
        gatepass_no = btnEdit.CommandArgument;
        if (gatepass_no == string.Empty)
        {
            objCommon.DisplayMessage("Gate Pass Not Found. Wait For Approval.", this);
            return;
        }
        //ShowReport("Gate Pass Report", "HostelGatePassReport.rpt");
        ShowGeneralReport("~,Reports,Hostel,HostelGatePassReport.rpt", "@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_GATEPASSNO=" + Convert.ToInt32(gatepass_no));
    }
    private void ShowGeneralReport(string path, string paramString)
    {
        /// Set Report
        ReportDocument customReport = new ReportDocument();
        string reportPath = Server.MapPath(path.Replace(",", "\\"));
        customReport.Load(reportPath);

        /// Assign parameters to report document        
        char ch = ',';
        string[] val = paramString.Split(ch);
        if (customReport.ParameterFields.Count > 0)
        {
            for (int i = 0; i < val.Length; i++)
            {
                int indexOfEql = val[i].IndexOf('=');
                int indexOfStar = val[i].IndexOf('*');

                string paramName = string.Empty;
                string value = string.Empty;
                string reportName = "MainRpt";

                paramName = val[i].Substring(0, indexOfEql);

                /// if report name is not passed with the parameter(means indexOfSlash will be -1) then 
                /// handle the scenario to work properly.
                if (indexOfStar > 0)
                {
                    value = val[i].Substring(indexOfEql + 1, ((indexOfStar - 1) - indexOfEql));
                    reportName = val[i].Substring(indexOfStar + 1);
                }
                else
                {
                    value = val[i].Substring(indexOfEql + 1);
                }

                if (reportName == "MainRpt")
                {
                    if (value == "null")
                    {
                        customReport.SetParameterValue(paramName, null);
                    }
                    else
                        customReport.SetParameterValue(paramName, value);
                }
                else
                    customReport.SetParameterValue(paramName, value, reportName);
            }
        }

        /// set login details & db details for report document
        this.ConfigureCrystalReports(customReport);

        /// set login details & db details for each subreport 
        /// inside main report document.
        for (int i = 0; i < customReport.Subreports.Count; i++)
        {
            ConfigureCrystalReports(customReport.Subreports[i]);
        }

        ////Export to PDF
        customReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, "HostelGatePass");
    }

    private void ConfigureCrystalReports(ReportDocument customReport)
    {
        ////SET Login Details & DB DETAILS
        ConnectionInfo connectionInfo = Common.GetCrystalConnection();
        Common.SetDBLogonForReport(connectionInfo, customReport);
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("hostel")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Hostel," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_GATEPASSNO=" + Convert.ToInt32(gatepass_no);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "HOSTEL_REPORT_ApplyStudDatewiseReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlAM_PM1_SelectedIndexChanged(object sender, EventArgs e) //Added By Himanshu Tamrakar 06-03-2024
    {
        int hour = DateTime.Now.Hour;
        int minute = DateTime.Now.Minute;
        if (ddlAM_PM1.SelectedValue == "PM")
        {
            if (DateTime.Now.Hour > 12)
            {
                //ddloutHourFrom.SelectedValue = Convert.ToString(DateTime.Now.Hour);
                ddloutHourFrom.SelectedValue = Convert.ToString(hour - 12);
                ddloutMinFrom.SelectedValue = Convert.ToString(minute);
            }
            else
            {
                ddloutHourFrom.SelectedValue = "12";
                ddloutMinFrom.SelectedValue = "0";
            }
        }
        if (ddlAM_PM1.SelectedValue == "AM")
        {

            if (hour < 12)
            {
                if (DateTime.Now.Hour > 12)
                {
                    //ddloutHourFrom.SelectedValue = Convert.ToString(DateTime.Now.Hour);
                    ddloutHourFrom.SelectedValue = Convert.ToString(hour);
                    ddloutMinFrom.SelectedValue = Convert.ToString(minute);
                }
                else
                {
                    ddloutHourFrom.SelectedValue = "12";
                    ddloutMinFrom.SelectedValue = "0";
                }
            }
            if (ddlAM_PM2.SelectedValue == "AM")
            {
                if (ddloutHourFrom.SelectedValue != "0")
                {
                    if (Convert.ToInt32(ddloutHourFrom.SelectedValue) > Convert.ToInt32(ddlinHourFrom.SelectedValue))
                    {
                        objCommon.DisplayMessage("In hour from should be greater than Out hour from.", this.Page);
                        ddloutHourFrom.Focus();
                        return;
                    }
                }
            }
        }
        if (txtoutDate.Text == string.Empty)
        {
            objCommon.DisplayMessage("Please select Out Date First.", this.Page);
            ddlAM_PM1.SelectedValue = "0";
            return;
        }
    }

    ////below code added by Himanshu tamrakar 05042024
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Convert.ToDateTime(txtFromDateSearch.Text) > Convert.ToDateTime(txtToDateSearch.Text))
        {
            objCommon.DisplayMessage("To Date is Greater Than From date.", this);
            txtToDateSearch.Text = string.Empty;
            txtFromDateSearch.Text = string.Empty;
            return;
        }

        string Applydate = string.IsNullOrEmpty(txtApplyDate.Text) ? null : DateTime.Parse(txtApplyDate.Text).ToString("yyyy-MM-dd");
        //int Purpose = string.IsNullOrEmpty(ddlPurposeSearch.SelectedValue)?0:Convert.ToInt32(ddlPurposeSearch.SelectedValue);
        string Todate = string.IsNullOrEmpty(txtToDateSearch.Text) ? null : DateTime.Parse(txtToDateSearch.Text).ToString("yyyy-MM-dd");
        string Fromdate = string.IsNullOrEmpty(txtFromDateSearch.Text) ? null : DateTime.Parse(txtFromDateSearch.Text).ToString("yyyy-MM-dd");
        // string Status = string.IsNullOrEmpty(ddlStatus.SelectedValue) ? null : ddlStatus.SelectedValue;
        this.BindListView(Applydate, 0, Todate, Fromdate, "0");
    }

    //protected void btnApplyGatePass_Click(object sender, EventArgs e)
    //{
    //    //divSearch.Visible = false;
    //    pnlStudentHGPRequestDetails.Visible = true;
    //    pnlbuttons.Visible = true;
    //}

    //below code added by Himanshu tamrakar 05042024
    protected void btnBack_Click(object sender, EventArgs e)
    {
        txtApplyDate.Text = string.Empty;
        //ddlPurposeSearch.SelectedValue = "0";
        txtToDateSearch.Text = string.Empty;
        txtFromDateSearch.Text = string.Empty;
        //ddlStatus.SelectedValue = "0";
        BindListView(null, 0, Convert.ToString(DateTime.Parse(Convert.ToString(Todate)).ToString("yyyy-MM-dd")), Convert.ToString(DateTime.Parse(Convert.ToString(Fromdate)).ToString("yyyy-MM-dd")), "0");
    }
}