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
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;  

public partial class ACADEMIC_NoduesApprovalReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    Student objS = new Student();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
    Student objstudent = new Student();

     string path = System.Configuration.ConfigurationManager.AppSettings["Path"];
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
                        Response.Redirect("~/notauthorized.aspx?page=NoduesApprovalReport.aspx");
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
                Response.Redirect("~/notauthorized.aspx?page=NoduesApprovalReport.aspx");
            }
        }
        else
        {
            Response.Redirect("~/notauthorized.aspx?page=NoduesApprovalReport.aspx");
        }
    }
    protected void btnShowapprovedstud_Click(object sender, EventArgs e)
    {
        ddlstatus.SelectedIndex = 0;
        BindListViewapprovedstudapprovedlist(1);
    }
    private void BindListViewapprovedstudapprovedlist(int status)
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
            DataSet ds = objSC.GetApprovedStudListApproved(objstudent, Convert.ToInt32(Session["userno"].ToString()), actual_College, userType, passbatch, status);
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
               
                divshow.Visible = true;

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
                   
                }
               
               
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "No Record Found.", this.Page);              
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
    
    protected void btnviewreport_Click(object sender, EventArgs e)
    {
        try
        {

            int IDNO = int.Parse((sender as Button).CommandArgument);
            ViewState["IDNO"] = IDNO;

            ShowReport("No Dues Certificate", "NoDuesCertificateReport_CPUK_CPUH.rpt");
        }


        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_LockMarksByScheme.btnGrade_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(ViewState["IDNO"]);

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updTeach, this.updTeach.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch
        {
            throw;
        }
    }
    private string ReturnExtension(string fileExtension)
    {
        switch (fileExtension)
        {
            case ".htm":
            case ".html":
            case ".log":
                return "text/HTML";
            case ".txt":
                return "text/plain";
            case ".doc":
                return "application/ms-word";
            case ".tiff":
            case ".tif":
                return "image/tiff";
            case ".asf":
                return "video/x-ms-asf";
            case ".avi":
                return "video/avi";
            case ".zip":
                return "application/zip";
            case ".xls":
            case ".csv":
                return "application/vnd.ms-excel";
            case ".gif":
                return "image/gif";
            case ".jpg":
            case "jpeg":
                return "image/jpeg";
            case ".bmp":
                return "image/bmp";
            case ".wav":
                return "audio/wav";
            case ".mp3":
                return "audio/mpeg3";
            case ".mpg":
            case "mpeg":
                return "video/mpeg";
            case ".rtf":
                return "application/rtf";
            case ".asp":
            case ".cs":
                return "text/asp";
            case ".pdf":
                return "application/pdf";
            case ".fdf":
                return "application/vnd.fdf";
            case ".ppt":
                return "application/mspowerpoint";
            case ".dwg":
                return "image/vnd.dwg";
            case ".msg":
                return "application/msoutlook";
            case ".xml":
            case ".sdxl":
                return "application/xml";
            case ".xdp":
                return "application/vnd.adobe.xdp+xml";
            default:
                return "application/octet-stream";
        }
    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        int ID = int.Parse((sender as Button).CommandArgument);
        ReportDocument rprt = new ReportDocument();  
        string filepath;  
        Response.Clear();
        filepath = Server.MapPath("~/Reports/Academic/" + "NoDuesCertificateReport.rpt");  
        rprt.ExportToDisk(ExportFormatType.PortableDocFormat, filepath);  
        FileInfo fileinfo = new FileInfo(filepath);
        Response.AddHeader("Content-Disposition", "inline;filenam=NoDuesCertificateReport.pdf");  
        Response.ContentType = "application/pdf";  
        Response.WriteFile(fileinfo.FullName);  
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