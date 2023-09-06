//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : PHD TRACKER                                                     
// CREATION DATE : 04-MAR-2019                                                        
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
using System.Drawing;
//--add crystalreport code
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Text;
using System.Xml.Linq;
using System.Net.Mail;
using System.Net.Security;
using System.Net;


public partial class Academic_PhdExaminerAllotment : System.Web.UI.Page
{
    // --- Common Object  declaration 
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    GridView GV = new GridView();

    // ----  Status Details  Variable Declaration --- 
    static int DeanAccept = 0; static int DeanApproval = 0; static int DeanReject = 0; static int DeanThsSub = 0; static int DeanThsApp = 0; static int DrctApproval = 0;
    static int DeanThsReject = 0;
    string ua_dept = string.Empty; static string RejectRemark = string.Empty; string ua_email = string.Empty;

    public int Ex_Acc1 = 0, Ex_Acc2 = 0, Ex_Acc3 = 0, Ex_Acc4 = 0; // -- Examiner Accept
    public int Ex_App1 = 0, Ex_App2 = 0, Ex_App3 = 0, Ex_App4 = 0;  //-- Examiner Approval 
    public int Ex_Rej1 = 0, Ex_Rej2 = 0, Ex_Rej3 = 0, Ex_Rej4 = 0;  // -- Examiner Reject
    public int Ex_Ths1 = 0, Ex_Ths2 = 0, Ex_Ths3 = 0, Ex_Ths4 = 0; // -- Examiner Thesis Approval 
    public int Ex_SbThs1 = 0, Ex_SbThs2 = 0, Ex_SbThs3 = 0, Ex_SbThs4 = 0;  // Examiner Thesis Submit 
    public int Ex_Final1 = 0, Ex_Final2 = 0, Ex_Final3 = 0, Ex_Final4 = 0;  // EXaminer Final Approval 
    public int Ex_ThsRej1 = 0, Ex_ThsRej2 = 0, Ex_ThsRej3 = 0, Ex_ThsRej4 = 0; // Examiner Thesis Reject 
    public static int AccCount = 0, AppCount = 0, ThsAppCount = 0, ThsSubCount = 0;
        public static int SupThsStatus = 0;
   // public string Pr_Ex1 = string.Empty ,Pr_Ex2 = string.Empty, Pr_Ex3 = string.Empty, Pr_Ex4 = string.Empty;

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
               // CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //**************************************
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                ViewState["usertype"] = ua_type;

                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                //ViewState["ipAddress"] = GetUserIPAddress();
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                //Populate all the DropDownLists
                FillDropDown();
                this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME", "ID > 0 ", "SRNO");
                ddlSearch.SelectedIndex = 0;

                if (ViewState["action"] == null)
                    ViewState["action"] = "add";

                ViewState["usertype"] = ua_type;



                if (ViewState["usertype"].ToString() == "1")
                {
                    pnlsearch.Visible = true;
                    dvgeneral.Visible = false;
                    //dvdetails.Visible = true;
                    divGeneralInfo.Visible = false;
                    pnldetails.Visible = false;
                    //dvstudentdetails.Visible = false;
                    dvexaminerdetails.Visible = false;
                    dvbuttons.Visible = false;


                }
                if (ViewState["usertype"].ToString() == "3")
                {
                    DisableDropDown();
                }
                else
                {
                    string ua_type_fac = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                    if (ua_type_fac == "4")
                    {
                        // DisableDropDown();
                    }
                }
                if (Request.QueryString["id"] != null)
                {
                    ViewState["action"] = "edit";
                    ShowStudentDetails();
                }

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
                    //txtSearch.Text = string.Empty;
                    //lvStudent.DataSource = null;
                    //lvStudent.DataBind();
                    //lblNoRecords.Text = string.Empty;
                }
            }
        }
        if (Page.Request.Params["__EVENTTARGET"] != null)
        {
            if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnsearchstu"))
            {
                string[] arg = Page.Request.Params["__EVENTARGUMENT"].ToString().Split(',');
                bindliststudent(arg[0], arg[1]);
            }

            if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnClose"))
            {
                // lblMsg.Text = string.Empty;

            }
        }


        divMsg.InnerHtml = string.Empty;
        ddlNdgc.Enabled = false;
    }

    // IPADRESS  DETAILS ..

    #region Genral Method

    //private string GetUserIPAddress()
    //{
    //    string User_IPAddress = string.Empty;
    //    string User_IPAddressRange = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
    //    if (string.IsNullOrEmpty(User_IPAddressRange))//without Proxy detection
    //    {
    //        User_IPAddress = Request.ServerVariables["REMOTE_ADDR"];

    //    }
    //    else////with Proxy detection
    //    {
    //        string[] splitter = { "," };
    //        string[] IP_Array = User_IPAddressRange.Split(splitter, System.StringSplitOptions.None);

    //        int LatestItem = IP_Array.Length - 1;

    //    }
    //    return User_IPAddress;
    //}

    public void DisableDropDown()
    {
        ddlExaminer1.Enabled = ddlExaminer2.Enabled = ddlExaminer3.Enabled = ddlExaminer4.Enabled = false;

        chkExaminer1.Enabled = chkExaminer2.Enabled = chkExaminer3.Enabled = chkExaminer4.Enabled = false;
    }

    public void CheckEnable()
    {
        chkExaminer1.Enabled = chkExaminer2.Enabled = chkExaminer3.Enabled = chkExaminer4.Enabled = true;
    }

    private void FillDropDown()
    {
        try
        {
            string ua_dept = objCommon.LookUp("User_Acc", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(Session["userno"]));
            //objCommon.FillDropDownList(ddlDepatment, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO=6", "BRANCHNO");

         
            objCommon.FillDropDownList(ddlExaminer1, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0", "NAME");
            objCommon.FillDropDownList(ddlExaminer2, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0", "NAME");
            objCommon.FillDropDownList(ddlExaminer3, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0", "NAME");
            objCommon.FillDropDownList(ddlExaminer4, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0", "NAME");

            //objCommon.FillDropDownList(ddlSupervisor, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO>0 AND UA_TYPE = 3 AND (DRCSTATUS='S' OR DRCSTATUS='SD')", "ua_fullname");
            //objCommon.FillDropDownList(ddlCoSupervisor, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO>0 AND UA_TYPE = 3 AND (DRCSTATUS='S' OR DRCSTATUS='SD')", "ua_fullname");
            //objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>10", "BATCHNO");
            //objCommon.FillDropDownList(ddlStatusCat, "ACD_PHDSTATUS_CATEGORY", "PHDSTATAUSCATEGORYNO", "PHDSTATAUSCATEGRYNAME", "PHDSTATAUSCATEGORYNO > 0", "PHDSTATAUSCATEGORYNO");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_PhdAnnexure.FillDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //private void CheckPageAuthorization()
    //{
    //    if (Request.QueryString["pageno"] != null)
    //    {
    //        //Check for Authorization of Page
    //        if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
    //        {
    //            Response.Redirect("~/notauthorized.aspx?page=PhdAnnexure.aspx");
    //        }
    //    }
    //    else
    //    {
    //        //Even if PageNo is Null then, don't show the page
    //        Response.Redirect("~/notauthorized.aspx?page=PhdAnnexure.aspx");
    //    }
    //}
    
    #endregion
    
    #region ButtonEvent and Methods

    //----------  Show Student Detils --- 
    private void ShowStudentDetails()
    {
        PhdController objSC = new PhdController();
        DataTableReader dtr = null;

        dtr = objSC.GetPHDTrackerDetails(Convert.ToInt32(Session["stuinfoidno"].ToString()));
        if (dtr != null)
        {
            if (dtr.Read())
            {
                //txtIDNo.Text = dtr["IDNO"].ToString();
                txtRegNo.ToolTip = dtr["IDNO"].ToString();
                txtRegNo.Text = dtr["IDNO"].ToString();
                txtEnrollno.ToolTip = dtr["ROLLNO"].ToString();
                txtEnrollno.Text = dtr["ENROLLNO"].ToString();
                //txtRegNo.Text = dtr["ROLLNO"].ToString();
                txtStudentName.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                txtFatherName.Text = dtr["FATHERNAME"] == null ? string.Empty : dtr["FATHERNAME"].ToString().ToUpper();
                txtDateOfJoining.Text = dtr["ADMDATE"] == DBNull.Value ? "" : Convert.ToDateTime(dtr["ADMDATE"]).ToString("dd/MM/yyyy");
                ddlDepatment.SelectedValue = dtr["BRANCHNO"] == null ? "0" : dtr["BRANCHNO"].ToString();
                ddlStatus.SelectedValue = dtr["PHDSTATUS"] == null ? "0" : dtr["PHDSTATUS"].ToString();
                ddlAdmBatch.SelectedValue = dtr["ADMBATCH"] == null ? "0" : dtr["ADMBATCH"].ToString();
                //txtTotCredits.Text =Convert.ToDecimal(dtr["CREDITS"]).ToString();
                ddlNdgc.SelectedValue = dtr["NOOFDGC"].ToString() == "0" ? "4" : dtr["NOOFDGC"].ToString();
               // ddlStatus.SelectedValue = dtr["FULLPART"] == null ? "0" : dtr["FULLPART"].ToString();
                //ddlSupervisorrole.SelectedValue = dtr["SUPERROLE"] == null ? "0" : dtr["SUPERROLE"].ToString();
                ddlStatusCat.SelectedValue = dtr["PHDSTATUSCAT"] == null ? "0" : dtr["PHDSTATUSCAT"].ToString();
                ddlSupervisor.SelectedValue = dtr["SUPERVISORNO"] == null ? "0" : dtr["SUPERVISORNO"].ToString();
                txtResearch.Text = dtr["RESEARCH"].ToString();
                btnSubmit.Enabled = true;

                //--------------------- Dean Status ------//              
                DeanAccept = Convert.ToInt32(dtr["DEAN_ACCEPT"] == null ? "0" : dtr["DEAN_ACCEPT"].ToString());
                DeanApproval = Convert.ToInt32(dtr["DEAN_APPROVAL"] == null ? "0" : dtr["DEAN_APPROVAL"].ToString());
                DeanThsSub = Convert.ToInt32(dtr["DEAN_THS_SUB"] == null ? "0" : dtr["DEAN_THS_SUB"].ToString());
                DeanThsApp = Convert.ToInt32(dtr["DEAN_THS_APP"] == null ? "0" : dtr["DEAN_THS_APP"].ToString());
                DrctApproval = Convert.ToInt32(dtr["DIRECTOR_APP"] == null ? "0" : dtr["DIRECTOR_APP"].ToString());
                DeanReject = Convert.ToInt32(dtr["DEAN_REJECT"] == null ? "0" : dtr["DEAN_REJECT"].ToString());
                DeanThsReject = Convert.ToInt32(dtr["DN_THS_REJECT"] == null ? "0" : dtr["DN_THS_REJECT"].ToString()); 

                //-----   Examiner details  -------------------//
                ddlExaminer1.SelectedValue = dtr["EXAMINER1"] == null ? "0" : dtr["EXAMINER1"].ToString();
                ddlExaminer2.SelectedValue = dtr["EXAMINER2"] == null ? "0" : dtr["EXAMINER2"].ToString();
                ddlExaminer3.SelectedValue = dtr["EXAMINER3"] == null ? "0" : dtr["EXAMINER3"].ToString();
                ddlExaminer4.SelectedValue = dtr["EXAMINER4"] == null ? "0" : dtr["EXAMINER4"].ToString();

                //------------------- call examiner selected index  --------------------//
                ddlExaminer1_SelectedIndexChanged(null, null);
                ddlExaminer2_SelectedIndexChanged(null, null);
                ddlExaminer3_SelectedIndexChanged(null, null);
                ddlExaminer4_SelectedIndexChanged(null, null);

                //---------------  Examiner  Accepted status ----//
                Ex_Acc1 = Convert.ToInt32(dtr["ACC_STATUS1"] == null ? "0" : dtr["ACC_STATUS1"].ToString());
                Ex_Acc2 = Convert.ToInt32(dtr["ACC_STATUS2"] == null ? "0" : dtr["ACC_STATUS2"].ToString());
                Ex_Acc3 = Convert.ToInt32(dtr["ACC_STATUS3"] == null ? "0" : dtr["ACC_STATUS3"].ToString());
                Ex_Acc4 = Convert.ToInt32(dtr["ACC_STATUS4"] == null ? "0" : dtr["ACC_STATUS4"].ToString());

                //--------------------Examiner Approval Status --------------//
                Ex_App1 = Convert.ToInt32(dtr["APP_STATUS1"] == null ? "0" : dtr["APP_STATUS1"].ToString());
                Ex_App2 = Convert.ToInt32(dtr["APP_STATUS2"] == null ? "0" : dtr["APP_STATUS2"].ToString());
                Ex_App3 = Convert.ToInt32(dtr["APP_STATUS3"] == null ? "0" : dtr["APP_STATUS3"].ToString());
                Ex_App4 = Convert.ToInt32(dtr["APP_STATUS4"] == null ? "0" : dtr["APP_STATUS4"].ToString());

                //--------------------Examiner Reject Status --------------//
                Ex_Rej1 = Convert.ToInt32(dtr["REJECT_STATUS1"] == null ? "0" : dtr["REJECT_STATUS1"].ToString());
                Ex_Rej2 = Convert.ToInt32(dtr["REJECT_STATUS2"] == null ? "0" : dtr["REJECT_STATUS2"].ToString());
                Ex_Rej3 = Convert.ToInt32(dtr["REJECT_STATUS3"] == null ? "0" : dtr["REJECT_STATUS3"].ToString());
                Ex_Rej4 = Convert.ToInt32(dtr["REJECT_STATUS4"] == null ? "0" : dtr["REJECT_STATUS4"].ToString());

                //--------------------Examiner thesis Approval Status --------------//
                Ex_Ths1 = Convert.ToInt32(dtr["THS_APP_STATUS1"] == null ? "0" : dtr["THS_APP_STATUS1"].ToString());
                Ex_Ths2 = Convert.ToInt32(dtr["THS_APP_STATUS2"] == null ? "0" : dtr["THS_APP_STATUS2"].ToString());
                Ex_Ths3 = Convert.ToInt32(dtr["THS_APP_STATUS3"] == null ? "0" : dtr["THS_APP_STATUS3"].ToString());
                Ex_Ths4 = Convert.ToInt32(dtr["THS_APP_STATUS4"] == null ? "0" : dtr["THS_APP_STATUS4"].ToString());

                //---- Examiner Thesis Submission ------------------------------//              
                Ex_SbThs1 = Convert.ToInt32(dtr["THS_SUB_STATUS1"] == null ? "0" : dtr["THS_SUB_STATUS1"].ToString());
                Ex_SbThs2 = Convert.ToInt32(dtr["THS_SUB_STATUS2"] == null ? "0" : dtr["THS_SUB_STATUS2"].ToString());
                Ex_SbThs3 = Convert.ToInt32(dtr["THS_SUB_STATUS3"] == null ? "0" : dtr["THS_SUB_STATUS3"].ToString());
                Ex_SbThs4 = Convert.ToInt32(dtr["THS_SUB_STATUS4"] == null ? "0" : dtr["THS_SUB_STATUS4"].ToString());

                //------------------ Examiner Final Approval ---------------------------//               
                Ex_Final1 = Convert.ToInt32(dtr["FINAL_APPROVAL1"] == null ? "0" : dtr["FINAL_APPROVAL1"].ToString());
                Ex_Final2 = Convert.ToInt32(dtr["FINAL_APPROVAL2"] == null ? "0" : dtr["FINAL_APPROVAL2"].ToString());
                Ex_Final3 = Convert.ToInt32(dtr["FINAL_APPROVAL3"] == null ? "0" : dtr["FINAL_APPROVAL3"].ToString());
                Ex_Final4 = Convert.ToInt32(dtr["FINAL_APPROVAL4"] == null ? "0" : dtr["FINAL_APPROVAL4"].ToString());

                //--- Examiner Thesis Rejection ---------------------------------------------//
                Ex_ThsRej1 = Convert.ToInt32(dtr["THS_REJECT_STATUS1"] == null ? "0" : dtr["THS_REJECT_STATUS1"].ToString());
                Ex_ThsRej2 = Convert.ToInt32(dtr["THS_REJECT_STATUS2"] == null ? "0" : dtr["THS_REJECT_STATUS2"].ToString());
                Ex_ThsRej3 = Convert.ToInt32(dtr["THS_REJECT_STATUS3"] == null ? "0" : dtr["THS_REJECT_STATUS3"].ToString());
                Ex_ThsRej4 = Convert.ToInt32(dtr["THS_REJECT_STATUS4"] == null ? "0" : dtr["THS_REJECT_STATUS4"].ToString());
               
                				


                //----------------Examiner Priority ----------------------------------------//                          
                txtprexaminer1.Text = (dtr["PRIORITY_EX1"] == null ? string.Empty : dtr["PRIORITY_EX1"].ToString());
                txtprExaminer2.Text = (dtr["PRIORITY_EX2"] == null ? string.Empty : dtr["PRIORITY_EX2"].ToString());
                txtprExaminer3.Text = (dtr["PRIORITY_EX3"] == null ? string.Empty : dtr["PRIORITY_EX3"].ToString());
                txtprExaminer4.Text = (dtr["PRIORITY_EX4"] == null ? string.Empty : dtr["PRIORITY_EX4"].ToString());

                //---------- Priority Enter only -------//

                if (txtprexaminer1.Text != string.Empty) { txtprexaminer1.Enabled = false; } else { txtprexaminer1.Enabled = true; }
                if (txtprExaminer2.Text != string.Empty) { txtprExaminer2.Enabled = false; } else { txtprExaminer2.Enabled = true; }
                if (txtprExaminer3.Text != string.Empty) { txtprExaminer3.Enabled = false; } else { txtprExaminer3.Enabled = true; }
                if (txtprExaminer4.Text != string.Empty) { txtprExaminer4.Enabled = false; } else { txtprExaminer4.Enabled = true; }
                
                // ---- Supervisior Thesis Status 
              SupThsStatus = Convert.ToInt32(dtr["SUPERVISIOR_THS_STATUS"] == null ? "0" : dtr["SUPERVISIOR_THS_STATUS"].ToString());

              AppCount = 0; AccCount = 0; ThsSubCount = 0; ThsAppCount = 0;

                //----examiner Approval count
                AppCount += Ex_App1 == 1 ? 1 : 0;
                AppCount += Ex_App2 == 1 ? 1 : 0;
                AppCount += Ex_App3 == 1 ? 1 : 0;
                AppCount += Ex_App4 == 1 ? 1 : 0;

                //--- examiner Accept count 
                AccCount += Ex_Acc1 == 1 ? 1 : 0;
                AccCount += Ex_Acc2 == 1 ? 1 : 0;
                AccCount += Ex_Acc3 == 1 ? 1 : 0;
                AccCount += Ex_Acc4 == 1 ? 1 : 0;

                //----- Examiner thesis Submit count 
                ThsSubCount += Ex_SbThs1 == 1 ? 1 : 0;
                ThsSubCount += Ex_SbThs2 == 1 ? 1 : 0;
                ThsSubCount += Ex_SbThs3 == 1 ? 1 : 0;
                ThsSubCount += Ex_SbThs4 == 1 ? 1 : 0;

                //--- examiner Thesis Approval Count 
                ThsAppCount += Ex_Ths1 == 1 ? 1 : 0;
                ThsAppCount += Ex_Ths2 == 1 ? 1 : 0;
                ThsAppCount += Ex_Ths3 == 1 ? 1 : 0;
                ThsAppCount += Ex_Ths4 == 1 ? 1 : 0;

                //----- External Examiner Report --
                btnExaminerReport.Visible = false;
                //  ---  dean submit 4 examiner 
                if (DeanAccept == 0)
                {
                    CheckEnable();
                    btnSubmit.Visible = true; btnApproval.Visible = false; btnReject.Visible = false; txtRemark.Enabled = false;
                }
                //  --  dean submit but not approve examiner not reject  -- 
                if (DeanAccept == 1 && DeanApproval == 0 && DeanReject == 0)
                {
                    if (AccCount >= 2)
                    {
                         CheckStatusAccepted();
                        btnSubmit.Enabled = false; btnApproval.Visible = true; btnReject.Visible = true; txtRemark.Enabled = true;
                    }
                    else 
                    {
                        btnSubmit.Enabled = true; btnApproval.Visible = true; btnReject.Visible = true; txtRemark.Enabled = true;
                    }
                    if (Ex_Acc1 == 1) { chkExaminer1.Checked = true; } else { chkExaminer1.Checked = false; }
                    if (Ex_Acc2 == 1) { chkExaminer2.Checked = true; } else { chkExaminer2.Checked = false; }
                    if (Ex_Acc3 == 1) { chkExaminer3.Checked = true; } else { chkExaminer3.Checked = false; }
                    if (Ex_Acc4 == 1) { chkExaminer4.Checked = true; } else { chkExaminer4.Checked = false; }
                
                }

                //  --  dean submit but not approve examiner but  reject one Examiner  -- 
                if (DeanAccept == 1 && DeanApproval == 0 && DeanReject == 1)
                {
                    btnSubmit.Enabled = true; btnApproval.Visible = true; btnReject.Visible = true; txtRemark.Enabled = true;

                    btnSubmit.Visible = true;
                    //divExaminer1.Visible = true;
                    //divExaminer2.Visible = true;
                    //divExaminer3.Visible = true;
                    //divExaminer4.Visible = true;

                    if (Ex_Acc1 == 1) { chkExaminer1.Checked = true; } else { chkExaminer1.Checked = false; }
                    if (Ex_Acc2 == 1) { chkExaminer2.Checked = true; } else { chkExaminer2.Checked = false; }
                    if (Ex_Acc3 == 1) { chkExaminer3.Checked = true; } else { chkExaminer3.Checked = false; }
                    if (Ex_Acc4 == 1) { chkExaminer4.Checked = true; } else { chkExaminer4.Checked = false; }

                    CheckStatusReject();
                }


                //  ---  dean approve examiner count approval status 
                if (DeanAccept == 1 && DeanApproval == 1 && DeanReject == 0 && DeanThsSub == 0)
                {
                    //sCheckStatusAccepted();

                    //chkExaminer1.Enabled = Ex_App1 == 1 ? false : true;
                    //chkExaminer2.Enabled = Ex_App2 == 1 ? false : true;
                    //chkExaminer3.Enabled = Ex_App3 == 1 ? false : true;
                    //chkExaminer4.Enabled = Ex_App4 == 1 ? false : true;

                    chkExaminer1.Checked = Ex_App1 == 1 ? true : false;
                    chkExaminer2.Checked = Ex_App2 == 1 ? true : false;
                    chkExaminer3.Checked = Ex_App3 == 1 ? true : false;
                    chkExaminer4.Checked = Ex_App4 == 1 ? true : false;

                    if (AccCount >= 2)
                    {
                        btnSubmit.Visible = false;
                        if (AppCount == 2)
                        {
                            btnThsSub.Visible = true; btnSubmit.Visible = false; btnApproval.Visible = false; btnReject.Visible = false; txtRemark.Enabled = false;
                        }
                        else
                        {
                            btnSubmit.Visible = false; btnApproval.Visible = true; btnReject.Visible = true; txtRemark.Enabled = true; txtRemark.Enabled = true;
                            btnThsSub.Visible = true;
                        }
                    }
                    else
                    {
                        //divExaminer1.Visible = true;
                        //divExaminer2.Visible = true;
                        //divExaminer3.Visible = true;
                        //divExaminer4.Visible = true;
                        btnSubmit.Visible = true; btnApproval.Visible = true; btnReject.Visible = true; txtRemark.Enabled = true; btnThsSub.Visible = true;
                    }


                }

                //-----  reject student details ---------------
                if (DeanAccept == 1 && DeanApproval == 1 && DeanReject == 1 )
                {

                    if (AppCount == 2)
                    {
                        CheckStatusAccApp();
                        btnThsSub.Visible = true; btnSubmit.Visible = false; btnApproval.Visible = false; btnReject.Visible = false; txtRemark.Enabled = false;                                         
                    }
                    else
                    {
                        //divExaminer1.Visible = true;
                        //divExaminer2.Visible = true;
                        //divExaminer3.Visible = true;
                        //divExaminer4.Visible = true;

                        CheckStatusAccApp();
                       // CheckStatusReject();

                        btnSubmit.Visible = true; btnApproval.Visible = true; btnReject.Visible = true; txtRemark.Enabled = true; btnThsSub.Visible = true;
                    }

                    if (AccCount >= 2)
                    {
                        ////  ---  chk status of 2 examiner final for synopsis 
                        //if (Ex_Acc1 == 1 || Ex_Rej1 == 1) { divExaminer1.Visible = true; } else { divExaminer1.Visible = false; }
                        //if (Ex_Acc2 == 1 || Ex_Rej2 == 1) { divExaminer2.Visible = true; } else { divExaminer2.Visible = false; }
                        //if (Ex_Acc3 == 1 || Ex_Rej3 == 1) { divExaminer3.Visible = true; } else { divExaminer3.Visible = false; }
                        //if (Ex_Acc4 == 1 || Ex_Rej4 == 1) { divExaminer4.Visible = true; } else { divExaminer4.Visible = false; }

                        btnSubmit.Visible = false;
                    }
                    else
                    {
                        //divExaminer1.Visible = true;
                        //divExaminer2.Visible = true;
                        //divExaminer3.Visible = true;
                        //divExaminer4.Visible = true;
                    }

                    CheckStatusReject();
                }
                else if (DeanAccept == 1 && DeanApproval == 1 && DeanReject == 0 && DeanThsSub == 0 && AppCount == 2)
                {
                   // CheckStatusAppReject();
                    CheckStatusAccApp();
                    btnThsSub.Visible = true; btnSubmit.Visible = false; btnApproval.Visible = false; btnReject.Visible = false; txtRemark.Enabled = false;
                }

                // ---- Thesis invitation send ...
                if (DeanAccept == 1 && DeanApproval == 1 && DeanThsSub == 1 && DeanThsApp == 0 && DeanThsReject == 0)
                {
                    if (ThsSubCount == 2)
                    {
                        btnThsApp.Visible = true; btnSubmit.Visible = false; btnApproval.Visible = false; btnReject.Visible = false; txtRemark.Enabled = true;
                        btnThsReject.Visible = true;
                        btnThsSub.Visible = false;
                        CheckStatusAppReject();
                        CheckStatusReject();
                    }
                    else
                    {
                        btnThsSub.Visible = true; btnThsApp.Visible = true; btnThsReject.Visible = true; txtRemark.Enabled = true;
                          if (AppCount == 2) { btnSubmit.Visible = false; btnReject.Visible = false; btnApproval.Visible = false; }
                          else { btnApproval.Visible = true; btnReject.Visible = true; btnSubmit.Visible = true; txtRemark.Enabled = true; if (AccCount >= 2) { btnSubmit.Visible = false; } }
                      
                    }
                                                      
                }

                // thesis  reject 
                if (DeanAccept == 1 && DeanApproval == 1 && DeanThsSub == 1 && DeanThsReject == 1 && DrctApproval == 0)
                {
                    if (Ex_ThsRej1 == 1) { lblExaminer1.ForeColor = Color.Red; chkExaminer1.Enabled = false; }
                    if (Ex_ThsRej2 == 1) { lblExaminer2.ForeColor = Color.Red; chkExaminer2.Enabled = false; }
                    if (Ex_ThsRej3 == 1) { lblExaminer3.ForeColor = Color.Red; chkExaminer3.Enabled = false; }
                    if (Ex_ThsRej4 == 1) { lblExaminer4.ForeColor = Color.Red; chkExaminer4.Enabled = false; }

                    btnThsApp.Visible = true; btnThsReject.Visible = true; txtRemark.Enabled = true;
                    if (ThsSubCount == 2)
                    {
                        btnThsApp.Visible = true; btnThsSub.Visible = false; btnSubmit.Visible = false; btnApproval.Visible = false;
                        btnReject.Visible = false; txtRemark.Enabled = false; btnThsReject.Visible = true; txtRemark.Enabled = true;
                    }
                    else
                    {
                        btnThsSub.Visible = true;
                        if (AppCount == 2) { btnSubmit.Visible = false; btnReject.Visible = false; btnApproval.Visible = false; }
                        else { btnApproval.Visible = true; btnReject.Visible = true; btnSubmit.Visible = true; txtRemark.Enabled = true; if (AccCount >= 2) { btnSubmit.Visible = false; } }
                    }    
                }
                
                //------------   Dean Thesis Approval  --- 
                if (DeanAccept == 1 && DeanApproval == 1  && DeanThsSub == 1 && DeanThsApp == 1 && DrctApproval == 0)
                {
                    if (ThsAppCount == 2)
                    {
                        if (Ex_Ths1 == 1) { chkExaminer1.Enabled = true; } else { chkExaminer1.Enabled = false; }
                        if (Ex_Ths2 == 1) { chkExaminer2.Enabled = true; } else { chkExaminer2.Enabled = false; }
                        if (Ex_Ths3 == 1) { chkExaminer3.Enabled = true; } else { chkExaminer3.Enabled = false; }
                        if (Ex_Ths4 == 1) { chkExaminer4.Enabled = true; } else { chkExaminer4.Enabled = false; } 

                        btndrctApp.Visible = true; ; btnThsApp.Visible = false; btnThsSub.Visible = false; btnSubmit.Visible = false; btnApproval.Visible = false;
                        btnReject.Visible = false; txtRemark.Enabled = false; btnThsReject.Visible = false;
                    }
                    else
                    {
                        btnThsApp.Visible = true; btnThsReject.Visible = true; txtRemark.Enabled = true;
                        if (ThsSubCount == 2)
                        {
                            btnThsApp.Visible = true; btnThsSub.Visible = false; btnSubmit.Visible = false; btnApproval.Visible = false;
                            btnReject.Visible = false; txtRemark.Enabled = false; btnThsReject.Visible = true; txtRemark.Enabled = true;
                        }
                        else
                        {
                            btnThsSub.Visible = true;
                            if (AppCount == 2) { btnSubmit.Visible = false; btnReject.Visible = false; btnApproval.Visible = false; }
                            else { btnApproval.Visible = true; btnReject.Visible = true; btnSubmit.Visible = true; txtRemark.Enabled = true; if (AccCount >= 2) { btnSubmit.Visible = false; } }
                        }                       
                    }
                    //  ---  chk status of 2 examiner final for synopsis 
                   
                }

                //-----------  Director Approval  --- 
                if (DeanAccept == 1 && DeanApproval == 1  && DeanThsSub == 1 && DeanThsApp == 1 && DrctApproval == 1)
                {
                    btnExaminerReport.Visible = true;

                    divExaminer1.Visible = Ex_Final1 == 1 ? true : false;
                    divExaminer2.Visible = Ex_Final2 == 1 ? true : false;
                    divExaminer3.Visible = Ex_Final3 == 1 ? true : false;
                    divExaminer4.Visible = Ex_Final4 == 1 ? true : false;

                    chkExaminer1.Enabled = Ex_Final1 == 1 ? false : true;
                    chkExaminer2.Enabled = Ex_Final2 == 1 ? false : true;
                    chkExaminer3.Enabled = Ex_Final3 == 1 ? false : true;
                    chkExaminer4.Enabled = Ex_Final4 == 1 ? false : true;
                    btndrctApp.Visible = false; ; btnThsApp.Visible = false; btnThsSub.Visible = false; btnSubmit.Visible = false; btnApproval.Visible = false;
                    btnReject.Visible = false; txtRemark.Enabled = false; btnThsReject.Visible = false;
                }

                LabelStatus();
                PhdDocuments();
              

            }
            else
            {
                btnSubmit.Enabled = false;
                objCommon.DisplayMessage("Student is Not Eligible Or Dean Not Final 4 Examiner!! ", this.Page);
            }
        }
        else
        {
            btnSubmit.Enabled = false;
            objCommon.DisplayMessage("Student is Not Eligible Or Dean Not Final 4 Examiner !! ", this.Page);
        }

        if (ViewState["usertype"].ToString() == "1")
        {
            pnlsearch.Visible = false;
            dvgeneral.Visible = true;
            //dvdetails.Visible = true;
            divGeneralInfo.Visible = false;
            pnldetails.Visible = true;
            //dvstudentdetails.Visible = false;           
            dvexaminerdetails.Visible = true;
            dvbuttons.Visible = true;
          
        }
    }

    //--- phd thesis and synopsis documents 
    public void PhdDocuments()
    {
        PhdController objSC = new PhdController();
      DataSet ds = objSC.RetrievePhdTrackerDoumentsDetails(Convert.ToInt32(txtRegNo.ToolTip.ToString()));
      if (ds.Tables[0].Rows.Count > 0)
      {
          lvUpload.DataSource = ds;
          lvUpload.DataBind();
      }
      else
      {
          lvUpload.DataSource = null;
          lvUpload.DataBind();
      }

    }

    //------  Approval and Reject Status --
    private void CheckStatusAppReject()
    {
        //  ---  chk status of 2 examiner final for synopsis 
        //if (Ex_Acc1 == 1 || Ex_Rej1 == 1) { divExaminer1.Visible = true; } else { divExaminer1.Visible = false; }
        //if (Ex_Acc2 == 1 || Ex_Rej2 == 1) { divExaminer2.Visible = true; } else { divExaminer2.Visible = false; }
        //if (Ex_Acc3 == 1 || Ex_Rej3 == 1) { divExaminer3.Visible = true; } else { divExaminer3.Visible = false; }
        //if (Ex_Acc4 == 1 || Ex_Rej4 == 1) { divExaminer4.Visible = true; } else { divExaminer4.Visible = false; }

        if (Ex_Acc1 == 1 && Ex_App1 == 1) { chkExaminer1.Checked = true; } else { chkExaminer1.Checked = false; }
        if (Ex_Acc2 == 1 && Ex_App2 == 1) { chkExaminer2.Checked = true; } else { chkExaminer2.Checked = false; }
        if (Ex_Acc3 == 1 && Ex_App3 == 1) { chkExaminer3.Checked = true; } else { chkExaminer3.Checked = false; }
        if (Ex_Acc4 == 1 && Ex_App4 == 1) { chkExaminer4.Checked = true; } else { chkExaminer4.Checked = false; }

    }

    //----  Accept and Approval Status ---
    private void CheckStatusAccApp()
    {
        //if (Ex_Acc1 == 1 && Ex_App1 == 1) { chkExaminer1.Enabled = false; chkExaminer1.Checked = true; } else { chkExaminer1.Enabled = true; chkExaminer1.Checked = false; }
        //if (Ex_Acc2 == 1 && Ex_App2 == 1) { chkExaminer2.Enabled = false; chkExaminer2.Checked = true; } else { chkExaminer2.Enabled = true; chkExaminer2.Checked = false; }
        //if (Ex_Acc3 == 1 && Ex_App3 == 1) { chkExaminer3.Enabled = false; chkExaminer3.Checked = true; } else { chkExaminer3.Enabled = true; chkExaminer3.Checked = false; }
        //if (Ex_Acc4 == 1 && Ex_App4 == 1) { chkExaminer4.Enabled = false; chkExaminer4.Checked = true; } else { chkExaminer4.Enabled = true; chkExaminer4.Checked = false; }

        //if (Ex_Acc1 == 1 && Ex_App1 == 1) {  chkExaminer1.Checked = true; } else { chkExaminer1.Checked = false; }
        //if (Ex_Acc2 == 1 && Ex_App2 == 1) {  chkExaminer2.Checked = true; } else { chkExaminer2.Checked = false; }
        //if (Ex_Acc3 == 1 && Ex_App3 == 1) {  chkExaminer3.Checked = true; } else { chkExaminer3.Checked = false; }
        //if (Ex_Acc4 == 1 && Ex_App4 == 1) {  chkExaminer4.Checked = true; } else { chkExaminer4.Checked = false; }
    }

    // Accept status  --- 
    private void CheckStatusAccepted()
    {
        //  ---  chk status of 2 examiner final for synopsis 
        if (Ex_Acc1 == 1) { divExaminer1.Visible = true; } else { divExaminer1.Visible = false; }
        if (Ex_Acc2 == 1) { divExaminer2.Visible = true; } else { divExaminer2.Visible = false; }
        if (Ex_Acc3 == 1) { divExaminer3.Visible = true; } else { divExaminer3.Visible = false; }
        if (Ex_Acc4 == 1) { divExaminer4.Visible = true; } else { divExaminer4.Visible = false; }
    }

    //--- Reject Status  ---
    private void CheckStatusReject()
    {
        //  ---  reject student examiner  
        if (Ex_Rej1 == 1) { lblExaminer1.ForeColor = Color.Red; chkExaminer1.Enabled = false; }
        if (Ex_Rej2 == 1) { lblExaminer2.ForeColor = Color.Red; chkExaminer2.Enabled = false; }
        if (Ex_Rej3 == 1) { lblExaminer3.ForeColor = Color.Red; chkExaminer3.Enabled = false; }
        if (Ex_Rej4 == 1) { lblExaminer4.ForeColor = Color.Red; chkExaminer4.Enabled = false; }
    }

    //--- label Status ---- 
    private void LabelStatus()
    {
        lblStatus1.Text = lblStatus2.Text = lblStatus3.Text = lblStatus4.Text = string.Empty;

        //-------------------------------------------------------------------------------------------------------
        if (Ex_ThsRej1 == 1) { lblStatus1.Text = "Thesis Rejected"; lblStatus1.ForeColor = Color.DarkRed; }
           else
              if (Ex_Ths1 == 1) { lblStatus1.Text = "Thesis Approved"; lblStatus1.ForeColor = Color.DarkGreen; }
                else
                  if (Ex_SbThs1 == 1) { lblStatus1.Text = "Thesis Invitation"; lblStatus1.ForeColor = Color.DarkBlue; } 
                   else
                      if (Ex_Rej1 == 1) { lblStatus1.Text = "Synopsis Rejected"; lblStatus1.ForeColor = Color.Crimson; }
                      else
                          if (Ex_Acc1 == 1 && Ex_App1 == 1) { lblStatus1.Text = "Synopsis Approved"; lblStatus1.ForeColor = Color.SeaGreen; }
                         else
                            if (Ex_Acc1 == 1) { lblStatus1.Text = "Synopsis Invitation"; lblStatus1.ForeColor = Color.RoyalBlue; } 
                              else { lblStatus1.Text = string.Empty; }
            //-----------------------------------------------------------------------------------------//

        if (Ex_ThsRej2 == 1) { lblStatus2.Text = "Thesis Rejected"; lblStatus2.ForeColor = Color.DarkRed; }
           else
             if (Ex_Ths2 == 1) { lblStatus2.Text = "Thesis Approved"; lblStatus2.ForeColor = Color.DarkGreen; }
               else
                 if (Ex_SbThs2 == 1) { lblStatus2.Text = "Thesis Invitation"; lblStatus2.ForeColor = Color.DarkBlue; } 
                  else
                     if (Ex_Rej2 == 1) { lblStatus2.Text = "Synopsis Rejected"; lblStatus2.ForeColor = Color.Crimson; }
                     else
                         if (Ex_Acc2 == 1 && Ex_App2 == 1) { lblStatus2.Text = "Synopsis Approved"; lblStatus2.ForeColor = Color.SeaGreen; }
                       else
                             if (Ex_Acc2 == 1) { lblStatus2.Text = "Synopsis Invitation"; lblStatus2.ForeColor = Color.RoyalBlue; } 
                         else { lblStatus2.Text = string.Empty; }
            //---------------------------------------------------------------------------------------------//
        if (Ex_ThsRej3 == 1) { lblStatus3.Text = "Thesis Rejected"; lblStatus3.ForeColor = Color.DarkRed; }
            else
             if (Ex_Ths3 == 1) { lblStatus3.Text = "Thesis Approved"; lblStatus3.ForeColor = Color.DarkGreen; }
              else
                 if (Ex_SbThs3 == 1) { lblStatus3.Text = "Thesis Invitation"; lblStatus3.ForeColor = Color.DarkBlue; } 
               else
                     if (Ex_Rej3 == 1) { lblStatus3.Text = "Synopsis Rejected"; lblStatus3.ForeColor = Color.Crimson; }
                 else
                         if (Ex_Acc3 == 1 && Ex_App3 == 1) { lblStatus3.Text = "Synopsis Approved"; lblStatus3.ForeColor = Color.SeaGreen; }
                   else
                             if (Ex_Acc3 == 1) { lblStatus3.Text = "Synopsis Invitation"; lblStatus3.ForeColor = Color.RoyalBlue; } 
                      else { lblStatus3.Text = string.Empty; }
            //------------------------------------------------------------------------------------------------//
        if (Ex_ThsRej4 == 1) { lblStatus4.Text = "Thesis Rejected"; lblStatus4.ForeColor = Color.DarkRed; }
            else
             if (Ex_Ths4 == 1) { lblStatus4.Text = "Thesis Approved"; lblStatus4.ForeColor = Color.DarkGreen; }
                else
                 if (Ex_SbThs4 == 1) { lblStatus4.Text = "Thesis Invitation"; lblStatus4.ForeColor = Color.DarkBlue; } 
                  else
                     if (Ex_Rej4 == 1) { lblStatus4.Text = "Synopsis Rejected"; lblStatus4.ForeColor = Color.Crimson; }
                    else
                         if (Ex_Acc4 == 1 && Ex_App4 == 1) { lblStatus4.Text = "Synopsis Approved"; lblStatus4.ForeColor = Color.SeaGreen; }
                        else
                             if (Ex_Acc4 == 1) { lblStatus4.Text = "Synopsis Invitation"; lblStatus4.ForeColor = Color.RoyalBlue; } 
                            else { lblStatus4.Text = string.Empty; }
        

       
            if (DrctApproval == 1)
            {
                if (Ex_Final1 == 1) { lblStatus1.Text = "Final Examiner"; lblStatus1.ForeColor = Color.Teal; } else { lblStatus1.Text = string.Empty; }
                if (Ex_Final2 == 1) { lblStatus2.Text = "Final Examiner"; lblStatus2.ForeColor = Color.Teal; } else { lblStatus2.Text = string.Empty; }
                if (Ex_Final3 == 1) { lblStatus3.Text = "Final Examiner"; lblStatus3.ForeColor = Color.Teal; } else { lblStatus3.Text = string.Empty; }
                if (Ex_Final4 == 1) { lblStatus4.Text = "Final Examiner"; lblStatus4.ForeColor = Color.Teal; } else { lblStatus4.Text = string.Empty; }
            }
            //else
            //{
                ////-------------------------------------------------------------------------------------------------------
                //if (Ex_ThsRej1 == 1) { lblStatus1.Text = "Thesis Rejected"; lblStatus1.ForeColor = Color.Red; }
                //else
                //    if (Ex_Ths1 == 1) { lblStatus1.Text = "Thesis Approved"; lblStatus1.ForeColor = Color.Green; }
                //    else
                //        if (Ex_SbThs1 == 1) { lblStatus4.Text = "Thesis Accepted"; lblStatus1.ForeColor = Color.Green; } else { lblStatus1.Text = string.Empty; }
                //-------------------------------------------------------------------------------------------------------
                //if (Ex_ThsRej2 == 1) { lblStatus2.Text = "Thesis Rejected"; lblStatus2.ForeColor = Color.Red; }
                //else
                //    if (Ex_Ths2 == 1) { lblStatus2.Text = "Thesis Approved"; lblStatus2.ForeColor = Color.Green; }
                //    else
                //        if (Ex_SbThs2 == 1) { lblStatus2.Text = "Thesis Accepted"; lblStatus2.ForeColor = Color.Green; } else { lblStatus2.Text = string.Empty; }

                //------------------------------------------------------------------------------------------------------
                //if (Ex_ThsRej3 == 1) { lblStatus3.Text = "Thesis Rejected"; lblStatus3.ForeColor = Color.Red; }
                //else
                //    if (Ex_Ths3 == 1) { lblStatus3.Text = "Thesis Approved"; lblStatus3.ForeColor = Color.Green; }
                //    else
                //        if (Ex_SbThs3 == 1) { lblStatus3.Text = "Thesis Accepted"; lblStatus3.ForeColor = Color.Green; } else { lblStatus3.Text = string.Empty; }

                //-------------------------------------------------------------------------------------------------------
                //if (Ex_ThsRej4 == 1) { lblStatus4.Text = "Thesis Rejected"; lblStatus4.ForeColor = Color.Red; }
                //else
                //    if (Ex_Ths4 == 1) { lblStatus4.Text = "Thesis Approved"; lblStatus4.ForeColor = Color.Green; }
                //    else
                //        if (Ex_SbThs4 == 1) { lblStatus4.Text = "Thesis Accepted"; lblStatus4.ForeColor = Color.Green; } else { lblStatus4.Text = string.Empty; }
           // }


          
        //}
    }
   
    //  ---  CLear All Control 
    private void ClearControl()
    {
        txtIDNo.Text = string.Empty;
        txtRegNo.Text = string.Empty;
        txtEnrollno.Text = string.Empty;
        txtStudentName.Text = string.Empty;
        txtFatherName.Text = string.Empty;
        txtRemark.Text = string.Empty;
        txtDateOfJoining.Text = string.Empty;
        txtFatherName.Text = string.Empty;
        txtResearch.Text = string.Empty;
        //txtSearch.Text = string.Empty;
        txtTotCredits.Text = string.Empty;
       // ddlAdmBatch.SelectedIndex = 0;
        //ddlCoSupervisor.SelectedIndex = 0;
        //ddlDepatment.SelectedIndex = 0;
        ddlNdgc.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        //ddlStatusCat.SelectedIndex = 0;
        ddlSupervisor.SelectedIndex = 0;
        ddlSupervisorrole.SelectedIndex = 0;

        divExaminer1.Visible = true;
        divExaminer2.Visible = true;
        divExaminer3.Visible = true;
        divExaminer4.Visible = true;

        ddlExaminer1.SelectedIndex = 0;
        ddlExaminer2.SelectedIndex = 0;
        ddlExaminer3.SelectedIndex = 0;
        ddlExaminer4.SelectedIndex = 0;

        txtemail1.Text = string.Empty;
        txtemail2.Text = string.Empty;
        txtemail3.Text = string.Empty;
        txtemail4.Text = string.Empty;

        txtMobile1.Text = string.Empty;
        txtMobile2.Text = string.Empty;
        txtMobile3.Text = string.Empty;
        txtMobile4.Text = string.Empty;

        SupervisorExaminer.Visible = false;

    }

    protected void lnkId_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        string url = string.Empty;
        if (Request.Url.ToString().IndexOf("?id=") > 0)
        {
            url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
        }
        else
        {
            url = Request.Url.ToString();
        }
        int idno = Convert.ToInt32(lnk.CommandArgument);
        Session["stuinfoidno"] = idno;
        ShowStudentDetails();
        

        //Response.Redirect(url + "&id=" + lnk.CommandArgument);

    }

    private void bindlist(string category, string searchtext)
    {
        string branchno = "0";
        PhdController objSC = new PhdController();
        DataSet ds = objSC.RetrieveStudentDetailsPHD(searchtext, category, branchno);
        if (ds.Tables[0].Rows.Count > 0)
        {
            //lvStudent.DataSource = ds;
            //lvStudent.DataBind();
            //lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
        }
        //else
            //lblNoRecords.Text = "Total Records : 0";

    }

    // Cancel Student
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControl();
        //Response.Redirect("ACADEMIC/PhdExaminerTracker.aspx");  
    }

    // Dean Accept student for Synopsis
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ViewState["usertype"].ToString() == "1")
        {
            DeanSubmitExaminerInfo();
        }
        else
        {
            objCommon.DisplayMessage("You are not Authorized Only Dean can Submit Details !!", this.Page);
        }
    }
    
    // Dean Accept student for Synopsis ---
    private void DeanSubmitExaminerInfo()
    {
        PhdController objSC = new PhdController();
        Student objS = new Student(); 
        DataTableReader dtr = null;
        try
        {
            int count = 0;
            count += chkExaminer1.Checked == true ? 1 : 0;
            count += chkExaminer2.Checked == true ? 1 : 0;
            count += chkExaminer3.Checked == true ? 1 : 0;
            count += chkExaminer4.Checked == true ? 1 : 0;
            if (DeanAccept == 0 && count != 2)
            {
                objCommon.DisplayMessage("Please Select Two Examiners ", this.Page);
            }
            else
            {
                if (count >= 1 && count <= 2)
                {
                    if (txtprexaminer1.Text != string.Empty && txtprExaminer2.Text != string.Empty && txtprExaminer3.Text != string.Empty && txtprExaminer4.Text != string.Empty)
                    {
                        if (fuEx1.FileName != string.Empty || fuEx2.FileName != string.Empty || fuEx3.FileName != string.Empty || fuEx4.FileName != string.Empty)
                        {

                            dtr = objSC.GetPHDTrackerDetails(Convert.ToInt32(Session["stuinfoidno"].ToString()));
                            if (dtr != null)
                            {
                                if (dtr.Read())
                                {
                                    Ex_Acc1 = Convert.ToInt32(dtr["ACC_STATUS1"] == null ? "0" : dtr["ACC_STATUS1"].ToString());
                                    Ex_Acc2 = Convert.ToInt32(dtr["ACC_STATUS2"] == null ? "0" : dtr["ACC_STATUS2"].ToString());
                                    Ex_Acc3 = Convert.ToInt32(dtr["ACC_STATUS3"] == null ? "0" : dtr["ACC_STATUS3"].ToString());
                                    Ex_Acc4 = Convert.ToInt32(dtr["ACC_STATUS4"] == null ? "0" : dtr["ACC_STATUS4"].ToString());

                                }
                            }

                            if (chkExaminer1.Checked == true)
                            {
                                if (Ex_Acc1 == 0)
                                {
                                    DeanSubmitStudentSynopsis(fuEx1, fuEx1.FileName.ToString(), ddlExaminer1.SelectedItem.Text.ToString()); objS.PhdExaminer1Status = 1; objS.PhdExaminerFile1 = ViewState["EX_FILE1"].ToString();
                                    SendEmail(txtemail1.Text.ToString(), ddlExaminer1.SelectedItem.Text.ToString(), Convert.ToInt32(ddlExaminer1.SelectedValue.ToString()), ViewState["EX_FILE1"].ToString(), "Requesting acceptance to evaluate PhD dissertation as an external examiner");
                                }
                                else { objS.PhdExaminer1Status = 0; objS.PhdExaminerFile1 = null; }
                            }
                            else { objS.PhdExaminer1Status = 0; objS.PhdExaminerFile1 = null; }

                            if (chkExaminer2.Checked == true)
                            {
                                if (Ex_Acc2 == 0)
                                {
                                    DeanSubmitStudentSynopsis(fuEx2, fuEx2.FileName.ToString(), ddlExaminer2.SelectedItem.Text.ToString()); objS.PhdExaminer2Status = 1; objS.PhdExaminerFile2 = ViewState["EX_FILE2"].ToString();
                                    SendEmail(txtemail2.Text.ToString(), ddlExaminer2.SelectedItem.Text.ToString(), Convert.ToInt32(ddlExaminer2.SelectedValue.ToString()), ViewState["EX_FILE2"].ToString(), "Requesting acceptance to evaluate PhD dissertation as an external examiner");
                                }
                                else { objS.PhdExaminer2Status = 0; objS.PhdExaminerFile2 = null; }
                            }
                            else { objS.PhdExaminer2Status = 0; objS.PhdExaminerFile2 = null; }

                            if (chkExaminer3.Checked == true)
                            {
                                if (Ex_Acc3 == 0)
                                {
                                    DeanSubmitStudentSynopsis(fuEx3, fuEx3.FileName.ToString(), ddlExaminer3.SelectedItem.Text.ToString()); objS.PhdExaminer3Status = 1; objS.PhdExaminerFile3 = ViewState["EX_FILE3"].ToString();
                                    SendEmail(txtemail3.Text.ToString(), ddlExaminer3.SelectedItem.Text.ToString(), Convert.ToInt32(ddlExaminer3.SelectedValue.ToString()), ViewState["EX_FILE3"].ToString(), "Requesting acceptance to evaluate PhD dissertation as an external examiner");
                                }
                                else { objS.PhdExaminer3Status = 0; objS.PhdExaminerFile3 = null; }
                            }
                            else { objS.PhdExaminer3Status = 0; objS.PhdExaminerFile3 = null; }


                            if (chkExaminer4.Checked == true)
                            {
                                if (Ex_Acc4 == 0)
                                {
                                    DeanSubmitStudentSynopsis(fuEx4, fuEx4.FileName.ToString(), ddlExaminer4.SelectedItem.Text.ToString()); objS.PhdExaminer4Status = 1; objS.PhdExaminerFile4 = ViewState["EX_FILE4"].ToString();
                                    SendEmail(txtemail4.Text.ToString(), ddlExaminer4.SelectedItem.Text.ToString(), Convert.ToInt32(ddlExaminer4.SelectedValue.ToString()), ViewState["EX_FILE4"].ToString(), "Requesting acceptance to evaluate PhD dissertation as an external examiner");
                                }
                                else { objS.PhdExaminer4Status = 0; objS.PhdExaminerFile4 = null; }
                            }
                            else { objS.PhdExaminer4Status = 0; objS.PhdExaminerFile4 = null; }

                            //objS.PhdExaminer1Status = chkExaminer1.Checked == true ? 1 : 0;
                            //objS.PhdExaminer2Status = chkExaminer2.Checked == true ? 1 : 0;
                            //objS.PhdExaminer3Status = chkExaminer3.Checked == true ? 1 : 0;
                            //objS.PhdExaminer4Status = chkExaminer4.Checked == true ? 1 : 0;

                            objS.IdNo = Convert.ToInt32(txtRegNo.Text);
                            objS.PhdExaminer1 = Convert.ToInt32(ddlExaminer1.SelectedValue);
                            objS.PhdExaminer2 = Convert.ToInt32(ddlExaminer2.SelectedValue);
                            objS.PhdExaminer3 = Convert.ToInt32(ddlExaminer3.SelectedValue);
                            objS.PhdExaminer4 = Convert.ToInt32(ddlExaminer4.SelectedValue);
                            objS.Uano = Convert.ToInt32(Session["userno"].ToString());
                            objS.IPADDRESS = ViewState["ipAddress"].ToString();

                            //====================== Examiner Priority =============//  
                            objS.PhdPriExaminer1 = txtprexaminer1.Text.Trim();
                            objS.PhdPriExaminer2 = txtprExaminer2.Text.Trim();
                            objS.PhdPriExaminer3 = txtprExaminer3.Text.Trim();
                            objS.PhdPriExaminer4 = txtprExaminer4.Text.Trim();

                            int output = Convert.ToInt32(objSC.SubmitPhdExaminerDetails(objS));
                            if (output == 1)
                            {
                                objCommon.DisplayMessage("Dean Submited Examiner Details Successfully!", this.Page);
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
                            objCommon.DisplayMessage("Please Select File To Upload", this.Page);
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage("Please Enter All Examiner Priority", this.Page);
                    }
                }
                else
                {
                    objCommon.DisplayMessage("Please Select Examiner But Not More Than Two", this.Page);
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

    //  synopsis ,Thesis File Upload CODE 
    private void DeanSubmitStudentSynopsis(FileUpload fupload, string file, string dcname)
    {
        //FileUpload fupload = new FileUpload();
        //fupload.ID = "fuEx" + id;          
        string rollno = string.Empty;
        string DOCNAME = string.Empty, FILENAME = string.Empty;
        //string path = "d:\\temp\\PHD\\DOCUMENTS\\";
        string path = System.Configuration.ConfigurationManager.AppSettings["PHD_DOCUMENTS"].ToString();

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

                if (fuEx1.HasFile) { ViewState["EX_FILE1"] = FileString.ToString(); } else { ViewState["EX_FILE1"] = null; }
                if (fuEx2.HasFile) { ViewState["EX_FILE2"] = FileString.ToString(); } else { ViewState["EX_FILE2"] = null; }
                if (fuEx3.HasFile) { ViewState["EX_FILE3"] = FileString.ToString(); } else { ViewState["EX_FILE3"] = null; }
                if (fuEx4.HasFile) { ViewState["EX_FILE4"] = FileString.ToString(); } else { ViewState["EX_FILE4"] = null; }
            }
        }
        catch (Exception ex) { }
    }

    // dean reject student 
    protected void btnReject_Click(object sender, EventArgs e)
    {
        if (ViewState["usertype"].ToString() == "1")
        {
            if (txtRemark.Text != "")
            {
                DeanRejectExaminerInfo("SYN");
            }
            else
            {
                objCommon.DisplayMessage("Please Insert Remark for Cancellation from Dean!!", this.Page);
            }
        }
        else
        {
            objCommon.DisplayMessage("You are not Authorized for Reject Examiner !!", this.Page);
        }
    }

    // Dean Accept student for Synopsis ---
    private void DeanRejectExaminerInfo(string Status)
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
            if (count >= 1 && count <= 2)
            {
                objS.PhdExaminer1Status = chkExaminer1.Checked == true ? 1 : 0;
                objS.PhdExaminer2Status = chkExaminer2.Checked == true ? 1 : 0;
                objS.PhdExaminer3Status = chkExaminer3.Checked == true ? 1 : 0;
                objS.PhdExaminer4Status = chkExaminer4.Checked == true ? 1 : 0;

                objS.IdNo = Convert.ToInt32(txtRegNo.Text);
                objS.Uano = Convert.ToInt32(Session["userno"].ToString());
                objS.IPADDRESS = ViewState["ipAddress"].ToString();
                objS.Remark = txtRemark.Text.ToString();
                int output = Convert.ToInt32(objSC.RejectPhdExaminerDetails(objS, Status));
                if (output == 1)
                {
                    objCommon.DisplayMessage("Examiner Rejected Successfully!", this.Page);
                    btnSubmit.Enabled = false;
                    ShowStudentDetails();
                }
                else
                {
                    objCommon.DisplayMessage("Examiner Not Rejected ", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage("Please Select Examiner But Not More Than Two", this.Page);
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

    // Dean Approval Student 
    protected void btnApproval_Click(object sender, EventArgs e)
    {
        if (ViewState["usertype"].ToString() == "1")
        {
            DeanApproveExaminer();
        }
        else
        {
            objCommon.DisplayMessage("You are not Authorized Only Dean Can Approved Examiner !!", this.Page);
        }
    }

    // Dean Approve  student for Synopsis ---
    private void DeanApproveExaminer()
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
            if (count >= 1 && count <= 2)
            {
                objS.PhdExaminer1Status = chkExaminer1.Checked == true ? 1 : 0;
                objS.PhdExaminer2Status = chkExaminer2.Checked == true ? 1 : 0;
                objS.PhdExaminer3Status = chkExaminer3.Checked == true ? 1 : 0;
                objS.PhdExaminer4Status = chkExaminer4.Checked == true ? 1 : 0;

                objS.IdNo = Convert.ToInt32(txtRegNo.Text);
                objS.Uano = Convert.ToInt32(Session["userno"].ToString());
                objS.IPADDRESS = ViewState["ipAddress"].ToString();

                int output = Convert.ToInt32(objSC.ApprovePhdExaminerByDean(objS));
                if (output == 1)
                {
                    objCommon.DisplayMessage("Examiners Approved Successfully!", this.Page);
                    btnSubmit.Enabled = false;
                    ShowStudentDetails();
                }
                else
                {
                    objCommon.DisplayMessage("Examiner Not Approved!! FIrst Accept Examiner . ", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage("Please Select Examiner But Not More Than Two", this.Page);
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

    // Dean Thesis Invitation 
    protected void btnThsSub_Click(object sender, EventArgs e)
    {
        if (ViewState["usertype"].ToString() == "1")
        {
            if (SupThsStatus > 0)
            {
                DeanSubThesisForExaminer();
            }
            else
            {
                objCommon.DisplayMessage("Thesis Invitation is Pending.Because Supervisior Not Submited Thesis.  !!", this.Page);
            }
        }
        else
        {
            objCommon.DisplayMessage("You are not Authorized For Thesis Invitation !!", this.Page);
        }
    }

    // Dean thesis Invitation ---
    private void DeanSubThesisForExaminer()
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

          //  count >= 1 &&
            if ( count <= 2)
            {
                if (fuEx1.FileName != string.Empty || fuEx2.FileName != string.Empty || fuEx3.FileName != string.Empty || fuEx4.FileName != string.Empty)
                {
                    if (chkExaminer1.Checked == true) { DeanSubmitStudentSynopsis(fuEx1, fuEx1.FileName.ToString(), ddlExaminer1.SelectedItem.Text.ToString()); objS.PhdExaminer1Status = 1; objS.PhdExaminerFile1 = ViewState["EX_FILE1"].ToString(); SendEmailThesis(txtemail1.Text.ToString(), ddlExaminer1.SelectedItem.Text.ToString(), Convert.ToInt32(ddlExaminer1.SelectedValue.ToString()), ViewState["EX_FILE1"].ToString(), "Request for dissertation evaluation report (Thesis Invitation)"); } else { objS.PhdExaminer1Status = 0; objS.PhdExaminerFile1 = null; }
                    if (chkExaminer2.Checked == true) { DeanSubmitStudentSynopsis(fuEx2, fuEx2.FileName.ToString(), ddlExaminer2.SelectedItem.Text.ToString()); objS.PhdExaminer2Status = 1; objS.PhdExaminerFile2 = ViewState["EX_FILE2"].ToString(); SendEmailThesis(txtemail2.Text.ToString(), ddlExaminer2.SelectedItem.Text.ToString(), Convert.ToInt32(ddlExaminer2.SelectedValue.ToString()), ViewState["EX_FILE2"].ToString(), "Request for dissertation evaluation report (Thesis Invitation)"); } else { objS.PhdExaminer2Status = 0; objS.PhdExaminerFile2 = null; }
                    if (chkExaminer3.Checked == true) { DeanSubmitStudentSynopsis(fuEx3, fuEx3.FileName.ToString(), ddlExaminer3.SelectedItem.Text.ToString()); objS.PhdExaminer3Status = 1; objS.PhdExaminerFile3 = ViewState["EX_FILE3"].ToString(); SendEmailThesis(txtemail3.Text.ToString(), ddlExaminer3.SelectedItem.Text.ToString(), Convert.ToInt32(ddlExaminer3.SelectedValue.ToString()), ViewState["EX_FILE3"].ToString(), "Request for dissertation evaluation report (Thesis Invitation)"); } else { objS.PhdExaminer3Status = 0; objS.PhdExaminerFile3 = null; }
                    if (chkExaminer4.Checked == true) { DeanSubmitStudentSynopsis(fuEx4, fuEx4.FileName.ToString(), ddlExaminer4.SelectedItem.Text.ToString()); objS.PhdExaminer4Status = 1; objS.PhdExaminerFile4 = ViewState["EX_FILE4"].ToString(); SendEmailThesis(txtemail4.Text.ToString(), ddlExaminer4.SelectedItem.Text.ToString(), Convert.ToInt32(ddlExaminer4.SelectedValue.ToString()), ViewState["EX_FILE4"].ToString(), "Request for dissertation evaluation report (Thesis Invitation)"); } else { objS.PhdExaminer4Status = 0; objS.PhdExaminerFile4 = null; }

                    objS.IdNo = Convert.ToInt32(txtRegNo.Text);
                    objS.Uano = Convert.ToInt32(Session["userno"].ToString());
                    objS.IPADDRESS = ViewState["ipAddress"].ToString();

                    int output = Convert.ToInt32(objSC.PhdThesisExaminerByDean(objS));
                    if (output == 1)
                    {
                        objCommon.DisplayMessage("Examiner Thesis Invitation Submited Successfully!", this.Page);
                        btnSubmit.Enabled = false;
                        ShowStudentDetails();
                    }
                    else
                    {
                        objCommon.DisplayMessage("Examiner Not Approved ", this.Page);
                    }
                }
                else
                {
                    objCommon.DisplayMessage("Please Select File To Upload", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage("Please Select Examiner", this.Page);
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

    // Dean Thesis Approval 
    protected void btnThsApp_Click(object sender, EventArgs e)
    {
        if (ViewState["usertype"].ToString() == "1")
        {
            DeanApproveThesisForExaminer();
        }
        else
        {
            objCommon.DisplayMessage("You are not Authorized For Thesis Approval !!", this.Page);
        }

    }

    // Dean Accept student for Synopsis ---
    private void DeanApproveThesisForExaminer()
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
            if (count >= 1)
            {
                objS.PhdExaminer1Status = chkExaminer1.Checked == true ? 1 : 0;
                objS.PhdExaminer2Status = chkExaminer2.Checked == true ? 1 : 0;
                objS.PhdExaminer3Status = chkExaminer3.Checked == true ? 1 : 0;
                objS.PhdExaminer4Status = chkExaminer4.Checked == true ? 1 : 0;

                objS.IdNo = Convert.ToInt32(txtRegNo.Text);
                objS.Uano = Convert.ToInt32(Session["userno"].ToString());
                objS.IPADDRESS = ViewState["ipAddress"].ToString();

                int output = Convert.ToInt32(objSC.PhdThesisExaminerApprovalByDean(objS));
                if (output == 1)
                {
                    objCommon.DisplayMessage("Examiner Thesis Invitation Approved Successfully!", this.Page);
                    btnSubmit.Enabled = false;
                    ShowStudentDetails();
                }
                else
                {
                    objCommon.DisplayMessage("Examiner Not Approved ", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage("Please Select Only Two Examiner", this.Page);
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

    // Director Approval 
    protected void btndrctApp_Click(object sender, EventArgs e)
    {
        if (ViewState["usertype"].ToString() == "1")
        {
            DirectorApproveExaminer();
        }
        else
        {
            objCommon.DisplayMessage("You are not Authorized For Final Approval !!", this.Page);
        }
    }

    // Dean Accept student for Synopsis ---
    private void DirectorApproveExaminer()
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
            if (count == 1)
            {
                objS.PhdExaminer1Status = chkExaminer1.Checked == true ? 1 : 0;
                objS.PhdExaminer2Status = chkExaminer2.Checked == true ? 1 : 0;
                objS.PhdExaminer3Status = chkExaminer3.Checked == true ? 1 : 0;
                objS.PhdExaminer4Status = chkExaminer4.Checked == true ? 1 : 0;

                objS.IdNo = Convert.ToInt32(txtRegNo.Text);
                objS.Uano = Convert.ToInt32(Session["userno"].ToString());
                objS.IPADDRESS = ViewState["ipAddress"].ToString();

                int output = Convert.ToInt32(objSC.PhdExaminerDirectorApproval(objS));
                if (output == 1)
                {
                    objCommon.DisplayMessage("Examiner Final Successfully!", this.Page);
                    btnSubmit.Enabled = false;
                    ShowStudentDetails();
                }
                else
                {
                    objCommon.DisplayMessage("Examiner Not Final ", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage("Please Select Only One Examiner", this.Page);
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

    #endregion
    //==== Phd Apply Student Report or External Report ============ 

    #region Report =========
    //=============  APPly Student list 
    protected void btnApply_Click(object sender, EventArgs e)
    {
        ShowReportApplystudent("PhDStudentExaminerDeatils", "Phd_Examiner_Apply_Tracker.rpt");
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

    // ------ Exminer Appointment Letter
    protected void btnExaminerReport_Click(object sender, EventArgs e)
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
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(txtIDNo.Text);
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

    //  ---- Examiner Details Excel   ----- 
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        PhdController objSC = new PhdController();
        DataSet ds = null;
        ds = objSC.RetrievePhdExaminerDetailsExcel(Convert.ToInt32(txtRegNo.Text));

        if (ds.Tables[0].Rows.Count > 0)
        {
            GV.DataSource = ds;
            GV.DataBind();
            this.CallExcel();
        }
        else
        {
            objCommon.DisplayMessage("Record Not Found!!", this.Page);
            return;
        }

    }

    // -- add excel report  ---// 
    private void CallExcel()
    {
        string attachment = "attachment; filename=PhdExaminerDetails " + DateTime.Now.ToShortDateString() + ".xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/vnd.MS-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        GV.HeaderRow.Style.Add("background-color", "#e3ac9a");
        GV.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }

    #endregion

    //======= examiner ---- ----------

    #region Dropdown list  ================
    protected void ddlExaminer1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = objCommon.FillDropDown("ACD_PHD_EXAMINER_MASTER", "MOBILE", "EMAILID", "IDNO= " + Convert.ToInt32(ddlExaminer1.SelectedValue), "idno");
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtMobile1.Text = ds.Tables[0].Rows[0]["MOBILE"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["MOBILE"].ToString();
            txtemail1.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["EMAILID"].ToString();
        }
    }

    protected void ddlExaminer2_SelectedIndexChanged(object sender, EventArgs e)
    {

        DataSet ds = objCommon.FillDropDown("ACD_PHD_EXAMINER_MASTER", "MOBILE", "EMAILID", "IDNO= " + Convert.ToInt32(ddlExaminer2.SelectedValue), "idno");
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtMobile2.Text = ds.Tables[0].Rows[0]["MOBILE"].ToString();
            txtemail2.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
        }
        //if (ddlExaminer1.Enabled == true)
        //{
        //    objCommon.FillDropDownList(ddlExaminer3, "ACD_PHD_EXAMINER_MASTER", "IDNO", "NAME", "IDNO>0 AND IDNO NOT IN (" + IDNOS + ")", "NAME");
        //}
    }

    protected void ddlExaminer3_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = objCommon.FillDropDown("ACD_PHD_EXAMINER_MASTER", "MOBILE", "EMAILID", "IDNO= " + Convert.ToInt32(ddlExaminer3.SelectedValue), "idno");
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtMobile3.Text = ds.Tables[0].Rows[0]["MOBILE"].ToString();
            txtemail3.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
        }
    }

    protected void ddlExaminer4_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = objCommon.FillDropDown("ACD_PHD_EXAMINER_MASTER", "MOBILE", "EMAILID", "IDNO= " + Convert.ToInt32(ddlExaminer4.SelectedValue), "idno");
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtMobile4.Text = ds.Tables[0].Rows[0]["MOBILE"].ToString();
            txtemail4.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
        }
    }
    #endregion


    //=============== SEND MAIL CODE For Synopsis --------
    public void SendEmail(string Emailid, string ExaminerName, int ExaminerNo, string ExaminerFile, string SUB)
    {
        try
        {
            string MSG = string.Empty;

            ua_email = objCommon.LookUp("USER_ACC", "UA_EMAIL", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + "AND UA_TYPE = 1");

            ReportDocument customReport = new ReportDocument();
            string reportPath = string.Empty;

            reportPath = Server.MapPath(@"~,Reports,Academic,rptPhdAppointmentExaminer.rpt".Replace(",", "\\"));
            // ShowReportProvisional("Provisional_Certificate", "rptProvisionalCertificateMtech2.rpt", lblrollno.ToolTip.ToString() == string.Empty ? "0" : lblrollno.ToolTip.ToString());

            customReport.Load(reportPath);
            TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
            TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
            ConnectionInfo crConnectionInfo = new ConnectionInfo();
            Tables CrTables;

            crConnectionInfo.ServerName = System.Configuration.ConfigurationManager.AppSettings["Server"];
            crConnectionInfo.DatabaseName = System.Configuration.ConfigurationManager.AppSettings["DataBase"];
            crConnectionInfo.UserID = System.Configuration.ConfigurationManager.AppSettings["UserID"];
            crConnectionInfo.Password = System.Configuration.ConfigurationManager.AppSettings["Password"];

            CrTables = customReport.Database.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
            {
                crtableLogoninfo = CrTable.LogOnInfo;
                crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                CrTable.ApplyLogOnInfo(crtableLogoninfo);
            }

            //Parameter to Report Document
            //================================
            //Extract Parameters From queryString
            customReport.SetParameterValue("@P_COLLEGE_CODE", Session["colcode"].ToString());
            customReport.SetParameterValue("@P_IDNO", Convert.ToInt32(txtRegNo.Text.ToString()));
            //customReport.SetParameterValue("@P_UANO", ExaminerNo);

            string PHD_REPORTS = System.Configuration.ConfigurationManager.AppSettings["PHD_REPORTS"].ToString();
            string PHD_DOCUMENTS = System.Configuration.ConfigurationManager.AppSettings["PHD_DOCUMENTS"].ToString();

            //var directoryInfo = new DirectoryInfo("d:\\temp\\PHD\\REPORT\\");

            //customReport.ExportToDisk(ExportFormatType.PortableDocFormat, "d:\\temp\\PHD\\REPORT\\" + ExaminerName + "_AppointmentLetter" + ".pdf");
            var directoryInfo = new DirectoryInfo(PHD_REPORTS);

            customReport.ExportToDisk(ExportFormatType.PortableDocFormat, PHD_REPORTS + ExaminerName + "_AppointmentLetter" + ".pdf");


            string EmailTemplate = "<html><body>" +
                         "<div align=\"center\">" +
                         "<table style=\"width:602px;border:#DB0F10 3px solid\" cellspacing=\"0\" cellpadding=\"0\">" +
                          "<tr>" +
                          "<td>" + "</tr>" +
                          "<tr>" +
                         "<td width=\"100%\" style=\"vertical-align:top;text-align:left;padding:20px 15px 20px 15px;height:200px;FONT-FAMILY: Verdana;FONT-SIZE: 12px\">#content</td>" +
                         "</tr>" +
                         "<tr>" +
                         "<td width=\"100%\" style=\"vertical-align:middle;text-align:left;padding:20px 15px 20px 15px;height:100px;FONT-FAMILY: Verdana;FONT-SIZE: 11px\"><b>Dean Academics<br/>NIT Raipur<br/><br/>Note: You are requested to reply back at (deanacad@nitrr.ac.in)</td>" +
                         "</tr>" +
                         "</table>" +
                         "</div>" +
                         "</body></html>";
            System.Text.StringBuilder mailBody = new System.Text.StringBuilder();
            mailBody.AppendFormat("<h2>Greetings from  Crescent!!</h2>");
            mailBody.AppendFormat("Dear " + ExaminerName + ",");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("<p>I am writing to ask for your willingness to serve as an external examiner for a PhD dissertation of (Name :" + txtStudentName.Text +  " ). </p>");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("<P> The synopsis of the dissertation has been attached for your kind perusal. The dissertation belongs to the domain closely related to your field of expertise. Upon receiving your acceptance, we will mail you the dissertation and will post the hard copy as well.</P>");
            mailBody.AppendFormat("<br/>");
            mailBody.AppendFormat("<p>As we would appreciate hearing from you at the earliest, Could you kindly confirm at (deanacad@nitrr.ac.in) your acceptance of reviewing the PhD Dissertation as External Examiner? In case of your non-availability, let us know that too.</p>");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("<p>I am very thankful for your kind cooperation. I look forward to hearing from you and am happy to answer any queries you may have relating to the dissertation examination process.</p>");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("<p>With regards, </p>");
            mailBody.AppendFormat("<br />");
            string Mailbody = mailBody.ToString();
            string nMailbody = EmailTemplate.Replace("#content", Mailbody);
            MailMessage msg = new MailMessage();
            var fromAddress = "noreply.mis@nitrr.ac.in";
            msg.From = new MailAddress(fromAddress, "CRESCENT");
            msg.From = new MailAddress(HttpUtility.HtmlEncode("noreply.mis@nitrr.ac.in"));
            msg.To.Add(Emailid);
            msg.Body = nMailbody;
            //msg.Attachments.Add(new Attachment("d:\\temp\\PHD\\REPORT\\" + ExaminerName + "_AppointmentLetter" + ".pdf"));
            //msg.Attachments.Add(new Attachment("d:\\temp\\PHD\\DOCUMENTS\\" + ExaminerFile));
            msg.Attachments.Add(new Attachment(PHD_REPORTS + ExaminerName + "_AppointmentLetter" + ".pdf"));
            msg.Attachments.Add(new Attachment(PHD_DOCUMENTS + ExaminerFile));

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
            msg.Attachments.Dispose();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "AdmitCard.btnSendEmail_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //=============== SEND MAIL CODE For THESIS INVITATION --------
    public void SendEmailThesis(string Emailid, string ExaminerName, int ExaminerNo, string ExaminerFile, string SUB)
    {
        try
        {
            string MSG = string.Empty;

            ua_email = objCommon.LookUp("USER_ACC", "UA_EMAIL", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + "AND UA_TYPE = 4");
            string PHD_DOCUMENTS = System.Configuration.ConfigurationManager.AppSettings["PHD_DOCUMENTS"].ToString();

            string EmailTemplate = "<html><body>" +
                         "<div align=\"center\">" +
                         "<table style=\"width:602px;border:#DB0F10 3px solid\" cellspacing=\"0\" cellpadding=\"0\">" +
                          "<tr>" +
                          "<td>" + "</tr>" +
                          "<tr>" +
                         "<td width=\"100%\" style=\"vertical-align:top;text-align:left;padding:20px 15px 20px 15px;height:200px;FONT-FAMILY: Verdana;FONT-SIZE: 12px\">#content</td>" +
                         "</tr>" +
                         "<tr>" +
                         "<td width=\"100%\" style=\"vertical-align:middle;text-align:left;padding:20px 15px 20px 15px;height:100px;FONT-FAMILY: Verdana;FONT-SIZE: 11px\"><b>Dean Academics<br/>NIT Raipur<br/><br/>Note: You are requested to communicate your responses at dean mail id and also send the hard copy of the evaluation report along with signed bill in the attached envelope. I will be happy to answer any queries you may have relating to the dissertation examination process.</td>" +
                         "</tr>" +
                         "</table>" +
                         "</div>" +
                         "</body></html>";
            System.Text.StringBuilder mailBody = new System.Text.StringBuilder();
            mailBody.AppendFormat("<h1>Greetings from NIT Raipur!!</h1>");
            mailBody.AppendFormat("Dear" + ExaminerName +",");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("<p>In accordance with our PhD Ordinance, earlier I have sent you an Invitation to evaluate the PhD dissertation of  (Name :" + txtStudentName.Text + " , Roll No. : " + txtRegNo.Text + " , Department :" + ddlDepatment.SelectedItem.Text + " ). </p>");
            mailBody.AppendFormat("<P>The copy of the dissertation is attached for your kind perusal along with the evaluate report format to be filled and submitted by you. An additional hard copy of the dissertation will be sent to your address through post.  <P/>");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("<p>You are requested to submit the detailed assessment report along with the recommendation in the attached format as per your convenience, but not later than 6 Weeks.  A copy of the review report will be provided to the student for further necessary actions. Should the reviews approve the dissertation to proceed to examination, as per the PhD Ordinance, the scholar is required to present an oral defense. </p>");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("<P>Dear Professor, also attached is the claim form for honorium of INR 5,000=00 for examining the dissertation which needs to be duly filled and sent along with the report.  <P/>");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("<P>I am thankful for your kind cooperation. Awaiting your reply at the earliest as per your convenience at (deanacad@nitrr.ac.in),<P/>"); 
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
            //  msg.Attachments.Add(new Attachment("d:\\temp\\PHD\\REPORT\\" + ExaminerName + "_AppointmentLetter" + ".pdf"));
            //msg.Attachments.Add(new Attachment("d:\\temp\\PHD\\DOCUMENTS\\" + ExaminerFile));
            msg.Attachments.Add(new Attachment(PHD_DOCUMENTS + ExaminerFile));
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
            msg.Attachments.Dispose();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "AdmitCard.btnSendEmail_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
    // ============  PhD Synopsis and Thesis Douments  ====== //

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

    #endregion

    protected void btnThsReject_Click(object sender, EventArgs e)
    {
        if (ViewState["usertype"].ToString() == "1")
        {
            if (txtRemark.Text != "")
            {
                DeanRejectExaminerInfo("THS");
            }
            else
            {
                objCommon.DisplayMessage("Please Insert Remark for Cancellation from Dean!!", this.Page);
            }
        }
        else
        {
            objCommon.DisplayMessage("You are not Authorized for Reject Examiner !!", this.Page);
        }
    }

    private void bindliststudent(string category, string searchtext)
    {

        StudentController objSC = new StudentController();
        DataSet ds = objSC.RetrieveStudentDetailsNewForPHDOnly(searchtext, category);

        if (ds.Tables[0].Rows.Count > 0)
        {
            pnlLV.Visible = true;
            liststudent.Visible = true;
            liststudent.DataSource = ds;
            liststudent.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), liststudent);//Set label 
            //lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            //lblNoRecords.Text = "Total Records : 0";
            liststudent.Visible = false;
            liststudent.DataSource = null;
            liststudent.DataBind();
        }
    }




    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            pnlLV.Visible = false;
            //lblNoRecords.Visible = false;
            liststudent.DataSource = null;
            liststudent.DataBind();
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
                        txtsearchstu.Visible = false;
                        pnlDropdown.Visible = true;

                        divtxt.Visible = false;
                        lblDropdown.Text = ddlSearch.SelectedItem.Text;
                        objCommon.FillDropDownList(ddlDropdown, tablename, column1, column2, column1 + ">0", column1);

                    }
                    else
                    {
                        pnltextbox.Visible = true;
                        divtxt.Visible = true;
                        txtsearchstu.Visible = true;
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
    }
    protected void btnsearchstu_Click(object sender, EventArgs e)
    {
        string value = string.Empty;
        if (ddlDropdown.SelectedIndex > 0)
        {
            value = ddlDropdown.SelectedValue;
        }
        else
        {
            value = txtsearchstu.Text;
        }


        bindliststudent(ddlSearch.SelectedItem.Text, value);
        ddlDropdown.ClearSelection();
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
}



