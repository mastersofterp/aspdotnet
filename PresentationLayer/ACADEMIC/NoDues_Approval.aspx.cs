using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EASendMail;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using SendGrid;
using SendGrid.Helpers.Mail;

public partial class ACADEMIC_NoDues_Approval : System.Web.UI.Page
{
    Common objCommon = new Common();
    Student objS = new Student();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
    Student objstudent = new Student();

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
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                DataSet dsCheck = objSC.CheckCautionApprovalAuthority(Convert.ToInt32(Session["userno"]));
                if (dsCheck.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToInt32(dsCheck.Tables[0].Rows[0]["APP_NO"].ToString()) == 0)
                    {
                        Response.Redirect("~/notauthorized.aspx?page=NoDues_Approval.aspx");
                    }
                }
                //Page Authorization
                //CheckPageAuthorization();
                objCommon.FillDropDownList(ddlPassBatch, "ACD_NODUES ", "DISTINCT (PASSING_YEAR) Pass", "PASSING_YEAR", "", "Pass");

            }
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=NoDues_Approval.aspx");
            }
        }
        else
        {
            Response.Redirect("~/notauthorized.aspx?page=NoDues_Approval.aspx");
        }
    }
    protected void btnShowapprovedstud_Click(object sender, EventArgs e)
    {
        ddlstatus.SelectedIndex = 0;
        BindListViewapprovedstud(-1);
    }
    private void BindListViewapprovedstud(int status)
    {
        int userNo = Convert.ToInt32(Session["userno"].ToString());
        int userType = Convert.ToInt32(Session["usertype"].ToString());

        try
        {
            DataSet dsColleges = objSC.GetCollegesByUser_For_NoDues(Convert.ToInt32(Session["userno"]));
            string college = string.Empty;
            string actual_College = string.Empty;
            if (dsColleges.Tables.Count > 0)
            {
                if (dsColleges.Tables[1].Rows.Count > 0)
                {
                    //int college_Caution_Money = Convert.ToInt32(dsColleges.Tables[1].Rows[0]["COLLEGE_CAUTION_MONEY"].ToString());
                    //for (int i = 0; i < dsColleges.Tables[0].Rows.Count; i++)
                    //{
                    //    college =Convert.ToInt32(dsColleges.Tables[0].Rows[i]["COLLEGE_NOS"].ToString());
                    //    if (college_Caution_Money == college)
                    //    {
                    //        actual_College = college;
                    //        ViewState["ACTUAL_COLLEGE"]= actual_College;
                    //        break;
                    //    }
                    //        //return;
                    //}
                    for (int i = 0; i < dsColleges.Tables[1].Rows.Count; i++)
                    {
                        college = Convert.ToString(dsColleges.Tables[1].Rows[i]["COLLEGE_NODUES"].ToString());
                        if (college != "" && college != string.Empty)
                        {
                            actual_College += college + ',';

                        }
                        //return;
                    }
                    ViewState["ACTUAL_COLLEGE"] = actual_College;
                }
            }
            string APPROVAL_1_UANO = objCommon.LookUp("ACD_AUTHORITY_APPROVAL_MASTER", "APPROVAL_1_UANO", "AUTHORITY_TYPE_NO=1 AND APPROVAL_1_UANO=" + userNo + " AND APPROVAL_1_UA_TYPE=" + userType + " AND COLLEGE_NO IN (SELECT VALUE FROM [dbo].[fn_Split]('" + actual_College + "',','))");
            ViewState["APPROVAL_1_UANO"] = APPROVAL_1_UANO;
            string APPROVAL_2_UANO = objCommon.LookUp("ACD_AUTHORITY_APPROVAL_MASTER", " DISTINCT APPROVAL_2_UANO", "AUTHORITY_TYPE_NO=1 AND APPROVAL_2_UANO=" + userNo + " AND APPROVAL_2_UA_TYPE=" + userType + " AND COLLEGE_NO IN (SELECT VALUE FROM [dbo].[fn_Split]('" + actual_College + "',','))");
            ViewState["APPROVAL_2_UANO"] = APPROVAL_2_UANO;
            string APPROVAL_3_UANO = objCommon.LookUp("ACD_AUTHORITY_APPROVAL_MASTER", " DISTINCT APPROVAL_3_UANO", "AUTHORITY_TYPE_NO=1 AND APPROVAL_3_UANO=" + userNo + " AND APPROVAL_3_UA_TYPE=" + userType + " AND COLLEGE_NO IN (SELECT VALUE FROM [dbo].[fn_Split]('" + actual_College + "',','))");
            ViewState["APPROVAL_3_UANO"] = APPROVAL_3_UANO;
            string APPROVAL_4_UANO = objCommon.LookUp("ACD_AUTHORITY_APPROVAL_MASTER", "DISTINCT APPROVAL_4_UANO", "AUTHORITY_TYPE_NO=1 AND APPROVAL_4_UANO=" + userNo + " AND APPROVAL_4_UA_TYPE=" + userType + " AND COLLEGE_NO IN (SELECT VALUE FROM [dbo].[fn_Split]('" + actual_College + "',','))");
            ViewState["APPROVAL_4_UANO"] = APPROVAL_4_UANO;
            string APPROVAL_5_UANO = objCommon.LookUp("ACD_AUTHORITY_APPROVAL_MASTER", "DISTINCT APPROVAL_5_UANO", "AUTHORITY_TYPE_NO=1 AND APPROVAL_5_UANO=" + userNo + " AND APPROVAL_5_UA_TYPE=" + userType + " AND COLLEGE_NO IN (SELECT VALUE FROM [dbo].[fn_Split]('" + actual_College + "',','))");
            ViewState["APPROVAL_5_UANO"] = APPROVAL_5_UANO;
            //ViewState["deptNo"] = 0;
            string passbatch = ddlPassBatch.SelectedItem.Text;
            DataSet ds = objSC.GetApprovedStudList(objstudent, Convert.ToInt32(Session["userno"].ToString()), actual_College, userType, passbatch, status);
            if (ds.Tables.Count == 0)
            {
                objCommon.DisplayMessage(this.Page, "Records Not Found.", this.Page);
                return;
            }
            hideshow();
            if (ds.Tables[0].Rows.Count > 0)
            {

                lvNoDuesApproved.DataSource = ds;
                lvNoDuesApproved.DataBind();
                lvNoDuesApproved.Visible = true;
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvNoDuesApproved);//Set label -
                //hfcount.Value = ds.Tables[0].Rows.Count.ToString();

                ViewState["stat"] = ds.Tables[0].Rows[0]["STATUS"];
                ViewState["Regno"] = ds.Tables[0].Rows[0]["REGISTER NO"];

                ViewState["Status"] = ds.Tables[0].Rows[0]["APPROVED"];

                lvNoDuesApproved.Visible = true;
                foreach (ListViewItem item in lvNoDuesApproved.Items)
                {
                    HiddenField hfidno = (item.FindControl("hfidno") as HiddenField);
                    CheckBox chkstat = (item.FindControl("chkRow") as CheckBox);
                    TextBox txtReasonap1 = (item.FindControl("txtReasonap1") as TextBox);
                    TextBox txtReasonap2 = (item.FindControl("txtReasonap2") as TextBox);
                    TextBox txtReasonap3 = (item.FindControl("txtReasonap3") as TextBox);
                    TextBox txtReasonap4 = (item.FindControl("txtReasonap4") as TextBox);
                    TextBox txtReasonap5 = (item.FindControl("txtReasonap5") as TextBox);
                    LinkButton btnName = (item.FindControl("btnName") as LinkButton);
                    string HODAPPROVE = objCommon.LookUp("ACD_NO_DUES_STATUS", "HOD_APPROVED", "IDNO=" + hfidno.Value);
                    string T_AND_PApprove = objCommon.LookUp("ACD_NO_DUES_STATUS", "T_AND_P_APPROVED", "IDNO=" + hfidno.Value);
                    string LibraryApprove = objCommon.LookUp("ACD_NO_DUES_STATUS", "LIBRARY_APPROVED", "IDNO=" + hfidno.Value);
                    string REGISTAR_APPROVED = objCommon.LookUp("ACD_NO_DUES_STATUS", "REGISTRAR_APPROVED", "IDNO=" + hfidno.Value);
                    string FINANCE_APPROVE = objCommon.LookUp("ACD_NO_DUES_STATUS", "FINANCE_DEPT_APPROVED", "IDNO=" + hfidno.Value);
                    //if (HODAPPROVE == "1" && LibraryApprove == "1" && REGISTAR_APPROVED == "1" && FINANCE_APPROVE == "1")
                    //{
                    //    chkstat.Enabled = false;
                    //}
                    btnName.ForeColor = System.Drawing.Color.Black;
                    if (Session["userno"].Equals(APPROVAL_1_UANO))
                    {
                        btnName.ForeColor = System.Drawing.Color.Black;
                        btnName.Enabled = false;
                        if (HODAPPROVE == "1" && T_AND_PApprove == "1" || HODAPPROVE == "1" && T_AND_PApprove == "2" || HODAPPROVE == "1")
                        {
                            chkstat.Enabled = false;
                        }
                    }
                    else if (Session["userno"].Equals(APPROVAL_2_UANO))
                    {
                        btnName.ForeColor = System.Drawing.Color.Black;
                        btnName.Enabled = false;
                        if (HODAPPROVE == "1" && T_AND_PApprove == "1" || HODAPPROVE == "1" && T_AND_PApprove == "1" && LibraryApprove == "2")
                        {
                            chkstat.Enabled = false;
                        }
                    }
                    else if (Session["userno"].Equals(APPROVAL_3_UANO))
                    {
                        btnName.ForeColor = System.Drawing.Color.Black;
                        btnName.Enabled = false;
                        if (HODAPPROVE == "1" && T_AND_PApprove == "1" && LibraryApprove == "1" || HODAPPROVE == "1" && T_AND_PApprove == "1" && LibraryApprove == "1" && REGISTAR_APPROVED == "2")
                        {
                            chkstat.Enabled = false;
                        }
                    }
                    else if (Session["userno"].Equals(APPROVAL_4_UANO))
                    {
                        btnName.ForeColor = System.Drawing.Color.Black;
                        btnName.Enabled = false;
                        if (HODAPPROVE == "1" && T_AND_PApprove == "1" && LibraryApprove == "1" && REGISTAR_APPROVED == "1" || HODAPPROVE == "1" && T_AND_PApprove == "1" && LibraryApprove == "1" && REGISTAR_APPROVED == "1" && FINANCE_APPROVE == "2")
                        {
                            chkstat.Enabled = false;
                        }
                    }
                    //else if (Session["userno"].Equals(APPROVAL_5_UANO))
                    //{
                    //    btnName.Enabled = true;
                    //}
                    //if (chkstat.Enabled == false)
                    //{
                    //    if (Session["userno"].Equals(ViewState["APPROVAL_1_UANO"]))
                    //    {
                    //        txtReasonap1.Visible = false;
                    //    }
                    //    else if (Session["userno"].Equals(ViewState["APPROVAL_2_UANO"]))
                    //    {
                    //        txtReasonap2.Visible = false;
                    //    }
                    //    else if (Session["userno"].Equals(ViewState["APPROVAL_3_UANO"]))
                    //    {
                    //        txtReasonap3.Visible = false;
                    //    }
                    //    else if (Session["userno"].Equals(ViewState["APPROVAL_4_UANO"]))
                    //    {
                    //        txtReasonap4.Visible = false;
                    //    }
                    //    else if (Session["userno"].Equals(ViewState["APPROVAL_5_UANO"]))
                    //    {
                    //        txtReasonap5.Visible = false;
                    //    }
                    //    //txtReason.Enabled = false;
                    //}
                }
                divshow.Visible = true;
                btnsavestatus.Visible = true;
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "No Record Found.", this.Page);
                divshow.Visible = false;
                lvNoDuesApproved.Visible = false;
                btnsavestatus.Visible = false;
                return;
            }
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.BindListViewJobLoc -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void hideshow()
    {
        if (Session["userno"].Equals(ViewState["APPROVAL_1_UANO"]))
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "$('#thReason1').show();$('td:nth-child(9)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thReason1').show();$('td:nth-child(9)').show();});", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "myfun1", "$('#thReason2').hide();$('td:nth-child(10)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thReason2').hide();$('td:nth-child(10)').hide();});", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "myfun2", "$('#thReason3').hide();$('td:nth-child(11)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thReason3').hide();$('td:nth-child(11)').hide();});", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "myfun3", "$('#thReason4').hide();$('td:nth-child(12)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thReason4').hide();$('td:nth-child(12)').hide();});", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "myfun4", "$('#thReason5').hide();$('td:nth-child(13)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thReason5').hide();$('td:nth-child(13)').hide();});", true);
        }
        else if (Session["userno"].Equals(ViewState["APPROVAL_2_UANO"]))
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "$('#thReason1').hide();$('td:nth-child(9)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thReason1').hide();$('td:nth-child(9)').hide();});", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "myfun1", "$('#thReason2').show();$('td:nth-child(10)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thReason2').show();$('td:nth-child(10)').show();});", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "myfun2", "$('#thReason3').hide();$('td:nth-child(11)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thReason3').hide();$('td:nth-child(11)').hide();});", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "myfun3", "$('#thReason4').hide();$('td:nth-child(12)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thReason4').hide();$('td:nth-child(12)').hide();});", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "myfun4", "$('#thReason5').hide();$('td:nth-child(13)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thReason5').hide();$('td:nth-child(13)').hide();});", true);
        }
        else if (Session["userno"].Equals(ViewState["APPROVAL_3_UANO"]))
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "$('#thReason1').hide();$('td:nth-child(9)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thReason1').hide();$('td:nth-child(9)').hide();});", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "myfun1", "$('#thReason2').hide();$('td:nth-child(10)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thReason2').hide();$('td:nth-child(10)').hide();});", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "myfun2", "$('#thReason3').show();$('td:nth-child(11)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thReason3').show();$('td:nth-child(11)').show();});", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "myfun3", "$('#thReason4').hide();$('td:nth-child(12)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thReason4').hide();$('td:nth-child(12)').hide();});", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "myfun4", "$('#thReason5').hide();$('td:nth-child(13)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thReason5').hide();$('td:nth-child(13)').hide();});", true);
        }
        else if (Session["userno"].Equals(ViewState["APPROVAL_4_UANO"]))
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "$('#thReason1').hide();$('td:nth-child(9)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thReason1').hide();$('td:nth-child(9)').hide();});", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "myfun1", "$('#thReason2').hide();$('td:nth-child(10)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thReason2').hide();$('td:nth-child(10)').hide();});", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "myfun2", "$('#thReason3').hide();$('td:nth-child(11)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thReason3').hide();$('td:nth-child(11)').hide();});", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "myfun3", "$('#thReason4').show();$('td:nth-child(12)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thReason4').show();$('td:nth-child(12)').show();});", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "myfun4", "$('#thReason5').hide();$('td:nth-child(13)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thReason5').hide();$('td:nth-child(13)').hide();});", true);
        }
        else if (Session["userno"].Equals(ViewState["APPROVAL_5_UANO"]))
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "$('#thReason1').hide();$('td:nth-child(9)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thReason1').hide();$('td:nth-child(9)').hide();});", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "myfun1", "$('#thReason2').hide();$('td:nth-child(10)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thReason2').hide();$('td:nth-child(10)').hide();});", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "myfun2", "$('#thReason3').hide();$('td:nth-child(11)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thReason3').hide();$('td:nth-child(11)').hide();});", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "myfun3", "$('#thReason4').hide();$('td:nth-child(12)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thReason4').hide();$('td:nth-child(12)').hide();});", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "myfun4", "$('#thReason5').show();$('td:nth-child(13)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thReason5').show();$('td:nth-child(13)').show();});", true);
        }
    }
    private DataSet getModuleConfig()
    {
        DataSet ds = objCommon.GetModuleConfig(Convert.ToInt32(Session["OrgId"]));
        return ds;
    }
    protected void btnsavestatus_Click(object sender, EventArgs e)
    {
        int count = 0;
        foreach (ListViewDataItem dataitem in lvNoDuesApproved.Items)
        {
            CheckBox cbRow = dataitem.FindControl("chkRow") as CheckBox;
            if (cbRow.Checked == true)
                count++;
        }
        if (count <= 0)
        {
            objCommon.DisplayMessage(this.updTeach, "Please Select atleast one Student.", this);
            return;
        }

        int updCount = 0;
        int chkCount = 0;
        //int dept_Save = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString())));
        string email_type = string.Empty;
        string Link = string.Empty;
        DataSet ds = getModuleConfig();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            email_type = ds.Tables[0].Rows[0]["EMAIL_TYPE"].ToString();
            Link = ds.Tables[0].Rows[0]["LINK"].ToString();
        }
        string collegeCode = string.Empty;
        collegeCode = objCommon.LookUp("REFF", "CODE_STANDARD", "OrganizationId=" + Convert.ToInt32(Session["OrgId"]));
        ViewState["COLCODE"] = collegeCode;

        foreach (ListViewItem item in lvNoDuesApproved.Items)
        {
            //objstudent.PassingYear = ddlPassBatch.SelectedItem.Text;

            CheckBox chkstat = (item.FindControl("chkRow") as CheckBox);
            Label lblRegno = (item.FindControl("lblRegno") as Label);
            TextBox txtReasonap1 = (item.FindControl("txtReasonap1") as TextBox);
            TextBox txtReasonap2 = (item.FindControl("txtReasonap2") as TextBox);
            TextBox txtReasonap3 = (item.FindControl("txtReasonap3") as TextBox);
            TextBox txtReasonap4 = (item.FindControl("txtReasonap4") as TextBox);
            TextBox txtReasonap5 = (item.FindControl("txtReasonap5") as TextBox);
            LinkButton lnkEmail = (item.FindControl("btnName") as LinkButton);
            //HiddenField hfstatus = (item.FindControl("hfstatus") as HiddenField);
            objstudent.RegNo = lblRegno.Text;
            string reason = string.Empty;
            int stat = 0;
            objS.EmailID = lnkEmail.ToolTip;
            string name = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_NO=" + Convert.ToInt32(Session["userno"]));
            if (Session["userno"].Equals(ViewState["APPROVAL_1_UANO"]))
            {
                reason = txtReasonap1.Text;
            }
            else if (Session["userno"].Equals(ViewState["APPROVAL_2_UANO"]))
            {
                reason = txtReasonap2.Text;
            }
            else if (Session["userno"].Equals(ViewState["APPROVAL_3_UANO"]))
            {
                reason = txtReasonap3.Text;
            }
            else if (Session["userno"].Equals(ViewState["APPROVAL_4_UANO"]))
            {
                reason = txtReasonap4.Text;
            }
            else if (Session["userno"].Equals(ViewState["APPROVAL_5_UANO"]))
            {
                reason = txtReasonap5.Text;
            }

            if (Session["userno"].ToString() != null && Session["userno"].ToString() != "" && Session["userno"].ToString() != string.Empty)
            {
                stat = Convert.ToInt32(chkstat.ToolTip);
            }
            if (ddlapppenstatus.SelectedValue == "1")
            {
                if (chkstat.Checked == true)
                {
                    chkCount++;
                    //if (reason == "" || reason.Equals(string.Empty))
                    //{
                    //    objCommon.DisplayMessage(updTeach, "Please Enter Reason.", this.Page);
                    //    //txtReason.Focus();
                    //    if (Session["userno"].Equals(ViewState["APPROVAL_1_UANO"]))
                    //    {
                    //        txtReasonap1.Focus();
                    //    }
                    //    else if (Session["userno"].Equals(ViewState["APPROVAL_2_UANO"]))
                    //    {
                    //        txtReasonap2.Focus();
                    //    }
                    //    else if (Session["userno"].Equals(ViewState["APPROVAL_3_UANO"]))
                    //    {
                    //        txtReasonap3.Focus();
                    //    }
                    //    else if (Session["userno"].Equals(ViewState["APPROVAL_4_UANO"]))
                    //    {
                    //        txtReasonap4.Focus();
                    //    }
                    //    else if (Session["userno"].Equals(ViewState["APPROVAL_5_UANO"]))
                    //    {
                    //        txtReasonap5.Focus();
                    //    }

                    //    return;
                    //}

                    stat = 1;
                    //txtCount++; Convert.ToInt32(Session["usertype"].ToString());
                    //CustomStatus CS = (CustomStatus)objStud.SavedCautionStatus(objstudent, stat, Convert.ToInt32(Session["userno"].ToString()), reason, dept_Save);
                    CustomStatus CS = (CustomStatus)objSC.SavedCautionStatus(objstudent, stat, Convert.ToInt32(Session["userno"]), reason, Convert.ToString(ViewState["ACTUAL_COLLEGE"]), Convert.ToInt32(Session["usertype"].ToString()));
                    if (CS.Equals(CustomStatus.RecordUpdated))
                    {
                        updCount++;

                        string message = "Dear " + lnkEmail.Text + ", Your No Dues Application has been Approve.";
                        string subject = "No Dues Status";

                        //------------Code for sending email,It is optional---------------
                        // int status = sendEmail(message, useremail, subject);
                        //int reg = TransferToEmail(objS.EmailID, message, subject); //--tempoary Commented
                        if (email_type == "1" && email_type != "")
                        {
                            int reg = TransferToEmail(objS.EmailID, message, subject);
                        }
                        else if (email_type == "2" && email_type != "")
                        {
                            Task<int> task = Execute(message, objS.EmailID, subject);
                        }
                        if (email_type == "3" && email_type != "")
                        {
                            OutLook_Email(message, objS.EmailID, subject);
                        }
                    }




                }


            }
            else
            {
                if (chkstat.Checked == true)
                {
                    chkCount++;

                    objCommon.DisplayMessage(this.updTeach, "No Dues Status is Pending.", this);
                    return;
                }
            }
            //else if (chkstat.Checked == true && ddlstatus.SelectedValue == "1")
            //{
            //    stat = 1;
            //    reason = string.Empty;
            //    CustomStatus CS = (CustomStatus)objSC.SavedCautionStatus(objstudent, stat, Convert.ToInt32(Session["userno"]), reason, Convert.ToString(ViewState["ACTUAL_COLLEGE"]), Convert.ToInt32(Session["usertype"].ToString()));
            //    if (CS.Equals(CustomStatus.RecordUpdated))
            //    {
            //        objCommon.DisplayMessage(this.Page, "No Dues Status Updated Successfully.", this.Page);
            //        ViewState["action"] = null;
            //        BindListViewapprovedstud(Convert.ToInt32(ddlstatus.SelectedValue));

            //        string message = "Dear " + lnkEmail.Text + ", Your No Dues Application has Approve By " + name;
            //        string subject = "No Dues Status";

            //        //------------Code for sending email,It is optional---------------
            //        // int status = sendEmail(message, useremail, subject);
            //        //int reg = TransferToEmail(objS.EmailID, message, subject); //--tempoary Commented
            //        if (email_type == "1" && email_type != "")
            //        {
            //            int reg = TransferToEmail(objS.EmailID, message, subject);
            //        }
            //        else if (email_type == "2" && email_type != "")
            //        {
            //            Task<int> task = Execute(message, objS.EmailID, subject);
            //        }
            //        if (email_type == "3" && email_type != "")
            //        {
            //            OutLook_Email(message, objS.EmailID, subject);
            //        }
            //    }
            //}

        }
        if (chkCount == updCount && chkCount > 0)
        {
            objCommon.DisplayMessage(this.Page, "No Dues Status Updated Successfully.", this.Page);
            ViewState["action"] = null;
            BindListViewapprovedstud(Convert.ToInt32(ddlstatus.SelectedValue));

        }
        ddlstatus.SelectedIndex = 0;
        ddlapppenstatus.SelectedIndex = 0;

    }

    public int TransferToEmail(string useremail, string message, string subject)
    {
        int ret = 0;
        try
        {
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("reff", "EMAILSVCID", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);

            if (dsconfig != null)
            {
                string fromAddress = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
                string fromPassword = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();

                MailMessage msg = new MailMessage();
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                // fromPassword = Common.DecryptPassword(fromPassword);
                msg.From = new System.Net.Mail.MailAddress(fromAddress, "RCPIPER");
                msg.To.Add(new System.Net.Mail.MailAddress(useremail));
                msg.Subject = subject;
                msg.Body = message;
                smtp.Credentials = new System.Net.NetworkCredential(fromAddress, fromPassword);
                smtp.EnableSsl = true;
                smtp.Port = 587; // 587
                smtp.Host = "smtp.gmail.com";

                ServicePointManager.ServerCertificateValidationCallback =
                delegate(object s, X509Certificate certificate,
                X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

                smtp.Send(msg);
                if (System.Net.Mail.DeliveryNotificationOptions.OnSuccess == System.Net.Mail.DeliveryNotificationOptions.OnSuccess)
                {
                    return ret = 1;
                    //Storing the details of sent email
                }
                else
                {
                    return ret = 0;
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return ret;

    }

    static async Task<int> Execute(string Message, string toEmailId, string sub)
    {
        int ret = 0;
        try
        {
            string collegeCode = string.Empty;

            Common objCommon = new Common();
            DataSet dsconfig = null;
            //dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,EMAILSVCPWD,SENDGRID_APIKEY", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY,SUBJECT_OTP", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            var fromAddress = new System.Net.Mail.MailAddress(dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString(), collegeCode);
            var toAddress = new System.Net.Mail.MailAddress(toEmailId, "");
            var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString(), collegeCode);
            var subject = sub;
            var to = new EmailAddress(toEmailId, "");
            var plainTextContent = "";
            var htmlContent = Message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);
            string res = Convert.ToString(response.IsCompleted);
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

    private int OutLook_Email(string Message, string toEmailId, string sub)
    {

        int ret = 0;
        try
        {
            Common objCommon = new Common();
            DataSet dsconfig = null;
            //dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,USER_PROFILE_SUBJECT,CollegeName", "EMAILSVCPWD,USER_PROFILE_SENDERNAME,COMPANY_EMAILSVCID AS MASTERSOFT_GRID_MAILID,SENDGRID_PWD AS MASTERSOFT_GRID_PASSWORD,SENDGRID_USERNAME AS MASTERSOFT_GRID_USERNAME", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            //dsconfig = objCommon.FillDropDown("REFF", "SLIIT_EMAIL,USER_PROFILE_SUBJECT,CollegeName", "SLIIT_EMAIL_PWD,USER_PROFILE_SENDERNAME,COMPANY_EMAILSVCID AS MASTERSOFT_GRID_MAILID,SENDGRID_PWD AS MASTERSOFT_GRID_PASSWORD,SENDGRID_USERNAME AS MASTERSOFT_GRID_USERNAME", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);

            dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,CollegeName", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            SmtpMail oMail = new SmtpMail("TryIt");
            oMail.From = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
            oMail.To = toEmailId;
            oMail.Subject = sub;
            oMail.HtmlBody = Message;
            // SmtpServer oServer = new SmtpServer("smtp.live.com");
            SmtpServer oServer = new SmtpServer("smtp.office365.com"); // modify on 29-01-2022
            oServer.User = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
            oServer.Password = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();
            oServer.Port = 587;
            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
            Console.WriteLine("start to send email over TLS...");
            EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();
            oSmtp.SendMail(oServer, oMail);
            Console.WriteLine("email sent successfully!");
            ret = 1;
        }
        catch (Exception ep)
        {
            Console.WriteLine("failed to send email with the following error:");
            Console.WriteLine(ep.Message);
            ret = 0;
        }
        return ret;
    }
    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlstatus.SelectedIndex > 0)
        {
            BindListViewapprovedstud(Convert.ToInt32(ddlstatus.SelectedValue));
        }
        else
        {
            lvNoDuesApproved.Visible = false;
            return;
        }
    }
}