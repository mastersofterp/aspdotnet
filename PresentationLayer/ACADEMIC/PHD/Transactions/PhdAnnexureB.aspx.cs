//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : PHD ANNEXURE-B                                                  
// CREATION DATE : 25-APRIL-2013                                                          
// CREATED BY    : ASHISH DHAKATE                             
// MODIFIED DATE :                 
// ADDED BY      :  Dipali Nanore                                
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
using System.Net;


public partial class Academic_StudentInfoEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string Sessionannexurb = string.Empty;
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
        //imgPhoto.ImageUrl = "~/images/nophoto.jpg";
        //Page.ClientScript.RegisterClientScriptInclude("selective", ResolveUrl(@"..\includes\prototype.js"));
        //Page.ClientScript.RegisterClientScriptInclude("selective1", ResolveUrl(@"..\includes\scriptaculous.js"));
        //Page.ClientScript.RegisterClientScriptInclude("selective2", ResolveUrl(@"..\includes\modalbox.js"));

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
            pnlmain.Visible = false;
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                //Populate all the DropDownLists
                //  ---------------------------------
                //FillDropDown();

                if (ViewState["action"] == null)
                    ViewState["action"] = "add";
                this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME", "ID > 0 AND IS_FEE_RELATED = 0", "SRNO");
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));

                Sessionannexurb = Session["currentsession"].ToString();
                ViewState["Sessionannexurb"] = Sessionannexurb;

                ViewState["usertype"] = ua_type;
                if (ViewState["usertype"].ToString() == "2")
                {
                    pnlId.Visible = false;
                    //dvApproved.Visible = false;
                    trApproved.Visible = false;
                    //imgCalDateOfBirth.Visible = false;
                    ShowStudentDetails();

                    //ShowSignDetails();
                    ViewState["action"] = "edit";

                }
                else
                {

                    string ua_type_fac = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                    if (ua_type_fac == "3" || ua_type_fac == "1")
                    {
                        //pnlDoc.Enabled = false;
                        pnlId.Enabled = false;
                        btnReport.Visible = false;
                        //dvApproved.Visible = true;
                        trApproved.Visible = true;
                        trApproved.Visible = false;
                        //divdetails.Visible = false;
                        pnlstudinfo.Visible = false;
                        pnlinfo.Visible = false;
                    }

                    pnlId.Visible = true;
                    lblRegNo.Enabled = true;
                    btnReport.Visible = false;

                    if (Request.QueryString["id"] != null)
                    {

                        ViewState["action"] = "edit";
                        ShowStudentDetails();
                        //ShowSignDetails();

                    }
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
                    txtSearch.Text = string.Empty;
                    lvStudent.DataSource = null;
                    lvStudent.DataBind();
                    lblNoRecords.Text = string.Empty;
                }
            }
        }
    }

    //private void FillDropDown()
    //{
    //    try
    //    {
    //    objCommon.FillDropDownList(ddlDepatment, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO=6", "BRANCHNO");
    //    objCommon.FillDropDownList(ddlSupervisor, "ACD_PHD_SUPERVISOR", "SUPERVISORNO", "SUPERVISORNAME", "SUPERVISORNO>0", "SUPERVISORNO");
    //    objCommon.FillDropDownList(ddlCoSupervisor, "ACD_PHD_SUPERVISOR", "SUPERVISORNO", "SUPERVISORNAME", "SUPERVISORNO>0", "SUPERVISORNO");
    //    objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>10", "BATCHNO");
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Academic_PhdAnnexure.FillDropDown-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    private void ChangeControlStatus(bool status)
    {

        foreach (Control c in Page.Controls)
            foreach (Control ctrl in c.Controls)

                if (ctrl is TextBox)

                    ((TextBox)ctrl).Enabled = status;

                else if (ctrl is Button)

                    ((Button)ctrl).Enabled = status;

                else if (ctrl is RadioButton)

                    ((RadioButton)ctrl).Enabled = status;

                else if (ctrl is ImageButton)

                    ((ImageButton)ctrl).Enabled = status;

                else if (ctrl is CheckBox)

                    ((CheckBox)ctrl).Enabled = status;

                else if (ctrl is DropDownList)

                    ((DropDownList)ctrl).Enabled = status;

                else if (ctrl is HyperLink)

                    ((HyperLink)ctrl).Enabled = status;

    }

    private void ShowStudentDetails()
    {

        StudentController objSC = new StudentController();
        DataTableReader dtr = null;

        if (ViewState["usertype"].ToString() == "2")
        {
            string count = objCommon.LookUp("ACD_PHD_STUD_ANNEXUREB", "COUNT(IDNO)", "IDNO=" + Convert.ToInt32(Convert.ToInt32(Session["idno"])) + " AND isnull(STUD_ELIGIBIL_STATUS,0)=1 AND isnull(ELIGIBIL_APROVED_STATUS,0) = 1");

            if (Convert.ToInt32(count) > 0)
            {
                dtr = objSC.GetStudentPHDDetails(Convert.ToInt32(Session["idno"]));
            }
            else
            {
                objCommon.DisplayMessage(this,"Your Not Submited Eligibility Form Or Your Eligibility Approval Is Pending !!", this.Page);
            }

            ///  dtr = objSC.GetStudentPHDDetails(Convert.ToInt32(Session["idno"]));
        }
        else
        {
            dtr = objSC.GetStudentPHDDetails(Convert.ToInt32(Session["stuinfoidno"]));
            pnDisplay.Enabled = true;
        }
        if (dtr != null)
        {
            if (dtr.Read())
            {
                txtIDNo.Text = dtr["IDNO"].ToString();
                //txtIDNo.ToolTip = dtr["REGNO"].ToString();
                lblRegNo.ToolTip = dtr["IDNO"].ToString();
                lblEnrollNo.ToolTip = dtr["ENROLLNO"].ToString();
                lblEnrollNo.Text = dtr["ENROLLNO"].ToString();
                lblRegNo.Text = dtr["IDNO"].ToString();
                //txtRegNo.Enabled = false;
                lblStudName.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                lblFatherName.Text = dtr["FATHERNAME"] == null ? string.Empty : dtr["FATHERNAME"].ToString().ToUpper();
                lblDateOfJoining.Text = dtr["ADMDATE"] == DBNull.Value ? "" : Convert.ToDateTime(dtr["ADMDATE"]).ToString("dd/MM/yyyy");
                lblBranch.Text = dtr["BRANCHNAME"] == null ? string.Empty : dtr["BRANCHNAME"].ToString();
                lblBranch.ToolTip = dtr["BRANCHNO"] == null ? string.Empty : dtr["BRANCHNO"].ToString();
                lblStatus.Text = dtr["PHDSTATUS"] == null ? string.Empty : dtr["PHDSTATUS"].ToString();
                //Added on 03/12/2015 by Chetan
                if (Session["usertype"].ToString() != "2")
                {
                    txtApprovedDate.Text = dtr["APPROVED_DATE"] == null ? string.Empty : dtr["APPROVED_DATE"].ToString();
                    ddlComplete.SelectedValue = dtr["COURSECOMP"] == null ? string.Empty : dtr["COURSECOMP"].ToString(); //Added on 03/12/2015 by Chetan
                }
                if (lblStatus.Text == "2")
                {
                    lblStatus.Text = "PART TIME";
                }
                else
                {
                    lblStatus.Text = "FULL TIME";
                }

                lblSupervisor.Text = dtr["PHDSUPERVISORNAME"] == null ? string.Empty : dtr["PHDSUPERVISORNAME"].ToString();
                lblSupervisor.ToolTip = dtr["PHDSUPERVISORNO"] == null ? string.Empty : dtr["PHDSUPERVISORNO"].ToString();
                txtAttempt1Date.Text = dtr["ATTEMPT1DATE_WRITTEN"] == null ? string.Empty : dtr["ATTEMPT1DATE_WRITTEN"].ToString();

                //*************************************************************
                if (txtAttempt1Date.Text != String.Empty || txtAttempt1Date.Text != "") //Added on 03/12/15 by Chetan
                {
                    txtAttempt1Date.Enabled = false;
                }
                txtAttempt2Date.Text = dtr["ATTEMPT2DATE_WRITTEN"] == null ? string.Empty : dtr["ATTEMPT2DATE_WRITTEN"].ToString();
                if (txtAttempt2Date.Text != String.Empty || txtAttempt2Date.Text != "") //Added on 03/12/15 by Chetan
                {
                    txtAttempt1Date.Enabled = false;
                    txtAttempt2Date.Enabled = false;
                }
                txtOralAttempt1Date.Text = dtr["ATTEMPT1DATE_ORAL"] == null ? string.Empty : dtr["ATTEMPT1DATE_ORAL"].ToString();
                if (txtOralAttempt1Date.Text != String.Empty || txtOralAttempt1Date.Text != "") //Added on 03/12/15
                {
                    txtOralAttempt1Date.Enabled = false;
                }
                txtOralAttempt2Date.Text = dtr["ATTEMPT2DATE_ORAL"] == null ? string.Empty : dtr["ATTEMPT2DATE_ORAL"].ToString();
                if (txtOralAttempt2Date.Text != String.Empty || txtOralAttempt2Date.Text != "") //Added on 03/12/15
                {
                    txtOralAttempt1Date.Enabled = false;
                    txtOralAttempt2Date.Enabled = false;
                }
                //*************************************************************
                txtReseachPlan.Text = dtr["RESEARCH_PLAN"] == null ? string.Empty : dtr["RESEARCH_PLAN"].ToString();

                ddlPHDresult.SelectedValue = dtr["RESULT_STATUS"] == null ? string.Empty : dtr["RESULT_STATUS"].ToString();

                ddlPHDResearchplan.SelectedValue = dtr["STATUS"] == null ? string.Empty : dtr["STATUS"].ToString();



                //check the supervisor remark status if  status (remarkstatus) is 1 then show the report button

                int count = Convert.ToInt32(objCommon.LookUp("ACD_PHD_STUD_ANNEXUREB", "count(*)", "IDNO=" + Convert.ToInt32(dtr["IDNO"])));
                if (count > 0)
                {
                    if (!(dtr["STATUS"] is DBNull))
                    {
                        int remarkstatus = Convert.ToInt32(dtr["STATUS"]);
                        if (remarkstatus == 1)
                        {
                            btnReport.Visible = true;
                            btnSubmit.Enabled = false;
                        }
                        else if (remarkstatus == 2)
                        {
                            btnReport.Visible = true;
                            btnSubmit.Enabled = true;
                        }
                        else
                        {
                            btnReport.Visible = false;
                        }
                    }
                    else
                    {
                        btnReport.Visible = false;
                        btnSubmit.Enabled = true;
                    }
                }

            }
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=studentinfoentry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=studentinfoentry.aspx");
        }
    }

    private void ClearControl()
    {
        txtIDNo.Text = string.Empty;
        lblRegNo.Text = string.Empty;
        lblEnrollNo.Text = string.Empty;
        lblFatherName.Text = string.Empty;
        lblStudName.Text = string.Empty;
        lblDateOfJoining.Text = string.Empty;
        lblBranch.Text = string.Empty;
        lblStatus.Text = string.Empty;
        lblSupervisor.Text = string.Empty;
        Session["qualifyTbl"] = null;
        txtAttempt1Date.Text = String.Empty;
        txtAttempt2Date.Text = String.Empty;
        txtOralAttempt1Date.Text = String.Empty;
        txtOralAttempt2Date.Text = String.Empty;
        txtReseachPlan.Text = String.Empty;
        txtAttempt1Date.Enabled = true;
        txtAttempt2Date.Enabled = true;
        txtOralAttempt1Date.Enabled = true;
        txtOralAttempt2Date.Enabled = true;
        txtApprovedDate.Text = string.Empty;
        ddlComplete.ClearSelection();
    }

    protected void lnkId_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        string url = string.Empty;
        if (Request.Url.ToString().IndexOf("&id=") > 0)
        {
            url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
        }
        int idno = Convert.ToInt32(lnk.CommandArgument);
        Session["stuinfoidno"] = idno;
        ShowStudentDetails();
        updEdit.Visible = false;
        pnDisplay.Visible = true;
        lvStudent.Visible = false;
        pnlstudinfo.Visible = true;
        divmain.Visible = true;
        pnlmain.Visible = true;
        pnlinfo.Visible = true;
    }

    private void    bindlist(string category, string searchtext)
    {

        //int dept = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(Session["userno"])));

        //if (dept > 0)
        //{
        //    string branchno = objCommon.LookUp("ACD_BRANCH", "BRANCHNO", "DEGREENO=6 AND DEPTNO=" + dept);


        //    StudentController objSC = new StudentController();
        //    DataSet ds = objSC.RetrieveStudentDetailsPHD(searchtext, category, branchno);

        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        lvStudent.DataSource = ds;
        //        lvStudent.DataBind();
        //        lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
        //    }
        //    else
        //        lblNoRecords.Text = "Total Records : 0";
        //}
        //else
        //{
        string branchno = "0";

        PhdController objSC = new PhdController();
        DataSet ds = objSC.RetrieveStudentDetailsPHD(searchtext, category, branchno);

        if (ds.Tables[0].Rows.Count > 0)
        {
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
        }
        else
            lblNoRecords.Text = "Total Records : 0";
        //}
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControl();
        //Response.Redirect(Request.Url.ToString());
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //Added on 02/12/2015 BY CHETAN RATHI
            btnSubmit.Enabled = false; //Added on 03/12/15
            StudentController objSC = new StudentController();
            Student objS = new Student();
            DataSet ds = null;

            string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
            //Added on 02/12/2015 BY CHETAN RATHI
            //*****************************************

            int idno = Convert.ToInt32(txtIDNo.Text);
            string today_date = DateTime.Now.ToShortDateString();
            if (ua_type == "1")
            {
                ds = objSC.GetPHDAnnexureB_Details_Admin(idno);
            }
            else
            {
                ds = objSC.GetPHDAnnexureB_Details(idno);
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["DEGREENO"].ToString() == "6")
                {
                    DateTime adm_date = Convert.ToDateTime(ds.Tables[0].Rows[0]["ADMDATE"].ToString());
                    string joining_date = adm_date.ToShortDateString();
                    string wt1 = txtAttempt1Date.Text;
                    string wt2 = txtAttempt2Date.Text;
                    string or1 = txtOralAttempt1Date.Text;
                    string or2 = txtOralAttempt2Date.Text;
                    string wd = string.Empty;
                    string fd = string.Empty;
                    string od = string.Empty;
                    if (wt1 != string.Empty)
                    {
                        if (wt2 != string.Empty)
                        {
                            wd = wt2;
                        }
                        else
                        {
                            wd = wt1;
                        }
                    }
                    else if (or1 != string.Empty)
                    {
                        if (or2 != string.Empty)
                        {
                            od = or2;
                        }
                        else
                        {
                            od = or1;
                        }
                    }

                    if (od != string.Empty)
                    {
                        fd = od;
                    }
                    else
                    {
                        fd = wd;
                    }

                    // calculate total month & days
                    //int totmonths = DateDifference(Convert.ToDateTime(joining_date), Convert.ToDateTime(today_date));
                    int totmonths = DateDifference(Convert.ToDateTime(joining_date), Convert.ToDateTime(fd));
                    int flag = 0;



                    //previous condition commented by neha 22March2021
                    //if (Convert.ToInt32(ds.Tables[0].Rows[0]["PHDSTATUS"].ToString()) == 1)
                    //{
                    //    if ((totmonths == Convert.ToInt32(ds.Tables[0].Rows[0]["PHD_FULLTIME_MONTH"].ToString()) && (ViewState["DAY"].ToString() == "0")) || (totmonths < Convert.ToInt32(ds.Tables[0].Rows[0]["PHD_FULLTIME_MONTH"].ToString())))
                    //    {
                    //        flag = 1;
                            
                    //    }
                    //}
                    //else if (Convert.ToInt32(ds.Tables[0].Rows[0]["PHDSTATUS"].ToString()) == 2)
                    //{
                    //    if (totmonths == Convert.ToInt32(ds.Tables[0].Rows[0]["PHD_PARTTIME_MONTH"].ToString()) && (ViewState["DAY"].ToString() == "0") || (totmonths < Convert.ToInt32(ds.Tables[0].Rows[0]["PHD_PARTTIME_MONTH"].ToString())))
                    //    {
                    //        flag = 1;
                    //    }
                    //}

                    //added bye neha 22march2021
                    if (DateTime.Compare(Convert.ToDateTime(ds.Tables[0].Rows[0]["COMP_LAST_DATE"].ToString()), Convert.ToDateTime(fd)) >= 0)
                    {
                        flag = 1;
                    }

                    if (ua_type == "1")
                    {
                        flag = 1;
                    }

                    if (flag == 1)
                    {
                        //if (ds.Tables[0].Rows[0]["SEM1"].ToString() == "PASS" && ds.Tables[0].Rows[0]["SEM2"].ToString() == "PASS")
                        //{
                        // change in credit condition >= 20092016

                        if (ua_type == "1" || (Convert.ToInt32(ds.Tables[0].Rows[0]["CREDITS_GIVEN"].ToString()) <= Convert.ToInt32(ds.Tables[0].Rows[0]["CREDITS_COMPLETED"].ToString())))
                        {
                            ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                            string ua_dec = objCommon.LookUp("User_Acc", "UA_DEC", "UA_NO=" + Convert.ToInt32(Session["userno"]));

                            if (ua_type == "1" || ua_type == "2" || ua_type == "3")
                            {
                                objS.IdNo = Convert.ToInt32(lblRegNo.Text.Trim());
                                if (!txtAttempt1Date.Text.Trim().Equals(string.Empty)) objS.Attempt1DateWritten = Convert.ToDateTime(txtAttempt1Date.Text.Trim());
                                if (!txtAttempt2Date.Text.Trim().Equals(string.Empty))
                                {
                                    objS.Attempt2DateWritten = Convert.ToDateTime(txtAttempt2Date.Text.Trim());
                                }
                                else
                                {
                                    objS.Attempt2DateWritten = null;
                                }
                                if (!txtOralAttempt1Date.Text.Trim().Equals(string.Empty)) objS.Attempt1DateOral = Convert.ToDateTime(txtOralAttempt1Date.Text.Trim());
                                if (!txtOralAttempt2Date.Text.Trim().Equals(string.Empty))
                                {
                                    objS.Attempt2DateOral = Convert.ToDateTime(txtOralAttempt2Date.Text.Trim());
                                }
                                else
                                {
                                    objS.Attempt2DateOral = null;
                                }

                                objS.Workdone = txtReseachPlan.Text;
                                //if (!txtApprovedDate.Text.Trim().Equals(string.Empty)) objS.ApprovedDate = Convert.ToDateTime(txtApprovedDate.Text.Trim());
                                objS.PhdSupervisorNo = Convert.ToInt32(lblSupervisor.ToolTip);
                                objS.CollegeCode = Session["colcode"].ToString();

                                // UPDATE THE ONLY PHD STUDENT DATA THROUGH STUDENT
                                if (ua_type == "1" || ua_type == "2")
                                {
                                    //if (pass == "")//**********
                                    //{
                                    string output = objSC.InsUpdatePHDStudentAnnexureB(objS);
                                    if (output != "-99")
                                    {
                                        Session["qualifyTbl"] = null;
                                        objCommon.DisplayMessage(this,"Student Information Updated Successfully!!", this.Page);
                                        if (ua_type != "1")
                                        {
                                            this.ShowStudentDetails();
                                        }
                                    }
                                    //}
                                    //else
                                    //{
                                    //    objCommon.DisplayMessage("Your Result is not passed!!", this.Page);
                                    //}
                                }
                                // check the supervisor login
                                if ((ua_type == "3" && ua_dec == "0") || ua_type == "1")
                                {
                                    //Check the Status of the student
                                    int chkstudentstatus = 0;
                                    if (ua_type == "1")
                                    {
                                        chkstudentstatus = Convert.ToInt32(objCommon.LookUp("ACD_PHD_STUD_ANNEXUREB", "count(*)", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim())));
                                    }
                                    else
                                    {
                                        chkstudentstatus = Convert.ToInt32(objCommon.LookUp("ACD_PHD_STUD_ANNEXUREB", "count(*)", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()) + " AND isnull(STUD_ELIGIBIL_STATUS,0)=1 AND isnull(ELIGIBIL_APROVED_STATUS,0) = 1 AND RESEARCH_PLAN IS NOT NULL"));
                                    }
                                    //   int chkstudentstatus = Convert.ToInt32(objCommon.LookUp("ACD_PHD_STUD_ANNEXUREB", "count(*)", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim())));

                                    if (chkstudentstatus > 0)
                                    {
                                        if (txtApprovedDate.Text == string.Empty)
                                        {
                                            objCommon.DisplayMessage(this,"Please enter Approved Date", this.Page);
                                            btnSubmit.Enabled = true; //Added on 03/12/15
                                        }
                                        else
                                        {
                                           // string ipaddress = getIPAddress();
                                            string ipaddress = Request.ServerVariables["REMOTE_ADDR"];
                                            //objS.Completecourse = ddlComplete.SelectedValue;
                                            if (!txtApprovedDate.Text.Trim().Equals(string.Empty)) objS.ApprovedDate = Convert.ToDateTime(txtApprovedDate.Text.Trim());
                                            if (ddlPHDresult.SelectedIndex > 0)
                                            {
                                                string output1 = objSC.UpdateSupervisorAnnexureBStatus(objS, Convert.ToInt32(ddlPHDresult.SelectedValue), Convert.ToInt32(ddlPHDResearchplan.SelectedValue), Convert.ToInt32(Session["userno"].ToString()), ipaddress);
                                                if (output1 != "-99")
                                                {
                                                    Session["qualifyTbl"] = null;
                                                    objCommon.DisplayMessage(this,"Information Updated Successfully!!", this.Page);
                                                    this.ShowStudentDetails();
                                                    btnReport.Visible = true;
                                                }
                                            }
                                            else
                                            {
                                                objCommon.DisplayMessage(this,"Please Select Result Status First", this.Page);
                                                btnSubmit.Enabled = true;
                                                return;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        objCommon.DisplayMessage(this,"Student has not forwarded the application to Supervisor", this.Page);
                                        btnSubmit.Enabled = true; //Added on 03/12/15
                                        return;
                                    }
                                }
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(this,"Your credits are not completed!!", this.Page);
                            btnSubmit.Enabled = false; //Added on 03/12/15
                        }
                        //}
                        //else
                        //{
                        //    objCommon.DisplayMessage("You are not passed in either I or II semester!!", this.Page);
                        //    btnSubmit.Enabled = false; //Added on 03/12/15
                        //}
                    }
                    else
                    {
                        objCommon.DisplayMessage(this,"Its too late!! You can not fill this form now!!", this.Page);
                        btnSubmit.Enabled = false; //Added on 03/12/15
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this,"Access Denied!! Only for PHD students!!", this.Page);
                    btnSubmit.Enabled = false; //Added on 03/12/15
                }

            }
            else
            {
                objCommon.DisplayMessage(this,"You can not fill this form!!", this.Page);
                btnSubmit.Enabled = false; //Added on 03/12/15
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

    ////private string getIPAddress()
    ////{


    ////    string str = "";

    ////    System.Net.Dns.GetHostName();

    ////    IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(str);

    ////    IPAddress[] addr = ipEntry.AddressList;

    ////    string IP = addr[addr.Length - 1].ToString();

    ////    return IP;
    ////}

    private void ShowReport(string reportTitle, string rptFileName)
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

    private DataRow GetEditableDataRow(DataTable dt, string value)
    {
        DataRow dataRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["QUALIFYNO"].ToString() == value)
                {
                    dataRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic.GetEditableDataRow() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return dataRow;
    }


    protected void btnReport_Click(object sender, EventArgs e)
    {

        ShowReport("PhdAnnexureBReport", "rptAnnexure-B.rpt");  
    }

    //**************************Added on 02/12/12 by Chetan*********************************************
    private int[] monthDay = new int[12] { 31, -1, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
    private DateTime fromDate;
    private DateTime toDate;
    private int year;
    private int month;
    private int day;

    public int DateDifference(DateTime d1, DateTime d2)
    {
        if (d1 > d2)
        {
            this.fromDate = d2;
            this.toDate = d1;
        }
        else
        {
            this.fromDate = d1;
            this.toDate = d2;
        }

        //Day Calculation
        int increment = 0;
        if (this.fromDate.Day > this.toDate.Day)
        {
            increment = this.monthDay[this.fromDate.Month - 1];
        }

        if (increment == -1)
        {
            if (DateTime.IsLeapYear(this.fromDate.Year))
            {
                increment = 29;
            }
            else
            {
                increment = 28;
            }
        }

        if (increment != 0)
        {
            day = (this.toDate.Day + increment) - this.fromDate.Day;
            increment = 1;
        }
        else
        {
            day = this.toDate.Day - this.fromDate.Day;
        }

        //Month Calculation
        if ((this.fromDate.Month + increment) > this.toDate.Month)
        {
            this.month = (this.toDate.Month + 12) - (this.fromDate.Month + increment);
            increment = 1;
        }
        else
        {
            this.month = (this.toDate.Month) - (this.fromDate.Month + increment);
            increment = 0;
        }

        //Year Calculation
        this.year = this.toDate.Year - (this.fromDate.Year + increment);
        //public int year;
        //public int month;
        //public int day;   
        int totalmonths = 0;
        if (this.year != 0)
        {
            totalmonths = this.year * 12;
            totalmonths += this.month;
        }
        else
        {
            totalmonths += this.month;
        }
        if (this.day > 0)
        {
            ViewState["DAY"] = this.day;
        }
        else if (this.day == 0)
        {
            ViewState["DAY"] = 0;
        }
        return totalmonths;
    }

    protected void ddlPHDresult_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPHDresult.SelectedValue == "1")
        {
            ddlPHDResearchplan.SelectedIndex = 1;
            ddlPHDResearchplan.Enabled = false;
        }
        else if (ddlPHDresult.SelectedValue == "2")
        {
            ddlPHDResearchplan.SelectedIndex = 2;
            ddlPHDResearchplan.Enabled = false;
        }
        else
        {
            ddlPHDResearchplan.SelectedIndex = 0;
            ddlPHDResearchplan.Enabled = true;
        }

    }

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

    protected void btnSearch_Click(object sender, EventArgs e)
        {
        //// Panel1.Visible = true;
        // lblNoRecords.Visible = true;
        // //divbranch.Attributes.Add("style", "display:none");
        // //divSemester.Attributes.Add("style", "display:none");
        // //divtxt.Attributes.Add("style", "display:none");
        // string value = string.Empty;
        // if (ddlDropdown.SelectedIndex > 0)
        // {
        //     value = ddlDropdown.SelectedValue;
        // }
        // else
        // {
        //     value = txtSearch.Text;
        // }

        // //ddlSearch.ClearSelection();

        // bindlist(ddlSearch.SelectedItem.Text, value);
        // ddlDropdown.ClearSelection();
        // txtSearch.Text = string.Empty;
        //// div_Studentdetail.Visible = false;
        // //divMSG.Visible = false;
        // //btnPayment.Visible = false;
        //// btnReciept.Visible = false;
        //// divPreviousReceipts.Visible = false;
        // //if (value == "BRANCH")
        // //{
        // //    divbranch.Attributes.Add("style", "display:block");

        // //}
        // //else if (value == "SEM")
        // //{
        // //    divSemester.Attributes.Add("style", "display:block");
        // //}
        // //else
        // //{
        // //    divtxt.Attributes.Add("style", "display:block");
        // //}

        // //ShowDetails();
        // Panel3.Visible = true;

        Panellistview.Visible = true;

        lblNoRecords.Visible = true;
        //divbranch.Attributes.Add("style", "display:none");
        //divSemester.Attributes.Add("style", "display:none");
        //divtxt.Attributes.Add("style", "display:none");
        string value = string.Empty;
        if (ddlDropdown.SelectedIndex > 0)
            {
            value = ddlDropdown.SelectedValue;
            }
        else
            {
            value = txtSearch.Text;
            }

        //ddlSearch.ClearSelection();

        bindlist(ddlSearch.SelectedItem.Text, value);
        ddlDropdown.ClearSelection();
        //txtSearch.Text = string.Empty;

        }

    protected void btnClose_Click(object sender, EventArgs e)
        {
        Response.Redirect(Request.Url.ToString());

        }
}



