//======================================================================================
// PROJECT NAME  : UAIMS [NIT RAIPUR]                                                          
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : CERTIFICATE PRINTING                                       
// CREATION DATE : 03-Sept-2012                                                       
// CREATED BY    : ASHISH MOTGHARE                                        
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================


using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.IO;
using System.Web;
using System.Web.UI.HtmlControls;
using MessagingToolkit.QRCode.Codec;
using System.Drawing;
using ClosedXML.Excel;


public partial class ACADEMIC_CertificateMaster : System.Web.UI.Page
    {

    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    QrCodeController objQrC = new QrCodeController();
    bool IsDataPresent = false;

    #region Page Action
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
                        {
                        //   lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                        }
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                    //fill dropdown method
                    PopulateDropDown();
                    if (Convert.ToInt32(Session["userno"]) == 1 || Convert.ToInt32(Session["userno"]) == 169)
                        {
                        btnStatsticalReport.Visible = true;
                        }
                    else
                        {
                        btnStatsticalReport.Visible = false;
                        }

                    if (Session["OrgId"].ToString() == "2")
                        {
                        CompProgram.Visible = true;
                        }
                    else
                        {
                        CompProgram.Visible = false;
                        }
                    if (Convert.ToInt32(Session["OrgId"]) == 10)
                        {
                        btnStatsticalReport.Visible = false;
                        }


                    }
                }
            divMsg.InnerHtml = string.Empty;
            }
        catch (Exception ex)
            {
            throw;
            }
        }

    private void CheckPageAuthorization()
        {
        if (Request.QueryString["pageno"] != null)
            {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                Response.Redirect("~/notauthorized.aspx?page=CertificateMaster.aspx");
                }
            }
        else
            {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CertificateMaster.aspx");
            }
        }
    #endregion

    #region Other Certificates
    private void PopulateDropDown()
        {
        try
            {
            //    //Fill Dropdown session
            //    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO DESC");

            //    //Fill degree
            //    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
            //    //Fill Dropdown SEMESTER
            //    objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNAME");
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 ", "SESSIONNO desc"); //--AND FLOCK = 1
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID >0 AND COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID");
            // objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0 AND ACTIVESTATUS=1", "BATCHNO DESC");
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0 AND ACTIVESTATUS=1", "SEMESTERNO");

            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0 AND ACTIVESTATUS=1", "DEGREENO");


            //Fill Dropdown admbatch
            objCommon.FillDropDownList(ddlAdmBatch1, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0 AND ACTIVESTATUS=1", "BATCHNAME DESC");
            objCommon.FillDropDownList(ddlAcademicYear, "ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "ACADEMIC_YEAR_ID>0 AND ACTIVE_STATUS=1", "ACADEMIC_YEAR_NAME DESC");

            objCommon.FillDropDownList(ddlSession1, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO DESC");

            //Fill degree
            objCommon.FillDropDownList(ddlDegree1, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
            //Fill Dropdown SEMESTER
            objCommon.FillDropDownList(ddlSemester1, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNAME");
            //Fill Dropdown admbatch
            objCommon.FillDropDownList(ddlAdmBatch1, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNAME DESC");

            objCommon.FillDropDownList(ddlreason, "ACD_TC_REASON_MASTER", "ID", "REASON", "ID>0 AND ACTIVE_STATUS=1 ", "REASON DESC");

            //Fill Dropdown certificate
            //objCommon.FillDropDownList(ddlCert, "ACD_CERTIFICATE_MASTER", "CERT_NO", "CERT_NAME", "CERT_NO>0 AND CERT_NO not in (1)", "CERT_NAME");
            objCommon.FillDropDownList(ddlCert, "ACD_CERTIFICATE_MASTER", "CERT_NO", "CERT_NAME", "CERT_NO>0 AND ACTIVESTATUS=1", "CERT_NO");

            }
        catch (Exception ex)
            {
            throw;
            }
        }

    protected void btnShowData_Click(object sender, EventArgs e)
        {
        lvStudentRecords.Visible = true;
        btnPrint.Enabled = true;

        try
            {

            CertificateMasterController objcertMasterController = new CertificateMasterController();
            DataSet ds;
            string CertShortName = objCommon.LookUp("ACD_CERTIFICATE_MASTER", "CERT_SHORT_NAME", "CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "");

            if (Session["OrgId"].ToString() == "2")
                {
                if (CertShortName == "TC")
                    {
                    if (rdotcpartfull.SelectedValue == "")
                        {
                        objCommon.DisplayUserMessage(updpnlExam, "Please Select Certificate!", this.Page);
                        return;
                        }
                    }
                }
            int sessionNo = 0;
            int branchNo = 0;
            int semesterNo = 0;
            int admbatchNo = 0;
            int degreeNo = 0;
            int collegeNo = 0;
            int tcpartfullno = 0;

            //if (Session["BC_Student"] == null || ((DataTable)Session["BC_Student"] == null))
            //{
            sessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            branchNo = Convert.ToInt32(ddlBranch.SelectedValue);
            semesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            admbatchNo = Convert.ToInt32(ddlAdmBatch.SelectedValue);
            degreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            collegeNo = Convert.ToInt32(ddlCollege.SelectedValue);
            if (Convert.ToInt32(Session["OrgId"]) == 2)
                {
                if (rdotcpartfull.SelectedValue != "")
                    tcpartfullno = Convert.ToInt32(rdotcpartfull.SelectedValue);
                else
                    tcpartfullno = 0;

                }
            int certno = Convert.ToInt32(ddlCert.SelectedValue);
            if (rdotcpartfull.SelectedValue == "3")
                {
                ds = objcertMasterController.GetStudentListForDiscontinue(admbatchNo, sessionNo, collegeNo, degreeNo, branchNo, semesterNo, CertShortName, tcpartfullno, certno);
                if (ds.Tables[0].Rows.Count > 0)
                    {
                    //lvStudentRecords.DataSource = ds.Tables[0];
                    lvStudentRecords.DataSource = ds;
                    lvStudentRecords.DataBind();
                    //lvStudentRecords.Visible = true;
                    objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudentRecords);//Set label 
                    //btnConfirm_BC.Enabled = true;
                    // btnReport.Enabled = true;
                    foreach (ListViewDataItem lvHead in lvStudentRecords.Items)
                        {
                        DropDownList ddlconductcharacter = lvHead.FindControl("ddlconductcharacter") as DropDownList;
                        HiddenField hfConductNo = lvHead.FindControl("hfConductNo") as HiddenField;
                        objCommon.FillDropDownList(ddlconductcharacter, "acd_tc_conduct_character", "CNO", "CONDUCT_CHARACTER", "CNO>0 AND  ACTIVESTATUS=1", "CNO");
                        ddlconductcharacter.SelectedValue = hfConductNo.Value;
                        ddlconductcharacter.SelectedIndex = Convert.ToInt32(hfConductNo.Value);
                        }

                    if (Convert.ToInt32(Session["OrgId"]) == 2)
                        {
                        if (CertShortName == "TC")
                            {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "$('#thconduct').show();$('td:nth-child(9)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thconduct').show();$('td:nth-child(9)').show();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun1", "$('#thcount').hide();$('td:nth-child(10)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thcount').hide();$('td:nth-child(10)').hide();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun2", "$('#thGReg').hide();$('td:nth-child(11)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thGReg').hide();$('td:nth-child(11)').hide();});", true);
                            }
                        else
                            {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "$('#thconduct').hide();$('td:nth-child(9)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thconduct').hide();$('td:nth-child(9)').hide();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun1", "$('#thcount').hide();$('td:nth-child(10)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thcount').hide();$('td:nth-child(10)').hide();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun2", "$('#thGReg').hide();$('td:nth-child(11)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thGReg').hide();$('td:nth-child(11)').hide();});", true);
                            }

                        }
                    else if (Convert.ToInt32(Session["OrgId"]) == 1)
                        {
                        if (CertShortName == "LC")
                            {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "$('#thconduct').hide();$('td:nth-child(9)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thconduct').hide();$('td:nth-child(9)').hide();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun1", "$('#thcount').show();$('td:nth-child(10)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thcount').show();$('td:nth-child(10)').show();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun2", "$('#thGReg').hide();$('td:nth-child(11)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thGReg').hide();$('td:nth-child(11)').hide();});", true);
                            }
                        else
                            {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "$('#thconduct').show();$('td:nth-child(9)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thconduct').hide();$('td:nth-child(9)').hide();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun1", "$('#thcount').hide();$('td:nth-child(10)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thcount').hide();$('td:nth-child(10)').hide();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun2", "$('#thGReg').hide();$('td:nth-child(11)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thGReg').hide();$('td:nth-child(11)').hide();});", true);
                            }
                        }
                    else if (Convert.ToInt32(Session["OrgId"]) == 6)
                        {
                        if (CertShortName == "TC")
                            {
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun2", "$('#thGReg').show();$('td:nth-child(11)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thGReg').show();$('td:nth-child(11)').show();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "$('#thconduct').show();$('td:nth-child(9)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thconduct').show();$('td:nth-child(9)').show();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun1", "$('#thcount').hide();$('td:nth-child(10)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thcount').hide();$('td:nth-child(10)').hide();});", true);
                            }
                        else
                            {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "$('#thconduct').hide();$('td:nth-child(9)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thconduct').hide();$('td:nth-child(9)').hide();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun1", "$('#thcount').hide();$('td:nth-child(10)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thcount').hide();$('td:nth-child(10)').hide();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun2", "$('#thGReg').hide();$('td:nth-child(11)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thGReg').hide();$('td:nth-child(11)').hide();});", true);
                            }
                        }
                    else
                        {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "$('#thconduct').hide();$('td:nth-child(9)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thconduct').hide();$('td:nth-child(9)').hide();});", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "myfun1", "$('#thcount').hide();$('td:nth-child(10)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thcount').hide();$('td:nth-child(10)').hide();});", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "myfun2", "$('#thGReg').hide();$('td:nth-child(11)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thGReg').hide();$('td:nth-child(11)').hide();});", true);
                        }

                    }
                else
                    {
                    objCommon.DisplayMessage(this.updpnlExam, "No student data found", this.Page);
                    lvStudentRecords.DataSource = null;
                    lvStudentRecords.DataBind();
                    // btnConfirm_BC.Enabled = false;
                    // btnReport.Enabled = false;
                    }


                }
            else
                {
                ds = objcertMasterController.GetStudentListForBC(admbatchNo, sessionNo, collegeNo, degreeNo, branchNo, semesterNo, CertShortName, tcpartfullno, certno);
                if (ds.Tables[0].Rows.Count > 0)
                    {
                    //lvStudentRecords.DataSource = ds.Tables[0];
                    lvStudentRecords.DataSource = ds;
                    lvStudentRecords.DataBind();
                    //lvStudentRecords.Visible = true;
                    objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudentRecords);//Set label 
                    //btnConfirm_BC.Enabled = true;
                    // btnReport.Enabled = true;
                    foreach (ListViewDataItem lvHead in lvStudentRecords.Items)
                        {
                        DropDownList ddlconductcharacter = lvHead.FindControl("ddlconductcharacter") as DropDownList;
                        HiddenField hfConductNo = lvHead.FindControl("hfConductNo") as HiddenField;
                        objCommon.FillDropDownList(ddlconductcharacter, "acd_tc_conduct_character", "CNO", "CONDUCT_CHARACTER", "CNO>0 AND  ACTIVESTATUS=1", "CNO");
                        ddlconductcharacter.SelectedValue = hfConductNo.Value;
                        ddlconductcharacter.SelectedIndex = Convert.ToInt32(hfConductNo.Value);
                        }

                    if (Convert.ToInt32(Session["OrgId"]) == 2)
                        {
                        if (CertShortName == "TC")
                            {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "$('#thconduct').show();$('td:nth-child(9)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thconduct').show();$('td:nth-child(9)').show();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun1", "$('#thcount').hide();$('td:nth-child(10)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thcount').hide();$('td:nth-child(10)').hide();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun2", "$('#thGReg').hide();$('td:nth-child(11)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thGReg').hide();$('td:nth-child(11)').hide();});", true);
                            }
                        else
                            {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "$('#thconduct').hide();$('td:nth-child(9)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thconduct').hide();$('td:nth-child(9)').hide();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun1", "$('#thcount').hide();$('td:nth-child(10)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thcount').hide();$('td:nth-child(10)').hide();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun2", "$('#thGReg').hide();$('td:nth-child(11)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thGReg').hide();$('td:nth-child(11)').hide();});", true);
                            }

                        }
                    else if (Convert.ToInt32(Session["OrgId"]) == 1)
                        {
                        if (CertShortName == "LC")
                            {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "$('#thconduct').hide();$('td:nth-child(9)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thconduct').hide();$('td:nth-child(9)').hide();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun1", "$('#thcount').show();$('td:nth-child(10)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thcount').show();$('td:nth-child(10)').show();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun2", "$('#thGReg').hide();$('td:nth-child(11)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thGReg').hide();$('td:nth-child(11)').hide();});", true);
                            }
                        else
                            {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "$('#thconduct').show();$('td:nth-child(9)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thconduct').hide();$('td:nth-child(9)').hide();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun1", "$('#thcount').hide();$('td:nth-child(10)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thcount').hide();$('td:nth-child(10)').hide();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun2", "$('#thGReg').hide();$('td:nth-child(11)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thGReg').hide();$('td:nth-child(11)').hide();});", true);
                            }
                        }
                    else if (Convert.ToInt32(Session["OrgId"]) == 6)
                        {
                        if (CertShortName == "TC")
                            {
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun2", "$('#thGReg').show();$('td:nth-child(11)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thGReg').show();$('td:nth-child(11)').show();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "$('#thconduct').show();$('td:nth-child(9)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thconduct').show();$('td:nth-child(9)').show();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun1", "$('#thcount').hide();$('td:nth-child(10)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thcount').hide();$('td:nth-child(10)').hide();});", true);
                            }
                        else
                            {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "$('#thconduct').hide();$('td:nth-child(9)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thconduct').hide();$('td:nth-child(9)').hide();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun1", "$('#thcount').hide();$('td:nth-child(10)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thcount').hide();$('td:nth-child(10)').hide();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun2", "$('#thGReg').hide();$('td:nth-child(11)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thGReg').hide();$('td:nth-child(11)').hide();});", true);
                            }
                        }
                    else if (Convert.ToInt32(Session["OrgId"]) == 19 || Convert.ToInt32(Session["OrgId"]) == 20)
                        {
                        if (CertShortName == "LC")
                            {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "$('#thconduct').hide();$('td:nth-child(9)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thconduct').show();$('td:nth-child(9)').show();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun1", "$('#thcount').show();$('td:nth-child(10)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thcount').show();$('td:nth-child(10)').show();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun2", "$('#thGReg').hide();$('td:nth-child(11)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thGReg').hide();$('td:nth-child(11)').hide();});", true);
                            }
                        else
                            {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "$('#thconduct').show();$('td:nth-child(9)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thconduct').hide();$('td:nth-child(9)').hide();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun1", "$('#thcount').hide();$('td:nth-child(10)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thcount').hide();$('td:nth-child(10)').hide();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun2", "$('#thGReg').hide();$('td:nth-child(11)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thGReg').hide();$('td:nth-child(11)').hide();});", true);
                            }
                        }
                    else
                        {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "$('#thconduct').hide();$('td:nth-child(9)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thconduct').hide();$('td:nth-child(9)').hide();});", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "myfun1", "$('#thcount').hide();$('td:nth-child(10)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thcount').hide();$('td:nth-child(10)').hide();});", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "myfun2", "$('#thGReg').hide();$('td:nth-child(11)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thGReg').hide();$('td:nth-child(11)').hide();});", true);
                        }

                    }
                //}
                else
                    {
                    objCommon.DisplayMessage(this.updpnlExam, "No student data found", this.Page);
                    lvStudentRecords.DataSource = null;
                    lvStudentRecords.DataBind();
                    // btnConfirm_BC.Enabled = false;
                    // btnReport.Enabled = false;
                    }
                }

            }
        catch (Exception ex)
            {
            throw;
            }
        }

    protected void btnShowData1_Click(object sender, EventArgs e)
        {
        try
            {
            DataSet dsBC;
            //DataSet dsissueCert;

            CertificateMasterController objcerMasterController = new CertificateMasterController();

            //DataSet ds;
            int sessionNo = 0;
            int branchNo = 0;
            int semesterNo = 0;
            int admbatchNo = 0;
            //string idNo = objCommon.LookUp("ACD_STUDENT", "IDNO", "CAN = 0 AND ADMCAN=0 AND REGNO='" + txtSearch_Enrollno_LC.Text + "'");
            //ViewState["idno"] = idNo;
            sessionNo = Convert.ToInt32(ddlSession1.SelectedValue);
            branchNo = Convert.ToInt32(ddlBranch1.SelectedValue);
            semesterNo = Convert.ToInt32(ddlSemester1.SelectedValue);
            admbatchNo = Convert.ToInt32(ddlAdmBatch1.SelectedValue);


            dsBC = objcerMasterController.GetStudentListForTC(branchNo, semesterNo, admbatchNo);
            if (dsBC.Tables[0].Rows.Count > 0)
                {

                lvIssueCert.DataSource = null;
                lvIssueCert.DataBind();

                lvStudentRecords_LC.DataSource = dsBC.Tables[0];
                lvStudentRecords_LC.DataBind();


                }
            else
                {
                objCommon.DisplayMessage(this.updpnlExam, "No student data found", this);
                lvStudentRecords_LC.DataSource = null;
                lvStudentRecords_LC.DataBind();
                }
            //}
            }
        catch (Exception ex)
            {
            throw;
            }

        }

    //TO SAVE THE INFORMATION OF ISSUING CERTIFICATE TO STUDENT
    protected void btnConfirm_BC_Click(object sender, EventArgs e)
        {
        string studentIds = string.Empty;
        //Get Student Details from lvStudent
        string CertShortName = objCommon.LookUp("ACD_CERTIFICATE_MASTER", "CERT_SHORT_NAME", "CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "");
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

            //if (ddlCert.SelectedValue == "2")    //ADDED BY VINAY MISHRA ON 28082023 FOR TICKET 47439 - Only for TC
            //{
            if (rdotcpartfull.SelectedValue == "")
                {
                objCommon.DisplayUserMessage(updpnlExam, "Please Select Certificate!", this.Page);
                return;
                }
            //else
            //{
            if (txtissuedate.Text == "")
                {
                objCommon.DisplayUserMessage(updpnlExam, "Please Enter Date of Issue", this.Page);
                return;
                }
            //}
            if (Chkstatus.Checked == true)
                {
                //if (txtbranchcode.Text == "")
                //{
                //    objCommon.DisplayUserMessage(updpnlExam, "Please Enter Branch/Programme", this.Page);
                //    return;
                //}
                }
            //}

            }
        //if (Session["OrgId"].ToString() == "6")
        //{
        //    if (txtGregno.Text == string.Empty || txtGregno.Text == "")
        //    {
        //        objCommon.DisplayUserMessage(updpnlExam, "Please Enter G.REG.No.", this.Page);
        //        return;
        //    }
        //}
        int count = 0;
        foreach (ListViewDataItem dataitem in lvStudentRecords.Items)
            {
            CheckBox cbRow = dataitem.FindControl("chkReport") as CheckBox;
            if (cbRow.Checked == true)
                count++;
            }
        if (count <= 0)
            {
            objCommon.DisplayMessage(this.updpnlExam, "Please Select atleast one Student for issuing Certificate", this);
            return;
            }

        ViewState["studcount"] = count;
        count = 0;

        CertificateMasterController objcertMasterController = new CertificateMasterController();
        CertificateMaster objcertMaster = new CertificateMaster();
        //if (ddlCert.SelectedValue == "7")
        //{
        //    DataSet ids = objCommon.FillDropDown("ACD_CERT_TRAN CT INNER JOIN ACD_STUDENT S ON(CT.IDNO=S.IDNO)", "S.REGNO", "S.STUDNAME", "CT.CERT_NO=1 AND CT.IDNO IN (" + GetStudentIDs().ToString().Replace("$", ",") + ")", "S.REGNO");
        //    if (ids.Tables[0].Rows.Count != Convert.ToInt32(ViewState["studcount"]))
        //    {
        //        //objCommon.DisplayMessage("Only " + ids.Tables[0].Rows[0]["STUDNAME"].ToString() + " is able to get " + ddlCert.SelectedItem.Text + " Certificate. Please issue Leaving Certificate First.", this);
        //        objCommon.DisplayMessage("Charactor Certificate is issue only after the Leaving Certificate is issued.", this);
        //        return;
        //    }
        //}
        try
            {

            foreach (ListViewDataItem dataitem in lvStudentRecords.Items)
                {
                //Get Student Details from lvStudent
                CheckBox cbRow = dataitem.FindControl("chkReport") as CheckBox;
                DropDownList ddlConductCharacter = dataitem.FindControl("ddlconductcharacter") as DropDownList;
                TextBox txtGReg = dataitem.FindControl("txtGReg") as TextBox;
                if (cbRow.Checked == true)
                    {
                    studentIds += cbRow.ToolTip + "$";

                    if (Session["OrgId"].ToString() == "2")
                        {
                        if (CertShortName == "TC")
                            {
                            if (cbRow.Checked && ddlConductCharacter.SelectedIndex == 0)
                                {
                                objCommon.DisplayMessage(this.Page, "Please Select Conduct & Character for selected Students.", this.Page);
                                return;

                                }
                            }
                        }

                    if (Session["OrgId"].ToString() == "6")
                        {
                        if (CertShortName == "TC")
                            {
                            if (cbRow.Checked && txtGReg.Text == string.Empty)
                                {
                                objCommon.DisplayMessage(this.Page, "Please Enter General Registration No.", this.Page);
                                txtGReg.Focus();
                                return;
                                }
                            if (cbRow.Checked && ddlConductCharacter.SelectedIndex == 0)
                                {
                                objCommon.DisplayMessage(this.Page, "Please Select Conduct & Character for selected Students.", this.Page);
                                return;

                                }
                            }
                        }

                    if (Session["OrgId"].ToString() == "10" || Session["OrgId"].ToString() == "11")
                        {
                        //Added by pooja for estimate certificate for prmitr ON ADTE 01-08-2023
                        string demand = objCommon.LookUp("ACD_DEMAND", "distinct 1", "IDNO=" + studentIds.TrimEnd('$') + "");
                        if (CertShortName == "EC")
                            {
                            //string demand = objCommon.LookUp("ACD_DEMAND", "distinct 1", "IDNO=" + IDNO + "");
                            if (demand == string.Empty)
                                {
                                objCommon.DisplayMessage(this.Page, "Please Create Demand for Estimate Certificate..!!!", this.Page);
                                return;
                                }
                            }

                        }
                    HiddenField hfRow = (dataitem.FindControl("hidIdNo")) as HiddenField;
                    objcertMaster.IdNo = Convert.ToInt32(hfRow.Value);
                    objcertMaster.IssueStatus = 1;
                    objcertMaster.CertNo = Convert.ToInt32(ddlCert.SelectedValue);

                    //Set to NULL (Leaving Certificate) 
                    objcertMaster.Attendance = "NULL";
                    objcertMaster.Conduct = ddlConductCharacter.SelectedItem.Text;
                    objcertMaster.Conduct_No = Convert.ToInt32(ddlConductCharacter.SelectedValue);
                    objcertMaster.CompleteProgram = Convert.ToString(ddlcompleteporg.SelectedItem.Text);

                    TextBox txtRemark = (dataitem.FindControl("txtRemark")) as TextBox;
                    objcertMaster.Remark = txtRemark.Text;
                    objcertMaster.IpAddress = ViewState["ipAddress"].ToString();
                    objcertMaster.UaNO = Convert.ToInt32(Session["userno"]);
                    objcertMaster.CollegeCode = Session["colcode"].ToString();
                    objcertMaster.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
                    objcertMaster.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
                    objcertMaster.ConvocationDate = txtConvocation.Text;
                    objcertMaster.Class = txtClass.Text;
                    objcertMaster.RegNo = txtGReg.Text;

                    //ADDED POOJA

                    orgid = Convert.ToInt32(Session["OrgId"]);
                    if (txtissuedate.Text.Trim() != "")
                        {
                        objcertMaster.IssueDate = Convert.ToDateTime(txtissuedate.Text.ToString());
                        }

                    if (Chkstatus != null && Chkstatus.Checked)
                        {
                        if (txtbranchcode.Text.Trim() != "")
                            {
                            Status = Convert.ToInt32(Chkstatus.Checked);
                            Branchcode = txtbranchcode.Text;
                            }
                        }
                    if (txtleaving.Text.Trim() != "")
                        {
                        objcertMaster.LeavingDate = Convert.ToDateTime(txtleaving.Text.ToString());
                        }


                    objcertMaster.Reason = ddlreason.SelectedItem.Text.Trim();

                    if (txtTuitionFee.Text.Trim() != "")
                        TuitionFee = Convert.ToDecimal(txtTuitionFee.Text.Trim());
                    else
                        TuitionFee = 0.0m;
                    if (txtExamFee.Text.Trim() != "")
                        ExamFee = Convert.ToDecimal(txtExamFee.Text.Trim());
                    else
                        ExamFee = 0.0m;
                    if (txtOtherFee.Text.Trim() != "")
                        OtherFee = Convert.ToDecimal(txtOtherFee.Text.Trim());
                    else
                        OtherFee = 0.0m;
                    if (txtHostelFee.Text.Trim() != "")
                        HostelFee = Convert.ToDecimal(txtHostelFee.Text.Trim());
                    else
                        HostelFee = 0.0m;
                    if (rdotcpartfull.SelectedValue != "")
                        tcpartfull = Convert.ToInt32(rdotcpartfull.SelectedValue);
                    else
                        tcpartfull = 0;


                    //insert bc certificate
                    CustomStatus cs = (CustomStatus)objcertMasterController.AddBonafideCertificate(objcertMaster, TuitionFee, ExamFee, OtherFee, HostelFee, tcpartfull, orgid, Status, Branchcode);
                    if (cs.Equals(CustomStatus.RecordSaved))
                        {
                        objCommon.DisplayMessage(this.updpnlExam, "Process Done Successfully !!!", this);
                        Enable_False();
                        btnReport.Enabled = true;
                        //if (ddlCert.SelectedValue == "6")
                        //{

                        //    btnExcelSheetReport.Enabled = true;
                        //}
                        }
                    else
                        {
                        btnReport.Enabled = false;
                        objCommon.DisplayMessage(this.updpnlExam, "Error !!!", this);
                        }
                    count++;
                    }
                }

            btnReport.Enabled = true;
            //GenerateQRCode();
            }
        catch (Exception ex)
            {
            throw;
            }
        }

    //Set all controls Enable = false after conform for printing certificate
    private void Enable_False()
        {
        btnConfirm_BC.Enabled = false;
        txtSearch_Enrollno_BC.Enabled = false;
        btnSearch_BC.Enabled = false;
        ddlSession.Enabled = false;
        ddlBranch.Enabled = false;
        ddlSemester.Enabled = false;
        ddlAdmBatch.Enabled = false;
        ddlCert.Enabled = false;



        ddlreason.Enabled = false;
        txtleaving.Enabled = false;
        //ddlEle1.Enabled = false;
        //ddlEle2.Enabled = false;
        //txtProjName.Enabled = false;
        //txtSpecProject.Enabled = false;
        //txtNoUniv.Enabled = false;
        //txtNoCopy.Enabled = false;
        btnShowData.Enabled = false;
        //btnCancel.Enabled = false;
        ddlcompleteporg.Enabled = false;
        }

    //Set all controls Enable = True after printing certificate
    private void Enable_True()
        {
        btnConfirm_BC.Enabled = true;
        txtSearch_Enrollno_BC.Enabled = true;
        btnSearch_BC.Enabled = true;
        ddlSession.Enabled = true;
        ddlBranch.Enabled = true;
        ddlSemester.Enabled = true;
        ddlAdmBatch.Enabled = true;
        ddlCert.Enabled = true;


        ddlreason.Enabled = true;
        txtleaving.Enabled = true;
        //ddlEle1.Enabled = true;
        //ddlEle2.Enabled = true;
        //txtProjName.Enabled = true;
        //txtSpecProject.Enabled = true;
        //txtNoUniv.Enabled = true;
        //txtNoCopy.Enabled = true;
        btnShowData.Enabled = true;
        btnCancel.Enabled = true;
        ddlcompleteporg.Enabled = true;
        }

    //single certificate print
    protected void btnSearch_BC_Click(object sender, EventArgs e)
        {
        lvStudentRecords.Visible = true;
        try
            {
            lvIssueCertBona.DataSource = null;
            lvIssueCertBona.DataBind();
            DataSet dsBC;
            CertificateMasterController objcerMasterController = new CertificateMasterController();
            string CertShortName = objCommon.LookUp("ACD_CERTIFICATE_MASTER", "CERT_SHORT_NAME", "CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "");

            string idNo = objCommon.LookUp("ACD_STUDENT", "IDNO", "ENROLLNO = '" + txtSearch_Enrollno_BC.Text + "' OR REGNO='" + txtSearch_Enrollno_BC.Text.Trim() + "'");
            //Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "ENROLLNO = '" + txtAdmissionNo.Text + "' OR REGNO='" + txtAdmissionNo.Text + "'"));
            if (idNo != "" && idNo != null)
                {
                int chkidNo = Convert.ToInt32(idNo);
                dsBC = objcerMasterController.GetStudentListForBC_BYIDNO(chkidNo);//SEARCH STUDENT FOR OTHER CERTIFICATES BY REG. NO.
                if (dsBC.Tables[0].Rows.Count > 0)
                    {
                    lvStudentRecords.DataSource = dsBC.Tables[0];
                    lvStudentRecords.DataBind();
                    objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudentRecords);//Set label
                    foreach (ListViewDataItem lvHead in lvStudentRecords.Items)
                        {
                        DropDownList ddlconductcharacter = lvHead.FindControl("ddlconductcharacter") as DropDownList;
                        HiddenField hfConductNo = lvHead.FindControl("hfConductNo") as HiddenField;
                        objCommon.FillDropDownList(ddlconductcharacter, "acd_tc_conduct_character", "CNO", "CONDUCT_CHARACTER", "CNO>0 AND  ACTIVESTATUS=1", "CNO");
                        ddlconductcharacter.SelectedValue = hfConductNo.Value;
                        ddlconductcharacter.SelectedIndex = Convert.ToInt32(hfConductNo.Value);
                        }

                    if (Convert.ToInt32(Session["OrgId"]) == 2)
                        {
                        if (CertShortName == "TC")
                            {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "$('#thconduct').show();$('td:nth-child(9)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thconduct').show();$('td:nth-child(9)').show();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun1", "$('#thcount').hide();$('td:nth-child(10)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thcount').hide();$('td:nth-child(10)').hide();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun2", "$('#thGReg').hide();$('td:nth-child(11)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thGReg').hide();$('td:nth-child(11)').hide();});", true);
                            }
                        else
                            {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "$('#thconduct').hide();$('td:nth-child(9)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thconduct').hide();$('td:nth-child(9)').hide();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun1", "$('#thcount').hide();$('td:nth-child(10)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thcount').hide();$('td:nth-child(10)').hide();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun2", "$('#thGReg').hide();$('td:nth-child(11)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thGReg').hide();$('td:nth-child(11)').hide();});", true);
                            }

                        }
                    else if (Convert.ToInt32(Session["OrgId"]) == 1)
                        {
                        if (CertShortName == "LC")
                            {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "$('#thconduct').hide();$('td:nth-child(9)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thconduct').hide();$('td:nth-child(9)').hide();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun1", "$('#thcount').show();$('td:nth-child(10)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thcount').show();$('td:nth-child(10)').show();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun2", "$('#thGReg').hide();$('td:nth-child(11)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thGReg').hide();$('td:nth-child(11)').hide();});", true);
                            }
                        else
                            {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "$('#thconduct').show();$('td:nth-child(9)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thconduct').hide();$('td:nth-child(9)').hide();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun1", "$('#thcount').hide();$('td:nth-child(10)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thcount').hide();$('td:nth-child(10)').hide();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun2", "$('#thGReg').hide();$('td:nth-child(11)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thGReg').hide();$('td:nth-child(11)').hide();});", true);
                            }
                        }
                    else if (Convert.ToInt32(Session["OrgId"]) == 6)
                        {
                        if (CertShortName == "TC")
                            {
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun2", "$('#thGReg').show();$('td:nth-child(11)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thGReg').show();$('td:nth-child(11)').show();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "$('#thconduct').show();$('td:nth-child(9)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thconduct').show();$('td:nth-child(9)').show();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun1", "$('#thcount').hide();$('td:nth-child(10)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thcount').hide();$('td:nth-child(10)').hide();});", true);
                            }
                        else
                            {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "$('#thconduct').hide();$('td:nth-child(9)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thconduct').hide();$('td:nth-child(9)').hide();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun1", "$('#thcount').hide();$('td:nth-child(10)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thcount').hide();$('td:nth-child(10)').hide();});", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "myfun2", "$('#thGReg').hide();$('td:nth-child(11)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thGReg').hide();$('td:nth-child(11)').hide();});", true);
                            }
                        }
                    else
                        {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "$('#thconduct').hide();$('td:nth-child(9)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thconduct').hide();$('td:nth-child(9)').hide();});", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "myfun1", "$('#thcount').hide();$('td:nth-child(10)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thcount').hide();$('td:nth-child(10)').hide();});", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "myfun2", "$('#thGReg').hide();$('td:nth-child(11)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thGReg').hide();$('td:nth-child(11)').hide();});", true);
                        }
                    }
                else
                    {
                    objCommon.DisplayMessage(this.updpnlExam, "No student data found", this);
                    lvStudentRecords.DataSource = null;
                    lvStudentRecords.DataBind();
                    }
                }
            else
                {
                objCommon.DisplayMessage(this.updpnlExam, "No student found having enrollment no.: " + txtSearch_Enrollno_BC.Text.Trim(), this);
                }
            }
        catch (Exception ex)
            {
            throw;
            }
        }

    protected void btnCancel_Click(object sender, EventArgs e)
        {
        Response.Redirect(Request.Url.ToString());
        }

    private string GetStudentIDs()
        {
        string studentIds = string.Empty;
        try
            {
            foreach (ListViewDataItem item in lvStudentRecords.Items)
                {
                if ((item.FindControl("chkReport") as CheckBox).Checked)
                    {
                    if (studentIds.Length > 0)
                        studentIds += "$";
                    studentIds += (item.FindControl("hidIdNo") as HiddenField).Value.Trim();
                    }
                }
            }
        catch (Exception ex)
            {
            throw;
            }
        return studentIds;
        }

    private string GetStudentIDsTC()
        {
        string studentIds = string.Empty;
        try
            {
            if (txtSearch_Enrollno_LC.Text != string.Empty)
                {
                string idNo = objCommon.LookUp("ACD_STUDENT", "IDNO", "CAN = 0 AND ADMCAN=0 AND REGNO='" + txtSearch_Enrollno_LC.Text + "'");
                ViewState["idno"] = idNo;


                int cntIdno = Convert.ToInt32(objCommon.LookUp("ACD_CERT_TRAN", "COUNT(IDNO)", "IDNO=" + idNo + "AND CERT_NO = 4"));
                // aleady certificate issue
                if (cntIdno <= 0)
                    {
                    foreach (ListViewDataItem item in lvStudentRecords_LC.Items)
                        {
                        if ((item.FindControl("chkReport") as CheckBox).Checked)
                            {

                            if (studentIds.Length > 0)
                                studentIds += "$";
                            studentIds += (item.FindControl("hidIdNo") as HiddenField).Value.Trim();

                            }
                        }
                    }
                else
                    {
                    foreach (ListViewDataItem item in lvIssueCert.Items)
                        {
                        if ((item.FindControl("chkReport") as CheckBox).Checked)
                            {

                            if (studentIds.Length > 0)
                                studentIds += "$";
                            studentIds += (item.FindControl("hidIdNo") as HiddenField).Value.Trim();

                            }
                        }
                    }
                }
            else
                {
                foreach (ListViewDataItem item in lvStudentRecords_LC.Items)
                    {
                    if ((item.FindControl("chkReport") as CheckBox).Checked)
                        {

                        if (studentIds.Length > 0)
                            studentIds += "$";
                        studentIds += (item.FindControl("hidIdNo") as HiddenField).Value.Trim();

                        }
                    }
                }
            }
        catch (Exception ex)
            {
            throw;
            }
        return studentIds;
        }

    protected void btnReportWithHeader_Click(object sender, EventArgs e)
        {
        if (ddlCert.SelectedValue == "1")
            {
            ShowReport_BC(GetStudentIDs(), "Bonafide_Certificate", "bonafide_certificate.rpt");
            }
        else if (ddlCert.SelectedValue == "2")
            {
            ShowReport_BC(GetStudentIDs(), "Passport_Certificate", "Passport_certificate.rpt");
            }
        else if (ddlCert.SelectedValue == "3")
            {
            ShowReport_BC(GetStudentIDs(), "Bonafide_Certificate_fees", "Other_certificate.rpt");
            }
        else if (ddlCert.SelectedValue == "6")
            {
            ShowReport_PVC(GetStudentIDs(), "Provisional_Certificate", "rptProvisionalCertificateWithHeader.rpt");
            }
        else if (ddlCert.SelectedValue == "7")
            {
            ShowReport_PVC(GetStudentIDs(), "Bonafide_Passed_Certificate", "bonafide_passed_certificate.rpt");
            }
        else if (ddlCert.SelectedValue == "9")
            {
            ShowReport_BC(GetStudentIDs(), "Bonafide_SGPA_CGPA_Certificate", "bonafide_certificate_SGPA_CGPA_WIthoutHeader.rpt");
            }
        else if (ddlCert.SelectedValue == "10")
            {
            ShowReport_BC(GetStudentIDs(), "Bonafide_SGPA_CGPA_Certificate", "bonafide_certificate_SGPA_CGPA_SEM.rpt");
            }
        //btnPrint.Enabled = false;
        Enable_True();
        }

    protected void btnReport_Click(object sender, EventArgs e)
        {
        //if (txtConvocation.Text == "")
        //{
        //    objCommon.DisplayMessage(this.updpnlExam, "Please Enter Convocation Date", this);
        //}
        //else
        int count = 0;
        foreach (ListViewDataItem dataitem in lvStudentRecords.Items)
            {
            CheckBox cbRow = dataitem.FindControl("chkReport") as CheckBox;
            if (cbRow.Checked == true)
                count++;
            }
        if (count <= 0)
            {
            objCommon.DisplayMessage(this.updpnlExam, "Please Select atleast one Student for issuing Certificate", this);
            return;
            }
        else
            {
            string CertShortName = objCommon.LookUp("ACD_CERTIFICATE_MASTER", "CERT_SHORT_NAME", "CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "");

            //if (ddlCert.SelectedValue == "1")
            if (CertShortName == "PC")
                {
                ShowReport_BC(GetStudentIDs(), "Provisional_Certificate", "Provisional_Certificate.rpt");
                }
            //else if (ddlCert.SelectedValue == "3")

             //added by pooja
            else if (CertShortName == "MC")
                {
                if (Session["OrgId"].ToString() == "1")
                    {
                    if (ddlAcademicYear.SelectedIndex == 0)
                        {
                        objCommon.DisplayMessage(this.updpnlExam, "Please Select Academic Year", this);
                        ddlAcademicYear.Focus();
                        return;
                        }
                    ShowReport_BC(GetStudentIDs(), "Migration_Certificate", "Migration_TC_RCPIT.rpt");
                    }
                else if (Session["OrgId"].ToString() == "3")
                    {
                    if (ddlAcademicYear.SelectedIndex == 0)
                        {
                        objCommon.DisplayMessage(this.updpnlExam, "Please Select Academic Year", this);
                        ddlAcademicYear.Focus();
                        return;
                        }
                    ShowReport_BC(GetStudentIDs(), "Migration_Certificate", "Migration_TC_CPUH.rpt");
                    }
                if (Session["OrgId"].ToString() == "4")
                    {
                    if (ddlAcademicYear.SelectedIndex == 0)
                        {
                        objCommon.DisplayMessage(this.updpnlExam, "Please Select Academic Year", this);
                        ddlAcademicYear.Focus();
                        return;
                        }
                    ShowReport_BC(GetStudentIDs(), "Migration_Certificate", "Migration_TC_CPUHAMIRPUR.rpt");
                    }
                else
                    {
                    ShowReport_BC(GetStudentIDs(), "Migration_Certificate", "Migration_Certificate_New.rpt");
                    }
                }
            //else if (ddlCert.SelectedValue == "4")
            else if (CertShortName == "TC")
                {
                if (Session["OrgId"].ToString() == "2")
                    {
                    if (rdotcpartfull.SelectedValue == "1" || rdotcpartfull.SelectedValue == "2")
                        {
                        int shift = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "distinct SHIFT", "IDNO IN ( SELECT * FROM fn_Split(REPLACE('" + GetStudentIDs() + "', '$',','),','))"));
                        //if (Convert.ToInt32(rdotcpartfull.SelectedValue) == 1)
                        if (shift == 1)
                            {

                            ShowReport_TC_CERT(GetStudentIDs(), "Leaving_Certificate", "rptFullTimeTC_Crescent.rpt");
                            }
                        else if (shift == 2)
                            {
                            ShowReport_TC_CERT(GetStudentIDs(), "Leaving_Certificate", "rptPartTimeTC.rpt");
                            }
                        }
                    else
                        {
                        ShowReport_TC_CERT(GetStudentIDs(), "Leaving_Certificate", "rptDiscontinue_TC_Crescent.rpt");
                        }
                    }
                else if (Session["OrgId"].ToString() == "6")
                    {
                    if (ddlAcademicYear.SelectedIndex == 0)
                        {
                        objCommon.DisplayMessage(this.updpnlExam, "Please Select Academic Year", this);
                        ddlAcademicYear.Focus();
                        return;
                        }
                    else
                        {
                        ShowReport_TC_CERT(GetStudentIDs(), "Leaving_Certificate", "RCPiperReport.rpt");
                        }
                    }
                //
                else if (Session["OrgId"].ToString() == "1")
                    {

                    if (ddlAcademicYear.SelectedIndex == 0)
                        {
                        objCommon.DisplayMessage(this.updpnlExam, "Please Select Academic Year", this);
                        ddlAcademicYear.Focus();
                        return;
                        }
                    else
                        {

                        ShowReport_TC_CERT(GetStudentIDs(), "Transfer_Certificate", "CrystalReport_LC_RCPIT.rpt");
                        //ShowReport_TC_CERT(GetStudentIDs(), "Transfer_Certificate", "rpt_TransferCertificate_PRMITR.rpt");
                        }

                    }

                //Added by pooja for PRMITR 
                else if (Session["OrgId"].ToString() == "10")
                    {

                    if (ddlAcademicYear.SelectedIndex == 0)
                        {
                        objCommon.DisplayMessage(this.updpnlExam, "Please Select Academic Year", this);
                        ddlAcademicYear.Focus();
                        return;
                        }
                    else
                        {

                        ShowReport_TC_CERT(GetStudentIDs(), "Transfer_Certificate", "rpt_TransferCertificate_PRMITR.rpt");
                        }

                    }
                else if (Session["OrgId"].ToString() == "11")
                    {

                    if (ddlAcademicYear.SelectedIndex == 0)
                        {
                        objCommon.DisplayMessage(this.updpnlExam, "Please Select Academic Year", this);
                        ddlAcademicYear.Focus();
                        return;
                        }
                    else
                        {

                        ShowReport_TC_CERT(GetStudentIDs(), "Transfer_Certificate", "rpt_TransferCertificate_PRMCEM.rpt");
                        }

                    }


                //ADDED BY POOJA  FOR CPUK and amhirpur reports ON DATE 16-06-2023
                else if (Session["OrgId"].ToString() == "3")
                    {

                    if (ddlAcademicYear.SelectedIndex == 0)
                        {
                        objCommon.DisplayMessage(this.updpnlExam, "Please Select Academic Year", this);
                        ddlAcademicYear.Focus();
                        return;
                        }
                    else
                        {

                        ShowReport_TC_CERT_CPUKH(GetStudentIDs(), "Transfer_Certificate", "Migration_TC_CPUK.rpt");

                        }

                    }

                else if (Session["OrgId"].ToString() == "4")  //amhirpur
                    {

                    if (ddlAcademicYear.SelectedIndex == 0)
                        {
                        objCommon.DisplayMessage(this.updpnlExam, "Please Select Academic Year", this);
                        ddlAcademicYear.Focus();
                        return;
                        }
                    else
                        {

                        ShowReport_TC_CERT_CPUKH(GetStudentIDs(), "Transfer_Certificate", "Migration_TC_CPUH.rpt");

                        }

                    }

                }
            //
            else if (CertShortName == "EBC")
                {
                if (Session["OrgId"].ToString() == "6")
                    {
                    ShowReport_BC(GetStudentIDs(), "ESTIMATE_Certificate", "rptEstimateBonafideCertificate_RCPIPER.rpt");
                    }

                else if (CertShortName == "EBC")
                    {

                    ShowReport_BC(GetStudentIDs(), "ESTIMATE_Certificate", "rptEstimateBonafideCertificate_RCPIT.rpt");

                    }
                }

            else if (CertShortName == "BC")
                {
                if (Session["OrgId"].ToString() == "1")
                    {
                    ShowReport_BC(GetStudentIDs(), "Bonafide_Certificate", "rptBonafideCertificate_RCPTI.rpt");

                    }
                else if (Session["OrgId"].ToString() == "6")
                    {
                    if (ddlAcademicYear.SelectedIndex == 0)
                        {
                        objCommon.DisplayMessage(this.updpnlExam, "Please Select Academic Year", this);
                        ddlAcademicYear.Focus();
                        return;
                        }
                    else
                        {
                        ShowReport_BC1(GetStudentIDs(), "Bonafide_Certificate", "rptBonafideCertificate_RCPIEPER.rpt");
                        }
                    }

                else if (Session["OrgId"].ToString() == "10")
                    {
                    ShowReport_BCPRMITR(GetStudentIDs(), "Bonafide_Certificate", "rptBonafideCertificate_PRMITR.rpt");
                    }
                else if (Session["OrgId"].ToString() == "11")
                    {
                    ShowReport_BCPRMITR(GetStudentIDs(), "Bonafide_Certificate", "rptBonafideCertificate_PRMCEM.rpt");
                    }

                //Added by pooja on date 05-05-2023 for maher bonafide passport certificate
                else if (Session["OrgId"].ToString() == "16")
                    {
                    ShowReport_BCMAHER(GetStudentIDs(), "Bonafide_Certificate", "BC_Passport_Visa_Other_Maher.rpt");
                    }
                else if (Session["OrgId"].ToString() == "19")
                    {
                    ShowReport_BCPRMITR(GetStudentIDs(), "Bonafide_Certificate", "rpt_Bonafied_PCEN.rpt");
                    }
                else if (Session["OrgId"].ToString() == "20")
                    {
                    ShowReport_BCPRMITR(GetStudentIDs(), "Bonafide_Certificate", "rpt_Bonafied_PJLCE.rpt");
                    }
                //
                }

           //Added by pooja on date 05-05-2023 for maher bonafide certificates

            else if (CertShortName == "BC1")
                {
                if (Session["OrgId"].ToString() == "16")
                    {
                    ShowReport_BCMAHER(GetStudentIDs(), "Bonafide_Certificate_1stYear", "BC1stYear_Maher.rpt");
                    }
                }
            else if (CertShortName == "BC2")
                {
                if (Session["OrgId"].ToString() == "16")
                    {
                    ShowReport_BCMAHER(GetStudentIDs(), "Bonafide_Certificate_2ndYear_Onward", "BC2nd_Year_Onward_Maher.rpt");
                    }
                }

        //
            else if (CertShortName == "LC")
                {
                if (Session["OrgId"].ToString() == "1")
                    {
                    if (ddlAcademicYear.SelectedIndex == 0)
                        {
                        objCommon.DisplayMessage(this.updpnlExam, "Please Select Academic Year", this);
                        ddlAcademicYear.Focus();
                        return;
                        }
                    ShowReport_BC(GetStudentIDs(), "Bonafide_Certificate", "CrystalReport_LC_RCPIT.rpt");
                    }
                else if (Session["OrgId"].ToString() == "19")
                    {
                    if (ddlAcademicYear.SelectedIndex == 0)
                        {
                        objCommon.DisplayMessage(this.updpnlExam, "Please Select Academic Year", this);
                        ddlAcademicYear.Focus();
                        return;
                        }
                    ShowReport_BC(GetStudentIDs(), "Bonafide_Certificate", "rptTC_Report_PCEN.rpt");

                    }
                else if (Session["OrgId"].ToString() == "20")
                    {
                    if (ddlAcademicYear.SelectedIndex == 0)
                        {
                        objCommon.DisplayMessage(this.updpnlExam, "Please Select Academic Year", this);
                        ddlAcademicYear.Focus();
                        return;
                        }
                    ShowReport_BC(GetStudentIDs(), "Bonafide_Certificate", "rptTC_Report_PJLCE.rpt");

                    }
                }

            //Added by pooja on date 16-06-2023 PRMITR certificates
            else if (CertShortName == "AC")
                {
                //if (ddlAcademicYear.SelectedIndex == 0)
                //{
                //    objCommon.DisplayMessage(this.updpnlExam, "Please Select Academic Year", this);
                //    ddlAcademicYear.Focus();
                //    return;
                //}
                if (Session["OrgId"].ToString() == "10")
                    {
                    ShowReport_BCPRMITR(GetStudentIDs(), "Appearance_Certificate", "rptAppearanceCert_PRMITR.rpt");
                    }
                else if (Session["OrgId"].ToString() == "11")
                    {
                    ShowReport_BCPRMITR(GetStudentIDs(), "Appearance_Certificate", "rptAppearanceCert_PRMCEM.rpt");

                    }
                }
            else if (CertShortName == "EC")
                {
                if (Session["OrgId"].ToString() == "10")
                    {
                    ShowReport_BCPRMITR(GetStudentIDs(), "Estimate_Certificate", "rptExpenditureCertificate_PRMITR.rpt");
                    }
                else if (Session["OrgId"].ToString() == "11")
                    {
                    ShowReport_BCPRMITR(GetStudentIDs(), "Estimate_Certificate", "rptExpenditureCertificate_PRMCEM.rpt");
                    }
                }

            else if (CertShortName == "CC")
                {
                if (Session["OrgId"].ToString() == "10")
                    {
                    ShowReport_BCPRMITR(GetStudentIDs(), "Character_Certificate", "rptCharacterCert_PRMITR.rpt");
                    }
                else if (Session["OrgId"].ToString() == "11")
                    {
                    ShowReport_BCPRMITR(GetStudentIDs(), "Character_Certificate", "rptCharacterCert_PRMCEM.rpt");
                    }
                else if (Session["OrgId"].ToString() == "3") //cpukota
                    {
                    ShowReport_CC_CPUH(GetStudentIDs(), "Character_Certificate", "CrystalReport_CharacterCertificate_CPUH.rpt");
                    }
                else if (Session["OrgId"].ToString() == "4")   //amhirpur
                    {
                    ShowReport_CC_CPUH(GetStudentIDs(), "Character_Certificate", "CrystalReport_CharacterCertificate_CPUH.rpt");
                    }
                else if (Session["OrgId"].ToString() == "19")
                    {
                    ShowReport_BCPRMITR(GetStudentIDs(), "Character_Certificate", "rptCharacterCertificate_PCEN.rpt");
                    }
                else if (Session["OrgId"].ToString() == "20")
                    {
                    ShowReport_BCPRMITR(GetStudentIDs(), "Character_Certificate", "rptCharacterCertificate_PJLCE.rpt");
                    }
                }
            }
        Enable_True();

        }
    #endregion


    //Added by Deepali on 27/08/2020 For QR Code on Grade card Report
    //This Method Generate QR-CODE & also  save image in ACD_STUD_PHOTO Table & QR-Code Files Folder.

    private void GenerateQRCode()
        {
        CertificateMasterController objcertMasterController = new CertificateMasterController();
        DataSet ds1;
        int sessionNo = 0;
        int branchNo = 0;
        int semesterNo = 0;
        int admbatchNo = 0;
        int degreeNo = 0;
        int collegeNo = 0;
        int certificateno = 0;
        //if (Session["BC_Student"] == null || ((DataTable)Session["BC_Student"] == null))
        //{
        sessionNo = Convert.ToInt32(ddlSession.SelectedValue);
        branchNo = Convert.ToInt32(ddlBranch.SelectedValue);
        semesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
        admbatchNo = Convert.ToInt32(ddlAdmBatch.SelectedValue);
        degreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
        collegeNo = Convert.ToInt32(ddlCollege.SelectedValue);
        certificateno = Convert.ToInt32(ddlCert.SelectedValue);

        foreach (ListViewDataItem item in lvStudentRecords.Items)
            {
            string Serial_Number = string.Empty;
            CheckBox chk = item.FindControl("chkReport") as CheckBox;
            int idno = Convert.ToInt32(chk.ToolTip);
            if (chk.Checked)
                {

                objQrC.GenerateCertficateSerialNumber(idno, degreeNo, branchNo, certificateno, semesterNo);

                DataSet ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_DEGREE D ON S.DEGREENO=D.DEGREENO INNER JOIN ACD_BRANCH B ON S.BRANCHNO=B.BRANCHNO LEFT OUTER JOIN ACD_CERTIFICATE_LOG TN ON (TN.IDNO=S.IDNO)", "DEGREENAME,LONGNAME BRANCH,CERT_SRNO,ROLLNO,*", "", "S.IDNO='" + idno + "'" + "AND CERT_NO=" + certificateno, "REGNO");

                // ds1 = objQrC.GetStudentDataForCertificateForQRCode(admbatchNo, sessionNo, collegeNo, degreeNo, branchNo, semesterNo, idno);
                ds1 = objQrC.GetStudentDataForQRCodeCert(idno);
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["CERT_SRNO"] as string))
                    {
                    Serial_Number = ds.Tables[0].Rows[0]["CERT_SRNO"].ToString();
                    }
                else
                    {
                    Serial_Number = string.Empty;
                    }

                if (ds1.Tables[0].Rows.Count > 0)
                    {
                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                        {
                        string Qrtext = "SerialNo: " + Serial_Number +
                               "; StudName: " + ds.Tables[0].Rows[i]["STUDNAME"].ToString().Trim() +
                               "; RegNo: " + ds.Tables[0].Rows[i]["REGNO"] +
                               "; RollNo: " + ds.Tables[0].Rows[i]["ROLLNO"] +
                               "; Programme: " + ds.Tables[0].Rows[i]["BRANCH"] +
                               "; Semester: " + ds1.Tables[0].Rows[i]["SEMESTERNO"] +
                               "; SGPA: " + ds1.Tables[0].Rows[i]["SGPA"] +
                                "";

                        Session["qr"] = Qrtext.ToString();
                        QRCodeEncoder encoder = new QRCodeEncoder();
                        encoder.QRCodeVersion = 10;
                        Bitmap img = encoder.Encode(Session["qr"].ToString());
                        //img.Save(Server.MapPath("~\\QrCode Files\\" + ds.Tables[0].Rows[0]["REGNO"].ToString().Trim() + ".Jpeg"));
                        img.Save(Server.MapPath("~\\img.Jpeg"));
                        ViewState["File"] = imageToByteArray(Request.PhysicalApplicationPath + "\\img.Jpeg");
                        //img.Save(Server.MapPath("~\\img.Jpeg"));
                        byte[] QR_IMAGE = ViewState["File"] as byte[];
                        long ret = objQrC.AddUpdateQrCode(Convert.ToInt16(ds1.Tables[0].Rows[i]["IDNO"].ToString().Trim()), QR_IMAGE);
                        }
                    }
                else
                    {
                    IsDataPresent = true;
                    objCommon.DisplayUserMessage(updpnlExam, "No data found for this selection!", this.Page);
                    return;
                    }
                }
            }
        }
    //This Method convert image to byte array.
    public byte[] imageToByteArray(string MyString)
        {
        FileStream ff = new FileStream(MyString, FileMode.Open);
        int ImageSize = (int)ff.Length;
        byte[] ImageContent = new byte[ff.Length];
        ff.Read(ImageContent, 0, ImageSize);
        ff.Close();
        ff.Dispose();
        return ImageContent;
        }

    #region SHOW CERTIFICATES
    private void ShowReport_BC(string param, string reportTitle, string rptFileName)
        {
        string studentIds = string.Empty;
        int bonafiedOption = 0;
        int semesterOption = 0;
        foreach (ListViewDataItem item in lvStudentRecords.Items)
            {
            if ((item.FindControl("chkReport") as CheckBox).Checked)
                {
                if (studentIds.Length > 0)
                    studentIds += ",";
                studentIds += (item.FindControl("hidIdNo") as HiddenField).Value.Trim();
                }
            }

        if (ddlCert.SelectedValue == "10")
            {
            bonafiedOption = Convert.ToInt16(rdbConversion.SelectedValue);
            }

        if (chkAddTextOption.Checked == true)
            {
            bonafiedOption = 1;
            }

        foreach (ListViewDataItem item in lvStudentRecords.Items)
            {
            int idno = 0;
            string CertShortName = objCommon.LookUp("ACD_CERTIFICATE_MASTER", "CERT_SHORT_NAME", "CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "");
            CheckBox chk = item.FindControl("chkReport") as CheckBox;

            if ((item.FindControl("chkReport") as CheckBox).Checked)
                {
                idno = Convert.ToInt32(objCommon.LookUp("ACD_CERT_TRAN", "COUNT(IDNO)", "IDNO=" + chk.ToolTip + "AND CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue)));
                if (idno == 0)
                //if (idno != studentIds)btnConfirm_BC
                    {
                    objCommon.DisplayMessage(this.updpnlExam, "Please Confirm Student First!!", this.Page);
                    return;
                    }
                else
                    {
                    string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                    url += "Reports/CommonReport.aspx?";
                    url += "pagetitle=" + reportTitle;
                    url += "&path=~,Reports,Academic," + rptFileName;

                    //if (CertShortName == "MC" || CertShortName == "TC")
                    //{
                    //    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGEID=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "";
                    //}
                    if (CertShortName == "MC")
                        {
                        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + ",@P_ACADEMIC_YEAR=" + Convert.ToInt32(ddlAcademicYear.SelectedValue) + "";
                        }
                    else if (CertShortName == "LC")
                        {
                        if ((Session["OrgId"].ToString() == "1"))
                            {
                            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + ",@P_ACADEMIC_YEAR=" + Convert.ToInt32(ddlAcademicYear.SelectedValue) + "";
                            }
                        else
                            {
                            url += "&param=@P_IDNO=" + param + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + ",@P_ACADEMIC_YEAR=" + Convert.ToInt32(ddlAcademicYear.SelectedValue) + "";

                            }
                        }
                    else
                        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue);
                        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "";

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                    sb.Append(@"window.open('" + url + "','','" + features + "');");
                    ScriptManager.RegisterClientScriptBlock(this.updpnlExam2, this.updpnlExam2.GetType(), "controlJSScript", sb.ToString(), true);
                    //}

                    }

                }
            }


        // string idno = objCommon.LookUp("ACD_CERT_TRAN", "IDNO", "IDNO IN(" + studentIds + ") AND CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue));


        //if (idno == "")
        ////if (idno != studentIds)
        //{
        //    objCommon.DisplayMessage(this.updpnlExam, "Please Confirm Student First!!", this.Page);
        //    return;
        //}
        //
        }

    //Added by pooja on date 05-05-2023 for bonafide passport certificate

    private void ShowReport_BCMAHER(string param, string reportTitle, string rptFileName)
        {
        string studentIds = string.Empty;
        int bonafiedOption = 0;
        int semesterOption = 0;
        foreach (ListViewDataItem item in lvStudentRecords.Items)
            {
            if ((item.FindControl("chkReport") as CheckBox).Checked)
                {
                if (studentIds.Length > 0)
                    studentIds += ",";
                studentIds += (item.FindControl("hidIdNo") as HiddenField).Value.Trim();
                }
            }

        if (ddlCert.SelectedValue == "10")
            {
            bonafiedOption = Convert.ToInt16(rdbConversion.SelectedValue);
            }

        if (chkAddTextOption.Checked == true)
            {
            bonafiedOption = 1;
            }

        foreach (ListViewDataItem item in lvStudentRecords.Items)
            {
            int idno = 0;
            string CertShortName = objCommon.LookUp("ACD_CERTIFICATE_MASTER", "CERT_SHORT_NAME", "CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "");
            CheckBox chk = item.FindControl("chkReport") as CheckBox;

            if ((item.FindControl("chkReport") as CheckBox).Checked)
                {
                idno = Convert.ToInt32(objCommon.LookUp("ACD_CERT_TRAN", "COUNT(IDNO)", "IDNO=" + chk.ToolTip + "AND CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue)));
                if (idno == 0)
                //if (idno != studentIds)btnConfirm_BC
                    {
                    objCommon.DisplayMessage(this.updpnlExam, "Please Confirm Student First!!", this.Page);
                    return;
                    }
                else
                    {
                    string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                    url += "Reports/CommonReport.aspx?";
                    url += "pagetitle=" + reportTitle;
                    url += "&path=~,Reports,Academic," + rptFileName;

                    if (CertShortName == "MC" || CertShortName == "TC")
                        {
                        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGEID=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "";
                        }
                    else if (CertShortName == "LC")
                        {
                        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + ",@P_ACADEMIC_YEAR=" + Convert.ToInt32(ddlAcademicYear.SelectedValue) + "";
                        }
                    else
                        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue);
                        url += "&param=@P_IDNO=" + param + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "";


                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                    sb.Append(@"window.open('" + url + "','','" + features + "');");
                    ScriptManager.RegisterClientScriptBlock(this.updpnlExam2, this.updpnlExam2.GetType(), "controlJSScript", sb.ToString(), true);
                    //}

                    }

                }
            }

        }
    //ADDED BY POOJA FOR CC CERTIFICATE CPUH ON DATE 16-06-2023

    private void ShowReport_CC_CPUH(string param, string reportTitle, string rptFileName)
        {
        string studentIds = string.Empty;
        int bonafiedOption = 0;
        int semesterOption = 0;
        foreach (ListViewDataItem item in lvStudentRecords.Items)
            {
            if ((item.FindControl("chkReport") as CheckBox).Checked)
                {
                if (studentIds.Length > 0)
                    studentIds += ",";
                studentIds += (item.FindControl("hidIdNo") as HiddenField).Value.Trim();
                }
            }

        if (ddlCert.SelectedValue == "10")
            {
            bonafiedOption = Convert.ToInt16(rdbConversion.SelectedValue);
            }

        if (chkAddTextOption.Checked == true)
            {
            bonafiedOption = 1;
            }

        foreach (ListViewDataItem item in lvStudentRecords.Items)
            {
            int idno = 0;
            string CertShortName = objCommon.LookUp("ACD_CERTIFICATE_MASTER", "CERT_SHORT_NAME", "CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "");
            CheckBox chk = item.FindControl("chkReport") as CheckBox;

            if ((item.FindControl("chkReport") as CheckBox).Checked)
                {
                idno = Convert.ToInt32(objCommon.LookUp("ACD_CERT_TRAN", "COUNT(IDNO)", "IDNO=" + chk.ToolTip + "AND CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue)));
                if (idno == 0)
                //if (idno != studentIds)btnConfirm_BC
                    {
                    objCommon.DisplayMessage(this.updpnlExam, "Please Confirm Student First!!", this.Page);
                    return;
                    }
                else
                    {
                    string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                    url += "Reports/CommonReport.aspx?";
                    url += "pagetitle=" + reportTitle;
                    url += "&path=~,Reports,Academic," + rptFileName;

                    if (CertShortName == "MC" || CertShortName == "TC")
                        {
                        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGEID=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "";
                        }
                    else if (CertShortName == "LC")
                        {
                        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + ",@P_ACADEMIC_YEAR=" + Convert.ToInt32(ddlAcademicYear.SelectedValue) + "";
                        }
                    else if (CertShortName == "CC")
                        {
                        url += "&param=@P_IDNO=" + param + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "";
                        }
                    else
                        url += "&param=@P_IDNO=" + param + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "";

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                    sb.Append(@"window.open('" + url + "','','" + features + "');");
                    ScriptManager.RegisterClientScriptBlock(this.updpnlExam2, this.updpnlExam2.GetType(), "controlJSScript", sb.ToString(), true);
                    //}

                    }

                }
            }


        // string idno = objCommon.LookUp("ACD_CERT_TRAN", "IDNO", "IDNO IN(" + studentIds + ") AND CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue));


        //if (idno == "")
        ////if (idno != studentIds)
        //{
        //    objCommon.DisplayMessage(this.updpnlExam, "Please Confirm Student First!!", this.Page);
        //    return;
        //}
        //
        }

    //
    private void ShowReport_BCPRMITR(string param, string reportTitle, string rptFileName)
        {
        string studentIds = string.Empty;
        int bonafiedOption = 0;
        int semesterOption = 0;
        foreach (ListViewDataItem item in lvStudentRecords.Items)
            {
            if ((item.FindControl("chkReport") as CheckBox).Checked)
                {
                if (studentIds.Length > 0)
                    studentIds += ",";
                studentIds += (item.FindControl("hidIdNo") as HiddenField).Value.Trim();
                }
            }

        if (ddlCert.SelectedValue == "10")
            {
            bonafiedOption = Convert.ToInt16(rdbConversion.SelectedValue);
            }

        if (chkAddTextOption.Checked == true)
            {
            bonafiedOption = 1;
            }

        foreach (ListViewDataItem item in lvStudentRecords.Items)
            {
            int idno = 0;
            string CertShortName = objCommon.LookUp("ACD_CERTIFICATE_MASTER", "CERT_SHORT_NAME", "CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "");
            CheckBox chk = item.FindControl("chkReport") as CheckBox;

            if ((item.FindControl("chkReport") as CheckBox).Checked)
                {
                idno = Convert.ToInt32(objCommon.LookUp("ACD_CERT_TRAN", "COUNT(IDNO)", "IDNO=" + chk.ToolTip + "AND CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue)));
                if (idno == 0)
                //if (idno != studentIds)btnConfirm_BC
                    {
                    objCommon.DisplayMessage(this.updpnlExam, "Please Confirm Student First!!", this.Page);
                    return;
                    }
                else
                    {
                    string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                    url += "Reports/CommonReport.aspx?";
                    url += "pagetitle=" + reportTitle;
                    url += "&path=~,Reports,Academic," + rptFileName;

                    if (CertShortName == "MC" || CertShortName == "TC")
                        {
                        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGEID=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "";
                        }
                    else if (CertShortName == "LC")
                        {
                        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + ",@P_ACADEMIC_YEAR=" + Convert.ToInt32(ddlAcademicYear.SelectedValue) + "";
                        }
                    else
                        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue);
                        url += "&param=@P_IDNO=" + param + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "";

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                    sb.Append(@"window.open('" + url + "','','" + features + "');");
                    ScriptManager.RegisterClientScriptBlock(this.updpnlExam2, this.updpnlExam2.GetType(), "controlJSScript", sb.ToString(), true);
                    //}

                    }

                }
            }


        // string idno = objCommon.LookUp("ACD_CERT_TRAN", "IDNO", "IDNO IN(" + studentIds + ") AND CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue));
        //if (idno == "")
        ////if (idno != studentIds)
        //{
        //    objCommon.DisplayMessage(this.updpnlExam, "Please Confirm Student First!!", this.Page);
        //    return;
        //}
        //
        }
    private void ShowReport_BC1(string param, string reportTitle, string rptFileName)
        {
        string studentIds = string.Empty;
        int bonafiedOption = 0;
        int semesterOption = 0;
        foreach (ListViewDataItem item in lvStudentRecords.Items)
            {
            if ((item.FindControl("chkReport") as CheckBox).Checked)
                {
                if (studentIds.Length > 0)
                    studentIds += ",";
                studentIds += (item.FindControl("hidIdNo") as HiddenField).Value.Trim();
                }
            }

        if (ddlCert.SelectedValue == "10")
            {
            bonafiedOption = Convert.ToInt16(rdbConversion.SelectedValue);
            }

        if (chkAddTextOption.Checked == true)
            {
            bonafiedOption = 1;
            }

        foreach (ListViewDataItem item in lvStudentRecords.Items)
            {
            int idno = 0;
            string CertShortName = objCommon.LookUp("ACD_CERTIFICATE_MASTER", "CERT_SHORT_NAME", "CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "");
            CheckBox chk = item.FindControl("chkReport") as CheckBox;

            if ((item.FindControl("chkReport") as CheckBox).Checked)
                {
                idno = Convert.ToInt32(objCommon.LookUp("ACD_CERT_TRAN", "COUNT(IDNO)", "IDNO=" + chk.ToolTip + "AND CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue)));
                if (idno == 0)
                //if (idno != studentIds)btnConfirm_BC
                    {
                    objCommon.DisplayMessage(this.updpnlExam, "Please Confirm Student First!!", this.Page);
                    return;
                    }
                else
                    {
                    string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                    url += "Reports/CommonReport.aspx?";
                    url += "pagetitle=" + reportTitle;
                    url += "&path=~,Reports,Academic," + rptFileName;

                    if (CertShortName == "MC" || CertShortName == "TC")
                        {
                        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGEID=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "";
                        }
                    else if (CertShortName == "LC" || CertShortName == "BC")
                        {
                        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + ",@P_ACADEMIC_YEAR=" + Convert.ToInt32(ddlAcademicYear.SelectedValue) + "";
                        }
                    else
                        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue);
                        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "";

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                    sb.Append(@"window.open('" + url + "','','" + features + "');");
                    ScriptManager.RegisterClientScriptBlock(this.updpnlExam2, this.updpnlExam2.GetType(), "controlJSScript", sb.ToString(), true);
                    //}

                    }

                }
            }


        // string idno = objCommon.LookUp("ACD_CERT_TRAN", "IDNO", "IDNO IN(" + studentIds + ") AND CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue));


        //if (idno == "")
        ////if (idno != studentIds)
        //{
        //    objCommon.DisplayMessage(this.updpnlExam, "Please Confirm Student First!!", this.Page);
        //    return;
        //}
        //
        }
    private void ShowReport_TC_CERT(string param, string reportTitle, string rptFileName)
        {
        string studentIds = string.Empty;
        int bonafiedOption = 0;
        int semesterOption = 0;
        foreach (ListViewDataItem item in lvStudentRecords.Items)
            {
            if ((item.FindControl("chkReport") as CheckBox).Checked)
                {
                if (studentIds.Length > 0)
                    studentIds += ",";
                studentIds += (item.FindControl("hidIdNo") as HiddenField).Value.Trim();
                }
            }
        foreach (ListViewDataItem item in lvStudentRecords.Items)
            {
            int idno = 0;
            string CertShortName = objCommon.LookUp("ACD_CERTIFICATE_MASTER", "CERT_SHORT_NAME", "CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "");
            CheckBox chk = item.FindControl("chkReport") as CheckBox;

            if ((item.FindControl("chkReport") as CheckBox).Checked)
                {
                idno = Convert.ToInt32(objCommon.LookUp("ACD_CERT_TRAN", "COUNT(IDNO)", "IDNO=" + chk.ToolTip + "AND CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue)));
                if (idno == 0)
                //if (idno != studentIds)btnConfirm_BC
                    {
                    objCommon.DisplayMessage(this.updpnlExam, "Please Confirm Student First!!", this.Page);
                    return;
                    }
                else
                    {
                    string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                    url += "Reports/CommonReport.aspx?";
                    url += "pagetitle=" + reportTitle;
                    url += "&path=~,Reports,Academic," + rptFileName;
                    if (Session["OrgId"].ToString() == "2")
                        {
                        url += "&param=@P_IDNO=" + param + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_CERT_NO=" + 2;
                        }
                    else if (Session["OrgId"].ToString() == "6")
                        {
                        //url += "&param=@P_IDNO=" + param + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + " @P_ACADEMIC_YEAR=" + Convert.ToInt32(ddlAcademicYear.SelectedValue) + ""; 
                        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + ",@P_ACADEMIC_YEAR=" + Convert.ToInt32(ddlAcademicYear.SelectedValue) + "";

                        //url += "&param=@P_IDNO=" + param + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + ""; ;
                        }

                    else
                        {
                        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + ",@P_ACADEMIC_YEAR=" + Convert.ToInt32(ddlAcademicYear.SelectedValue) + "";
                        }
                    //+",@P_LEAVE_DATE=" + Convert.ToDateTime(txtleaving.Text); ;
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                    sb.Append(@"window.open('" + url + "','','" + features + "');");
                    ScriptManager.RegisterClientScriptBlock(this.updpnlExam2, this.updpnlExam2.GetType(), "controlJSScript", sb.ToString(), true);
                    //}
                    }

                }
            }


        // string idno = objCommon.LookUp("ACD_CERT_TRAN", "IDNO", "IDNO IN(" + studentIds + ") AND CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue));


        //if (idno == "")
        ////if (idno != studentIds)
        //{
        //    objCommon.DisplayMessage(this.updpnlExam, "Please Confirm Student First!!", this.Page);
        //    return;
        //}
        //
        }

    private void ShowReport_TC_CERT_CPUKH(string param, string reportTitle, string rptFileName)
        {
        string studentIds = string.Empty;
        int bonafiedOption = 0;
        int semesterOption = 0;
        foreach (ListViewDataItem item in lvStudentRecords.Items)
            {
            if ((item.FindControl("chkReport") as CheckBox).Checked)
                {
                if (studentIds.Length > 0)
                    studentIds += ",";
                studentIds += (item.FindControl("hidIdNo") as HiddenField).Value.Trim();
                }
            }
        foreach (ListViewDataItem item in lvStudentRecords.Items)
            {
            int idno = 0;
            string CertShortName = objCommon.LookUp("ACD_CERTIFICATE_MASTER", "CERT_SHORT_NAME", "CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "");
            CheckBox chk = item.FindControl("chkReport") as CheckBox;

            if ((item.FindControl("chkReport") as CheckBox).Checked)
                {
                idno = Convert.ToInt32(objCommon.LookUp("ACD_CERT_TRAN", "COUNT(IDNO)", "IDNO=" + chk.ToolTip + "AND CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue)));
                if (idno == 0)
                //if (idno != studentIds)btnConfirm_BC
                    {
                    objCommon.DisplayMessage(this.updpnlExam, "Please Confirm Student First!!", this.Page);
                    return;
                    }
                else
                    {
                    string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                    url += "Reports/CommonReport.aspx?";
                    url += "pagetitle=" + reportTitle;
                    url += "&path=~,Reports,Academic," + rptFileName;
                    if (Session["OrgId"].ToString() == "2")
                        {
                        url += "&param=@P_IDNO=" + param + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_CERT_NO=" + 2;
                        }
                    else if (Session["OrgId"].ToString() == "6")
                        {
                        //url += "&param=@P_IDNO=" + param + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + " @P_ACADEMIC_YEAR=" + Convert.ToInt32(ddlAcademicYear.SelectedValue) + ""; 
                        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + ",@P_ACADEMIC_YEAR=" + Convert.ToInt32(ddlAcademicYear.SelectedValue) + "";
                        //url += "&param=@P_IDNO=" + param + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + ""; ;
                        }
                    else
                        {
                        url += "&param=@P_IDNO=" + param + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + ",@P_ACADEMIC_YEAR=" + Convert.ToInt32(ddlAcademicYear.SelectedValue) + "";
                        }
                    //+",@P_LEAVE_DATE=" + Convert.ToDateTime(txtleaving.Text); ;
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                    sb.Append(@"window.open('" + url + "','','" + features + "');");
                    ScriptManager.RegisterClientScriptBlock(this.updpnlExam2, this.updpnlExam2.GetType(), "controlJSScript", sb.ToString(), true);
                    //}
                    }

                }
            }


        // string idno = objCommon.LookUp("ACD_CERT_TRAN", "IDNO", "IDNO IN(" + studentIds + ") AND CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue));


        //if (idno == "")
        ////if (idno != studentIds)
        //{
        //    objCommon.DisplayMessage(this.updpnlExam, "Please Confirm Student First!!", this.Page);
        //    return;
        //}
        //
        }

    private void ShowReport_Conv(string param, string reportTitle, string rptFileName)
        {
        string studentIds = string.Empty;
        //int bonafiedOption = 0;
        foreach (ListViewDataItem item in lvStudentRecords.Items)
            {
            if ((item.FindControl("chkReport") as CheckBox).Checked)
                {
                if (studentIds.Length > 0)
                    studentIds += ",";
                studentIds += (item.FindControl("hidIdNo") as HiddenField).Value.Trim();
                }
            }

        //if (chkAddTextOption.Checked == true)
        //{
        //    bonafiedOption = 1;
        //}
        string idno = objCommon.LookUp("ACD_CERT_TRAN", "IDNO", "IDNO IN(" + studentIds + ")");
        if (idno == "")
            {
            objCommon.DisplayMessage(this.updpnlExam, "Please Confirm Student First!!", this.Page);
            return;
            }
        else
            {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_DEGREE=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCH=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_OPTION=" + Convert.ToInt16(rdbConversion.SelectedValue) + ",@P_SEMESTERNO_OPTION=" + Convert.ToInt16(semesterBonafied.SelectedValue) + "";
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            }
        }

    private void ShowReport_TC(string param, string reportTitle, string rptFileName)
        {

        string studentIds = string.Empty;

        if (txtSearch_Enrollno_LC.Text != string.Empty)
            {
            string idNo = objCommon.LookUp("ACD_STUDENT", "IDNO", "CAN = 0 AND ADMCAN=0 AND REGNO='" + txtSearch_Enrollno_LC.Text + "'");
            ViewState["idno"] = idNo;


            int cntIdno = Convert.ToInt32(objCommon.LookUp("ACD_CERT_TRAN", "COUNT(IDNO)", "IDNO=" + idNo + "AND CERT_NO = 4"));
            if (cntIdno <= 0)
                {
                foreach (ListViewDataItem item in lvStudentRecords_LC.Items)
                    {
                    if ((item.FindControl("chkReport") as CheckBox).Checked)
                        {
                        if (studentIds.Length > 0)
                            studentIds += ",";
                        studentIds += (item.FindControl("hidIdNo") as HiddenField).Value.Trim();
                        }
                    }
                }
            else
                {
                foreach (ListViewDataItem item in lvIssueCert.Items)
                    {
                    if ((item.FindControl("chkReport") as CheckBox).Checked)
                        {
                        if (studentIds.Length > 0)
                            studentIds += ",";
                        studentIds += (item.FindControl("hidIdNo") as HiddenField).Value.Trim();
                        }
                    }
                }
            }
        else
            {
            foreach (ListViewDataItem item in lvStudentRecords_LC.Items)
                {
                if ((item.FindControl("chkReport") as CheckBox).Checked)
                    {
                    if (studentIds.Length > 0)
                        studentIds += ",";
                    studentIds += (item.FindControl("hidIdNo") as HiddenField).Value.Trim();
                    }
                }
            }
        string idno = objCommon.LookUp("ACD_CERT_TRAN", "IDNO", "IDNO IN(" + studentIds + ")");
        if (idno == "")
            {
            objCommon.DisplayMessage(this.updpnlExam, "Please Confirm Student First!!", this.Page);
            return;
            }
        else
            {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_CERT_NO=" + 4 + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession1.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester1.SelectedValue) + ",@P_DEGREE=" + Convert.ToInt32(ddlDegree1.SelectedValue) + ",@P_BRANCH=" + Convert.ToInt32(ddlBranch1.SelectedValue);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            }

        }

    private void ShowReport_PVC(string param, string reportTitle, string rptFileName)
        {
        string studentIds = string.Empty;
        foreach (ListViewDataItem item in lvStudentRecords.Items)
            {
            if ((item.FindControl("chkReport") as CheckBox).Checked)
                {
                if (studentIds.Length > 0)
                    studentIds += ",";
                studentIds += (item.FindControl("hidIdNo") as HiddenField).Value.Trim();
                }
            }
        string idno = objCommon.LookUp("ACD_CERT_TRAN", "IDNO", "IDNO IN(" + studentIds + ")");
        if (idno == "")
            {
            objCommon.DisplayMessage(this.updpnlExam, "Please Confirm Student First!!", this.Page);
            return;
            }
        else
            {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + "";
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            }
        }

    private void ShowReportData(string param, string reportTitle, string rptFileName)
        {
        string studentIds = string.Empty;
        foreach (ListViewDataItem item in lvStudentRecords.Items)
            {
            if ((item.FindControl("chkReport") as CheckBox).Checked)
                {
                if (studentIds.Length > 0)
                    studentIds += ",";
                studentIds += (item.FindControl("hidIdNo") as HiddenField).Value.Trim();
                }
            }
        string idno = objCommon.LookUp("ACD_CERT_TRAN", "IDNO", "IDNO IN(" + studentIds + ")");
        if (idno == "")
            {
            objCommon.DisplayMessage(this.updpnlExam, "Please Confirm Student First!!", this.Page);
            return;
            }
        else
            {

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            // url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + 0 + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_ORDER_BY=1,@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGEID=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "";
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);

            }
        }

    //added by pooja on date 22-06-2023 for view tc certificate cpuh and cpuk
    private void ShowReport_TC_View_CPUKH(string param, string reportTitle, string rptFileName)
        {
        string studentIds = string.Empty;
        int bonafiedOption = 0;
        int semesterOption = 0;
        foreach (ListViewDataItem item in lvStudentRecords.Items)
            {
            if ((item.FindControl("chkReport") as CheckBox).Checked)
                {
                if (studentIds.Length > 0)
                    studentIds += ",";
                studentIds += (item.FindControl("hidIdNo") as HiddenField).Value.Trim();
                }
            }

        if (ddlCert.SelectedValue == "10")
            {
            bonafiedOption = Convert.ToInt16(rdbConversion.SelectedValue);
            }

        if (chkAddTextOption.Checked == true)
            {
            bonafiedOption = 1;
            }

        foreach (ListViewDataItem item in lvStudentRecords.Items)
            {
            int idno = 0;
            string CertShortName = objCommon.LookUp("ACD_CERTIFICATE_MASTER", "CERT_SHORT_NAME", "CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "");
            CheckBox chk = item.FindControl("chkReport") as CheckBox;
            TextBox txtRemark = item.FindControl("txtRemark") as TextBox;

            if ((item.FindControl("chkReport") as CheckBox).Checked)
                {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;

                if (CertShortName == "MC")
                    {
                    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGEID=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "";
                    }
                else if (CertShortName == "TC")
                    {
                    url += "&param=@P_IDNO=" + param + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + ",@P_ACADEMIC_YEAR=" + Convert.ToInt32(ddlAcademicYear.SelectedValue) + ",@P_LEAVE_DATE=" + txtleaving.Text + ",@P_REASON=" + ddlreason.SelectedItem.Text + ",@P_REMARK=" + txtRemark.Text;
                    }

                else
                    //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue);
                    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");
                ScriptManager.RegisterClientScriptBlock(this.updpnlExam2, this.updpnlExam2.GetType(), "controlJSScript", sb.ToString(), true);
                //}
                }
            }


        // string idno = objCommon.LookUp("ACD_CERT_TRAN", "IDNO", "IDNO IN(" + studentIds + ") AND CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue));


        //if (idno == "")
        ////if (idno != studentIds)
        //{
        //    objCommon.DisplayMessage(this.updpnlExam, "Please Confirm Student First!!", this.Page);
        //    return;
        //}
        //
        }




    private void ShowReport_BC_View(string param, string reportTitle, string rptFileName)
        {
        string studentIds = string.Empty;
        int bonafiedOption = 0;
        int semesterOption = 0;
        foreach (ListViewDataItem item in lvStudentRecords.Items)
            {
            if ((item.FindControl("chkReport") as CheckBox).Checked)
                {
                if (studentIds.Length > 0)
                    studentIds += ",";
                studentIds += (item.FindControl("hidIdNo") as HiddenField).Value.Trim();
                }
            }

        if (ddlCert.SelectedValue == "10")
            {
            bonafiedOption = Convert.ToInt16(rdbConversion.SelectedValue);
            }

        if (chkAddTextOption.Checked == true)
            {
            bonafiedOption = 1;
            }

        foreach (ListViewDataItem item in lvStudentRecords.Items)
            {
            int idno = 0;
            string CertShortName = objCommon.LookUp("ACD_CERTIFICATE_MASTER", "CERT_SHORT_NAME", "CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "");
            CheckBox chk = item.FindControl("chkReport") as CheckBox;
            TextBox txtRemark = item.FindControl("txtRemark") as TextBox;

            if ((item.FindControl("chkReport") as CheckBox).Checked)
                {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;

                if (CertShortName == "MC" || CertShortName == "TC")
                    {
                    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGEID=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "";
                    }
                else if (CertShortName == "LC")
                    {
                    if ((Session["OrgId"].ToString() == "1"))
                        {
                        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + ",@P_ACADEMIC_YEAR=" + Convert.ToInt32(ddlAcademicYear.SelectedValue) + ",@P_LEAVE_DATE=" + txtleaving.Text + ",@P_REASON=" + ddlreason.SelectedItem.Text + ",@P_REMARK=" + txtRemark.Text;
                        }
                    else
                        {
                        url += "&param=@P_IDNO=" + param + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + ",@P_ACADEMIC_YEAR=" + Convert.ToInt32(ddlAcademicYear.SelectedValue) + ",@P_LEAVE_DATE=" + txtleaving.Text + ",@P_REASON=" + ddlreason.SelectedItem.Text + ",@P_REMARK=" + txtRemark.Text;

                        }
                    }

                else
                    //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue);
                    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_ADMBATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");
                ScriptManager.RegisterClientScriptBlock(this.updpnlExam2, this.updpnlExam2.GetType(), "controlJSScript", sb.ToString(), true);
                //}
                }
            }


        // string idno = objCommon.LookUp("ACD_CERT_TRAN", "IDNO", "IDNO IN(" + studentIds + ") AND CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue));


        //if (idno == "")
        ////if (idno != studentIds)
        //{
        //    objCommon.DisplayMessage(this.updpnlExam, "Please Confirm Student First!!", this.Page);
        //    return;
        //}
        //
        }
    #endregion

    #region LC
    protected void btnSearch_LC_Click(object sender, EventArgs e)
        {
        try
            {
            DataSet dsBC;
            DataSet dsissueCert;

            CertificateMasterController objcerMasterController = new CertificateMasterController();

            string idNo = objCommon.LookUp("ACD_STUDENT", "IDNO", "CAN = 0 AND ADMCAN=0 AND REGNO='" + txtSearch_Enrollno_LC.Text + "'");
            ViewState["idno"] = idNo;
            int cntIdno = Convert.ToInt32(objCommon.LookUp("ACD_CERT_TRAN", "COUNT(IDNO)", "IDNO=" + idNo + "AND CERT_NO = 4"));
            // aleady certificate issue
            if (cntIdno > 0)
                {
                dsissueCert = objcerMasterController.GetStudentListForIssueCert(Convert.ToInt32(ViewState["idno"]));
                if (dsissueCert != null && dsissueCert.Tables.Count > 0)
                    {
                    //set null
                    lvStudentRecords_LC.DataSource = null;
                    lvStudentRecords_LC.DataBind();
                    lvStudentRecords_LC.Visible = false;


                    lvIssueCert.DataSource = dsissueCert.Tables[0];
                    lvIssueCert.DataBind();

                    btnConfirm_LC.Visible = false;
                    }
                else
                    {
                    lvIssueCert.DataSource = null;
                    lvIssueCert.DataBind();
                    }
                }
            else
                {
                // first time certificate issue
                if (idNo != "" && idNo != null)
                    {
                    int chkidNo = Convert.ToInt32(idNo);
                    dsBC = objcerMasterController.GetStudentListForBC_BYIDNO(chkidNo);
                    if (dsBC.Tables[0].Rows.Count > 0)
                        {

                        lvIssueCert.DataSource = null;
                        lvIssueCert.DataBind();

                        lvStudentRecords_LC.DataSource = dsBC.Tables[0];
                        lvStudentRecords_LC.DataBind();
                        }
                    else
                        {
                        objCommon.DisplayMessage(this.updpnlExam, "No student data found", this);
                        lvStudentRecords_LC.DataSource = null;
                        lvStudentRecords_LC.DataBind();
                        }
                    }
                else
                    {
                    objCommon.DisplayMessage(this.updpnlExam, "No student found having enrollment no.: " + txtSearch_Enrollno_BC.Text.Trim(), this);
                    }
                }
            btnConfirm_LC.Enabled = true;
            }
        catch (Exception ex)
            {
            throw;
            }
        }

    protected void btnCan_Click(object sender, ImageClickEventArgs e)
        {
        try
            {

            ImageButton btnEdit = sender as ImageButton;
            int CERT_TR_NO = int.Parse(btnEdit.CommandArgument);
            CertificateMasterController objCMC = new CertificateMasterController();

            CustomStatus cs = (CustomStatus)objCMC.CanelCertificate(CERT_TR_NO);
            if (cs.Equals(CustomStatus.RecordUpdated))
                objCommon.DisplayMessage(this.updpnlExam, "Selected Student Certificate Cancel Successfully", this);
            //int cntIdno = Convert.ToInt32(objCommon.LookUp("ACD_CERT_TRAN", "COUNT(IDNO)", "IDNO=" + idNo + "AND CERT_NO = 4"));
            // aleady certificate issue

            DataSet dsBC;
            //DataSet dsissueCert;

            CertificateMasterController objcerMasterController = new CertificateMasterController();

            string idNo = objCommon.LookUp("ACD_STUDENT", "IDNO", "CAN = 0 AND ADMCAN=0 AND REGNO='" + txtSearch_Enrollno_LC.Text + "'");
            ViewState["idno"] = idNo;


            int cntIdno1 = Convert.ToInt32(objCommon.LookUp("ACD_CERT_TRAN", "COUNT(IDNO)", "IDNO=" + idNo + "AND CERT_NO = 4"));

            if (cntIdno1 <= 0)
                {
                if (idNo != "" && idNo != null)
                    {
                    int chkidNo = Convert.ToInt32(idNo);
                    dsBC = objcerMasterController.GetStudentListForBC_BYIDNO(chkidNo);
                    if (dsBC.Tables[0].Rows.Count > 0)
                        {

                        lvIssueCert.DataSource = null;
                        lvIssueCert.DataBind();

                        lvStudentRecords_LC.DataSource = dsBC.Tables[0];
                        lvStudentRecords_LC.DataBind();
                        }
                    else
                        {
                        objCommon.DisplayMessage(this.updpnlExam, "No student data found", this);
                        lvStudentRecords_LC.DataSource = null;
                        lvStudentRecords_LC.DataBind();
                        }
                    }
                else
                    {
                    objCommon.DisplayMessage(this.updpnlExam, "No student found having enrollment no.: " + txtSearch_Enrollno_BC.Text.Trim(), this);
                    }
                }
            }
        catch (Exception ex)
            {
            throw;
            }
        }

    private string GetStudentID()
        {
        string idno = string.Empty;
        try
            {
            foreach (ListViewDataItem item in lvStudentRecords_LC.Items)
                {
                if ((item.FindControl("chkReport") as CheckBox).Checked)
                    {
                    idno = (item.FindControl("hidIdNo") as HiddenField).Value.Trim();
                    }
                }
            }
        catch (Exception ex)
            {
            throw;
            }
        return idno;
        }

    protected void btnConfirm_LC_Click(object sender, EventArgs e)
        {
        decimal TuitionFee = 0.0m;
        decimal ExamFee = 0.0m;
        decimal OtherFee = 0.0m;
        decimal HostelFee = 0.0m;
        int tcpartfull = 0;
        int orgid = 0;
        int Status = 0;
        string Branchcode = string.Empty;
        CertificateMasterController objcertMasterController = new CertificateMasterController();
        CertificateMaster objcertMaster = new CertificateMaster();

        try
            {
            foreach (ListViewDataItem dataitem in lvStudentRecords_LC.Items)
                {
                //Get Student Details from lvStudent
                CheckBox cbRow = dataitem.FindControl("chkReport") as CheckBox;
                if (cbRow.Checked == true)
                    {
                    HiddenField hfRow = (dataitem.FindControl("hidIdNo")) as HiddenField;
                    int count = Convert.ToInt32(objCommon.LookUp("ACD_CERT_TRAN", "COUNT(*)", "CERT_NO=4  AND IDNO=" + Convert.ToInt32(hfRow.Value)));
                    if (count > 0)
                        {
                        //objCommon.DisplayMessage("Already issued the Transfer Certificates For This Student", this);

                        int issueStatus = Convert.ToInt32(objCommon.LookUp("ACD_CERT_TRAN", "COUNT(ISSUE_STATUS)", "ISSUE_STATUS = 1 AND CERT_NO = 4 AND IDNO =" + Convert.ToInt32(hfRow.Value)));
                        //{
                        if (issueStatus == 1)
                            {
                            CertificateMasterController objcerMasterController = new CertificateMasterController();
                            objcerMasterController.UpdateIssueStatusCertificate(Convert.ToInt32(hfRow.Value));

                            }
                        else if (issueStatus == 2)
                            {
                            CertificateMasterController objcerMasterController = new CertificateMasterController();
                            objcerMasterController.UpdateIssueStatusCertificate(Convert.ToInt32(hfRow.Value));

                            }
                        //}

                        }
                    else
                        {
                        objcertMaster.IdNo = Convert.ToInt32(hfRow.Value);
                        objcertMaster.CertNo = 4;
                        objcertMaster.Attendance = ddlAttendance.SelectedItem.Text.Trim();
                        objcertMaster.Conduct = ddlConduct.SelectedItem.Text.Trim();
                        objcertMaster.LeavingDate = Convert.ToDateTime(txtEndDate.Text.ToString());

                        objcertMaster.IpAddress = ViewState["ipAddress"].ToString();
                        objcertMaster.UaNO = Convert.ToInt32(Session["userno"]);
                        objcertMaster.CollegeCode = Convert.ToString(Session["colcode"]);
                        int sessionNo = Convert.ToInt32(objCommon.LookUp("ACD_TRRESULT", "MAX(SESSIONNO)", "IDNO=" + Convert.ToInt32(hfRow.Value)));
                        objcertMaster.SessionNo = sessionNo;
                        int semesterNo = Convert.ToInt32(objCommon.LookUp("ACD_TRRESULT", "MAX(SEMESTERNO)", "IDNO=" + Convert.ToInt32(hfRow.Value)));
                        objcertMaster.SemesterNo = semesterNo;

                        objcertMaster.Reason = txtReason.Text.Trim();
                        objcertMaster.Remark = txtRemark.Text.Trim();

                        //ADDED POOJA
                        //objcertMaster.OrganizationId = Convert.ToInt32(Session["OrgId"]);
                        int Orgid = Convert.ToInt32(Session["OrgId"]);
                        objcertMaster.IssueDate = Convert.ToDateTime(txtissuedate.Text.ToString());
                        //



                        if (txtTuitionFee.Text.Trim() != "")
                            TuitionFee = Convert.ToDecimal(txtTuitionFee.Text.Trim());
                        else
                            TuitionFee = 0.0m;
                        if (txtExamFee.Text.Trim() != "")
                            ExamFee = Convert.ToDecimal(txtExamFee.Text.Trim());
                        else
                            ExamFee = 0.0m;
                        if (txtOtherFee.Text.Trim() != "")
                            OtherFee = Convert.ToDecimal(txtOtherFee.Text.Trim());
                        else
                            OtherFee = 0.0m;
                        if (txtHostelFee.Text.Trim() != "")
                            HostelFee = Convert.ToDecimal(txtHostelFee.Text.Trim());
                        else
                            HostelFee = 0.0m;

                        objcertMaster.IssueStatus = 1;
                        if (rdotcpartfull.SelectedValue != "")
                            tcpartfull = Convert.ToInt32(rdotcpartfull.SelectedValue);
                        else
                            tcpartfull = 0;
                        //insert Leaving certificate
                        CustomStatus cs = (CustomStatus)objcertMasterController.AddBonafideCertificate(objcertMaster, TuitionFee, ExamFee, OtherFee, HostelFee, tcpartfull, orgid, Status, Branchcode);
                        if (cs.Equals(CustomStatus.RecordSaved))
                            {
                            objCommon.DisplayMessage(this.updpnlExam, "Process Done Successfully !!!", this);
                            btnConfirm_LC.Enabled = false;
                            btnPrint.Enabled = true;
                            btnSearch_LC.Enabled = false;
                            txtSearch_Enrollno_LC.Enabled = false;
                            txtEndDate.Enabled = false;
                            ddlConduct.Enabled = false;
                            ddlAttendance.Enabled = false;
                            btnConfirm_LC.Enabled = false;
                            lvIssueCert.DataSource = null;
                            lvIssueCert.DataBind();
                            }
                        else
                            {
                            btnReport.Enabled = false;
                            objCommon.DisplayMessage(this.updpnlExam, "Error !!!", this);
                            }
                        }
                    }
                else
                    {
                    objCommon.DisplayMessage(this.updpnlExam, "Please Select Student to conform issuing Leaving Certificate", this);
                    return;
                    }
                }
            }
        catch (Exception ex)
            {
            throw;
            }
        }

    protected void btnPrint_Click(object sender, EventArgs e)
        {
        ShowReport_TC(GetStudentIDsTC(), "Migration_Certificate", "Migration_certificate.rpt");

        }

    private void ShowReport(string idno, string reportTitle, string rptFileName)
        {
        try
            {
            int sessionNo = Convert.ToInt32(objCommon.LookUp("ACD_TRRESULT", "MAX(SESSIONNO)", "IDNO=" + Convert.ToInt32(ViewState["idno"])));
            int semesterNo = Convert.ToInt32(objCommon.LookUp("ACD_TRRESULT", "MAX(SEMESTERNO)", "IDNO=" + Convert.ToInt32(ViewState["idno"])));
            int status = Convert.ToInt32(objCommon.LookUp("ACD_CERT_TRAN CT INNER JOIN ACD_CERTIFICATE_MASTER CM ON (CT.CERT_NO = CM.CERT_NO)", "MAX(ISSUE_STATUS)", "CT.IDNO = " + idno + "AND CM.CERT_NO=1"));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(idno) + ",@CERT_NO=1,@END=" + txtEndDate.Text + ",@P_SESSIONNO= " + Convert.ToInt32(sessionNo) + ",conduct= " + ddlConduct.SelectedItem.Text + ",@P_ISSUE_STATUS=" + status + ",@P_SEMESTERNO=" + Convert.ToInt32(semesterNo);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            }
        catch (Exception ex)
            {
            throw;
            }
        }

    protected void btnCancel_LC_Click(object sender, EventArgs e)
        {
        Response.Redirect(Request.Url.ToString());
        }
    #endregion

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
        {
        //tcLeaving.ActiveTabIndex = 1;
        //Fill Dropdown BRANCH
        ddlSemester.SelectedIndex = -1;
        ddlBranch.SelectedIndex = -1;
        ddlCert.SelectedIndex = -1;
        txtConvocation.Text = string.Empty;
        txtConvocation.Visible = false;
        lblConvocation.Visible = false;
        lblClass.Visible = false;
        txtClass.Visible = false;
        txtClass.Text = string.Empty;
        if (ddlDegree.SelectedIndex > 0)
            {
            if (ddlDegree.SelectedValue == "3")
                {
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO<5", "SEMESTERNAME");
                }
            else
                {
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0 AND ACTIVESTATUS=1", "SEMESTERNAME");
                }
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", "B.ACTIVESTATUS=1 AND CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "LONGNAME");
            ddlBranch.Focus();
            }
        else
            {
            objCommon.DisplayMessage("Please Select Degree!", this.Page);
            ddlDegree.Focus();
            }

        }

    protected void ddlDegree1_SelectedIndexChanged(object sender, EventArgs e)
        {
        //tcLeaving.ActiveTabIndex = 0;
        //Fill Dropdown BRANCH
        //objCommon.FillDropDownList(ddlBranch1, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO>0 AND DEGREENO=" + Convert.ToInt32(ddlDegree1.SelectedValue), "LONGNAME");

        if (ddlDegree1.SelectedIndex > 0)
            {
            objCommon.FillDropDownList(ddlBranch1, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH BR ON (CD.BRANCHNO=BR.BRANCHNO)", "CD.BRANCHNO", "LONGNAME", "DEGREENO =" + Convert.ToInt32(ddlDegree1.SelectedValue), "BRANCHNO");
            }
        else
            {
            objCommon.DisplayMessage("Please Select Degree!", this.Page);
            ddlDegree1.Focus();
            }
        }


    protected void ddlCert_SelectedIndexChanged(object sender, EventArgs e)
        {
        lvStudentRecords.Visible = false;
        string CertShortName = objCommon.LookUp("ACD_CERTIFICATE_MASTER", "CERT_SHORT_NAME", "CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "");
        string semesterNo = objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "(DURATION*2)", "DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + "");

        ddlSemester.SelectedIndex = -1;
        if (semesterNo == "")
            {
            semesterNo = "0";
            } //ADDED BY VINAY MISHRA ON 28082023 FOR TICKET 47439
        if (ddlCert.SelectedIndex > 0)
            {
            if (Session["OrgId"].ToString() == "1")
                {
                if (CertShortName == "MC")
                    {
                    objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0 AND ISNULL(ACTIVESTATUS,0)=1", "SEMESTERNO ASC");

                    }

                if (CertShortName == "PC" || CertShortName == "TC")
                    {
                    objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO=" + Convert.ToInt32(semesterNo), "SEMESTERNO ASC");   // MODIFIED BY VINAY MISHRA ON 28082023
                    //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO=" + semesterNo, "SEMESTERNO ASC");
                    }

                else
                    {
                    // objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO ASC");
                    if (ddlDegree.SelectedValue == "9")
                        {
                        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO<3 AND SEMESTERNO<>0", "SEMESTERNO ASC");
                        }
                    else if (ddlDegree.SelectedValue == "4" || ddlDegree.SelectedValue == "3" || ddlDegree.SelectedValue == "7" || ddlDegree.SelectedValue == "6" || ddlDegree.SelectedValue == "8")
                    // else if (ddlDegree.SelectedValue == "2" || ddlDegree.SelectedValue == "4" || ddlDegree.SelectedValue == "3" || ddlDegree.SelectedValue == "6" || ddlDegree.SelectedValue == "7" || ddlDegree.SelectedValue == "8")
                        {
                        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO<7 AND SEMESTERNO<>0", "SEMESTERNO ASC");
                        }
                    else if (ddlDegree.SelectedValue == "5" || ddlDegree.SelectedValue == "1" || ddlDegree.SelectedValue == "2")
                        {
                        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO<9 AND SEMESTERNO<>0", "SEMESTERNO ASC");
                        }
                    else if (ddlDegree.SelectedValue == "10" || ddlDegree.SelectedValue == "11")
                        {
                        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO<5 AND SEMESTERNO<>0", "SEMESTERNO ASC");
                        }
                    else
                        {
                        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0 AND SEMESTERNO<>0", "SEMESTERNO ASC");
                        }
                    }
                }
            else
                {
                if (CertShortName == "PC" || CertShortName == "MC" || CertShortName == "TC")
                    {
                    if (Session["OrgId"].ToString() == "3" || Session["OrgId"].ToString() == "4" || Session["OrgId"].ToString() == "5" || Convert.ToInt32(Session["OrgId"].ToString()) >= 9)
                        {
                        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0 AND ISNULL(ACTIVESTATUS,0)=1", "SEMESTERNO ASC");
                        }
                    else
                        {
                        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO=" + Convert.ToInt32(semesterNo), "SEMESTERNO ASC");   // MODIFIED BY VINAY MISHRA ON 28082023
                        }

                    }


                else
                    {
                    // objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO ASC");
                    if (ddlDegree.SelectedValue == "9")
                        {
                        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO<3 AND SEMESTERNO<>0", "SEMESTERNO ASC");
                        }
                    else if (ddlDegree.SelectedValue == "4" || ddlDegree.SelectedValue == "3" || ddlDegree.SelectedValue == "7" || ddlDegree.SelectedValue == "6" || ddlDegree.SelectedValue == "8")
                    // else if (ddlDegree.SelectedValue == "2" || ddlDegree.SelectedValue == "4" || ddlDegree.SelectedValue == "3" || ddlDegree.SelectedValue == "6" || ddlDegree.SelectedValue == "7" || ddlDegree.SelectedValue == "8")
                        {
                        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO<7 AND SEMESTERNO<>0", "SEMESTERNO ASC");
                        }
                    else if (ddlDegree.SelectedValue == "5" || ddlDegree.SelectedValue == "1" || ddlDegree.SelectedValue == "2")
                        {
                        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO<9 AND SEMESTERNO<>0", "SEMESTERNO ASC");
                        }
                    else if (ddlDegree.SelectedValue == "10" || ddlDegree.SelectedValue == "11")
                        {
                        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO<5 AND SEMESTERNO<>0", "SEMESTERNO ASC");
                        }
                    else
                        {
                        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0 AND SEMESTERNO<>0", "SEMESTERNO ASC");
                        }
                    }
                }
            }
        else
            {
            objCommon.DisplayMessage("Please Certificate Type!", this.Page);
            ddlCert.Focus();
            }

        if (ddlCert.SelectedIndex == -1)
            {
            txtConvocation.Visible = false;
            lblConvocation.Visible = false;
            }
        if (ddlCert.SelectedValue == "6")
            {
            btnExcelSheetReport.Visible = true;
            btnExcelSheetReport.Enabled = false;
            }

        if (CertShortName == "PC")
            {
            txtConvocation.Text = string.Empty;
            lblConvocation.Visible = false;
            txtConvocation.Visible = false;
            chkAddTextOption.Visible = false;
            lblClass.Visible = false;
            ;
            txtClass.Visible = true;
            lvStudentRecords.Visible = false;
            }
        if (ddlCert.SelectedIndex == 0)
            {
            btnExcelSheetReport.Visible = false;
            lblClass.Visible = false;
            lblConvocation.Visible = false;
            txtConvocation.Visible = false;
            txtClass.Visible = false;
            txtClass.Text = string.Empty;
            txtConvocation.Text = string.Empty;
            lvStudentRecords.Visible = false;
            }
        if (Session["OrgId"].ToString() == "2")
            {
            if (ddlCert.SelectedValue == "2")
                {
                divtcpartfull.Visible = true;

                }
            else
                {
                divtcpartfull.Visible = false;
                }

            }

        if (ddlCert.SelectedValue != "6")
            {
            btnExcelSheetReport.Visible = false;
            }
        if (ddlCert.SelectedValue == "8")
            {
            idConversion.Visible = true;
            }
        if (ddlCert.SelectedValue != "8")
            {
            idConversion.Visible = false;
            }
        if (ddlCert.SelectedValue == "9")
            {
            trBonaSemester.Visible = true;
            }
        if (ddlCert.SelectedValue != "9")
            {
            trBonaSemester.Visible = false;
            }
        if (ddlCert.SelectedValue == "10")
            {
            trBonaSemester.Visible = true;
            idConversion.Visible = true;
            }
        if (ddlCert.SelectedValue != "10")
            {
            trBonaSemester.Visible = false;
            idConversion.Visible = false;
            }
        if (ddlCert.SelectedValue == "9")
            {
            trBonaSemester.Visible = true;
            }
        if (ddlCert.SelectedValue == "8")
            {
            idConversion.Visible = true;
            trBonaSemester.Visible = true;
            }
        if (Session["OrgId"].ToString() == "1")
            {
            if (CertShortName == "LC")
                {
                divAcademic.Visible = true;
                btnView.Visible = true;
                //rdotcpartfull.SelectedValue = "0";
                }

            if (CertShortName == "TC")
                {
                divAcademic.Visible = true;

                }
            if (CertShortName == "MC")
                {
                divAcademic.Visible = true;

                }
            }
        if (Session["OrgId"].ToString() == "6")
            {
            if (CertShortName == "BC")
                {
                divAcademic.Visible = true;
                }
            if (CertShortName == "TC")
            //if (CertShortName == "LC")
                {
                divAcademic.Visible = true;
                btnView.Visible = true;
                }



            }
        //added by pooja on adte 17-06-2023
        if (Session["OrgId"].ToString() == "3")
            {

            if (CertShortName == "TC")
                {
                divAcademic.Visible = true;
                btnView.Visible = true;

                }
            if (CertShortName == "MC")
                {
                divAcademic.Visible = true;

                }


            }

        if (Session["OrgId"].ToString() == "4")
            {

            if (CertShortName == "TC")
                {
                divAcademic.Visible = true;
                btnView.Visible = true;

                }
            if (CertShortName == "MC")
                {
                divAcademic.Visible = true;

                }


            }

        if (Session["OrgId"].ToString() == "10" || Session["OrgId"].ToString() == "11" || Session["OrgId"].ToString() == "19" || Session["OrgId"].ToString() == "20")
            {
            if (CertShortName == "LC")
                {
                divAcademic.Visible = true;
                btnView.Visible = true;
                //rdotcpartfull.SelectedValue = "0";
                }

            if (CertShortName == "TC")
                {
                divAcademic.Visible = true;


                }
            }

        }

    protected void btnExcelSheetReport_Click(object sender, EventArgs e)
        {
        ShowReport1("xls", "Excel Provisional  Report", "rptProvisionalCertificateForExcel.rpt");
        }

    private void ShowReport1(string exporttype, string reportTitle, string rptFileName)
        {
        try
            {

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            //url += "pagetitle=" + reportTitle;
            url += "exporttype=" + exporttype;
            url += "&filename=" + reportTitle.Replace(" ", "-").ToString() + "." + exporttype;
            url += "&path=~,REPORTS,ACADEMIC," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + GetStudentIDs().ToString() + ",@P_CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + "";

            divMsg.InnerHtml = "<script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += "window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.UP, this.updpnlMain.GetType(), "controlJSScript", sb.ToString(), true);

            }
        catch (Exception ex)
            {
            throw;
            }
        }

    protected void btnEnrollBonafide_Click(object sender, EventArgs e)
        {

        try
            {
            DataSet dsissueCert;
            CertificateMasterController objcerMasterController = new CertificateMasterController();

            string idNo = objCommon.LookUp("ACD_STUDENT", "IDNO", "CAN = 0 AND ENROLLNO='" + txtEnrollBonafide.Text.Trim() + "'");
            lvStudentRecords.DataSource = null;
            lvStudentRecords.DataBind();

            if (idNo != "" && idNo != null)
                {
                int chkidNo = Convert.ToInt32(idNo);
                dsissueCert = objcerMasterController.GetStudentListForIssueCertBona(chkidNo);
                if (dsissueCert.Tables[0].Rows.Count > 0)
                    {
                    //set null
                    lvIssueCertBona.DataSource = dsissueCert.Tables[0];
                    lvIssueCertBona.DataBind();
                    }
                else
                    {
                    objCommon.DisplayMessage(this.updpnlExam, "No student data found", this.Page);
                    lvIssueCertBona.DataSource = null;
                    lvIssueCertBona.DataBind();

                    }
                }
            else
                {
                objCommon.DisplayMessage(this.updpnlExam, "No student found having enrollment no.: " + txtEnrollBonafide.Text.Trim(), this);
                }
            }
        catch (Exception ex)
            {
            throw;
            }
        }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
        {
        // ddlCert.SelectedIndex = -1;
        txtConvocation.Text = string.Empty;
        txtConvocation.Visible = false;
        lblConvocation.Visible = false;
        lblClass.Visible = false;
        txtClass.Visible = false;
        txtClass.Text = string.Empty;
        lvStudentRecords.Visible = false;
        objCommon.FillDropDownList(semesterBonafied, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "(SEMESTERNO <= " + Convert.ToInt16(ddlSemester.SelectedValue) + " OR SEMESTERNO=1) AND SEMESTERNO > 0 ", "SEMESTERNO");
        }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
        {
        ddlBranch.SelectedIndex = -1;
        ddlDegree.SelectedIndex = -1;
        ddlSemester.SelectedIndex = -1;
        ddlCert.SelectedIndex = -1;
        txtConvocation.Visible = false;
        lblConvocation.Visible = false;
        txtConvocation.Text = string.Empty;
        lblClass.Visible = false;
        txtClass.Visible = false;
        txtClass.Text = string.Empty;
        lvStudentRecords.Visible = false;
        if (ddlCollege.SelectedIndex > 0)
            {
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D , ACD_COLLEGE_DEGREE C, ACD_COLLEGE_DEGREE_BRANCH CD", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO=C.DEGREENO AND CD.DEGREENO=D.DEGREENO AND C.DEGREENO>0 AND (C.COLLEGE_ID=" + ddlCollege.SelectedValue + " OR " + ddlCollege.SelectedValue + "= 0) AND CD.COLLEGE_ID IN(" + Session["college_nos"] + ")", "DEGREENAME");
            //ddlDegree.Focus();
            }
        }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
        {
        ddlCollege.SelectedIndex = -1;
        ddlDegree.SelectedIndex = -1;
        ddlBranch.SelectedIndex = -1;
        ddlSemester.SelectedIndex = -1;
        ddlCert.SelectedIndex = -1;
        txtConvocation.Text = string.Empty;
        txtConvocation.Visible = false;
        lblConvocation.Visible = false;
        lblClass.Visible = false;
        txtClass.Visible = false;
        txtClass.Text = string.Empty;
        lvStudentRecords.Visible = false;
        }
    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
        ddlSession.SelectedIndex = -1;
        ddlCollege.SelectedIndex = -1;
        ddlDegree.SelectedIndex = -1;
        ddlBranch.SelectedIndex = -1;
        ddlSemester.SelectedIndex = -1;
        ddlCert.SelectedIndex = -1;
        txtConvocation.Text = string.Empty;
        lblConvocation.Visible = false;
        txtConvocation.Visible = false;
        lblClass.Visible = false;
        txtClass.Visible = false;
        txtClass.Text = string.Empty;
        lvStudentRecords.Visible = false;

        }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
        ddlSemester.SelectedIndex = -1;
        ddlCert.SelectedIndex = -1;
        lblClass.Visible = false;
        lblConvocation.Visible = false;
        txtClass.Visible = false;
        txtConvocation.Visible = false;
        txtConvocation.Text = string.Empty;
        txtClass.Text = string.Empty;
        lvStudentRecords.Visible = false;
        }
    protected void btnStatsticalReport_Click(object sender, EventArgs e)
        {
        try
            {
            CertificateMasterController objcertMasterController = new CertificateMasterController();
            DataSet ds = objcertMasterController.GetAllCertificateStatisticalData();
            ds.Tables[0].TableName = "Certificate Generated Count";
            ds.Tables[1].TableName = "Certificate Generated Student";


            if (ds.Tables[0].Rows.Count < 1)
                {
                ds.Tables[0].Rows.Add("No Record Found");
                }
            if (ds.Tables[1].Rows.Count < 1)
                {
                ds.Tables[1].Rows.Add("No Record Found");
                }

            using (XLWorkbook wb = new XLWorkbook())
                {
                foreach (System.Data.DataTable dt in ds.Tables)
                    {
                    //Add System.Data.DataTable as Worksheet.
                    wb.Worksheets.Add(dt);
                    }
                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=CertificateStatisticalReport.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                    }
                }
            }
        catch
            {
            throw;
            }
        }
    protected void lvStudentRecords_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
        //DropDownList ddlconductcharacter = (DropDownList)e.Item.FindControl("ddlconductcharacter");
        //HiddenField hfConductNo = (HiddenField)e.Item.FindControl("hfConductNo");

        //CertificateMasterController objcertMasterController = new CertificateMasterController();
        //DataSet ds;
        //string CertShortName = objCommon.LookUp("ACD_CERTIFICATE_MASTER", "CERT_SHORT_NAME", "CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "");

        //int sessionNo = 0;
        //int branchNo = 0;
        //int semesterNo = 0;
        //int admbatchNo = 0;
        //int degreeNo = 0;
        //int collegeNo = 0;
        //int tcpartfullno = 0;

        ////if (Session["BC_Student"] == null || ((DataTable)Session["BC_Student"] == null))
        ////{
        //sessionNo = Convert.ToInt32(ddlSession.SelectedValue);
        //branchNo = Convert.ToInt32(ddlBranch.SelectedValue);
        //semesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
        //admbatchNo = Convert.ToInt32(ddlAdmBatch.SelectedValue);
        //degreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
        //collegeNo = Convert.ToInt32(ddlCollege.SelectedValue);
        //if (Convert.ToInt32(Session["OrgId"]) == 2)
        //{
        //    tcpartfullno = Convert.ToInt32(rdotcpartfull.SelectedValue);
        //}
        //int certno = Convert.ToInt32(ddlCert.SelectedValue);
        //ds = objcertMasterController.GetStudentListForBC(admbatchNo, sessionNo, collegeNo, degreeNo, branchNo, semesterNo, CertShortName, tcpartfullno, certno);

        //objCommon.FillDropDownList(ddlconductcharacter, "acd_tc_conduct_character", "CNO", "CONDUCT_CHARACTER", "CNO>0 AND  ACTIVESTATUS=1", "CNO");

        //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //{
        //    //if (ds.Tables[0].Rows[i]["CONDUCT_NO"].ToString().Equals(ds.Tables[0].Rows[i]["CONDUCT_NO"].ToString()))
        //    //{
        //    if (e.Item.ItemType == ListViewItemType.DataItem)
        //    {
        //        string aaa = ds.Tables[0].Rows[i]["CONDUCT_NO"].ToString();

        //        ddlconductcharacter.SelectedValue = hfConductNo.Value;
        //    }
        //}
        //}
        }
    protected void btnView_Click(object sender, EventArgs e)
        {
        int count = 0;
        foreach (ListViewDataItem dataitem in lvStudentRecords.Items)
            {
            CheckBox cbRow = dataitem.FindControl("chkReport") as CheckBox;
            if (cbRow.Checked == true)
                count++;
            }
        if (count <= 0)
            {
            //objCommon.DisplayMessage(this.updpnlExam, "Please Select atleast one Student for issuing Certificate", this);
            return;
            }
        else
            {
            string CertShortName = objCommon.LookUp("ACD_CERTIFICATE_MASTER", "CERT_SHORT_NAME", "CERT_NO=" + Convert.ToInt32(ddlCert.SelectedValue) + "");
            if (CertShortName == "LC")
                {
                if (Session["OrgId"].ToString() == "1")
                    {

                    if (ddlAcademicYear.SelectedIndex == 0)
                        {
                        objCommon.DisplayMessage(this.updpnlExam, "Please Select Academic Year", this);
                        ddlAcademicYear.Focus();
                        return;
                        }
                    ShowReport_BC_View(GetStudentIDs(), "Leaving_Certificate", "CrystalReport_LC_RCPIT_View.rpt");
                    }
                else if (Session["OrgId"].ToString() == "19")
                    {
                    if (ddlAcademicYear.SelectedIndex == 0)
                        {
                        objCommon.DisplayMessage(this.updpnlExam, "Please Select Academic Year", this);
                        ddlAcademicYear.Focus();
                        return;
                        }
                    ShowReport_BC_View(GetStudentIDs(), "Leaving_Certificate", "rptTC_Report_PCEN_View.rpt");
                    }
                else if (Session["OrgId"].ToString() == "20")
                    {
                    if (ddlAcademicYear.SelectedIndex == 0)
                        {
                        objCommon.DisplayMessage(this.updpnlExam, "Please Select Academic Year", this);
                        ddlAcademicYear.Focus();
                        return;
                        }
                    ShowReport_BC_View(GetStudentIDs(), "Leaving_Certificate", "rptTC_Report_PJLCE_View.rpt");
                    }
                }


            else if (CertShortName == "TC")
                {
                if (Session["OrgId"].ToString() == "3")
                    {
                    if (ddlAcademicYear.SelectedIndex == 0)
                        {
                        objCommon.DisplayMessage(this.updpnlExam, "Please Select Academic Year", this);
                        ddlAcademicYear.Focus();
                        return;
                        }
                    ShowReport_TC_View_CPUKH(GetStudentIDs(), "Transfer_Certificate", "CrystalReport_LC_CPUK_View.rpt");
                    }
                else if (Session["OrgId"].ToString() == "4")
                    {
                    if (ddlAcademicYear.SelectedIndex == 0)
                        {
                        objCommon.DisplayMessage(this.updpnlExam, "Please Select Academic Year", this);
                        ddlAcademicYear.Focus();
                        return;
                        }
                    ShowReport_TC_View_CPUKH(GetStudentIDs(), "Transfer_Certificate", "CrystalReport_LC_CPUH_View.rpt");
                    }

                }
            }
        }
    //protected void Chkstatus_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (Chkstatus.Checked)
    //    {
    //        dvbranch.Visible = true;
    //    }
    //    else
    //    {
    //        dvbranch.Visible = false;
    //    }
    //}
    protected void rdotcpartfull_SelectedIndexChanged(object sender, EventArgs e)
        {
        if (rdotcpartfull.SelectedValue == "3")
            {
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0 AND SEMESTERNO<>0", "SEMESTERNO ASC");
            }
        }
    }
