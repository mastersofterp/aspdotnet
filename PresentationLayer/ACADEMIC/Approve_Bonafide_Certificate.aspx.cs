//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : APPROVE BONAFIDE CERTIFICATE
// CREATION DATE : 06-DECEMBER-2021                                                    
// CREATED BY    : PRANITA A. HIRADKAR                                                        
// MODIFIED DATE : 
// MODIFIED DESC : 
//=======================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

public partial class ACADEMIC_Approve_Bonafide_Certificate : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    CertificateMasterController objCerti = new CertificateMasterController();
    StudentController objSC = new StudentController();

    #region Page Load
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
                //CHECK THE STUDENT LOGIN
                if (Convert.ToInt32(Session["usertype"]) == 3 || Convert.ToInt32(Session["usertype"]) == 4)
                {
                    objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
                    objCommon.FillDropDownList(ddlCertificate, "ACD_CERTIFICATE_MASTER", "CERT_NO", "CERT_NAME", "CERT_NO > 0", "CERT_NO");
                    objCommon.FillDropDownList(ddlconductcharacter, "acd_tc_conduct_character", "CNO", "CONDUCT_CHARACTER", "CNO>0 AND  ACTIVESTATUS=1", "CNO");
                    Panel1.Visible = false;
                    string IPADDRESS = string.Empty;
                    IPADDRESS = Request.ServerVariables["REMOTE_ADDR"];
                    ViewState["IPAddress"] = IPADDRESS;
                    btnSubmit.Visible = false;
                    //  ddlCertificate.SelectedIndex = 1;

                }
                else if (Convert.ToInt32(Session["usertype"]) == 1)
                {
                    objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
                    objCommon.FillDropDownList(ddlCertificate, "ACD_CERTIFICATE_MASTER", "CERT_NO", "CERT_NAME", "CERT_NO > 0", "CERT_NO");
                    objCommon.FillDropDownList(ddlconductcharacter, "acd_tc_conduct_character", "CNO", "CONDUCT_CHARACTER", "CNO>0 AND  ACTIVESTATUS=1", "CNO");
                    Panel1.Visible = false;
                    string IPADDRESS = string.Empty;
                    IPADDRESS = Request.ServerVariables["REMOTE_ADDR"];
                    ViewState["IPAddress"] = IPADDRESS;
                    btnSubmit.Visible = false;
                }
                else
                {
                    objCommon.DisplayMessage(updBonafide, "you are not authorized to view this page.!!", this.Page);
                    return;
                }
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
                Response.Redirect("~/notauthorized.aspx?page=Approve_Bonafide_Certificate.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Approve_Bonafide_Certificate.aspx");
        }
    }
    #endregion

    #region bindlistview
    private void BindListView()
    {
        try
        {
            Panel1.Visible = true;
            DataSet ds = objCerti.GetApproveBonafideCertificate(Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlCertificate.SelectedValue));
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
                btnSubmit.Visible = true;
            }
            else
            {
                objCommon.DisplayMessage(this.updBonafide, "Record Not Found", this);
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                Panel1.Visible = false;
                btnSubmit.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Approve_Bonafide_Certificate.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindListViewFacuity()
    {
        try
        {
            Panel1.Visible = true;
            string SP_Name2 = "PKG_GET_APPROVE_BONAFIDE_CERTIFICATE_FACUILTY";
            string SP_Parameters2 = "@P_ADMBATCH,@P_CERT_NO,@P_UNO";
            string Call_Values2 = "" + Convert.ToInt32(ddlAdmbatch.SelectedValue) + "," + Convert.ToInt32(ddlCertificate.SelectedValue) + "," + Convert.ToInt32(Session["userno"]) + "";
            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
                btnSubmit.Visible = true;
            }
            else
            {
                objCommon.DisplayMessage(this.updBonafide, "Record Not Found", this);
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                Panel1.Visible = false;
                btnSubmit.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Approve_Bonafide_Certificate.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlAdmbatch_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    private void clear()
    {
        Panel1.Visible = false;
        ddlAdmbatch.SelectedIndex = 0;
        div2.Visible = false;
        issuedate.Visible = false;
        divconduct.Visible = false;
        divGRegNo.Visible = false;
        txtGReg.Text = string.Empty;
        ddlCertificate.SelectedIndex = 0;
        ddlSelectcertificate.SelectedIndex = 0;
        txtissuedate.Text = string.Empty;
        ddlconductcharacter.SelectedIndex = 0;
        btnSubmit.Visible = false;
     
    }
    #endregion

    #region Submit
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string CertShortName = objCommon.LookUp("ACD_CERTIFICATE_MASTER", "CERT_SHORT_NAME", "CERT_NO=" + Convert.ToInt32(ddlCertificate.SelectedValue) + "");
        decimal TuitionFee = 0.0m;
        decimal ExamFee = 0.0m;
        decimal OtherFee = 0.0m;
        decimal HostelFee = 0.0m;
        int orgid = 0;
        int Status = 0;
        string Branchcode = string.Empty;
        int tcpartfull = 0;
        if (Session["OrgId"].ToString() == "2")
        {
            if (txtissuedate.Text == "")
            {
                objCommon.DisplayUserMessage(updBonafide, "Please Enter Date of Issue", this.Page);
                return;
            }
        }
            int count = 0;
            foreach (ListViewDataItem lv in lvStudents.Items)
            {
                Panel1.Visible = true;
                CheckBox chkbox = lv.FindControl("cbRow") as CheckBox;
                int id = Convert.ToInt32(chkbox.ToolTip);
                ViewState["idno"] = id;
                Label ApproveStatus = lv.FindControl("lblApprovestatus") as Label;
                int approve_by = Convert.ToInt32(Session["userno"]);
                string ip_address = ViewState["IPAddress"].ToString();
                int cert_no = Convert.ToInt32(ddlCertificate.SelectedValue);
                if (chkbox.Checked == true)
                {
                    Status = 1;
                    CustomStatus cs = (CustomStatus)objCerti.UpdateApproveBonafideCertificateStatus(id, approve_by, ip_address, Status, cert_no);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        count++;
                    }
                }
            }
            if (count == 0)
            {
                objCommon.DisplayMessage(this.updBonafide, "Select Any One Record To Approve Student Certificate ", this.Page);
            }
            else if (count > 0)
            {
                objCommon.DisplayMessage(this.updBonafide, "Students Certificate Approved Successfully.", this.Page);
                if (Convert.ToInt32(Session["usertype"]) == 3 || Convert.ToInt32(Session["usertype"]) == 4)
                {
                    BindListViewFacuity();
                }
                else if (Convert.ToInt32(Session["usertype"]) == 1)
                {
                    BindListView();
                }
                CertificateMasterController objcertMasterController = new CertificateMasterController();
                CertificateMaster objcertMaster = new CertificateMaster();
                //HiddenField hfRow = (dataitem.FindControl("hidIdNo")) as HiddenField;
                objcertMaster.IdNo = Convert.ToInt32(ViewState["idno"].ToString());
                objcertMaster.IssueStatus = 1;
                objcertMaster.CertNo = Convert.ToInt32(ddlCertificate.SelectedValue);

                //Set to NULL (Leaving Certificate) 
                objcertMaster.Attendance = "NULL";
                objcertMaster.Conduct = ddlconductcharacter.SelectedItem.Text;
                objcertMaster.Conduct_No = Convert.ToInt32(ddlconductcharacter.SelectedValue);
                objcertMaster.CompleteProgram = "";
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
               // TextBox txtRemark = (dataitem.FindControl("txtRemark")) as TextBox;
                objcertMaster.Remark = "";
                objcertMaster.IpAddress = ViewState["ipAddress"].ToString();
                objcertMaster.UaNO = Convert.ToInt32(Session["userno"]);
                objcertMaster.CollegeCode = Session["colcode"].ToString();
                objcertMaster.SessionNo = 0;
                objcertMaster.SemesterNo = Convert.ToInt32(objCommon.LookUp("acd_student", "SEMESTERNO", "IDNO=" + Convert.ToInt32(ViewState["idno"].ToString())));
                objcertMaster.ConvocationDate ="";
                objcertMaster.Class = "";
                objcertMaster.RegNo = txtGReg.Text;

                //ADDED POOJA

                orgid = Convert.ToInt32(Session["OrgId"]);
                if (txtissuedate.Text.Trim() != "")
                {
                    objcertMaster.IssueDate = Convert.ToDateTime(txtissuedate.Text.ToString());
                }
                objcertMaster.LeavingDate = DateTime.Now;
                    objcertMaster.Reason = "";
                    if (ddlSelectcertificate.SelectedValue != "") tcpartfull = Convert.ToInt32(ddlSelectcertificate.SelectedValue); else tcpartfull = 0;
                    CustomStatus cs = (CustomStatus)objcertMasterController.AddBonafideCertificate(objcertMaster, TuitionFee, ExamFee, OtherFee, HostelFee, tcpartfull, orgid, Status, Branchcode);   
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Approve_Bonafide_Certificate.btnSubmit_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable");
        }
    }
    #endregion

    #region excel report
    protected void btnExcelReport_Click(object sender, EventArgs e)
    {
        try
        {
            GridView GVDayWiseAtt = new GridView();
            DataSet ds = new DataSet();
            ds = objCerti.GetExcelBonafideCertificateReport(Convert.ToInt32(ddlAdmbatch.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                GVDayWiseAtt.DataSource = ds;
                ViewState["Data"] = ds;
                GVDayWiseAtt.DataBind();
                string attachment = "attachment; filename=Approve_Bonafide_Certificate_Student" + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVDayWiseAtt.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(this.updBonafide, "No Data Found for current selection.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Approve_Bonafide_Certificate.btnExcelReport_Click()-> " + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable");
        }
    }
    #endregion

    #region show
    protected void ddlCertificate_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCertificate.SelectedIndex > 0)
            {
                string CertShortName = objCommon.LookUp("ACD_CERTIFICATE_MASTER", "CERT_SHORT_NAME", "CERT_NO=" + Convert.ToInt32(ddlCertificate.SelectedValue) + "");
                // string semesterNo = objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "(DURATION*2)", "DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + "");
                if (Session["OrgId"].ToString() == "2")
                {
                    if (CertShortName == "TC")
                    {
                        div2.Visible = true;
                        issuedate.Visible = true;
                        divconduct.Visible = true;
                        divGRegNo.Visible = false;
                        lvStudents.DataSource = null;
                        lvStudents.DataBind();
                        Panel1.Visible = false;
                        btnSubmit.Visible = false;

                    }
                    else
                    {
                        div2.Visible = false;
                        issuedate.Visible = true;
                        divconduct.Visible = false;
                        divGRegNo.Visible = false;
                        lvStudents.DataSource = null;
                        lvStudents.DataBind();
                        Panel1.Visible = false;
                        btnSubmit.Visible = false;
                    }

                }
                else
                {
                    div2.Visible = false;
                    issuedate.Visible = true;
                    divconduct.Visible = false;
                    divGRegNo.Visible = true;
                    lvStudents.DataSource = null;
                    lvStudents.DataBind();
                    btnSubmit.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Approve_Bonafide_Certificate.ddlCertificate_SelectedIndexChanged()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(Session["usertype"]) == 3 || Convert.ToInt32(Session["usertype"]) == 4)
            {
                BindListViewFacuity();
            }
            else if (Convert.ToInt32(Session["usertype"]) == 1)
            {
                BindListView();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Approve_Bonafide_Certificate.btnShow_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion
}