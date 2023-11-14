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
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data.SqlClient;
using SendGrid;
using SendGrid.Helpers.Mail;
using EASendMail;
using System.Net;
using System.Net.Mail;
using BusinessLogicLayer.BusinessLogic;


public partial class HOSTEL_GATEPASS_HostelGatePassRequest : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    GatePassRequest objGatePass = new GatePassRequest();
    GatePassRequestController objGPR = new GatePassRequestController();
    //SendEmailCommon objSendEmail = new SendEmailCommon();

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
                   // this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                }
                if (Convert.ToInt16(Session["usertype"]) == 1) adminsearch.Visible = true;
                
                objCommon.FillDropDownList(ddlSearch, "ACD_STUDENT", "IDNO", "STUDNAME", "HOSTELER=1 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "IDNO");
               string HostelNo = objCommon.LookUp("ACD_HOSTEL_ROOM_ALLOTMENT", "HOSTEL_NO", "RESIDENT_NO=" + Convert.ToInt32(Session["idno"]) + " and  CAN=0 AND HOSTEL_SESSION_NO=" + Convert.ToInt32(Session["hostel_session"]));



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
                BindListView();
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
            objGatePass.OutHourFrom = Convert.ToInt32(txtoutHourFrom.Text);
            objGatePass.OutMinFrom = Convert.ToInt32(txtoutMinFrom.Text.Trim());
            objGatePass.OutAMPM = ddlAM_PM1.SelectedValue;
            objGatePass.InDate = DateTime.Parse(txtinDate.Text.Trim());
            objGatePass.InHourFrom = Convert.ToInt32(txtinHourFrom.Text.Trim());
            objGatePass.InMinFrom = Convert.ToInt32(txtinMinFrom.Text.Trim());
            objGatePass.InAMPM = ddlAM_PM2.SelectedValue;
            objGatePass.PurposeID = Convert.ToInt32(ddlPurpose.SelectedValue);
            objGatePass.PurposeOther = txtOther.Text.Trim();
            objGatePass.Remarks = txtRemark.Text;
            objGatePass.IDNO = Convert.ToInt32(Session["idno"]);
            objGatePass.CollegeCode = Session["colcode"].ToString();
            objGatePass.organizationid = Session["OrgId"].ToString();

            if (ViewState["action"].ToString().Equals("add"))
            {
                if (objGatePass.OutDate < System.DateTime.Now.AddDays(-1) )   //  DateTime.Now.ToString("MM/dd/yyyy h:mm tt")
                {
                    objCommon.DisplayMessage("Please select out date Today OR greater than today's date.", this.Page);
                    txtoutDate.Focus();
                    return;
                }
            }
            
            if (objGatePass.InDate < objGatePass.OutDate)
            {
                objCommon.DisplayMessage("Please select in date greater than out date.", this.Page);
                txtinDate.Focus();
                return;
            }

            /// check form action whether add or update

                if (ViewState["action"].ToString().Equals("add"))
                {
                    if (CheckDuplicateEntry() == true)
                    {
                        objCommon.DisplayMessage("Entry for this Selection Already Done.", this.Page);
                        return;
                    }

                    int cs = objGPR.Insert_Update_GatePassRequest(objGatePass, Convert.ToInt32(ddlHostel.SelectedValue));
                    if (cs==1)
                    {
                        objCommon.DisplayMessage("Record Saved Successfully.", this.Page);
                        ViewState["action"] = "add";
                        Clear();
                        Sendmail();
                    }
                    else if (cs == -99)
                    {
                        objCommon.DisplayMessage("Passing path Not found.Contact to Administrator.", this.Page);
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
                        else if (cs == -99)
                        {
                            objCommon.DisplayMessage("Passing path Not found.Contact to Administrator.", this.Page);
                            ViewState["action"] = "add";
                            Clear();
                        }
                    }
                }
                
                 BindListView();  
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
            btnSubmit.Text = "Update";
            ImageButton btnEdit = sender as ImageButton;
            int gatepass_no = int.Parse(btnEdit.CommandArgument);
            ShowDetail(gatepass_no);
            ViewState["action"] = "edit";
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
        Session["idno"] = ddlSearch.SelectedValue;
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
            objCommon.FillDropDownList(ddlPurpose, "ACD_HOSTEL_PURPOSE_MASTER", "PURPOSE_NO", "PURPOSE_NAME", "ISACTIVE=1", "PURPOSE_NO");
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
        txtoutHourFrom.Text = string.Empty;
        txtoutMinFrom.Text = string.Empty;       
        txtinDate.Text = string.Empty;
        txtinHourFrom.Text = string.Empty;
        txtinMinFrom.Text = string.Empty;
        ddlAM_PM1.SelectedIndex = 0;
        ddlAM_PM2.SelectedIndex = 0;
        ddlPurpose.SelectedIndex = 0;
        txtOther.Text = string.Empty;
        txtRemark.Text = string.Empty;
        ddlStuType.SelectedIndex = 0;
        ddlHostel.SelectedIndex = 0;
        ddlSearch.SelectedIndex = 0;
    }

    private void BindListView()
    {
        try
        {
            DataSet ds = objGPR.GetAllGatePass();
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
                txtoutHourFrom.Text = dr["OUT_HOUR_FROM"] == null ? string.Empty : dr["OUT_HOUR_FROM"].ToString();
                txtoutMinFrom.Text = dr["OUT_MIN_FROM"] == null ? string.Empty : dr["OUT_MIN_FROM"].ToString();
                ddlAM_PM1.SelectedValue = dr["OUT_AM_PM"] == null ? string.Empty : dr["OUT_AM_PM"].ToString();
                txtinDate.Text = dr["INDATE"] == null ? string.Empty : dr["INDATE"].ToString();
                txtinHourFrom.Text = dr["IN_HOUR_FROM"] == null ? string.Empty : dr["IN_HOUR_FROM"].ToString();
                txtinMinFrom.Text = dr["IN_MIN_FROM"] == null ? string.Empty : dr["IN_MIN_FROM"].ToString();
                ddlAM_PM2.SelectedValue = dr["IN_AM_PM"] == null ? string.Empty : dr["IN_AM_PM"].ToString();
                ddlPurpose.SelectedValue = dr["PURPOSE_ID"] == null ? string.Empty : dr["PURPOSE_ID"].ToString();
                txtOther.Text = dr["PURPOSE_OTHER"] == null ? string.Empty : dr["PURPOSE_OTHER"].ToString();
                txtRemark.Text = dr["REMARKS"] == null ? string.Empty : dr["REMARKS"].ToString();          
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

                //status = objSendEmail.SendEmail(FatherID, message, subject, EmailID , " ");
                status = objCommon.sendEmail(message, FatherID, subject);
            }
        }

        if (status == 1) 
        {
            objCommon.DisplayMessage(this.Page, "Email Sent Successfully.", this.Page);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "functionConfirm", "confirmmsg();", true);
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Failed to send mail.", this.Page);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "functionConfirm", "confirmmsg();", true);
        }

    }

    #endregion Private Methods
    
}