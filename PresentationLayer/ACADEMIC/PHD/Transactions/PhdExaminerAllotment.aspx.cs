//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : PHD Examiner Allotment                                                    
// CREATION DATE : 31/05/2019                                                         
// CREATED BY    : Dipali Nanore                           
// MODIFIED DATE :                 
// ADDED BY      :   Dipali Nanore                                
// MODIFIED DESC :                                                                      
//======================================================================================
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Common;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Net.Mail;
using System.Net;


public partial class Academic_PhdExaminerAllotment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    static int chkdrcstatus = 0; static int SupStatus = 0; static int DeanStatus = 0; static int DeanApprovalStatus = 0; static int Vivaconfirm = 0;
    static int DeanExternalStatus = 0; static int CoSupStatus = 0;
    string ua_dept = string.Empty; static string RejectRemark = string.Empty;
    string Filepath = string.Empty;
    string Filename = string.Empty;
    string Filepath_presynopsis = string.Empty;
    string Filename_presynopsis = string.Empty;
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
        //********************************
        string Sessionexam = string.Empty;

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
                //CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //**************************************
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                ViewState["usertype"] = ua_type;

                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                if (Session["usertype"].ToString() == "1")
                {
                    //dvdetails.Visible = true;
                    //divGeneralInfo.Visible = false;
                    //dvexaminerdetails.Visible = false;
                    //dvbuttons.Visible = false;
                   
                    //ShowStudentDetails();
                    //dvdetails.Visible = false;
                    //divGeneralInfo.Visible = true;
                    //dvexaminerdetails.Visible = true;s
                    //dvbuttons.Visible = true;

                }
                
               
                //else
                //{
                //    dvdetails.Visible = false;
                //    divGeneralInfo.Visible = true;
                //    dvexaminerdetails.Visible = true;
                //    dvbuttons.Visible = true;
                //    ShowStudentDetails();
                //    //if (Request.QueryString["id"] != null)
                //    //{
                //    //   // ViewState["action"] = "edit";
                //    //    ShowStudentDetails();
                //    //}
                //}
               
                //Populate all the DropDownLists
                FillDropDown();
                this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME", "ID > 0 ", "SRNO");
                ddlSearch.SelectedIndex = 0;
                ViewState["Receiptcode"] = "PHD";
                if (ViewState["action"] == null)
                    ViewState["action"] = "add";

                ViewState["usertype"] = ua_type;

                if (ViewState["usertype"].ToString() == "3")
                {
                    EnableDropDown();
                }
                else
                {
                    string ua_type_fac = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                    if (ua_type_fac == "4")
                    {
                        DisableDropDown();
                    }
                }
                //if (Request.QueryString["id"] != null)
                //{
                //    ViewState["action"] = "edit";
                //    ShowStudentDetails();
                //}

            }
        }
        else
        {

            if (Page.Request.Params["__EVENTTARGET"] != null)
            {
                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnsearch"))
                {
                    string[] arg = Page.Request.Params["__EVENTARGUMENT"].ToString().Split(',');
                    bindlist(arg[0], arg[1]);
                }

                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btncancelmodal"))
                {
                    txtSearch.Text = string.Empty;
                    lvStudent.DataSource = null;
                    lvStudent.DataBind();
                    lblNoRecords.Text = string.Empty;
                }



               

            }
        }

        if (Page.Request.Params["__EVENTTARGET"] != null)
        {
            if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnsearchstu"))
            {
                string[] arg = Page.Request.Params["__EVENTARGUMENT"].ToString().Split(',');
               // bindliststudent(arg[0], arg[1]);
            }

            if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnClose"))
            {
                // lblMsg.Text = string.Empty;

            }
        }
      
        divMsg.InnerHtml = string.Empty;
        ddlNdgc.Enabled = false;
      
    }

    public void EnableDropDown()
    {
        ddlExaminer1.Enabled = ddlExaminer2.Enabled = ddlExaminer3.Enabled = ddlExaminer4.Enabled = ddlExaminer5.Enabled = ddlExaminer6.Enabled = ddlExaminer7.Enabled =
            ddlExaminer8.Enabled = ddlExaminer9.Enabled = ddlExaminer10.Enabled = true;

        chkExaminer1.Enabled = chkExaminer2.Enabled = chkExaminer3.Enabled = chkExaminer4.Enabled = chkExaminer5.Enabled =
            chkExaminer6.Enabled = chkExaminer7.Enabled = chkExaminer8.Enabled = chkExaminer9.Enabled = chkExaminer10.Enabled = false;

    }

    public void DisableDropDown()
    {
        ddlExaminer1.Enabled = ddlExaminer2.Enabled = ddlExaminer3.Enabled = ddlExaminer4.Enabled = ddlExaminer5.Enabled = ddlExaminer6.Enabled = ddlExaminer7.Enabled =
            ddlExaminer8.Enabled = ddlExaminer9.Enabled = ddlExaminer10.Enabled = false;

        chkExaminer1.Enabled = chkExaminer2.Enabled = chkExaminer3.Enabled = chkExaminer4.Enabled = chkExaminer5.Enabled =
            chkExaminer6.Enabled = chkExaminer7.Enabled = chkExaminer8.Enabled = chkExaminer9.Enabled = chkExaminer10.Enabled = false;
    }

    public void CheckEnable()
    {
        chkExaminer1.Enabled = chkExaminer2.Enabled = chkExaminer3.Enabled = chkExaminer4.Enabled = chkExaminer5.Enabled =
              chkExaminer6.Enabled = chkExaminer7.Enabled = chkExaminer8.Enabled = chkExaminer9.Enabled = chkExaminer10.Enabled = true;
    }

    public void CheckVisibleFalse()
    {
        chkExaminer1.Visible = chkExaminer2.Visible = chkExaminer3.Visible = chkExaminer4.Visible = chkExaminer5.Visible =
              chkExaminer6.Visible = chkExaminer7.Visible = chkExaminer8.Visible = chkExaminer9.Visible = chkExaminer10.Visible = false;
    }

    public void CheckVisibleTrue()
    {
        chkExaminer1.Visible = chkExaminer2.Visible = chkExaminer3.Visible = chkExaminer4.Visible = chkExaminer5.Visible =
              chkExaminer6.Visible = chkExaminer7.Visible = chkExaminer8.Visible = chkExaminer9.Visible = chkExaminer10.Visible = true;
    }
    
    private void FillDropDown()
    {
        try
        {
            string ua_dept = objCommon.LookUp("User_Acc", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(Session["userno"]));
            //objCommon.FillDropDownList(ddlDepatment, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO", "BRANCHNO");

            objCommon.FillDropDownList(ddlExaminer1, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0  AND ISNULL(EXAMINERSTATUS,0)=1", "NAME");
            objCommon.FillDropDownList(ddlExaminer2, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0  AND ISNULL(EXAMINERSTATUS,0)=1", "NAME");
            objCommon.FillDropDownList(ddlExaminer3, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0  AND ISNULL(EXAMINERSTATUS,0)=1", "NAME");
            objCommon.FillDropDownList(ddlExaminer4, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0  AND ISNULL(EXAMINERSTATUS,0)=1", "NAME");
            objCommon.FillDropDownList(ddlExaminer5, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0  AND ISNULL(EXAMINERSTATUS,0)=1", "NAME");
            objCommon.FillDropDownList(ddlExaminer6, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0  AND ISNULL(EXAMINERSTATUS,0)=1", "NAME");
            objCommon.FillDropDownList(ddlExaminer7, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0  AND ISNULL(EXAMINERSTATUS,0)=1", "NAME");
            objCommon.FillDropDownList(ddlExaminer8, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0  AND ISNULL(EXAMINERSTATUS,0)=1", "NAME");
            objCommon.FillDropDownList(ddlExaminer9, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0  AND ISNULL(EXAMINERSTATUS,0)=1", "NAME");
            objCommon.FillDropDownList(ddlExaminer10, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0 AND ISNULL(EXAMINERSTATUS,0)=1", "NAME");

            //objCommon.FillDropDownList(ddlSupervisor, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO>0 AND UA_TYPE = 3 AND (DRCSTATUS='S' OR DRCSTATUS='SD')", "ua_fullname");
            //objCommon.FillDropDownList(ddlCoSupervisor, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO>0 AND UA_TYPE = 3 AND (DRCSTATUS='S' OR DRCSTATUS='SD')", "ua_fullname");
            //objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>10", "BATCHNO");
          //  objCommon.FillDropDownList(ddlDRCChairman, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO>0 AND UA_TYPE = 3 AND  (DRCSTATUS='S' OR  DRCSTATUS='D' OR DRCSTATUS='SD')", "ua_fullname");
            //objCommon.FillDropDownList(ddlStatusCat, "ACD_PHDSTATUS_CATEGORY", "PHDSTATAUSCATEGORYNO", "PHDSTATAUSCATEGRYNAME", "PHDSTATAUSCATEGORYNO > 0", "PHDSTATAUSCATEGORYNO");
            // ddlDRCChairman.SelectedIndex = 1;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_PhdAnnexure.FillDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowStudentDetails()
    {
        PhdController objSC = new PhdController();
        DataTableReader dtr = null;
        dtr = objSC.GetExaminerPHDDetails(Convert.ToInt32(Session["stuinfoidno"]));
       
        if (dtr != null)
        {
            if(dtr.Read())
            {

                #region commented
                //lblidno.Text = dtr["IDNO"].ToString();
                //txtRegNo.ToolTip = dtr["IDNO"].ToString();
                //txtEnrollno.ToolTip = dtr["ROLLNO"].ToString();
                //txtEnrollno.Text = dtr["ENROLLNO"].ToString();
                //txtRegNo.Text = dtr["ROLLNO"].ToString();
                //txtStudentName.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                //txtFatherName.Text = dtr["FATHERNAME"] == null ? string.Empty : dtr["FATHERNAME"].ToString().ToUpper();
                //txtDateOfJoining.Text = dtr["ADMDATE"] == DBNull.Value ? "" : Convert.ToDateTime(dtr["ADMDATE"]).ToString("dd/MM/yyyy");
                //ddlDepatment.SelectedValue = dtr["BRANCHNO"] == null ? "0" : dtr["BRANCHNO"].ToString();
                //hfstatusno.Value = dtr["PHDSTATUS"] == null ? "0" : dtr["PHDSTATUS"].ToString();
                //if (dtr["PHDSTATUS"] == null)
                //{
                //    partfull.Text = "";
                //}
                //if (dtr["PHDSTATUS"].ToString() == "1")
                //{
                //    partfull.Text = "Fulltime";
                //}
                //if (dtr["PHDSTATUS"].ToString() == "2")
                //{
                //    partfull.Text = "Parttime";
                //}
                //ddlAdmBatch.SelectedValue = dtr["ADMBATCH"] == null ? "0" : dtr["ADMBATCH"].ToString();
                //txtTotCredits.Text =Convert.ToDecimal(dtr["CREDITS"]).ToString();
                //ddlNdgc.SelectedValue = dtr["NOOFDGC"].ToString() == "0" ? "4" : dtr["NOOFDGC"].ToString();
                //ddlStatus.SelectedValue = dtr["FULLPART"] == null ? "0" : dtr["FULLPART"].ToString();
                //ddlSupervisorrole.SelectedValue = dtr["SUPERROLE"] == null ? "0" : dtr["SUPERROLE"].ToString();
                //ddlStatusCat.SelectedValue = dtr["PHDSTATUSCAT"] == null ? "0" : dtr["PHDSTATUSCAT"].ToString();
                //ddlSupervisor.SelectedValue = dtr["SUPERVISORNO"] == null ? "0" : dtr["SUPERVISORNO"].ToString();
                //lblname.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                //lbldate.Text = dtr["ADMDATE"] == DBNull.Value ? "" : Convert.ToDateTime(dtr["ADMDATE"]).ToString("dd/MM/yyyy");
                //string a = dtr["DRCCHAIRMANNO"].ToString() == null ? "0" : dtr["DRCCHAIRMANNO"].ToString();
                //ddlDRCChairman.SelectedValue = a == string.Empty ? "0" : dtr["DRCCHAIRMANNO"].ToString();
                //txtResearch.Text = dtr["RESEARCH"].ToString();
                //btnSubmit.Enabled = true;
                ////-------------------------------------
                //SupStatus = Convert.ToInt32(dtr["SUPERVISORSTATUS"] == null ? "0" : dtr["SUPERVISORSTATUS"].ToString());
                //chkdrcstatus = Convert.ToInt32(dtr["DRCCHAIRMANSTATUS"] == null ? "0" : dtr["DRCCHAIRMANSTATUS"].ToString());
                //DeanStatus = Convert.ToInt32(dtr["DEAN_STATUS"] == null ? "0" : dtr["DEAN_STATUS"].ToString());
                //DeanApprovalStatus = Convert.ToInt32(dtr["DEAN_APPROVAL_STATUS"] == null ? "0" : dtr["DEAN_APPROVAL_STATUS"].ToString());
                //Vivaconfirm = Convert.ToInt32(dtr["VIVA_CONFIRM"] == null ? "0" : dtr["VIVA_CONFIRM"].ToString());
                //DeanExternalStatus = Convert.ToInt32(dtr["DEAN_EXTERNAL_STATUS"] == null ? "0" : dtr["DEAN_EXTERNAL_STATUS"].ToString());

                ////-------------  joint supervisior  ------------------//
                //CoSupStatus = Convert.ToInt32(dtr["JOINTSUPERVISORSTATUS"] == null ? "0" : dtr["JOINTSUPERVISORSTATUS"].ToString());
                //ddlCoSupervisor.SelectedValue = dtr["JOINTSUPERVISORNO"] == "0" ? "0" : dtr["JOINTSUPERVISORNO"].ToString();
                // ----- Pre synopsis data --- //
                //txtdatepre.Text = dtr["PREDATE"] == DBNull.Value ? "" : (dtr["PREDATE"]).ToString();


                ////----- Check drc condition -------------------//
                //ddlExaminer1.SelectedValue = dtr["EXAMINER1"] == null ? "0" : dtr["EXAMINER1"].ToString();
                //ddlExaminer2.SelectedValue = dtr["EXAMINER2"] == null ? "0" : dtr["EXAMINER2"].ToString();
                //ddlExaminer3.SelectedValue = dtr["EXAMINER3"] == null ? "0" : dtr["EXAMINER3"].ToString();
                //ddlExaminer4.SelectedValue = dtr["EXAMINER4"] == null ? "0" : dtr["EXAMINER4"].ToString();
                //ddlExaminer5.SelectedValue = dtr["EXAMINER5"] == null ? "0" : dtr["EXAMINER5"].ToString();
                //ddlExaminer6.SelectedValue = dtr["EXAMINER6"] == null ? "0" : dtr["EXAMINER6"].ToString();
                //ddlExaminer7.SelectedValue = dtr["EXAMINER7"] == null ? "0" : dtr["EXAMINER7"].ToString();
                //ddlExaminer8.SelectedValue = dtr["EXAMINER8"] == null ? "0" : dtr["EXAMINER8"].ToString();
                //ddlExaminer9.SelectedValue = dtr["EXAMINER9"] == null ? "0" : dtr["EXAMINER9"].ToString();
                //ddlExaminer10.SelectedValue = dtr["EXAMINER10"] == null ? "0" : dtr["EXAMINER10"].ToString();

#endregion

                //---------------------------------------------------------------
                //Session["stuinfoidno"] = idno;
                lblidno.Text = dtr["IDNO"].ToString();
                lblenrollmentnos.Text = dtr["ENROLLNO"].ToString();
                lblrollno.Text = dtr["ROLLNO"].ToString();
                lblnames.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                lblfathername.Text = dtr["FATHERNAME"] == null ? string.Empty : dtr["FATHERNAME"].ToString().ToUpper();
                lbljoiningdate.Text = dtr["ADMDATE"] == DBNull.Value ? "" : Convert.ToDateTime(dtr["ADMDATE"]).ToString("dd/MM/yyyy");
                lbldepartment.Text = dtr["LONGNAME"] == null ? string.Empty : dtr["LONGNAME"].ToString();
                hfdepartmentno.Value = dtr["BRANCHNO"] == null ? "0" : dtr["BRANCHNO"].ToString();
                hfstatusno.Value = dtr["PHDSTATUS"] == null ? "0" : dtr["PHDSTATUS"].ToString();
                if (dtr["PHDSTATUS"] == null)
                {
                    partfull.Text = "";
                }
                if (dtr["PHDSTATUS"].ToString() == "1")
                {
                    partfull.Text = "Fulltime";
                }
                if (dtr["PHDSTATUS"].ToString() == "2")
                {
                    partfull.Text = "Parttime";
                }
                lbladmbatch.Text = dtr["BATCHNAME"] == null ? string.Empty : dtr["BATCHNAME"].ToString();
                hfbatchno.Value = dtr["ADMBATCH"] == null ? "0" : dtr["ADMBATCH"].ToString();
               // lblcredit.Text = Convert.ToDecimal(dtr["CREDITS"]).ToString();
                hfdgcmemberno.Value = dtr["NOOFDGC"].ToString() == "0" ? "4" : dtr["NOOFDGC"].ToString();
                lbldgcmember.Text = dtr["NOOFDGC"].ToString() == "0" ? "4" : dtr["NOOFDGC"].ToString();
                
                hfstatusno.Value = dtr["FULLPART"] == null ? "0" : dtr["FULLPART"].ToString();
                if (dtr["FULLPART"] == null)
                {
                    lblstatus.Text = "";
                }
                if (dtr["FULLPART"].ToString() == "1")
                {
                    lblstatus.Text = "Full Time";
                }
                if (dtr["FULLPART"].ToString() == "2")
                {
                    lblstatus.Text = "Part Time";
                }
                lblSupervisorrole.Text = dtr["SUPERVISERROLE"] == null ? string.Empty : dtr["SUPERVISERROLE"].ToString();
                lblSupervisor.Text = dtr["SUPERVISORNAME"] == null ? string.Empty : dtr["SUPERVISORNAME"].ToString();
                hfSupervisorno.Value = dtr["SUPERVISORNO"] == null ? "0" : dtr["SUPERVISORNO"].ToString();
                hfSupervisorroleno.Value = dtr["SUPERROLE"] == null ? "0" : dtr["SUPERROLE"].ToString();
                hfJointsuperno.Value = dtr["JOINTSUPERVISORNO"] == "0" ? "0" : dtr["JOINTSUPERVISORNO"].ToString();
                hfstatuscatno.Value = dtr["PHDSTATUSCAT"] == null ? "0" : dtr["PHDSTATUSCAT"].ToString();
                lblstatuscategory.Text = dtr["PHDSTATAUSCATEGRYNAME"] == null ? string.Empty : dtr["PHDSTATAUSCATEGRYNAME"].ToString();
                lblname.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                lbldate.Text = dtr["ADMDATE"] == DBNull.Value ? "" : Convert.ToDateTime(dtr["ADMDATE"]).ToString("dd/MM/yyyy");
                string a = dtr["DRCCHAIRMANNO"].ToString() == null ? "0" : dtr["DRCCHAIRMANNO"].ToString();
                hfdrcChairmanno.Value = a == string.Empty ? "0" : dtr["DRCCHAIRMANNO"].ToString();
                lbldrcChairman.Text = dtr["DRCCHAIRMANAME"] == null ? string.Empty : dtr["DRCCHAIRMANAME"].ToString();

                txtResearch.Text = dtr["RESEARCH"].ToString();
                btnSubmit.Enabled = true;
                //-------------------------------------
                SupStatus = Convert.ToInt32(dtr["SUPERVISORSTATUS"] == null ? "0" : dtr["SUPERVISORSTATUS"].ToString());
                chkdrcstatus = Convert.ToInt32(dtr["DRCCHAIRMANSTATUS"] == null ? "0" : dtr["DRCCHAIRMANSTATUS"].ToString());
                DeanStatus = Convert.ToInt32(dtr["DEAN_STATUS"] == null ? "0" : dtr["DEAN_STATUS"].ToString());
                DeanApprovalStatus = Convert.ToInt32(dtr["DEAN_APPROVAL_STATUS"] == null ? "0" : dtr["DEAN_APPROVAL_STATUS"].ToString());
                Vivaconfirm = Convert.ToInt32(dtr["VIVA_CONFIRM"] == null ? "0" : dtr["VIVA_CONFIRM"].ToString());
                DeanExternalStatus = Convert.ToInt32(dtr["DEAN_EXTERNAL_STATUS"] == null ? "0" : dtr["DEAN_EXTERNAL_STATUS"].ToString());

                //-------------  joint supervisior  ------------------//
                CoSupStatus = Convert.ToInt32(dtr["JOINTSUPERVISORSTATUS"] == null ? "0" : dtr["JOINTSUPERVISORSTATUS"].ToString());
                ddlCoSupervisor.SelectedValue = dtr["JOINTSUPERVISORNO"] == "0" ? "0" : dtr["JOINTSUPERVISORNO"].ToString();
                // ----- Pre synopsis data --- //
                txtdatepre.Text = dtr["PREDATE"] == DBNull.Value ? "" : (dtr["PREDATE"]).ToString();
                //-----------------------------------------------------------------------------------


                int exam1 = Convert.ToInt32(objCommon.LookUp("ACD_PHD_EXAMINER", "COUNT(1)", "idno=" + dtr["IDNO"].ToString()));
                if (exam1 > 0)
                {
                    try
                    {
                        objCommon.FillDropDownList(ddlExaminer1, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0", "NAME");
                        objCommon.FillDropDownList(ddlExaminer2, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0", "NAME");
                        objCommon.FillDropDownList(ddlExaminer3, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0", "NAME");
                        objCommon.FillDropDownList(ddlExaminer4, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0 ", "NAME");
                        objCommon.FillDropDownList(ddlExaminer5, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0 ", "NAME");
                        objCommon.FillDropDownList(ddlExaminer6, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0 ", "NAME");
                        objCommon.FillDropDownList(ddlExaminer7, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0 ", "NAME");
                        objCommon.FillDropDownList(ddlExaminer8, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0 ", "NAME");
                        objCommon.FillDropDownList(ddlExaminer9, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0 ", "NAME");
                        objCommon.FillDropDownList(ddlExaminer10, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0", "NAME");
                    }
                    catch { }

                    ddlExaminer1.SelectedValue = dtr["EXAMINER1"] == null ? "0" : dtr["EXAMINER1"].ToString();
                    ddlExaminer2.SelectedValue = dtr["EXAMINER2"] == null ? "0" : dtr["EXAMINER2"].ToString();
                    ddlExaminer3.SelectedValue = dtr["EXAMINER3"] == null ? "0" : dtr["EXAMINER3"].ToString();
                    ddlExaminer4.SelectedValue = dtr["EXAMINER4"] == null ? "0" : dtr["EXAMINER4"].ToString();
                    ddlExaminer5.SelectedValue = dtr["EXAMINER5"] == null ? "0" : dtr["EXAMINER5"].ToString();
                    ddlExaminer6.SelectedValue = dtr["EXAMINER6"] == null ? "0" : dtr["EXAMINER6"].ToString();
                    ddlExaminer7.SelectedValue = dtr["EXAMINER7"] == null ? "0" : dtr["EXAMINER7"].ToString();
                    ddlExaminer8.SelectedValue = dtr["EXAMINER8"] == null ? "0" : dtr["EXAMINER8"].ToString();
                    ddlExaminer9.SelectedValue = dtr["EXAMINER9"] == null ? "0" : dtr["EXAMINER9"].ToString();
                    ddlExaminer10.SelectedValue = dtr["EXAMINER10"] == null ? "0" : dtr["EXAMINER10"].ToString();
                }
                else
                {
                    try
                    {
                        objCommon.FillDropDownList(ddlExaminer1, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0  AND ISNULL(EXAMINERSTATUS,0)=1", "NAME");
                        objCommon.FillDropDownList(ddlExaminer2, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0  AND ISNULL(EXAMINERSTATUS,0)=1", "NAME");
                        objCommon.FillDropDownList(ddlExaminer3, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0  AND ISNULL(EXAMINERSTATUS,0)=1", "NAME");
                        objCommon.FillDropDownList(ddlExaminer4, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0  AND ISNULL(EXAMINERSTATUS,0)=1", "NAME");
                        objCommon.FillDropDownList(ddlExaminer5, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0  AND ISNULL(EXAMINERSTATUS,0)=1", "NAME");
                        objCommon.FillDropDownList(ddlExaminer6, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0  AND ISNULL(EXAMINERSTATUS,0)=1", "NAME");
                        objCommon.FillDropDownList(ddlExaminer7, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0  AND ISNULL(EXAMINERSTATUS,0)=1", "NAME");
                        objCommon.FillDropDownList(ddlExaminer8, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0  AND ISNULL(EXAMINERSTATUS,0)=1", "NAME");
                        objCommon.FillDropDownList(ddlExaminer9, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0  AND ISNULL(EXAMINERSTATUS,0)=1", "NAME");
                        objCommon.FillDropDownList(ddlExaminer10, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0 AND ISNULL(EXAMINERSTATUS,0)=1", "NAME");

                    }
                    catch { }

                    ddlExaminer1.SelectedValue = dtr["EXAMINER1"] == null ? "0" : dtr["EXAMINER1"].ToString();
                    ddlExaminer2.SelectedValue = dtr["EXAMINER2"] == null ? "0" : dtr["EXAMINER2"].ToString();
                    ddlExaminer3.SelectedValue = dtr["EXAMINER3"] == null ? "0" : dtr["EXAMINER3"].ToString();
                    ddlExaminer4.SelectedValue = dtr["EXAMINER4"] == null ? "0" : dtr["EXAMINER4"].ToString();
                    ddlExaminer5.SelectedValue = dtr["EXAMINER5"] == null ? "0" : dtr["EXAMINER5"].ToString();
                    ddlExaminer6.SelectedValue = dtr["EXAMINER6"] == null ? "0" : dtr["EXAMINER6"].ToString();
                    ddlExaminer7.SelectedValue = dtr["EXAMINER7"] == null ? "0" : dtr["EXAMINER7"].ToString();
                    ddlExaminer8.SelectedValue = dtr["EXAMINER8"] == null ? "0" : dtr["EXAMINER8"].ToString();
                    ddlExaminer9.SelectedValue = dtr["EXAMINER9"] == null ? "0" : dtr["EXAMINER9"].ToString();
                    ddlExaminer10.SelectedValue = dtr["EXAMINER10"] == null ? "0" : dtr["EXAMINER10"].ToString();
                }



                //------------------- call examiner selected index  --------------------//
                ddlExaminer1_SelectedIndexChanged(null, null);
                ddlExaminer2_SelectedIndexChanged(null, null);
                ddlExaminer3_SelectedIndexChanged(null, null);
                ddlExaminer4_SelectedIndexChanged(null, null);
                ddlExaminer5_SelectedIndexChanged(null, null);
                ddlExaminer6_SelectedIndexChanged(null, null);
                ddlExaminer7_SelectedIndexChanged(null, null);
                ddlExaminer8_SelectedIndexChanged(null, null);
                ddlExaminer9_SelectedIndexChanged(null, null);
                ddlExaminer10_SelectedIndexChanged(null, null);

                //-----------------------------------------------------------------------------
                RejectRemark = dtr["REJECTREMARK"] == null ? string.Empty : dtr["REJECTREMARK"].ToString();
                txtviva.Text = dtr["VIVA_DATE"] == null ? string.Empty : dtr["VIVA_DATE"].ToString();
                if (txtviva.Text != "")
                {
                    txtviva.Enabled = false;
                }
                //----------------------------------------------
                //if (SupStatus == 1 && ViewState["usertype"].ToString() == "3" && ddlDRCChairman.SelectedValue == Session["userno"].ToString())
                if (SupStatus == 1 && ViewState["usertype"].ToString() == "3" && hfdrcChairmanno.Value == Session["userno"].ToString())
                {
                    DisableDropDown();
                    btnSubmit.Text = "Confirm";
                }
                //-------------- dean status -----------------//
                if (ViewState["usertype"].ToString() == "4")
                {
                    if (ViewState["usertype"].ToString() == "4" && DeanStatus == 0)
                    {
                        if (SupStatus == 1 && chkdrcstatus == 1)
                        {
                            btnReject.Visible = true;
                            txtRemark.Enabled = true;
                            btnSubmit.Text = "Confirm";
                            DisableDropDown();
                        }
                        else
                        {
                            objCommon.DisplayMessage("Please Approve Supervisor & DRC chairman first !!", this.Page);
                            btnSubmit.Enabled = false;
                        }
                    }
                    else if (DeanStatus == 1 && DeanApprovalStatus == 0)
                    {
                        btnReport.Visible = true;
                        txtRemark.Enabled = false;
                        btnReject.Visible = false;
                        btnSubmit.Text = "Approval";
                        CheckEnable();
                    }
                    else
                    {
                        btnReport.Visible = true;
                        btnAppoint.Visible = true;
                        txtRemark.Enabled = false;
                        //   btnExternalReport.Visible = true;
                    }

                    divpresynopsis.Visible = true;
                    divsynopsis.Visible = true;
                    txtdatepre.Enabled = false;
                }
                /// --- all status  ------/// 
                if (SupStatus == 1 && chkdrcstatus == 1 && DeanStatus == 1 && CoSupStatus == 1)
                {
                    btnReport.Visible = true;
                    // tdremark.Visible = false;
                }
                if (SupStatus == 1 && CoSupStatus == 1  && chkdrcstatus == 1 && DeanStatus == 1 && DeanApprovalStatus == 1 && DeanExternalStatus == 0 && ViewState["usertype"].ToString() == "3")
                {
                    btnReport.Visible = true;
                    //  btnAppoint.Visible = true;
                    //  btnSubmit.Enabled = false;
                    //  btnExternalReport.Visible = true;
                    DisableDropDown();
                    CheckVisibleFalse();
                }

                if (SupStatus == 1 && CoSupStatus == 1 && chkdrcstatus == 1 && DeanStatus == 1 && DeanApprovalStatus == 1 && DeanExternalStatus == 1)
                {
                    btnReport.Visible = true;
                     btnAppoint.Visible = true;
                     btnSubmit.Enabled = false;
                    //  btnExternalReport.Visible = true;
                    DisableDropDown();
                   // CheckVisibleFalse();
                }

                //  --- dean external confirm 
                if (ViewState["usertype"].ToString() == "4" && SupStatus == 1 && CoSupStatus == 1 && chkdrcstatus == 1 && DeanStatus == 1 && DeanApprovalStatus == 1 && DeanExternalStatus == 0)
                {
                    div1.Visible = true;
                    divConfirm.Visible = false;
                    //trViva.Visible = true;
                    btnSubmit.Enabled = true;
                    chkExaminer1.Enabled = Convert.ToBoolean(Convert.ToInt32(dtr["EXAMINER1STATUS"] == "0" ? "0" : dtr["EXAMINER1STATUS"].ToString()));
                    chkExaminer2.Enabled = Convert.ToBoolean(Convert.ToInt32(dtr["EXAMINER2STATUS"] == "0" ? "0" : dtr["EXAMINER2STATUS"].ToString()));
                    chkExaminer3.Enabled = Convert.ToBoolean(Convert.ToInt32(dtr["EXAMINER3STATUS"] == "0" ? "0" : dtr["EXAMINER3STATUS"].ToString()));
                    chkExaminer4.Enabled = Convert.ToBoolean(Convert.ToInt32(dtr["EXAMINER4STATUS"] == "0" ? "0" : dtr["EXAMINER4STATUS"].ToString()));
                    chkExaminer5.Enabled = Convert.ToBoolean(Convert.ToInt32(dtr["EXAMINER5STATUS"] == "0" ? "0" : dtr["EXAMINER5STATUS"].ToString()));
                    chkExaminer6.Enabled = Convert.ToBoolean(Convert.ToInt32(dtr["EXAMINER6STATUS"] == "0" ? "0" : dtr["EXAMINER6STATUS"].ToString()));
                    chkExaminer7.Enabled = Convert.ToBoolean(Convert.ToInt32(dtr["EXAMINER7STATUS"] == "0" ? "0" : dtr["EXAMINER7STATUS"].ToString()));
                    chkExaminer8.Enabled = Convert.ToBoolean(Convert.ToInt32(dtr["EXAMINER8STATUS"] == "0" ? "0" : dtr["EXAMINER8STATUS"].ToString()));
                    chkExaminer9.Enabled = Convert.ToBoolean(Convert.ToInt32(dtr["EXAMINER9STATUS"] == "0" ? "0" : dtr["EXAMINER9STATUS"].ToString()));
                    chkExaminer10.Enabled = Convert.ToBoolean(Convert.ToInt32(dtr["EXAMINER10STATUS"] == "0" ? "0" : dtr["EXAMINER10STATUS"].ToString()));
                }

                //  supervisior viva confirm 
                //if (ViewState["usertype"].ToString() == "3" && ddlSupervisor.SelectedValue == Session["userno"].ToString() && SupStatus == 1 && CoSupStatus == 1 && chkdrcstatus == 1 && DeanStatus == 1 && DeanApprovalStatus == 1 && DeanExternalStatus == 1 && Vivaconfirm == 0)
                if (ViewState["usertype"].ToString() == "3" && hfSupervisorno.Value == Session["userno"].ToString() && SupStatus == 1 && CoSupStatus == 1 && chkdrcstatus == 1 && DeanStatus == 1 && DeanApprovalStatus == 1 && DeanExternalStatus == 1 && Vivaconfirm == 0)
                {
                    div1.Visible = true;
                    divConfirm.Visible = false;
                    trViva.Visible = true;
                    btnSubmit.Enabled = true;
                }
                if (SupStatus == 1 && CoSupStatus == 1 && chkdrcstatus == 1 && DeanStatus == 1 && DeanApprovalStatus == 1 && Vivaconfirm == 1)
                {
                    btnSubmit.Visible = false;
                    btnReject.Visible = false;
                    btnApply.Visible = true;
                    btnAppoint.Visible = true;
                    btnReport.Visible = true;
                    btnExternalReport.Visible = true;
                    trViva.Visible = true;
                }
                if (SupStatus == 0 && CoSupStatus == 0 && chkdrcstatus == 0 && DeanStatus == 0 && DeanApprovalStatus == 0 && RejectRemark != string.Empty)
                {
                    divremark.Visible = true;
                }
                if (SupStatus == 1 && CoSupStatus == 1 && chkdrcstatus == 1 && DeanStatus == 1 && DeanApprovalStatus == 1 && Vivaconfirm == 1)
                {
                    btnExternalReport.Visible = true;
                }

                if (ViewState["usertype"].ToString() == "1" && Vivaconfirm == 0)
                {
                    trViva.Visible = true;
                    btnSubmit.Enabled = true;
                    chkExaminer1.Enabled = false;
                    ddlExaminer1.Enabled = true;
                    ddlExaminer2.Enabled = false;
                    ddlExaminer3.Enabled = false;
                    ddlExaminer4.Enabled = false;
                    ddlExaminer5.Enabled = false;
                    ddlExaminer6.Enabled = false;
                    ddlExaminer7.Enabled = false;
                    ddlExaminer8.Enabled = false;
                    ddlExaminer9.Enabled = false;
                    ddlExaminer10.Enabled = false;
                }
                else if (ViewState["usertype"].ToString() == "1" && Vivaconfirm == 1)
                {
                    trViva.Visible = true;
                    btnSubmit.Enabled = false;
                    chkExaminer1.Enabled = false;
                    ddlExaminer1.Enabled = false;
                    ddlExaminer2.Enabled = false;
                    ddlExaminer3.Enabled = false;
                    ddlExaminer4.Enabled = false;
                    ddlExaminer5.Enabled = false;
                    ddlExaminer6.Enabled = false;
                    ddlExaminer7.Enabled = false;
                    ddlExaminer8.Enabled = false;
                    ddlExaminer9.Enabled = false;
                    ddlExaminer10.Enabled = false;

                    btnApply.Visible = true;
                    btnAppoint.Visible = true;
                    btnReport.Visible = true;
                    btnExternalReport.Visible = true;
                }

                //if (ViewState["usertype"].ToString() == "3" && ddlSupervisor.SelectedValue == Session["userno"].ToString() && SupStatus == 1 || ViewState["usertype"].ToString() == "4")
                    if (ViewState["usertype"].ToString() == "3" && hfSupervisorno.Value == Session["userno"].ToString() && SupStatus == 1 || ViewState["usertype"].ToString() == "4")
                {
                    DataSet ds = objCommon.FillDropDown("ACD_PHD_EXAMINER", "SYNOPSIS_NAME FILENAME ,SYN_PATH PATH", "IDNO", "IDNO=" + Convert.ToInt32(Request.QueryString["id"].ToString()) + " AND ISNULL(SYNOPSIS_STATUS,0) = 1", "");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lvUpload.DataSource = ds;
                        lvUpload.DataBind();
                        lvUpload.Visible = true;
                    }

                    DataSet ds1 = objCommon.FillDropDown("ACD_PHD_EXAMINER", "PRE_SYNOPSIS_NAME FILENAME ,PRE_SYN_PATH PATH", "IDNO", "IDNO=" + Convert.ToInt32(Request.QueryString["id"].ToString()) + " AND ISNULL(PRE_SYN_STATUS,0) = 1", "");
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        lvpresynopsis.DataSource = ds1;
                        lvpresynopsis.DataBind();
                        lvpresynopsis.Visible = true;
                    }
                }
                //if (ViewState["usertype"].ToString() == "3" && ddlSupervisor.SelectedValue == Session["userno"].ToString())
                    if (ViewState["usertype"].ToString() == "3" && hfSupervisorno.Value == Session["userno"].ToString())
                    {
                        divsynopsis.Visible = true;
                        divpresynopsis.Visible = true;
                        txtdatepre.Enabled = true;
                    }
                


                //if (ddlSupervisor.SelectedValue != Session["userno"].ToString() && ddlCoSupervisor.SelectedValue != Session["userno"].ToString() && ddlDRCChairman.SelectedValue != Session["userno"].ToString() && ViewState["usertype"].ToString() != "4" && ViewState["usertype"].ToString() != "1") 
                    if (hfSupervisorno.Value != Session["userno"].ToString() && hfJointsuperno.Value != Session["userno"].ToString() && hfdrcChairmanno.Value != Session["userno"].ToString() && ViewState["usertype"].ToString() != "4" && ViewState["usertype"].ToString() != "1") 
                {
                    objCommon.DisplayMessage("Your Not Eligible For This Student  !! ", this.Page);
                    pnDisplay.Visible = false;
                }
            }
            else
            {
                btnSubmit.Enabled = false;
                objCommon.DisplayMessage("Either Student Annexure-F Status is Pending OR Student is Not Eligible For Registration !! ", this.Page);
            }
        }
        else
        {
            btnSubmit.Enabled = false;
            objCommon.DisplayMessage("Either Student Annexure-F Status is Pending OR Student is Not Eligible For Registration !! ", this.Page);
        }

    }

    //private void CheckPageAuthorization()
    //{
    //    if (Request.QueryString["pageno"] != null)
    //    {
    //        //Check for Authorization of Page
    //        if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
    //        {
    //            Response.Redirect("~/notauthorized.aspx?page=PhdExaminerAllotment.aspx");
    //        }
    //    }
    //    else
    //    {
    //        //Even if PageNo is Null then, don't show the page
    //        Response.Redirect("~/notauthorized.aspx?page=PhdExaminerAllotment.aspx");
    //    }
    //}

    private void ClearControl()
    {
        lblidno.Text = string.Empty;
        txtRegNo.Text = string.Empty;
        txtEnrollno.Text = string.Empty;
        txtStudentName.Text = string.Empty;
        txtFatherName.Text = string.Empty;
    }

    private void SubmitData()
    {
        if (fuSynopsis.FileName != string.Empty && fupresynopsis.FileName != string.Empty && txtdatepre.Text != string.Empty)
        {
            SupervisiorSubmitThesis(fuSynopsis, fuSynopsis.FileName, "Synopsis");

            SupervisiorSubmitPreSynopsis(fupresynopsis, fupresynopsis.FileName, "Pre_Synopsis");
 
            PhdController objSC = new PhdController();
            Student objS = new Student();
            try
            {
                //objS.IdNo =   Convert.ToInt32(lblidno.Text);
                //objS.RollNo = txtRegNo.Text.Trim();
                //objS.PhdSupervisorNo =  Convert.ToInt32(ddlSupervisor.SelectedValue);
                //objS.DrcChairNo = Convert.ToInt32(ddlDRCChairman.SelectedValue);
                objS.IdNo = Convert.ToInt32(lblidno.Text);
                objS.RollNo = lblrollno.Text.Trim();
                objS.PhdSupervisorNo = Convert.ToInt32(hfSupervisorno.Value);
                objS.JoinsupervisorNo = Convert.ToInt32(hfJointsuperno.Value);
                objS.DrcChairNo = Convert.ToInt32(hfdrcChairmanno.Value);

                objS.PhdExaminer1 = Convert.ToInt32(ddlExaminer1.SelectedValue);
                objS.PhdExaminer2 = Convert.ToInt32(ddlExaminer2.SelectedValue);
                objS.PhdExaminer3 = Convert.ToInt32(ddlExaminer3.SelectedValue);
                objS.PhdExaminer4 = Convert.ToInt32(ddlExaminer4.SelectedValue);
                objS.PhdExaminer5 = Convert.ToInt32(ddlExaminer5.SelectedValue);
                objS.PhdExaminer6 = Convert.ToInt32(ddlExaminer6.SelectedValue);
                objS.PhdExaminer7 = Convert.ToInt32(ddlExaminer7.SelectedValue);
                objS.PhdExaminer8 = Convert.ToInt32(ddlExaminer8.SelectedValue);
                objS.PhdExaminer9 = Convert.ToInt32(ddlExaminer9.SelectedValue);
                objS.PhdExaminer10 = Convert.ToInt32(ddlExaminer10.SelectedValue);
                objS.PhdSynName = Filename;
                objS.PhdSynFile = Filepath;
                objS.PhdPresyndate = Convert.ToDateTime(txtdatepre.Text.ToString());
                objS.PhdPreSynName = Filename_presynopsis;
                objS.PhdPreSynFile = Filepath_presynopsis;
                int ExaminerNo = Convert.ToInt32(objSC.AddPhdExaminerDetailsSupervisor(objS));
                if (ExaminerNo == 1)
                {
                    objCommon.DisplayMessage("Supervisior Added Examiner Details Successfully!", this.Page);

                    //SendEmailThesis(ddlSupervisor.SelectedItem.Text.ToString(), Convert.ToInt32(ddlSupervisor.SelectedValue.ToString()), "Regarding Thesis Submission");
                    SendEmailThesis(lblSupervisor.Text.ToString(), Convert.ToInt32(hfSupervisorno.Value.ToString()), "Regarding Thesis Submission");
                  
                   // ShowStudentDetails();
                    //ClearControl();
                    DisableDropDown();

                }
                if (ExaminerNo == 2)
                {
                    objCommon.DisplayMessage("Student Already Register", this.Page);

                }
            }

            catch (Exception ex)
            {

                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "Academic_StudentInfoEntry.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");

                this.ClearControl();
            }
        }
        else 
        {
            objCommon.DisplayMessage("Please Select Pre Synopsis and Synopsis File To Upload and Enter Pre Synopsis Presentation Date ", this.Page);
        }
    }

    //  synopsis , File Upload CODE 
    private void SupervisiorSubmitThesis(FileUpload fupload, string file, string dcname)
    {
        //FileUpload fupload = new FileUpload();
        //fupload.ID = "fuEx" + id;          
        string rollno = string.Empty;
        string DOCNAME = string.Empty, FILENAME = string.Empty;
       // string path = "d:\\temp\\PHD\\SYNOPSIS\\";
        //string path = "c:\\temp\\PHD\\SYNOPSIS\\";
        string path = System.Configuration.ConfigurationManager.AppSettings["PHD_SYNOPSIS"].ToString();


        DOCNAME = dcname;
        FILENAME = fupload.FileName;
        rollno = txtEnrollno.Text.ToString().Trim();
        string FileString = rollno.ToString() + "_" + DOCNAME.ToString() + "_" + FILENAME.ToString();

        try
        {
            if (!(Directory.Exists(path)))
                Directory.CreateDirectory(path);
            if (fupload.HasFile)
            {
                string[] array1 = Directory.GetFiles(path);

                foreach (string str in array1)
                {
                    if ((path + rollno.ToString() + "_" + DOCNAME + "_" + fupload.FileName.ToString()).Equals(str))
                    {
                        File.Delete(path + FileString);
                    }
                }
                fupload.PostedFile.SaveAs(path + rollno.ToString() + "_" + DOCNAME + "_" + fupload.FileName);
                FileString = rollno.ToString() + "_" + DOCNAME.ToString() + "_" + FILENAME.ToString();
                Filename = FileString;
                Filepath = path + FileString;
            }
        }
        catch (Exception ex) { }
    }


    private void SubmitDataAdmin()
    {
        StudentController objSC = new StudentController();
        Student objS = new Student();
        try
        {
            if (ddlExaminer1.SelectedValue == "0")
            {
                objCommon.DisplayMessage("Please Select VIVA Examination Date  and VIVA External", this.Page);
                return;
            }
            else
            {
                //objS.IdNo = Convert.ToInt32(lblidno.Text);
                //objS.RollNo = txtRegNo.Text.Trim();
                //objS.PhdSupervisorNo = Convert.ToInt32(ddlSupervisor.SelectedValue);
                //objS.DrcChairNo = Convert.ToInt32(ddlDRCChairman.SelectedValue);
                objS.IdNo = Convert.ToInt32( lblidno.Text);
                objS.RollNo = lblrollno.Text.Trim();
                //objS.PhdSupervisorNo = Convert.ToInt32(hfSupervisorno.Value);
                //objS.DrcChairNo = Convert.ToInt32(hfdrcChairmanno.Value);
                objS.PhdExaminer1 = Convert.ToInt32(ddlExaminer1.SelectedValue);
                //objS.YearOfExam = txtviva.Text;

                int ExaminerNo = Convert.ToInt32(objSC.AddPhdExaminerDetailsadmin(objS));
                if (ExaminerNo == 1)
                {
                    objCommon.DisplayMessage("Admi-n Added Viva Details Successfully!", this.Page);
                    ShowStudentDetails();
                    //ClearControl();
                    DisableDropDown();

                }
                if (ExaminerNo == 2)
                {
                    objCommon.DisplayMessage("Student Already Register", this.Page);

                }
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentInfoEntry.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

            this.ClearControl();
        }
      
    }

    protected void lnkId_Click(object sender, EventArgs e)
    {
        
        LinkButton lnk = sender as LinkButton;
        string url = string.Empty;
        if (Request.Url.ToString().IndexOf("?id=") > 0)
        {
            url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("?id="));
        }
        else
        {
            url = Request.Url.ToString();
        }

        int idno = Convert.ToInt32(lnk.CommandArgument);
        Session["stuinfoidno"] = idno;
        ShowStudentDetails();
        lvStudent.Visible = false;
        pnDisplay.Visible = true;
        updEdit.Visible = false;

       // Response.Redirect(url + "?id=" + lnk.CommandArgument);

    }

    private void bindlist(string category, string searchtext)
        {
        StudentController objSC = new StudentController();
        DataSet ds = objSC.RetrieveStudentDetailsNewForPHDOnly(searchtext, category);

        if (ds.Tables[0].Rows.Count > 0)
            {
            Panellistview.Visible = true;
            // divReceiptType.Visible = false;
            //  divStudSemester.Visible = false;
            lvStudent.Visible = true;
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label - 
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
            }
        else
            {
            lblNoRecords.Text = "Total Records : 0";
            lvStudent.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            }
        }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControl();
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string IDNO = objCommon.LookUp("ACD_PHD_EXAMINER", "IDNO","");
        if (IDNO == string.Empty)
        {
            //if (ViewState["usertype"].ToString() == "3" && ddlSupervisor.SelectedValue == Session["userno"].ToString())
            if (ViewState["usertype"].ToString() == "3" && hfSupervisorno.Value == Session["userno"].ToString())
            {
                if (fuSynopsis.FileName != string.Empty && fupresynopsis.FileName != string.Empty && txtdatepre.Text != string.Empty)
                {
                    if (ddlExaminer1.SelectedValue != "0" && ddlExaminer2.SelectedValue != "0" && ddlExaminer3.SelectedValue != "0" && ddlExaminer4.SelectedValue != "0" && ddlExaminer5.SelectedValue != "0" && ddlExaminer6.SelectedValue != "0" && ddlExaminer7.SelectedValue != "0" && ddlExaminer8.SelectedValue != "0") // && ddlExaminer9.SelectedValue != "0" && ddlExaminer10.SelectedValue != "0")
                    {
                        SubmitData();

                        //if (ViewState["usertype"].ToString() == "3" && ddlSupervisor.SelectedValue == Session["userno"].ToString() && ddlDRCChairman.SelectedValue == Session["userno"].ToString() && chkdrcstatus == 0 && SupStatus == 1 && CoSupStatus == 1)
                        if (ViewState["usertype"].ToString() == "3" && hfSupervisorno.Value == Session["userno"].ToString() && hfdrcChairmanno.Value == Session["userno"].ToString() && chkdrcstatus == 0 && SupStatus == 1 && CoSupStatus == 1)
                        {
                            UpdateInformation("DRC");
                            //ShowStudentDetails();
                        }
                        ShowStudentDetails();
                    }
                    else
                    {
                        objCommon.DisplayMessage("Please Select Examiner ", this.Page);
                    }

                }
                else
                {
                    objCommon.DisplayMessage("Please Select Pre Synopsis and Synopsis File To Upload and Enter Pre Synopsis Presentation Date ", this.Page);
                }
            }
            else if (ViewState["usertype"].ToString() == "1")
            {
                SubmitDataAdmin();
            }
            else
            {
                objCommon.DisplayMessage("Your Not Supervisior Of The Student . Only This Student Supervisior Can Register first!!", this.Page);
            }
        }
        else
        {
            //if (ViewState["usertype"].ToString() == "3" && ddlSupervisor.SelectedValue == Session["userno"].ToString())
            //{
            //    objCommon.DisplayMessage("Student Already Register!!", this.Page);
            //}
            //if (ViewState["usertype"].ToString() == "3" && ddlCoSupervisor.SelectedValue == Session["userno"].ToString() && CoSupStatus == 0 && SupStatus == 1)
            if (ViewState["usertype"].ToString() == "3" && hfJointsuperno.Value == Session["userno"].ToString() && CoSupStatus == 0 && SupStatus == 1)
            {
                UpdateInformation("CS");
                //ShowStudentDetails();
            }

            //if (ViewState["usertype"].ToString() == "3" && ddlDRCChairman.SelectedValue == Session["userno"].ToString() && chkdrcstatus == 0 && SupStatus == 1 && CoSupStatus == 1)
            if (ViewState["usertype"].ToString() == "3" && hfdrcChairmanno.Value == Session["userno"].ToString() && chkdrcstatus == 0 && SupStatus == 1 && CoSupStatus == 1)
            {
                UpdateInformation("DRC");
                //ShowStudentDetails();
            }
            //else
            //{
            //    objCommon.DisplayMessage("Drcchairman Confirmation Alredy Done!!", this.Page);
            //}

            if (ViewState["usertype"].ToString() == "4" && DeanStatus == 0 && chkdrcstatus == 1 && SupStatus == 1 && CoSupStatus == 1 )
            {
                UpdateInformation("DC");

                btnReport.Visible = true;
                //ShowStudentDetails();
            }
            //else
            //{
            //    objCommon.DisplayMessage("Dean Confirmation Alredy Done!!", this.Page);
            //}


            if (ViewState["usertype"].ToString() == "4" && DeanStatus == 1 && DeanApprovalStatus == 0 && chkdrcstatus == 1 && SupStatus == 1 && CoSupStatus == 1)
            {
                if (chkExaminer1.Checked == true || chkExaminer2.Checked == true || chkExaminer3.Checked == true || chkExaminer4.Checked == true || chkExaminer5.Checked == true || chkExaminer6.Checked == true || chkExaminer7.Checked == true || chkExaminer8.Checked == true || chkExaminer9.Checked == true || chkExaminer10.Checked == true)
                {
                    UpdateDeanapproval();

                }
                else
                {
                    objCommon.DisplayMessage("Please Select Atleast Four Examiner!!", this.Page);
                }
            }
            //else
            //{
            //    objCommon.DisplayMessage("Examiner Approval Alredy Done!!", this.Page);
            //}
               //  dean external confirm 
            if (ViewState["usertype"].ToString() == "4" && SupStatus == 1 && CoSupStatus == 1  && chkdrcstatus == 1 && DeanStatus == 1 && DeanApprovalStatus == 1 && DeanExternalStatus == 0)
            {
                UpdateVivaInformationDean();
                DisableDropDown();
            }
             //  supervisor viva confirm 
            //if (ViewState["usertype"].ToString() == "3" && ddlSupervisor.SelectedValue == Session["userno"].ToString() && SupStatus == 1 && CoSupStatus == 1 && chkdrcstatus == 1 && DeanStatus == 1 && DeanApprovalStatus == 1 && DeanExternalStatus == 1 && Vivaconfirm == 0)
            if (ViewState["usertype"].ToString() == "3" && hfSupervisorno.Value == Session["userno"].ToString() && SupStatus == 1 && CoSupStatus == 1 && chkdrcstatus == 1 && DeanStatus == 1 && DeanApprovalStatus == 1 && DeanExternalStatus == 1 && Vivaconfirm == 0)
            {
                UpdateVivaInformation();
            }

            //else
            //{
            //    objCommon.DisplayMessage("Student Viva-Voice Confirmation Alredy Done!!", this.Page);
            //}

            if (SupStatus == 0 && CoSupStatus == 0 && chkdrcstatus == 0 && DeanStatus == 0 && DeanApprovalStatus == 0 && RejectRemark != string.Empty && hfSupervisorno.Value == Session["userno"].ToString())
            {
                UpdateInformation("S");
            }

            if (ViewState["usertype"].ToString() == "1" && Vivaconfirm == 1)
            {
                objCommon.DisplayMessage("Student Viva-Voice Confirmation Alredy Done!!", this.Page);
            }
            if (ViewState["usertype"].ToString() == "1")
            {
                SubmitDataAdmin();
            }
            else
            {
                objCommon.DisplayMessage("Your Not Supervisior Of The Student . Only This Student Supervisior Can Register first!!", this.Page);
            }

        }
    }

    /// <summary>
    ///  Dean Final Confirmation 
    /// </summary>
    private void UpdateDeanapproval()
    {
        PhdController objSC = new PhdController();
        Student objS = new Student();
        try
        {
            int count = 0;
            count += chkExaminer1.Checked == true ? 1 : 0;
            count += chkExaminer2.Checked == true ? 1 : 0;
            count += chkExaminer3.Checked == true ? 1 : 0;
            count += chkExaminer4.Checked == true ? 1 : 0;
            count += chkExaminer5.Checked == true ? 1 : 0;
            count += chkExaminer6.Checked == true ? 1 : 0;
            count += chkExaminer7.Checked == true ? 1 : 0;
            count += chkExaminer8.Checked == true ? 1 : 0;
            count += chkExaminer9.Checked == true ? 1 : 0;
            count += chkExaminer10.Checked == true ? 1 : 0;
            if (count == 4)
            {
                objS.IdNo = Convert.ToInt32(lblidno.Text);
                objS.PhdExaminer1Status = chkExaminer1.Checked == true ? 1 : 0;
                objS.PhdExaminer2Status = chkExaminer2.Checked == true ? 1 : 0;
                objS.PhdExaminer3Status = chkExaminer3.Checked == true ? 1 : 0;
                objS.PhdExaminer4Status = chkExaminer4.Checked == true ? 1 : 0;
                objS.PhdExaminer5Status = chkExaminer5.Checked == true ? 1 : 0;
                objS.PhdExaminer6Status = chkExaminer6.Checked == true ? 1 : 0;
                objS.PhdExaminer7Status = chkExaminer7.Checked == true ? 1 : 0;
                objS.PhdExaminer8Status = chkExaminer8.Checked == true ? 1 : 0;
                objS.PhdExaminer9Status = chkExaminer9.Checked == true ? 1 : 0;
                objS.PhdExaminer10Status = chkExaminer10.Checked == true ? 1 : 0;
                objS.IdNo = Convert.ToInt32(lblidno.Text);
                int output = Convert.ToInt32(objSC.UpdatePhdDeanApproval(objS));
                if (output == 1)
                {
                    objCommon.DisplayMessage("Dean Approve Four Examiner Successfully!", this.Page);
                    ShowStudentDetails();
                    btnSubmit.Enabled = false;

                }
                else
                {
                    objCommon.DisplayMessage("Student Already Register", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage("Please Select Only Four Examiner For Examination", this.Page);
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentInfoEntry.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

            this.ClearControl();
        }
    }


    // supervisior update viva date  -----
    private void UpdateVivaInformation()
    {
        PhdController objSC = new PhdController();
        Student objS = new Student();
        try
        {
            if (txtviva.Text == "")
            {
                objCommon.DisplayMessage("Please Select VIVA Examination Date ", this.Page);
                return;
            }
            else
            {                       
                    objS.PhdExaminer1Status = 0;
                    objS.IdNo = Convert.ToInt32(lblidno.Text);
                    objS.YearOfExam = txtviva.Text;

                    int output = Convert.ToInt32(objSC.UpdatePhdSupervisiorVivaApproval(objS));
                    if (output == 1)
                    {
                        objCommon.DisplayMessage("Supervisor Added Viva-Voice  Examination Details Successfully!", this.Page);
                        btnExternalReport.Visible = true;
                        btnSubmit.Enabled = false;
                        ShowStudentDetails();
                    }
                    else
                    {
                        objCommon.DisplayMessage("Student Already Register", this.Page);
                    }
               
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentInfoEntry.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

            this.ClearControl();
        }
    }

    //  dean viva external update --- 
    private void UpdateVivaInformationDean()
    {
        PhdController objSC = new  PhdController();
        Student objS = new Student();
        try
        {
         
                int count = 0;
                count += chkExaminer1.Checked == true ? 1 : 0;
                count += chkExaminer2.Checked == true ? 1 : 0;
                count += chkExaminer3.Checked == true ? 1 : 0;
                count += chkExaminer4.Checked == true ? 1 : 0;
                count += chkExaminer5.Checked == true ? 1 : 0;
                count += chkExaminer6.Checked == true ? 1 : 0;
                count += chkExaminer7.Checked == true ? 1 : 0;
                count += chkExaminer8.Checked == true ? 1 : 0;
                count += chkExaminer9.Checked == true ? 1 : 0;
                count += chkExaminer10.Checked == true ? 1 : 0;
                if (count == 1)
                {
                    if (chkExaminer1.Checked == true)
                    { objS.PhdExaminer1Status = Convert.ToInt32(ddlExaminer1.SelectedValue); }
                    else if (chkExaminer2.Checked == true)
                    { objS.PhdExaminer1Status = Convert.ToInt32(ddlExaminer2.SelectedValue); }
                    else if (chkExaminer3.Checked == true)
                    { objS.PhdExaminer1Status = Convert.ToInt32(ddlExaminer3.SelectedValue); }
                    else if (chkExaminer4.Checked == true)
                    { objS.PhdExaminer1Status = Convert.ToInt32(ddlExaminer4.SelectedValue); }
                    else if (chkExaminer5.Checked == true)
                    { objS.PhdExaminer1Status = Convert.ToInt32(ddlExaminer5.SelectedValue); }
                    else if (chkExaminer6.Checked == true)
                    { objS.PhdExaminer1Status = Convert.ToInt32(ddlExaminer6.SelectedValue); }
                    else if (chkExaminer7.Checked == true)
                    { objS.PhdExaminer1Status = Convert.ToInt32(ddlExaminer7.SelectedValue); }
                    else if (chkExaminer8.Checked == true)
                    { objS.PhdExaminer1Status = Convert.ToInt32(ddlExaminer8.SelectedValue); }
                    else if (chkExaminer9.Checked == true)
                    { objS.PhdExaminer1Status = Convert.ToInt32(ddlExaminer9.SelectedValue); }
                    else if (chkExaminer10.Checked == true)
                    { objS.PhdExaminer1Status = Convert.ToInt32(ddlExaminer10.SelectedValue); }
                    else
                    { objCommon.DisplayMessage("Please Select Examiner", this.Page); }
                    objS.IdNo = Convert.ToInt32(lblidno.Text);
                    objS.YearOfExam = txtviva.Text;

                    int output = Convert.ToInt32(objSC.UpdatePhdDeanVivaApproval(objS));
                    if (output == 1)
                    {
                        objCommon.DisplayMessage("Dean Added Viva-Voice Extenal  Examination Details Successfully!", this.Page);
                        //btnExternalReport.Visible = true;
                        btnSubmit.Enabled = false;
                        ShowStudentDetails();
                    }
                    else
                    {
                        objCommon.DisplayMessage("Student Already Register", this.Page);
                    }
                }
                else
                {
                    objCommon.DisplayMessage("Please Select Only One Examiner For VIVA-VOICE Examination", this.Page);
                }
            
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentInfoEntry.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

            this.ClearControl();
        }
    }

    // --- Update Supervisor Status --- 
    private void UpdateInformation(string Key)
    {
        PhdController objSC = new PhdController();
        Student objS = new Student();
        try
        {
            objS.IdNo = Convert.ToInt32(lblidno.Text);
            objS.PhdStatusValue = Key;
            objS.IdNo = Convert.ToInt32(lblidno.Text);
            int output = Convert.ToInt32(objSC.UpdateExaminerStatus(objS));
            if (output == 1)
            {
                if (Key == "DRC")
                {
                    objCommon.DisplayMessage("DrcChairman Confirm Examiner Successfully!", this.Page);
                    btnSubmit.Enabled = false;
                }
                if (Key == "DC")
                {
                    objCommon.DisplayMessage(" Dean Confirm Examiner Successfully!", this.Page);
                    btnSubmit.Enabled = false;
                    btnReject.Visible = false;
                }
                if (Key == "S")
                {
                    objCommon.DisplayMessage("Supervisior Added Examiner Details Successfully!", this.Page);
                    btnSubmit.Enabled = false;
                }
                if (Key == "CS")
                {
                    objCommon.DisplayMessage("Joint Supervisior Confirm Examiner Details Successfully!", this.Page);
                    btnSubmit.Enabled = false;
                }
            }
            else
            {
                objCommon.DisplayMessage("Student Already Register", this.Page);
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentInfoEntry.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

            this.ClearControl();
        }
    }

    //  dean reject student 
    protected void btnReject_Click(object sender, EventArgs e)
    {
        PhdController objSC = new PhdController();
        Student objS = new Student();
        if (ViewState["usertype"].ToString() == "4")
        {
            if (txtRemark.Text != "")
            {
                objS.IdNo = Convert.ToInt32(lblidno.Text);
                string Remark = txtRemark.Text;
                string output = objSC.RejectDeanStatus(objS, Remark);
                if (output != "-99")
                {
                    objCommon.DisplayMessage("Student Rejected Successfully!!", this.Page);
                    btnSubmit.Enabled = false;
                }
            }
            else
            {
                objCommon.DisplayMessage("Please Insert Remark for Cancellation from Dean!!", this.Page);
            }
        }
        else
        {
            objCommon.DisplayMessage("You are not Authorized for Reject !!", this.Page);
        }
    }

    //==== Phd Defence Report or External Report ============ 

    #region Report ===
    protected void btnExternalReport_Click(object sender, EventArgs e)
    {
        ShowReportExternal("PhdExternalExaminerReport", "rptPhdDefence.rpt");
    }

    private void ShowReportExternal(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(lblidno.Text);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updBatch, this.updBatch.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_PhdAnnexure.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    //--------------------------------------------------------------
    protected void btnApply_Click(object sender, EventArgs e)
    {
        ShowReportApplystudent("PhDStudentExaminerDeatils", "Phd_Examiner_Apply.rpt");
    }

    private void ShowReportApplystudent(string reportTitle, string rptFileName)
    {
        try
        {
            string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //  url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO= 0";
            }

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            //ScriptManager.RegisterClientScriptBlock(this.updBatch, this.updBatch.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_PhdAnnexure.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    //---------------------------------------------------------------------
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReportExaminer("PhdExaminerInformation", "rptExaminerInformation.rpt");
    }
    private void ShowReportExaminer(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(lblidno.Text);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updBatch, this.updBatch.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_PhdAnnexure.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    //---------------------------------------------------------------------   
    protected void btnAppoint_Click(object sender, EventArgs e)
    {
        ShowReportAppoint("PhdExternalReport", "rptPhdAppointment.rpt");
    }

    private void ShowReportAppoint(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(lblidno.Text);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updBatch, this.updBatch.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_PhdAnnexure.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #endregion

    //======= examiner ---- ----------

    #region Dropdown list  ====
    protected void ddlExaminer1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DataSet ds = objCommon.FillDropDown("ACD_PHD_EXAMINER_MASTER", "MOBILE", "EMAILID", "IDNO= " + Convert.ToInt32(ddlExaminer1.SelectedValue), "idno");
        DataSet ds = objCommon.FillDropDown("ACD_PHD_EXAMINER_MASTER", "Name, Department,MOBILE,EMAILID,Address", "EMAILID", "IDNO= " + Convert.ToInt32(ddlExaminer1.SelectedValue), "idno");
        if (ds.Tables[0].Rows.Count > 0)
        {
           // txtMobile1.Text = ds.Tables[0].Rows[0]["MOBILE"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["MOBILE"].ToString();
          //  txtemail1.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["EMAILID"].ToString();

            txtAllRecords.Text = ((ds.Tables[0].Rows[0]["Name"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["Name"].ToString() + ", ") +
               (ds.Tables[0].Rows[0]["Department"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["Department"].ToString() + ", ") +
               //(ds.Tables[0].Rows[0]["SPECIALIZATION"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["SPECIALIZATION"].ToString() + ", ") +
               (ds.Tables[0].Rows[0]["MOBILE"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["MOBILE"].ToString() + ", ") +
               (ds.Tables[0].Rows[0]["EMAILID"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["EMAILID"].ToString() + ", ") +
               (ds.Tables[0].Rows[0]["Address"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["Address"].ToString())
               ); 
        }

        // objCommon.FillDropDownList(ddlExaminer2, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0 and idno <>" + Convert.ToInt32(ddlExaminer1.SelectedValue), "IDNO");

    }

    protected void ddlExaminer2_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string IDNOS = ddlExaminer1.SelectedValue + "," + ddlExaminer2.SelectedValue;
      //  DataSet ds = objCommon.FillDropDown("ACD_PHD_EXAMINER_MASTER", "MOBILE", "EMAILID", "IDNO= " + Convert.ToInt32(ddlExaminer2.SelectedValue), "idno");
        DataSet ds = objCommon.FillDropDown("ACD_PHD_EXAMINER_MASTER", "Name, Department,MOBILE,EMAILID,Address", "EMAILID", "IDNO= " + Convert.ToInt32(ddlExaminer2.SelectedValue), "idno");
        if (ds.Tables[0].Rows.Count > 0)
        {
//txtMobile2.Text = ds.Tables[0].Rows[0]["MOBILE"].ToString();
  //          txtemail2.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
            txtAllRecords1.Text = ((ds.Tables[0].Rows[0]["Name"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["Name"].ToString() + ", ") +
                     (ds.Tables[0].Rows[0]["Department"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["Department"].ToString() + ", ") +
                     //(ds.Tables[0].Rows[0]["SPECIALIZATION"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["SPECIALIZATION"].ToString() + ", ") +
                     (ds.Tables[0].Rows[0]["MOBILE"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["MOBILE"].ToString() + ", ") +
                     (ds.Tables[0].Rows[0]["EMAILID"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["EMAILID"].ToString() + ", ") +
                     (ds.Tables[0].Rows[0]["Address"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["Address"].ToString())
                     ); 
        }
        //if (ddlExaminer1.Enabled == true)
        //{
        //    objCommon.FillDropDownList(ddlExaminer3, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0 AND IDNO NOT IN (" + IDNOS + ")", "NAME");
        //}
    }

    protected void ddlExaminer3_SelectedIndexChanged(object sender, EventArgs e)
    {
        // string IDNOS = ddlExaminer1.SelectedValue + "," + ddlExaminer2.SelectedValue + "," + ddlExaminer3.SelectedValue;
        DataSet ds = objCommon.FillDropDown("ACD_PHD_EXAMINER_MASTER", "Name, Department,MOBILE,EMAILID,Address", "EMAILID", "IDNO= " + Convert.ToInt32(ddlExaminer3.SelectedValue), "idno");
        if (ds.Tables[0].Rows.Count > 0)
        {
        //    txtMobile3.Text = ds.Tables[0].Rows[0]["MOBILE"].ToString();
          //  txtemail3.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
            txtAllrecords3.Text = ((ds.Tables[0].Rows[0]["Name"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["Name"].ToString() + ", ") +
                         (ds.Tables[0].Rows[0]["Department"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["Department"].ToString() + ", ") +
                         //(ds.Tables[0].Rows[0]["SPECIALIZATION"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["SPECIALIZATION"].ToString() + ", ") +
                         (ds.Tables[0].Rows[0]["MOBILE"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["MOBILE"].ToString() + ", ") +
                         (ds.Tables[0].Rows[0]["EMAILID"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["EMAILID"].ToString() + ", ") +
                         (ds.Tables[0].Rows[0]["Address"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["Address"].ToString())
                         ); 
        }

        //if (ddlExaminer1.Enabled == true)
        //{
        //    objCommon.FillDropDownList(ddlExaminer4, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0 AND IDNO NOT IN (" + IDNOS + ")", "NAME");
        //}
    }

    protected void ddlExaminer4_SelectedIndexChanged(object sender, EventArgs e)
    {
        // string IDNOS = ddlExaminer1.SelectedValue + "," + ddlExaminer2.SelectedValue + "," + ddlExaminer3.SelectedValue + "," + ddlExaminer4.SelectedValue;
        DataSet ds = objCommon.FillDropDown("ACD_PHD_EXAMINER_MASTER", "Name, Department,MOBILE,EMAILID,Address", "EMAILID", "IDNO= " + Convert.ToInt32(ddlExaminer4.SelectedValue), "idno");
        if (ds.Tables[0].Rows.Count > 0)
        {
      //      txtMobile4.Text = ds.Tables[0].Rows[0]["MOBILE"].ToString();
       //     txtemail4.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
            txtAllRecords4.Text = ((ds.Tables[0].Rows[0]["Name"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["Name"].ToString() + ", ") +
                        (ds.Tables[0].Rows[0]["Department"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["Department"].ToString() + ", ") +
                        //(ds.Tables[0].Rows[0]["SPECIALIZATION"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["SPECIALIZATION"].ToString() + ", ") +
                        (ds.Tables[0].Rows[0]["MOBILE"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["MOBILE"].ToString() + ", ") +
                        (ds.Tables[0].Rows[0]["EMAILID"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["EMAILID"].ToString() + ", ") +
                        (ds.Tables[0].Rows[0]["Address"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["Address"].ToString())
                        ); 
        }
        //if (ddlExaminer1.Enabled == true)
        //{
        //    objCommon.FillDropDownList(ddlExaminer5, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0 AND IDNO NOT IN (" + IDNOS + ")", "NAME");
        //}
    }

    protected void ddlExaminer5_SelectedIndexChanged(object sender, EventArgs e)
    {
        // string IDNOS = ddlExaminer1.SelectedValue + "," + ddlExaminer2.SelectedValue + "," + ddlExaminer3.SelectedValue + "," + ddlExaminer4.SelectedValue + "," + ddlExaminer5.SelectedValue;
        DataSet ds = objCommon.FillDropDown("ACD_PHD_EXAMINER_MASTER", "Name, Department,MOBILE,EMAILID,Address", "EMAILID", "IDNO= " + Convert.ToInt32(ddlExaminer5.SelectedValue), "idno");
        if (ds.Tables[0].Rows.Count > 0)
        {
           // txtMobile5.Text = ds.Tables[0].Rows[0]["MOBILE"].ToString();
         //   txtemail5.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
            txtAllrecords5.Text = ((ds.Tables[0].Rows[0]["Name"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["Name"].ToString() + ", ") +
                       (ds.Tables[0].Rows[0]["Department"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["Department"].ToString() + ", ") +
                       //(ds.Tables[0].Rows[0]["SPECIALIZATION"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["SPECIALIZATION"].ToString() + ", ") +
                       (ds.Tables[0].Rows[0]["MOBILE"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["MOBILE"].ToString() + ", ") +
                       (ds.Tables[0].Rows[0]["EMAILID"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["EMAILID"].ToString() + ", ") +
                       (ds.Tables[0].Rows[0]["Address"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["Address"].ToString())
                       ); 
        }
        //if (ddlExaminer1.Enabled == true)
        //{
        //    objCommon.FillDropDownList(ddlExaminer6, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0 AND IDNO NOT IN (" + IDNOS + ")", "NAME");
        //}
    }

    protected void ddlExaminer6_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string IDNOS = ddlExaminer1.SelectedValue + "," + ddlExaminer2.SelectedValue + "," + ddlExaminer3.SelectedValue + "," + ddlExaminer4.SelectedValue + "," + ddlExaminer5.SelectedValue + "," + ddlExaminer6.SelectedValue;
        DataSet ds = objCommon.FillDropDown("ACD_PHD_EXAMINER_MASTER", "Name, Department,MOBILE,EMAILID,Address", "EMAILID", "IDNO= " + Convert.ToInt32(ddlExaminer6.SelectedValue), "idno");
        if (ds.Tables[0].Rows.Count > 0)
        {
          //  txtMobile6.Text = ds.Tables[0].Rows[0]["MOBILE"].ToString();
          //  txtemail6.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
            txtAllRecords6.Text = ((ds.Tables[0].Rows[0]["Name"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["Name"].ToString() + ", ") +
                      (ds.Tables[0].Rows[0]["Department"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["Department"].ToString() + ", ") +
                      //(ds.Tables[0].Rows[0]["SPECIALIZATION"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["SPECIALIZATION"].ToString() + ", ") +
                      (ds.Tables[0].Rows[0]["MOBILE"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["MOBILE"].ToString() + ", ") +
                      (ds.Tables[0].Rows[0]["EMAILID"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["EMAILID"].ToString() + ", ") +
                      (ds.Tables[0].Rows[0]["Address"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["Address"].ToString())
                      ); 
        }
        //if (ddlExaminer1.Enabled == true)
        //{
        //    objCommon.FillDropDownList(ddlExaminer7, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0 AND IDNO NOT IN (" + IDNOS + ")", "NAME");
        //}
    }

    protected void ddlExaminer7_SelectedIndexChanged(object sender, EventArgs e)
    {
        // string IDNOS = ddlExaminer1.SelectedValue + "," + ddlExaminer2.SelectedValue + "," + ddlExaminer3.SelectedValue + "," + ddlExaminer4.SelectedValue + "," + ddlExaminer5.SelectedValue + "," + ddlExaminer6.SelectedValue + "," + ddlExaminer7.SelectedValue;
        DataSet ds = objCommon.FillDropDown("ACD_PHD_EXAMINER_MASTER", "Name, Department,MOBILE,EMAILID,Address", "EMAILID", "IDNO= " + Convert.ToInt32(ddlExaminer7.SelectedValue), "idno");
        if (ds.Tables[0].Rows.Count > 0)
        {
         //   txtMobile7.Text = ds.Tables[0].Rows[0]["MOBILE"].ToString();
          //  txtemail7.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
            txtAllrecords7.Text = ((ds.Tables[0].Rows[0]["Name"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["Name"].ToString() + ", ") +
                          (ds.Tables[0].Rows[0]["Department"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["Department"].ToString() + ", ") +
                          //(ds.Tables[0].Rows[0]["SPECIALIZATION"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["SPECIALIZATION"].ToString() + ", ") +
                          (ds.Tables[0].Rows[0]["MOBILE"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["MOBILE"].ToString() + ", ") +
                          (ds.Tables[0].Rows[0]["EMAILID"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["EMAILID"].ToString() + ", ") +
                          (ds.Tables[0].Rows[0]["Address"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["Address"].ToString())
                          ); 
        }//if (ddlExaminer1.Enabled == true)
        //{
        //    objCommon.FillDropDownList(ddlExaminer8, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0 AND IDNO NOT IN (" + IDNOS + ")", "NAME");
        //}
    }

    protected void ddlExaminer8_SelectedIndexChanged(object sender, EventArgs e)
    {
        // string IDNOS = ddlExaminer1.SelectedValue + "," + ddlExaminer2.SelectedValue + "," + ddlExaminer3.SelectedValue + "," + ddlExaminer4.SelectedValue + "," + ddlExaminer5.SelectedValue + "," + ddlExaminer6.SelectedValue + "," + ddlExaminer7.SelectedValue + "," + ddlExaminer8.SelectedValue;
        DataSet ds = objCommon.FillDropDown("ACD_PHD_EXAMINER_MASTER", "Name, Department,MOBILE,EMAILID,Address", "EMAILID", "IDNO= " + Convert.ToInt32(ddlExaminer8.SelectedValue), "idno");
        if (ds.Tables[0].Rows.Count > 0)
        {
         //   txtMobile8.Text = ds.Tables[0].Rows[0]["MOBILE"].ToString();
          //  txtemail8.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
            txtAllRecords8.Text = ((ds.Tables[0].Rows[0]["Name"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["Name"].ToString() + ", ") +
                        (ds.Tables[0].Rows[0]["Department"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["Department"].ToString() + ", ") +
                        //(ds.Tables[0].Rows[0]["SPECIALIZATION"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["SPECIALIZATION"].ToString() + ", ") +
                        (ds.Tables[0].Rows[0]["MOBILE"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["MOBILE"].ToString() + ", ") +
                        (ds.Tables[0].Rows[0]["EMAILID"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["EMAILID"].ToString() + ", ") +
                        (ds.Tables[0].Rows[0]["Address"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["Address"].ToString())
                        ); 
        }//if (ddlExaminer1.Enabled == true)
        //{
        //    objCommon.FillDropDownList(ddlExaminer9, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0 AND IDNO NOT IN (" + IDNOS + ")", "NAME");
        //}
    }

    protected void ddlExaminer9_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string IDNOS = ddlExaminer1.SelectedValue + "," + ddlExaminer2.SelectedValue + "," + ddlExaminer3.SelectedValue + "," + ddlExaminer4.SelectedValue + "," + ddlExaminer5.SelectedValue + "," + ddlExaminer6.SelectedValue + "," + ddlExaminer7.SelectedValue + "," + ddlExaminer8.SelectedValue + "," + ddlExaminer9.SelectedValue;
        DataSet ds = objCommon.FillDropDown("ACD_PHD_EXAMINER_MASTER", "MOBILE", "EMAILID", "IDNO= " + Convert.ToInt32(ddlExaminer9.SelectedValue), "idno");
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtMobile9.Text = ds.Tables[0].Rows[0]["MOBILE"].ToString();
            txtemail9.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
        }//if (ddlExaminer1.Enabled == true)
        //{
        //    objCommon.FillDropDownList(ddlExaminer10, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0 AND IDNO NOT IN (" + IDNOS + ")", "NAME");
        //}
    }

    protected void ddlExaminer10_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = objCommon.FillDropDown("ACD_PHD_EXAMINER_MASTER", "MOBILE", "EMAILID", "IDNO= " + Convert.ToInt32(ddlExaminer10.SelectedValue), "idno");
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtMobile10.Text = ds.Tables[0].Rows[0]["MOBILE"].ToString();
            txtemail10.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
        }
    }

    #endregion

    #region  ========= Download File =======// 

    protected void lnkDownloadDoc_Click(object sender, EventArgs e)
    {
        ListViewDataItem item = (ListViewDataItem)(sender as Control).NamingContainer;

        HiddenField hdfFilename = (HiddenField)item.FindControl("hdfFilename");
        LinkButton lnkbtndoc = (LinkButton)item.FindControl("lnkDownloadDoc");

        string FILENAME = string.Empty;

        FILENAME = lnkbtndoc.Text.ToString();

        string filePath = hdfFilename.Value.ToString().Trim();

        FileStream Writer = null;
        //-------
        //string[] array1 = Directory.GetFiles(filePath);
        //foreach (string str in array1)
        //{
        //    filePath = filePath;
        //}

       // filePath = filePath;
        lnkbtndoc.ToolTip = filePath;
        DownloadFile(filePath);
        Writer = File.Open(filePath, FileMode.Append, FileAccess.Write, FileShare.None);
        Writer.Close();

    }

    public void DownloadFile(string filePath1)
    {
        try
        {

            FileStream sourceFile = new FileStream((filePath1), FileMode.Open);
            long fileSize = sourceFile.Length;
            byte[] getContent = new byte[(int)fileSize];
            sourceFile.Read(getContent, 0, (int)sourceFile.Length);
            sourceFile.Close();

            Response.Clear();
            Response.BinaryWrite(getContent);
            Response.ContentType = GetResponseType(filePath1.Substring(filePath1.IndexOf('.')));
            Response.AddHeader("content-disposition", "attachment; filename=" + filePath1);
        }
        catch (Exception ex)
        {
            Response.Clear();
            Response.ContentType = GetResponseType(filePath1.Substring(filePath1.IndexOf('.')));
            Response.Write("Unable to download the attachment.");
        }
    }

    private string GetResponseType(string fileExtension)
    {
        string ret = string.Empty;
        switch (fileExtension.ToLower())
        {
            case ".doc":
                ret = "application/vnd.ms-word";
                break;

            case ".docx":
                ret = "application/vnd.ms-word";
                break;

            case ".wps":
                ret = "application/ms-excel";
                break;

            case ".jpeg":
                ret = "image/jpeg";
                break;

            case ".gif":
                ret = "image/gif";
                break;

            case ".png":
                ret = "image/png";
                break;

            case ".bmp":
                ret = "image/bmp";
                break;

            case ".tiff":
                ret = "image/tiff";
                break;

            case ".ico":
                ret = "image/x-icon";
                break;
            case ".txt":
                ret = "text/plain";
                break;

            case ".pdf":
                ret = "application/pdf";
                break;

            case ".jpg":
                ret = "image/jpg";
                break;

            case "":
                ret = "";
                break;

            default:
                ret = "";
                break;
        }
        return ret;
    }

    //=============== SEND MAIL For THESIS submition --------
    public void SendEmailThesis( string ExaminerName, int ExaminerNo, string SUB)
    {
        try
        {
            string MSG = string.Empty;
            string Emailid = objCommon.LookUp("USER_ACC", "UA_EMAIL", "UA_NO= " + ExaminerNo + " AND UA_TYPE = 3");
            string ua_email = objCommon.LookUp("USER_ACC", "UA_EMAIL", "UA_NO= 13754 AND UA_TYPE = 4");
            string EmailTemplate = "<html><body>" +
                         "<div align=\"center\">" +
                         "<table style=\"width:602px;border:#DB0F10 3px solid\" cellspacing=\"0\" cellpadding=\"0\">" +
                          "<tr>" +
                          "<td>" + "</tr>" +
                          "<tr>" +
                         "<td width=\"100%\" style=\"vertical-align:top;text-align:left;padding:20px 15px 20px 15px;height:200px;FONT-FAMILY: Verdana;FONT-SIZE: 12px\">#content</td>" +
                         "</tr>" +
                         "<tr>" +
                         "<td width=\"100%\" style=\"vertical-align:middle;text-align:left;padding:20px 15px 20px 15px;height:100px;FONT-FAMILY: Verdana;FONT-SIZE: 11px\"><b>Dean Academics<br/>NIT Raipur<br/><br/>Note: You are requested to communicate your responses at dean mail id.</td>" +
                         "</tr>" +
                         "</table>" +
                         "</div>" +
                         "</body></html>";
            System.Text.StringBuilder mailBody = new System.Text.StringBuilder();
            mailBody.AppendFormat("<h1>Greetings from NIT Raipur!!</h1>");
            mailBody.AppendFormat("Dear  " + ExaminerName + " ,");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("<p> This is for your kind information that you have submitted the synopsis of (Name :" + txtStudentName.Text + " , Roll No. : " + txtRegNo.Text + " , Department :" + ddlDepatment.SelectedItem.Text + " ) on Date : "+ DateTime.Today.Date.ToShortDateString() +". </p>");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("<p>You are further required to submit the thesis of above candidate on or before Date (" + DateTime.Today.Date.AddMonths(6).ToShortDateString() + ") without fail.</p>");
            mailBody.AppendFormat("<br />");          
            mailBody.AppendFormat("<P>Thank You <P/>");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("<P>With regards, <P/>");

            string Mailbody = mailBody.ToString();
            string nMailbody = EmailTemplate.Replace("#content", Mailbody);
            MailMessage msg = new MailMessage();
            var fromAddress = "noreply.mis@nitrr.ac.in";
            msg.From = new MailAddress(fromAddress, "NITRAIPUR");
            msg.From = new MailAddress(HttpUtility.HtmlEncode("noreply.mis@nitrr.ac.in"));
            msg.To.Add(Emailid);
            msg.Body = nMailbody;
            // msg.Attachments.Add(new Attachment("d:\\temp\\PHD\\REPORT\\" + ExaminerName + "_AppointmentLetter" + ".pdf"));
           // msg.Attachments.Add(new Attachment("d:\\temp\\PHD\\DOCUMENTS\\" + ExaminerFile));
            msg.IsBodyHtml = true;
            msg.CC.Add(ua_email);
            msg.Subject = SUB;
            SmtpClient smt = new SmtpClient("smtp.googlemail.com");
            smt.Port = 587;
            smt.Credentials = new NetworkCredential(HttpUtility.HtmlEncode("noreply.mis@nitrr.ac.in"), HttpUtility.HtmlEncode("mis@nitraipur"));
            //smt.UseDefaultCredentials = false;

            //---------------for certificate validation error solution---//
            ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
            //----------------------------------------------------------------//
            //---
            //Add this line to bypass the certificate validation
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,
                    System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                    System.Security.Cryptography.X509Certificates.X509Chain chain,
                    System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            //--
            smt.EnableSsl = true;
            smt.Send(msg);
            string script = "<script>alert('Mail Sent Successfully')</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "mailSent", script);
            //msg.Attachments.Dispose();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "AdmitCard.btnSendEmail_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
  

    #endregion

    //  Pre Synopsis File Upload CODE 
    private void SupervisiorSubmitPreSynopsis(FileUpload fupload, string file, string dcname)
    {
        //FileUpload fupload = new FileUpload();
        //fupload.ID = "fuEx" + id;          
        string rollno = string.Empty;
        string DOCNAME = string.Empty, FILENAME = string.Empty;
        //string path = "d:\\temp\\PHD\\PRE_SYNOPSIS\\";
        //string path = "c:\\temp\\PHD\\PRE_SYNOPSIS\\";
        string path = System.Configuration.ConfigurationManager.AppSettings["PHD_PRE_SYNOPSIS"].ToString();


        DOCNAME = dcname;
        FILENAME = fupload.FileName;
        rollno = txtEnrollno.Text.ToString().Trim();
        string FileString = rollno.ToString() + "_" + DOCNAME.ToString() + "_" + FILENAME.ToString();

        try
        {
            if (!(Directory.Exists(path)))
                Directory.CreateDirectory(path);
            if (fupload.HasFile)
            {
                string[] array1 = Directory.GetFiles(path);

                foreach (string str in array1)
                {
                    if ((path + rollno.ToString() + "_" + DOCNAME + "_" + fupload.FileName.ToString()).Equals(str))
                    {
                        File.Delete(path + FileString);
                    }
                }
                fupload.PostedFile.SaveAs(path + rollno.ToString() + "_" + DOCNAME + "_" + fupload.FileName);
                FileString = rollno.ToString() + "_" + DOCNAME.ToString() + "_" + FILENAME.ToString();
                Filename_presynopsis = FileString;
                Filepath_presynopsis = path + FileString;
            }
        }
        catch (Exception ex) { }
    }


    //===  not use  == //

    #region no use  ----//
    private void CheckActivity()
    {
        string sessionno = string.Empty;

        sessionno = ViewState["Sessionexam"].ToString();

        ActivityController objActController = new ActivityController();
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
        {
            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin!!", this.Page);
                pnDisplay.Visible = false;

            }

            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                pnDisplay.Visible = false;

                if (ViewState["usertype"].ToString() == "3")
                {
                    pnDisplay.Visible = true;
                    btnReject.Visible = false;
                    btnReject.Enabled = false;

                    ControlActivityOFF();
                }
                if (ViewState["usertype"].ToString() == "2")
                {
                    // === add to find annexure A register or not  ==>  added by dipali on 23072018 
                    string ActiveIdno = objCommon.LookUp("ACD_PHD_DGC", "IDNO", "IDNO=" + Convert.ToInt32(Convert.ToInt32(Session["idno"].ToString())));
                    if (ActiveIdno != string.Empty)
                    {
                        pnDisplay.Visible = true;
                        ControlActivityOFF();
                    }
                }
            }
        }
        else
        {
            // objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            objCommon.DisplayMessage("this Activity has been Stopped. So You Can View Details and Download Certificate .", this.Page);
            pnDisplay.Visible = false;

            if (ViewState["usertype"].ToString() == "3")
            {
                pnDisplay.Visible = true;
                // ddlDGCSupervisor.Enabled = ddlJointSupervisor.Enabled = ddlInstFac.Enabled = ddlDRC.Enabled = ddlDRCChairman.Enabled = ddlJointSupervisor.Enabled = false;
                // ddlCoSupervisor.Enabled = ddlCoSupervisor.Enabled = ddlInstFac.Enabled = ddlJointSupervisorSecond.Enabled = false;
                ControlActivityOFF();
            }
            if (ViewState["usertype"].ToString() == "2")
            {
                // === add to find annexure A register or not  ==>  added by dipali on 23072018 
                string ActiveIdno = objCommon.LookUp("ACD_PHD_DGC", "IDNO", "IDNO=" + Convert.ToInt32(Convert.ToInt32(Session["idno"].ToString())));
                if (ActiveIdno != string.Empty)
                {
                    pnDisplay.Visible = true;
                    ControlActivityOFF();
                }
            }
        }
        dtr.Close();
    }

    public void ControlActivityOFF()
    {
        txtTotCredits.Enabled = ddlAdmBatch.Enabled = ddlStatus.Enabled = txtResearch.Enabled = txtStudentName.Enabled = txtDateOfJoining.Enabled = false;
        txtFatherName.Enabled = ddlStatusCat.Enabled = ddlDepatment.Enabled = ddlSupervisor.Enabled = ddlSupervisorrole.Enabled = btnSubmit.Enabled = false;
        btnReject.Enabled = false;
    }
    #endregion


    //added by pooja
    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
        try
            {
            //Panel3.Visible = false;
            lblNoRecords.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            if (ddlSearch.SelectedIndex > 0)
                {
                DataSet ds = objCommon.GetSearchDropdownDetails(ddlSearch.SelectedItem.Text);
                if (ds.Tables[0].Rows.Count > 0)
                    {
                    string ddltype = ds.Tables[0].Rows[0]["CRITERIATYPE"].ToString();
                    string tablename = ds.Tables[0].Rows[0]["TABLENAME"].ToString();
                    string column1 = ds.Tables[0].Rows[0]["COLUMN1"].ToString();
                    string column2 = ds.Tables[0].Rows[0]["COLUMN2"].ToString();
                    if (ddltype == "ddl")
                        {
                        pnltextbox.Visible = false;
                        txtSearch.Visible = false;
                        pnlDropdown.Visible = true;

                        divtxt.Visible = false;
                        lblDropdown.Text = ddlSearch.SelectedItem.Text;


                        objCommon.FillDropDownList(ddlDropdown, tablename, column1, column2, column1 + ">0", column1);


                        //if(ddlSearch.SelectedItem.Text.Equals("BRANCH"))
                        //{
                        //    objCommon.FillDropDownList(ddlDropdown, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_BRANCH B ON(CDB.BRANCHNO = B.BRANCHNO)", "DISTINCT B.BRANCHNO", "B.LONGNAME", "B.BRANCHNO>0 AND CDB.OrganizationId =" + Convert.ToInt32(Session["OrgId"]), "B.BRANCHNO");
                        //}
                        //else if(ddlSearch.SelectedItem.Text.Equals("SEMESTER"))
                        //{
                        //    objCommon.FillDropDownList(ddlDropdown, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
                        //}
                        }
                    else
                        {
                        pnltextbox.Visible = true;
                        divtxt.Visible = true;
                        txtSearch.Visible = true;
                        pnlDropdown.Visible = false;

                        }
                    }
                }
            else
                {

                pnltextbox.Visible = false;
                pnlDropdown.Visible = false;

                }
            }
        catch
            {
            throw;
            }
        txtSearch.Text = string.Empty;
        }
    //private void bindliststudent(string category, string searchtext)
    //{

    //    StudentController objSC = new StudentController();
    //    DataSet ds = objSC.RetrieveStudentDetailsNewForPHDOnly(searchtext, category);

    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        pnlLV.Visible = true;
    //        liststudent.Visible = true;
    //        liststudent.DataSource = ds;
    //        liststudent.DataBind();
    //        objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label 
    //        lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
    //    }
    //    else
    //    {
    //        lblNoRecords.Text = "Total Records : 0";
    //        liststudent.Visible = false;
    //        liststudent.DataSource = null;
    //        liststudent.DataBind();
    //    }
    //}

    protected void btnsearchstu_Click(object sender, EventArgs e)
    {
        lblNoRecords.Visible = true;
        string value = string.Empty;
        if (ddlDropdown.SelectedIndex > 0)
        {
            value = ddlDropdown.SelectedValue;
        }
        else
        {
            value = txtSearch.Text;
        }



       // bindliststudent(ddlSearch.SelectedItem.Text, value);
        ddlDropdown.ClearSelection();
       // txtsearchstu.Text = string.Empty;
    }
  
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }


    protected void btnsearch_Click(object sender, EventArgs e)
        {

        }
    protected void btnClose_Click1(object sender, EventArgs e)
        {

        }
}



