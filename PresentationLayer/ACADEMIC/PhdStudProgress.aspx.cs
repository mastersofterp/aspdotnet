//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : PHD STUDENT PROGRESS                                                     
// CREATION DATE : 20-MARCH-2013                                                          
// CREATED BY    : ASHISH DHAKATE                             
// MODIFIED DATE :                 
// ADDED BY      :                                  
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


public partial class Academic_StudentInfoEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

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
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                   // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                //Populate all the DropDownLists
                //FillDropDown();
                
                if (ViewState["action"] == null)
                    ViewState["action"] = "add";
                
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_IDNO=" + Convert.ToInt32(Session["idno"]));
                

                ViewState["usertype"] = ua_type;
                if (ViewState["usertype"].ToString() == "2")
                {
                    pnlId.Visible = false;
                    dvRemark.Visible = false;
                    //imgCalDateOfBirth.Visible = false;
                    ShowStudentDetails();
                    
                    //ShowSignDetails();
                    ViewState["action"] = "edit";
                    
                }
                else
                {

                    string ua_type_fac = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                    if (ua_type_fac == "6" || ua_type_fac == "4")
                    {
                        //pnlDoc.Enabled = false;
                        pnlId.Enabled = false;
                        btnReport.Visible = false;
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
            dtr = objSC.GetStudentPHDDetails(Convert.ToInt32(Session["idno"]));
           
        }
        else
        {
            dtr = objSC.GetStudentPHDDetails(Convert.ToInt32(Request.QueryString["id"].ToString()));
            pnDisplay.Enabled = true;
        }
        if (dtr != null)
        {
            if (dtr.Read())
            {
                txtIDNo.Text = dtr["IDNO"].ToString();
                //txtIDNo.ToolTip = dtr["REGNO"].ToString();
                lblRegNo.ToolTip = dtr["IDNO"].ToString();
                lblEnrollNo.ToolTip = dtr["ROLLNO"].ToString();
                lblEnrollNo.Text = dtr["ROLLNO"].ToString();
                lblRegNo.Text = dtr["IDNO"].ToString();
                //txtRegNo.Enabled = false;
                lblStudName.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                lblFatherName.Text = dtr["FATHERNAME"] == null ? string.Empty : dtr["FATHERNAME"].ToString().ToUpper();
                lblDateOfJoining.Text = dtr["ADMDATE"] == DBNull.Value ? "" : Convert.ToDateTime(dtr["ADMDATE"]).ToString("dd/MM/yyyy");
                lblBranch.Text = dtr["BRANCHNAME"] == null ? string.Empty : dtr["BRANCHNAME"].ToString();
                lblBranch.ToolTip = dtr["BRANCHNO"] == null ? string.Empty : dtr["BRANCHNO"].ToString();
                lblStatus.Text = dtr["PHDSTATUS"] == null ? string.Empty : dtr["PHDSTATUS"].ToString();
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
                lblCoSupervisor.Text = dtr["PHDCOSUPERVISORNAME"] == null ? string.Empty : dtr["PHDCOSUPERVISORNAME"].ToString();
                lblCoSupervisor.ToolTip = dtr["PHDCOSUPERVISORNO1"] == null ? string.Empty : dtr["PHDCOSUPERVISORNO1"].ToString();
                lblCredits.Text = dtr["CREDITS"] == null ? string.Empty : dtr["CREDITS"].ToString();
                txtReserchTopic.Text = dtr["TOPICS"] == null ? string.Empty : dtr["TOPICS"].ToString();
                txtDescription.Text = dtr["WORKDONE"] == null ? string.Empty : dtr["WORKDONE"].ToString();
                txtRemark.Text = dtr["REMARK"] == null ? string.Empty : dtr["REMARK"].ToString();
                ddlGrade.SelectedValue = dtr["GRADE"] == null ? "0" : dtr["GRADE"].ToString();

                //chack the supervisor remark status if  status (remarkstatus) is 1 then show the report button

                int count = Convert.ToInt32(objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "count(*)", "IDNO=" + Convert.ToInt32(dtr["IDNO"])));
                if (count > 0)
                {
                    int remarkstatus = Convert.ToInt32(dtr["REMARKSTATUS"]);

                    if (remarkstatus == 1)
                    {
                        btnReport.Visible = true;
                    }
                    else
                    {
                        btnReport.Visible = false;
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
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
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
        lblCredits.Text = string.Empty;
        lblCoSupervisor.Text = string.Empty;
        txtReserchTopic.Text = string.Empty;
        txtDescription.Text = string.Empty;
        txtRemark.Text = string.Empty;
        ddlGrade.SelectedIndex = 0;
        Session["qualifyTbl"] = null;
    }

    protected void lnkId_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        string url = string.Empty;
        if (Request.Url.ToString().IndexOf("&id=") > 0)
        {
            url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
        }
        else
        {
            url = Request.Url.ToString();
        }

        Response.Redirect(url + "&id=" + lnk.CommandArgument);

        
    }

    private void bindlist(string category, string searchtext)
    {
        
        int dept = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(Session["userno"])));

        if (dept > 0)
        {
            string branchno = objCommon.LookUp("ACD_BRANCH", "BRANCHNO", "DEGREENO=3 AND DEPTNO=" + dept);


            StudentController objSC = new StudentController();
            DataSet ds = objSC.RetrieveStudentDetailsPHD(searchtext, category, branchno);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvStudent.DataSource = ds;
                lvStudent.DataBind();
                lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
            }
            else
                lblNoRecords.Text = "Total Records : 0";
        }
        else
        {
            string branchno = "0";

            StudentController objSC = new StudentController();
            DataSet ds = objSC.RetrieveStudentDetailsPHD(searchtext, category, branchno);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvStudent.DataSource = ds;
                lvStudent.DataBind();
                lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
            }
            else
                lblNoRecords.Text = "Total Records : 0";
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControl();
        //Response.Redirect(Request.Url.ToString());
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        StudentController objSC = new StudentController();
        Student objS = new Student();

        try
        {
            string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
            string ua_dec = objCommon.LookUp("User_Acc", "UA_DEC", "UA_NO=" + Convert.ToInt32(Session["userno"]));


            if (ua_type == "1" || ua_type == "2" || (ua_type == "3" && ua_dec == "0"))
            {

                objS.IdNo = Convert.ToInt32(txtIDNo.Text);
                objS.EnrollNo = lblEnrollNo.Text.Trim();
                objS.RegNo = lblRegNo.Text.Trim();
                objS.RollNo = lblRegNo.Text.Trim();
                if (!lblStudName.Text.Trim().Equals(string.Empty)) objS.StudName = lblStudName.Text.Trim();
                if (!lblFatherName.Text.Trim().Equals(string.Empty)) objS.FatherName = lblFatherName.Text.Trim();
                if (!lblDateOfJoining.Text.Trim().Equals(string.Empty)) objS.Dob = Convert.ToDateTime(lblDateOfJoining.Text.Trim());
                objS.BranchNo = Convert.ToInt32(lblBranch.ToolTip);
                objS.PhdSupervisorNo = Convert.ToInt32(lblSupervisor.ToolTip);
                objS.PhdCoSupervisorNo1 = Convert.ToInt32(lblCoSupervisor.ToolTip);
                if (!lblCredits.Text.Trim().Equals(string.Empty)) objS.Credits = Convert.ToInt32(lblCredits.Text);
                if (!txtReserchTopic.Text.Trim().Equals(string.Empty)) objS.Topics = txtReserchTopic.Text.Trim();
                if (!txtDescription.Text.Trim().Equals(string.Empty)) objS.Workdone = txtDescription.Text.Trim();
                if (!txtRemark.Text.Trim().Equals(string.Empty)) objS.Remark = txtRemark.Text.Trim();
                objS.Grade = Convert.ToInt32(ddlGrade.SelectedValue);

                objS.CollegeCode = Session["colcode"].ToString();

                //UANO SAVE
                if (ua_type == "1" || ua_type == "3")
                {
                    objS.Uano = Convert.ToInt32(Session["userno"]);
                }
                else
                {
                    objS.Uano = 0;
                }

                // UPDATE THE ONLY PHD STUDENT DATA THROUGH STUDENT
                if (ua_type == "1" || ua_type == "2")
                {
                    string output = objSC.UpdatePHDStudentResult(objS);
                    if (output != "-99")
                    {
                        Session["qualifyTbl"] = null;
                        objCommon.DisplayMessage("Student Information Updated Successfully!!", this.Page);
                        this.ShowStudentDetails();
                    }
                   
                   

                }
                
                // check the supervisor login
                if (ua_type == "3" && ua_dec == "0")
                {
                    //Check the Status of the student

                    int chkstudentstatus = Convert.ToInt32(objCommon.LookUp("ACD_PHD_STUDENT_RESULT", "count(*)", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim())));
                    objS.Grade = Convert.ToInt32(ddlGrade.SelectedValue);

                    if (chkstudentstatus > 0)
                    {
                        if (txtRemark.Text == string.Empty)
                        {
                            objCommon.DisplayMessage("Please Enter Student Remark", this.Page);
                        }
                        if (ddlGrade.SelectedValue == "-1")
                        {
                            objCommon.DisplayMessage("Please Select Grade", this.Page);
                        }
                        else
                        {

                            if (!txtRemark.Text.Trim().Equals(string.Empty)) objS.Remark = txtRemark.Text.Trim();
                            string output1 = objSC.UpdateSupervisorRemarkStatus(objS);
                            if (output1 != "-99")
                            {
                                Session["qualifyTbl"] = null;
                                objCommon.DisplayMessage("Information Updated Successfully!!", this.Page);
                                this.ShowStudentDetails();
                                btnReport.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage("Student not forword the application to Supervisor", this.Page);
                        return;
                    }
                }
            }

            //Check the Status of the Supervisor if status yes to permission for the HODs

            //int chksuperstatus = Convert.ToInt32(objCommon.LookUp("ACD_PHD_DGC", "count(*)", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim())));
            //if (chksuperstatus > 0)
            //{
            //    int chkSupervisorStatus = Convert.ToInt32(objCommon.LookUp("ACD_PHD_DGC", "SUPERVISORSTATUS", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim())));

            //    if (chkSupervisorStatus == 1)
            //    {
            //        if (ua_type == "3" && ua_dec == "1")
            //        {
            //            objS.IdNo = Convert.ToInt32(txtIDNo.Text);
            //            string output = objSC.UpdateHODStatus(objS);
            //            if (output != "-99")
            //            {
            //                objCommon.DisplayMessage("Status Updated Successfully!!", this.Page);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        objCommon.DisplayMessage("Supervisor not forword this student application", this.Page);
            //        return;
            //    }
            //}
            //else
            //{
            //    objCommon.DisplayMessage("Cannot the Assign the DGC members", this.Page);
            //    return;
            //}

            //Update the DRC status by login DGC and admin.

            //if (ua_type == "6" || ua_type == "1" || ua_type == "4")
            //{
            //    objS.IdNo = Convert.ToInt32(txtIDNo.Text);
            //    string output = objSC.UpdateDRCStatus(objS);
            //    if (output != "-99")
            //    {
            //        objCommon.DisplayMessage("Status Updated Successfully!!", this.Page);
            //        ShowReport("PHDAnnexureConfirm", "rptAnnexureConfirmation.rpt");
            //    }
            //}
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
        ShowReport("PhdProgressReport", "rptPhdProgressReport.rpt");
    }
}

   

